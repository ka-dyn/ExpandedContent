using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using UnityEngine.Serialization;
using UnityEngine;

namespace ExpandedContent.Tweaks.Components {

    [AllowMultipleComponents]
    [ComponentName("Spell penetration bonus against unit with fact")]

    public class SpellPenetrationBonusAgainstFact : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ISubscriber, IInitiatorRulebookSubscriber {
        
        public ContextValue Value;
        public ModifierDescriptor Descriptor = ModifierDescriptor.UntypedStackable;
        public BlueprintUnitFactReference m_CheckedFact;
        public BlueprintUnitFact CheckedFact => m_CheckedFact?.Get();

        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt) {
            if (evt.Target.Descriptor.HasFact(CheckedFact)) {
                int bonus = Value.Calculate(base.Context);
                evt.AddSpellPenetration(bonus, Descriptor);
            }
        }
        public void OnEventDidTrigger(RuleSpellResistanceCheck evt) {
        }
    }
}
