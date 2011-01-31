-- =============================================
-- Author:		nigel.garside@gmail.com
-- Create date: 11/12/2010
-- Description:	Asset prices for an asset class
-- =============================================
ALTER PROCEDURE [dbo].[spHistoricPrice]

	@assetClassID nchar(4)

AS

SELECT
CAST([Date] AS INT) AS [Date],
[Value]
FROM vwHistoricDataList
WHERE AssetClassID=@assetClassID
ORDER BY [Date]

RETURN
