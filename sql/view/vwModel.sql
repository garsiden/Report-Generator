ALTER VIEW [dbo].[vwModel] AS

SELECT tblModel.StrategyID, tblModel.AssetClassID, Sum(tblModel.Weighting) AS Weighting
FROM (tblAssetClass INNER JOIN tblModel ON tblAssetClass.ID = tblModel.AssetClassID) LEFT JOIN tblAssetGroup ON tblAssetClass.ID = tblAssetGroup.ID
GROUP BY tblModel.StrategyID, tblModel.AssetClassID, tblAssetGroup.ID
HAVING (((tblAssetGroup.ID) IS NULL))

