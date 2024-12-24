using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Enums;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics;
using static TabletopTweaks.Core.MechanicsChanges.AdditionalModifierDescriptors;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Buffs.Blueprints;


namespace ExpandedContent.Tweaks.Curses {
    internal class DeepOne {

        public static void AddDeepOneCurse() {

            var OracleCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b0a5118b4fb793241bc7042464b23fab");
            var BeneficialCurse = Resources.GetBlueprint<BlueprintFeatureSelection>("2dda67424ee8e0b4d83ef01a73ca6bff");
            var MysteryGiftFeatureCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4e7265c0ae1345db90d3375f4ced94cc");
            var DualCursedSecondCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cc6fda79e8c340b88c84689414a9abbe");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var AccursedWitchArchetype = Resources.GetBlueprint<BlueprintArchetype>("c5f6e53e71059fb4d802ce81a277a12d");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var SeamantleSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7ef49f184922063499b8f1346fb7f521");
            var ElementalBodyWater1Spell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2d46a6f1dbef51f4c9e14fc163ee4124");
            var ElementalBodyWater2Spell = Resources.GetBlueprintReference<BlueprintAbilityReference>("935b63be93800394f8f7ae17060b041a");
            var ElementalBodyWater3Spell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c82a0d6472794d245a186eff5d6f0f41");
            var ElementalBodyWater4Spell = Resources.GetBlueprintReference<BlueprintAbilityReference>("96d2ab91f2d2329459a8dab496c5bede");
            var SeamantleBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("1c05dd3a1c78b0e4e9f7438a43e7a9fd");
            var ElementalBodyWater1Buff = Resources.GetBlueprintReference<BlueprintBuffReference>("37cad1e28c452ec48810cfcf342bffd7");
            var ElementalBodyWater2Buff = Resources.GetBlueprintReference<BlueprintBuffReference>("7c547fe47c399fe429e574f86d2b7618");
            var ElementalBodyWater3Buff = Resources.GetBlueprintReference<BlueprintBuffReference>("e24ea1f5005649846b798318b5238e34");
            var ElementalBodyWater4Buff = Resources.GetBlueprintReference<BlueprintBuffReference>("f0abf98bb3bce4f4e877a8e8c2eccf41");
            var FreedomOfMovementBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("1533e782fca42b84ea370fc1dcbf4fc1");
            var DeepOneIcon = AssetLoader.LoadInternal("Skills", "Icon_DeepOneCurse.png");


            var DeepOneCurseFeatureLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("DeepOneCurseFeatureLevel1", bp => {
                bp.SetName("Deep One");
                bp.SetDescription("The lure of the ocean tugs at your soul. \nYou reduce your base land speed by 5 feet.");
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = -5;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var DeepOneCurseFeatureLevel5 = Helpers.CreateBlueprint<BlueprintFeature>("DeepOneCurseFeatureLevel5", bp => {
                bp.SetName("Deep One");
                bp.SetDescription("Your natural armor bonus increases by 1 as your skin thickens.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.AC;
                    c.Descriptor = (ModifierDescriptor)NaturalArmor.Stackable;
                    c.Value = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var DeepOneCurseFeatureLevel10 = Helpers.CreateBlueprint<BlueprintFeature>("DeepOneCurseFeatureLevel10", bp => {
                bp.SetName("Deep One");
                bp.SetDescription("You gain a +1 bonus to your caster level when casting spells with the cold descriptor.");
                bp.AddComponent<IncreaseSpellDescriptorCasterLevel>(c => {
                    c.BonusCasterLevel = 1;
                    c.Descriptor = SpellDescriptor.Cold;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var DeepOneCurseFeatureLevel15 = Helpers.CreateBlueprint<BlueprintFeature>("DeepOneCurseFeatureLevel15", bp => {
                bp.SetName("Deep One");
                bp.SetDescription("At 15th level, add elemental body - water (I,II,III,IV) to your list of oracle spells known. " +
                    "While under the effects of these spells or the seamantle spell, gain the benefits of the freedom of movement spell.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater1Spell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater1Spell;
                    c.SpellLevel = 4;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater2Spell;
                    c.SpellLevel = 5;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater2Spell;
                    c.SpellLevel = 5;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater3Spell;
                    c.SpellLevel = 6;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater3Spell;
                    c.SpellLevel = 6;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater4Spell;
                    c.SpellLevel = 7;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = ElementalBodyWater4Spell;
                    c.SpellLevel = 7;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<BuffExtraEffects>(c => {
                    c.m_CheckedBuff = SeamantleBuff;
                    c.m_CheckedBuffList = new BlueprintBuffReference[] { 
                        ElementalBodyWater1Buff,
                        ElementalBodyWater2Buff,
                        ElementalBodyWater3Buff,
                        ElementalBodyWater4Buff
                    };
                    c.m_ExtraEffectBuff = FreedomOfMovementBuff;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var DeepOneCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("DeepOneCurseProgression", bp => {
                bp.SetName("Deep One");
                bp.SetDescription("The lure of the ocean tugs at your soul. \nYou reduce your base land speed by 5 feet. " +
                    "\nAt 5th level, your natural armor bonus increases by 1 as your skin thickens. " +
                    "\nAt 10th level, you gain a +1 bonus to your caster level when casting spells with the cold descriptor. " +
                    "\nAt 15th level, add elemental body - water (I,II,III,IV) to your list of oracle spells known. " +
                    "While under the effects of these spells or the seamantle spell, gain the benefits of the freedom of movement spell.");
                bp.m_Icon = DeepOneIcon;
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
                    Helpers.LevelEntry(1, DeepOneCurseFeatureLevel1),
                    Helpers.LevelEntry(5, DeepOneCurseFeatureLevel5),
                    Helpers.LevelEntry(10, DeepOneCurseFeatureLevel10),
                    Helpers.LevelEntry(15, DeepOneCurseFeatureLevel15)
                };                
                bp.GiveFeaturesForPreviousLevels = true;            
            });
            DeepOneCurseProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.CheckInProgression = true;
                c.m_Feature = DeepOneCurseProgression.ToReference<BlueprintFeatureReference>();
                c.HideInUI = true;
            });
            var BeneficialDeepOneCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("BeneficialDeepOneCurseProgression", bp => {
                bp.SetName("Deep One");
                bp.SetDescription("The lure of the ocean tugs at your soul. \nYou reduce your base land speed by 5 feet. " +
                    "\nAt 5th level, your natural armor bonus increases by 1 as your skin thickens. " +
                    "\nAt 10th level, you gain a +1 bonus to your caster level when casting spells with the cold descriptor. " +
                    "\nAt 15th level, add elemental body - water (I,II,III,IV) to your list of oracle spells known. " +
                    "\nWhile under the effects of these spells or the seamantle spell, gain the benefits of the freedom of movement spell.");
                bp.m_Icon = DeepOneIcon;
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
                    c.m_Feature = DeepOneCurseProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.LevelEntries = new LevelEntry[] {
                    //Helpers.LevelEntry(1, DeepOneCurseFeatureLevel1),
                    Helpers.LevelEntry(5, DeepOneCurseFeatureLevel5),
                    Helpers.LevelEntry(10, DeepOneCurseFeatureLevel10),
                    Helpers.LevelEntry(15, DeepOneCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var DeepOneCurseNoProgression = Helpers.CreateBlueprint<BlueprintProgression>("DeepOneCurseNoProgression", bp => {
                bp.SetName("Deep One");
                bp.SetDescription("The lure of the ocean tugs at your soul. \nYou reduce your base land speed by 5 feet. " +
                    "\nAt 5th level, your natural armor bonus increases by 1 as your skin thickens. " +
                    "\nAt 10th level, you gain a +1 bonus to your caster level when casting spells with the cold descriptor. " +
                    "\nAt 15th level, add elemental body - water (I,II,III,IV) to your list of oracle spells known. " +
                    "\nWhile under the effects of these spells or the seamantle spell, gain the benefits of the freedom of movement spell.");
                bp.m_Icon = DeepOneIcon;
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
                    Helpers.LevelEntry(1, DeepOneCurseFeatureLevel1)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = DeepOneCurseProgression.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
            });

            OracleCurseSelection.m_AllFeatures = OracleCurseSelection.m_AllFeatures.AppendToArray(DeepOneCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_AllFeatures = BeneficialCurse.m_AllFeatures.AppendToArray(BeneficialDeepOneCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_Features = BeneficialCurse.m_Features.AppendToArray(BeneficialDeepOneCurseProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_AllFeatures = MysteryGiftFeatureCurseSelection.m_AllFeatures.AppendToArray(DeepOneCurseNoProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_Features = MysteryGiftFeatureCurseSelection.m_Features.AppendToArray(DeepOneCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_AllFeatures = DualCursedSecondCurseSelection.m_AllFeatures.AppendToArray(DeepOneCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_Features = DualCursedSecondCurseSelection.m_Features.AppendToArray(DeepOneCurseNoProgression.ToReference<BlueprintFeatureReference>());

        }
    }
}
