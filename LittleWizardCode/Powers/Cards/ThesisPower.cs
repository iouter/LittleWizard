using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class ThesisPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override object InitInternalData() => new Data();

    public override bool TryModifyEnergyCostInCombat(
        CardModel card,
        decimal originalCost,
        out decimal modifiedCost
    )
    {
        if (
            card.Owner.Creature != Owner
            || card.Pile is not { Type: PileType.Hand }
            || GetInternalData<Data>().CardsPlayedThisTurn >= Amount
        )
        {
            modifiedCost = originalCost;
            return false;
        }
        modifiedCost = 0;
        return true;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (
            cardPlay.Card.Owner.Creature == Owner
            && cardPlay is { IsAutoPlay: false, IsLastInSeries: true }
        )
        {
            GetInternalData<Data>().CardsPlayedThisTurn++;
        }
        return Task.CompletedTask;
    }

    public override Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        CombatState combatState
    )
    {
        if (side == Owner.Side)
        {
            GetInternalData<Data>().CardsPlayedThisTurn = 0;
        }
        return Task.CompletedTask;
    }

    private class Data
    {
        public int CardsPlayedThisTurn;
    }
}
