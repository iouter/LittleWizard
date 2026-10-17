using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class OceanusPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player || cardPlay.Card.Type != CardType.Attack)
            return;

        if (cardPlay.Card.TargetType == TargetType.AllEnemies)
        {
            var enemies = Owner.CombatState?.HittableEnemies;
            await PowerCmd.Apply<WaterElement>(context, enemies!, Amount, Owner, cardPlay.Card);
            return;
        }

        await PowerCmd.Apply<WaterElement>(context, cardPlay.Target!, Amount, Owner, cardPlay.Card);
    }
}
