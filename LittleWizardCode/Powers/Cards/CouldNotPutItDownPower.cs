using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class CouldNotPutItDownPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override CardLocation ModifyCardPlayResultLocation(
        CardModel card,
        bool isAutoPlay,
        ResourceInfo resources,
        CardLocation cardLocation
    )
    {
        if (
            card.Owner.Creature != Owner
            || card.Type != CardType.Skill
            || cardLocation.pileType != PileType.Discard
        )
            return cardLocation;

        var playedCount = CombatManager.Instance.History.CardPlaysStarted.Count(e =>
            e.HappenedThisTurn(CombatState)
            && e.CardPlay.Card.Type == CardType.Skill
            && e.CardPlay.Card.Owner == Owner.Player
        );

        if (playedCount >= Amount)
        {
            return cardLocation;
        }
        cardLocation.pileType = PileType.Draw;
        cardLocation.position = CardPilePosition.Top;
        return cardLocation;
    }

    public override Task AfterModifyingCardPlayResultLocation(
        CardModel card,
        CardLocation cardLocation
    )
    {
        if (card.Owner.Creature != Owner)
            return Task.CompletedTask;
        if (cardLocation is { pileType: PileType.Draw, position: CardPilePosition.Top })
            Flash();
        return Task.CompletedTask;
    }
}
