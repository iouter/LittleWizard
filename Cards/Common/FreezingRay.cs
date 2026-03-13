using BaseLib.Utils;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Common;

public class FreezingRay() : LittleWizardCard(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies), IElementCard
{
    private const string CalculatedWaterElement = "CalculatedWaterElement";
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(6),
        new CalculationExtraVar(1),
        new CalculatedVar(CalculatedWaterElement).WithMultiplier((card, target) => target?.GetPowerAmount<WaterElement>() ?? 0)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await CommonActions.CardAttack(this, cardPlay.Target, ((CalculatedVar) DynamicVars[CalculatedWaterElement]).Calculate(cardPlay.Target)).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}