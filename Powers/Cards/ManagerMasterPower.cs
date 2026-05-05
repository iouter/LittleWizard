using LittleWizard.Api.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Powers.Cards;

public class ManagerMasterPower : LittleWizardPower
{
    private int _stack;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override int DisplayAmount => _stack;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        _stack = 0;
        await Task.CompletedTask;
    }

    public override async Task AfterEnergySpent(CardModel card, int amount)
    {
        if (Owner.Player == null || card.Owner != Owner.Player)
            return;

        if (card.CanonicalKeywords.Contains(CardKeyword.Ethereal))
            return;

        _stack += amount;
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

        if (_stack > 0)
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

        if (cardPlay.Card.CanonicalKeywords.Contains(CardKeyword.Ethereal) && _stack > 0)
        {
            _stack--;
        }

        await Task.CompletedTask;
    }
}
