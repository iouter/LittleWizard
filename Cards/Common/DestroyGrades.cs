using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class DestroyGrades()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FireElement>(1)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Innate];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await Utils.GivePower<FireElement>(this, cardPlay);
        var card = await CommonActions.SelectSingleCard(
            this,
            CardSelectorPrefs.ExhaustSelectionPrompt,
            choiceContext,
            PileType.Hand
        );
        if (card == null)
            return;
        await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
