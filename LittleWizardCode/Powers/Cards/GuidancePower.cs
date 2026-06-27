using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class GuidancePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        var playerCreature = cardPlay.Card.Owner.Creature;
        if (playerCreature == null)
            return;

        var combatState = playerCreature.CombatState;
        if (combatState == null)
            return;

        var enemies = combatState.HittableEnemies;
        if (enemies.Count == 0)
            return;

        foreach (var enemy in enemies)
        {
            await PowerCmd.Apply<GuidanceMarkPower>(context, enemy, Amount, Owner, cardPlay.Card);
        }
    }
}
