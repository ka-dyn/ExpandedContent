using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Classes.DrakeClass;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Cheats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class SkulkingHunter {
        public static void AddSkulkingHunter() {

            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var SlayerStudyTargetFeature = Resources.GetBlueprint<BlueprintFeature>("09bdd9445ac38044389476689ae8d5a1");
            var SlayerSwiftStudyTargetFeature = Resources.GetBlueprint<BlueprintFeature>("40d4f55a5ac0e4f469d67d36c0dfc40b");
            var NatureSense = Resources.GetBlueprint<BlueprintFeature>("22f280eeb4abde34bb92b58ec2673dad");
            var HunterAnimalFocusFeature = Resources.GetBlueprint<BlueprintFeature>("443365823b7d6d14b8d12f4e7bce1077");
            var HunterExtraAnimalFocusFeature = Resources.GetBlueprint<BlueprintFeature>("cbc90fcdb60809744989d386a98cd21c");
            var HunterSpelllist = Resources.GetBlueprint<BlueprintSpellList>("57c894665b7895c499b3dce058c284b3");
            var BardSpellSlotsTable = Resources.GetBlueprint<BlueprintSpellsTable>("0a8eec9ca5c0dc64795243ab3c55d924");
            var BardSpellsKnownTable = Resources.GetBlueprint<BlueprintSpellsTable>("90de6b4f591d1184497fbfbae6e16547");
            var Invisibility = Resources.GetBlueprint<BlueprintAbility>("89940cde01689fb46946b2f8cd7b66b7");
            var SlayerStudyTargetBuff = Resources.GetBlueprint<BlueprintBuff>("45548967b714e254aa83f23354f174b0");

            var SkulkingHunterArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("SkulkingHunterArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"SkulkingHunterArchetype.Name", "Skulking Hunter");
                bp.LocalizedDescription = Helpers.CreateString($"SkulkingHunterArchetype.Description", "Skulking hunters use their magic to lie in wait for their prey, ambushing them with a decisive, fatal strike.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"SkulkingHunterArchetype.Description", "Skulking hunters use their magic to lie in wait for their prey, ambushing them with a decisive, fatal strike.");
            });


            //Spells are added later in ModSupport to also grab modded spells
            var SkulkingHunterSpelllist = Helpers.CreateBlueprint<BlueprintSpellList>("SkulkingHunterSpelllist", bp => {
                bp.IsMythic = false;
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() 
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() 
                    },
                };
            });
            var SkulkingHunterSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("SkulkingHunterSpellbook", bp => {
                bp.Name = Helpers.CreateString($"SkulkingHunterSpellbook.Name", "Skulking Hunter");
                bp.m_SpellsPerDay = BardSpellSlotsTable.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellsKnown = BardSpellsKnownTable.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Wisdom;
                bp.Spontaneous = true;
                bp.SpellsPerLevel = 0;
                bp.AllSpellsKnown = false;
                bp.CantripsType = CantripsType.Orisions;
                bp.CasterLevelModifier = 0;
                bp.IsArcane = false;
            });
            SkulkingHunterArchetype.m_ReplaceSpellbook = SkulkingHunterSpellbook.ToReference<BlueprintSpellbookReference>();


            var SkulkingSpellsFeature = Helpers.CreateBlueprint<BlueprintFeature>("SkulkingSpellsFeature", bp => {
                bp.SetName("Skulking Spells");
                bp.SetDescription("A skulking hunter casts divine spells drawn from the druid, ranger, and sorcerer/wizard spell list. Only druid and sorcerer/wizard spells of 6th level and lower and ranger spells " +
                    "that belong to the enchantment, illusion, and transmutation schools are considered to be part of the skulking hunter’s spell list. If a spell appears on any combination of these spell lists, " +
                    "the skulking hunter uses the lower of the two spell levels listed for the spell. This ability modifies the standard spells and orisons abilities and replaces nature training.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = Invisibility.Icon;                
            });

            var StudyTargetPetRank = Helpers.CreateBlueprint<BlueprintFeature>("StudyTargetPetRank", bp => {
                bp.SetName("StudyTargetPetRank");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.Ranks = 5;
            });
            var SkulkingPetFeature = Helpers.CreateBlueprint<BlueprintFeature>("SkulkingPetFeature", bp => {
                bp.SetName("Skulking Pet");
                bp.SetDescription("The skulking hunter shares the bonuses granted by her studied target ability with her animal companion, however these bonuses do not stack with other sources of studied target.");
                bp.m_Icon = SlayerStudyTargetFeature.Icon;
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.m_CheckedFact = SlayerStudyTargetBuff.ToReference<BlueprintUnitFactReference>();
                    c.AttackBonus = 0;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Not = false;
                });
                bp.AddComponent<DamageBonusAgainstFactOwner>(c => {
                    c.m_CheckedFact = SlayerStudyTargetBuff.ToReference<BlueprintUnitFactReference>();
                    c.DamageBonus = 0;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.MasterFeatureRank;
                    c.m_Feature = StudyTargetPetRank.ToReference<BlueprintFeatureReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });




            var SkulkingHunterStudyTargetFeature = Helpers.CreateBlueprint<BlueprintFeature>("SkulkingHunterStudyTargetFeature", bp => {
                bp.SetName("Studied Target");
                bp.SetDescription("At 1st level, the skulking hunter gains the slayer’s studied target class feature. She uses her hunter level as her effective slayer level to determine the effects of studied target, " +
                    "stacking with other sources of studied target. The skulking hunter shares the bonuses granted by her studied target ability with her animal companion, however these bonuses do not stack with " +
                    "other sources of studied target.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SlayerStudyTargetFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddFeatureToPet>(c => {
                    c.m_Feature = SkulkingPetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = SlayerStudyTargetFeature.Icon;
            });

            var SlayerStudyTargetBuffConfig = Resources.GetBlueprint<BlueprintBuff>("45548967b714e254aa83f23354f174b0").GetComponent<ContextRankConfig>();
            SlayerStudyTargetBuffConfig.m_AdditionalArchetypes = SlayerStudyTargetBuffConfig.m_AdditionalArchetypes.AppendToArray(SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>());
            SlayerStudyTargetBuffConfig.m_Class = SlayerStudyTargetBuffConfig.m_Class.AppendToArray(HunterClass.ToReference<BlueprintCharacterClassReference>());

            

            SkulkingHunterArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, NatureSense, HunterAnimalFocusFeature),
                    Helpers.LevelEntry(8, HunterExtraAnimalFocusFeature)
            };
            SkulkingHunterArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, SkulkingSpellsFeature, SkulkingHunterStudyTargetFeature, StudyTargetPetRank),
                    Helpers.LevelEntry(5, StudyTargetPetRank),
                    Helpers.LevelEntry(7, SlayerSwiftStudyTargetFeature),
                    Helpers.LevelEntry(10, StudyTargetPetRank),
                    Helpers.LevelEntry(15, StudyTargetPetRank),
                    Helpers.LevelEntry(20, StudyTargetPetRank)

            };



            if (ModSettings.AddedContent.Archetypes.IsDisabled("Skulking Hunter")) { return; }
            HunterClass.m_Archetypes = HunterClass.m_Archetypes.AppendToArray(SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
