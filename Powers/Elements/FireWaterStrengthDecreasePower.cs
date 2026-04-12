using BaseLib.Abstracts;
using LittleWizard.Api.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Powers.Elements.Reacts;

public class FireWaterStrengthDecreasePower : CustomTemporaryPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override string CustomPackedIconPath =>
        "res://LittleWizard/images/powers/fire_and_water_element_reactor_power.png";
    public override string CustomBigIconPath =>
        "res://LittleWizard/images/powers/big/fire_and_water_element_reactor_power.png";
    public override PowerModel InternallyAppliedPower => ModelDb.Power<StrengthPower>();
    public override AbstractModel OriginModel => ModelDb.Power<FireAndWaterElementReactorPower>();

    protected override Func<Creature, decimal, Creature?, CardModel?, bool, Task> ApplyPowerFunc =>
        async (target, amount, applier, cardSource, _) =>
        {
            await PowerCmd.Apply<StrengthPower>(target, -amount, applier, cardSource);
        };
}
