using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class EarthFury()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string EarthElementCalculation = "EarthElementCalculation";

    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(0),
            new CalculationExtraVar(1),
            new ExtraDamageVar(1),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
                (card, _) => GetMultiplierAmount(card)
            ),
            new CalculatedVar(EarthElementCalculation).WithMultiplier(
                (card, _) => GetMultiplierAmount(card)
            ),
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Earth];

    private static int GetMultiplierAmount(CardModel card)
    {
        return card.Owner.Creature.Block / 2;
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        await AnimationHelper.TriggerCastAnimationOwner(this);
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        if (target!.IsDead)
        {
            return;
        }
        await PowerCmd.Apply<EarthElement>(
            choiceContext,
            target,
            ((CalculatedVar)DynamicVars[EarthElementCalculation]).Calculate(target),
            Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
