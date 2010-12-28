-- =============================================
-- Author:		Nigel Garside
-- Create date: 15/12/2010
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[spClientAssetReturn]
 
-- asset class			'979de312-8e99-49d3-9d41-54ecae0cad5c'
-- asset investments	'636c8103-e06d-4575-aafc-574474c2d7f8'
	@startDate datetime = null,
	@ClientGUID uniqueidentifier = '636c8103-e06d-4575-aafc-574474c2d7f8'
AS
BEGIN
SET NOCOUNT ON;

DECLARE @assetCount int;

SET @assetCount = (SELECT COUNT(Amount) FROM tblClientAsset WHERE ClientGUID=@ClientGuid);

IF @assetCount = 0
	exec spClientAssetClassReturn @ClientGUID = @ClientGUID, @startDate = @startDate
ELSE
	exec spClientAssetInvestmentReturn @ClientGUID = @ClientGUID, @startDate = @startDate
END
