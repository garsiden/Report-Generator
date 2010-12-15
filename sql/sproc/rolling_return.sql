USE [RepGen]
GO
/****** Object:  StoredProcedure [dbo].[RollingReturn]    Script Date: 12/11/2010 15:59:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nigel Garside
-- Create date: 11/12/2010
-- Description:	Rolling returns for charts
-- =============================================
ALTER PROCEDURE [dbo].[RollingReturn] 
	-- Add the parameters for the stored procedure here
	@years int = 1, 
	@asset_class nchar(4) = 'CASH'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH f
	AS
	(
		SELECT ROW_NUMBER() OVER (ORDER BY [date]) + (12 * @years) AS rn, [value]
		FROM tblHistoricData
		WHERE benchmarkID=@asset_class
	)

	SELECT  t.date, Log(t.value/f.value)/@years AS [return]
	FROM f,
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]
			FROM tblHistoricData
			WHERE benchmarkID=@asset_class) AS t
	WHERE f.rn=t.rn

/*
Expected 3 year values
0.09894667
0.092216237
0.093683891
0.102312488
0.100658601
0.095305138
0.093326232
0.088836598
0.08817961
0.079298755
0.080762095
0.085987502
0.082304003
0.077660571
0.077697961
*/

END
