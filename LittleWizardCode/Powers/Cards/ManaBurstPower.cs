using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class ManaBurstPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override int DisplayAmount => GetInternalData<Data>().SkillCardsUsed;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override object InitInternalData() => new Data();

    protected virtual int GetThreshold()
    {
        return 4;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player)
            return;
        if (cardPlay.Card.Type != CardType.Skill)
        {
            return;
        }
        var data = GetInternalData<Data>();
        data.SkillCardsUsed += 1;
        InvokeDisplayAmountChanged();
        if (data.SkillCardsUsed >= GetThreshold())
        {
            Flash();
            await PlayerCmd.GainEnergy(1, Owner.Player);
            data.SkillCardsUsed = 0;
            InvokeDisplayAmountChanged();
        }
    }

    private class Data
    {
        public int SkillCardsUsed;
    }
}
