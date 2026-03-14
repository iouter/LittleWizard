using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Uncommon;

public class ManaConvert() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        var cards = await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Hand.GetPile(Owner)
                .Cards.Where(c => c is IElementCard || c.Enchantment is IElementCard).ToList(), Owner, prefs);
        var cardModels = cards as CardModel[] ?? cards.ToArray();
        if (cardModels.Length == 0)
        {
            return;
        }
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
