using LittleWizard.Api.Animation;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class MagicMissile()
    : LittleWizardCard(0, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy)
{
    private static readonly Random _random = new Random();

    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(3, ValueProp.Unpowered)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner?.Creature == null)
            return;

        var combatState = Owner.Creature.CombatState;
        if (combatState == null)
            return;

        var enemies = combatState.HittableEnemies?.ToList();
        if (enemies == null || enemies.Count == 0)
            return;

        int x = ResolveEnergyXValue();
        int times = x * x;
        int damagePerHit = IsUpgraded ? 4 : 3;

        await AnimationHelper.TriggerCastAnimationOwner(this);

        for (int i = 0; i < times; i++)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Attack", 0.05f);
            var target = enemies[_random.Next(enemies.Count)];
            await CreatureCmd.Damage(
                choiceContext,
                target,
                damagePerHit,
                ValueProp.Unpowered,
                Owner.Creature,
                this
            );
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
    }
}
