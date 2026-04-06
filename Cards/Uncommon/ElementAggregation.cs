using LittleWizard.Api.Cards;
using LittleWizard.Api.Powers;
using LittleWizard.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Uncommon;

public class ElementAggregation()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Creature.Player == null)
            return;
        var card = CardFactory
            .GetDistinctForCombat(
                Owner,
                ModelDb
                    .CardPool<LittleWizardCardPool>()
                    .GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint)
                    .Where(ElementHelper.IsElementCard),
                1,
                Owner.Creature.Player.RunState.Rng.CombatCardSelection
            )
            .FirstOrDefault();
        if (card == null)
            return;
        if (IsUpgraded)
            card.SetToFreeThisTurn();
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
