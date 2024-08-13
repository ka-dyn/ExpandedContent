using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Kingmaker.Armies.TacticalCombat.Grid.TacticalCombatGrid;

namespace ExpandedContent.Tweaks.Components {

    [AllowMultipleComponents]
    [ComponentName("Increase spell caster level if Arcane or Divine")]

    public class IncreaseSpellCasterLevelMagicSourceOnly : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber {

        public enum AffectedSpellSource {
            Arcane,
            Divine
        }
        [SerializeField]
        public AffectedSpellSource m_AffectedSpellSource;
        public ModifierDescriptor Descriptor = ModifierDescriptor.UntypedStackable;
        public int BonusDC;

        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt) {

            if (evt.Blueprint is BlueprintAbility ability) {
                if (ability.Type == AbilityType.Spell) {
                    if ((m_AffectedSpellSource == AffectedSpellSource.Arcane && evt.m_SpellSource == SpellSource.Arcane) || (m_AffectedSpellSource == AffectedSpellSource.Divine && evt.m_SpellSource == SpellSource.Divine)) {
                        evt.AddBonusCasterLevel(BonusDC, Descriptor);
                    } 
                }
            }
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt) {
        }

    }
}
