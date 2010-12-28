 -- =============================================
-- Author:		Nigel Garside
-- Create date: 11/12/2010
-- Description:	Rolling returns for charts
-- =============================================
ALTER PROCEDURE [dbo].[spTenYearReturn] 
	@startDate datetime = '1999-09-30', 
	@assetClassID nchar(4) = 'GLEQ'
AS
BEGIN

	SET NOCOUNT ON;

	WITH f
	AS
	(
		SELECT ROW_NUMBER() OVER (ORDER BY [Date]) + 1 AS rn, [value]
		FROM tblHistoricData
		WHERE assetClassID=@assetClassID
	)

	SELECT  CAST(t.Date AS INT) AS [Date], Log(t.value/f.value) AS [Value]
	FROM f,
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]
			FROM tblHistoricData
			WHERE assetClassID=@assetClassID AND [date] >= @startDate) AS t
	WHERE f.rn=t.rn

END
