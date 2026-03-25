using LittleWizard.Api.Powers;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Powers.Cards;

public class IgnitePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount,
        Creature? applier,
        out decimal modifiedAmount)
    {
        if (amount == 0 || canonicalPower is not FireElement || !canonicalPower.Owner.IsEnemy || applier != Owner)
        {
            modifiedAmount = amount;
            return false;
        }

        modifiedAmount = 2 * amount;
        return true;
    }
}