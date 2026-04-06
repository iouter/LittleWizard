using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Rare;

public class SelfHypnosis()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(17, ValueProp.Move),
            new PowerVar<VulnerablePower>(3),
            new PowerVar<WeakPower>(3),
            new CardsVar(3),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await Utils.GivePower<VulnerablePower>(this, cardPlay);
        await Utils.GivePower<WeakPower>(this, cardPlay);

        var cards = await CommonActions.SelectCards(
            this,
            SelectionScreenPrompt,
            choiceContext,
            PileType.Hand,
            0,
            DynamicVars.Cards.IntValue
        );
        foreach (var card in cards)
            if (card.Keywords.Contains(CardKeyword.Ethereal))
                card.RemoveKeyword(CardKeyword.Ethereal);
            else
                card.AddKeyword(CardKeyword.Ethereal);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}
