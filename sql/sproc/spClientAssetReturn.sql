-- Author:		nigel.garside@gmail.com
-- Create date: 15/12/2010
-- Description:	Calculates return on clients assets in tblClientAssetClass
-- =============================================
ALTER PROCEDURE [dbo].[spClientAssetReturn] 
	@ClientGUID uniqueidentifier = '636c8103-e06d-4575-aafc-574474c2d7f8'
AS
BEGIN

SET NOCOUNT ON;

WITH c AS
(
SELECT CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
FROM (SELECT AssetClassId, Weighting
FROM  tblClientAssetClass WHERE ClientGUID=@ClientGUID) piv
 PIVOT ( SUM(Weighting)
FOR AssetClassID IN (CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO)) AS chld
)

SELECT   CAST([Date] AS INT) AS [Date],
COALESCE((r.CASH * rr.CASH), 0) +
COALESCE((r.COMM * rr.COMM), 0) +
COALESCE((r.HEDG * rr.HEDG), 0) +
COALESCE((r.COPR * rr.COPR), 0) +
COALESCE((r.GLEQ * rr.GLEQ), 0) +
COALESCE((r.LOSH * rr.LOSH), 0) +
COALESCE((r.PREQ * rr.PREQ), 0) +
COALESCE((r.UKCB * rr.UKCB), 0) +
COALESCE((r.UKEQ * rr.UKEQ), 0) +
COALESCE((r.UKGB * rr.UKGB), 0) +
COALESCE((r.UKHY * rr.UKHY), 0) +
COALESCE((r.WOBO * rr.WOBO), 0)
AS [Value] FROM
(SELECT * FROM c) r,
(SELECT * FROM vwRawReturn) rr


END
