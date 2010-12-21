-- Author:		nigel.garside@gmail.com
-- Create date: 11/12/2010
-- Description:	Calculates Models returns
-- =============================================

ALTER PROCEDURE dbo.spModelReturn

	(
	@strategyId char(2) = 'CO',
	@startDate datetime = '1996-12-31'
	)

AS
	/* SET NOCOUNT ON */

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
(SELECT * FROM vwModelAllocation WHERE strategyId=@strategyId) r,
(SELECT * FROM vwRawReturn) rr
WHERE rr.Date >= CAST(@startDate AS INT)

	RETURN
