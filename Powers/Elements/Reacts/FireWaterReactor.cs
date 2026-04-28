using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Elements.Reacts;

public class FireWaterReactor : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath =>
        "res://LittleWizard/images/powers/fire_and_water_element_reactor_power.png";
    public override string CustomBigIconPath =>
        "res://LittleWizard/images/powers/big/fire_and_water_element_reactor_power.png";

    public override async Task BeforeApplied(
        Creature target,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        await CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            target,
            amount,
            ValueProp.Unpowered,
            applier,
            null
        );
        await PowerCmd.Apply<StrengthPower>(target, -amount, applier, null);
    }

    public override async Task AfterPowerAmountChanged(
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power != this || amount == Amount)
        {
            return;
        }
        await CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            Owner,
            amount,
            ValueProp.Unpowered,
            applier,
            null
        );
        await PowerCmd.Apply<StrengthPower>(Owner, -amount, applier, null);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            await PowerCmd.Apply<StrengthPower>(Owner, Amount, null, null);
            await PowerCmd.Remove(this);
        }
    }
}
