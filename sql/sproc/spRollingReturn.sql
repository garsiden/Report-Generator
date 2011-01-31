-- Author:		Nigel Garside
-- Create date: 11/12/2010
-- Description:	Rolling returns for charts
-- =============================================
ALTER PROCEDURE [dbo].[spRollingReturn] 

	@years int = 1, 
	@assetClassID nchar(4) = 'CASH'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH f
	AS
	(
		SELECT ROW_NUMBER() OVER (ORDER BY [Date]) + (12 * @years) AS rn, [value]
		FROM tblHistoricData
		WHERE assetClassID=@assetClassID
	)

	SELECT  CAST(t.Date AS INT) AS [Date], Log(t.value/f.value)/@years AS [Value]
	FROM f,
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]
			FROM tblHistoricData
			WHERE assetClassID=@assetClassID) AS t
	WHERE f.rn=t.rn

END
