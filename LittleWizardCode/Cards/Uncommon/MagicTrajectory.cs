using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class MagicTrajectory()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string ExtraCards = "ExtraCards";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(2), new(ExtraCards, 2), new PowerVar<BaseElement>(10)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.Draw(this, choiceContext);
        if (
            await ElementHelper.RemoveElementAtMost(
                this,
                choiceContext,
                cardPlay,
                DynamicVars.Power<BaseElement>().BaseValue
            )
        )
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars[ExtraCards].BaseValue, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ExtraCards].UpgradeValueBy(1);
    }
}
