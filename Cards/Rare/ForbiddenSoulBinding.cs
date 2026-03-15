using LittleWizard.Api;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class ForbiddenSoulBinding() : LittleWizardCard(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DrainVar(15)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target != null)
        {
            // Deal 15 life drain + additional based on target's block
            var blockAmount = target.CurrentBlock;
            var totalDrain = DynamicVars.Drain.IntValue + blockAmount;
            
            await CommonActions.DamageTarget(this, target, totalDrain).Execute(choiceContext);
            await CommonActions.Heal(this, Owner.Creature, totalDrain).Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade removes Exhaust keyword
    }
}
