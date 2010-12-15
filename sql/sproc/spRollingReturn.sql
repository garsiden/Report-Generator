-- =============================================
-- Author:		Nigel Garside
-- Create date: 11/12/2010
-- Description:	Rolling returns for charts
-- =============================================
ALTER PROCEDURE [dbo].[RollingReturn] 
	-- Add the parameters for the stored procedure here
	@years int = 1, 
	@asset_class nchar(4) = 'CASH'
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
		WHERE benchmarkID=@asset_class
	)

	SELECT  CAST(t.Date AS INT) AS [Date], Log(t.value/f.value)/@years AS [Value]
	FROM f,
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]
-- CAST([date]	AS int) AS [date]
			FROM tblHistoricData
			WHERE benchmarkID=@asset_class) AS t
	WHERE f.rn=t.rn

/*
Expected values
0.133918818
0.14135951
0.130405169
0.161543882
0.155110673
0.145870277
0.13265738
0.128198296
0.158185405
0.156394544
0.149795161
0.169432012
0.173565882
0.165851852
0.142512771
0.132278398
0.120292285
0.09742942






*/

END
