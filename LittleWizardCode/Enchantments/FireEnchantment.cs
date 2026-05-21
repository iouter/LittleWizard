using LittleWizard.LittleWizardCode.Api.Enchantments;
using LittleWizard.LittleWizardCode.Api.Interface;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Enchantments;

public sealed class FireEnchantment : LittleWizardEnchantment, IElementEnchantment
{
    public override bool ShowAmount => true;

    public override bool CanEnchantCardType(CardType cardType)
    {
        return cardType == CardType.Attack;
    }

    public override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay? cardPlay)
    {
        if (cardPlay is { Target: not null })
            await PowerCmd.Apply<FireElement>(
                choiceContext,
                cardPlay.Target,
                Amount,
                cardPlay.Card.Owner.Creature,
                cardPlay.Card
            );
    }
}
