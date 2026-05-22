using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Elements;

public class FireElement : BaseElement
{
    public override async Task AfterSideTurnStart(
        CombatSide side,
        IReadOnlyList<Creature> creatures,
        ICombatState combatState
    )
    {
        if (side != Owner.Side)
            return;
        VfxCmd.PlayOnCreatureCenter(Owner, "vfx/vfx_fire_element");
        PlaySound();

        int damage;
        if (Amount == 1)
            damage = 1;
        else
            damage = Amount / 2;

        if (damage > 0)
        {
            await CreatureCmd.Damage(
                new ThrowingPlayerChoiceContext(),
                Owner,
                damage,
                ValueProp.Unblockable | ValueProp.Unpowered,
                null,
                null
            );
            VfxCmd.PlayOnCreatureCenter(Owner, "vfx/vfx_fire_element");
        }

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
                ElementHelper.FireAndWater(
                    new ThrowingPlayerChoiceContext(),
                    Owner,
                    Amount,
                    amount,
                    applier
                );
                modifiedAmount = 0;
                return true;
            case EarthElement earth:
                ElementHelper.FireAndEarth(
                    new ThrowingPlayerChoiceContext(),
                    Owner,
                    Amount,
                    amount,
                    applier
                );
                modifiedAmount = 0;
                return true;
            default:
                modifiedAmount = amount;
                return false;
        }
    }
}
