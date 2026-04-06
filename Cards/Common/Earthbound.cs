using BaseLib.Utils;
using LittleWizard.Api.Cards;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Common;

public class Earthbound()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(6),
            new CalculationExtraVar(1),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(
                (card, target) => target?.GetPowerAmount<EarthElement>() ?? 0
            ),
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}
