using MegaCrit.Sts2.Core.Entities.Creatures;

namespace LittleWizard.Api.Interface;

public interface IAfterElementReactor
{
    Task AfterElementReact(Creature owner, Creature target);
}