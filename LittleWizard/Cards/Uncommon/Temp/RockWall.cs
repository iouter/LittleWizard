using System.Collections.Generic;
using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class RockWall() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(25)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.GainBlock(this, DynamicVars.Block.Value).Execute(choiceContext);
        
        // Apply a debuff preventing element cards for 2 turns
        var player = choiceContext.GetPlayer();
        await Utils.ApplyPower(player, typeof(NoElementCardsPower), 2);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(5);
    }
}

// Custom power to prevent playing element cards
public class NoElementCardsPower : PowerModel<NoElementCardsPower>
{
    public override int BaseAmount => 2;
    
    public override bool ShouldPreventCardPlay(Card card)
    {
        return card is IElementCard;
    }
}
