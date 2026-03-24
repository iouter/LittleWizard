using LittleWizard.Api.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Api.Powers;

public static class ElementHelper
{
    public static async Task RandomElement(Creature target,
        decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        var randomElement = Rng.Chaotic.NextInt(0, 3);
        switch (randomElement)
        {
            case 0:
            {
                await PowerCmd.Apply<FireElement>(target, amount, applier, cardSource);
                return;
            }
            case 1:
            {
                await PowerCmd.Apply<WaterElement>(target, amount, applier, cardSource);
                return;
            }
            case 2:
            {
                await PowerCmd.Apply<EarthElement>(target, amount, applier, cardSource);
                return;
            }
        }
    }

    public static void FireAndWater(Creature owner, decimal amountA, decimal amountB, Creature? applier)
    {
        var sum = amountA + amountB;
        PowerCmd.Apply<ElementTemporaryStrengthPower>(owner, sum, applier, null);
        PowerCmd.Apply<VulnerablePower>(owner, sum, applier, null);
        _ = OnElementReactor(owner, applier);
    }

    public static void FireAndEarth(Creature owner, decimal amountA, decimal amountB, Creature? applier)
    {
        CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), owner, amountA * amountB, ValueProp.Unpowered, applier,
            null);
        _ = OnElementReactor(owner, applier);
    }

    public static void WaterAndEarth(Creature owner, decimal amountA, decimal amountB, Creature? applier)
    {
        if (amountA * amountB > Rng.Chaotic.NextInt(0, 100)) CreatureCmd.Stun(owner);
        _ = OnElementReactor(owner, applier);
    }

    private static async Task OnElementReactor(Creature owner, Creature? applier)
    {
        if (owner.CombatState == null) return;
        foreach (var player in owner.CombatState.Players)
        foreach (var power in player.Creature.Powers)
        {
            if (power is not IAfterElementReactor afterElementReactor) continue;
            await afterElementReactor.AfterElementReact(player.Creature, owner);
        }

        foreach (var power in owner.Powers)
        {
            if (power is not IAfterElementReactor afterElementReactor) continue;
            await afterElementReactor.AfterElementReact(owner, owner);
        }
    }
}