using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class ElementBurst()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string AnyElement = "AnyElement";
    private const string ElementName = "ElementName";
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(4),
            new CalculationExtraVar(3),
            new ThresholdVar(8),
            new CalculatedVar(AnyElement).WithMultiplier(
                (card, target) =>
                {
                    var stringVar = (StringVar)card.DynamicVars[ElementName];
                    if (target == null)
                    {
                        stringVar.StringValue = "???";
                        return 0;
                    }

                    var element = ElementHelper.GetElement(target);
                    if (element == null)
                    {
                        stringVar.StringValue = "???";
                        return 0;
                    }
                    stringVar.StringValue = element.Title.GetRawText();
                    var threshold = card.DynamicVars.Threshold().BaseValue;
                    return Math.Floor(element.Amount / threshold);
                }
            ),
            new StringVar(ElementName, "???"),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var element = ElementHelper.GetElement(cardPlay.Target!);
        if (element == null)
        {
            ((StringVar)DynamicVars[ElementName]).StringValue = "";
            return;
        }
        await PowerCmd.Apply(
            choiceContext,
            element,
            cardPlay.Target!,
            ((CalculatedVar)DynamicVars[AnyElement]).Calculate(cardPlay.Target),
            Owner.Creature,
            this
        );
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(1);
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
