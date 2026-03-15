using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class ThesisPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterTurnStart(PlayerChoiceContext choiceContext, MegaCrit.Sts2.Core.Entities.Players.Player player)
    {
        if (Owner != player.Creature) return;
        
        // Make cards drawn this turn cost 0
        // This requires tracking cards drawn and modifying their cost temporarily
        await ApplyZeroCostToDrawnCards(player);
    }

    private async Task ApplyZeroCostToDrawnCards(MegaCrit.Sts2.Core.Entities.Players.Player player)
    {
        // Implementation would need to track drawn cards and set their cost to 0 for the turn
        // This is a simplified placeholder
        foreach (var card in player.Hand.Cards)
        {
            // Set temporary zero cost for this turn
            // Actual implementation would need game's temporary cost modification system
        }
    }
}
