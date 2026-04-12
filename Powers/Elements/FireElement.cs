//using System.Linq;//
using Godot;
//using LittleWizard.Api.Nodes;//
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
//using MegaCrit.Sts2.Core.Nodes.Vfx;//
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Elements;

public class FireElement : BaseElement
{
    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != Owner.Side)
            return;

        /*var enemies = combatState.HittableEnemies;
        if (!enemies.Any())
            return;

        foreach (var target in enemies)
        {
            GD.Print($"Creating fire vfx for target {target.Name}");
            var vfx = SNFireBurningVfx.Create(target, 1.0f, true);
            if (vfx == null)
                GD.PrintErr($"Failed to create fire vfx for {target.Name}");
            else
                GD.Print($"Vfx created: {vfx.Name}");*/

        await CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            Owner,
            Amount,
            ValueProp.Unblockable | ValueProp.Unpowered,
            null,
            null
        );
        //};//
        if (!Owner.IsAlive)
            await Cmd.CustomScaledWait(0.1f, 0.25f);
    }

    public override bool TryModifyPowerAmountReceived(
        PowerModel canonicalPower,
        Creature target,
        decimal amount,
        Creature? applier,
        out decimal modifiedAmount
    )
    {
        if (target != Owner || amount == 0)
        {
            modifiedAmount = amount;
            return false;
        }

        switch (canonicalPower)
        {
            case WaterElement water:
            {
                ElementHelper.FireAndWater(Owner, Amount, amount, applier);
                modifiedAmount = 0;
                return true;
            }
            case EarthElement earth:
            {
                ElementHelper.FireAndEarth(Owner, Amount, amount, applier);
                modifiedAmount = 0;
                return true;
            }
            default:
            {
                modifiedAmount = amount;
                return false;
            }
        }
    }
}
