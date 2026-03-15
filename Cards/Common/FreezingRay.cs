using System.Diagnostics;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Common;

public class FreezingRay()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(6),
        new ExtraDamageVar(1),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, target) =>
            target?.GetPowerAmount<WaterElement>() ?? 0)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Debug.Assert(CombatState != null, nameof(CombatState) + " != null");
        await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate(cardPlay.Target)).FromCard(this)
            .TargetingAllOpponents(CombatState).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }
}