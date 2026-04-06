using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Cards.Uncommon;

public class WaterVapour()
    : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WaterVapourPower>(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target is null)
            return;

        var artifact = target.GetPowerAmount<ArtifactPower>();
        if (artifact > 0)
        {
            await PowerCmd.Remove<ArtifactPower>(target);
            await PowerCmd.Apply<StrengthPower>(target, -1 * artifact, Owner.Creature, this);
        }

        await Utils.GivePower<WaterVapourPower>(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVarsHelper.GetPowerVar<WaterVapourPower>(DynamicVars).UpgradeValueBy(1);
    }
}
