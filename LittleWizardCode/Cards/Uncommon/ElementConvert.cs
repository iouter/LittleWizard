using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class ElementConvert()
    : LittleWizardCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        var fireAmount = cardPlay.Target.GetPowerAmount<FireElement>();
        var waterAmount = cardPlay.Target.GetPowerAmount<WaterElement>();
        var earthAmount = cardPlay.Target.GetPowerAmount<EarthElement>();

        if (fireAmount > 0)
        {
            await PowerCmd.Remove<FireElement>(cardPlay.Target);
            await PowerCmd.Apply<WaterElement>(
                choiceContext,
                cardPlay.Target,
                fireAmount,
                Owner.Creature,
                this
            );
        }
        else if (waterAmount > 0)
        {
            await PowerCmd.Remove<WaterElement>(cardPlay.Target);
            await PowerCmd.Apply<EarthElement>(
                choiceContext,
                cardPlay.Target,
                waterAmount,
                Owner.Creature,
                this
            );
        }
        else if (earthAmount > 0)
        {
            await PowerCmd.Remove<EarthElement>(cardPlay.Target);
            await PowerCmd.Apply<FireElement>(
                choiceContext,
                cardPlay.Target,
                earthAmount,
                Owner.Creature,
                this
            );
        }

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override PileType GetResultPileTypeForCardPlay()
    {
        if (!IsUpgraded)
        {
            return base.GetResultPileTypeForCardPlay();
        }
        var result = base.GetResultPileTypeForCardPlay();
        return result != PileType.Discard ? result : PileType.Hand;
    }
}
