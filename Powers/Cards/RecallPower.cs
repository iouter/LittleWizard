using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace LittleWizard.Powers.Cards;

public class RecallPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private bool _hasTriggered = false;

    public override async Task AfterTurnStart(PlayerChoiceContext choiceContext, MegaCrit.Sts2.Core.Entities.Players.Player player)
    {
        if (Owner != player.Creature || _hasTriggered) return;
        
        _hasTriggered = true;
        
        // Select from 15 random cards and add to deck/hand
        // This requires a selection screen implementation
        await Utils.SelectAndAddRandomCards(15, player);
    }

    public override async Task AfterCombatEnd()
    {
        _hasTriggered = false;
    }
}
