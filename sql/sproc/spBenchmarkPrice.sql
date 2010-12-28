-- Author:		Nigel Garside
-- Create date: 17/12/2010
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[spBenchmarkPrice] 

	@startDate datetime = '1999-09-30', 
	@benchmarkID nchar(4) = 'CAMA'

AS
BEGIN

	SET NOCOUNT ON;


SELECT CAST([Date] AS INT) AS [Date], BenchmarkID, [Value]
FROM dbo.tblBenchmarkData
WHERE BenchmarkID=@benchmarkID AND [Date] >= @startDate

END
