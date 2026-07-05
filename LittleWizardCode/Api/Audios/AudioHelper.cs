using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace LittleWizard.LittleWizardCode.Api.Audios;

public static class AudioHelper
{
    private const string AudioBasePath = "res://LittleWizard/audios/";

    public static async void PlaySound(string path)
    {
        if (NonInteractiveMode.IsActive)
            return;

        AudioStream? stream = null;
        try
        {
            stream = GD.Load<AudioStream>(path);
            if (stream == null)
            {
                GD.PrintErr($"[AudioHelper] Failed to load audio: {path}");
                return;
            }
        }
        catch (Exception e)
        {
            GD.PrintErr($"[AudioHelper] Exception loading audio {path}: {e.Message}");
            return;
        }

        var player = new AudioStreamPlayer { Stream = stream, Bus = "SFX" };
        player.Finished += () => player.QueueFree();

        var combatRoom = NCombatRoom.Instance;
        if (combatRoom != null)
        {
            combatRoom.AddChild(player);
        }
        else
        {
            (Engine.GetMainLoop() as SceneTree)?.Root?.AddChild(player);
        }
        player.Play();
    }

    public static void PlayOnAttack(CardModel card)
    {
        PlaySound(AudioBasePath + "attack_common.wav");
    }

    public static void PlayOnSkill(CardModel card)
    {
        PlaySound(AudioBasePath + "cast_common.wav");
    }
}
