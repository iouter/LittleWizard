using BaseLib.Abstracts;
using LittleWizard.Powers.Elements.Reacts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LittleWizard.Powers.Elements;

public class ElementTemporaryStrengthPower : CustomTemporaryPowerModel
{
	protected override Func<Creature, decimal, Creature?, CardModel?, bool, Task> ApplyPowerFunc =>
		async (creature, amount, applier, boolean, task) =>
			await PowerCmd.Apply<StrengthPower>(creature, -amount, applier, boolean, task);

	public override string CustomPackedIconPath =>
		"res://LittleWizard/images/powers/element_temporary_strength_power.png";
	public override string CustomBigIconPath =>
		"res://LittleWizard/images/powers/big/element_temporary_strength_power.png";
	public override PowerModel InternallyAppliedPower => ModelDb.Power<StrengthPower>();
	public override AbstractModel OriginModel => ModelDb.Power<FireAndWaterElementReactorPower>();
}
