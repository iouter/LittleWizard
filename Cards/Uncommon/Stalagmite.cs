using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Utils;
using LittleWizard.Api;
using LittleWizard.Api.Cards;
using LittleWizard.Api.DynamicVars;
using LittleWizard.Api.Extensions;
using LittleWizard.Powers.Elements;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LittleWizard.Cards.Uncommon
{
    public class Stalagmite : LittleWizardCard
    {
        public Stalagmite()
            : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true, true) { }

        protected override HashSet<CardTag> CanonicalTags
        {
            get { return new HashSet<CardTag> { CardTagExtensions.LittleWizardElement }; }
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new List<DynamicVar>(
                    new DynamicVar[]
                    {
                        new BlockVar(7, ValueProp.Move),
                        new DamageVar(3, ValueProp.Move),
                        new PowerVar<EarthElement>(3),
                    }
                );
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CommonActions.CardBlock(this, cardPlay);
            await CommonActions
                .CardAttack(this, cardPlay, 1, null, null, null)
                .Execute(choiceContext);
            await Utils.GivePower<EarthElement>(this, cardPlay);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(1);
            DynamicVarsHelper.GetPowerVar<EarthElement>(base.DynamicVars).UpgradeValueBy(2);
        }
    }
}
