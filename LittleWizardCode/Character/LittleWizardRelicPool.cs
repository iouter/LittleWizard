using BaseLib.Abstracts;
using Godot;

namespace LittleWizard.LittleWizardCode.Character;

public class LittleWizardRelicPool : CustomRelicPoolModel
{
    public override string EnergyColorName => LittleWizard.InnerName;

    public override Color LabOutlineColor => LittleWizard.CharacterColor;
}
