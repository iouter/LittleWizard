using System.Reflection;
using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;

namespace LittleWizard.LittleWizardCode.Api.Nodes;

public partial class SNParticlesContainer : NParticlesContainer
{
    private static readonly FieldInfo? ParticlesField = typeof(NParticlesContainer).GetField(
        "_particles",
        BindingFlags.NonPublic | BindingFlags.Instance
    );

    public override void _Ready()
    {
        base._Ready();
        var particles2Ds = GetParticles(this);
        if (particles2Ds != null && particles2Ds.Count != 0)
            return;
        particles2Ds = [];
        foreach (var child in GetChildren())
            if (child is GpuParticles2D particles)
                particles2Ds.Add(particles);
    }

    public static Array<GpuParticles2D>? GetParticles(NParticlesContainer instance) =>
        ParticlesField?.GetValue(instance) as Array<GpuParticles2D>;
}
