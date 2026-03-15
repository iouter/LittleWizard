using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Powers.Cards;

public class EmergePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task OnElementReactionTriggered(Creature target, ElementReaction reaction)
    {
        // When an enemy triggers an element reaction, give them 4 stacks of random element
        var randomElement = GetRandomElement();
        await ApplyElementStacks(target, randomElement, 4);
    }

    private ElementType GetRandomElement()
    {
        var elements = new[] { ElementType.Fire, ElementType.Water, ElementType.Earth };
        return elements[new Random().Next(elements.Length)];
    }

    private async Task ApplyElementStacks(Creature target, ElementType element, int stacks)
    {
        // Apply element stacks to target
        // Implementation depends on your element system
        await PowerCmd.Apply<ElementPower>(target, stacks, Owner.Creature, null);
    }
}
