using Godot;
using HarmonyLib;
using LittleWizard.Extensions;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace LittleWizard;

[ModInitializer(nameof(Initialize))]

public partial class MainFile : Node
{
    private const string ModId = "LittleWizard"; //At the moment, this is used only for the Logger and harmony names.
    public static string ModIdPublic => ModId;

    public static Logger Logger { get; } = new(ModId, LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);

        harmony.PatchAll();
    }
}