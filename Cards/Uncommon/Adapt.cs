using BaseLib.Utils;
using LittleWizard.Api.Animation;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class Adapt() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(1), new EnergyVar(2), new BlockVar(13, ValueProp.Move)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Innate];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        var fireAmount = cardPlay.Target.GetPowerAmount<FireElement>();
        var waterAmount = cardPlay.Target.GetPowerAmount<WaterElement>();
        var earthAmount = cardPlay.Target.GetPowerAmount<EarthElement>();

        if (fireAmount > 0)
        {
            await CommonActions.Draw(this, choiceContext);
            await CommonActions.Draw(this, choiceContext);
        }
        else if (waterAmount > 0)
        {
            if (Owner.Creature.Player != null)
                await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner.Creature.Player);
        }
        else if (earthAmount > 0)
            await CommonActions.CardBlock(this, cardPlay);
        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
