using Godot;
using MegaCrit.Sts2.Core.Helpers;

namespace LittleWizard.Api.Audios;

public static class AudioHelper
{
    public static void PlaySound(string path)
    {
        if (NonInteractiveMode.IsActive)
            return;
        var stream = GD.Load<AudioStream>(path);
        if (stream == null)
        {
            GD.PrintErr($"[LittleWizard] Failed to load: {path}");
            return;
        }
        var player = new AudioStreamPlayer();
        player.Stream = stream;
        player.Finished += () => player.QueueFree();
        var root = (Engine.GetMainLoop() as SceneTree)?.Root;
        root?.AddChild(player);
        player.Play();
    }
}
