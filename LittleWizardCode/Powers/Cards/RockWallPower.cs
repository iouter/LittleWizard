using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

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
        if (amount == 0 || applier != Owner || canonicalPower is not EarthElement)
        {
            modifiedAmount = amount;
            return false;
        }

        modifiedAmount = 2 * amount;
        PowerCmd.Decrement(this);
        return true;
    }
}
