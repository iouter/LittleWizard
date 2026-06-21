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
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class EarthFury()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(0),
            new ExtraDamageVar(1),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
                (card, _) => card.Owner?.Creature?.Block / 2 ?? 0
            ),
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Earth];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null)
            return;

        var ownerCreature = Owner.Creature;
        if (ownerCreature == null)
            return;

        int halfBlock = Owner.Creature.Block / 2;
        if (halfBlock <= 0)
            return;

        await AnimationHelper.TriggerCastAnimationOwner(this);

        await CreatureCmd.Damage(
            choiceContext,
            target,
            halfBlock,
            ValueProp.Move,
            Owner.Creature,
            this
        );

        await PowerCmd.Apply<EarthElement>(choiceContext, target, halfBlock, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
