CREATE VIEW [dbo].[vwBenchmarkDataList] AS

SELECT BenchmarkID,[Date], [Value]
FROM (
	SELECT [Date], ACMA, BAMA, CAMA, GLGR, STBO
	FROM tblBenchmarkData) p
UNPIVOT
	([Value] FOR BenchmarkID IN
		(ACMA, BAMA, CAMA, GLGR, STBO)
) AS unpvt
