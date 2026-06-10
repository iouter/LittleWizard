using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class CouldNotPutItDownPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override (PileType, CardPilePosition) ModifyCardPlayResultPileTypeAndPosition(
        CardModel card,
        bool isAutoPlay,
        ResourceInfo resources,
        PileType pileType,
        CardPilePosition position
    )
    {
        if (card.Owner.Creature != Owner)
            return (pileType, position);
        if (card.Type != CardType.Skill)
            return (pileType, position);
        if (pileType != PileType.Discard)
            return (pileType, position);

        int playedCount = CombatManager.Instance.History.CardPlaysStarted.Count(e =>
            e.HappenedThisTurn(CombatState)
            && e.CardPlay.Card.Type == CardType.Skill
            && e.CardPlay.Card.Owner == Owner.Player
        );

        if (playedCount == 0)
            return (PileType.Draw, CardPilePosition.Top);

        return (pileType, position);
    }

    public override Task AfterModifyingCardPlayResultPileOrPosition(
        CardModel card,
        PileType pileType,
        CardPilePosition position
    )
    {
        if (card.Owner.Creature != Owner)
            return Task.CompletedTask;
        if (pileType == PileType.Draw && position == CardPilePosition.Top)
            Flash();
        return Task.CompletedTask;
    }
}
