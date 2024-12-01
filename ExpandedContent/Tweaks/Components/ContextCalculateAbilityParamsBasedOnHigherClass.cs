using JetBrains.Annotations;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using Owlcat.QA.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Serialization;
using UnityEngine;
using Kingmaker.UnitLogic;
using static TabletopTweaks.Core.Utilities.ClassTools;
using static Kingmaker.GameModes.GameModeType;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Kin or Archetype stat calculator")]
    //please remember to test
    public class ContextCalculateAbilityParamsBasedOnHigherClass : ContextAbilityParamsCalculator {
        public bool UseKineticistMainStat;

        [HideIf("UseKineticistMainStat")]
        public StatType StatType = StatType.Charisma;

        [SerializeField]
        public BlueprintCharacterClassReference[] m_Classes;

        [SerializeField]
        public BlueprintArchetypeReference[] m_Archetypes;

        public override AbilityParams Calculate(MechanicsContext context) {
            UnitEntityData maybeCaster = context.MaybeCaster;
            if (maybeCaster == null) {
                PFLog.Default.Error(this, "Caster is missing");
                return context.Params;
            }

            return Calculate(context, context.AssociatedBlueprint, maybeCaster, context.SourceAbilityContext?.Ability);
        }

        public AbilityParams Calculate([NotNull] AbilityData ability) {
            return Calculate(null, ability.Blueprint, ability.Caster, ability);
        }

        public AbilityParams Calculate([CanBeNull] MechanicsContext context, [NotNull] BlueprintScriptableObject blueprint, [NotNull] UnitEntityData caster, [CanBeNull] AbilityData ability) {
            StatType value = StatType;
            if (UseKineticistMainStat) {
                UnitPartKineticist obj = caster?.Get<UnitPartKineticist>();
                if (obj == null) {
                    PFLog.Default.Error(blueprint, $"Caster is not kineticist: {caster} ({blueprint.NameSafe()})");
                }

                value = obj?.MainStatType ?? StatType;
            }

            RuleCalculateAbilityParams ruleCalculateAbilityParams = ((ability != null) ? new RuleCalculateAbilityParams(caster, ability) : new RuleCalculateAbilityParams(caster, blueprint, null));
            ruleCalculateAbilityParams.ReplaceStat = value;
            int abilityCasterLevel = 0; //starts at 0
            foreach (ClassData data in caster.Descriptor.Progression.Classes) {
                if (!m_Classes.HasReference(data.CharacterClass)) {//Is it in the list go to next if
                    continue;//If not, end this loop and skip to next class
                }

                if (m_Archetypes.Length > 0) {

                    if (m_Archetypes.Any(archetype => data.CharacterClass.Archetypes.HasReference(archetype))) {//Does the characterclass have any of the archetypes on the list?

                        if (m_Archetypes.Any(archetype => data.Archetypes.Contains(archetype))) {//If archetype matches add level, if not then ignore
                            abilityCasterLevel = Math.Max(abilityCasterLevel, data.Level);
                        } //no bonus from this class

                    } else {
                        abilityCasterLevel = Math.Max(abilityCasterLevel, data.Level);
                    }

                } else {
                    abilityCasterLevel = Math.Max(abilityCasterLevel, data.Level);//archetype list was 0
                }
            }

            ruleCalculateAbilityParams.ReplaceCasterLevel = abilityCasterLevel;
            ruleCalculateAbilityParams.ReplaceSpellLevel = abilityCasterLevel / 2;
            if (context != null) {
                return context.TriggerRule(ruleCalculateAbilityParams).Result;
            }

            return Rulebook.Trigger(ruleCalculateAbilityParams).Result;
        }

    }
}
