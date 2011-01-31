-- =============================================
-- Author:		nigel.garside@gmail.com
-- Create date: 11/12/2010
-- Description:	Get raw return for asset class historic data
-- =============================================

ALTER PROCEDURE [dbo].[spRawReturn]

AS

WITH f AS
(
	SELECT ROW_NUMBER() OVER (ORDER BY [Date]) + 1 AS rn,
       CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
	FROM tblHistoricData
)

SELECT  CAST(t .Date AS INT) AS [Date], LOG (t .CASH / f.CASH) AS CASH, LOG (t .COMM / f.COMM) AS COMM, Log (t .COPR / f.COPR) AS COPR, LOG (t .GLEQ / f.GLEQ) AS GLEQ, 
	LOG (t .HEDG / f.HEDG) AS HEDG, LOG (t .LOSH / f.LOSH) AS LOSH, LOG (t .PREQ / f.PREQ) AS PREQ, LOG (t .UKCB / f.UKCB) AS UKCB, 
	LOG (t .UKEQ / f.UKEQ) AS UKEQ, LOG (t .UKGB / f.UKGB) AS UKGB, LOG (t .UKHY / f.UKHY) AS UKHY, LOG (t .WOBO / f.WOBO) AS WOBO
FROM  f,
	(SELECT  ROW_NUMBER() OVER (ORDER BY [Date]) AS rn, [Date], CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
		FROM tblHistoricData) t
WHERE f.rn = t .rn

RETURN
