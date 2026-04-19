using LittleWizard.Api.Powers;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Powers.Cards;

public class RockWallPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool TryModifyPowerAmountReceived(
        PowerModel canonicalPower,
        Creature target,
        decimal amount,
        Creature? applier,
        out decimal modifiedAmount
    )
    {
        if (
            amount == 0
            || target != Owner
            || canonicalPower is not EarthElement
            || !canonicalPower.IsVisible
        )
        {
            modifiedAmount = amount;
            return false;
        }

        modifiedAmount = 2 * amount;
        PowerCmd.Decrement(this);
        return true;
    }
}
