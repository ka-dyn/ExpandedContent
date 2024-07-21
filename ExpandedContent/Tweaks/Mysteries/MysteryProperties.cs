using Kingmaker.UnitLogic.Mechanics.Properties;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UI.UnitSettings.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class MysteryProperties {
        public static void AddMysteryProperties() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");


            var OracleRevelationProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("OracleRevelationProperty", bp => {
                bp.AddComponent<SummClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.Div2,
                        m_Negate = false
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<StatValueGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_Negate = false,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.Stat = StatType.Charisma;
                    c.ValueType = StatValueGetter.ReturnType.Bonus;
                });
                bp.BaseValue = 10;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Sum;
            });

            var OracleRevelationNoRavenerProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("OracleRevelationNoRavenerProperty", bp => {
                bp.AddComponent<SummClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.Div2,
                        m_Negate = false
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] {  };
                });
                bp.AddComponent<StatValueGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_Negate = false,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.Stat = StatType.Charisma;
                    c.ValueType = StatValueGetter.ReturnType.Bonus;
                });
                bp.BaseValue = 10;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Sum;
            });
        } 
    }
}
