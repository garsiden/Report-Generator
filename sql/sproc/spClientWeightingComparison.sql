-- Author:		nigel.garside@gmail.com
-- Create date: 19/12/2010
-- Description:	Compares asset weightings from tblClientAssetClass
-- and selected Model
-- =============================================
ALTER PROCEDURE [dbo].[spClientWeightingComparison] 

	@clientGUID uniqueidentifier = '1426a508-fc66-4e2d-b3cc-b3e1e1240a0e',
	@strategyID nchar(2) = 'CO'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;

WITH cw
AS
(
SELECT ID, [Name], COALESCE(c.Weighting,0) AS Weighting FROM tblAssetClass
 LEFT OUTER JOIN
(SELECT * FROM tblClientAssetClass WHERE ClientGUID=@clientGUID) c
 ON tblAssetClass.ID = c.AssetClassID
 WHERE IsGroup = 0
)

SELECT cw.ID AS AssetClassID, cw.Name AS AssetClassName, CAST((cw.Weighting - mw.Weighting) AS FLOAT) AS WeightingDifference FROM cw,
(SELECT ID, [Name], COALESCE(m.Weighting,0) AS Weighting FROM tblAssetClass
 LEFT OUTER JOIN
(SELECT * FROM vwModel WHERE StrategyID=@strategyID) m
 ON tblAssetClass.ID = m.AssetClassID
 WHERE IsGroup = 0) mw
WHERE cw.ID = mw.ID

END
