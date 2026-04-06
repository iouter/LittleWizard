using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class MorningGrumpiness()
    : LittleWizardCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BlockVar(20, ValueProp.Move), new PowerVar<MorningGrumpinessPower>(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await Utils.GivePower<MorningGrumpinessPower>(this, play);
        if (Owner.Creature.Player != null)
            PlayerCmd.EndTurn(Owner.Creature.Player, true);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
