ALTER VIEW [dbo].[vwClientWeighting] AS
SELECT
COALESCE(CASH, 0) AS CASH,
COALESCE(COMM, 0) AS COMM,
COALESCE(COPR, 0) AS COPR,
COALESCE(GLEQ, 0) AS GLEQ,
COALESCE(HEDG, 0) AS HEDG,
COALESCE(LOSH, 0) AS LOSH,
COALESCE(PREQ, 0) AS PREQ,
COALESCE(UKCB, 0) AS UKCB,
COALESCE(UKEQ, 0) AS UKEQ,
COALESCE(UKGB, 0) AS UKGB,
COALESCE(UKHY, 0) AS UKHY,
COALESCE(WOBO, 0) AS WOBO
FROM (SELECT AssetClassId, Weighting
FROM  tblClientAssetClass WHERE ClientGUID='636c8103-e06d-4575-aafc-574474c2d7f8') piv
 PIVOT ( SUM(Weighting)
FOR AssetClassID IN (CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO)) AS chld
