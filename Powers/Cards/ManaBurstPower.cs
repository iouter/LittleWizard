using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class ManaBurstPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override int DisplayAmount => GetInternalData<Data>().SkillCardsUsed;

    protected override object InitInternalData()
    {
        return new Data();
    }

    protected virtual int GetThreshold()
    {
        return 4;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player)
            return;
        var data = GetInternalData<Data>();
        data.SkillCardsUsed += 1;
        if (data.SkillCardsUsed >= GetThreshold())
        {
            Flash();
            await PlayerCmd.GainEnergy(1, Owner.Player);
            data.SkillCardsUsed = 0;
        }
    }

    private class Data
    {
        public int SkillCardsUsed;
    }
}
