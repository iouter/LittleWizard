using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class ManaBurst() : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ManaBurstPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (IsUpgraded)
            await PowerCmd.Apply<ManaBurstUpgradePower>(
                Owner.Creature,
                DynamicVarsHelper.GetPowerVar<ManaBurstPower>(DynamicVars).BaseValue,
                Owner.Creature,
                this
            );
        else
            await Utils.GivePower<ManaBurstPower>(this, cardPlay);
    }
}
