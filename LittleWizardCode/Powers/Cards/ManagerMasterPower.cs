using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace LittleWizard.Powers.Cards
{
    public class ManagerMasterPower : LittleWizardPower
    {
        private class Data
        {
            public int FreeEtherealCards = 0;
        }

        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        public override int DisplayAmount => GetInternalData<Data>().FreeEtherealCards;
        public override bool ShouldReceiveCombatHooks => true;

        protected override object InitInternalData() => new Data();

        public override Task AfterEnergySpent(CardModel card, int amount)
        {
            if (Owner?.Player == null)
                return Task.CompletedTask;
            if (amount > 0)
            {
                var data = GetInternalData<Data>();
                data.FreeEtherealCards += amount;
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
                }
            }
            await base.BeforeCardPlayed(cardPlay);
        }

        public override async Task AfterCombatEnd(CombatRoom room)
        {
            await PowerCmd.Remove(this);
        }
    }
}
