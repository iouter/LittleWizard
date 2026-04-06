using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Powers.Elements;

public class FireAndWaterElementReactorPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        await PowerCmd.Apply<ElementTemporaryStrengthPower>(Owner, Amount, applier, cardSource);
        await PowerCmd.Apply<VulnerablePower>(Owner, Amount, applier, cardSource);
        await PowerCmd.Remove(this);
    }
}
