using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using LittleWizard.LittleWizardCode.Api.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.LittleWizardCode.Powers.Cards;

public class ManaShieldPower : LittleWizardPower, IHasSecondAmount
{
    private const string PlayerTurnStart = "PlayerTurnStart";
    private const string CardPlayed = "CardPlayed";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new(PlayerTurnStart + "Base", 0),
            new(PlayerTurnStart + "Extra", 3),
            new CustomCalculatedVar(PlayerTurnStart).WithMultiplier((power, _) => power.Amount),
            new(CardPlayed + "Base", 0),
            new(CardPlayed + "Extra", 2),
            new CustomCalculatedVar(CardPlayed).WithMultiplier((power, _) => power.Amount),
        ];

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player
    )
    {
        if (Owner != player.Creature)
            return;

        await CreatureCmd.GainBlock(Owner, 3 * Amount, ValueProp.Move, null);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player)
            return;

        var card = cardPlay.Card;

        if (ElementHelper.IsElementCard(card))
        {
            await CreatureCmd.GainBlock(Owner, 2 * Amount, ValueProp.Move, cardPlay);
        }
    }

    public override int DisplayAmount =>
        (int)((CustomCalculatedVar)DynamicVars[PlayerTurnStart]).CalculateCustom(null);

    public string GetSecondAmount() =>
        $"{((CustomCalculatedVar)DynamicVars[CardPlayed]).CalculateCustom(null)}";
}
