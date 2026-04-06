using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class ManagerMaster()
    : LittleWizardCard(0, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<ManagerMasterPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Creature.Player is { PlayerCombatState: not null })
        {
            var cards = Owner.Creature.Player.PlayerCombatState.Hand.Cards.ToList();
            if (cards.Any(card => card.CanonicalKeywords.Contains(CardKeyword.Ethereal)))
                await Utils.GivePower<ManagerMasterPower>(this, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<ManagerMasterPower>(DynamicVars).UpgradeValueBy(1);
    }
}
