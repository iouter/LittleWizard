using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class WaterUnderTheBridge()
    : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<WaterUnderTheBridgePower>(5)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await Utils.GivePower<WaterUnderTheBridgePower>(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<WaterUnderTheBridgePower>(DynamicVars).UpgradeValueBy(2);
    }
}
