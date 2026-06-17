using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using LittleWizard.LittleWizardCode.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class TidalWave()
    : LittleWizardCard(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string Reactor = "Reactor";
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(3, ValueProp.Move),
            new RepeatVar(3),
            new ThresholdVar(2),
            new CalculationBaseVar(0),
            new CalculationExtraVar(1),
            new CalculatedVar(Reactor).WithMultiplier(
                (card, target) =>
                    Utils.GetThresholdMultiplier<WaterEarthReactor>(card, target)
                    + Utils.GetThresholdMultiplier<FireWaterReactor>(card, target)
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await AnimationHelper.TriggerCastAnimationOwner(this);
        await CommonActions
            .CardAttack(this, cardPlay, hitCount: DynamicVars.Repeat.IntValue)
            .Execute(choiceContext);
        await CommonActions.Apply<WaterElement>(
            choiceContext,
            cardPlay.Target!,
            this,
            ((CalculatedVar)DynamicVars[Reactor]).Calculate(cardPlay.Target)
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1);
    }
}
