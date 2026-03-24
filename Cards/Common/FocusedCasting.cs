using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Interface;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class FocusedCasting() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.Self), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<FocusedCastingPower>(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<FocusedCastingPower>(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<FocusedCastingPower>(DynamicVars).UpgradeValueBy(1);
    }
}