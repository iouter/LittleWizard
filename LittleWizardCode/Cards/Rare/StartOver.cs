using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class StartOver() : LittleWizardCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.PlayerCombatState == null)
            return;

        var handCards = Owner.PlayerCombatState.Hand.Cards.ToList();
        var count = handCards.Count;

        foreach (var card in handCards)
            await CardCmd.Exhaust(choiceContext, card);
        await CardPileCmd.Draw(choiceContext, count, Owner);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
