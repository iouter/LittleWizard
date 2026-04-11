using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Powers.Elements.Reacts;

public class WaterAndEarthElementReactorPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        PowerCmd.Apply<ElementTemporaryStrengthPower>(Owner, Amount, applier, cardSource);
        PowerCmd.Apply<ElementBlockPower>(Owner, Amount, applier, cardSource);
        PowerCmd.Remove(this);
        return Task.CompletedTask;
    }
}
