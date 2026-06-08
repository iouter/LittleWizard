using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class TidalWave()
    : LittleWizardCard(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(24, ValueProp.Move),
            new CalculationBaseVar(0),
            new CalculationExtraVar(2),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(
                (_, target) => target?.GetPowerAmount<WaterElement>() ?? 0
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await AnimationHelper.TriggerCastAnimationOwner(this);
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await CommonActions.CardBlock(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(12);
    }
}
