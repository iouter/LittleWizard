using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Powers.Cards;

public class SimpleTrickPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool TryModifyEnergyCostInCombat(
        CardModel card,
        decimal originalCost,
        out decimal modifiedCost
    )
    {
        if (card.Type == CardType.Power)
        {
            modifiedCost = originalCost - Amount;
            return true;
        }

        modifiedCost = originalCost;
        return false;
    }
}
