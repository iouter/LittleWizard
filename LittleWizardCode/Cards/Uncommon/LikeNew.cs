using System.Reflection;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Character;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class LikeNew() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    private static PropertyInfo? _hasEnergyCostXProperty;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Creature.Player == null)
            return;
        var cardToExhaust = await CommonActions.SelectSingleCard(
            this,
            CardSelectorPrefs.ExhaustSelectionPrompt,
            choiceContext,
            PileType.Hand
        );

        if (cardToExhaust == null)
            return;
        int cost;
        if (cardToExhaust.Keywords.Contains(CardKeyword.Unplayable))
        {
            cost = 0;
        }
        else if (GetHasEnergyCostX(cardToExhaust))
        {
            cost = 1;
        }
        else
        {
            cost = cardToExhaust.EnergyCost.GetResolved();
        }
        await CardCmd.Exhaust(choiceContext, cardToExhaust);

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
        card.EnergyCost.SetThisCombat(cost);
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }

    private static bool GetHasEnergyCostX(object cardToExhaust)
    {
        ArgumentNullException.ThrowIfNull(cardToExhaust);

        // 缓存 PropertyInfo 避免重复反射
        if (_hasEnergyCostXProperty == null)
            _hasEnergyCostXProperty = cardToExhaust
                .GetType()
                .GetProperty(
                    "HasEnergyCostX",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public
                );

        if (_hasEnergyCostXProperty == null)
            throw new InvalidOperationException(
                "Property 'HasEnergyCostX' not found on type " + cardToExhaust.GetType()
            );

        return (bool)(_hasEnergyCostXProperty.GetValue(cardToExhaust) ?? false);
    }
}
