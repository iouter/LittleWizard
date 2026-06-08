using BaseLib.Extensions;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class ElementDraw()
    : LittleWizardCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new EnergyVar(2), new PowerVar<BaseElement>(10)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        var targetAmount = DynamicVars.Power<BaseElement>().BaseValue;
        var target = cardPlay.Target;
        var fire = target.GetPowerAmount<FireElement>();
        var water = target.GetPowerAmount<WaterElement>();
        var earth = target.GetPowerAmount<EarthElement>();
        if (fire > 0)
        {
            var fireTarget = Math.Min(fire, targetAmount);
            await PowerCmd.Apply<FireElement>(
                choiceContext,
                target,
                -fireTarget,
                Owner.Creature,
                this
            );
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        }
        else if (water > 0)
        {
            var waterTarget = Math.Min(water, targetAmount);
            await PowerCmd.Apply<WaterElement>(
                choiceContext,
                target,
                -waterTarget,
                Owner.Creature,
                this
            );
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        }
        else if (earth > 0)
        {
            var earthTarget = Math.Min(earth, targetAmount);
            await PowerCmd.Apply<EarthElement>(
                choiceContext,
                target,
                -earthTarget,
                Owner.Creature,
                this
            );
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        }
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}
