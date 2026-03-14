using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class RockBlast() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        
        var hasElement = cardPlay.Target.GetPowerAmount<FireElement>() > 0 ||
                        cardPlay.Target.GetPowerAmount<WaterElement>() > 0 ||
                        cardPlay.Target.GetPowerAmount<EarthElement>() > 0;
        
        if (hasElement)
        {
            await PowerCmd.Apply<EarthElement>(cardPlay.Target, 5, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Apply 5 earth element after element combination triggers
    }
}
