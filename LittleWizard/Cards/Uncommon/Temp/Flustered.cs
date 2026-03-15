using System.Collections.Generic;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class Flustered() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override CardKeyword[] CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var player = choiceContext.GetPlayer();
        var hand = player.Hand;
        var deck = player.DrawPile;
        
        while (hand.Count < player.MaxHandSize && deck.Count > 0)
        {
            var card = deck[0];
            deck.RemoveAt(0);
            hand.Add(card);
            
            if (card.Cost == 0)
            {
                break;
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cost.UpgradeValueBy(-1); // Reduce cost from 2 to 1
    }
}
