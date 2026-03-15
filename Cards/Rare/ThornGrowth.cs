using LittleWizard.Api;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class ThornGrowth() : LittleWizardCard(0, CardType.Skill, CardRarity.Rare, TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(2, ValueProp.Move),
        new PowerVar<Thorns>(7),
        new BlockVar(7)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Deal 2 damage to self and all allies
        await CommonActions.DamageSelf(this, 2).Execute(choiceContext);
        
        // Gain 7 Thorns and 7 Block
        await Utils.GivePower<Thorns>(this, cardPlay);
        await CommonActions.GainBlock(this, DynamicVars.Block.IntValue).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<Thorns>(DynamicVars).UpgradeValueBy(3);
        DynamicVars.Block.UpgradeValueBy(2);
    }
}
