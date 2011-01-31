-- =============================================
-- Author:		Nigel Garside
-- Create date: 15/12/2010
-- Description:	Get Client's Asset return by investment or asset class
-- =============================================
ALTER PROCEDURE [dbo].[spClientAssetReturn]

	@ClientGUID uniqueidentifier

AS
BEGIN
SET NOCOUNT ON;

DECLARE @assetCount int;

SET @assetCount = (SELECT COUNT(Amount) FROM tblClientAsset WHERE ClientGUID=@ClientGuid);

IF @assetCount = 0
	exec spClientAssetClassReturn @ClientGUID = @ClientGUID
ELSE
	exec spClientAssetInvestmentReturn @ClientGUID = @ClientGUID

END
