using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Api.DynamicVars;

public class DiscardsVar(decimal baseValue) : DynamicVar(DefaultName, baseValue)
{
    public const string DefaultName = "Discards";
}
