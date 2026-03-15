using LittleWizard.Api;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare;

public class TidalWave() : LittleWizardCard(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(36, ValueProp.Move),
        new PowerVar<MegaCrit.Sts2.Core.Powers.Stun>(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Check if target has at least 6 Water Element stacks
        var target = cardPlay.Target;
        if (target != null)
        {
            var waterPower = target.GetPower<WaterElement>();
            if (waterPower != null && waterPower.Stacks >= 6)
            {
                await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
                await Utils.GivePower<MegaCrit.Sts2.Core.Powers.Stun>(this, cardPlay);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(12);
    }
}
