using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Cards;

public class ManaShieldPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player
    )
    {
        if (Owner != player.Creature)
            return;

        await CreatureCmd.GainBlock(Owner, 3, ValueProp.Move, null);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player)
            return;

        var card = cardPlay.Card;

        if (ElementHelper.IsElementCard(card))
        {
            await CreatureCmd.GainBlock(Owner, 2, ValueProp.Move, cardPlay);
            ;
        }
    }
}
