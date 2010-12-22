ALTER VIEW [dbo].[vwModel] AS

SELECT tblModel.StrategyID, tblModel.AssetClassID, Sum(tblModel.Weighting) AS Weighting
FROM tblAssetClass INNER JOIN tblModel ON tblAssetClass.ID = tblModel.AssetClassID
GROUP BY tblModel.StrategyID, tblModel.AssetClassID, tblAssetClass.IsGroup
HAVING (((tblAssetClass.IsGroup)=0))
UNION ALL SELECT StrategyID,  AssetClassID, Weighting FROM tblModelBreakdown;
