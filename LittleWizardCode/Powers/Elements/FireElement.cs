using BaseLib.Hooks;
using Godot;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Elements;

public class FireElement : BaseElement
{
    private static readonly Material? FireBarMaterial = GD.Load<Material>(
        "res://materials/vfx/bar/vfx_fire_bar.tres"
    );

    public override async Task AfterSideTurnStart(
        CombatSide side,
        IReadOnlyList<Creature> creatures,
        ICombatState combatState
    )
    {
        if (side != Owner.Side)
            return;
        /*VfxCmd.PlayOnCreatureCenter(Owner, "vfx/vfx_fire_element"); */
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
            /*VfxCmd.PlayOnCreatureCenter(Owner, "vfx/vfx_fire_element"); */
        }

        if (!Owner.IsAlive)
            await Cmd.CustomScaledWait(0.1f, 0.25f);
    }

    public override IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(
        HealthBarForecastContext context
    )
    {
        var damage = Amount == 1 ? 1 : Amount / 2;
        if (damage <= 0)
            yield break;
        yield return new HealthBarForecastSegment(
            amount: damage,
            color: new Color("ECAB1C"),
            direction: HealthBarForecastDirection.FromRight,
            order: 10,
            overlayMaterial: FireBarMaterial
        );
    }
}
