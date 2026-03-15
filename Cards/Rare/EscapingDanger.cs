using LittleWizard.Api;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class EscapingDanger() : LittleWizardCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3, ValueProp.Move)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Deal 3 damage to self
        await CommonActions.DamageSelf(this, 3).Execute(choiceContext);
        
        // Remove all debuffs from self
        var debuffs = Owner.Creature.Powers.Where(p => p.IsDebuff).ToList();
        foreach (var debuff in debuffs)
        {
            await Owner.Creature.RemovePower(debuff);
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade removes Exhaust keyword (already not exhaust by default)
    }
}
