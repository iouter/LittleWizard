using BaseLib.Abstracts;
using Godot;

namespace LittleWizard.Character;

public class LittleWizardCardPool : CustomCardPoolModel
{
	public override string Title => LittleWizard.InnerName;
	public override string BigEnergyIconPath => "res://LittleWizard/images/ui/combat/LittleWizard_energy_icon.png";

	public override string TextEnergyIconPath => "res://LittleWizard/images/ui/combat//text_LittleWizard_energy_icon.png";

	public override string CardFrameMaterialPath => "card_frame_red";
	public override Color ShaderColor => new("384A61");
	public override float H => 0.75f;
	public override float S => 0.6f; 
	public override float V => 1.0f; 
	public override Color DeckEntryCardColor => new("4E437F");
	public override Color EnergyOutlineColor => new("4E437F");
	public override bool IsColorless => false;
}
