using BaseLib.Cards.Variables;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.LittleWizardCode.Powers.Elements;

public class WaterElement : BaseElement
{
    private const string TempWaterPower = "tempWaterPower";
    private decimal AmountApplied { get; set; }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new(TempWaterPower + "Base", 0),
            new(TempWaterPower + "Extra", -1),
            new CustomCalculatedVar("tempWaterPower").WithMultiplier(
                (power, _) => GetDamageAdditive(power.Amount)
            ),
        ];

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

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        Removed += async () =>
        {
            await PowerCmd.Apply<StrengthPower>(
                new ThrowingPlayerChoiceContext(),
                Owner,
                -AmountApplied,
                null,
                null
            );
        };
        return Task.CompletedTask;
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
        {
            return;
        }
        var amountApplied = GetDamageAdditive(Amount) - GetDamageAdditive(Amount - amount);
        AmountApplied += amountApplied;
        await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, amountApplied, applier, null);
    }

    private static decimal GetDamageAdditive(decimal amount)
    {
        return -Math.Ceiling(amount / 3);
    }
}
