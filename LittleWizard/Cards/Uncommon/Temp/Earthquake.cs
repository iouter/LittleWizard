using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class Earthquake() : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5, ValueProp.Move), new PowerVar<EarthElement>(3)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        await Utils.GivePower<EarthElement>(this, play);
        
        // Apply Fragile and Weak (1 stack each base, 2 stacks upgraded)
        int debuffStacks = IsUpgraded ? 2 : 1;
        foreach (var enemy in choiceContext.GetEnemies())
        {
            await CommonActions.ApplyDebuff(enemy, DebuffType.Fragile, debuffStacks).Execute(choiceContext);
            await CommonActions.ApplyDebuff(enemy, DebuffType.Weak, debuffStacks).Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}
