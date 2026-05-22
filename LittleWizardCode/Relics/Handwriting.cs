using LittleWizard.LittleWizardCode.Api.Relics;
using LittleWizard.LittleWizardCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Relics;

public class Handwriting : LittleWizardRelics
{
    public override RelicRarity Rarity => RelicRarity.Shop;

    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IReadOnlyList<Creature> creatures,
        ICombatState combatState
    )
    {
        if (side != Owner.Creature.Side || combatState.RoundNumber > 1)
            return;
        Flash();
        var cards = ModelDb
            .CardPool<LittleWizardCardPool>()
            .GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint)
            .Where(card => card.Type == CardType.Power)
            .ToList();
        var card = Owner.RunState.Rng.CombatCardGeneration.NextItem(cards);
        if (card == null)
        {
            return;
        }
        card.SetToFreeThisCombat();
        await CardPileCmd.Add(card, PileType.Hand);
    }
}
