using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using ExpandedContent.Tweaks.Components;

namespace ExpandedContent.Tweaks.Curses {
    internal class Lich {

        public static void AddLichCurse() {

            var OracleCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b0a5118b4fb793241bc7042464b23fab");
            var BeneficialCurse = Resources.GetBlueprint<BlueprintFeatureSelection>("2dda67424ee8e0b4d83ef01a73ca6bff");
            var MysteryGiftFeatureCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4e7265c0ae1345db90d3375f4ced94cc");
            var DualCursedSecondCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cc6fda79e8c340b88c84689414a9abbe");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var AccursedArchetype = Resources.GetBlueprint<BlueprintArchetype>("c5f6e53e71059fb4d802ce81a277a12d");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");
            var CommandUndeadSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("0b101dd5618591e478f825f0eef155b4");
            var CreateUndeadBaseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("76a11b460be25e44ca85904d6806e5a3");
            var LichIcon = AssetLoader.LoadInternal("Skills", "Icon_LichCurse.png");


            var LichCurseFeatureLevel5 = Helpers.CreateBlueprint<BlueprintFeature>("LichCurseFeatureLevel5", bp => {
                bp.SetName("Lich");
                bp.SetDescription("At 5th level, add command undead to your list of 2nd-level oracle spells known.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CommandUndeadSpell;
                    c.SpellLevel = 2;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CommandUndeadSpell;
                    c.SpellLevel = 2;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var LichCurseFeatureLevel10 = Helpers.CreateBlueprint<BlueprintFeature>("LichCurseFeatureLevel10", bp => {
                bp.SetName("Lich");
                bp.SetDescription("At 10th level, add create undead to your list of 5th-level oracle spells known.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CreateUndeadBaseSpell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CreateUndeadBaseSpell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var LichCurseFeatureLevel15 = Helpers.CreateBlueprint<BlueprintFeature>("LichCurseFeatureLevel15", bp => {
                bp.SetName("Lich");
                bp.SetDescription("At 15th level, you are immune to death effects.");
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Death;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var LichCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("LichCurseProgression", bp => {
                bp.SetName("Lich");
                bp.SetDescription("Every living spellcaster hides a secret in their flesh—a unique, personalized set of conditions that, when all are fulfilled in the correct order, " +
                    "can trigger the transformation into a lich. Normally, one must expend years and tens of thousands of gold pieces to research this deeply personalized method of " +
                    "attaining immortality. Yet, in a rare few cases, chance and ill fortune can conspire against an unsuspecting spellcaster. \nYou have yet to turn into an undead " +
                    "creature, but you are close. You take damage from positive energy and heal from negative energy as if you were undead." +
                    "\nAt 5th level, add command undead to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add create undead to your list of 5th-level oracle spells known. " +
                    "\nAt 15th level, you are immune to death effects.");
                bp.m_Icon = LichIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleCurse };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = AccursedArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, NegativeEnergyAffinity),
                    Helpers.LevelEntry(5, LichCurseFeatureLevel5),
                    Helpers.LevelEntry(10, LichCurseFeatureLevel10),
                    Helpers.LevelEntry(15, LichCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            LichCurseProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.CheckInProgression = true;
                c.m_Feature = LichCurseProgression.ToReference<BlueprintFeatureReference>();
                c.HideInUI = true;
            });
            var BeneficialLichCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("BeneficialLichCurseProgression", bp => {
                bp.SetName("Lich");
                bp.SetDescription("Every living spellcaster hides a secret in their flesh—a unique, personalized set of conditions that, when all are fulfilled in the correct order, " +
                    "can trigger the transformation into a lich. Normally, one must expend years and tens of thousands of gold pieces to research this deeply personalized method of " +
                    "attaining immortality. Yet, in a rare few cases, chance and ill fortune can conspire against an unsuspecting spellcaster. \nYou have yet to turn into an undead " +
                    "creature, but you are close. You take damage from positive energy and heal from negative energy as if you were undead." +
                    "\nAt 5th level, add command undead to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add create undead to your list of 5th-level oracle spells known. " +
                    "\nAt 15th level, you are immune to death effects." +
                    "\n\n(The Negative Energy Affinity granted at level 1 is not considered a penalty and therefore is not removed.)");
                bp.m_Icon = LichIcon;
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = AccursedArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = LichCurseProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, NegativeEnergyAffinity),
                    Helpers.LevelEntry(5, LichCurseFeatureLevel5),
                    Helpers.LevelEntry(10, LichCurseFeatureLevel10),
                    Helpers.LevelEntry(15, LichCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var LichCurseNoProgression = Helpers.CreateBlueprint<BlueprintProgression>("LichCurseNoProgression", bp => {
                bp.SetName("Lich");
                bp.SetDescription("Every living spellcaster hides a secret in their flesh—a unique, personalized set of conditions that, when all are fulfilled in the correct order, " +
                    "can trigger the transformation into a lich. Normally, one must expend years and tens of thousands of gold pieces to research this deeply personalized method of " +
                    "attaining immortality. Yet, in a rare few cases, chance and ill fortune can conspire against an unsuspecting spellcaster. \nYou have yet to turn into an undead " +
                    "creature, but you are close. You take damage from positive energy and heal from negative energy as if you were undead." +
                    "\nAt 5th level, add command undead to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add create undead to your list of 5th-level oracle spells known. " +
                    "\nAt 15th level, you are immune to death effects.");
                bp.m_Icon = LichIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleCurse };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = AccursedArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, NegativeEnergyAffinity)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = LichCurseProgression.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
            });

            OracleCurseSelection.m_AllFeatures = OracleCurseSelection.m_AllFeatures.AppendToArray(LichCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_AllFeatures = BeneficialCurse.m_AllFeatures.AppendToArray(BeneficialLichCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_Features = BeneficialCurse.m_Features.AppendToArray(BeneficialLichCurseProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_AllFeatures = MysteryGiftFeatureCurseSelection.m_AllFeatures.AppendToArray(LichCurseNoProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_Features = MysteryGiftFeatureCurseSelection.m_Features.AppendToArray(LichCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_AllFeatures = DualCursedSecondCurseSelection.m_AllFeatures.AppendToArray(LichCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_Features = DualCursedSecondCurseSelection.m_Features.AppendToArray(LichCurseNoProgression.ToReference<BlueprintFeatureReference>());

        }
    }
}
