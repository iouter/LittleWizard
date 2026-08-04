using Godot;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Nodes.HoverTips;

namespace LittleWizard.LittleWizardCode.Api.Nodes;

public partial class ElementWheel : Control
{
    public override void _Ready()
    {
        BindHighlight("FireHighlight", HoverTipsValue.Fire);
        BindHighlight("WaterHighlight", HoverTipsValue.Water);
        BindHighlight("EarthHighlight", HoverTipsValue.Earth);
        BindHighlight("FireWaterHighlight", HoverTipsValue.FireWater);
        BindHighlight("FireEarthHighlight", HoverTipsValue.FireEarth);
        BindHighlight("WaterEarthHighlight", HoverTipsValue.WaterEarth);
    }

    private void BindHighlight(NodePath path, IHoverTip hoverTip)
    {
        var highlight = GetNode<TextureRect>(path);
        highlight.MouseEntered += () =>
        {
            SetHighlight(highlight, true);
            NHoverTipSet.CreateAndShow(highlight, hoverTip, HoverTipAlignment.Right);
        };
        highlight.MouseExited += () =>
        {
            SetHighlight(highlight, false);
            NHoverTipSet.Remove(highlight);
        };
    }

    private static void SetHighlight(CanvasItem highlight, bool isVisible)
    {
        var color = highlight.SelfModulate;
        color.A = isVisible ? 1f : 0f;
        highlight.SelfModulate = color;
    }
}
