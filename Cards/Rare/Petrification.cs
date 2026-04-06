using BaseLib.Utils;
using LittleWizard.Api.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Rare;

public class Petrification() : LittleWizardCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    private const string IncreasedBlock = "IncreasedBlock";
    private decimal _extraBlockFromPlays;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BlockVar(6, ValueProp.Move), new(IncreasedBlock, 6)];

    private decimal ExtraBlockFromPlays
    {
        get => _extraBlockFromPlays;
        set
        {
            AssertMutable();
            _extraBlockFromPlays = value;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        if (Owner.PlayerCombatState == null)
            return;
        var played = Owner.PlayerCombatState.AllCards.OfType<Petrification>();
        foreach (var play in played)
            play.BuffFromPlay(DynamicVars[IncreasedBlock].BaseValue);
    }

    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        var block = DynamicVars.Block;
        block.BaseValue += ExtraBlockFromPlays;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2);
        DynamicVars[IncreasedBlock].UpgradeValueBy(2);
    }

    private void BuffFromPlay(decimal extraBlock)
    {
        var block = DynamicVars.Block;
        block.BaseValue += extraBlock;
        ExtraBlockFromPlays += extraBlock;
    }
}
