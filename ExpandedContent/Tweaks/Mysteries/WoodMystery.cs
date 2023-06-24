using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.Formations.Facts;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class WoodMystery {
        public static void AddWoodMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var WoodMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleWoodMystery.png");


            //Spelllist
            var ShillelaghAbility = Resources.GetModBlueprint<BlueprintAbility>("ShillelaghAbility");
            var BarkSkinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5b77d7cc65b8ab74688e74a37fc2f553");
            var BurningEntangleSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("8a76293f5ab8485da95ef6293a11358c");
            var ThornBodySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2daf9c5112f16d54ab3cd6904c705c59");
            var PlantShapeIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIAbility");
            var PlantShapeIIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIAbility");
            var PlantShapeIIIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIIAbility");
            var ChangeStaffSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("26be70c4664d07446bdfe83504c1d757");
            var WoodenPhalanxAbility = Resources.GetModBlueprint<BlueprintAbility>("WoodenPhalanxAbility");
            var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
            var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
            var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
            var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
            var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
            var MindBlankSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var MindBlankMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("87a29febd010993419f2a4a9bee11cfc");
            var TrueSeeingSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4cf3d0fae3239ec478f51e86f49161cb");
            var TrueSeeingMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fa08cb49ade3eee42b5fd42bd33cb407");
            var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
            var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
            var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
            var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
            var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
            var OracleWoodSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleWoodSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShillelaghAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BarkSkinSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BurningEntangleSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ThornBodySpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PlantShapeIAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PlantShapeIIAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PlantShapeIIIAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChangeStaffSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WoodenPhalanxAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherWoodSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherWoodSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShillelaghAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveBlindnessSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomMassSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingMassSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankMassSpell;
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoWoodSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWoodSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShillelaghAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BurningEntangleSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChangeStaffSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WoodenPhalanxAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var EnlightenedPhilosopherFinalRevelationBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("9f1ee3c61ef993d448b0b866ee198ea8");
            var EnlightenedPhilosopherFinalRevelationResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d19c2e7ec505b734a973ce8d0986f4d6");            
            var OracleWoodFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleWoodFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become a living creature of wood. You gain a +4 natural armor bonus to your Armor Class and you gain immunity to paralysis, poison, " +
                    "polymorph, sleep, and stunning.");
                bp.AddComponent(PlantType.GetComponent<AddFacts>());
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.AC;
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Value = 4;
                });                
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            }); //Finish please 
            //Main Mystery Feature
            var OracleWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("An oracle with the wood mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, " +
                    "{g|Encyclopedia:Stealth}Stealth{/g}  and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWoodFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleWoodSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("An oracle with the wood mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, " +
                    "{g|Encyclopedia:Stealth}Stealth{/g}  and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherWoodSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("Gain access to the spells and revelations of the wood mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWoodFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleWoodSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("Gain access to the spells and revelations of the wood mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWoodFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoWoodSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //RavenerHunter
            var RavenerHunterWoodMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterWoodMysteryProgression", bp => {
                bp.SetName("Wood");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = WoodMysteryIcon;
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                //LevelEntry added later                
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //WoodArmor
            var OracleRevelationWoodArmorResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationWoodArmorResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    LevelIncrease = 1,
                    StartingIncrease = 1,
                };
            });
            var OracleRevelationWoodArmorDCBuff = Helpers.CreateBuff("OracleRevelationWoodArmorDCBuff", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 4,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Heal,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Armor;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
            });
            var OracleRevelationWoodArmorDRBuff = Helpers.CreateBuff("OracleRevelationWoodArmorDRBuff", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 4,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Heal,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Armor;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.UsePool = false;
                    c.Pool = new ContextValue();
                    c.Or = false;
                    c.BypassedByMaterial = false;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.BypassedByForm = true;
                    c.Form = PhysicalDamageForm.Slashing;
                    c.BypassedByMagic = false;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = false;
                    c.Alignment = DamageAlignment.Good;
                    c.BypassedByReality = false;
                    c.Reality = DamageRealityType.Ghost;
                    c.BypassedByWeaponType = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.BypassedByEpic = false;
                    c.m_CheckedFactMythic = new BlueprintUnitFactReference();
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
            });
            var OracleRevelationWoodArmorDCAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWoodArmorDCAbility", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = OracleRevelationWoodArmorResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationWoodArmorDCBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationWoodArmorDRAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWoodArmorDRAbility", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = OracleRevelationWoodArmorResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationWoodArmorDRBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationWoodArmorDC = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodArmorDC", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWoodArmorDCAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWoodArmorDR = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodArmorDR", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWoodArmorDRAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWoodArmorFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodArmorFeature", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 13;
                    c.m_Feature = OracleRevelationWoodArmorDC.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>()
                    }; c.Level = 13;
                    c.m_Feature = OracleRevelationWoodArmorDR.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWoodArmorResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            //WoodBond
            var OracleRevelationWoodBondFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodBondFeature", bp => {
                bp.SetName("Wood Bond");
                bp.SetDescription("Your mystical bond with wood is such that your weapons become an extension of your body. You gain a +1 competence bonus on attack rolls when " +
                    "wielding a weapon made of or mostly consisting of wood (such as a bow, club, quarterstaff, or spear). This bonus increases by +1 at 5th level and every five " +
                    "levels thereafter.");
                bp.m_Icon = SenseVitals.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<WeaponMultipleCategoriesContextAttackBonus>(c => {                    
                    c.Descriptor = ModifierDescriptor.Circumstance;
                    c.Categories = new WeaponCategory[] { 
                        WeaponCategory.Greatclub | 
                        WeaponCategory.Club | 
                        WeaponCategory.Javelin | 
                        WeaponCategory.Kama | 
                        WeaponCategory.Longbow | 
                        WeaponCategory.Longspear | 
                        WeaponCategory.Nunchaku | 
                        WeaponCategory.Quarterstaff | 
                        WeaponCategory.Shortbow | 
                        WeaponCategory.Shortspear | 
                        WeaponCategory.SlingStaff | 
                        WeaponCategory.Spear | 
                        WeaponCategory.Trident |
                        WeaponCategory.LightCrossbow |
                        WeaponCategory.HeavyCrossbow
                    };
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.ExceptForCategories = false;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 5;
                    c.m_Class = new BlueprintCharacterClassReference[] { 
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
            });
            //TreeForm


            //WoddenWeaponEnchant

            //ThornBurst




            //Ravener Hunter Cont.
            var RavenerHunterWoodRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterWoodRevelationSelection", bp => {
                bp.SetName("Wood Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(OracleRevelationWoodArmorFeature, OracleRevelationWoodBondFeature, OracleRevelationGuidingStarFeature, OracleRevelationInterstallarVoidFeature, 
                   OracleRevelationLureOfTheWoodFeature, OracleRevelationSprayOfShootingStarsFeature);
            });
            RavenerHunterWoodMysteryProgression.LevelEntries = new LevelEntry[] {
                 Helpers.LevelEntry(1, RavenerHunterWoodRevelationSelection),
                 Helpers.LevelEntry(8, RavenerHunterWoodRevelationSelection)
            };
            RavenerHunterWoodMysteryProgression.UIGroups = new UIGroup[] {
                Helpers.CreateUIGroup(RavenerHunterWoodRevelationSelection, RavenerHunterWoodRevelationSelection)
            };
            var RavenerHunterChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("RavenerHunterChargedByNatureSelection");
            var SecondChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("SecondChargedByNatureSelection");
            RavenerHunterChargedByNatureSelection.m_AllFeatures = RavenerHunterChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>());
            SecondChargedByNatureSelection.m_AllFeatures = SecondChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>());

            MysteryTools.RegisterMystery(OracleWoodMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleWoodMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherWoodMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherWoodMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistWoodMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistWoodMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoWoodMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoWoodMysteryFeature);
        }
    }
}
