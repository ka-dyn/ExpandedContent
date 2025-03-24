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
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using UnityEngine.Serialization;
using UnityEngine;
using Kingmaker.UnitLogic.Mechanics;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("SavingThrowBonusAgainstSpecificSpells but with ContextValue")]
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]

    public class SavingThrowContextBonusAgainstSpecificSpells : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSavingThrow>, IRulebookHandler<RuleSavingThrow>, ISubscriber, IInitiatorRulebookSubscriber {

        [SerializeField]
        public BlueprintAbilityReference[] m_Spells;

        public ModifierDescriptor ModifierDescriptor;

        public ContextValue Value;


        [SerializeField]
        public BlueprintUnitFactReference[] m_BypassFeatures;

        public ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference> Spells => m_Spells;

        public ReferenceArrayProxy<BlueprintUnitFact, BlueprintUnitFactReference> BypassFeatures => m_BypassFeatures;

        public void OnEventAboutToTrigger(RuleSavingThrow evt) {
            BlueprintAbility blueprintAbility = evt.Reason.Context?.SourceAbility;
            UnitEntityData unitEntityData = evt.Reason.Context?.MaybeCaster;
            int num = Value.Calculate(Fact.MaybeContext);

            bool flag = unitEntityData != null;
            if (flag) {
                foreach (BlueprintUnitFact bypassFeature in BypassFeatures) {
                    flag = !unitEntityData.Descriptor.HasFact(bypassFeature);
                }
            }

            if (blueprintAbility != null && Spells.Contains(blueprintAbility) && flag) {
                evt.AddTemporaryModifier(evt.Initiator.Stats.SaveWill.AddModifier(num, base.Runtime, ModifierDescriptor));
                evt.AddTemporaryModifier(evt.Initiator.Stats.SaveReflex.AddModifier(num, base.Runtime, ModifierDescriptor));
                evt.AddTemporaryModifier(evt.Initiator.Stats.SaveFortitude.AddModifier(num, base.Runtime, ModifierDescriptor));
            }
        }

        public void OnEventDidTrigger(RuleSavingThrow evt) {
        }

    }
}
