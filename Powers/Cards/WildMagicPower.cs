using LittleWizard.Api;
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Cards;

public class WildMagicPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyDamageMultiplicative(
        Creature? target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        return dealer != null && (Utils.IsPoweredAttack(props) || cardSource == null ||
                                  (dealer != Owner && !Owner.Pets.Contains(dealer)) || target == null)
            ? base.ModifyDamageMultiplicative(target, amount, props, dealer, cardSource)
            : Amount;
    }

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource)
    {
        if (giver != Owner || power is not BaseElement)
            return base.ModifyPowerAmountGiven(power, giver, amount, target, cardSource);

        return Amount;
    }

    public override decimal ModifyBlockMultiplicative(Creature target, decimal block, ValueProp props,
        CardModel? cardSource,
        CardPlay? cardPlay)
    {
        return target != Owner ? base.ModifyBlockMultiplicative(target, block, props, cardSource, cardPlay) : 0;
    }
}