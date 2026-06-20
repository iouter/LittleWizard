using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.HoverTips;

namespace LittleWizard.LittleWizardCode.Api;

public static class HoverTipsValue
{
    public static IHoverTip Fire => HoverTipFactory.FromPower<FireElement>();
    public static IHoverTip Water => HoverTipFactory.FromPower<WaterElement>();
    public static IHoverTip Earth => HoverTipFactory.FromPower<EarthElement>();
}
