using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UI.GenericSlot;
using Kingmaker.Blueprints.Items.Ecnchantments;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Mooncaller {
        public static void AddMooncaller() {

            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var ResistNaturesLureFeature = Resources.GetBlueprint<BlueprintFeature>("ad6a5b0e1a65c3540986cf9a7b006388");
            var VenomImmunityFeature = Resources.GetBlueprint<BlueprintFeature>("5078622eb5cecaf4683fa16a9b948c2c");
            var PurityOfBodyFeature = Resources.GetBlueprint<BlueprintFeature>("9b02f77c96d6bba4daf9043eff876c76");
            //Spells for Resist Call of the Wild
            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var FeeblemindSpell = Resources.GetBlueprint<BlueprintAbility>("444eed6e26f773a40ab6e4d160c67faa");
            var DazeSpell = Resources.GetBlueprint<BlueprintAbility>("55f14bc84d7c85446b07a1b5dd6b2b4c");
            var InsanitySpell = Resources.GetBlueprint<BlueprintAbility>("2b044152b3620c841badb090e01ed9de");

            var MooncallerArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("MooncallerArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"MooncallerArchetype.Name", "Mooncaller");
                bp.LocalizedDescription = Helpers.CreateString($"MooncallerArchetype.Description", "A mooncaller is bound to the subtle influences of the ever-changing moon and its endless " +
                    "cycles from light to dark and back again.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"MooncallerArchetype.Description", "A mooncaller is bound to the subtle influences of the ever-changing moon and its endless " +
                    "cycles from light to dark and back again.");
                
            });
            var NightSightFeature = Helpers.CreateBlueprint<BlueprintFeature>("NightSightFeature", bp => {
                bp.SetName("Night Sight");
                bp.SetDescription("At 2nd level, a Mooncallers vision enhances to better traverse the land at night. They gain a +4 bonus to their Perception.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Value = 4;
                });
            });
            var ResistCallOfTheWildFeature = Helpers.CreateBlueprint<BlueprintFeature>("ResistCallOfTheWildFeature", bp => {
                bp.SetName("Resist Call of the Wild");
                bp.SetDescription("At 4th level, a mooncaller gains a +4 bonus on saving throws to avoid confusion, daze, feeblemind, and insanity effects. She also gains a +4 bonus " +
                    "against the, spell-like, and supernatural abilities of creatures with the shapechanger subtype.");
                /// Shapechanger type not in game yet
                //bp.AddComponent<SavingThrowBonusAgainstFact>(c => {
                //    c.m_CheckedFact = ShapechangerType;
                //    c.Value = 4;
                //});
                bp.AddComponent<SavingThrowBonusAgainstSpecificSpells>(c => {
                    c.m_Spells = new BlueprintAbilityReference[] {
                        ConfusionSpell.ToReference<BlueprintAbilityReference>(),
                        FeeblemindSpell.ToReference<BlueprintAbilityReference>(),
                        DazeSpell.ToReference<BlueprintAbilityReference>(),
                        InsanitySpell.ToReference<BlueprintAbilityReference>()
                    };
                    c.Value = 4;
                });
            });
            var WolfsbaneFeature1DR = Helpers.CreateBlueprint<BlueprintFeature>("WolfsbaneFeature1DR", bp => {
                bp.SetName("Wolfsbane");
                bp.SetDescription("At 13th level, a mooncaller gains DR 3/silver, increasing to DR 4/silver at 16th level and DR 5/silver at 19th level.");
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 3;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Silver;
                });
            });
            var WolfsbaneFeature2DR = Helpers.CreateBlueprint<BlueprintFeature>("WolfsbaneFeature2DR", bp => {
                bp.SetName("Wolfsbane");
                bp.SetDescription("At 13th level, a mooncaller gains DR 3/silver, increasing to DR 4/silver at 16th level and DR 5/silver at 19th level.");
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 4;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Silver;
                });
            });
            var WolfsbaneFeature3DR = Helpers.CreateBlueprint<BlueprintFeature>("WolfsbaneFeature3DR", bp => {
                bp.SetName("Wolfsbane");
                bp.SetDescription("At 13th level, a mooncaller gains DR 3/silver, increasing to DR 4/silver at 16th level and DR 5/silver at 19th level.");
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 5;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Silver;
                });
            });
            var WolfsbaneFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("WolfsbaneFeature1", bp => {
                bp.SetName("Wolfsbane");
                bp.SetDescription("At 13th level, a mooncaller gains DR 3/silver, increasing to DR 4/silver at 16th level and DR 5/silver at 19th level.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 16;
                    c.m_Feature = WolfsbaneFeature1DR.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
            });
            var WolfsbaneFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("WolfsbaneFeature2", bp => {
                bp.SetName("Wolfsbane");
                bp.SetDescription("At 13th level, a mooncaller gains DR 3/silver, increasing to DR 4/silver at 16th level and DR 5/silver at 19th level.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 19;
                    c.m_Feature = WolfsbaneFeature2DR.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
            });
            var WolfsbaneFeature3 = Helpers.CreateBlueprint<BlueprintFeature>("WolfsbaneFeature3", bp => {
                bp.SetName("Wolfsbane");
                bp.SetDescription("At 13th level, a mooncaller gains DR 3/silver, increasing to DR 4/silver at 16th level and DR 5/silver at 19th level.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 19;
                    c.m_Feature = WolfsbaneFeature3DR.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel= false;
                });
                bp.HideInUI = true;
            });
            MooncallerArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, ResistNaturesLureFeature),
                    Helpers.LevelEntry(9, VenomImmunityFeature),
            };
            MooncallerArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, NightSightFeature),
                    Helpers.LevelEntry(4, ResistCallOfTheWildFeature),
                    Helpers.LevelEntry(9, PurityOfBodyFeature),
                    Helpers.LevelEntry(13, WolfsbaneFeature1),
                    Helpers.LevelEntry(16, WolfsbaneFeature2),
                    Helpers.LevelEntry(19, WolfsbaneFeature3)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Mooncaller")) { return; }
            DruidClass.m_Archetypes = DruidClass.m_Archetypes.AppendToArray(MooncallerArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
