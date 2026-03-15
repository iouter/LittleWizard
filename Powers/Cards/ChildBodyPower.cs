using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class ChildBodyPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (Owner != player.Creature) return;
        await PlayerCmd.LoseEnergy(1, player);
    }

    public override bool ShouldDiscardHandAtEndOfTurn(Player player)
    {
        return false;
    }
}
