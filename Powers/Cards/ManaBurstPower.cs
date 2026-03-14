using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Powers.Cards;

public class ManaBurstPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override int DisplayAmount => GetInternalData<Data>().SkillCardsUsed;

    protected override object InitInternalData() => (object)new ManaBurstPower.Data();

    protected virtual int GetThreshold() => 4;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player)
        {
            return;
        }
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