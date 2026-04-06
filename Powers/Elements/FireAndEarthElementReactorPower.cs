using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Elements;

public class FireAndEarthElementReactorPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        await CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            Owner,
            Amount,
            ValueProp.Unpowered,
            applier,
            cardSource
        );
        await PowerCmd.Remove(this);
    }
}
