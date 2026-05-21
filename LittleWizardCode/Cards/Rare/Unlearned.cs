using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class Unlearned() : LittleWizardCard(2, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<UnlearnedPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<UnlearnedPower>(this, cardPlay, choiceContext);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
