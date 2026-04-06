using System.Diagnostics;
using LittleWizard.Api.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Api;

public static class Utils
{
    public static async Task GivePower<T>(
        Creature target,
        DynamicVarSet varSet,
        Creature? applier,
        CardModel cardModel
    )
        where T : PowerModel
    {
        await PowerCmd.Apply<T>(
            target,
            DynamicVarsHelper.GetPowerVar<T>(varSet).BaseValue,
            applier,
            cardModel
        );
    }

    public static async Task GivePower<T>(CardModel cardModel, CardPlay play)
        where T : PowerModel
    {
        switch (cardModel.TargetType)
        {
            case TargetType.Self:
            {
                await GivePower<T>(
                    cardModel.Owner.Creature,
                    cardModel.DynamicVars,
                    cardModel.Owner.Creature,
                    cardModel
                );
                return;
            }
            case TargetType.AllEnemies:
            {
                Debug.Assert(cardModel.CombatState != null);
                foreach (var enemy in cardModel.CombatState.HittableEnemies)
                    await GivePower<T>(
                        enemy,
                        cardModel.DynamicVars,
                        cardModel.Owner.Creature,
                        cardModel
                    );
                return;
            }
            case TargetType.RandomEnemy:
            {
                Debug.Assert(cardModel.CombatState != null);
                var targets = cardModel.CombatState.HittableEnemies;
                var target = cardModel.Owner.RunState.Rng.CombatTargets.NextItem(targets);
                if (target == null)
                    return;
                await GivePower<T>(
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
                Debug.Assert(play.Target != null);
                await GivePower<T>(
                    play.Target,
                    cardModel.DynamicVars,
                    cardModel.Owner.Creature,
                    cardModel
                );
                return;
            }
        }
    }

    public static bool IsPoweredAttack(ValueProp props)
    {
        return props.HasFlag(ValueProp.Move) && !props.HasFlag(ValueProp.Unpowered);
    }
}
