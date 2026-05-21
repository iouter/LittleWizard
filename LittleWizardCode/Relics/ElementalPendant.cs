using LittleWizard.LittleWizardCode.Api.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LittleWizard.LittleWizardCode.Relics;

public class ElementalPendant : AfterElementReactRelics
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(3)];
    private bool _usedThisTurn;

    private bool UsedThisTurn
    {
        get => _usedThisTurn;
        set
        {
            if (_usedThisTurn == value)
            {
                return;
            }
            AssertMutable();
            _usedThisTurn = value;
        }
    }

    protected override async Task AfterElementReact(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (owner != Owner.Creature || UsedThisTurn)
        {
            return;
        }
        Flash();
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        UsedThisTurn = true;
    }
}
