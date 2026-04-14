using BaseLib.Abstracts;
using BaseLib.Utils;
using LittleWizard.Api.Extensions;
using LittleWizard.Character;

namespace LittleWizard.Api.Relics;

[Pool(typeof(LittleWizardRelicPool))]
public abstract class LittleWizardRelics : CustomRelicModel
{
    protected override string BigIconPath =>
        $"{Utils.GetModelSnakeCase(this)}.png".BigRelicImagePath();
    public override string PackedIconPath =>
        $"{Utils.GetModelSnakeCase(this)}.tres".TresRelicImagePath();

    protected override string PackedIconOutlinePath =>
        $"{Utils.GetModelSnakeCase(this)}_outline.tres".TresRelicImagePath();
}
