using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Animation;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Rare;

public class CelestialRockSpell()
    : LittleWizardCard(2, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(48, ValueProp.Move), new PowerVar<FireElement>(10), new RepeatVar(1)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (CombatState == null)
        {
            return;
        }
        for (var i = 0; i < DynamicVars.Repeat.BaseValue; i++)
        {
            var targets = CombatState.HittableEnemies;
            var target = Owner.RunState.Rng.CombatTargets.NextItem(targets);
            if (target == null)
                continue;
            await CommonActions.CardAttack(this, target).Execute(choiceContext);
            await Utils.GivePower<FireElement>(target, DynamicVars, Owner.Creature, this);
            await AnimationHelper.TriggerCastAnimationOwner(this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1);
    }
}
