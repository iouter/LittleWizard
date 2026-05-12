using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class RockBlast()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<EarthElement>(5), new RepeatVar(1)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Earth];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        for (var i = 0; i < DynamicVars.Repeat.BaseValue; i++)
        {
            var hasElement =
                cardPlay.Target.GetPowerAmount<FireElement>() > 0
                || cardPlay.Target.GetPowerAmount<WaterElement>() > 0
                || cardPlay.Target.GetPowerAmount<EarthElement>() > 0;

            if (hasElement)
                await Utils.GivePower<EarthElement>(this, cardPlay);
        }

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1);
    }
}
