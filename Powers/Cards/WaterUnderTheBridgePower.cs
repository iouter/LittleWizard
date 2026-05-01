using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
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

    protected override object InitInternalData() => new Data();

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player || GetInternalData<Data>().IsPlayed)
            return;
        if (cardPlay.Card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
        {
            await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Move, cardPlay);
            GetInternalData<Data>().IsPlayed = true;
        }
    }

    public override Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        CombatState combatState
    )
    {
        if (side == Owner.Side)
        {
            GetInternalData<Data>().IsPlayed = false;
        }
        return Task.CompletedTask;
    }

    private class Data
    {
        public bool IsPlayed;
    }
}
