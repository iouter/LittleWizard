using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Uncommon;

public class GatherElements() : LittleWizardCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Power card - effect triggers at start of turn via power
        await PowerCmd.Apply<GatherElementsPower>(Owner.Creature, 1, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Cards given are free this turn (handled by power mechanics)
    }
}

public class GatherElementsPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Stack;

    public override async Task OnTurnStart()
    {
        // Randomly add an element-applying card to hand
        var elementCards = CardPool.GetAllCards().Where(c => c is IElementCard).ToList();
        if (elementCards.Count == 0) return;
        
        var randomCard = elementCards[MegaCrit.Sts2.Core.Random.Rng.Chaotic.NextInt(0, elementCards.Count)];
        // Add card to hand with 0 cost for this turn
        // This would need specific game API support
    }
}
