using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Common;

public class FrozenWorld() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(8),
            new CalculationExtraVar(8),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(
                (card, _) => IsWaterElementEnough(card) ? 1 : 0
            ),
            new PowerVar<WaterElement>(4),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2);
        DynamicVars.CalculationExtra.UpgradeValueBy(2);
    }

    protected override bool ShouldGlowGoldInternal => IsWaterElementEnough(this);

    private static bool IsWaterElementEnough(CardModel card)
    {
        return card.CombatState != null
            && card.CombatState.HittableEnemies.Any(c =>
                c.GetPowerAmount<WaterElement>() >= card.DynamicVars.Power<WaterElement>().BaseValue
            );
    }
}
