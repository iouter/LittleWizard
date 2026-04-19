using BaseLib.Utils;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class TemporaryHandwriting()
    : LittleWizardCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    private const string ExtraCards = "ExtraCards";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(1), new(ExtraCards, 1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.Draw(this, choiceContext);
        if (Owner.PlayerCombatState == null)
        {
            return;
        }
        var count = Owner.PlayerCombatState.Hand.Cards.Count;
        var draws = DynamicVars[ExtraCards].BaseValue;
        if (count < 5)
            await CardPileCmd.Draw(choiceContext, draws, Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ExtraCards].UpgradeValueBy(1);
    }
}
