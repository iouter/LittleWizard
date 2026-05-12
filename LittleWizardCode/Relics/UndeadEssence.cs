using LittleWizard.LittleWizardCode.Api.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace LittleWizard.LittleWizardCode.Relics;

public class UndeadEssence : LittleWizardRelics
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(1)];
    private int _resurrectionCount;

    private int ResurrectionCount
    {
        get => _resurrectionCount;
        set
        {
            if (value == _resurrectionCount)
            {
                return;
            }
            AssertMutable();
            _resurrectionCount = value;
            InvokeDisplayAmountChanged();
        }
    }

    public override int DisplayAmount => ResurrectionCount;

    public override Task AfterCombatEnd(CombatRoom room)
    {
        ResurrectionCount += 1;
        return base.AfterCombatEnd(room);
    }

    public override async Task AfterPreventingDeath(Creature creature)
    {
        if (ResurrectionCount <= 0)
        {
            return;
        }
        Flash();
        ResurrectionCount -= 1;
        await CreatureCmd.Heal(creature, DynamicVars.Heal.BaseValue);
    }
}
