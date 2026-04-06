using BaseLib.Utils;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Rare;

public class NotMyTime()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(3, ValueProp.Move), new CardsVar(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        if (Owner.Creature.Player == null)
            return;

        for (var i = 0; i < DynamicVars.Cards.IntValue; i++)
        {
            var card = Owner.Creature.Player.RunState.Rng.CombatCardSelection.NextItem(
                PileType.Exhaust.GetPile(Owner).Cards
            );
            if (card == null)
                continue;
            await CardCmd.AutoPlay(choiceContext, card, Owner.Creature);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}
