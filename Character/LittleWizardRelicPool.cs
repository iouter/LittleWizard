using BaseLib.Abstracts;
using Godot;

namespace LittleWizard.Character;

public class LittleWizardRelicPool : CustomRelicPoolModel
{
    public override string EnergyColorName => LittleWizard.InnerName;

    public override Color LabOutlineColor => LittleWizard.CharacterColor;
}
