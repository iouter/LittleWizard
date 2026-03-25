using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class TemporaryHandwriting() : LittleWizardCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    private const string ExtraCards = "ExtraCards";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1),
        new(ExtraCards, 1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // 抽1张牌
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);

        if (Owner.PlayerCombatState is { DrawPile.Cards.Count: > 0 })
        {
            var count = Owner.PlayerCombatState.DrawPile.Cards.Count;
            var draws = DynamicVars[ExtraCards].BaseValue;
            if (count >= draws)
                await CardPileCmd.Draw(choiceContext, draws, Owner);
            else
                await CardPileCmd.Draw(choiceContext, count, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ExtraCards].UpgradeValueBy(1);
    }
}