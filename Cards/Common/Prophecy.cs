using BaseLib.Utils;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Common;

public class Prophecy() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var card = await CommonActions.SelectSingleCard(
            this,
            SelectionScreenPrompt,
            choiceContext,
            PileType.Discard
        );
        if (card == null)
            return;
        await CardPileCmd.Add(card, PileType.Draw);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
