using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.LittleWizardCode.Api.Audios;
using LittleWizard.LittleWizardCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LittleWizard.LittleWizardCode.Api.Cards;

[Pool(typeof(LittleWizardCardPool))]
public abstract class LittleWizardCard(
    int baseCost,
    CardType type,
    CardRarity rarity,
    TargetType target,
    bool showInCardLibrary = true,
    bool autoAdd = true
) : CustomCardModel(baseCost, type, rarity, target, showInCardLibrary, autoAdd)
{
    public override string? CustomPortraitPath =>
        $"res://{MainFile.ModId}/images/card_portraits/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png";

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Type == CardType.Attack)
            AudioHelper.PlayOnAttack(cardPlay.Card);
        return base.BeforeCardPlayed(cardPlay);
    }
}
