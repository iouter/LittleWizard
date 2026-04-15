using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using LittleWizard.Character;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LittleWizard.Api.Cards;

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
}
