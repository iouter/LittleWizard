using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class ExploreTaboo() : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self), IElementCard
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<FireElement>(Owner.Creature, 3, Owner.Creature, this);
        await PowerCmd.Apply<ExploreTabooPower>(Owner.Creature, 1, Owner.Creature, this);
    }

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override void OnUpgrade()
    {
        // Upgrade: Gain 2 energy instead of 1
    }
}

public class ExploreTabooPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task OnTurnStart()
    {
        await PlayerCmd.GainEnergy(1, Owner);
    }
}
