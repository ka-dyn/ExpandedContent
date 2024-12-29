using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("React to spell if Arcane or Divine")]//Works but only when targeted (either single or AoE), not when walking into an AoE

    //AddBuffOnApplyingSpell used as example
    public class ContextActionOnApplyingSpell : UnitFactComponentDelegate, IApplyAbilityEffectHandler, ISubscriber, 
        ITargetRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ITargetRulebookSubscriber {

        public enum AffectedSpellSource {
            Arcane,
            Divine
        }
        public AffectedSpellSource m_AffectedSpellSource;
        public ActionList ActionOnSelf;

        public void OnAbilityEffectApplied(AbilityExecutionContext context) {
        }

        public void OnTryToApplyAbilityEffect(AbilityExecutionContext context, TargetWrapper target) {

            IFactContextOwner factContextOwner = base.Fact as IFactContextOwner;
            if (target.Unit != base.Owner) { return; }

            if ((context.Ability.m_SpellSource == SpellSource.Arcane && m_AffectedSpellSource == AffectedSpellSource.Arcane) ||
                (context.Ability.m_SpellSource == SpellSource.Divine && m_AffectedSpellSource == AffectedSpellSource.Divine)) {
                if (factContextOwner != null) {
                    Main.Log("ContextActionOnApplyingSpell triggered");
                    factContextOwner.RunActionInContext(this.ActionOnSelf, base.Owner);
                }
            }
        }

        public void OnAbilityEffectAppliedToTarget(AbilityExecutionContext context, TargetWrapper target) {
        }

        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt) {
        }

        public void OnEventDidTrigger(RuleSpellResistanceCheck evt) {
        }


    }
}
