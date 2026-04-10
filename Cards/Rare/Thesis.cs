using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Powers.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LittleWizard.Cards.Rare
{
    public class Thesis : LittleWizardCard
    {
        public Thesis()
            : base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true, true) { }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get { return new List<DynamicVar> { new PowerVar<ThesisPower>(1) }; }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await Utils.GivePower<ThesisPower>(this, cardPlay);
        }

        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}
