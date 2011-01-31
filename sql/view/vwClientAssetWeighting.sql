ALTER VIEW [dbo].[vwClientAssetWeighting] AS
SELECT ClientGUID, AssetClassID, [Name] As AssetClass, SUM(Weighting) AS Weighting
FROM (
	SELECT ClientGUID, CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
	FROM tblClientAsset) p
UNPIVOT (
	Weighting FOR AssetClassID IN (
		CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
	)
) AS unpvt, tblAssetClass
WHERE AssetClassID=ID
GROUP BY ClientGUID, AssetClassID,[Name]
