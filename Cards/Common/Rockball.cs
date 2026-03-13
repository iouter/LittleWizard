using LittleWizard.Cards.Interface;
using LittleWizard.Localization.DynamicVars;
using LittleWizard.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class Rockball() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<EarthElement>(3)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await PowerCmd.Apply<EarthElement>(play.Target, DynamicVarsHelper.GetPowerVar<EarthElement>(DynamicVars).BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<EarthElement>(DynamicVars).UpgradeValueBy(3);
    }
}