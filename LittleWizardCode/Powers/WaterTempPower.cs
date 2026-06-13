using BaseLib.Cards.Variables;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers;

public class WaterTempPower : LittleWizardPower
{
    private const string TempWaterPower = "tempWaterPower";
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new(TempWaterPower + "Base", 0),
            new(TempWaterPower + "Extra", -1),
            new CustomCalculatedVar("tempWaterPower").WithMultiplier(
                (power, _) => GetDamageAdditive(power)
            ),
        ];

    public override int DisplayAmount => (int)GetDamageAdditive(this);

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
}
