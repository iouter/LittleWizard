using LittleWizard.Api.Cards;
using LittleWizard.Character;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Rare;

public class Copying() : LittleWizardCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    private const string Selected = "Selected";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new(Selected, 9),
        new CardsVar(3)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Creature.Player == null) return;
        var allCards = new List<CardModel>();
        foreach (var cardPool in ModelDb.AllCharacterCardPools)
        {
            if (cardPool == ModelDb.CardPool<LittleWizardCardPool>()) continue;
            allCards.AddRange(cardPool.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint));
        }

        var canSelectedCards = CardFactory.GetDistinctForCombat(Owner, allCards, DynamicVars[Selected].IntValue,
            Owner.Creature.Player.RunState.Rng.CombatCardSelection);
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, DynamicVars.Cards.IntValue);
        var cards = await CardSelectCmd.FromSimpleGrid(choiceContext, canSelectedCards.ToList(), Owner.Creature.Player,
            prefs);
        foreach (var card in cards) await CardPileCmd.Add(card, PileType.Hand);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}