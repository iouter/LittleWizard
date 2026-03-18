using BaseLib.Abstracts;
using LittleWizard.Extensions;
using Godot;


namespace LittleWizard.Character;

public class LittleWizardCardPool : CustomCardPoolModel
{
	public override string Title => LittleWizard.InnerName;
    public override string BigEnergyIconPath => "charui/LittleWizard_energy_icon.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_LittleWizard_energy_icon.png".ImagePath();
	public override string EnergyColorName => LittleWizard.InnerName;

    public override Color ShaderColor => new("384A61");
    //HSV空间色彩
    public override float H => 0.75f; //色调 0-1
    public override float S => 0.7f; //饱和度
    public override float V => 0.7f; //明度
    public override Color DeckEntryCardColor => LittleWizard.CharacterColor;

    //public override Color EnergyOutlineColor => new("000000");

    public override bool IsColorless => false;
}
