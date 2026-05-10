using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

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
            return;

        for (var i = 0; i < DynamicVars.Repeat.BaseValue; i++)
        {
            var targets = CombatState.HittableEnemies;
            var target = Owner.RunState.Rng.CombatTargets.NextItem(targets);
            if (target == null)
                continue;

            decimal damageValue = DynamicVars.Damage.ToDecimal(null);

            await new AttackCommand(damageValue)
                .FromCard(this)
                .WithHitFx("vfx/vfx_fire_ball")
                .Targeting(target)
                .Execute(choiceContext);

            // 施加火焰元素能力
            await Utils.GivePower<FireElement>(target, DynamicVars, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1);
    }
}
