using LittleWizard.Api.Cards;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class FrozenWorld()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(8),
            new CalculationExtraVar(8),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(
                (_, target) => target != null && target.GetPowerAmount<WaterElement>() >= 4 ? 1 : 0
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(
            Owner.Creature,
            DynamicVars.CalculatedBlock.Calculate(cardPlay.Target),
            ValueProp.Move,
            cardPlay
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2);
        DynamicVars.CalculationExtra.UpgradeValueBy(2);
    }
}
