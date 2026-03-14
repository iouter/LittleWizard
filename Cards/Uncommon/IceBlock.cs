using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class IceBlock() : LittleWizardCard(3, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        
        var waterAmount = cardPlay.Target.GetPowerAmount<WaterElement>();
        if (waterAmount >= 7)
        {
            await PowerCmd.Remove<WaterElement>(cardPlay.Target);
            CreatureCmd.Stun(cardPlay.Target);
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Requires 6 water element instead of 7
    }
}
