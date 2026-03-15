using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class ManaShieldPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterTurnStart(PlayerChoiceContext choiceContext, MegaCrit.Sts2.Core.Entities.Players.Player player)
    {
        if (Owner != player.Creature) return;
        
        // Gain 3 block at start of turn
        await PlayerCmd.GainBlock(3, player);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player) return;
        
        // Check if the card applies elements
        if (CardAppliesElement(cardPlay.Card))
        {
            await PlayerCmd.GainBlock(2, cardPlay.Card.Owner);
        }
    }

    protected override async Task OnApply()
    {
        // Gain 4 block when played
        await PlayerCmd.GainBlock(4, Owner.Player);
    }

    private bool CardAppliesElement(CardModel card)
    {
        // Check if card has element application logic
        return card is LittleWizardCard lwCard && lwCard.AppliesElement();
    }
}
