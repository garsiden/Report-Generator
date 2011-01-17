-- =============================================
-- Author:		Nigel Garside
-- Create date: 15/12/2010
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[spClientAssetClassReturn] 
	@startDate datetime = null,
	@ClientGUID uniqueidentifier = '979de312-8e99-49d3-9d41-54ecae0cad5c'

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
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
(SELECT * FROM vwRawReturn WHERE [Date] >= @startDate OR @startDate IS NULL) rr


END
