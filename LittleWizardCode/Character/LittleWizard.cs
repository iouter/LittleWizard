using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using Godot;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Cards.Basic;
using LittleWizard.LittleWizardCode.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Character;

public class LittleWizard : PlaceholderCharacterModel
{
    public const string InnerName = "little_wizard";
    public static readonly Color CharacterColor = new("384A61");

    public override Color NameColor => CharacterColor;

    public static readonly Color Color = new(1.0f, 0.8f, 0.0f);
    public override Color MapDrawingColor => Color;
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 76;

    public override CardPoolModel CardPool => ModelDb.CardPool<LittleWizardCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<LittleWizardRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<LittleWizardPotionPool>();
    public override RelicIconData CustomYummyCookie =>
        new(
            "little_wizard_cookie.png".BigRelicImagePath(),
            "little_wizard_cookie.tres".TresRelicImagePath(),
            "little_wizard_cookie_outline.tres".TresRelicImagePath()
        );

    public override IEnumerable<CardModel> StartingDeck =>
        [
            ModelDb.Card<StrikeLittleWizard>(),
            ModelDb.Card<StrikeFireLittleWizard>(),
            ModelDb.Card<StrikeWaterLittleWizard>(),
            ModelDb.Card<StrikeEarthLittleWizard>(),
            ModelDb.Card<ColorfulBalls>(),
            ModelDb.Card<DefendLittleWizard>(),
            ModelDb.Card<DefendLittleWizard>(),
            ModelDb.Card<DefendLittleWizard>(),
            ModelDb.Card<DefendLittleWizard>(),
            ModelDb.Card<Callback>(),
        ];

    public override IReadOnlyList<RelicModel> StartingRelics => [ModelDb.Relic<ElementalOre>()];

    public override string CustomEnergyCounterPath =>
        "res://LittleWizard/scenes/LittleWizard/LittleWizard_energy_counter.tscn";
    public override Color EnergyLabelOutlineColor => new(0x552262FF);

    public override string CustomAttackSfx => "res://";
    public override string CustomCastSfx => "res://";
    public override string CustomDeathSfx => "res://";

    /* public override string CustomTrailPath =>
         "res://LittleWizard/scenes/LittleWizard/card_trail_LittleWizard.tscn";*/
    public override string CustomVisualPath =>
        "res://LittleWizard/scenes/LittleWizard/LittleWizard.tscn";

    public override string CustomMerchantAnimPath =>
        "res://LittleWizard/scenes/LittleWizard/LittleWizard_merchant.tscn";

    public override string CustomRestSiteAnimPath =>
        "res://LittleWizard/scenes/LittleWizard/LittleWizard_rest_site.tscn";

    public override string CustomIconPath =>
        "res://LittleWizard/scenes/LittleWizard/LittleWizard_icon.tscn";

    public override string CustomIconTexturePath =>
        "res://LittleWizard/images/LittleWizard/character_icon_LittleWizard.png";

    public override string CustomIconOutlineTexturePath =>
        "res://LittleWizard/images/ui/top_panel/character_icon_LittleWizard_outline.png";

    public override string CustomCharacterSelectLockedIconPath =>
        "res://LittleWizard/images/LittleWizard/char_select_LittleWizard_locked.png";

    public override string CustomCharacterSelectIconPath =>
        "res://LittleWizard/images/LittleWizard/char_select_LittleWizard.png";
    public override string CustomArmPointingTexturePath =>
        "res://LittleWizard/images/LittleWizard/hands/multiplayer_hand_LittleWizard_point.png";

    public override string CustomArmRockTexturePath =>
        "res://LittleWizard/images/LittleWizard/hands/multiplayer_hand_LittleWizard_rock.png";

    public override string CustomArmPaperTexturePath =>
        "res://LittleWizard/images/LittleWizard/hands/multiplayer_hand_LittleWizard_paper.png";

    public override string CustomArmScissorsTexturePath =>
        "res://LittleWizard/images/LittleWizard/hands/multiplayer_hand_LittleWizard_scissors.png";
    public override string CustomCharacterSelectBg =>
        "res://LittleWizard/scenes/LittleWizard/LittleWizard_select_bg.tscn";
    public override string CustomMapMarkerPath =>
        "res://LittleWizard/images/packed/map/icons/map_marker_LittleWizard.png";
}
