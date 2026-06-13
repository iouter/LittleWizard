using BaseLib.Abstracts;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers;

public class WaterTempPower : LittleWizardPower, IHasSecondAmount
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool ShouldReceiveCombatHooks => true;

    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            foreach (var v in base.CanonicalVars)
                yield return v;
            yield return new DynamicVar("tempWaterPower", 0);
        }
    }

    public override int DisplayAmount => -(int)Math.Ceiling((decimal)Amount / 3);

    public string GetSecondAmount()
    {
        return $"{Amount}";
    }

    private void UpdateTempWaterPowerVar()
    {
        int reduction = (int)Math.Ceiling((decimal)Amount / 3);
        DynamicVars["tempWaterPower"].BaseValue = reduction;
        InvokeDisplayAmountChanged();
    }

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        UpdateTempWaterPowerVar();
        await Task.CompletedTask;
    }

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power == this)
        {
            UpdateTempWaterPowerVar();
        }
        await Task.CompletedTask;
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
        return -Math.Ceiling((decimal)(Amount / 3));
    }
}
