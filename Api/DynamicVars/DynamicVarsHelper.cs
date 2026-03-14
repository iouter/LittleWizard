using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Api.DynamicVars;

public static class DynamicVarsHelper
{
    public static RandomElementVar GetRandomElementVar(DynamicVarSet varSet)
    {
        return (RandomElementVar) varSet[RandomElementVar.DefaultName];
    }

    public static DiscardsVar GetDiscardsVar(DynamicVarSet varSet)
    {
        return (DiscardsVar) varSet[DiscardsVar.DefaultName];
    }

    public static ThresholdVar GetThresholdVar(DynamicVarSet varSet)
    {
        return (ThresholdVar) varSet[ThresholdVar.DefaultName];
    }

    public static PowerVar<T> GetPowerVar<T>(DynamicVarSet varSet) where T : PowerModel
    {
        return (PowerVar<T>) varSet[typeof(T).Name];
    }
}