using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Elements;

public class WaterElement : BaseElement
{
    public override decimal ModifyDamageAdditive(
        Creature? target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource
    )
    {
        // ReSharper disable once PossibleLossOfFraction
        return Owner != dealer || !Utils.IsPoweredAttack(props)
            ? 0M
            : -Math.Ceiling((decimal)(Amount / 2));
    }

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

        var existing = Owner.GetPower<StrengthPower>();
        await PowerCmd.Remove(existing);

        if (Amount > 1)
        {
            await PowerCmd.Apply<StrengthPower>(
                choiceContext,
                Owner,
                -Amount / 2,
                applier,
                cardSource
            );
        }

        if (!applier!.HasPower<WaterElement>())
            await PowerCmd.Remove(existing);
    }
}
