using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Api.Powers;

public abstract class BaseMoreElementReceivedPower : CustomPowerModel
{
    public override PowerType Type => Owner.IsPlayer ? PowerType.Buff : PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private string GetIconBaseName() => Id.Entry.RemovePrefix().ToLowerInvariant();

    public override string CustomPackedIconPath =>
        $"res://{MainFile.ModId}/images/powers/{GetIconBaseName()}.png";

    public override string CustomBigIconPath =>
        $"res://{MainFile.ModId}/images/powers/{GetIconBaseName()}.png";

    public override bool TryModifyPowerAmountReceived(
        PowerModel canonicalPower,
        Creature target,
        decimal amount,
        Creature? applier,
        out decimal modifiedAmount
    )
    {
        if (
            amount == 0
            || target != Owner
            || canonicalPower is not BaseElement
            || !canonicalPower.IsVisible
        )
        {
            modifiedAmount = amount;
            return false;
        }

        modifiedAmount = amount + Amount;
        return true;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Side)
            return;
        await PowerCmd.Remove(this);
    }
}
