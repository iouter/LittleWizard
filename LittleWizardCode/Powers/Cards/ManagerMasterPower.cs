using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class ManagerMasterPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override int DisplayAmount => GetInternalData<Data>().FreeEtherealCards;
    public override bool ShouldReceiveCombatHooks => true;

    protected override object InitInternalData() => new Data();

    public override bool IsInstanced => true;

    private class Data
    {
        public int FreeEtherealCards = 0;
    }

    private void RefreshFreeOnCurrentHand()
    {
        if (Owner?.Player?.PlayerCombatState?.Hand?.Cards == null)
            return;
        var data = GetInternalData<Data>();
        if (data.FreeEtherealCards <= 0)
            return;

        foreach (var card in Owner.Player.PlayerCombatState.Hand.Cards)
        {
            if (card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
            {
                card.SetToFreeThisCombat();
            }
        }
    }

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        await base.AfterApplied(applier, cardSource);
        RefreshFreeOnCurrentHand();
    }

    public override Task AfterEnergySpent(CardModel card, int amount)
    {
        if (Owner?.Player == null)
            return Task.CompletedTask;
        if (amount > 0)
        {
            var data = GetInternalData<Data>();
            data.FreeEtherealCards += amount;
            InvokeDisplayAmountChanged();
            RefreshFreeOnCurrentHand();
        }
        return Task.CompletedTask;
    }

    public override Task AfterCardDrawn(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool fromHandDraw
    )
    {
        if (Owner?.Player == null)
            return Task.CompletedTask;
        if (card.Owner == Owner.Player && card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
        {
            var data = GetInternalData<Data>();
            if (data.FreeEtherealCards > 0)
            {
                card.SetToFreeThisCombat();
            }
        }
        return Task.CompletedTask;
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (Owner?.Player == null)
            return;

        var card = cardPlay.Card;
        if (card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
        {
            var data = GetInternalData<Data>();
            if (data.FreeEtherealCards > 0)
            {
                card.SetToFreeThisCombat();
                data.FreeEtherealCards--;
                InvokeDisplayAmountChanged();
            }
        }
        await base.BeforeCardPlayed(cardPlay);
    }

    public override async Task AfterCombatEnd(CombatRoom room)
    {
        await PowerCmd.Remove(this);
    }
}
