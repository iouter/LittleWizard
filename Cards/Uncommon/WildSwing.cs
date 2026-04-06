using BaseLib.Utils;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class WildSwing()
    : LittleWizardCard(0, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<VulnerablePower>(1), new DamageVar(7, ValueProp.Move), new RepeatVar(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await PowerCmd.Apply<VulnerablePower>(
            Owner.Creature,
            DynamicVars.Vulnerable.BaseValue,
            Owner.Creature,
            this
        );
        await CommonActions
            .CardAttack(this, play, DynamicVars.Repeat.IntValue + ResolveEnergyXValue())
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1);
    }
}
