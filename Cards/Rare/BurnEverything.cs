using System.Diagnostics;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Cards;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class BurnEverything()
    : LittleWizardCard(0, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTagExtensions.LittleWizardElement];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<FireElement>(4), new PowerVar<BurnEverythingPower>(4)];

    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var xValue =
            DynamicVarsHelper.GetPowerVar<FireElement>(DynamicVars).BaseValue
            * ResolveEnergyXValue();
        Debug.Assert(CombatState != null, nameof(CombatState) + " != null");
        foreach (var enemy in CombatState.Enemies)
            await PowerCmd.Apply<FireElement>(enemy, xValue, Owner.Creature, this);
        await Utils.GivePower<BurnEverythingPower>(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<FireElement>(DynamicVars).UpgradeValueBy(1);
    }
}
