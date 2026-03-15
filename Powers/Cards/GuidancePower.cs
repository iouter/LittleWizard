using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Powers.Cards;

public class GuidancePower : LittleWizardPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private Creature? _markedEnemy;

    public override async Task AfterTurnStart(PlayerChoiceContext choiceContext, MegaCrit.Sts2.Core.Entities.Players.Player player)
    {
        if (Owner != player.Creature) return;
        
        // Randomly mark an enemy
        var enemies = player.Creature.CombatState?.Enemies.Where(e => e.IsAlive).ToList();
        if (enemies != null && enemies.Count > 0)
        {
            var random = new Random();
            _markedEnemy = enemies[random.Next(enemies.Count)];
        }
    }

    public override async Task ModifyDamageTaken(PlayerChoiceContext choiceContext, ref DamageResult result, ValueProp props, Creature attacker)
    {
        if (_markedEnemy == attacker)
        {
            // Increase damage taken by marked enemy by 30% (or 20% if ethereal)
            int multiplier = HasKeyword(CardKeyword.Ethereal) ? 130 : 120;
            result.BaseValue = result.BaseValue * multiplier / 100;
        }
    }

    public override async Task AfterCombatEnd()
    {
        _markedEnemy = null;
    }
}
