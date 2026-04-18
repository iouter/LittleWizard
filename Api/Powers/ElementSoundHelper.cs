using Godot;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Api.Powers
{
    public static class ElementSoundHelper
    {
        private static void PlaySound(string path)
        {
            if (NonInteractiveMode.IsActive)
                return;
            var stream = GD.Load<AudioStream>(path);
            if (stream == null)
            {
                GD.PrintErr($"[ElementSoundHelper] Failed to load: {path}");
                return;
            }
            var player = new AudioStreamPlayer();
            player.Stream = stream;
            player.Finished += () => player.QueueFree();
            var root = (Engine.GetMainLoop() as SceneTree)?.Root;
            root?.AddChild(player);
            player.Play();
        }

        // ---------- Play when generating element (on application) ----------
        /*public override Task AfterApplied(Creature? applier, CardModel? cardSource)
            {
                ElementSoundHelper.PlayAppliedSound(this, applier);
                return base.AfterApplied(applier, cardSource);
            }*/
        public static void PlayAppliedSound(
            PowerModel element,
            Creature? applier,
            string? customSoundPath = null
        )
        {
            if (applier == null || applier.Side != CombatSide.Player)
                return;
            string? path = customSoundPath ?? GetDefaultAppliedSound(element);
            if (!string.IsNullOrEmpty(path))
                PlaySound(path);
        }

        private static string? GetDefaultAppliedSound(PowerModel element) =>
            element switch
            {
                FireElement => "res://LittleWizard/audio/Generated_Fire.wav",
                WaterElement => "res://LittleWizard/audio/Generated_Water.wav",
                EarthElement => "res://LittleWizard/audio/Generated_Earth.wav",
                _ => null,
            };

        // ---------- Play when the element triggers an effect  ----------
        /*public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
        {
            ElementSoundHelper.PlayTriggerSound(this);
        }*/
        public static void PlayTriggerSound(PowerModel element, string? customSoundPath = null)
        {
            string? path = customSoundPath ?? GetDefaultTriggerSound(element);
            if (!string.IsNullOrEmpty(path))
                PlaySound(path);
        }

        private static string? GetDefaultTriggerSound(PowerModel element) =>
            element switch
            {
                FireElement => "res://LittleWizard/audio/Generated_Fire.wav",
                WaterElement => "",
                EarthElement => "",
                _ => null,
            };

        // ---------- Play when elemental reaction occurs ----------
        /*public override async Task AfterDamageReceived(...)
        {
            ElementSoundHelper.PlayReactionSound("WaterEarth");
        }*/
        public static void PlayReactionSound(string reactionName, string? customSoundPath = null)
        {
            string? path = customSoundPath ?? GetDefaultReactionSound(reactionName);
            if (!string.IsNullOrEmpty(path))
                PlaySound(path);
        }

        private static string? GetDefaultReactionSound(string reactionName) =>
            reactionName switch
            {
                "WaterEarth" => "",
                "FireWater" => "",
                "FireEarth" => "",
                _ => null,
            };
    }
}
