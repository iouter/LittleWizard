using LittleWizard.LittleWizardCode.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Api.Powers;

public abstract class AfterElementReactPower : LittleWizardPower
{
    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext ctx,
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power.Owner != Owner)
            return;
        if (power is FireWaterReactor or FireEarthReactor or WaterEarthReactor)
            await AfterElementReact(ctx, Owner, amount, applier, cardSource);
        await base.AfterPowerAmountChanged(ctx, power, amount, applier, cardSource);
    }

    protected abstract Task AfterElementReact(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    );
}
