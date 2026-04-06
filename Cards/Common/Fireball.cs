using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class Fireball()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FireElement>(3)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await Utils.GivePower<FireElement>(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<FireElement>(DynamicVars).UpgradeValueBy(1);
    }
}
