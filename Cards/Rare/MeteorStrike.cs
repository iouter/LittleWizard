using LittleWizard.Api;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class MeteorStrike() : LittleWizardCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(48, ValueProp.Move),
        new PowerVar<FireElement>(10)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Deal damage to a random enemy
        var randomEnemy = Owner.CombatState?.HittableEnemies.FirstOrDefault();
        if (randomEnemy != null)
        {
            await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
            await Utils.GivePower<FireElement>(this, cardPlay);
            
            // Play the card a second time
            await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
            await Utils.GivePower<FireElement>(this, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade handled by playing twice
    }
}
