-- =============================================
-- Author:		Nigel Garside
-- Create date: 15/12/2010
-- Description:	Get Client's Asset return by investment
-- =============================================
ALTER PROCEDURE [dbo].[spClientAssetInvestmentReturn] 

	@ClientGUID uniqueidentifier

AS
BEGIN
SET NOCOUNT ON;

DECLARE @tamnt money

SET @tamnt = (SELECT SUM(Amount) FROM tblClientAsset WHERE ClientGUID = @ClientGUID);

WITH c AS
(
SELECT
 SUM(CASH*Amount)/@tamnt AS CASH, SUM(COMM*Amount)/@tamnt AS COMM, SUM(COPR*Amount)/@tamnt AS COPR,
 SUM(GLEQ*Amount)/@tamnt AS GLEQ, SUM(HEDG*Amount)/@tamnt AS HEDG, SUM(LOSH*Amount)/@tamnt AS LOSH,
 SUM(PREQ*Amount)/@tamnt AS PREQ, SUM(UKCB*Amount)/@tamnt AS UKCB, SUM(UKEQ*Amount)/@tamnt AS UKEQ,
 SUM(UKGB*Amount)/@tamnt AS UKGB, SUM(UKHY*Amount)/@tamnt AS UKHY, SUM(WOBO*Amount)/@tamnt AS WOBO
FROM  tblClientAsset
WHERE ClientGUID=@ClientGUID
GROUP BY ClientGUID
)

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
(SELECT * FROM c) r,
(SELECT * FROM vwRawReturn) rr

END
