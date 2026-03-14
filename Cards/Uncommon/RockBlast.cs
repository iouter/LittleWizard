using LittleWizard.Api;
using LittleWizard.Cards.Interface;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Uncommon;

public class RockBlast() : LittleWizardCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy), IElementCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<EarthElement>(5),
        new RepeatVar(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        for (var i = 0; i < DynamicVars.Repeat.BaseValue; i++)
        {
            var hasElement = cardPlay.Target.GetPowerAmount<FireElement>() > 0 ||
                                    cardPlay.Target.GetPowerAmount<WaterElement>() > 0 ||
                                    cardPlay.Target.GetPowerAmount<EarthElement>() > 0;
                    
                    if (hasElement)
                    {
                        await Utils.GivePower<EarthElement>(this, cardPlay);
                    }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1);
    }
}
