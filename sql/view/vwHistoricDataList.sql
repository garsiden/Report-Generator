ALTER VIEW [dbo].[vwHistoricDataList] AS

SELECT AssetClassID,[Date], [Value]
FROM (
	SELECT [Date], CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
	FROM tblHistoricData) p
UNPIVOT
	([Value] FOR AssetClassID IN
		(CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO)
) AS unpvt
