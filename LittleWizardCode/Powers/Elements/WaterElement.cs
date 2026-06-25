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

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new(TempWaterPower + "Base", 0),
            new(TempWaterPower + "Extra", -1),
            new CustomCalculatedVar("tempWaterPower").WithMultiplier(
                (power, _) => GetDamageAdditive(power)
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

        await PowerCmd.Decrement(this);
    }

    private static decimal GetDamageAdditive(PowerModel power)
    {
        return -Math.Ceiling((decimal)power.Amount / 3);
    }

    public string GetSecondAmount() => $"{GetDamageAdditive(this)}";
}
