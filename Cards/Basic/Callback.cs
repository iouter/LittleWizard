using LittleWizard.Api.Cards;
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Basic;

public sealed class Callback()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        var card = (
            await CardSelectCmd.FromSimpleGrid(
                choiceContext,
                PileType.Discard.GetPile(Owner).Cards.Where(ElementHelper.IsElementCard).ToList(),
                Owner,
                prefs
            )
        ).FirstOrDefault();
        if (card == null)
            return;
        await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Top);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
