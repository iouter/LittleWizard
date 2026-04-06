using BaseLib.Abstracts;
using Godot;

namespace LittleWizard.Character;

public class LittleWizardPotionPool : CustomPotionPoolModel
{
    public override string EnergyColorName => LittleWizard.InnerName;

    public override Color LabOutlineColor => LittleWizard.CharacterColor;
}
