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
    internal class Accursed {

        public static void AddAccursedCurse() {

            var OracleCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b0a5118b4fb793241bc7042464b23fab");
            var BeneficialCurse = Resources.GetBlueprint<BlueprintFeatureSelection>("2dda67424ee8e0b4d83ef01a73ca6bff");
            var MysteryGiftFeatureCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4e7265c0ae1345db90d3375f4ced94cc");
            var DualCursedSecondCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cc6fda79e8c340b88c84689414a9abbe");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var AccursedWitchArchetype = Resources.GetBlueprint<BlueprintArchetype>("c5f6e53e71059fb4d802ce81a277a12d");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var IllOmenSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ca577309cedc4f1daf6fe5795fb2619b");
            var BestowCurseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("989ab5c44240907489aba0a8568d0603");
            var AccursedIcon = AssetLoader.LoadInternal("Skills", "Icon_AccursedCurse.png");


            var AccursedCurseFeatureLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("AccursedCurseFeatureLevel1", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("You are cursed with misfortune and sorrow, and you cannot gain benefit from morale bonuses. " +
                    "However, you gain a +4 bonus to all saving throws against curse effects. ");
                bp.AddComponent<DisableAllMoraleBonuses>();
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Curse;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AccursedCurseBeneficialFeatureLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("AccursedCurseBeneficialFeatureLevel1", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("You are cursed with misfortune and sorrow, and you cannot gain benefit from morale bonuses. " +
                    "However, you gain a +4 bonus to all saving throws against curse effects. ");
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Curse;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AccursedCurseFeatureLevel5 = Helpers.CreateBlueprint<BlueprintFeature>("AccursedCurseFeatureLevel5", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("At 5th level, add ill omen to your list of 2nd-level oracle spells known.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = IllOmenSpell;
                    c.SpellLevel = 2;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = IllOmenSpell;
                    c.SpellLevel = 2;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AccursedCurseFeatureLevel10 = Helpers.CreateBlueprint<BlueprintFeature>("AccursedCurseFeatureLevel10", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("At 10th level, add bestow curse to your list of 4th-level oracle spells known.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = BestowCurseSpell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = BestowCurseSpell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AccursedCurseFeatureLevel15 = Helpers.CreateBlueprint<BlueprintFeature>("AccursedCurseFeatureLevel15", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("At 15th level, you are immune to curse effects except for your own oracle curse.");
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Curse;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AccursedCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("AccursedCurseProgression", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("You are cursed with misfortune and sorrow, and you cannot gain benefit from morale bonuses. " +
                    "However, you gain a +4 bonus to all saving throws against curse effects." +
                    "\nAt 5th level, add ill omen to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add bestow curse to your list of 4th-level oracle spells known. " +
                    "\nAt 15th level, you are immune to curse effects except for your own oracle curse.");
                bp.m_Icon = AccursedIcon;
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
                    Helpers.LevelEntry(1, AccursedCurseFeatureLevel1),
                    Helpers.LevelEntry(5, AccursedCurseFeatureLevel5),
                    Helpers.LevelEntry(10, AccursedCurseFeatureLevel10),
                    Helpers.LevelEntry(15, AccursedCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            AccursedCurseProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.CheckInProgression = true;
                c.m_Feature = AccursedCurseProgression.ToReference<BlueprintFeatureReference>();
                c.HideInUI = true;
            });
            var BeneficialAccursedCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("BeneficialAccursedCurseProgression", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("You are cursed with misfortune and sorrow, and you cannot gain benefit from morale bonuses. " +
                    "However, you gain a +4 bonus to all saving throws against curse effects." +
                    "\nAt 5th level, add ill omen to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add bestow curse to your list of 4th-level oracle spells known. " +
                    "\nAt 15th level, you are immune to curse effects except for your own oracle curse.");
                bp.m_Icon = AccursedIcon;
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
                    c.m_Feature = AccursedCurseProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AccursedCurseBeneficialFeatureLevel1),
                    Helpers.LevelEntry(5, AccursedCurseFeatureLevel5),
                    Helpers.LevelEntry(10, AccursedCurseFeatureLevel10),
                    Helpers.LevelEntry(15, AccursedCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var AccursedCurseNoProgression = Helpers.CreateBlueprint<BlueprintProgression>("AccursedCurseNoProgression", bp => {
                bp.SetName("Accursed");
                bp.SetDescription("You are cursed with misfortune and sorrow, and you cannot gain benefit from morale bonuses. " +
                    "However, you gain a +4 bonus to all saving throws against curse effects." +
                    "\nAt 5th level, add ill omen to your list of 2nd-level oracle spells known. " +
                    "\nAt 10th level, add bestow curse to your list of 4th-level oracle spells known. " +
                    "\nAt 15th level, you are immune to curse effects except for your own oracle curse.");
                bp.m_Icon = AccursedIcon;
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
                    Helpers.LevelEntry(1, AccursedCurseFeatureLevel1)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = AccursedCurseProgression.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
            });

            OracleCurseSelection.m_AllFeatures = OracleCurseSelection.m_AllFeatures.AppendToArray(AccursedCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_AllFeatures = BeneficialCurse.m_AllFeatures.AppendToArray(BeneficialAccursedCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_Features = BeneficialCurse.m_Features.AppendToArray(BeneficialAccursedCurseProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_AllFeatures = MysteryGiftFeatureCurseSelection.m_AllFeatures.AppendToArray(AccursedCurseNoProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_Features = MysteryGiftFeatureCurseSelection.m_Features.AppendToArray(AccursedCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_AllFeatures = DualCursedSecondCurseSelection.m_AllFeatures.AppendToArray(AccursedCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_Features = DualCursedSecondCurseSelection.m_Features.AppendToArray(AccursedCurseNoProgression.ToReference<BlueprintFeatureReference>());

        }
    }
}
