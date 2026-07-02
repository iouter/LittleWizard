using BaseLib.Cards.Variables;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Elements.Reacts;

public class FireWaterReactor : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath =>
        "res://LittleWizard/images/powers/fire_and_water_element_reactor_power.png";
    public override string CustomBigIconPath =>
        "res://LittleWizard/images/powers/big/fire_and_water_element_reactor_power.png";

    private const string FireWater = "FireWater";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new(FireWater + "Base", 0),
            new(FireWater + "Extra", 1),
            new CustomCalculatedVar(FireWater).WithMultiplier(
                (power, _) => power.CalculateElementAmount()
            ),
        ];

    public override async Task BeforeApplied(
        Creature target,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        await PowerCmd.Apply<StrengthPower>(
            new ThrowingPlayerChoiceContext(),
            target,
            this.CalculateElementAmount(
                isPositive: false,
                amount: (int)amount,
                combatState: target.CombatState
            ),
            applier,
            null
        );
    }

    public override async Task AfterDamageReceivedLate(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource
    )
    {
        if (target != Owner)
            return;
        if (cardSource == null || cardSource.Type != CardType.Attack)
            return;
        await CreatureCmd.Damage(choiceContext, Owner, Amount, ValueProp.Unpowered, dealer, null);
    }

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
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
        await CreatureCmd.Damage(choiceContext, Owner, amount, ValueProp.Unpowered, applier, null);
        await PowerCmd.Apply<StrengthPower>(
            choiceContext,
            Owner,
            this.CalculateElementAmount(isPositive: false, amount: (int)amount),
            applier,
            null
        );
    }

    public override async Task AfterSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> creatures
    )
    {
        if (side == CombatSide.Enemy)
        {
            await PowerCmd.Apply<StrengthPower>(
                choiceContext,
                Owner,
                this.CalculateElementAmount(),
                null,
                null
            );
            await PowerCmd.Remove(this);
        }
    }
}
