using LittleWizard.Api.Cards;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LittleWizard.Cards.Uncommon;

public class Spread()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var target = play.Target;
        if (CombatState == null || target == null)
            return;
        var enemies = CombatState.HittableEnemies;
        foreach (var enemy in enemies)
        {
            if (target == enemy)
                continue;

            var fire = target.GetPowerAmount<FireElement>();
            var water = target.GetPowerAmount<WaterElement>();
            var earth = target.GetPowerAmount<EarthElement>();
            if (fire > 0)
                await PowerCmd.Apply<FireElement>(enemy, fire, Owner.Creature, this);
            else if (water > 0)
                await PowerCmd.Apply<WaterElement>(enemy, water, Owner.Creature, this);
            else if (earth > 0)
                await PowerCmd.Apply<EarthElement>(enemy, earth, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
