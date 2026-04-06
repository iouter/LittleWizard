using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Rare;

public class ThornGrowth()
    : LittleWizardCard(0, CardType.Skill, CardRarity.Rare, TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new HpLossVar(2), new PowerVar<ThornsPower>(4), new BlockVar(5, ValueProp.Move)];

    public override CardMultiplayerConstraint MultiplayerConstraint =>
        CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (CombatState == null)
            return;
        foreach (
            var creature in CombatState
                .GetTeammatesOf(Owner.Creature)
                .Where(c => c is { IsAlive: true, IsPlayer: true })
        )
        {
            await CreatureCmd.Damage(
                choiceContext,
                creature,
                DynamicVars.HpLoss.BaseValue,
                ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move,
                this
            );
            await PowerCmd.Apply<ThornsPower>(
                creature,
                DynamicVarsHelper.GetPowerVar<ThornsPower>(DynamicVars).IntValue,
                Owner.Creature,
                this
            );
            await CreatureCmd.GainBlock(creature, DynamicVars.Block, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<ThornsPower>(DynamicVars).UpgradeValueBy(3);
        DynamicVars.Block.UpgradeValueBy(2);
    }
}
