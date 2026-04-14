using System;

namespace LittleWizard.Api;

public static class CardPathUtils
{
    public static string GetCardImagePath(this Type cardType)
    {
        if (cardType == null)
            throw new ArgumentNullException(nameof(cardType));
        string snakeName = NamingUtils.ToSnakeCase(cardType.Name);
        return $"res://{MainFile.ModId}/images/card_portraits/{snakeName}.png";
    }
}
