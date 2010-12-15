SELECT     ROW_NUMBER() OVER (ORDER BY [Date]) AS RankNumber, CAST(Date AS int) AS Date, Value AS PreviousValue, Value
FROM         dbo.tblHistoricData
