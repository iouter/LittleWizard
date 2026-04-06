using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Powers.Cards;

public class ManagerMasterPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool causedByEthereal
    )
    {
        if (Owner.Player == null || card.Owner != Owner.Player)
            return;

        if (causedByEthereal)
            await PowerCmd.Remove(this);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (Owner.Player == null)
        {
            await PowerCmd.Remove(this);
            return;
        }

        await PowerCmd.Apply<EnergyNextTurnPower>(
            Owner.Player.Creature,
            Amount,
            Owner.Player.Creature,
            null
        );
        await PowerCmd.Remove(this);
    }
}
