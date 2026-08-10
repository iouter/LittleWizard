using System.Reflection;
using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;

namespace LittleWizard.LittleWizardCode.Api.Nodes;

public partial class SNParticlesContainer : NParticlesContainer
{
    private bool _initialized = false;

    public override void _EnterTree()
    {
        base._EnterTree();
        EnsureInitialized();
    }

    public override void _Ready()
    {
        base._Ready();
        EnsureInitialized();
    }

    private void EnsureInitialized()
    {
        if (_initialized)
            return;

        var field = typeof(NParticlesContainer).GetField(
            "_particles",
            BindingFlags.NonPublic | BindingFlags.Instance
        );
        if (field == null)
        {
            GD.PushError("SNParticlesContainer: Failed to find _particles field via reflection.");
            return;
        }

        var particles = field.GetValue(this) as Array<GpuParticles2D>;
        if (particles != null && particles.Count > 0)
        {
            _initialized = true;
            return;
        }

        particles = new Array<GpuParticles2D>();
        foreach (var child in GetChildren())
        {
            if (child is GpuParticles2D gp)
                particles.Add(gp);
        }

        field.SetValue(this, particles);
        _initialized = true;
    }
}
