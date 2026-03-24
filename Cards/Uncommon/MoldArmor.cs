using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon;

public class MoldArmor() : LittleWizardCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(8, ValueProp.Move),
        new PowerVar<EarthElement>(6)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

        var earthAmount = cardPlay.Target.GetPowerAmount<EarthElement>();
        if (earthAmount >= DynamicVarsHelper.GetPowerVar<EarthElement>(DynamicVars).BaseValue)
        {
            await PowerCmd.Remove<EarthElement>(cardPlay.Target);
            await CreatureCmd.GainBlock(Owner.Creature, cardPlay.Target.Block, ValueProp.Unpowered, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        AddKeyword(CardKeyword.Retain);
    }
}