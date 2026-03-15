using System.Collections.Generic;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class GoneWithTheWind() : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        // Passive effect: gain block when playing Exhaust cards
        // This is handled by a custom power
        var player = choiceContext.GetPlayer();
        await Utils.ApplyPower(player, typeof(GoneWithTheWindPower), 1);
    }

    protected override void OnUpgrade()
    {
        // Upgrade increases block from 5 to 7
        // Handled in the power class
    }
}

// Custom power to track exhaust card plays
public class GoneWithTheWindPower : PowerModel<GoneWithTheWindPower>
{
    public override int BaseAmount => 5;
    
    public override async Task OnExhaustCardPlayed(PlayerChoiceContext context, Card card)
    {
        var player = context.GetPlayer();
        var blockAmount = IsUpgraded ? 7 : 5;
        await CommonActions.GainBlock(null, blockAmount).Execute(context);
    }
}
