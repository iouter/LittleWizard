using BaseLib.Utils;
using LittleWizard.Api.Animation;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class FreezingRay()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(6),
            new ExtraDamageVar(1),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
                (card, target) => target?.GetPowerAmount<WaterElement>() ?? 0
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }
}
