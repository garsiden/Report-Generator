-- =============================================
-- Author:		nigel.garside@gmail.com
-- Create date: 11/12/2010
-- Description:	Asset prices for an asset class
-- =============================================

ALTER PROCEDURE dbo.spHistoricPrice

	@assetClassID nchar(4) = 'GLEQ'

AS
	/* SET NOCOUNT ON */


SELECT
CAST([Date] AS INT) AS [Date],
[Value]
FROM tblHistoricData
WHERE AssetClassID=@assetClassID
ORDER BY [Date]


RETURN
