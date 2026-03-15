using System.Collections.Generic;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class Ignite() : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var target = play.Target;
        
        // Deal damage
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        
        // Check if target doesn't have Fire element
        var hasFire = target.Powers.Any(p => p.PowerType == typeof(FireElement));
        
        if (!hasFire)
        {
            // Apply a buff that doubles next fire element application
            await Utils.ApplyCustomEffect(target, "DoubleFire", 1);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}
