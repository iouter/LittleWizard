using BaseLib.Abstracts;
using BaseLib.Extensions;
using LittleWizard.Api.Audios;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Api.Powers;

public abstract class LittleWizardPower : CustomPowerModel
{
    protected virtual string GetBaseName() => Id.Entry.RemovePrefix().ToLowerInvariant();

    public override string CustomPackedIconPath =>
        $"res://{MainFile.ModId}/images/powers/{GetBaseName()}.png";

    public override string CustomBigIconPath =>
        $"res://{MainFile.ModId}/images/powers/{GetBaseName()}.png";

    protected virtual bool HasCustomAudio => false;

    protected virtual string CustomAudioName => $"{GetBaseName()}.wav";

    private string CustomAudioPath => $"res://{MainFile.ModId}/audios/powers/{CustomAudioName}";

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        if (applier is { Side: CombatSide.Player })
        {
            PlaySound();
        }
        await base.AfterApplied(applier, cardSource);
    }

    protected void PlaySound()
    {
        if (!HasCustomAudio)
        {
            return;
        }
        AudioHelper.PlaySound(CustomAudioPath);
    }
}
