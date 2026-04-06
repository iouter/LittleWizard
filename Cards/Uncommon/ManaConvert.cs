using LittleWizard.Api.Cards;
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Uncommon;

public class ManaConvert()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
        var cards = await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            PileType.Hand.GetPile(Owner).Cards.Where(ElementHelper.IsElementCard).ToList(),
            Owner,
            prefs
        );
        var cardModels = cards as CardModel[] ?? cards.ToArray();
        if (cardModels.Length == 0)
            return;
        foreach (var card in cardModels)
        {
            await CardCmd.Exhaust(choiceContext, card);
            await PlayerCmd.GainEnergy(1, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
