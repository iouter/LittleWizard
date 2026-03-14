using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class ElementAggregation() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Add a random element-applying card to hand
        var elementCards = CardPool.GetAllCards().Where(c => c is IElementCard).ToList();
        if (elementCards.Count == 0) return;
        
        var randomCard = elementCards[MegaCrit.Sts2.Core.Random.Rng.Chaotic.NextInt(0, elementCards.Count)];
        // This would need specific API to add card to hand with 0 cost
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        // Also: Cards given are free this turn
    }
}
