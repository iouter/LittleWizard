using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class HealingCurse()
    : LittleWizardCard(3, CardType.Skill, CardRarity.Rare, TargetType.AnyAlly)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(10)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override CardMultiplayerConstraint MultiplayerConstraint =>
        CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.Heal(cardPlay.Target!, DynamicVars.Heal.IntValue);
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
