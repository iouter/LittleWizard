using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Api.DynamicVars;

public static class DynamicVarsHelper
{
    public static RandomElementVar RandomElement(this DynamicVarSet varSet)
    {
        return (RandomElementVar)varSet[RandomElementVar.DefaultName];
    }

    public static DiscardsVar Discards(this DynamicVarSet varSet)
    {
        return (DiscardsVar)varSet[DiscardsVar.DefaultName];
    }

    public static ThresholdVar Threshold(this DynamicVarSet varSet)
    {
        return (ThresholdVar)varSet[ThresholdVar.DefaultName];
    }
}
