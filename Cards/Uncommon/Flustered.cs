using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class Flustered() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override CardKeyword[] CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (Owner.Creature.Player is { PlayerCombatState: not null })
        {
            var handCards = Owner.Creature.Player.PlayerCombatState.Hand.Cards;
            while (handCards.Count == 10)
            {
                var card = (await CardPileCmd.Draw(choiceContext, 1, Owner)).FirstOrDefault();
                if (card?.EnergyCost != null && card.EnergyCost.GetResolved() == 0) break;
            }
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}