using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class RetrainPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private bool _hasTriggered = false;

    public override async Task AfterTurnStart(PlayerChoiceContext choiceContext, MegaCrit.Sts2.Core.Entities.Players.Player player)
    {
        if (Owner != player.Creature || _hasTriggered) return;
        
        _hasTriggered = true;
        
        // Create 3 "Skip" cards and add to hand
        for (int i = 0; i < 3; i++)
        {
            var skipCard = new SkipCard();
            await CardPileCmd.AddToHand(skipCard, player);
        }
    }

    public override async Task AfterCombatEnd()
    {
        _hasTriggered = false;
    }
}

// Custom Skip card definition
public class SkipCard : CustomCardModel
{
    public SkipCard() : base(0, CardType.Skill, CardRarity.Special, TargetType.Self, false, false)
    {
    }

    public override string Name => "略过";
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Discard all cards in hand and draw the same amount
        var handCards = cardPlay.Owner.Hand.Cards.ToList();
        int count = handCards.Count;
        
        foreach (var card in handCards)
        {
            await CardPileCmd.Move(card, cardPlay.Owner.DiscardPile);
        }
        
        await PlayerCmd.DrawCards(count, cardPlay.Owner);
    }
}
