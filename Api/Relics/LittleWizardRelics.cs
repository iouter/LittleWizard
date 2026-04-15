using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.Api.Extensions;
using LittleWizard.Character;

namespace LittleWizard.Api.Relics;

[Pool(typeof(LittleWizardRelicPool))]
public abstract class LittleWizardRelics : CustomRelicModel
{
    protected override string BigIconPath => $"{GetBaseFileName()}.png".BigRelicImagePath();
    public override string PackedIconPath => $"{GetBaseFileName()}.tres".TresRelicImagePath();

    protected override string PackedIconOutlinePath =>
        $"{GetBaseFileName()}_outline.tres".TresRelicImagePath();

    private string GetBaseFileName() => Id.Entry.RemovePrefix().ToLowerInvariant();
}