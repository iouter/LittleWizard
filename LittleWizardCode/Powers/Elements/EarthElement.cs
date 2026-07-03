using BaseLib.Cards.Variables;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Elements;

public class EarthElement : BaseElement
{
    private const string EarthBlock = "EarthBlock";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new(EarthBlock + "Base", 0),
            new(EarthBlock + "Extra", 1),
            new CustomCalculatedVar(EarthBlock).WithMultiplier((power, _) => GetBlock(power)),
        ];

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
            creature = dealer.PetOwner!.Creature;
        }
        if (creature.Player == null || GetInternalData<Data>().IsAttacked)
            return;
        GetInternalData<Data>().IsAttacked = true;
        Flash();
        await CreatureCmd.GainBlock(creature, GetBlock(this), ValueProp.Move, null);
    }

    public override Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IReadOnlyList<Creature> creatures,
        ICombatState combatState
    )
    {
        if (side == Owner.Side)
        {
            GetInternalData<Data>().IsAttacked = false;
        }
        return Task.CompletedTask;
    }

    private static int GetBlock(PowerModel power)
    {
        return power.CalculateElementAmount();
    }
}
