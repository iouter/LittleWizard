using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class ElementBurst()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string ElementBurstKey = "ElementBurstAmount";

    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(4),
            new CalculationExtraVar(4),
            new CalculatedVar(ElementBurstKey).WithMultiplier(
                (card, target) =>
                {
                    if (target == null)
                        return 0;

                    int fire = target.GetPowerAmount<FireElement>();
                    int water = target.GetPowerAmount<WaterElement>();
                    int earth = target.GetPowerAmount<EarthElement>();

                    int currentAmount = 0;
                    if (fire > 0)
                        currentAmount = fire;
                    else if (water > 0)
                        currentAmount = water;
                    else if (earth > 0)
                        currentAmount = earth;
                    else
                        return 0;

                    return currentAmount / 8;
                }
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null)
            return;

        if (DynamicVars[ElementBurstKey] is not CalculatedVar calcVar)
            return;
        int totalAmount = (int)calcVar.Calculate(target);
        if (totalAmount <= 0)
            return;

        PowerModel? power;
        if (target.HasPower<FireElement>())
            power = ModelDb.Power<FireElement>().ToMutable();
        else if (target.HasPower<WaterElement>())
            power = ModelDb.Power<WaterElement>().ToMutable();
        else if (target.HasPower<EarthElement>())
            power = ModelDb.Power<EarthElement>().ToMutable();
        else
            return;

        await AnimationHelper.TriggerCastAnimationOwner(this);

        await PowerCmd.Apply(
            choiceContext,
            power,
            target,
            totalAmount,
            Owner.Creature,
            this,
            false
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(1);
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
