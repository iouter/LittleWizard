using LittleWizard.Api.Extensions;
using LittleWizard.Api.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

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

    public static void FireAndWater(
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        PowerCmd.Apply<FireAndWaterElementReactorPower>(owner, amountA + amountB, applier, null);
    }

    public static void FireAndEarth(
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        PowerCmd.Apply<FireAndEarthElementReactorPower>(owner, amountA * amountB, applier, null);
    }

    public static void WaterAndEarth(
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        PowerCmd.Apply<WaterAndEarthElementReactorPower>(owner, amountA * amountB, applier, null);
    }

    public static bool IsElementCard(CardModel card)
    {
        return card.Tags.Contains(CardTagExtensions.LittleWizardElement)
            || card.Enchantment is IElementEnchantment;
    }
}
