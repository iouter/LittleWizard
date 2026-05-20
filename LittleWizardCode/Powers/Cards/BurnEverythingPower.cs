using LittleWizard.LittleWizardCode.Api.Powers;
using LittleWizard.LittleWizardCode.Powers.Elements;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class BurnEverythingPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Side)
            return;
        await PowerCmd.Apply<FireElement>(Owner, Amount, Owner, null);
    }
}
