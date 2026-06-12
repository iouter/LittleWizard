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
        var maxUpgrades = DynamicVars.Cards.IntValue;
        if (maxUpgrades <= 0)
            return;

        var upgradableIndices = new List<int>();
        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards[i].IsUpgradable)
                upgradableIndices.Add(i);
        }

        var upgradedCards = new List<CardModel>();
        var rng = Owner.RunState.Rng.CombatCardSelection;
        for (int i = 0; i < maxUpgrades && upgradableIndices.Count > 0; i++)
        {
            int randPos = rng.NextInt(upgradableIndices.Count);
            int cardIndex = upgradableIndices[randPos];
            var card = handCards[cardIndex];
            CardCmd.Upgrade(card);
            upgradedCards.Add(card);
            upgradableIndices.RemoveAt(randPos);
        }

        if (upgradedCards.Count > 0)
            CardCmd.Preview(upgradedCards, time: 1.0f, CardPreviewStyle.HorizontalLayout);

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Cards.UpgradeValueBy(2);
    }
}
