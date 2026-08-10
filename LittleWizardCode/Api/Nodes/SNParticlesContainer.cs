using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;

namespace LittleWizard.LittleWizardCode.Api.Nodes;

public partial class SNParticlesContainer : NParticlesContainer
{
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
        if (_particles != null)
            return;
        _particles = new Array<GpuParticles2D>();
        foreach (var child in GetChildren())
        {
            if (child is GpuParticles2D gp)
                _particles.Add(gp);
        }
    }
}
