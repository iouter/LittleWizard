using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.HoverTips;

namespace LittleWizard.LittleWizardCode.Api;

public static class HoverTipsValue
{
    public static readonly IHoverTip Fire = HoverTipFactory.FromPower<FireElement>();
    public static readonly IHoverTip Water = HoverTipFactory.FromPower<WaterElement>();
    public static readonly IHoverTip Earth = HoverTipFactory.FromPower<EarthElement>();
}
