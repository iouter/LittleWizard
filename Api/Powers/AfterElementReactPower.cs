using LittleWizard.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Api.Powers;

public abstract class AfterElementReactPower : LittleWizardPower
{
    public override async Task AfterPowerAmountChanged(
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power.Owner != Owner)
            return;
        if (power is FireWaterReactor or FireEarthReactor or WaterEarthReactor)
            await AfterElementReact(Owner, amount, applier, cardSource);
        await base.AfterPowerAmountChanged(power, amount, applier, cardSource);
    }

    protected abstract Task AfterElementReact(
        Creature owner,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    );
}
