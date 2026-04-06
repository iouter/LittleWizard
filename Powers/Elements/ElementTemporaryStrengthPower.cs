using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Powers.Elements;

public class ElementTemporaryStrengthPower : CustomTemporaryPowerModel
{
    protected override Func<Creature, decimal, Creature?, CardModel?, bool, Task> ApplyPowerFunc =>
        PowerCmd.Apply<StrengthPower>;

    public override PowerModel InternallyAppliedPower => ModelDb.Power<StrengthPower>();
    public override AbstractModel OriginModel => ModelDb.Power<FireAndWaterElementReactorPower>();
}
