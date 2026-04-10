using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Animation;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Rare;

public class CelestialRockSpell()
    : LittleWizardCard(2, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy, true, true)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(48, ValueProp.Move), new PowerVar<FireElement>(10)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);

        Creature target = cardPlay.Target;

        if (target != null)
        {
            decimal fireAmount = DynamicVars["FireElement"].BaseValue;
            await PowerCmd.Apply<FireElement>(target, fireAmount, Owner.Creature, this, false);
        }

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        BaseReplayCount += 1;
    }
}
