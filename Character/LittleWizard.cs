using BaseLib.Abstracts;
using Godot;
using LittleWizard.Cards.Basic;
using LittleWizard.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Character;

public class LittleWizard : PlaceholderCharacterModel
{
    public const string InnerName = "little_wizard";
    public static readonly Color CharacterColor = new("384A61");

    public override Color NameColor => CharacterColor;
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 76;

    public override CardPoolModel CardPool => ModelDb.CardPool<LittleWizardCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<LittleWizardRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<LittleWizardPotionPool>();

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
        ModelDb.Card<Callback>()
    ];

	public override IReadOnlyList<RelicModel> StartingRelics =>
	[
		ModelDb.Relic<FireElementGem>()
	];
	public override CustomEnergyCounter? CustomEnergyCounter => new
	CustomEnergyCounter(EnergyCounterPaths,new Color(0.4f,0.1f,0.9f),new Color(0.7f,0.1f,0.9f));
	private string EnergyCounterPaths(int i)
	{
		return
    "res://LittleWizard/images/ui/combat/energy_counters/LittleWizard/LittleWizard_orb_layer_" + i +".png";
	}
    public override string CustomTrailPath => "";
    public override string CustomIconPath => "res://LittleWizard/scenes/LittleWizard/LittleWizard_icon.tscn";
    public override string CustomIconTexturePath => "res://LittleWizard/images/LittleWizard/character_icon_LittleWizard.png";
    public override string CustomCharacterSelectIconPath => "res://LittleWizard/images/ui/top_panel/character_icon_LittleWizard_outline.png";
}
