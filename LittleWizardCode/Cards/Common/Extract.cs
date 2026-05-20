using System.Diagnostics;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Common;

public class Extract() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new RandomElementVar(2), new CardsVar(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ElementHelper.RandomElement(
            cardPlay.Target!,
            DynamicVarsHelper.GetRandomElementVar(DynamicVars).BaseValue,
            Owner.Creature,
            this
        );
        await CommonActions.Draw(this, choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetRandomElementVar(DynamicVars).UpgradeValueBy(1);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}
