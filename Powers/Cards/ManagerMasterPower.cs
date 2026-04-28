using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Powers.Cards;

public class ManagerMasterPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        SetAmount(0);
        await Task.CompletedTask;
    }

    public override async Task AfterEnergySpent(CardModel card, int amount)
    {
        if (Owner.Player == null || card.Owner != Owner.Player)
            return;

        if (card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
            return;

        SetAmount(Amount + amount);
        await Task.CompletedTask;
    }

    public override bool TryModifyEnergyCostInCombat(
        CardModel card,
        decimal originalCost,
        out decimal modifiedCost
    )
    {
        modifiedCost = originalCost;

        if (Owner.Player == null || card.Owner != Owner.Player)
            return false;

        if (!card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
            return false;

        if (Amount > 0)
        {
            modifiedCost = 0;
            return true;
        }

        return false;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Player == null || cardPlay.Card.Owner != Owner.Player)
            return;

        if (cardPlay.Card.CanonicalKeywords.Contains(CardKeyword.Ethereal) && Amount > 0)
            SetAmount(Amount - 1);

        await Task.CompletedTask;
    }
}
