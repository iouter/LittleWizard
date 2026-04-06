using BaseLib.Utils;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Common;

public class ErosionRay()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        ArgumentNullException.ThrowIfNull(play.Target);
        var debuffs = play.Target.Powers.Where(p => p.Type == PowerType.Debuff).ToList();
        if (debuffs.Count <= 0)
            return;
        var randomDebuff = Rng.Chaotic.NextItem(debuffs);
        if (randomDebuff == null)
            return;
        await PowerCmd.Apply(randomDebuff, play.Target, randomDebuff.Amount, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}
