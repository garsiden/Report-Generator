-- =============================================਀ⴀⴀ 䄀甀琀栀漀爀㨀ऀऀ渀椀最攀氀⸀最愀爀猀椀搀攀䀀最洀愀椀氀⸀挀漀洀 
-- Create date: 11/12/2010਀ⴀⴀ 䐀攀猀挀爀椀瀀琀椀漀渀㨀ऀ 
-- =============================================਀ 
ALTER PROCEDURE [dbo].[spRawReturn]਀ 
AS਀ऀ⼀⨀ 匀䔀吀 一伀䌀伀唀一吀 伀一 ⨀⼀ 
਀圀䤀吀䠀 昀 䄀匀 
(਀ऀ匀䔀䰀䔀䌀吀 刀伀圀开一唀䴀䈀䔀刀⠀⤀ 伀嘀䔀刀 ⠀伀刀䐀䔀刀 䈀夀 嬀䐀愀琀攀崀⤀ ⬀ ㄀ 䄀匀 爀渀Ⰰ 
       CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO਀ऀ䘀刀伀䴀 琀戀氀䠀椀猀琀漀爀椀挀䐀愀琀愀 
)਀ 
SELECT  CAST(t .Date AS INT) AS [Date], LOG (t .CASH / f.CASH) AS CASH, LOG (t .COMM / f.COMM) AS COMM, Log (t .COPR / f.COPR) AS COPR, LOG (t .GLEQ / f.GLEQ) AS GLEQ, ਀ऀ䰀伀䜀 ⠀琀 ⸀䠀䔀䐀䜀 ⼀ 昀⸀䠀䔀䐀䜀⤀ 䄀匀 䠀䔀䐀䜀Ⰰ 䰀伀䜀 ⠀琀 ⸀䰀伀匀䠀 ⼀ 昀⸀䰀伀匀䠀⤀ 䄀匀 䰀伀匀䠀Ⰰ 䰀伀䜀 ⠀琀 ⸀倀刀䔀儀 ⼀ 昀⸀倀刀䔀儀⤀ 䄀匀 倀刀䔀儀Ⰰ 䰀伀䜀 ⠀琀 ⸀唀䬀䌀䈀 ⼀ 昀⸀唀䬀䌀䈀⤀ 䄀匀 唀䬀䌀䈀Ⰰ  
	LOG (t .UKEQ / f.UKEQ) AS UKEQ, LOG (t .UKGB / f.UKGB) AS UKGB, LOG (t .UKHY / f.UKHY) AS UKHY, LOG (t .WOBO / f.WOBO) AS WOBO਀䘀刀伀䴀  昀Ⰰ 
	(SELECT  ROW_NUMBER() OVER (ORDER BY [Date]) AS rn, [Date], CASH, COMM, COPR, GLEQ, HEDG, LOSH, PREQ, UKCB, UKEQ, UKGB, UKHY, WOBO਀ऀऀ䘀刀伀䴀 琀戀氀䠀椀猀琀漀爀椀挀䐀愀琀愀⤀ 琀 
WHERE f.rn = t .rn਀ 
RETURN਀�