using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Common;

public class ErosionRay()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        ArgumentNullException.ThrowIfNull(play.Target);
        var debuffs = play.Target.Powers.Where(p => p.Type == PowerType.Debuff).ToList();
        if (debuffs.Count <= 0)
            return;
        if (play.Target.CombatState != null)
        {
            var randomDebuff = play.Target.CombatState.RunState.Rng.CombatOrbGeneration.NextItem(
                debuffs
            );
            var temp = randomDebuff?.ClonePreservingMutability();
            if (temp is not PowerModel clone)
            {
                return;
            }
            await PowerCmd.Apply(
                choiceContext,
                clone,
                play.Target,
                clone.Amount,
                Owner.Creature,
                this
            );
        }

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}
