ALTER VIEW [dbo].[vwModelAllocationAffluent] AS
SELECT StrategyId, CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
FROM (SELECT StrategyId, AssetClassId, WeightingAffluent
FROM  vwModel) piv
 PIVOT (SUM(WeightingAffluent)
FOR AssetClassID IN (CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO)) AS chld
