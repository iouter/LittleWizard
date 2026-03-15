using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Cards;

public class WildMagicPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task ModifyDamage(PlayerChoiceContext choiceContext, ref DamageResult result, ValueProp props, CardModel card)
    {
        if (card.CardType != CardType.Attack) return;
        
        // Double damage
        result.BaseValue *= 2;
        
        // Double element stacks applied
        // This would need custom logic to double element application
    }

    public override bool ShouldTargetRandomEnemy(CardModel card)
    {
        return card.CardType == CardType.Attack;
    }
}
