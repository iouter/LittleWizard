using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Common;

public class AccelerateBurning()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    private const string CalculatedFireElement = "CalculatedFireElement";
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(1),
            new ThresholdVar(5),
            new CalculationExtraVar(2),
            new CalculatedVar(CalculatedFireElement).WithMultiplier(
                Utils.GetThresholdMultiplier<FireElement>
            ),
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Fire];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<FireElement>(
            choiceContext,
            cardPlay.Target!,
            ((CalculatedVar)DynamicVars[CalculatedFireElement]).Calculate(cardPlay.Target),
            Owner.Creature,
            this
        );
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Threshold().UpgradeValueBy(-1);
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
