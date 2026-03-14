using LittleWizard.Api.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Api;

public static class Utils
{
    public static async Task GivePower<T>(Creature target, DynamicVarSet varSet, Creature? applier, CardModel cardModel) where T : PowerModel
    {
        await PowerCmd.Apply<T>(target, DynamicVarsHelper.GetPowerVar<T>(varSet).BaseValue, applier, cardModel);
    }
    
    public static async Task GivePower<T>(CardModel cardModel, CardPlay play) where T : PowerModel
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await GivePower<T>(play.Target,
            cardModel.DynamicVars,
            cardModel.Owner.Creature,
            cardModel);
    }
}