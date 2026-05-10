using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
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
            GD.PrintErr($"[LittleWizard] Failed to load: {path}");
            return;
        }
        var player = new AudioStreamPlayer();
        player.Stream = stream;
        player.Finished += () => player.QueueFree();
        (Engine.GetMainLoop() as SceneTree)?.Root?.AddChild(player);
        player.Play();
    }

    public static void PlaySoundOnAttack(CardModel card)
    {
        if (card?.Type != CardType.Attack)
            return;
        PlaySound("res://LittleWizard/audios/powers/attack_common.wav");
    }
}
