using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Api.Animation;

public static class AnimationHelper
{
    public static async Task TriggerAnimationOwner(
        CardModel card,
        string triggerName,
        float waitTime
    )
    {
        await CreatureCmd.TriggerAnim(card.Owner.Creature, triggerName, waitTime);
    }

    public static async Task TriggerCastAnimationOwner(CardModel card, float waitTime)
    {
        await CreatureCmd.TriggerAnim(card.Owner.Creature, "Cast", waitTime);
    }

    public static async Task TriggerCastAnimationOwner(CardModel card)
    {
        await CreatureCmd.TriggerAnim(
            card.Owner.Creature,
            "Cast",
            card.Owner.Character.CastAnimDelay
        );
    }
}
