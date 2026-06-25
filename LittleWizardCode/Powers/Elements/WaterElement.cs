using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Elements;

public class WaterElement : BaseElement, IHasSecondAmount
{
    private const string TempWaterPower = "tempWaterPower";
    private const string DecrementAmount = "decrementAmount";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new(TempWaterPower + "Base", 0),
            new(TempWaterPower + "Extra", -1),
            new CustomCalculatedVar(TempWaterPower).WithMultiplier(
                (power, _) => GetDamageAdditive(power)
            ),
            new(DecrementAmount + "Base", 0),
            new(DecrementAmount + "Extra", 1),
            new CustomCalculatedVar(DecrementAmount).WithMultiplier(
                (power, _) => GetDecrementAmount(power)
            ),
        ];

    public override decimal ModifyDamageAdditive(
        Creature? target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource
    )
    {
        if (Owner != dealer || !props.IsPoweredAttack())
            return 0M;
        return GetDamageAdditive(this);
    }

    public override async Task AfterSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants
    )
    {
        if (side != CombatSide.Enemy)
        {
            return;
        }

        await PowerCmd.ModifyAmount(choiceContext, this, -GetDecrementAmount(this), null, null);
    }

    private static decimal GetDamageAdditive(PowerModel power)
    {
        return -Math.Ceiling((decimal)power.Amount / 3);
    }

    private static decimal GetDecrementAmount(PowerModel power)
    {
        var k = power.Amount / 10;
        return 1 + 4 * k;
    }

    public string GetSecondAmount() => $"{GetDamageAdditive(this)}";
}
