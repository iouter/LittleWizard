using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class SelfHypnosis()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BlockVar(17, ValueProp.Move), new CardsVar(3)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        await AnimationHelper.TriggerCastAnimationOwner(this);

        var cards = await CommonActions.SelectCards(
            this,
            SelectionScreenPrompt,
            choiceContext,
            PileType.Hand,
            0,
            DynamicVars.Cards.IntValue
        );
        foreach (var card in cards)
            if (card.Keywords.Contains(CardKeyword.Ethereal))
                card.RemoveKeyword(CardKeyword.Ethereal);
            else
                card.AddKeyword(CardKeyword.Ethereal);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}
