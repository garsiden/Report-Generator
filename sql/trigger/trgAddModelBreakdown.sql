ALTER TRIGGER trgAddModelBreakdown
ON tblStrategy FOR INSERT AS

IF (@@ROWCOUNT>1) BEGIN
	RAISERROR('Only one Strategy may be added at a time', 16, 10)
	ROLLBACK TRAN
	RETURN
END

DECLARE @StrategyID nchar(2)

SET @StrategyID = (SELECT ID FROM inserted)

INSERT tblModelBreakdown (StrategyID, AssetClassID, WeightingHNW, WeightingAffluent)
SELECT @StrategyID, AssetClassID, 0, 0 FROM tblAssetGroupClass

IF (@@ERROR<>0)
	ROLLBACK TRANSACTION
RETURN
