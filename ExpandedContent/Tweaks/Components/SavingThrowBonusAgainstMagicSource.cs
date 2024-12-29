using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("Bonus against spell DC if Arcane or Divine")]//Test me!!!

    public class SavingThrowBonusAgainstMagicSource : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSavingThrow>, IRulebookHandler<RuleSavingThrow>, ISubscriber, IInitiatorRulebookSubscriber {
        public enum AffectedSpellSource {
            Arcane,
            Divine
        }
        [SerializeField]
        public AffectedSpellSource m_AffectedSpellSource;
        public ModifierDescriptor Descriptor = ModifierDescriptor.UntypedStackable;
        public ContextValue Value;
        public bool Reflex = true;
        public bool Fortitude = true;
        public bool Will = true;


        public void OnEventAboutToTrigger(RuleSavingThrow evt) {
            int bonus = Value.Calculate(Fact.MaybeContext);

            if (evt.Reason.Ability.Blueprint.Type == Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Spell) {
                if ((evt.Reason.Ability.m_SpellSource == Kingmaker.UnitLogic.Abilities.SpellSource.Arcane && m_AffectedSpellSource == AffectedSpellSource.Arcane) ||
                    (evt.Reason.Ability.m_SpellSource == Kingmaker.UnitLogic.Abilities.SpellSource.Divine && m_AffectedSpellSource == AffectedSpellSource.Divine)) 
                    {
                    if (Reflex) {
                        evt.AddTemporaryModifier(evt.Initiator.Stats.SaveReflex.AddModifier(bonus * Fact.GetRank(), Runtime, Descriptor));
                    }
                    if (Fortitude) {
                        evt.AddTemporaryModifier(evt.Initiator.Stats.SaveFortitude.AddModifier(bonus * Fact.GetRank(), Runtime, Descriptor));
                    }
                    if (Will) {
                        evt.AddTemporaryModifier(evt.Initiator.Stats.SaveWill.AddModifier(bonus * Fact.GetRank(), Runtime, Descriptor));
                    }
                }
            }

            
            
        }

        public void OnEventDidTrigger(RuleSavingThrow evt) {
        }
    }
}
