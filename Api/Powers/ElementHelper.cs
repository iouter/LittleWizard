using LittleWizard.Api.Extensions;
using LittleWizard.Api.Interface;
using LittleWizard.Powers.Elements;
using LittleWizard.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Api.Powers;

public static class ElementHelper
{
    public static async Task RandomElement(
        Creature target,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (applier is { CombatState: not null })
        {
            var randomElement = applier.CombatState.RunState.Rng.CombatOrbGeneration.NextInt(0, 3);
            switch (randomElement)
            {
                case 0:
                    await PowerCmd.Apply<FireElement>(target, amount, applier, cardSource);
                    return;
                case 1:
                    await PowerCmd.Apply<WaterElement>(target, amount, applier, cardSource);
                    return;
                case 2:
                    await PowerCmd.Apply<EarthElement>(target, amount, applier, cardSource);
                    return;
            }
        }
    }

    public static void FireAndWater(
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        decimal totalAmount = amountA + amountB;
        CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            owner,
            totalAmount,
            ValueProp.Unpowered,
            applier,
            null
        );
        PowerCmd.Apply<StrengthPower>(owner, -totalAmount, applier, null);
        PowerCmd.Apply<FireWaterReactor>(owner, totalAmount, applier, null);
    }

    public static void FireAndEarth(
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        decimal totalAmount = amountA + amountB;
        CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            owner,
            totalAmount,
            ValueProp.Unpowered,
            applier,
            null
        );
        PowerCmd.Apply<FireEarthReactor>(owner, totalAmount, applier, null);
    }

    public static void WaterAndEarth(
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        decimal totalAmount = amountA + amountB;

        PowerCmd.Apply<StrengthPower>(owner, -totalAmount, applier, null);
        PowerCmd.Apply<WaterEarthReactor>(owner, totalAmount, applier, null);
    }

    public static bool IsElementCard(CardModel card)
    {
        return card.Tags.Contains(CardTagExtensions.LittleWizardElement)
            || card.Enchantment is IElementEnchantment;
    }
}
