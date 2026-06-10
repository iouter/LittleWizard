using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Cards.Common;

public class AnvilTransfer()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(9, ValueProp.Move), new CardsVar(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (Owner.Creature.Player == null || Owner.PlayerCombatState == null)
            return;

        var handCards = Owner.PlayerCombatState.Hand.Cards;
        var upgradableCards = handCards.Where(c => c.IsUpgradable).ToList();
        var upgradedCards = new List<CardModel>();

        for (int i = 0; i < (int)DynamicVars.Cards.BaseValue && upgradableCards.Count > 0; i++)
        {
            var card = Owner.Creature.Player.RunState.Rng.CombatCardSelection.NextItem(
                upgradableCards
            );
            if (card != null)
            {
                CardCmd.Upgrade(card);
                upgradedCards.Add(card);
                upgradableCards.Remove(card);
            }
        }

        if (upgradedCards.Count > 0)
        {
            CardCmd.Preview(upgradedCards, time: 1.0f, CardPreviewStyle.HorizontalLayout);
        }

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Cards.UpgradeValueBy(2);
    }
}
