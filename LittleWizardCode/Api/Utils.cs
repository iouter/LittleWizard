using BaseLib.Extensions;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Api;

public static class Utils
{
    public static async Task GivePower<T>(
        PlayerChoiceContext context,
        Creature target,
        DynamicVarSet varSet,
        Creature? applier,
        CardModel? cardModel
    )
        where T : PowerModel
    {
        await PowerCmd.Apply<T>(
            context,
            target,
            DynamicVarsHelper.GetPowerVar<T>(varSet).BaseValue,
            applier,
            cardModel
        );
    }

    public static async Task GivePower<T>(
        PlayerChoiceContext context,
        IReadOnlyList<Creature> targets,
        DynamicVarSet varSet,
        Creature? applier,
        CardModel? cardModel
    )
        where T : PowerModel
    {
        await PowerCmd.Apply<T>(
            context,
            targets,
            DynamicVarsHelper.GetPowerVar<T>(varSet).BaseValue,
            applier,
            cardModel
        );
    }

    public static async Task GivePower<T>(
        CardModel cardModel,
        CardPlay play,
        PlayerChoiceContext context
    )
        where T : PowerModel
    {
        switch (cardModel.TargetType)
        {
            case TargetType.Self:
            {
                await GivePower<T>(
                    context,
                    cardModel.Owner.Creature,
                    cardModel.DynamicVars,
                    cardModel.Owner.Creature,
                    cardModel
                );
                return;
            }
            case TargetType.AllEnemies:
            {
                await GivePower<T>(
                    context,
                    cardModel.CombatState!.HittableEnemies,
                    cardModel.DynamicVars,
                    cardModel.Owner.Creature,
                    cardModel
                );
                return;
            }
            case TargetType.RandomEnemy:
            {
                var targets = cardModel.CombatState!.HittableEnemies;
                var target = cardModel.Owner.RunState.Rng.CombatTargets.NextItem(targets);
                if (target == null)
                    return;
                await GivePower<T>(
                    context,
                    target,
                    cardModel.DynamicVars,
                    cardModel.Owner.Creature,
                    cardModel
                );
                return;
            }
            case TargetType.None:
            case TargetType.AnyEnemy:
            case TargetType.AnyPlayer:
            case TargetType.AnyAlly:
            case TargetType.TargetedNoCreature:
            case TargetType.Osty:
            case TargetType.AllAllies:
            default:
            {
                await GivePower<T>(
                    context,
                    play.Target!,
                    cardModel.DynamicVars,
                    cardModel.Owner.Creature,
                    cardModel
                );
                return;
            }
        }
    }

    public static async Task GivePower<T>(
        RelicModel relicModel,
        Creature target,
        PlayerChoiceContext context
    )
        where T : PowerModel
    {
        await GivePower<T>(
            context,
            target,
            relicModel.DynamicVars,
            relicModel.Owner.Creature,
            null
        );
    }

    public static async Task GivePower<T>(
        RelicModel relicModel,
        IReadOnlyList<Creature> targets,
        PlayerChoiceContext context
    )
        where T : PowerModel
    {
        await GivePower<T>(
            context,
            targets,
            relicModel.DynamicVars,
            relicModel.Owner.Creature,
            null
        );
    }

    public static bool IsPoweredAttack(ValueProp props)
    {
        return props.HasFlag(ValueProp.Move) && !props.HasFlag(ValueProp.Unpowered);
    }

    public static string GetModelSnakeCase(AbstractModel model)
    {
        return model.Id.Entry.RemovePrefix().ToLowerInvariant();
    }

    public static async Task<IEnumerable<CardModel>> SelectCards(
        Player player,
        LocString selectionPrompt,
        PlayerChoiceContext context,
        PileType pileType,
        int count = 1
    )
    {
        var prefs = new CardSelectorPrefs(selectionPrompt, count);
        var pile = pileType.GetPile(player);
        var cardModelList = pile.Cards;
        if (pile.Type == PileType.Draw)
            cardModelList = cardModelList
                .OrderBy(c => c.Rarity)
                .ThenBy((Func<CardModel, ModelId>)(c => c.Id))
                .ToList();
        return await CardSelectCmd.FromSimpleGrid(context, cardModelList, player, prefs);
    }

    public static async Task<IEnumerable<CardModel>> SelectCards(
        Player player,
        LocString selectionPrompt,
        PlayerChoiceContext context,
        PileType pileType,
        int minCount,
        int maxCount
    )
    {
        var prefs = new CardSelectorPrefs(selectionPrompt, minCount, maxCount);
        var pile = pileType.GetPile(player);
        var cardModelList = pile.Cards;
        if (pile.Type == PileType.Draw)
            cardModelList = cardModelList
                .OrderBy(c => c.Rarity)
                .ThenBy((Func<CardModel, ModelId>)(c => c.Id))
                .ToList();
        return await CardSelectCmd.FromSimpleGrid(context, cardModelList, player, prefs);
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
}
