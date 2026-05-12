using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class ForbiddenSoulBinding()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(15),
            new ExtraDamageVar(1),
            new CalculatedDamageVar(ValueProp.Unblockable).WithMultiplier(
                (_, target) => target?.Block ?? 0
            ),
        ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Ethereal, CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
            return;
        await CreatureCmd.Damage(
            choiceContext,
            cardPlay.Target,
            DynamicVars.CalculatedDamage.Calculate(cardPlay.Target),
            ValueProp.Unblockable,
            this
        );
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}
