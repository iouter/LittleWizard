using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Powers.Cards;

public class UnlearnedPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool causedByEthereal
    )
    {
        if (Owner.Player != card.Owner)
            return;

        await PowerCmd.Apply<StrengthPower>(Owner, Amount, Owner, null);
        await PowerCmd.Apply<DexterityPower>(Owner, Amount, Owner, null);
    }
}
