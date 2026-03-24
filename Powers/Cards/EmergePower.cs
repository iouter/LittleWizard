using LittleWizard.Api.Interface;
using LittleWizard.Api.Powers;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace LittleWizard.Powers.Cards;

public class EmergePower : LittleWizardPower, IAfterElementReactor
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public async Task AfterElementReact(Creature owner, Creature target)
    {
        if (owner != Owner) return;
        await ElementHelper.RandomElement(target, Amount, owner, null);
    }
}