namespace LittleWizard.Extensions;

//Mostly utilities to get asset paths.
public static class StringExtensions
{

    public static string ImagePath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", path);
    }
    
    public static string CardImagePath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", "card_portraits", path);
    }
    public static string BigCardImagePath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", "card_portraits", "big", path);
    }

    public static string PowerImagePath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", "powers", path);
    }

    public static string BigPowerImagePath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", "powers", "big", path);
    }

    public static string RelicImagePath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", "relics", path);
    }

    public static string BigRelicImagePath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", "relics", "big", path);
    }

    public static string CharacterUiPath(this string path)
    {
        return Path.Join(MainFile.ModIdPublic, "images", "charui", path);
    }
}