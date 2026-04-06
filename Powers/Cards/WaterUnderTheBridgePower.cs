using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Cards;

public class WaterUnderTheBridgePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != Owner)
            return;
        if (cardPlay.Card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
            await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Move, cardPlay);
    }
}
