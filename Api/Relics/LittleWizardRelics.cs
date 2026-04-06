using System.Text;
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

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var lower = input.ToLowerInvariant();
        var sb = new StringBuilder();
        var lastWasSeparator = true;

        foreach (var c in lower)
            if (char.IsLetterOrDigit(c))
            {
                sb.Append(c);
                lastWasSeparator = false;
            }
            else
            {
                if (!lastWasSeparator)
                {
                    sb.Append('_');
                    lastWasSeparator = true;
                }
            }

        return sb.ToString().Trim('_');
    }

    private string GetBaseFileName()
    {
        var rawName = Id.Entry.RemovePrefix();
        return ToSnakeCase(rawName);
    }
}
