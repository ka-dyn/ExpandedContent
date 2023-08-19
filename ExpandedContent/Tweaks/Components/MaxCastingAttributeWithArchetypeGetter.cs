using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using UnityEngine;
using Kingmaker.UnitLogic;
using System.Runtime.Remoting.Contexts;
using System.Linq;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Max Casting Attribute With Archetype Getter")]
    public class MaxCastingAttributeWithArchetypeGetter : PropertyValueGetter {
        //I have no idea if this would work //It worked //yay
        [SerializeField]
        public BlueprintCharacterClassReference[] m_Classes;

        [SerializeField]
        public BlueprintArchetypeReference[] m_Archetypes;

        [SerializeField]
        public bool AttributeBonus;

        public StatType DefaultStat;

        public override int GetBaseValue(UnitEntityData unit) {
            int num = 0;
            BlueprintCharacterClassReference[] classes = m_Classes;
            BlueprintArchetypeReference[] archetypes = m_Archetypes;
            foreach (ClassData data in unit.Descriptor.Progression.Classes) {
                if (archetypes.Length > 0) {
                    if (archetypes.Any(archetype => data.CharacterClass.Archetypes.HasReference(archetype))) {
                        if (archetypes.Any(archetype => data.Archetypes.Contains(archetype))) {
                            StatType valueOrDefault = ((data != null) ? SimpleBlueprintExtendAsObject.Or(data.Spellbook, null)?.CastingAttribute : null).GetValueOrDefault();
                            num = ((valueOrDefault != 0 || data == null) ? Math.Max(num, unit.Stats.GetStat<ModifiableValueAttributeStat>(valueOrDefault)?.Bonus ?? 0) : Math.Max(num, unit.Stats.GetStat<ModifiableValueAttributeStat>(DefaultStat)?.Bonus ?? 0));
                        }
                    } else {
                        StatType valueOrDefault = ((data != null) ? SimpleBlueprintExtendAsObject.Or(data.Spellbook, null)?.CastingAttribute : null).GetValueOrDefault();
                        num = ((valueOrDefault != 0 || data == null) ? Math.Max(num, unit.Stats.GetStat<ModifiableValueAttributeStat>(valueOrDefault)?.Bonus ?? 0) : Math.Max(num, unit.Stats.GetStat<ModifiableValueAttributeStat>(DefaultStat)?.Bonus ?? 0));
                    }
                } else {
                    StatType valueOrDefault = ((data != null) ? SimpleBlueprintExtendAsObject.Or(data.Spellbook, null)?.CastingAttribute : null).GetValueOrDefault();
                    num = ((valueOrDefault != 0 || data == null) ? Math.Max(num, unit.Stats.GetStat<ModifiableValueAttributeStat>(valueOrDefault)?.Bonus ?? 0) : Math.Max(num, unit.Stats.GetStat<ModifiableValueAttributeStat>(DefaultStat)?.Bonus ?? 0));
                }
            }

            return num;
        }
    }
}
