-- =============================================
-- Author:		Nigel Garside
-- Create date: 15/12/2010
-- Description:	Get Client's Asset Weighting by investment or asset class
-- =============================================
ALTER PROCEDURE [dbo].[spClientAssetWeighting]
 
	@ClientGUID uniqueidentifier
AS
BEGIN
SET NOCOUNT ON;

DECLARE @assetCount int;

SET @assetCount = (SELECT COUNT(Amount) FROM tblClientAsset WHERE ClientGUID=@ClientGuid);

IF @assetCount > 0
	SELECT AssetClass, CAST(Weighting AS FLOAT) AS Weighting FROM vwClientAssetWeighting WHERE ClientGUID=@ClientGUID
ELSE
	SELECT AssetClass, CAST(Weighting AS FLOAT) AS Weighting FROM vwClientAssetClassWeighting WHERE ClientGUID=@ClientGUID
END
