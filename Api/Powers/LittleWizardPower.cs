using System.Text;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using LittleWizard.Api.Extensions;

namespace LittleWizard.Api.Powers;

public abstract class LittleWizardPower : CustomPowerModel
{
    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var lower = input.ToLowerInvariant();
        var sb = new StringBuilder();
        bool lastWasSeparator = true;

        foreach (char c in lower)
        {
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
        }

        return sb.ToString().Trim('_');
    }

    private string GetIconBaseName()
    {
        string rawName = Id.Entry.RemovePrefix();
        return ToSnakeCase(rawName);
    }

    public override string CustomPackedIconPath => $"{GetIconBaseName()}.png".PowerImagePath();

    public override string CustomBigIconPath => $"{GetIconBaseName()}.png".BigPowerImagePath();
}
