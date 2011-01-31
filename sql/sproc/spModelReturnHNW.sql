-- =============================================
-- Author:		nigel.garside@gmail.com
-- Create date: 11/12/2010
-- Description:	Get Model reurn for strategy and HNW client status
-- =============================================

ALTER PROCEDURE [dbo].[spModelReturnHNW]
(
	@strategyID nchar(2)
)
AS

WITH hnw AS
(SELECT * FROM vwModelAllocationHNW WHERE StrategyID=@StrategyID)

SELECT   CAST([Date] AS INT) AS [Date],
COALESCE((r.CASH * rr.CASH), 0) +
COALESCE((r.COMM * rr.COMM), 0) +
COALESCE((r.HEDG * rr.HEDG), 0) +
COALESCE((r.COPR * rr.COPR), 0) +
COALESCE((r.GLEQ * rr.GLEQ), 0) +
COALESCE((r.LOSH * rr.LOSH), 0) +
COALESCE((r.PREQ * rr.PREQ), 0) +
COALESCE((r.UKCB * rr.UKCB), 0) +
COALESCE((r.UKEQ * rr.UKEQ), 0) +
COALESCE((r.UKGB * rr.UKGB), 0) +
COALESCE((r.UKHY * rr.UKHY), 0) +
COALESCE((r.WOBO * rr.WOBO), 0)
AS [Value] FROM
(SELECT * FROM hnw ) r,
(SELECT * FROM vwRawReturn) rr

	RETURN
