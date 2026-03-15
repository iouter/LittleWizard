using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class LifeDrain() : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override CardKeyword[] CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var target = play.Target;
        var player = choiceContext.GetPlayer();
        
        // Deal life loss (5 damage that ignores block)
        await CommonActions.DealDamage(target, 5, DamageType.LifeLoss).Execute(choiceContext);
        
        // Heal self for 2
        await CommonActions.Heal(player, 2).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        // Remove Exhaust on upgrade - handled by not applying exhaust keyword when upgraded
        // This is a special case where we need to conditionally apply keywords
    }
    
    public override CardKeyword[] GetKeywords()
    {
        if (IsUpgraded)
        {
            return []; // No keywords when upgraded
        }
        return base.GetKeywords();
    }
}
