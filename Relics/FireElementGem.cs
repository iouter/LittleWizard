using LittleWizard.Api.Relics;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Relics;

public sealed class FireElementGem : LittleWizardRelics
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FireElement>(1M)];

    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        CombatState combatState
    )
    {
        if (side != Owner.Creature.Side || combatState.RoundNumber > 1)
            return;
        Flash();
        await PowerCmd.Apply<FireElement>(
            combatState.HittableEnemies,
            DynamicVars["FireElement"].BaseValue,
            Owner.Creature,
            null
        );
    }
}
