using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Elements;

public class WaterElement : BaseElement
{
    private int oldReduction;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.TempWater];

    public override bool TryModifyPowerAmountReceived(
        PowerModel canonicalPower,
        Creature target,
        decimal amount,
        Creature? applier,
        out decimal modifiedAmount
    )
    {
        if (target != Owner || amount == 0)
        {
            modifiedAmount = amount;
            return false;
        }
        switch (canonicalPower)
        {
            case FireElement fire:
            {
                ElementHelper.FireAndWater(
                    new ThrowingPlayerChoiceContext(),
                    Owner,
                    Amount,
                    amount,
                    applier
                );
                modifiedAmount = 0;
                return true;
            }
            case EarthElement earth:
            {
                ElementHelper.WaterAndEarth(
                    new ThrowingPlayerChoiceContext(),
                    Owner,
                    Amount,
                    amount,
                    applier
                );
                modifiedAmount = 0;
                return true;
            }
            default:
            {
                modifiedAmount = amount;
                return false;
            }
        }
    }

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (power != this)
            return;

        int newReduction = Amount / 3;
        int delta = newReduction - oldReduction;
        if (delta != 0)
        {
            await PowerCmd.Apply<WaterTempPower>(choiceContext, Owner, delta, applier, cardSource);
            oldReduction = newReduction;
        }
    }

    public override async Task AfterRemoved(Creature Owner)
    {
        var temp = Owner.GetPower<WaterTempPower>();
        await PowerCmd.Remove(temp);
        await base.AfterRemoved(Owner);
    }
}
