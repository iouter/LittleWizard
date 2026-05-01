using System.Diagnostics;
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Elements.Reacts;

public class WaterEarthReactor : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath =>
        "res://LittleWizard/images/powers/water_and_earth_element_reactor_power.png";
    public override string CustomBigIconPath =>
        "res://LittleWizard/images/powers/big/water_and_earth_element_reactor_power.png";

    public override async Task BeforeApplied(
        Creature target,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
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
        await PowerCmd.Apply<StrengthPower>(Owner, -amount, applier, null);
    }

    public override async Task AfterDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource
    )
    {
        if (target != Owner || dealer == null || result.WasFullyBlocked)
            return;

        var creature = dealer;
        if (dealer.Monster is Osty)
        {
            Debug.Assert(dealer.PetOwner != null);
            creature = dealer.PetOwner.Creature;
        }

        if (creature.Player == null)
            return;

        Flash();
        await CreatureCmd.GainBlock(creature, Amount, ValueProp.Move, null);
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
