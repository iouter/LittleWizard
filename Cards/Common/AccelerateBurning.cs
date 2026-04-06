using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class AccelerateBurning()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    private const string CalculatedFireElement = "CalculatedFireElement";
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(1),
            new ThresholdVar(5),
            new CalculationExtraVar(1),
            new CalculatedVar(CalculatedFireElement).WithMultiplier(
                (card, target) =>
                    Math.Floor((decimal)(target?.GetPowerAmount<FireElement>() ?? 0))
                    / DynamicVarsHelper.GetThresholdVar(card.DynamicVars).BaseValue
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await PowerCmd.Apply<FireElement>(
            cardPlay.Target,
            ((CalculatedVar)DynamicVars[CalculatedFireElement]).Calculate(cardPlay.Target),
            Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetThresholdVar(DynamicVars).UpgradeValueBy(-1);
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
