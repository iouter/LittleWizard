using LittleWizard.Api;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class NotMyTime() : LittleWizardCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        
        // Play 3 random attack cards from exhaust pile
        var exhaustAttacks = Owner.Player.Exhaust.Pile.Where(c => c is AttackCardModel).ToList();
        var randomAttacks = exhaustAttacks.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
        
        foreach (var attack in randomAttacks)
        {
            await CommonActions.PlayCard(attack, cardPlay).Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        // Already plays 3 attacks by default
    }
}
