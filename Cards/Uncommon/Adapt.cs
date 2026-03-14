using LittleWizard.Api;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class Adapt() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(8, ValueProp.Move),
        new PowerVar<FireElement>(1),
        new BlockVar(13, ValueProp.Move)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Innate
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        
        var fireAmount = cardPlay.Target.GetPowerAmount<FireElement>();
        var waterAmount = cardPlay.Target.GetPowerAmount<WaterElement>();
        var earthAmount = cardPlay.Target.GetPowerAmount<EarthElement>();

        if (fireAmount > 0)
        {
            await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
            await Utils.GivePower<FireElement>(this, cardPlay);
        }
        
        if (waterAmount > 0)
        {
            await PlayerCmd.GainEnergy(2, Owner);
        }
        
        if (earthAmount > 0)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        // Innate is already set
    }
}
