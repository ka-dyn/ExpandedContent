using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("IncomingSpellResistanceSkip")]

    public class IncomingSpellResistanceSkip : UnitFactComponentDelegate, ITargetRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ISubscriber, ITargetRulebookSubscriber {

        public bool AllSpells;
        public BlueprintAbilityReference[] m_Spells;
        public ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference> Spells => m_Spells;


        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt) {
            if (!AllSpells && (Spells.Contains(evt.Ability) || Spells.Contains(SimpleBlueprintExtendAsObject.Or(evt.Ability, null)?.Parent))) {
                evt.IgnoreSpellResistance = true;
            }

            if (AllSpells) {
                evt.IgnoreSpellResistance = true;
            }
        }

        public void OnEventDidTrigger(RuleSpellResistanceCheck evt) {
            
        }

    }
}
