using System.Collections.Generic;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class MorningGrumpiness() : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override CardKeyword[] CanonicalKeywords => [CardKeyword.Innate];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        // End turn immediately after gaining block and applying effects
        await CommonActions.GainBlock(this, 20).Execute(choiceContext);
        
        // Apply Stun (skip next turn) - 2 stacks
        var player = choiceContext.GetPlayer();
        await Utils.ApplyPower(player, typeof(StunPower), 2);
        
        // When stun is removed, gain 6 permanent Strength
        // This requires a custom trigger mechanism
        await Utils.ApplyCustomEffect(player, "MorningGrumpinessTrigger", 1);
    }

    protected override void OnUpgrade()
    {
        // Already Innate at base
    }
}
