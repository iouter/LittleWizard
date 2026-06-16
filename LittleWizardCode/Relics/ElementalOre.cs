using LittleWizard.LittleWizardCode.Api;
using LittleWizard.LittleWizardCode.Api.Relics;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.LittleWizardCode.Relics;

public class ElementalOre : AfterElementReactRelics
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<DrawCardsNextTurnPower>(1)];

    protected override async Task AfterElementReact(
        PlayerChoiceContext ctx,
        Creature owner,
        decimal amount,
        Creature? applier,
        CardModel? cardSource
    )
    {
        if (Owner.Creature != owner || Status != RelicStatus.Active)
        {
            return;
        }
        Flash();
        await Utils.Apply<DrawCardsNextTurnPower>(this, Owner.Creature, ctx);
        Status = RelicStatus.Normal;
    }

    public override Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
            return Task.CompletedTask;
        Status = RelicStatus.Active;
        return Task.CompletedTask;
    }

    public override RelicModel GetUpgradeReplacement() => ModelDb.Relic<ElementalGem>();
}
