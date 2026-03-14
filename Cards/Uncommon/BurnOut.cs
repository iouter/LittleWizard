using LittleWizard.Api;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class BurnOut() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(6),
        new CalculationExtraVar(2)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        
        var fireAmount = cardPlay.Target.GetPowerAmount<FireElement>();
        if (fireAmount > 0)
        {
            await PowerCmd.Remove<FireElement>(cardPlay.Target);
            
            var totalDamage = fireAmount * DynamicVars.CalculationBase.BaseValue;
            await CreatureCmd.Damage(choiceContext, cardPlay.Target, totalDamage, ValueProp.Move, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2);
    }
}
