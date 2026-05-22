using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Api.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Relics;

public class ApprenticeCodex : LittleWizardRelics
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

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
        var card = CardFactory
            .GetDistinctForCombat(
                Owner,
                Owner
                    .Character.CardPool.GetUnlockedCards(
                        Owner.UnlockState,
                        Owner.RunState.CardMultiplayerConstraint
                    )
                    .Where(c => c.Tags.Contains(CardTagExtensions.LittleWizardElement)),
                1,
                Owner.RunState.Rng.CombatCardGeneration
            )
            .FirstOrDefault();
        if (card == null)
        {
            return;
        }
        card.SetToFreeThisTurn();
        await CardPileCmd.Add(card, PileType.Hand);
    }
}
