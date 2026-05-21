using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Powers.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class Flustered() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override CardKeyword[] CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FlusteredPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var cardsToDraw = 10 - Owner.PlayerCombatState!.Hand.Cards.Count;
        if (cardsToDraw > 0)
        {
            await CardPileCmd.Draw(choiceContext, cardsToDraw, Owner);
        }

        await Utils.GivePower<FlusteredPower>(this, play, choiceContext);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
