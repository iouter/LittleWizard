using HarmonyLib;
using LittleWizard.LittleWizardCode.Character;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;

namespace LittleWizard.LittleWizardCode.Api.Patches;

[HarmonyPatch(typeof(ColorfulPhilosophers))]
public static class ColorfulPhilosophersPatch
{
    [HarmonyPatch("CardPoolColorOrder", MethodType.Getter)]
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    public static void Postfix(ref IEnumerable<CardPoolModel> __result)
    {
        __result = __result.Append(ModelDb.CardPool<LittleWizardCardPool>());
    }
}
