using System.Diagnostics.CodeAnalysis;
using LittleWizard.LittleWizardCode.Api.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace LittleWizard.LittleWizardCode.Relics;

public class UndeadEssence : LittleWizardRelics
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(1)];
    private int _resurrectionCount;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SavedProperty]
    public int ResurrectionCount
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

    public override bool ShowCounter => true;

    public override Task AfterCombatEnd(CombatRoom room)
    {
        ResurrectionCount += 1;
        Flash();
        return base.AfterCombatEnd(room);
    }

    public override bool ShouldDieLate(Creature creature)
    {
        return creature != Owner.Creature || ResurrectionCount <= 0;
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
