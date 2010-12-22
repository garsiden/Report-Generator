ALTER VIEW [dbo].[vwClientAssetWeightingComparison] AS
WITH cw
AS
(
SELECT ID, [Name], COALESCE(c.Weighting,0) AS Weighting FROM tblAssetClass
 LEFT OUTER JOIN
(SELECT * FROM tblClientAssetClass WHERE ClientGUID='636c8103-e06d-4575-aafc-574474c2d7f8') c
 ON tblAssetClass.ID = c.AssetClassID
 WHERE IsGroup = 0
)

SELECT cw.ID, cw.Name, (mw.Weighting - cw.Weighting) AS WeightingDifference FROM cw,
(SELECT ID, [Name], COALESCE(m.Weighting,0) AS Weighting FROM tblAssetClass
 LEFT OUTER JOIN
(SELECT * FROM vwModel WHERE StrategyID='CO') m
 ON tblAssetClass.ID = m.AssetClassID
 WHERE IsGroup = 0) mw
WHERE cw.ID = mw.ID
