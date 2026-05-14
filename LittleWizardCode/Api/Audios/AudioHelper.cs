using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Api.Audios;

public static class AudioHelper
{
    public static void PlaySound(string path)
    {
        if (NonInteractiveMode.IsActive)
            return;
        var stream = GD.Load<AudioStream>(path);
        if (stream == null)
        {
            return;
        }
        var player = new AudioStreamPlayer();
        player.Stream = stream;
        player.Finished += () => player.QueueFree();
        (Engine.GetMainLoop() as SceneTree)?.Root?.AddChild(player);
        player.Play();
    }

    public static void PlayOnAttack(CardModel card)
    {
        PlaySound("");
    }
}
