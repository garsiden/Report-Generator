ALTER VIEW [dbo].[vwClientAssetClassWeighting] AS
SELECT ClientGUID, AssetClassID, [Name] As AssetClass, Weighting
FROM (
	SELECT ClientGUID, CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
	FROM tblClientAssetClass) p
UNPIVOT (
	Weighting FOR AssetClassID IN (
		CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
	)
) AS unpvt, tblAssetClass
WHERE AssetClassID=ID
