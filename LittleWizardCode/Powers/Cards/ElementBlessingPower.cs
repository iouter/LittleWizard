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
    public override int DisplayAmount => GetInternalData<Data>().CardPlayed;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (card.Owner != Owner.Player)
            return;

        if (ElementHelper.IsElementCard(card))
        {
            if (GetInternalData<Data>().CardPlayed > 0)
            {
                await PlayerCmd.GainEnergy(Amount, card.Owner);
                GetInternalData<Data>().CardPlayed = 0;
                InvokeDisplayAmountChanged();
            }
            else
            {
                GetInternalData<Data>().CardPlayed += 1;
                InvokeDisplayAmountChanged();
            }
        }
    }

    private class Data
    {
        public int CardPlayed;
    }
}
