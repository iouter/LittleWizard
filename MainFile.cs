using System.Reflection;
using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace LittleWizard;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "LittleWizard"; //At the moment, this is used only for the Logger and harmony names.

    public static Logger Logger { get; } = new(ModId, LogType.Generic);

    private static void InitializeTools()
    {
        var assembly = Assembly.GetExecutingAssembly();
        ScriptManagerBridge.LookupScriptsInAssembly(assembly);
    }

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        InitializeTools();
        harmony.PatchAll();
    }
}
