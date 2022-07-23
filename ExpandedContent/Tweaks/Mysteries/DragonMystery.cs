using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class DragonMystery {

        public static void AddDragonMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var BlindFight = Resources.GetBlueprint<BlueprintFeature>("4e219f5894ad0ea4daa0699e28c37b1d");
            
            //Spelllist
            var CauseFearSpell = Resources.GetBlueprint<BlueprintAbility>("bd81a3931aa285a4f9844585b5d97e51");
            var ResistEnergySpell = Resources.GetBlueprint<BlueprintAbility>("21ffef7791ce73f468b6fca4d9371e8b");
            var ResistEnergyCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("7bb0c402f7f789d4d9fae8ca87b4c7e2");
            var FearSpell = Resources.GetBlueprint<BlueprintAbility>("d2aeac47450c76347aebbc02e4f463e0");
            var SpellResistanceSpell = Resources.GetBlueprint<BlueprintAbility>("0a5ddfbcfb3989543ac7c936fc256889");
            var DispelMagicGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("f0f761b808dc4b149b08eaf44b99f633");
            var TrueSeeingSpell = Resources.GetBlueprint<BlueprintAbility>("4cf3d0fae3239ec478f51e86f49161cb");
            var TrueSeeingMassSpell = Resources.GetBlueprint<BlueprintAbility>("fa08cb49ade3eee42b5fd42bd33cb407");
            var FormOfTheDragonIIISpell = Resources.GetBlueprint<BlueprintAbility>("1cdc4ad4c208246419b98a35539eafa6");
            var OverwhelmingPresenceSpell = Resources.GetBlueprint<BlueprintAbility>("41cf93453b027b94886901dbfc680cb9");
            var OwlsWisdomSpell = Resources.GetBlueprint<BlueprintAbility>("f0455c9295b53904f9e02fc571dd2ce1");
            var RemoveBlindnessSpell = Resources.GetBlueprint<BlueprintAbility>("c927a8b0cd3f5174f8c0b67cdbfde539");
            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var OwlsWisdomMassSpell = Resources.GetBlueprint<BlueprintAbility>("9f5ada581af3db4419b54db77f44e430");
            var MindBlankSpell = Resources.GetBlueprint<BlueprintAbility>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var MindBlankMassSpell = Resources.GetBlueprint<BlueprintAbility>("87a29febd010993419f2a4a9bee11cfc");
            var OracleDragonSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CauseFearSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergySpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergyCommunalSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FearSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SpellResistanceSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingMassSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FormOfTheDragonIIISpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherDragonSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherDragonSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CauseFearSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveBlindnessSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomMassSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingMassSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankMassSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });            

            //Final Revelation (not done just here to stop errors)
            var OracleDragonFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonFinalRevelation");

            //Main Mystery Feature
            var OracleDragonMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonMysteryFeature", bp => {
                //bp.m_Icon = Waiting on Gnomes
                bp.SetName("Dragon");
                bp.SetDescription("An oracle with the bones mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Persuasion}Persuasion{/g}, " +
                    "{g|Encyclopedia:Perception}Perception{/g} and {g|Encyclopedia:Knowledge_Arcana}Knowledge (world) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleDragonFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleDragonSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.AddComponent<AddFeaturesFromSelectionToDescription>(c => {
                    //c.Introduction - Does this need filling in??
                    c.m_FeatureSelection = OracleRevelationSelection.ToReference<BlueprintFeatureSelectionReference>();
                    c.OnlyIfRequiresThisFeature = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherDragonMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherDragonMysteryFeature", bp => {
                //bp.m_Icon = Waiting on Gnomes
                bp.SetName("Dragon");
                bp.SetDescription("An oracle with the bones mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Persuasion}Persuasion{/g}, " +
                    "{g|Encyclopedia:Perception}Perception{/g} and {g|Encyclopedia:Knowledge_Arcana}Knowledge (world) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherDragonSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
            });
            //DivineHerbalistMystery
            var DivineHerbalistDragonMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistDragonMysteryFeature", bp => {
                //bp.m_Icon = Waiting on Gnomes
                bp.SetName("Dragon");
                bp.SetDescription("An oracle with the bones mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Persuasion}Persuasion{/g}, " +
                    "{g|Encyclopedia:Perception}Perception{/g} and {g|Encyclopedia:Knowledge_Arcana}Knowledge (world) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleDragonFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleDragonSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
            });

            //Wings of the Dragon
            var BuffWingsDraconicRed = Resources.GetBlueprint<BlueprintBuff>("08ae1c01155a2184db869e9ebedc758d");
            var BuffWingsDraconicBlue = Resources.GetBlueprint<BlueprintBuff>("800cde038f9e6304d95365edc60ab0a4");
            var BuffWingsDraconicGold = Resources.GetBlueprint<BlueprintBuff>("984064a3dd0f25444ad143b8a33d7d92");
            var BuffWingsDraconicGreen = Resources.GetBlueprint<BlueprintBuff>("a4ccc396e60a00f44907e95bc8bf463f");
            var BuffWingsDraconicBrass = Resources.GetBlueprint<BlueprintBuff>("7f5acae38fc1e0f4c9325d8a4f4f81fc");
            var BuffWingsDraconicWhite = Resources.GetBlueprint<BlueprintBuff>("381a168acd79cd54baf87a17ca861d9b");
            var BuffWingsDraconicBlack = Resources.GetBlueprint<BlueprintBuff>("ddfe6e85e1eed7a40aa911280373c228");
            var BuffWingsDraconicCopper = Resources.GetBlueprint<BlueprintBuff>("a25d6fc69cba80548832afc6c4787379");
            var BuffWingsDraconicBronze = Resources.GetBlueprint<BlueprintBuff>("482ee5d001527204bb86e34240e2ce65");
            var BuffWingsDraconicSilver = Resources.GetBlueprint<BlueprintBuff>("5a791c1b0bacee3459d7f5137fa0bd5f");
            var OracleRevelationWingsOfTheDragonResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationWingsOfTheDragonResource", bp => {

            });




            var OracleRevelationWingsOfTheDragonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationWingsOfTheDragonSelection", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.\nColour of the wings is selected when choosing this revelation.");





                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleDragonMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherDragonMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistDragonMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            //Dragon Sense Revelation
            var OracleDragonSensesBlindsense = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonSensesBlindsense", bp => {
                bp.SetName("Dragon Senses");
                bp.SetDescription("");
                bp.AddComponent<Blindsense>(c => {
                    c.Range = 30.Feet();
                });
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonSenses = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonSenses", bp => {
                bp.SetName("Dragon Senses");
                bp.SetDescription("Your senses take on a keen draconic edge. You gain a +2 bonus on {g|Encyclopedia:Perception}Perception{/g} checks. At 5th level gain a " +
                    "blindsense of 30 feet allowing you to locate unseen foes. At 11th level gain the Blind Fight feat. At 15th level " +
                    "your {g|Encyclopedia:Perception}Perception{/g} check bonus increases to +4.");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueShared = AbilitySharedValue.Damage,
                        ValueRank = AbilityRankType.Default,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { OracleClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem() {
                            BaseValue = 14,
                            ProgressionValue = 2
                        },
                        new ContextRankConfig.CustomProgressionItem() {
                            BaseValue = 100,
                            ProgressionValue = 4
                        }
                    };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 5;
                    c.m_Feature = OracleDragonSensesBlindsense.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BlindFight.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleDragonMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherDragonMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistDragonMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });

            //Scaled Toughness Revelation
            var OracleRevelationScaledToughnessResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationScaledToughnessResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep =true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    StartingLevel = 7,
                    LevelStep = 6,
                    StartingIncrease = 1,
                    PerStepIncrease = 1
                };
                bp.m_UseMax = true;
                bp.m_Max = 2;
            });
            var OracleRevelationScaledToughnessBuff = Helpers.CreateBlueprint<BlueprintBuff>("OracleRevelationScaledToughnessBuff", bp => {
                bp.SetName("Scaled Toughness");
                bp.SetDescription("You can manifest the scaly toughness of dragonkind. Once per day as a swift action you can harden your skin, " +
                    "granting you DR 10/magic. During this time, you are also immune to paralysis and sleep effects. This effect lasts for a number " +
                    "of rounds equal to your oracle level. At 13th level, you can use this ability twice per day. You must be at least 7th level to " +
                    "select this revelation.");
                //bp.m_Icon = ???
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 10;
                    c.BypassedByMagic = true;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Paralyzed;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Sleeping;
                });
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var OracleRevelationScaledToughnessAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationScaledToughnessAbility", bp => {
                bp.SetName("Scaled Toughness");
                bp.SetDescription("You can manifest the scaly toughness of dragonkind. Once per day as a swift action you can harden your skin, " +
                    "granting you DR 10/magic. During this time, you are also immune to paralysis and sleep effects. This effect lasts for a number " +
                    "of rounds equal to your oracle level. At 13th level, you can use this ability twice per day. You must be at least 7th level to " +
                    "select this revelation.");
                //bp.m_Icon = ???
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationScaledToughnessBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = true,
                            ToCaster = true,
                            AsChild = false,
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.BonusValue;
                    c.m_StepLevel = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationScaledToughnessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationScaledToughness = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationScaledToughness", bp => {
                bp.SetName("Scaled Toughness");
                bp.SetDescription("You can manifest the scaly toughness of dragonkind. Once per day as a swift action you can harden your skin, " +
                    "granting you DR 10/magic. During this time, you are also immune to paralysis and sleep effects. This effect lasts for a number " +
                    "of rounds equal to your oracle level. At 13th level, you can use this ability twice per day. You must be at least 7th level to " +
                    "select this revelation.");
                //bp.m_Icon = ???
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationScaledToughnessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationScaledToughnessAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleDragonMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherDragonMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistDragonMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
        }
    }
}
