using BaseLib.Extensions;
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

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class IceBlock()
    : LittleWizardCard(3, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WaterElement>(6)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Water];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        var waterAmount = cardPlay.Target.GetPowerAmount<WaterElement>();
        var targetAmount = DynamicVars.Power<WaterElement>().BaseValue;
        if (waterAmount >= targetAmount)
        {
            await PowerCmd.Apply<WaterElement>(
                choiceContext,
                cardPlay.Target,
                -targetAmount,
                Owner.Creature,
                this
            );
            await CreatureCmd.Stun(cardPlay.Target);
        }

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
