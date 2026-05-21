using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class GatherElementsPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        ICombatState combatState
    )
    {
        if (player != Owner.Player)
            return;

        var cards = CardFactory
            .GetDistinctForCombat(
                player,
                ModelDb
                    .CardPool<LittleWizardCardPool>()
                    .GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
                    .Where(ElementHelper.IsElementCard),
                Amount,
                player.RunState.Rng.CombatCardSelection
            )
            .ToList();

        foreach (var card in cards)
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner.Player);
    }
}
