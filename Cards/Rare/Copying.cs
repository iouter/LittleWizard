using LittleWizard.Api;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Rare;

public class Copying() : LittleWizardCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Select up to 3 cards from other characters' 9 cards and add to hand
        var allCards = CardDatabase.GetAllCards().Where(c => c.CharacterId != "LittleWizard").Take(9).ToList();
        var selectedCards = await CommonActions.SelectCards(this, "Select up to 3 cards", choiceContext, allCards, 3);
        
        foreach (var card in selectedCards)
        {
            await Owner.Player.Hand.AddCard(card.CreateCopy());
        }
    }

    protected override void OnUpgrade()
    {
        // Already 0 energy by default
    }
}
