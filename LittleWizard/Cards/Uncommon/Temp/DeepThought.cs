using System.Threading.Tasks;
using InscryptionAPI.Card;
using InscryptionAPI.Misc;

namespace LittleWizard.Cards.Uncommon.Temp;

public class DeepThought() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override CardKeyword[] CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        // End turn, keeping remaining energy and hand
        await choiceContext.EndTurn(keepEnergyAndHand: true);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cost.UpgradeValueBy(-1);
    }
}
