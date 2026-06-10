using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Rare;

public class NotMyTime()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(3, ValueProp.Move), new CardsVar(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await AnimationHelper.TriggerCastAnimationOwner(this);
        if (Owner.Creature.Player == null)
            return;

        var exhaustPile = PileType.Exhaust.GetPile(Owner);
        var attackCards = exhaustPile
            .Cards.Where(c =>
                !c.Keywords.Contains(CardKeyword.Unplayable) && c.Type == CardType.Attack
            )
            .ToList();

        if (attackCards.Count == 0)
            return;

        int actualCount = (int)Math.Min(DynamicVars.Cards.BaseValue, attackCards.Count);

        var rng = Owner.RunState.Rng.CombatCardSelection;
        var shuffled = attackCards.OrderBy(_ => rng.NextInt()).ToList();
        var toPlay = shuffled.Take(actualCount).ToList();

        foreach (var card in toPlay)
        {
            await CardCmd.AutoPlay(choiceContext, card, cardPlay.Target);

            if (card.Pile?.Type != PileType.Exhaust)
            {
                await CardPileCmd.Add(card, PileType.Exhaust, CardPilePosition.Bottom);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}
