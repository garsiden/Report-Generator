-- Author:		Nigel Garside਀ⴀⴀ 䌀爀攀愀琀攀 搀愀琀攀㨀 ㄀㄀⼀㄀㈀⼀㈀　㄀　 
-- Description:	Rolling returns for charts਀ⴀⴀ 㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀㴀 
ALTER PROCEDURE [dbo].[spRollingReturn] ਀ 
	@years int = 1, ਀ऀ䀀愀猀猀攀琀䌀氀愀猀猀䤀䐀 渀挀栀愀爀⠀㐀⤀ 㴀 ✀䌀䄀匀䠀✀ 
AS਀䈀䔀䜀䤀一 
	-- SET NOCOUNT ON added to prevent extra result sets from਀ऀⴀⴀ 椀渀琀攀爀昀攀爀椀渀最 眀椀琀栀 匀䔀䰀䔀䌀吀 猀琀愀琀攀洀攀渀琀猀⸀ 
	SET NOCOUNT ON;਀ 
	WITH f਀ऀ䄀匀 
	(਀ऀऀ匀䔀䰀䔀䌀吀 刀伀圀开一唀䴀䈀䔀刀⠀⤀ 伀嘀䔀刀 ⠀伀刀䐀䔀刀 䈀夀 嬀䐀愀琀攀崀⤀ ⬀ ⠀㄀㈀ ⨀ 䀀礀攀愀爀猀⤀ 䄀匀 爀渀Ⰰ 嬀瘀愀氀甀攀崀 
		FROM vwHistoricDataList਀ऀऀ圀䠀䔀刀䔀 愀猀猀攀琀䌀氀愀猀猀䤀䐀㴀䀀愀猀猀攀琀䌀氀愀猀猀䤀䐀 
	)਀ 
	SELECT  CAST(t.Date AS INT) AS [Date], Log(t.value/f.value)/@years AS [Value]਀ऀ䘀刀伀䴀 昀Ⰰ 
		(SELECT ROW_NUMBER() OVER (ORDER BY [date]) as rn, [value], [date]਀ऀऀऀ䘀刀伀䴀 瘀眀䠀椀猀琀漀爀椀挀䐀愀琀愀䰀椀猀琀 
			WHERE assetClassID=@assetClassID) AS t਀ऀ圀䠀䔀刀䔀 昀⸀爀渀㴀琀⸀爀渀 
਀䔀一䐀 
