using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class Earthquake()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(5, ValueProp.Move),
            new PowerVar<EarthElement>(3),
            new PowerVar<VulnerablePower>(1),
            new PowerVar<WeakPower>(1),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await Utils.GivePower<EarthElement>(this, play);
        await Utils.GivePower<VulnerablePower>(this, play);
        await Utils.GivePower<WeakPower>(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Vulnerable.UpgradeValueBy(1);
        DynamicVars.Weak.UpgradeValueBy(1);
    }
}
