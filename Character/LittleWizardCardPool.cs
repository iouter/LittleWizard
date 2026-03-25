using BaseLib.Abstracts;
using Godot;

namespace LittleWizard.Character;

public class LittleWizardCardPool : CustomCardPoolModel
{
    public override string Title => LittleWizard.InnerName;
    public override string BigEnergyIconPath => "res://LittleWizard/images/ui/combat/LittleWizard_energy_icon.png";

    public override string TextEnergyIconPath =>
        "res://LittleWizard/images/ui/combat//text_LittleWizard_energy_icon.png";

    public override string EnergyColorName => LittleWizard.InnerName;

    public override Color ShaderColor => new("384A61");

    //HSV�ռ�ɫ��
    public override float H => 0.75f; //ɫ�� 0-1
    public override float S => 0.7f; //���Ͷ�
    public override float V => 0.7f; //����
    public override Color DeckEntryCardColor => LittleWizard.CharacterColor;

    //public override Color EnergyOutlineColor => new("000000");

    public override bool IsColorless => false;
}