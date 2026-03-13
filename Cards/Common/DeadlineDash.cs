using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Common;

public class DeadlineDash() : LittleWizardCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.PlayerCombatState == null)
        {
            return;
        }
        if (Owner.PlayerCombatState.Energy != 0)
        {
            await PlayerCmd.GainEnergy(1, Owner);
        }
        else
        {
            await CardPileCmd.Draw(choiceContext, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}