using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class EmergePower : AfterElementReactPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override async Task AfterElementReact(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (owner != Owner)
            return;
        await ElementHelper.RandomElement(ctx, owner, Amount, applier, cardSource);
    }
}
