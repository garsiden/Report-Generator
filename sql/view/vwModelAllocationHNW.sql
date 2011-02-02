ALTER VIEW [dbo].[vwModelAllocationHNW] AS
SELECT StrategyId, CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
FROM (SELECT StrategyId, AssetClassId, WeightingHNW
FROM  vwModel) piv
 PIVOT (SUM(WeightingHNW)
FOR AssetClassID IN (CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO)) AS chld