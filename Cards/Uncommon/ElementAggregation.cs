using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Character;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

namespace LittleWizard.Cards.Uncommon;

public class ElementAggregation() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var card = CardFactory.GetDistinctForCombat(Owner,
            ModelDb.CardPool<LittleWizardCardPool>().GetUnlockedCards(Owner.UnlockState,
                Owner.RunState.CardMultiplayerConstraint).Where(model => model is IElementCard),
            1, Rng.Chaotic).FirstOrDefault();
        if (card == null)
        {
            return;
        }
        if (IsUpgraded)
        {
            card.SetToFreeThisTurn();
        }
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
