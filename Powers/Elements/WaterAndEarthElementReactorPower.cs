using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

namespace LittleWizard.Powers.Elements;

public class WaterAndEarthElementReactorPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        if (Amount > Rng.Chaotic.NextInt(0, 100))
            await CreatureCmd.Stun(Owner);
        await PowerCmd.Remove(this);
    }
}
