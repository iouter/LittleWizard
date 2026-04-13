using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Api.Powers;

public abstract class BaseMoreElementGivenPower : CustomPowerModel
{
    public override PowerType Type => Owner.IsPlayer ? PowerType.Buff : PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyPowerAmountGiven(
        PowerModel canonicalPower,
        Creature giver,
        decimal amount,
        Creature? target,
        CardModel? cardSource
    )
    {
        if (
            amount == 0
            || giver != Owner
            || canonicalPower is not BaseElement
            || !canonicalPower.IsVisible
        )
        {
            return amount;
        }
        return amount + Amount;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Side)
            return;
        await PowerCmd.Remove(this);
    }
}
