using LittleWizard.Api.Cards;
using LittleWizard.Cards.Others;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Rare;

public class Retrain() : LittleWizardCard(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(3)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        for (var i = 0; i < DynamicVars.Cards.IntValue; i++)
            await CardPileCmd.AddGeneratedCardToCombat(
                ModelDb.Card<PassOver>(),
                PileType.Hand,
                true
            );
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
