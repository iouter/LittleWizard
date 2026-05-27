using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Elements.Reacts;

public class FireEarthReactor : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath =>
        "res://LittleWizard/images/powers/fire_and_earth_element_reactor_power.png";
    public override string CustomBigIconPath =>
        "res://LittleWizard/images/powers/big/fire_and_earth_element_reactor_power.png";

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power != this || amount == Amount)
            return;
        await CreatureCmd.Damage(choiceContext, Owner, amount, ValueProp.Unpowered, applier, null);
    }

    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        if (command == null)
            return;
        if (command.Attacker?.Side != CombatSide.Player)
            return;
        if (command.ModelSource is not CardModel card || card.Type != CardType.Attack)
            return;

        await CreatureCmd.Damage(
            choiceContext,
            Owner,
            Amount,
            ValueProp.Unpowered,
            command.Attacker,
            null
        );
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

        if (cardSource == null && !dealer.HasPower<ThornsPower>())
            return;

        var creature = dealer;
        if (dealer.Monster is Osty)
        {
            creature = dealer.PetOwner!.Creature;
        }

        if (creature.Player == null)
            return;

        Flash();
        await CreatureCmd.GainBlock(creature, Amount, ValueProp.Move, null);
    }

    public override async Task AfterSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> creatures
    )
    {
        if (side == CombatSide.Enemy)
        {
            await PowerCmd.Remove(this);
        }
    }
}
