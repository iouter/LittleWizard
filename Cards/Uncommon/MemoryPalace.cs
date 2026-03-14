using LittleWizard.Api;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class MemoryPalace() : LittleWizardCard(3, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Copy 5 cards from draw pile to hand
        // This needs specific API support for copying cards from draw pile
        await PowerCmd.Apply<MemoryPalacePower>(Owner.Creature, 5, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Copy 6 cards instead of 5
    }
}

public class MemoryPalacePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
}
