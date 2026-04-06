using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Common;

public class ShortCircuitWaterBall()
    : LittleWizardCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(7, ValueProp.Move), new PowerVar<WaterElement>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await Utils.GivePower<WaterElement>(this, cardPlay);
        if (cardPlay.Target != null)
            await PowerCmd.Remove<ArtifactPower>(cardPlay.Target);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVarsHelper.GetPowerVar<WaterElement>(DynamicVars).UpgradeValueBy(1);
    }
}
