 -- =============================================
-- Author:		Nigel Garside
-- Create date: 11/12/2010
-- Description:	Ten year returns for charts
-- =============================================
ALTER PROCEDURE [dbo].[spTenYearReturn] 

	@AssetClassID nchar(4)
AS
BEGIN

	SET NOCOUNT ON;

	WITH f
	AS
	(
		SELECT ROW_NUMBER() OVER (ORDER BY [Date]) + 1 AS rn, [value]
		FROM vwHistoricDataList
		WHERE AssetClassID=@AssetClassID
	)

	SELECT  CAST(t.Date AS INT) AS [Date], Log(t.value/f.value) AS [Value]
	FROM f,
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]
			FROM vwHistoricDataList
			WHERE AssetClassID=@AssetClassID) AS t
	WHERE f.rn=t.rn

END
