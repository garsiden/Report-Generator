-- Author:		nigel.garside@gmail.com਀ⴀⴀ 䌀爀攀愀琀攀 搀愀琀攀㨀 ㄀㄀⼀㄀㈀⼀㈀　㄀　 
-- Description:	Returns for an Asset Class from tblHistoricData਀ⴀⴀ 㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀 
ALTER PROCEDURE [dbo].[spAssetClassReturn] ਀ 
	@startDate datetime = '1999-09-30', ਀ऀ䀀愀猀猀攀琀䌀氀愀猀猀䤀䐀 渀挀栀愀爀⠀㐀⤀ 㴀 ✀䜀䰀䔀儀✀ 
AS਀䈀䔀䜀䤀一 
	-- SET NOCOUNT ON added to prevent extra result sets from਀ऀⴀⴀ 椀渀琀攀爀昀攀爀椀渀最 眀椀琀栀 匀䔀䰀䔀䌀吀 猀琀愀琀攀洀攀渀琀猀⸀ 
	SET NOCOUNT ON;਀ऀ圀䤀吀䠀 昀 
	AS਀ऀ⠀ 
		SELECT ROW_NUMBER() OVER (ORDER BY [Date]) + 1 AS rn, [value]਀ऀऀ䘀刀伀䴀 瘀眀䠀椀猀琀漀爀椀挀䐀愀琀愀䰀椀猀琀 
		WHERE assetClassID=@assetClassID਀ऀ⤀ 
਀匀䔀䰀䔀䌀吀 ⨀ 䘀刀伀䴀 ⠀ 
	SELECT  CAST(t.Date AS INT) AS [Date], Log(t.value/f.value) AS [Value]਀ऀ䘀刀伀䴀 昀Ⰰ 
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]਀ऀऀऀ䘀刀伀䴀 瘀眀䠀椀猀琀漀爀椀挀䐀愀琀愀䰀椀猀琀 
			WHERE assetClassID=@assetClassID) AS t਀ऀ圀䠀䔀刀䔀 昀⸀爀渀㴀琀⸀爀渀ऀ ⤀ 爀 
WHERE r.[Date] >= CAST(@startDate AS INT)਀ 
END਀�