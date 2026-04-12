using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("OwnerAbilityTargetSavingThrowBonus but can be locked to certain spells")]
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]

    public class OwnerAbilityTargetSavingThrowBonusExpanded : UnitFactComponentDelegate, IGlobalRulebookHandler<RuleSavingThrow>, IRulebookHandler<RuleSavingThrow>, ISubscriber, IGlobalRulebookSubscriber {

        public int Bonus;
        public ModifierDescriptor Descriptor;

        public bool CheckAbilityType;

        [ShowIf("CheckAbilityType")]
        public AbilityType Type;

        public bool OnlyTheseAbilities;

        [SerializeField]
        public BlueprintAbilityReference[] m_Spells;
        public ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference> Spells => m_Spells;


        public ConditionsChecker Conditions;

        public void OnEventAboutToTrigger(RuleSavingThrow evt) {
            BlueprintAbility blueprintAbility = evt.Reason.Context?.SourceAbility;
            if (evt.Reason.Caster != base.Owner || (CheckAbilityType && (evt.Reason.Ability == null || evt.Reason.Ability.Blueprint.Type != Type)) || (OnlyTheseAbilities && !Spells.Contains(blueprintAbility))) {
                return;
            }

            if (Conditions != null) {
                using (base.Context.GetDataScope(evt.Initiator)) {
                    if (!Conditions.Check()) {
                        return;
                    }
                }
            }

            evt.AddModifier(Bonus, base.Fact, Descriptor);
        }

        public void OnEventDidTrigger(RuleSavingThrow evt) {
        }
    }
}
