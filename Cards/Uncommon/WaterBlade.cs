using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class WaterBlade() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies), IElementCard
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.PlayerCombatState == null) return;
        
        foreach (var enemy in Owner.PlayerCombatState.Enemies)
        {
            var waterAmount = enemy.GetPowerAmount<WaterElement>();
            if (waterAmount >= 6)
            {
                // Remove block from enemy
                enemy.CurrentBlock = 0;
            }
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
