using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using LittleWizard.LittleWizardCode.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class BrewPotions()
    : LittleWizardCard(0, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BrewPotionsPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<BrewPotionsPower>(this, cardPlay, choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<BrewPotionsPower>(DynamicVars).UpgradeValueBy(1);
    }
}
