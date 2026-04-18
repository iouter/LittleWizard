using System.Diagnostics;
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Elements;

public class EarthElement : BaseElement
{
    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        ElementSoundHelper.PlayAppliedSound(this, applier);
        return base.AfterApplied(applier, cardSource);
    }

    protected override object InitInternalData() => new Data();

    private class Data
    {
        public bool IsAttacked;
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
        if (target != Owner || dealer == null || !props.IsPoweredAttack() || result.WasFullyBlocked)
            return;
        var creature = dealer;
        if (dealer.Monster is Osty)
        {
            Debug.Assert(dealer.PetOwner != null);
            creature = dealer.PetOwner.Creature;
        }
        if (creature.Player == null || GetInternalData<Data>().IsAttacked)
            return;
        GetInternalData<Data>().IsAttacked = true;
        Flash();
        await CreatureCmd.GainBlock(creature, Amount, ValueProp.Move, null);
    }

    public override Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        CombatState combatState
    )
    {
        if (side == Owner.Side)
        {
            GetInternalData<Data>().IsAttacked = false;
        }
        return Task.CompletedTask;
    }

    public override bool TryModifyPowerAmountReceived(
        PowerModel canonicalPower,
        Creature target,
        decimal amount,
        Creature? applier,
        out decimal modifiedAmount
    )
    {
        if (target != Owner || amount == 0)
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
