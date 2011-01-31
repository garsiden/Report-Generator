-- Author:		Nigel Garside
-- Create date: 11/12/2010
-- Description:	Rolling returns for charts
-- =============================================
ALTER PROCEDURE [dbo].[spRollingReturn] 

	@years int = 1, 
	@assetClassID nchar(4)
AS
BEGIN

	SET NOCOUNT ON;

	WITH f
	AS
	(
		SELECT ROW_NUMBER() OVER (ORDER BY [Date]) + (12 * @years) AS rn, [value]
		FROM vwHistoricDataList
		WHERE assetClassID=@assetClassID
	)

	SELECT  CAST(t.Date AS INT) AS [Date], Log(t.value/f.value)/@years AS [Value]
	FROM f,
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]
			FROM vwHistoricDataList
			WHERE assetClassID=@assetClassID) AS t
	WHERE f.rn=t.rn

END
