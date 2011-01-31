-- =============================================
-- Author:		nigel.garside@gmail.com
-- Create date: 11/12/2010
-- Description:	Get Model return for strategy and client status type
-- =============================================

ALTER PROCEDURE [dbo].[spModelReturn]
(
	@StrategyID nchar(2),
	@ModelType nchar(3)
)

AS

IF @ModelType = 'HNW'
	exec spModelReturnHNW @StrategyID=@StrategyID
ELSE
    exec spModelReturnAffluent @StrategyID=@StrategyID

