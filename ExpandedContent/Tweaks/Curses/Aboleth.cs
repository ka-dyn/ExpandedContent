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
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Curses {
    internal class Aboleth {

        public static void AddAbolethCurse() {

            var OracleCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b0a5118b4fb793241bc7042464b23fab");
            var BeneficialCurse = Resources.GetBlueprint<BlueprintFeatureSelection>("2dda67424ee8e0b4d83ef01a73ca6bff");
            var MysteryGiftFeatureCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4e7265c0ae1345db90d3375f4ced94cc");
            var DualCursedSecondCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cc6fda79e8c340b88c84689414a9abbe");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var AccursedWitchArchetype = Resources.GetBlueprint<BlueprintArchetype>("c5f6e53e71059fb4d802ce81a277a12d");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var HypnotismSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("88367310478c10b47903463c5d0152b0");
            var MirrorImageSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3e4ab69ada402d145a5e0ad3ad4b8564");
            var HoldMonsterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("41e8a952da7a5c247b3ec1c2dbb73018");
            var CloakOfDreamsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7f71a70d822af94458dc1a235507e972");
            var AbolethIcon = AssetLoader.LoadInternal("Skills", "Icon_AbolethCurse.png");


            var AbolethCurseFeatureLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("AbolethCurseFeatureLevel1", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("Your mind is marked by aboleth tampering, either as a result of a close encounter with one of these creatures while you were a child, " +
                    "or even through an ancestor, close family member, or one of your sisters or brothers in faith—this associate’s interaction with the aboleths may be " +
                    "all that it takes to infect you. \nYou take a –2 penalty on saving throws against mind-affecting effects and add hypnotism to your list of 1st-level " +
                    "oracle spells known. ");
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                    c.ModifierDescriptor = ModifierDescriptor.Penalty;
                    c.Value = -2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = HypnotismSpell;
                    c.SpellLevel = 1;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = HypnotismSpell;
                    c.SpellLevel = 1;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AbolethCurseBeneficialFeatureLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("AbolethCurseBeneficialFeatureLevel1", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("Your mind is marked by aboleth tampering, either as a result of a close encounter with one of these creatures while you were a child, " +
                    "or even through an ancestor, close family member, or one of your sisters or brothers in faith—this associate’s interaction with the aboleths may be " +
                    "all that it takes to infect you. \nYou take a –2 penalty on saving throws against mind-affecting effects and add hypnotism to your list of 1st-level " +
                    "oracle spells known. ");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = HypnotismSpell;
                    c.SpellLevel = 1;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = HypnotismSpell;
                    c.SpellLevel = 1;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AbolethCurseFeatureLevel5 = Helpers.CreateBlueprint<BlueprintFeature>("AbolethCurseFeatureLevel5", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("At 5th level, add mirror image to your list of 2nd-level oracle spells known.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = MirrorImageSpell;
                    c.SpellLevel = 2;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = MirrorImageSpell;
                    c.SpellLevel = 2;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AbolethCurseFeatureLevel10 = Helpers.CreateBlueprint<BlueprintFeature>("AbolethCurseFeatureLevel10", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("At 10th level, add hold monster to your list of 4th-level oracle spells known");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = HoldMonsterSpell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = HoldMonsterSpell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AbolethCurseFeatureLevel15 = Helpers.CreateBlueprint<BlueprintFeature>("AbolethCurseFeatureLevel15", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("At 15th level, add cloak of dreams to your list of 6th-level oracle spells known.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CloakOfDreamsSpell;
                    c.SpellLevel = 6;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CloakOfDreamsSpell;
                    c.SpellLevel = 6;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AbolethCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("AbolethCurseProgression", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("Your mind is marked by aboleth tampering, either as a result of a close encounter with one of these creatures while you were a child, " +
                    "or even through an ancestor, close family member, or one of your sisters or brothers in faith—this associate’s interaction with the aboleths may be " +
                    "all that it takes to infect you. \nYou take a –2 penalty on saving throws against mind-affecting effects and add hypnotism to your list of 1st-level " +
                    "oracle spells known. " +
                    "\nAt 5th level, add mirror image to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add hold monster to your list of 4th-level oracle spells known. " +
                    "\nAt 15th level, add cloak of dreams to your list of 6th-level oracle spells known.");
                bp.m_Icon = AbolethIcon;
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
                        m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AbolethCurseFeatureLevel1),
                    Helpers.LevelEntry(5, AbolethCurseFeatureLevel5),
                    Helpers.LevelEntry(10, AbolethCurseFeatureLevel10),
                    Helpers.LevelEntry(15, AbolethCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            AbolethCurseProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.CheckInProgression = true;
                c.m_Feature = AbolethCurseProgression.ToReference<BlueprintFeatureReference>();
                c.HideInUI = true;
            });
            var BeneficialAbolethCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("BeneficialAbolethCurseProgression", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("Your mind is marked by aboleth tampering, either as a result of a close encounter with one of these creatures while you were a child, " +
                    "or even through an ancestor, close family member, or one of your sisters or brothers in faith—this associate’s interaction with the aboleths may be " +
                    "all that it takes to infect you. \nYou take a –2 penalty on saving throws against mind-affecting effects and add hypnotism to your list of 1st-level " +
                    "oracle spells known. " +
                    "\nAt 5th level, add mirror image to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add hold monster to your list of 4th-level oracle spells known. " +
                    "\nAt 15th level, add cloak of dreams to your list of 6th-level oracle spells known.");
                bp.m_Icon = AbolethIcon;
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
                        m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = AbolethCurseProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AbolethCurseBeneficialFeatureLevel1),
                    Helpers.LevelEntry(5, AbolethCurseFeatureLevel5),
                    Helpers.LevelEntry(10, AbolethCurseFeatureLevel10),
                    Helpers.LevelEntry(15, AbolethCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var AbolethCurseNoProgression = Helpers.CreateBlueprint<BlueprintProgression>("AbolethCurseNoProgression", bp => {
                bp.SetName("Aboleth");
                bp.SetDescription("Your mind is marked by aboleth tampering, either as a result of a close encounter with one of these creatures while you were a child, " +
                    "or even through an ancestor, close family member, or one of your sisters or brothers in faith—this associate’s interaction with the aboleths may be " +
                    "all that it takes to infect you. \nYou take a –2 penalty on saving throws against mind-affecting effects and add hypnotism to your list of 1st-level " +
                    "oracle spells known. " +
                    "\nAt 5th level, add mirror image to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add hold monster to your list of 4th-level oracle spells known. " +
                    "\nAt 15th level, add cloak of dreams to your list of 6th-level oracle spells known.");
                bp.m_Icon = AbolethIcon;
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
                        m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AbolethCurseFeatureLevel1)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = AbolethCurseProgression.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
            });

            OracleCurseSelection.m_AllFeatures = OracleCurseSelection.m_AllFeatures.AppendToArray(AbolethCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_AllFeatures = BeneficialCurse.m_AllFeatures.AppendToArray(BeneficialAbolethCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_Features = BeneficialCurse.m_Features.AppendToArray(BeneficialAbolethCurseProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_AllFeatures = MysteryGiftFeatureCurseSelection.m_AllFeatures.AppendToArray(AbolethCurseNoProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_Features = MysteryGiftFeatureCurseSelection.m_Features.AppendToArray(AbolethCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_AllFeatures = DualCursedSecondCurseSelection.m_AllFeatures.AppendToArray(AbolethCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_Features = DualCursedSecondCurseSelection.m_Features.AppendToArray(AbolethCurseNoProgression.ToReference<BlueprintFeatureReference>());


            //Stupid fish icon
            if (ModSettings.AddedContent.Miscellaneous.IsDisabled("Aboleth Curse Better Icon")) { return; }
            var AbolethBetterIcon = AssetLoader.LoadInternal("Skills", "Icon_FishCurse.png");
            AbolethCurseProgression.m_Icon = AbolethBetterIcon;
            BeneficialAbolethCurseProgression.m_Icon = AbolethBetterIcon;
            AbolethCurseNoProgression.m_Icon = AbolethBetterIcon;
        }
    }
}
