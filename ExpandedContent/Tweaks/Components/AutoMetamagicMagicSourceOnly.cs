using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using JetBrains.Annotations;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using UnityEngine;

namespace ExpandedContent.Tweaks.Components { 

    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]
    [ComponentName("AutoMetamagic spells from a Arcane or Divine source")]
    public class AutoMetamagicMagicSourceOnly : UnitFactComponentDelegate<AutoMetamagicData>, 
        IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, 
        ISubscriber, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell> {

        public enum AffectedSpellSource {
            Arcane,
            Divine
        }
        [SerializeField]
        public AffectedSpellSource m_AffectedSpellSource;
        public Metamagic Metamagic;
        public bool Once = false;
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt) {
            if (evt.Blueprint is BlueprintAbility ability) {   
                if (ability.Type == AbilityType.Spell) {
                    if ((m_AffectedSpellSource == AffectedSpellSource.Arcane && evt.m_SpellSource == SpellSource.Arcane) || (m_AffectedSpellSource == AffectedSpellSource.Divine && evt.m_SpellSource == SpellSource.Divine)) {
                        evt.AddMetamagic(Metamagic);
                        base.Data.Spell = evt.AbilityData;
                    } 
                    else {
                        base.Data.Spell = null;
                    }
                }                
            }
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt) {
        }

        public void OnEventAboutToTrigger(RuleCastSpell evt) {
            if (evt.Spell == base.Data.Spell && Once) {
                base.Owner.RemoveFact(base.Fact);
            }
        }

        public void OnEventDidTrigger(RuleCastSpell evt) {
        }
    }
}
