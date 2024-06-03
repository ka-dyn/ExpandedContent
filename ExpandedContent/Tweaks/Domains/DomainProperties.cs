using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Domains {
    internal class DomainProperties {
        public static void AddDomainProperties() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var SeparatistArchetype = Resources.GetBlueprint<BlueprintArchetype>("67401c74ee094e82a2afd645a847f5cc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");

            var SeparatistAsIsProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty", bp => {
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.BaseValue = 0;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Sum;
            });
            var SeparatistWithDruidAsIsProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("SeparatistWithDruidAsIsProperty", bp => {
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.BaseValue = 0;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Sum;
            });


        }
    }
}
