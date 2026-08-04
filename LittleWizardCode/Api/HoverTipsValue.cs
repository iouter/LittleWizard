using LittleWizard.LittleWizardCode.Powers.Elements;
using LittleWizard.LittleWizardCode.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.HoverTips;

namespace LittleWizard.LittleWizardCode.Api;

public static class HoverTipsValue
{
    public static IHoverTip Fire => HoverTipFactory.FromPower<FireElement>();
    public static IHoverTip Water => HoverTipFactory.FromPower<WaterElement>();
    public static IHoverTip Earth => HoverTipFactory.FromPower<EarthElement>();
    public static IHoverTip FireWater => HoverTipFactory.FromPower<FireWaterReactor>();
    public static IHoverTip FireEarth => HoverTipFactory.FromPower<FireEarthReactor>();
    public static IHoverTip WaterEarth => HoverTipFactory.FromPower<WaterEarthReactor>();
}
