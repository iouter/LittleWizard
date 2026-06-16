using BaseLib.Extensions;
using LittleWizard.LittleWizardCode.Api.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Api;

public static class Utils
{
    public static async Task Apply<T>(
        PlayerChoiceContext context,
        Creature target,
        DynamicVarSet varSet,
        Creature? applier,
        CardModel? cardModel
    )
        where T : PowerModel
    {
        await PowerCmd.Apply<T>(context, target, varSet.Power<T>().BaseValue, applier, cardModel);
    }

    public static async Task Apply<T>(
        PlayerChoiceContext context,
        IReadOnlyList<Creature> targets,
        DynamicVarSet varSet,
        Creature? applier,
        CardModel? cardModel
    )
        where T : PowerModel
    {
        await PowerCmd.Apply<T>(context, targets, varSet.Power<T>().BaseValue, applier, cardModel);
    }

    public static async Task Apply<T>(
        RelicModel relicModel,
        Creature target,
        PlayerChoiceContext context
    )
        where T : PowerModel
    {
        await Apply<T>(context, target, relicModel.DynamicVars, relicModel.Owner.Creature, null);
    }

    public static async Task Apply<T>(
        RelicModel relicModel,
        IReadOnlyList<Creature> targets,
        PlayerChoiceContext context
    )
        where T : PowerModel
    {
        await Apply<T>(context, targets, relicModel.DynamicVars, relicModel.Owner.Creature, null);
    }

    public static async Task<CardModel?> SelectSingleCard(
        Player player,
        LocString selectionPrompt,
        PlayerChoiceContext context,
        PileType pileType
    )
    {
        var prefs = new CardSelectorPrefs(selectionPrompt, 1);
        var pile = pileType.GetPile(player);
        var cardModelList = pile.Cards;
        if (pile.Type == PileType.Draw)
            cardModelList = cardModelList
                .OrderBy(c => c.Rarity)
                .ThenBy((Func<CardModel, ModelId>)(c => c.Id))
                .ToList();
        return (
            await CardSelectCmd.FromSimpleGrid(context, cardModelList, player, prefs)
        ).FirstOrDefault();
    }

    public static decimal GetThresholdMultiplier<T>(CardModel card, Creature? target)
        where T : PowerModel
    {
        return Math.Floor((decimal)(target?.GetPowerAmount<T>() ?? 0))
            / card.DynamicVars.Threshold().BaseValue;
    }
}
