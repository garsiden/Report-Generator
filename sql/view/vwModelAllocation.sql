ALTER VIEW [dbo].[vwModelAllocation] AS
SELECT StrategyId, CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
FROM (SELECT StrategyId, AssetClassId, Weighting
FROM  vwModel) piv
 PIVOT (SUM(Weighting)
FOR AssetClassID IN (CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO)) AS chld
