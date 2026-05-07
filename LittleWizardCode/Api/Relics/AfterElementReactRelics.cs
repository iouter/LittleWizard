using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Api.Relics;

public abstract class AfterElementReactRelics : LittleWizardRelics
{
    public override async Task AfterPowerAmountChanged(
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power.Owner != Owner.Creature)
            return;
        if (power is FireWaterReactor or FireEarthReactor or WaterEarthReactor)
            await AfterElementReact(Owner.Creature, amount, applier, cardSource);
        await base.AfterPowerAmountChanged(power, amount, applier, cardSource);
    }

    protected abstract Task AfterElementReact(
        Creature owner,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    );
}
