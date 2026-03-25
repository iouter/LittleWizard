using BaseLib.Abstracts;
using BaseLib.Extensions;

namespace LittleWizard.Api.Powers;

public abstract class LittleWizardPower : CustomPowerModel
{
    public override string CustomPackedIconPath => "res://images/" + Character.LittleWizard.InnerName + "/powers/" +
                                                   Id.Entry.RemovePrefix().ToLowerInvariant() + ".png";

    public override string CustomBigIconPath => "res://images/" + Character.LittleWizard.InnerName + "/powers/big/" +
                                                Id.Entry.RemovePrefix().ToLowerInvariant() + ".png";
}