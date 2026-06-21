using LittleWizard.LittleWizardCode.Powers.Elements;
using LittleWizard.LittleWizardCode.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Api.Powers;

public abstract class BaseElement : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override bool HasCustomAudio => true;

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power.Owner != Owner || power is not BaseElement)
        {
            return;
        }

        var fire = Owner.GetPower<FireElement>();
        var water = Owner.GetPower<WaterElement>();
        var earth = Owner.GetPower<EarthElement>();

        if (fire != null && water != null)
        {
            var amountReactor = fire.Amount + water.Amount;
            await PowerCmd.Remove(fire);
            await PowerCmd.Remove(water);
            await PowerCmd.Apply<FireWaterReactor>(
                choiceContext,
                Owner,
                amountReactor,
                applier,
                null
            );
            return;
        }

        if (fire != null && earth != null)
        {
            var amountReactor = fire.Amount + earth.Amount;
            await PowerCmd.Remove(fire);
            await PowerCmd.Remove(earth);
            await PowerCmd.Apply<FireEarthReactor>(
                choiceContext,
                Owner,
                amountReactor,
                applier,
                null
            );
            return;
        }

        if (water != null && earth != null)
        {
            await PowerCmd.Remove(water);
            await PowerCmd.Remove(earth);
            await PowerCmd.Apply<WaterEarthReactor>(
                choiceContext,
                Owner,
                water.Amount + earth.Amount,
                applier,
                null
            );
        }
    }
}
