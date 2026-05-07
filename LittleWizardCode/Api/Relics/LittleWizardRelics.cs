using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Character;

namespace LittleWizard.LittleWizardCode.Api.Relics;

[Pool(typeof(LittleWizardRelicPool))]
public abstract class LittleWizardRelics : CustomRelicModel
{
    protected override string BigIconPath => $"{GetBaseFileName()}.png".BigRelicImagePath();
    public override string PackedIconPath => $"{GetBaseFileName()}.tres".TresRelicImagePath();

    protected override string PackedIconOutlinePath =>
        $"{GetBaseFileName()}_outline.tres".TresRelicImagePath();

    private string GetBaseFileName() => Id.Entry.RemovePrefix().ToLowerInvariant();
}
