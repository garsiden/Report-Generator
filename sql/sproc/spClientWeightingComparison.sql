-- Author:		nigel.garside@gmail.com
-- Create date: 19/12/2010
-- Description:	Compares asset weightings from vwClientAssetClassWeighting
-- or vwClientAssetWeighting and selected Model
-- =============================================

ALTER PROCEDURE [dbo].[spClientWeightingComparison] 

	@ClientGUID uniqueidentifier,
	@StrategyID nchar(2)
AS
BEGIN

SET NOCOUNT ON;

DECLARE @assetCount int;

SET @assetCount = (SELECT COUNT(Amount) FROM tblClientAsset WHERE ClientGUID=@ClientGuid);

IF @assetCount = 0
	SELECT c.AssetClassID, c.AssetClass AS AssetClassName,
		CAST(c.Weighting - COALESCE(m.WeightingHNW,0) AS FLOAT) AS WeightingDifferenceHNW,
		CAST(c.Weighting - COALESCE(m.WeightingAffluent,0) AS FLOAT) AS WeightingDifferenceAffluent
		FROM   vwClientAssetClassWeighting c
			LEFT OUTER JOIN
		(SELECT * FROM vwModel WHERE StrategyID=@StrategyID) m ON c.AssetClassID = m.AssetClassID
		WHERE ClientGUID=@ClientGUID
ELSE

	SELECT c.AssetClassID, c.AssetClass AS AssetClassName,
		CAST(c.Weighting - COALESCE(m.WeightingHNW,0) AS FLOAT) AS WeightingDifferenceHNW,
		CAST(c.Weighting - COALESCE(m.WeightingAffluent,0) AS FLOAT) AS WeightingDifferenceAffluent
		FROM   vwClientAssetWeighting c
			LEFT OUTER JOIN
		(SELECT * FROM vwModel WHERE StrategyID=@StrategyID) m ON c.AssetClassID = m.AssetClassID
		WHERE ClientGUID=@ClientGUID


END
