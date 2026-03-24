using LittleWizard.Api.Cards;
using LittleWizard.Api.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class ElementConvert()
    : LittleWizardCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
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
            await PowerCmd.Apply<WaterElement>(cardPlay.Target, fireAmount, Owner.Creature, this);
        }
        else if (waterAmount > 0)
        {
            await PowerCmd.Remove<WaterElement>(cardPlay.Target);
            await PowerCmd.Apply<EarthElement>(cardPlay.Target, waterAmount, Owner.Creature, this);
        }
        else if (earthAmount > 0)
        {
            await PowerCmd.Remove<EarthElement>(cardPlay.Target);
            await PowerCmd.Apply<WaterElement>(cardPlay.Target, earthAmount, Owner.Creature, this);
        }
    }

    protected override PileType GetResultPileType()
    {
        if (!IsUpgraded) return base.GetResultPileType();
        var resultPileType = base.GetResultPileType();
        return resultPileType != PileType.Discard ? resultPileType : PileType.Hand;
    }
}