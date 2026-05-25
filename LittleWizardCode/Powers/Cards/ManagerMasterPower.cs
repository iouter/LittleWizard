using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class ManagerMasterPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override int DisplayAmount => GetInternalData<Data>().FreeEtherealCards;
    public override bool ShouldReceiveCombatHooks => true;

    protected override object InitInternalData() => new Data();

    public override PowerInstanceType InstanceType => PowerInstanceType.InstancedPerApplier;

    private class Data
    {
        public int FreeEtherealCards;
    }

    public override Task AfterEnergySpent(CardModel card, int amount)
    {
        if (card.Owner.Creature != Owner)
        {
            return Task.CompletedTask;
        }

        if (
            card.Keywords.Contains(CardKeyword.Ethereal)
            && amount == 0
            && GetInternalData<Data>().FreeEtherealCards > 0
        )
        {
            GetInternalData<Data>().FreeEtherealCards--;
            InvokeDisplayAmountChanged();
            return Task.CompletedTask;
        }

        if (amount <= 0)
        {
            return Task.CompletedTask;
        }

        GetInternalData<Data>().FreeEtherealCards += amount;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    public override bool TryModifyEnergyCostInCombat(
        CardModel card,
        decimal originalCost,
        out decimal modifiedCost
    )
    {
        if (
            Owner == card.Owner.Creature
            && card.Keywords.Contains(CardKeyword.Ethereal)
            && GetInternalData<Data>().FreeEtherealCards > 0
        )
        {
            modifiedCost = 0;
            return true;
        }
        modifiedCost = originalCost;
        return false;
    }
}
