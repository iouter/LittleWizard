using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class UnlearnedPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardExhausted(PlayerChoiceContext context, CardModel card)
    {
        if (Owner.Player != card.Owner) return;
        
        // Gain 1 Strength and 1 Agility for each exhausted card
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, 1, Owner.Creature, null);
        await PowerCmd.Apply<AgilityPower>(Owner.Creature, 1, Owner.Creature, null);
    }
}
