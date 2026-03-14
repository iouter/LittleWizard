using LittleWizard.Api;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class ManaBurst() : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<ManaBurstPower>(Owner.Creature, 4, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Every 3 skill cards instead of 4
    }
}

public class ManaBurstPower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private int _skillCardsPlayed = 0;

    public override async Task AfterCardPlay(CardModel card)
    {
        if (card.Type == CardType.Skill && card.Owner == Owner)
        {
            _skillCardsPlayed++;
            if (_skillCardsPlayed >= Amount)
            {
                await PlayerCmd.GainEnergy(1, Owner);
                _skillCardsPlayed = 0;
            }
        }
    }
}
