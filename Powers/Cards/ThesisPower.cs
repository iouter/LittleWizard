using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class ThesisPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override Task AfterPlayerTurnStartLate(PlayerChoiceContext choiceContext, Player player)
    {
        var cards = PileType.Hand.GetPile(player).Cards;
        foreach (var card in cards)
            card.SetToFreeThisTurn();

        return Task.CompletedTask;
    }
}
