using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class BurnOut()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(0),
            new ExtraDamageVar(5),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
                Utils.GetThresholdMultiplier<FireElement>
            ),
            new ThresholdVar(2),
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Fire];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        VfxCmd.PlayOnCreatureCenter(cardPlay.Target!, "vfx/vfx_fire_mo");
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(2);
    }
}
