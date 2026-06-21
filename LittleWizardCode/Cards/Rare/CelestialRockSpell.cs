using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class CelestialRockSpell()
    : LittleWizardCard(3, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(48, ValueProp.Move), new PowerVar<FireElement>(10)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Fire];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var targets = CombatState!.HittableEnemies;
        var target = Owner.RunState.Rng.CombatTargets.NextItem(targets);
        if (target == null)
        {
            return;
        }
        var damageValue = DynamicVars.Damage.BaseValue;

        await new AttackCommand(damageValue)
            .FromCard(this)
            .WithHitFx("vfx/vfx_fire_ball")
            .Targeting(target)
            .Execute(choiceContext);

        await CommonActions.Apply<FireElement>(choiceContext, target, this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}
