using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Relics;

public class RitualList : LittleWizardRelics
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player
    )
    {
        if (Owner != player || player.Creature.CombatState!.RoundNumber > 1)
            return;
        Flash();
        var card = await Utils.SelectSingleCard(
            Owner,
            SelectionScreenPrompt,
            choiceContext,
            PileType.Draw
        );
        if (card == null)
        {
            return;
        }
        await CardPileCmd.Add(card, PileType.Hand);
    }
}
