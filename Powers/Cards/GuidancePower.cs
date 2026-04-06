using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class GuidancePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player
    )
    {
        if (player.Creature != Owner)
            return;
        if (player.Creature.CombatState == null)
            return;
        var target = player.RunState.Rng.CombatTargets.NextItem(
            player.Creature.CombatState.HittableEnemies
        );
        if (target == null)
            return;
        await PowerCmd.Apply<GuidanceMarkPower>(target, Amount, Owner, null);
    }
}
