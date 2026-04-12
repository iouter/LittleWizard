using System.Diagnostics;
using BaseLib.Abstracts;
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

public class WaterEarthDebuffPower : CustomTemporaryPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerModel InternallyAppliedPower => ModelDb.Power<StrengthPower>();
    public override AbstractModel OriginModel => ModelDb.Power<WaterAndEarthElementReactorPower>();

    public override string CustomPackedIconPath =>
        "res://LittleWizard/images/powers/water_and_earth_element_reactor_power.png";
    public override string CustomBigIconPath =>
        "res://LittleWizard/images/powers/big/water_and_earth_element_reactor_power.png";

    protected override Func<Creature, decimal, Creature?, CardModel?, bool, Task> ApplyPowerFunc =>
        async (target, amount, applier, cardSource, _) =>
        {
            await PowerCmd.Apply<StrengthPower>(target, -amount, applier, cardSource);
        };

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

        if (creature.Player == null)
            return;

        Flash();
        await CreatureCmd.GainBlock(creature, Amount, ValueProp.Move, null);
    }
}
