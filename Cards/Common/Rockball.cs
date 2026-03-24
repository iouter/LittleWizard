using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class Rockball() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<EarthElement>(3)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await Utils.GivePower<EarthElement>(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<EarthElement>(DynamicVars).UpgradeValueBy(3);
    }
}