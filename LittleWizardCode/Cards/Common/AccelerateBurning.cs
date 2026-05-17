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
    private const string CalculatedExtraAmountKey = "CalculationExtraAmount";

    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(1),
            new ThresholdVar(5),
            new CalculationExtraVar(1),
            new DynamicVar("CalculatedFireElement", 1),
            new CalculatedVar(CalculatedExtraAmountKey).WithMultiplier(
                (card, target) =>
                {
                    decimal extra = card.DynamicVars.CalculationExtra.BaseValue;
                    decimal threshold = DynamicVarsHelper
                        .GetThresholdVar(card.DynamicVars)
                        .BaseValue;
                    int fire = target?.GetPowerAmount<FireElement>() ?? 0;
                    int quotient = (int)Math.Floor((decimal)fire / threshold);
                    int result = quotient * (int)extra;
                    if (result > 0)
                        result--;
                    return result;
                }
            ),
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Fire];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        decimal baseVal = DynamicVars["CalculatedFireElement"].BaseValue;
        decimal extraVal = ((CalculatedVar)DynamicVars[CalculatedExtraAmountKey]).Calculate(
            cardPlay.Target
        );
        decimal total = baseVal + extraVal;
        await PowerCmd.Apply<FireElement>(cardPlay.Target, total, Owner.Creature, this);
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetThresholdVar(DynamicVars).UpgradeValueBy(-1);
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
