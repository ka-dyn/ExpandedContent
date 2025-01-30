using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {
    
    [ComponentName("Immune to Energy Drain from X")]

    public class AddImmunityToEnergyDrainFromFact : UnitFactComponentDelegate, ITargetRulebookHandler<RuleDrainEnergy>, IRulebookHandler<RuleDrainEnergy>, ISubscriber, ITargetRulebookSubscriber {
        public BlueprintUnitFactReference m_CheckedFact;
        public BlueprintUnitFact CheckedFact => m_CheckedFact?.Get();

        public void OnEventAboutToTrigger(RuleDrainEnergy evt) {
            if (evt.Initiator.Descriptor.HasFact(CheckedFact)) {
                evt.TargetIsImmune = true;
            }            
        }

        public void OnEventDidTrigger(RuleDrainEnergy evt) {
        }

    }
}
