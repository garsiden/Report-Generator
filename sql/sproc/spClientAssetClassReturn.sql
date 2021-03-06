-- =============================================
-- Author:		Nigel Garside
-- Create date: 15/12/2010
-- Description:	Get Client's Asset returns by asset class
-- =============================================
ALTER PROCEDURE [dbo].[spClientAssetClassReturn] 

	@ClientGUID uniqueidentifier

AS

BEGIN

SET NOCOUNT ON;

WITH c AS
(
SELECT * FROM tblClientAssetClass WHERE ClientGUID = @ClientGUID
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
