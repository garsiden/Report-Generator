-- Author:		Nigel Garside
-- Create date: 17/12/2010
-- Description:	Get prices for a Benchmark
-- =============================================
ALTER PROCEDURE [dbo].[spBenchmarkPrice] 

	@BenchmarkID nchar(4)

AS
BEGIN

SET NOCOUNT ON;

SELECT CAST([Date] AS INT) AS [Date], BenchmarkID, [Value]
FROM dbo.vwBenchmarkDataList
WHERE BenchmarkID=@BenchmarkID

END
