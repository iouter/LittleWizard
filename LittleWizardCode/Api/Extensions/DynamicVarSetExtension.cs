using LittleWizard.LittleWizardCode.Api.DynamicVars;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Api.Extensions;

public static class DynamicVarSetExtension
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
