using BaseLib.Utils;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Others;

//TODO: Updated by ArchaicTooth from Callback, waiting for Baselib support
public sealed class Turnback()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Ancient, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var task = CommonActions.SelectCards(
            this,
            SelectionScreenPrompt,
            choiceContext,
            PileType.Discard,
            0,
            2
        );
        if (!task.IsCompletedSuccessfully)
            return;
        foreach (var card in task.Result)
            await CommonActions.Draw(card, choiceContext);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
