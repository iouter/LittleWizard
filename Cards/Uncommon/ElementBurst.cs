using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class ElementBurst() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        
        var fireAmount = cardPlay.Target.GetPowerAmount<FireElement>();
        var waterAmount = cardPlay.Target.GetPowerAmount<WaterElement>();
        var earthAmount = cardPlay.Target.GetPowerAmount<EarthElement>();

        if (fireAmount > 0)
        {
            var bonus = (int)(fireAmount / 5) * 3;
            await PowerCmd.Apply<FireElement>(cardPlay.Target, 4 + bonus, Owner.Creature, this);
        }
        else if (waterAmount > 0)
        {
            var bonus = (int)(waterAmount / 5) * 3;
            await PowerCmd.Apply<WaterElement>(cardPlay.Target, 4 + bonus, Owner.Creature, this);
        }
        else if (earthAmount > 0)
        {
            var bonus = (int)(earthAmount / 5) * 3;
            await PowerCmd.Apply<EarthElement>(cardPlay.Target, 4 + bonus, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Give 5 base element and 4 extra per 5 layers
    }
}
