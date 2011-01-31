ALTER VIEW [dbo].[vwClientAssetWeightingType]
AS
SELECT     AssetClass, CAST(Weighting AS FLOAT) AS Weighting
FROM         dbo.vwClientAssetWeighting
