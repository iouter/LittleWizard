using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class ElementBlessingPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player) return;
        
        // Check if the card applies elements
        if (CardAppliesElement(cardPlay.Card))
        {
            await PlayerCmd.GainEnergy(1, cardPlay.Card.Owner);
        }
    }

    private bool CardAppliesElement(CardModel card)
    {
        // Check if card has element application logic
        // This would need to be implemented based on your element system
        return card is LittleWizardCard lwCard && lwCard.AppliesElement();
    }
}
