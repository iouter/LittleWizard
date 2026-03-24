using LittleWizard.Api.Cards;
using LittleWizard.Api.Interface;
using LittleWizard.Character;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Uncommon;

public class LikeNew() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Creature.Player == null) return;
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
        var cardToExhaust =
            (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Hand.GetPile(Owner).Cards.ToList(), Owner,
                prefs)).FirstOrDefault();

        if (cardToExhaust == null) return;
        var cost = cardToExhaust.EnergyCost.GetResolved();
        await CardCmd.Exhaust(choiceContext, cardToExhaust);

        var card = CardFactory.GetDistinctForCombat(Owner,
            ModelDb.CardPool<LittleWizardCardPool>().GetUnlockedCards(Owner.UnlockState,
                Owner.RunState.CardMultiplayerConstraint).Where(model => model is IElementCard),
            1, Owner.Creature.Player.RunState.Rng.CombatCardSelection).FirstOrDefault();

        if (card == null) return;
        card.EnergyCost.SetThisCombat(cost);
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}