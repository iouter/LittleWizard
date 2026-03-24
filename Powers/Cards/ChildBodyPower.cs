using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace LittleWizard.Powers.Cards;

public sealed class ChildBodyPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        return player != Owner.Player ? amount : amount - Amount;
    }

    public override bool ShouldFlush(Player player)
    {
        return player != Owner.Player;
    }
}