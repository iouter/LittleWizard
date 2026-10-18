using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class OceanusFormPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player || cardPlay.Card.Type != CardType.Attack)
            return;

        var enemies = Owner.CombatState?.HittableEnemies;
        if (enemies == null || enemies.Count == 0)
            return;

        var target = Owner.CombatState.RunState.Rng.CombatTargets.NextItem(enemies);
        if (target == null)
            return;

        await PowerCmd.Apply<WaterElement>(context, target, Amount, Owner, cardPlay.Card);
    }
}
