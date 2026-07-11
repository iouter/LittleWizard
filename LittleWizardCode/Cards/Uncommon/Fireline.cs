using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Api.Extensions;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

public class Fireline()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    private const string FirelineFireElement = "FirelineFireElement";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new PowerVar<FireElement>(1),
            new CalculationBaseVar(1),
            new CalculationExtraVar(1),
            new CalculatedVar(FirelineFireElement).WithMultiplier(
                (card, _) => PileType.Exhaust.GetPile(card.Owner).Cards.Count
            ),
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipsValue.Fire];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var cardToExhaust = await CommonActions.SelectSingleCard(
            this,
            CardSelectorPrefs.ExhaustSelectionPrompt,
            choiceContext,
            PileType.Hand
        );
        if (cardToExhaust != null)
        {
            await CardCmd.Exhaust(choiceContext, cardToExhaust);
        }

        await PowerCmd.Apply<FireElement>(
            choiceContext,
            CombatState!.HittableEnemies,
            ((CalculatedVar)DynamicVars[FirelineFireElement]).Calculate(null),
            Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
