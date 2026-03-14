using LittleWizard.Api;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Cards;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class ExploreTaboo() : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<FireElement>(3),
        new PowerVar<ExploreTabooPower>(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<FireElement>(this, cardPlay);
        await Utils.GivePower<ExploreTabooPower>(this, cardPlay);
    }

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Ethereal
    ];

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<ExploreTabooPower>(DynamicVars).UpgradeValueBy(1);
    }
}