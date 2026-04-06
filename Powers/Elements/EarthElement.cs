using LittleWizard.Api;
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Elements;

public class EarthElement : BaseElement
{
    public override decimal ModifyDamageMultiplicative(
        Creature? target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource
    )
    {
        if (target != Owner || !Utils.IsPoweredAttack(props))
            return base.ModifyDamageMultiplicative(target, amount, props, dealer, cardSource);
        return base.ModifyDamageMultiplicative(target, amount, props, dealer, cardSource)
            + (decimal)(Amount * 0.03);
    }

    public override bool TryModifyPowerAmountReceived(
        PowerModel canonicalPower,
        Creature target,
        decimal amount,
        Creature? applier,
        out decimal modifiedAmount
    )
    {
        if (target != Owner)
        {
            modifiedAmount = amount;
            return false;
        }

        switch (canonicalPower)
        {
            case FireElement fire:
            {
                ElementHelper.FireAndEarth(Owner, Amount, amount, applier);
                modifiedAmount = 0;
                return true;
            }
            case WaterElement water:
            {
                ElementHelper.WaterAndEarth(Owner, Amount, amount, applier);
                modifiedAmount = 0;
                return true;
            }
            default:
            {
                modifiedAmount = amount;
                return false;
            }
        }
    }
}
