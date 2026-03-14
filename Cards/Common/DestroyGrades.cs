using LittleWizard.Api;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.Cards.Common;

public class DestroyGrades() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<FireElement>(1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Innate
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<FireElement>(this, cardPlay);
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
        var card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).FirstOrDefault();
        if (card == null)
            return;
        await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}