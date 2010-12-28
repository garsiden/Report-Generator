-- =============================================
-- Author:		nigel.garside@gmail.com
-- Create date: 11/12/2010
-- Description:	
-- =============================================

ALTER PROCEDURE dbo.spRawReturn

AS
	/* SET NOCOUNT ON */

WITH f AS (
SELECT ROW_NUMBER() OVER (ORDER BY[Date]) + 1 as rn, CASH,COMM,COPR,GLEQ,HEDG,LOSH,PREQ,UKCB,UKEQ,UKGB,UKHY,WOBO
FROM
(
SELECT [Date], [Value], AssetClassID FROM tblHistoricData
) piv
PIVOT
(
 MAX([Value]) for AssetClassID in (CASH,COMM,COPR,GLEQ,HEDG,LOSH,PREQ,UKCB,UKEQ,UKGB,UKHY,WOBO)
) as chld)

SELECT t.Date,
LOG(t.CASH/f.CASH) AS CASH, Log(t.COMM/f.COMM) AS COMM, Log(t.COPR/f.COPR) AS COPR, LOG(t.GLEQ/f.GLEQ) AS GLEQ,
LOG(t.HEDG/f.HEDG) AS HEDG, LOG(t.LOSH/f.LOSH) AS LOSH, LOG(t.PREQ/f.PREQ) AS PREQ, LOG(t.UKCB/f.UKCB) AS UKCB,
LOG(t.UKEQ/f.UKEQ) AS UKEQ, LOG(t.UKGB/f.UKGB) AS UKGB, LOG(t.UKHY/f.UKHY) AS UKHY, LOG(t.WOBO/f.WOBO) AS WOBO
FROM f,
(SELECT ROW_NUMBER() OVER (ORDER BY[Date])as rn, [Date],
 CASH,COMM,COPR,GLEQ,HEDG,LOSH,PREQ,UKCB,UKEQ,UKGB,UKHY,WOBO
FROM
(
SELECT [Date], [Value], AssetClassID FROM tblHistoricData
) piv
PIVOT
(
 MAX([Value]) for AssetClassID in (CASH,COMM,COPR,GLEQ,HEDG,LOSH,PREQ,UKCB,UKEQ,UKGB,UKHY,WOBO)
) as chld  ) t
WHERE f.rn = t.rn

RETURN
