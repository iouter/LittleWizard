using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace LittleWizard.Powers.Cards;

public class ManaSurgePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        if (player.Creature != Owner) return amount;
        return amount + Amount;
    }
}