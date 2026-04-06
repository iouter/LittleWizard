using System.Diagnostics;
using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Cards;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class Ignite()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(5, ValueProp.Move), new PowerVar<IgnitePower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        Debug.Assert(play.Target != null);
        if (play.Target.GetPowerAmount<FireElement>() <= 0)
            await Utils.GivePower<IgnitePower>(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}
