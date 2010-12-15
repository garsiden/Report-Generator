SELECT     StrategyId, CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO
FROM         (SELECT     StrategyId, AssetClassId, Weighting
                       FROM          tblModel) piv PIVOT (SUM(Weighting) FOR AssetClassID IN (CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, 
                      WOBO)) AS chld
