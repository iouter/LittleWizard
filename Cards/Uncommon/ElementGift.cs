using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class ElementGift() : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<ElementGiftPower>(Owner.Creature, 1, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}

public class ElementGiftPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task OnTurnStart()
    {
        // Randomly get an element-applying card, make it free this turn
        var elementCards = CardPool.GetAllCards().Where(c => c is IElementCard).ToList();
        if (elementCards.Count == 0) return;
        
        // This needs specific API support for adding cards to hand with 0 cost
    }
}
