using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class BrewPotions()
    : LittleWizardCard(0, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BrewPotionsPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<BrewPotionsPower>(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<BrewPotionsPower>(DynamicVars).UpgradeValueBy(1);
    }
}
