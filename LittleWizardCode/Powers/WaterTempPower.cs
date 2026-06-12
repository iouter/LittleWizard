using System;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers;

public class WaterTempPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override int DisplayAmount => -(int)Math.Ceiling((decimal)Amount / 3);

    public override decimal ModifyDamageAdditive(
        Creature? target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource
    )
    {
        return Owner != dealer || !Utils.IsPoweredAttack(props)
            ? 0M
            : -Math.Ceiling((decimal)(Amount / 3));
    }
}
