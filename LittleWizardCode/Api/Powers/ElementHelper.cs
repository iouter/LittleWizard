using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Api.Interface;
using LittleWizard.LittleWizardCode.Powers.Elements;
using LittleWizard.LittleWizardCode.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Api.Powers;

public static class ElementHelper
{
    public static async Task RandomElement(
        PlayerChoiceContext ctx,
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
                    await PowerCmd.Apply<FireElement>(ctx, target, amount, applier, cardSource);
                    return;
                case 1:
                    await PowerCmd.Apply<WaterElement>(ctx, target, amount, applier, cardSource);
                    return;
                case 2:
                    await PowerCmd.Apply<EarthElement>(ctx, target, amount, applier, cardSource);
                    return;
            }
        }
    }

    public static void FireAndWater(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        PowerCmd.Apply<FireWaterReactor>(ctx, owner, amountA + amountB, applier, null);
    }

    public static void FireAndEarth(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        PowerCmd.Apply<FireEarthReactor>(ctx, owner, amountA + amountB, applier, null);
    }

    public static void WaterAndEarth(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amountA,
        decimal amountB,
        Creature? applier
    )
    {
        PowerCmd.Apply<WaterEarthReactor>(ctx, owner, amountA + amountB, applier, null);
    }

    public static bool IsElementCard(CardModel card)
    {
        return card.Tags.Contains(CardTagExtensions.LittleWizardElement)
            || card.Enchantment is IElementEnchantment;
    }

    public static async Task<bool> RemoveElementAtMost(
        CardModel card,
        PlayerChoiceContext ctx,
        CardPlay cardPlay,
        decimal maxAmount
    )
    {
        var target = cardPlay.Target;
        if (target == null)
            return false;

        var fire = target.GetPowerAmount<FireElement>();
        if (fire > 0)
        {
            int toRemove = (int)Math.Min(maxAmount, fire);
            await PowerCmd.Apply<FireElement>(ctx, target, -toRemove, card.Owner.Creature, card);
            return true;
        }
        var water = target.GetPowerAmount<WaterElement>();
        if (water > 0)
        {
            int toRemove = (int)Math.Min(maxAmount, water);
            await PowerCmd.Apply<WaterElement>(ctx, target, -toRemove, card.Owner.Creature, card);
            return true;
        }
        var earth = target.GetPowerAmount<EarthElement>();
        if (earth > 0)
        {
            int toRemove = (int)Math.Min(maxAmount, earth);
            await PowerCmd.Apply<EarthElement>(ctx, target, -toRemove, card.Owner.Creature, card);
            return true;
        }

        return false;
    }
}
