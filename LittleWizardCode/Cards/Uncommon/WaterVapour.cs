using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.DynamicVars;
using LittleWizard.LittleWizardCode.Powers.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class WaterVapour()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WaterVapourPower>(2)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipsValue.Water, HoverTipsValue.TempWater];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target is null)
            return;

        var artifact = target.GetPowerAmount<ArtifactPower>();
        if (artifact > 0)
        {
            await PowerCmd.Remove<ArtifactPower>(target);
            await PowerCmd.Apply<StrengthPower>(
                choiceContext,
                target,
                -1 * artifact,
                Owner.Creature,
                this
            );
        }

        await Utils.GivePower<WaterVapourPower>(this, cardPlay, choiceContext);
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<WaterVapourPower>(DynamicVars).UpgradeValueBy(1);
    }
}
