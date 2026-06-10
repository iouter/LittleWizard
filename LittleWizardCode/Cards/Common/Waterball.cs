using BaseLib.Utils;
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
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Common;

public class Waterball()
    : LittleWizardCard(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(8, ValueProp.Move),
            new PowerVar<WaterElement>(3),
            new PowerVar<WaterBallCostAdd>(1),
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipsValue.Water, HoverTipsValue.TempWater];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await Utils.GivePower<WaterElement>(this, play, choiceContext);
        await AnimationHelper.TriggerCastAnimationOwner(this);
        if (!Owner.Creature.HasPower<WaterBallCostAdd>())
            await PowerCmd.Apply<WaterBallCostAdd>(
                choiceContext,
                Owner.Creature,
                DynamicVarsHelper.GetPowerVar<WaterBallCostAdd>(DynamicVars).BaseValue,
                Owner.Creature,
                this
            );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVarsHelper.GetPowerVar<WaterElement>(DynamicVars).UpgradeValueBy(2);
    }
}
