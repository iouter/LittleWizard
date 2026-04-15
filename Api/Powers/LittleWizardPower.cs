using BaseLib.Abstracts;
using LittleWizard.Api.Extensions;

namespace LittleWizard.Api.Powers;

public abstract class LittleWizardPower : CustomPowerModel
{
    public override string CustomPackedIconPath =>
        $"{Utils.GetModelSnakeCase(this)}.png".PowerImagePath();

    public override string CustomBigIconPath =>
        $"{Utils.GetModelSnakeCase(this)}.png".BigPowerImagePath();
}
