-- Author:		Nigel Garside
-- Create date: 11/12/2010
-- Description:	Rolling returns for charts
-- =============================================
ALTER PROCEDURE [dbo].[spDrawdown] 
	-- Add the parameters for the stored procedure here
	@assetClassID nchar(4) = 'UKGB'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH f
	AS
	(
		SELECT ROW_NUMBER() OVER (ORDER BY [Date]) + 1 AS rn, [value]
		FROM tblHistoricData
		WHERE assetClassID=@assetClassID
	)

	SELECT  t.rn - 1 AS RankNumber, CAST(t.Date AS INT) AS [Date], f.value as PreviousValue, t.value As [Value]
	FROM f,
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]
			FROM tblHistoricData
			WHERE AssetClassID=@assetClassID) AS t
	WHERE f.rn=t.rn

/*

0.011533236
0.01335448
-0.014244555
0.015316942
0.0210189
0.01015719
0.014253805
0.000186202
0.035393023
0.004214308




*/

END
