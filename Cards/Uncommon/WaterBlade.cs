using System.Diagnostics;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class WaterBlade()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<WaterElement>(6)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Debug.Assert(CombatState != null, nameof(CombatState) + " != null");
        foreach (var enemy in CombatState.HittableEnemies)
        {
            if (enemy.GetPowerAmount<WaterElement>() <=
                DynamicVarsHelper.GetPowerVar<WaterElement>(DynamicVars).BaseValue) continue;
            if (enemy.Block > 0) await CreatureCmd.LoseBlock(enemy, enemy.Block);
            await PowerCmd.Remove<WaterElement>(enemy);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}