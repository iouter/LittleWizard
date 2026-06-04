using BaseLib.Abstracts;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Cards.Basic;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Others;

public sealed class Turnback()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Ancient, TargetType.Self),
        ITranscendenceCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(3), new BlockVar(8, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        var cards = await CommonActions.SelectCards(
            this,
            SelectionScreenPrompt,
            choiceContext,
            PileType.Discard,
            0,
            DynamicVars.Cards.IntValue
        );
        await CardPileCmd.Add(cards, PileType.Hand);
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
