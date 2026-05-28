using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class MagicMissile()
    : LittleWizardCard(0, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy)
{
    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(3, ValueProp.Unpowered)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int times = ResolveEnergyXValue() * ResolveEnergyXValue();

        await CommonActions
            .CardAttack(this, cardPlay, hitCount: times)
            .Unpowered()
            .Execute(choiceContext);

        var target = cardPlay.Target;
        if (target == null && CombatState != null)
        {
            var enemies = CombatState.HittableEnemies;
            if (enemies.Count > 0)
                target = Owner.RunState.Rng.CombatTargets.NextItem(enemies);
        }
        if (target == null)
            return;

        var flutter = target.GetPower<FlutterPower>();
        if (flutter != null)
        {
            for (int i = 0; i < times; i++)
            {
                if (flutter.Amount <= 0)
                    break;
                await PowerCmd.Decrement(flutter);
            }
        }

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
    }
}
