using System;
using System.Threading.Tasks;
using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Powers.Cards
{
    public class ThesisPower : LittleWizardPower
    {
        public override PowerType Type
        {
            get { return PowerType.Buff; }
        }

        public override PowerStackType StackType
        {
            get { return PowerStackType.Counter; }
        }

        protected override object InitInternalData()
        {
            return new ThesisPower.Data();
        }

        public override bool TryModifyEnergyCostInCombat(
            CardModel card,
            decimal originalCost,
            out decimal modifiedCost
        )
        {
            modifiedCost = originalCost;
            if (this.ShouldSkip(card))
            {
                return false;
            }
            modifiedCost = 0;
            return true;
        }

        public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {
            if (
                cardPlay.Card.Owner.Creature == base.Owner
                && !cardPlay.IsAutoPlay
                && cardPlay.IsLastInSeries
            )
            {
                base.GetInternalData<ThesisPower.Data>().cardsPlayedThisTurn++;
            }
            return Task.CompletedTask;
        }

        public override Task BeforeSideTurnStart(
            PlayerChoiceContext choiceContext,
            CombatSide side,
            CombatState combatState
        )
        {
            if (side == base.Owner.Side)
            {
                base.GetInternalData<ThesisPower.Data>().cardsPlayedThisTurn = 0;
            }
            return Task.CompletedTask;
        }

        private bool ShouldSkip(CardModel card)
        {
            bool flag = card.Owner.Creature == base.Owner;
            bool flag2 = card.Pile != null && card.Pile.Type == PileType.Hand;
            return !flag
                || !flag2
                || base.GetInternalData<ThesisPower.Data>().cardsPlayedThisTurn >= base.Amount;
        }

        private class Data
        {
            public int cardsPlayedThisTurn;
        }
    }

    private class Data
    {
        public int CardsPlayedThisTurn;
    }
}
