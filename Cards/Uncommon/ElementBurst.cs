using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class ElementBurst()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string AnyElement = "AnyElement";
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(4),
            new CalculationExtraVar(3),
            new ThresholdVar(5),
            new CalculatedVar(AnyElement).WithMultiplier(
                (card, target) =>
                {
                    if (target == null)
                        return 0;
                    int[] elementsAmount =
                    [
                        target.GetPowerAmount<FireElement>(),
                        target.GetPowerAmount<WaterElement>(),
                        target.GetPowerAmount<EarthElement>(),
                    ];
                    var threshold = DynamicVarsHelper.GetThresholdVar(card.DynamicVars).IntValue;
                    return (
                        from amount in elementsAmount
                        where amount > 0
                        select Math.Floor((decimal)amount / threshold)
                    ).FirstOrDefault();
                }
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        var fireAmount = cardPlay.Target.GetPowerAmount<FireElement>();
        var waterAmount = cardPlay.Target.GetPowerAmount<WaterElement>();
        var earthAmount = cardPlay.Target.GetPowerAmount<EarthElement>();

        if (fireAmount > 0)
        {
            await PowerCmd.Apply<FireElement>(
                cardPlay.Target,
                ((CalculatedVar)DynamicVars[AnyElement]).Calculate(cardPlay.Target),
                Owner.Creature,
                this
            );
            return;
        }

        if (waterAmount > 0)
        {
            await PowerCmd.Apply<WaterElement>(
                cardPlay.Target,
                ((CalculatedVar)DynamicVars[AnyElement]).Calculate(cardPlay.Target),
                Owner.Creature,
                this
            );
            return;
        }

        if (earthAmount > 0)
            await PowerCmd.Apply<EarthElement>(
                cardPlay.Target,
                ((CalculatedVar)DynamicVars[AnyElement]).Calculate(cardPlay.Target),
                Owner.Creature,
                this
            );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(1);
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
