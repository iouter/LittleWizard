using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class DeepThoughtPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool ShouldPlayerResetEnergy(Player player)
    {
        return player.Creature.CombatState != null &&
               (player.Creature.CombatState.RoundNumber == 1 || player != Owner.Player);
    }

    public override bool ShouldFlush(Player player)
    {
        return player != Owner.Player;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Enemy)
            return;
        await PowerCmd.Decrement(this);
    }
}