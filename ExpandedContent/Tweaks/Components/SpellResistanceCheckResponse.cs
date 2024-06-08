using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("SpellResistanceCheckResponse")]

    public class SpellResistanceCheckResponse : UnitFactComponentDelegate, ITargetRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ISubscriber, ITargetRulebookSubscriber {

        public bool OnlyPass;
        public bool OnlyFail;
        public ActionList ActionOnSelf;
        public ActionList ActionOnCaster;
        public BlueprintAbilityReference[] m_Spells;
        public ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference> Spells => m_Spells;


        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt) {
        }

        public void OnEventDidTrigger(RuleSpellResistanceCheck evt) {
            if (CheckConditions(evt) && base.Fact.MaybeContext != null) {
                //using (base.Fact.MaybeContext?.GetDataScope(base.Owner)) {
                //    (base.Fact as IFactContextOwner)?.RunActionInContext(ActionOnSelf, base.Owner);
                //}
                (base.Fact as IFactContextOwner)?.RunActionInContext(ActionOnCaster, evt.Initiator);
                (base.Fact as IFactContextOwner)?.RunActionInContext(ActionOnSelf, evt.Target);


            }
        }

        public bool CheckConditions(RuleSpellResistanceCheck evt) {
            if (((OnlyPass && evt.IsSpellResisted) || (OnlyFail && !evt.IsSpellResisted)) && !Spells.Contains(evt.Ability)) {
                return false;
            }

            return true;
        }

    }
}
