using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace LittleWizard.Powers.Cards;

public class BrewPotionsPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterCombatEnd(CombatRoom room)
    {
        if (Owner.Player == null) return Task.CompletedTask;
        for (var index = 0; index < Amount; ++index)
            room.AddExtraReward(Owner.Player, new PotionReward(Owner.Player));
        return Task.CompletedTask;
    }
}