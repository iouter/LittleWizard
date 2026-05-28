using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.LittleWizardCode.Powers;

public class WaterTempPower : LittleWizardPower
{
    private int totalStrength = 0;

    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power != this)
            return;
        int delta = Amount - totalStrength;
        if (delta != 0)
        {
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, -delta, applier, cardSource);
            totalStrength = Amount;
        }
    }

    public override async Task AfterRemoved(Creature Owner)
    {
        if (totalStrength != 0)
        {
            await PowerCmd.Apply<StrengthPower>(
                new ThrowingPlayerChoiceContext(),
                Owner,
                totalStrength,
                null,
                null
            );
        }
        await base.AfterRemoved(Owner);
    }
}
