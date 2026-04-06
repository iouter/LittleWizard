using LittleWizard.Api;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Cards.Common;

public class RemoveDefense()
    : LittleWizardCard(2, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<VulnerablePower>(4)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        if (cardPlay.Target.Block > 0)
            await CreatureCmd.LoseBlock(cardPlay.Target, cardPlay.Target.Block);
        await Utils.GivePower<VulnerablePower>(this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        // 变为 3 能量
        EnergyCost.UpgradeBy(-1);
    }
}
