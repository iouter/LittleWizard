using LittleWizard.Cards.Interface;
using LittleWizard.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

namespace LittleWizard.Powers.Cards;

public class GatherElementsPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player != Owner.Player) return;

        var cards = CardFactory.GetDistinctForCombat(player,
                ModelDb.CardPool<LittleWizardCardPool>().GetUnlockedCards(player.UnlockState,
                    player.RunState.CardMultiplayerConstraint).Where(model => model is IElementCard),
                Amount, Rng.Chaotic).ToList();

        foreach (var card in cards)
        {
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        }
    }
}