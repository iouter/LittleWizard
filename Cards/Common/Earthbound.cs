using BaseLib.Utils;
using LittleWizard.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Common;

public class Earthbound() : LittleWizardCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    private const string CalculatedEarthElement = "CalculatedEarthElement";
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(6),
        new CalculationExtraVar(1),
        new CalculatedVar(CalculatedEarthElement).WithMultiplier((card, target) => target?.GetPowerAmount<EarthElement>() ?? 0)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await CreatureCmd.GainBlock(cardPlay.Target,
            ((CalculatedVar)DynamicVars[CalculatedEarthElement]).Calculate(cardPlay.Target), ValueProp.Move, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}