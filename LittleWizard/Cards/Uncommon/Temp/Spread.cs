using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class Spread() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override CardKeyword[] CanonicalKeywords => [CardKeyword.Retain];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var target = play.Target;
        var allEnemies = choiceContext.GetEnemies().ToList();
        
        // Get all element powers from the target
        var elementPowers = target.Powers.Where(p => IsElementPower(p)).ToList();
        
        // Apply those elements to other enemies
        foreach (var otherEnemy in allEnemies.Where(e => e != target))
        {
            foreach (var elementPower in elementPowers)
            {
                await Utils.ApplyPower(otherEnemy, elementPower.PowerType, elementPower.Stacks);
            }
        }
    }

    private bool IsElementPower(Power power)
    {
        // Check if this is an element power (Fire, Water, Earth)
        return power.PowerType == typeof(FireElement) || 
               power.PowerType == typeof(WaterElement) || 
               power.PowerType == typeof(EarthElement);
    }
}
