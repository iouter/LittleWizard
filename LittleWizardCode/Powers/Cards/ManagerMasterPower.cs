using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class ManagerMasterPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override int DisplayAmount => GetInternalData<Data>().FreeEtherealCards;

    protected override object InitInternalData() => new Data();

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        SetAmount(1);
        GetInternalData<Data>().FreeEtherealCards = 0;
        InvokeDisplayAmountChanged();
    }

    public override async Task AfterEnergySpent(CardModel card, int amount)
    {
        if (Owner.Player == null || card.Owner != Owner.Player)
            return;
        if (card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
            return;

        GetInternalData<Data>().FreeEtherealCards += amount;
        InvokeDisplayAmountChanged();
    }

    public override bool TryModifyEnergyCostInCombat(
        CardModel card,
        decimal originalCost,
        out decimal modifiedCost
    )
    {
        modifiedCost = originalCost;
        if (Owner.Player == null || card.Owner != Owner.Player)
            return false;
        if (!card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
            return false;

        if (GetInternalData<Data>().FreeEtherealCards > 0)
        {
            modifiedCost = 0;
            return true;
        }
        return false;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Player == null || cardPlay.Card.Owner != Owner.Player)
            return;

        if (cardPlay.Card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
        {
            var data = GetInternalData<Data>();
            if (data.FreeEtherealCards > 0)
            {
                data.FreeEtherealCards--;
                InvokeDisplayAmountChanged();
            }
        }
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (Owner.Player == null)
            return;
        if (side == Owner.Side)
        {
            GetInternalData<Data>().FreeEtherealCards = 0;
            InvokeDisplayAmountChanged();
        }
    }

    private class Data
    {
        public int FreeEtherealCards;
    }
}
