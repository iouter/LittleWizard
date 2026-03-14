using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class ManaConvert() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var elementCardsInHand = Owner.PlayerCombatState?.Hand?.Cards
            .Where(c => c is IElementCard || (c.Enchantment != null && c.Enchantment is IElementCard))
            .ToList() ?? new List<CardModel>();

        if (elementCardsInHand.Count == 0) return;

        foreach (var card in elementCardsInHand)
        {
            await CardCmd.Exhaust(choiceContext, card);
            await PlayerCmd.GainEnergy(1, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
