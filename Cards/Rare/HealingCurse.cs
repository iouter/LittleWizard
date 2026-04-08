using System.Diagnostics;
using LittleWizard.Api.Animation;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class HealingCurse()
    : LittleWizardCard(3, CardType.Skill, CardRarity.Rare, TargetType.AnyAlly)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(10)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override CardMultiplayerConstraint MultiplayerConstraint =>
        CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Debug.Assert(cardPlay.Target != null);
        await CreatureCmd.Heal(cardPlay.Target, DynamicVars.Heal.IntValue);
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
