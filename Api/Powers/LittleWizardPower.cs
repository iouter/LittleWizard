using BaseLib.Abstracts;
using BaseLib.Extensions;
using LittleWizard.Api.Extensions;

namespace LittleWizard.Api.Powers;

public abstract class LittleWizardPower : CustomPowerModel
{
    private string GetIconBaseName()
    {
        string rawName = Id.Entry.RemovePrefix();
        return rawName.ToLowerInvariant();
    }

    public override string CustomPackedIconPath
    {
        get
        {
            string fileName = $"{GetIconBaseName()}.png";
            return $"res://{MainFile.ModId}/images/powers/{fileName}";
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            string fileName = $"{GetIconBaseName()}.png";
            return $"res://{MainFile.ModId}/images/powers/{fileName}";
        }
    }
}
