using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Powers;
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
                ElementHelper.FireAndWater(
                    new ThrowingPlayerChoiceContext(),
                    Owner,
                    Amount,
                    amount,
                    applier
                );
                modifiedAmount = 0;
                return true;
            }
            case EarthElement earth:
            {
                ElementHelper.WaterAndEarth(
                    new ThrowingPlayerChoiceContext(),
                    Owner,
                    Amount,
                    amount,
                    applier
                );
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

    public override decimal ModifyDamageAdditive(
        Creature? target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource
    )
    {
        if (Owner != dealer || !Utils.IsPoweredAttack(props))
            return 0M;
        return GetDamageAdditive(this);
    }

    private static decimal GetDamageAdditive(PowerModel power)
    {
        return -Math.Ceiling((decimal)power.Amount / 3);
    }

    public string GetSecondAmount() => $"{GetDamageAdditive(this)}";
}
