using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Animation;
using LittleWizard.LittleWizardCode.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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
        if (Owner.PlayerCombatState == null)
            return;

        var upgradeCount = DynamicVars.Cards.IntValue;
        var upgradableCards = Owner
            .PlayerCombatState.Hand.Cards.Where(c => c.IsUpgradable)
            .ToList();
        var upgradedCards = new List<CardModel>();
        var rng = Owner.RunState.Rng.CombatCardSelection;
        for (int i = 0; i < upgradeCount && upgradableCards.Count > 0; i++)
        {
            var card = rng.NextItem(upgradableCards);
            if (card == null)
                continue;
            CardCmd.Upgrade(card);
            upgradedCards.Add(card);
            upgradableCards.Remove(card);
        }

        CardCmd.Preview(upgradedCards, 1f);

        await AnimationHelper.TriggerCastAnimationOwner(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Cards.UpgradeValueBy(2);
    }
}
