using System.Collections.Generic;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class WildSwing() : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var self = choiceContext.GetPlayer();
        await CommonActions.ApplyDebuff(self, DebuffType.Fragile, 1).Execute(choiceContext);
        
        var damageCmd = CommonActions.CardAttack(this, play.Target);
        // Base: 2 hits, Upgrade adds 1 more hit per upgrade level
        int hitCount = IsUpgraded ? 3 : 2;
        for (int i = 0; i < hitCount; i++)
        {
            await damageCmd.Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        // Damage stays same, just extra hit handled in OnPlay
    }
}
