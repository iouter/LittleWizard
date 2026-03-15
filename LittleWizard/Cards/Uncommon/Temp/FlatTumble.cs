using System.Collections.Generic;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class FlatTumble() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(7)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.GainBlock(this, DynamicVars.Block.Value).Execute(choiceContext);
        
        // Return this card to hand
        var player = choiceContext.GetPlayer();
        var playedCard = play.Card;
        player.DiscardPile.Remove(playedCard);
        player.Hand.Add(playedCard);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);
    }
}
