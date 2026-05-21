using LittleWizard.LittleWizardCode.Api.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Relics;

public class ArcaneTome : AfterElementReactRelics
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override async Task AfterElementReact(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (owner != Owner.Creature)
        {
            return;
        }
        Flash();
        await CardPileCmd.Draw(
            new ThrowingPlayerChoiceContext(),
            DynamicVars.Cards.BaseValue,
            Owner
        );
    }
}
