using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public sealed class WarmupBeamPower : LittleWizardPower
{
    public override PowerType Type => Owner.IsPlayer ? PowerType.Buff : PowerType.Debuff;
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
            || canonicalPower is not BaseElement
            || !canonicalPower.IsVisible
        )
        {
            modifiedAmount = amount;
            return false;
        }

        modifiedAmount = amount + Amount;
        return true;
    }
}
