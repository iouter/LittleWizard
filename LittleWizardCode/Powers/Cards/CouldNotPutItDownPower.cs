using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class CouldNotPutItDownPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override object InitInternalData() => new Data();

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (
            Owner == card.Owner.Creature
            && GetInternalData<Data>().CardModel == null
            && card.Type == CardType.Skill
        )
        {
            GetInternalData<Data>().CardModel = card;
        }
        return Task.CompletedTask;
    }

    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player
    )
    {
        var card = GetInternalData<Data>().CardModel;
        if (player.Creature != Owner || card == null)
        {
            return;
        }
        await CardPileCmd.Add(card, PileType.Hand);
        GetInternalData<Data>().CardModel = null;
    }

    private class Data
    {
        public CardModel? CardModel;
    }
}
