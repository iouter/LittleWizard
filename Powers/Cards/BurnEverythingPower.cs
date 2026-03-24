using LittleWizard.Api.Powers;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class BurnEverythingPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Side) return;
        await PowerCmd.Apply<FireElement>(Owner, Amount, Owner, null);
    }

    public override async Task BeforeDeath(Creature creature)
    {
        if (creature != Owner) return;

        if (Owner.CombatState == null) return;
        foreach (var enemy in Owner.CombatState.HittableEnemies)
        {
            if (enemy.IsDead) continue;
            var fire = enemy.GetPowerAmount<FireElement>();
            if (fire > 0) await PowerCmd.Apply<FireElement>(enemy, fire, Owner, null);
        }
    }
}