using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class Guidance() : LittleWizardCard(0, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<GuidancePower>(2)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<GuidancePower>(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<GuidancePower>(DynamicVars).UpgradeValueBy(1);
    }
}
