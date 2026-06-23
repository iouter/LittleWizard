using BaseLib.Abstracts;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Cards;
using LittleWizard.LittleWizardCode.Character;
using LittleWizard.LittleWizardCode.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace LittleWizard.LittleWizardCode.Cards.Others;

[Pool(typeof(EventCardPool))]
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

    public override CardPoolModel VisualCardPool => ModelDb.CardPool<LittleWizardCardPool>();
}
