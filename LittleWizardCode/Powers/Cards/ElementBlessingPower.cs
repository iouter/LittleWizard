using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class ElementBlessingPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    public override int DisplayAmount => GetInternalData<Data>()?.CardPlayed ?? 0;

    protected override object InitInternalData() => new Data();

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        var card = cardPlay.Card;

        if (card.Owner?.Creature != Owner)
            return;

        if (!ElementHelper.IsElementCard(card))
            return;

        var data = GetInternalData<Data>();

        if (data.CardPlayed > 0)
        {
            if (card.Owner != null)
                await PlayerCmd.GainEnergy(Amount, card.Owner);
            data.CardPlayed = 0;
            InvokeDisplayAmountChanged();
        }
        else
        {
            data.CardPlayed++;
            InvokeDisplayAmountChanged();
        }
    }

    private class Data
    {
        public int CardPlayed;
    }
}
