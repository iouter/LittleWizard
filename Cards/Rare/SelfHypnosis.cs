using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Rare;

public class SelfHypnosis() : LittleWizardCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(19, ValueProp.Move),
        new PowerVar<MegaCrit.Sts2.Core.Powers.Vulnerable>(3),
        new PowerVar<MegaCrit.Sts2.Core.Powers.Weaken>(3)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await Utils.GivePower<MegaCrit.Sts2.Core.Powers.Vulnerable>(this, cardPlay, DynamicVars.Vulnerable.BaseValue);
        await Utils.GivePower<MegaCrit.Sts2.Core.Powers.Weaken>(this, cardPlay, DynamicVars.Weak.BaseValue);
        
        // Add or remove Ethereal from up to 4 cards in hand
        var handCards = Owner.Player.Hand.Cards.ToList();
        var maxCards = Math.Min(4, handCards.Count);
        for (int i = 0; i < maxCards; i++)
        {
            var card = handCards[i];
            if (card.Keywords.Contains(CardKeyword.Ethereal))
            {
                card.RemoveKeyword(CardKeyword.Ethereal);
            }
            else
            {
                card.AddKeyword(CardKeyword.Ethereal);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}
