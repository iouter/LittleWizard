using LittleWizard.Api;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class Petrification() : LittleWizardCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(8)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.GainBlock(this, DynamicVars.Block.IntValue).Execute(choiceContext);
        
        // Increase block for future plays of this card
        DynamicVars.Block.UpgradeValueBy(8);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(8);
    }
}
