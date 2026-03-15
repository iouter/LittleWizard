using LittleWizard.Api;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class BurnEverything() : LittleWizardCard(-1, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new XVar(),
        new PowerVar<FireElement>(5)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var xValue = DynamicVars.X.IntValue;
        
        // Apply 5*X Fire Element to all enemies
        if (Owner.CombatState != null)
        {
            foreach (var enemy in Owner.CombatState.HittableEnemies)
            {
                var firePower = enemy.GetPower<FireElement>();
                if (firePower == null)
                {
                    await Utils.GivePower<FireElement>(enemy, DynamicVars, Owner.Creature, this);
                }
                else
                {
                    firePower.Stacks += 5 * xValue;
                }
            }
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade increases Fire Element from 4*X to 5*X (already 5*X by default)
    }
}
