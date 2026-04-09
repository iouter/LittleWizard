using BaseLib.Abstracts;
using BaseLib.Utils;
using LittleWizard.Api.Cards;
using LittleWizard.Cards.Basic;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Others;

public sealed class Turnback()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Ancient, TargetType.Self),
        ITranscendenceCard
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

    public CardModel GetTranscendenceTransformedCard()
    {
        return ModelDb.Card<Callback>();
    }
}
