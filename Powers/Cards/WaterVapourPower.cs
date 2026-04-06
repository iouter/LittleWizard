using LittleWizard.Api.Powers;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace LittleWizard.Powers.Cards;

public class WaterVapourPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != CombatSide.Enemy)
            return;
        await PowerCmd.Apply<WaterElement>(Owner, Amount, Owner, null);
    }
}
