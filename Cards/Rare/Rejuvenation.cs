using LittleWizard.Api.Animation;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class Rejuvenation() : LittleWizardCard(6, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(3)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var prefs = new CardSelectorPrefs(
            CardSelectorPrefs.RemoveSelectionPrompt,
            0,
            DynamicVars.Cards.IntValue
        );
        var cards = await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            PileType
                .Deck.GetPile(Owner)
                .Cards.Where(c => !c.Keywords.Contains(CardKeyword.Eternal))
                .ToList(),
            Owner,
            prefs
        );
        foreach (var card in cards)
        {
            await AnimationHelper.TriggerCastAnimationOwner(this);
            await CardPileCmd.RemoveFromDeck(card);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
