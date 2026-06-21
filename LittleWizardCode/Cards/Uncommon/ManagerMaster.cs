using BaseLib.Abstracts;
using BaseLib.Utils;
using HarmonyLib;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Events;

namespace LittleWizard.LittleWizardCode.Cards.Uncommon;

[HarmonyPatch(typeof(TrashHeap), nameof(TrashHeap.Cards), MethodType.Getter)]
public class ManagerMaster()
    : LittleWizardCard(3, CardType.Power, CardRarity.Event, TargetType.Self),
        ITrashHeapCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<ManagerMasterPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.Apply<ManagerMasterPower>(choiceContext, this, cardPlay);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
