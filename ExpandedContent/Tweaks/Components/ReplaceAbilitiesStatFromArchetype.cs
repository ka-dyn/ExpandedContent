using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System.Linq;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Replace stat for Ability DC depending on class archetype")]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [TypeId("1ce850fdf5f24b8182e4b53a6de47e4e")]
    public class ReplaceAbilitiesStatFromArchetype :
        UnitFactComponentDelegate,
        IInitiatorRulebookHandler<RuleCalculateAbilityParams>,
        IRulebookHandler<RuleCalculateAbilityParams>,
        ISubscriber,
        IInitiatorRulebookSubscriber {

        public BlueprintAbilityReference[] m_Ability;
        public StatType NormalStat;
        public BlueprintArchetypeReference Archetype;
        public StatType ArchetypeStat;

        public ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference> Ability => (ReferenceArrayProxy<BlueprintAbility, BlueprintAbilityReference>)this.m_Ability;

        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt) {
            if (!Ability.Contains(evt.Spell))
                return;
            if (Owner.Progression.IsArchetype(Archetype)) {
                evt.ReplaceStat = ArchetypeStat;
                return;
            }
            evt.ReplaceStat = NormalStat;
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt) {
        }
    }
}
