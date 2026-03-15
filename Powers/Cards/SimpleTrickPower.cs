using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace LittleWizard.Powers.Cards;

public class SimpleTrickPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override int ModifyCardCost(CardModel card, int originalCost)
    {
        if (card.CardType == CardType.Power && Owner.Player == card.Owner)
        {
            return Mathf.Max(0, originalCost - 1);
        }
        return originalCost;
    }
}
