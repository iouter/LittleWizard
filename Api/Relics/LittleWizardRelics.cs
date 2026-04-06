using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.Character;
using LittleWizard.Api.Extensions;
using System.Text;

namespace LittleWizard.Api.Relics;

[Pool(typeof(LittleWizardRelicPool))]
public abstract class LittleWizardRelics : CustomRelicModel{
    private static string PascalToSnakeCase(string input){
        if (string.IsNullOrEmpty(input))
            return input;

        var sb = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsUpper(c)){
                if (i > 0)
                    sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else{sb.Append(c);
            }
        }
        return sb.ToString();
    }
    private string GetBaseFileName(){
        string rawName = Id.Entry.RemovePrefix();          
        return PascalToSnakeCase(rawName);                 
    }
    protected override string BigIconPath => $"{GetBaseFileName()}.png".BigRelicImagePath();

    public override string PackedIconPath => $"{GetBaseFileName()}.tres".TresRelicImagePath();

    protected override string PackedIconOutlinePath => $"{GetBaseFileName()}_outline.tres".TresRelicImagePath();
}