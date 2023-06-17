using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
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
    internal class HeavensMystery {
        public static void AddHeavensMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");

            var HeavensMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleHeavensMystery.png");

            //Spelllist
            var ColorSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("91da41b9793a4624797921f221db653c");
            var RainbowPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4b8265132f9c8174f87ce7fa6d0fe47b");
            var PrismaticSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b22fd434bdb60fb4ba1068206402c4cf");
            var ChainLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("645558d63604747428d55f0dd3a4cb58");
            var SearingLightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("bf0accce250381a44b857d4af6c8e10d");
            var HypnoticPatternAbility = Resources.GetModBlueprint<BlueprintAbility>("HypnoticPatternAbility");
            var SunburstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e96424f70ff884947b06f41a765b7658");
            var PolarMidnightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ba48abb52b142164eba309fd09898856");
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
            var OracleHeavensSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleHeavensSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ColorSpraySpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HypnoticPatternAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SearingLightSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RainbowPatternSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BreakEnchantmentSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChainLightningSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PrismaticSpraySpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SunburstSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolarMidnightSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherHeavensSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherHeavensSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ColorSpraySpell;
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
            var OceansEchoHeavensSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoHeavensSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ColorSpraySpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SearingLightSpell;
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
                    c.m_Spell = SunburstSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolarMidnightSpell;
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var EnlightenedPhilosopherFinalRevelationBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("9f1ee3c61ef993d448b0b866ee198ea8");
            var EnlightenedPhilosopherFinalRevelationResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d19c2e7ec505b734a973ce8d0986f4d6");
            var OracleHeavensFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleHeavensFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon achieving 20th level, your rapport with the heavens grants you perfect harmony with the universe. You receive a {g|Encyclopedia:Bonus}bonus{/g} " +
                    "on all {g|Encyclopedia:Saving_Throw}saving throws{/g} equal to your {g|Encyclopedia:Charisma}Charisma{/g} modifier. You become immune to fear effects, and automatically " +
                    "confirm all critical hits. Once per day, should you die, 1 {g|Encyclopedia:Combat_Round}round{/g} after dying you are reborn. Your body re-forms with all your equipment, " +
                    "and you return to life with maximum {g|Encyclopedia:HP}hit points{/g}.");
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveFortitude;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveWill;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveReflex;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Frightened;
                });
                bp.AddComponent<InitiatorCritAutoconfirm>();
                bp.AddComponent<DeathActions>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = EnlightenedPhilosopherFinalRevelationBuff,
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0                            
                        }
                        );
                    c.CheckResource = true;
                    c.OnlyOnParty = false;
                    c.m_Resource = EnlightenedPhilosopherFinalRevelationResource;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = EnlightenedPhilosopherFinalRevelationResource;
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Main Mystery Feature
            var OracleHeavensMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleHeavensMysteryFeature", bp => {
                bp.m_Icon = HeavensMysteryIcon;
                bp.SetName("Heavens");
                bp.SetDescription("An oracle with the heavens mystery adds {g|Encyclopedia:Perception}Perception{/g} and {g|Encyclopedia:Knowledge_Arcana}Knowledge (Arcana) {/g} to " +
                    "her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleHeavensFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleHeavensSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherHeavensMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherHeavensMysteryFeature", bp => {
                bp.m_Icon = HeavensMysteryIcon;
                bp.SetName("Heavens");
                bp.SetDescription("An oracle with the heavens mystery adds {g|Encyclopedia:Perception}Perception{/g} and {g|Encyclopedia:Knowledge_Arcana}Knowledge (Arcana) {/g} to " +
                    "her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherHeavensSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistHeavensMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistHeavensMysteryFeature", bp => {
                bp.m_Icon = HeavensMysteryIcon;
                bp.SetName("Heavens");
                bp.SetDescription("Gain access to the spells and revelations of the heavens mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleHeavensFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleHeavensSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoHeavensMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoHeavensMysteryFeature", bp => {
                bp.m_Icon = HeavensMysteryIcon;
                bp.SetName("Heavens");
                bp.SetDescription("Gain access to the spells and revelations of the heavens mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleHeavensFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoHeavensSpells.ToReference<BlueprintFeatureReference>();
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
            var RavenerHunterHeavensMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterHeavensMysteryProgression", bp => {
                bp.SetName("Heavens");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = HeavensMysteryIcon;
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
            //Awesome Display
            var ScintillatingPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4dc60d08c6c4d3c47b413904e4de5ff0");
            var OracleRevelationAwesomeDisplay = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationAwesomeDisplay", bp => {
                bp.SetName("Awesome Display");
                bp.SetDescription("Your phantasmagoric displays accurately model the mysteries of the night sky, dumbfounding all who behold them. The spell DC of all Pattern spells are increased by 2, " +
                    "additionally you gain access to scintillating pattern as a 8th level spell. (You still need to be able to cast 8th level spells to cast scintillating pattern) \nPattern spells: color " +
                    "spray, hypnotic pattern, rainbow pattern, scintillating pattern.");
                bp.AddComponent<IncreaseSpellDC>(c => {
                    c.m_Spell = ColorSpraySpell;
                    c.HalfMythicRank = false;
                    c.UseContextBonus = false;
                    c.Value = new ContextValue();
                    c.BonusDC = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<IncreaseSpellDC>(c => {
                    c.m_Spell = RainbowPatternSpell;
                    c.HalfMythicRank = false;
                    c.UseContextBonus = false;
                    c.Value = new ContextValue();
                    c.BonusDC = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<IncreaseSpellDC>(c => {
                    c.m_Spell = ScintillatingPatternSpell;
                    c.HalfMythicRank = false;
                    c.UseContextBonus = false;
                    c.Value = new ContextValue();
                    c.BonusDC = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<IncreaseSpellDC>(c => {
                    c.m_Spell = HypnoticPatternAbility.ToReference<BlueprintAbilityReference>();
                    c.HalfMythicRank = false;
                    c.UseContextBonus = false;
                    c.Value = new ContextValue();
                    c.BonusDC = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ScintillatingPatternSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationAwesomeDisplay.ToReference<BlueprintFeatureReference>());
            //Coat of Many Stars
            var EdictOfImpenetrableFortress = Resources.GetBlueprint<BlueprintAbility>("d7741c08ccf699e4a8a8f8ab2ed345f8");
            var OracleRevelationCoatOfManyStarsResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationCoatOfManyStarsResource", bp => {
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
            var OracleRevelationCoatOfManyStarsDCBuff = Helpers.CreateBuff("OracleRevelationCoatOfManyStarsDCBuff", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g}. At 7th level, and every four " +
                    "levels thereafter, this bonus increases by +2. At 13th level, this armor grants you {g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. " +
                    "You can use this coat for 1 hour number of times per day equal to oracle level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
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
            var OracleRevelationCoatOfManyStarsDRBuff = Helpers.CreateBuff("OracleRevelationCoatOfManyStarsDRBuff", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g}. At 7th level, and every four " +
                    "levels thereafter, this bonus increases by +2. At 13th level, this armor grants you {g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. " +
                    "You can use this coat for 1 hour number of times per day equal to oracle level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
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
            var OracleRevelationCoatOfManyStarsDCAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationCoatOfManyStarsDCAbility", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g}. At 7th level, and every four " +
                    "levels thereafter, this bonus increases by +2. At 13th level, this armor grants you {g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. " +
                    "You can use this coat for 1 hour number of times per day equal to oracle level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = OracleRevelationCoatOfManyStarsResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationCoatOfManyStarsDCBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationCoatOfManyStarsDRAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationCoatOfManyStarsDRAbility", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g}. At 7th level, and every four " +
                    "levels thereafter, this bonus increases by +2. At 13th level, this armor grants you {g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. " +
                    "You can use this coat for 1 hour number of times per day equal to oracle level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = OracleRevelationCoatOfManyStarsResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationCoatOfManyStarsDRBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationCoatOfManyStarsDC = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationCoatOfManyStarsDC", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationCoatOfManyStarsDCAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationCoatOfManyStarsDR = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationCoatOfManyStarsDR", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationCoatOfManyStarsDRAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationCoatOfManyStarsFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationCoatOfManyStarsFeature", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g}. At 7th level, and every four " +
                    "levels thereafter, this bonus increases by +2. At 13th level, this armor grants you {g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. " +
                    "You can use this coat for 1 hour number of times per day equal to oracle level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 13;
                    c.m_Feature = OracleRevelationCoatOfManyStarsDC.ToReference<BlueprintFeatureReference>();
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
                    c.m_Feature = OracleRevelationCoatOfManyStarsDR.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationCoatOfManyStarsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationCoatOfManyStarsFeature.ToReference<BlueprintFeatureReference>());
            //Dweller in Darkness
            var UndeadType = Resources.GetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");
            var UndeadMindAffection = Resources.GetBlueprint<BlueprintFeature>("7853143d87baea1429bb409b023edb6b");
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var PhantasmalKillerSpell = Resources.GetBlueprint<BlueprintAbility>("6717dbaef00c0eb4897a1c908a75dfe5");
            var WeirdSpell = Resources.GetBlueprint<BlueprintAbility>("870af83be6572594d84d276d7fc583e0");
            var Stunned = Resources.GetBlueprintReference<BlueprintBuffReference>("09d39b38bb7c6014394b6daced9bacd3");
            var OracleRevelationDwellerInTheDarknessResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationDwellerInTheDarknessResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            var OracleRevelationDwellerInTheDarknessAbility1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDwellerInTheDarknessAbility1", bp => {
                bp.SetName("Dweller in the Darkness - Phantasmal Killer");
                bp.SetDescription("Once per day, you cast your psyche into the void of space to attract the attention of a terrible otherworldly being. \nPhantasmal Killer: You create a phantasmal image of " +
                    "the most fearsome creature imaginable to the subject simply by forming the fears of the subject's subconscious mind into something that its conscious mind can visualize: this most horrible " +
                    "beast. Only the spell's subject can see the phantasmal killer. You see only a vague shape. The target first gets a Will save to recognize the image as unreal. If that save fails, the phantasm " +
                    "touches the subject, and the subject must succeed on a Fortitude save or die from fear. Even if the Fortitude save is successful, the subject takes 3d6 points of damage.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionSavingThrow() { 
                                    Type = SavingThrowType.Fortitude,
                                    FromBuff = false,
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(
                                                new ContextActionDealDamage() {
                                                    m_Type = ContextActionDealDamage.Type.Damage,
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData() {
                                                            Reality = 0,
                                                            Alignment = 0,
                                                            Precision = false
                                                        },
                                                        Physical = new DamageTypeDescription.PhysicalData() {
                                                            Material = 0,
                                                            Form = 0,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.Magic
                                                    },
                                                    Drain = false,
                                                    AbilityType = StatType.Unknown,
                                                    EnergyDrainType = EnergyDrainType.Temporary,
                                                    Duration = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        m_IsExtendable = true,
                                                    },
                                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.D6,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 3,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                    },
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                }),
                                            Failed = Helpers.CreateActionList(
                                                new ContextActionKill() {
                                                    Dismember = UnitState.DismemberType.None
                                                })
                                        })
                                }
                            )
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDwellerInTheDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "c44b16795a67d6748a95b365b6e2274a" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<AbilityTargetHasNoFactUnless>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { UndeadType.ToReference<BlueprintUnitFactReference>() };
                    c.m_UnlessFact = UndeadMindAffection.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { ConstructType.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Illusion;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Fear | SpellDescriptor.Emotion | SpellDescriptor.NegativeEmotion;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = PhantasmalKillerSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.Quicken | Metamagic.Empower | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("InflictPainAbility.SavingThrow", "Will disbelief, then Fortitude partial");
            });
            var OracleRevelationDwellerInTheDarknessAbility2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDwellerInTheDarknessAbility2", bp => {
                bp.SetName("Dweller in the Darkness - Weird");
                bp.SetDescription("Once per day, you cast your psyche into the void of space to attract the attention of a terrible otherworldly being. \nThis {g|Encyclopedia:Spell}spell{/g} functions like " +
                    "phantasmal killer, except it can affect more than one creature. Only the affected creatures see the phantasmal creatures {g|Encyclopedia:Attack}attacking{/g} them, though you see the attackers " +
                    "as shadowy shapes. If a subject's {g|Encyclopedia:Saving_Throw}Fortitude save{/g} succeeds, it still takes {g|Encyclopedia:Dice}3d6{/g} points of {g|Encyclopedia:Damage}damage{/g} and is " +
                    "stunned for 1 {g|Encyclopedia:Combat_Round}round{/g}. The subject also takes 1d4 points of {g|Encyclopedia:Strength}Strength{/g} damage. \nPhantasmal Killer: You create a phantasmal image of " +
                    "the most fearsome creature imaginable to the subject simply by forming the fears of the subject's subconscious mind into something that its conscious mind can visualize: this most horrible " +
                    "beast. Only the spell's subject can see the phantasmal killer. You see only a vague shape. The target first gets a Will save to recognize the image as unreal. If that save fails, the phantasm " +
                    "touches the subject, and the subject must succeed on a Fortitude save or die from fear. Even if the Fortitude save is successful, the subject takes 3d6 points of damage.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    Type = SavingThrowType.Fortitude,
                                    FromBuff = false,
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(
                                                new ContextActionDealDamage() {
                                                    m_Type = ContextActionDealDamage.Type.Damage,
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData() {
                                                            Reality = 0,
                                                            Alignment = 0,
                                                            Precision = false
                                                        },
                                                        Physical = new DamageTypeDescription.PhysicalData() {
                                                            Material = 0,
                                                            Form = 0,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.Magic
                                                    },
                                                    Drain = false,
                                                    AbilityType = StatType.Unknown,
                                                    EnergyDrainType = EnergyDrainType.Temporary,
                                                    Duration = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        m_IsExtendable = true,
                                                    },
                                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.D6,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 3,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                    },
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                },
                                                new ContextActionApplyBuff() {
                                                    m_Buff = Stunned,
                                                    Permanent = false,
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = new ContextValue(),
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 1,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        m_IsExtendable = true
                                                    },
                                                    IsFromSpell = false,
                                                },
                                                new ContextActionDealDamage() {
                                                    m_Type = ContextActionDealDamage.Type.AbilityDamage,
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Physical,
                                                        Common = new DamageTypeDescription.CommomData() {
                                                            Reality = 0,
                                                            Alignment = 0,
                                                            Precision = false
                                                        },
                                                        Physical = new DamageTypeDescription.PhysicalData() {
                                                            Material = 0,
                                                            Form = PhysicalDamageForm.Slashing,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.Fire
                                                    },
                                                    Drain = false,
                                                    AbilityType = StatType.Strength,
                                                    EnergyDrainType = EnergyDrainType.Temporary,
                                                    Duration = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        m_IsExtendable = true,
                                                    },
                                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.D4,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 1,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                    },
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                }
                                                ),
                                            Failed = Helpers.CreateActionList(
                                                new ContextActionKill() {
                                                    Dismember = UnitState.DismemberType.None
                                                })
                                        })
                                }
                            )
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDwellerInTheDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "c44b16795a67d6748a95b365b6e2274a" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Enemy;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Illusion;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Fear | SpellDescriptor.Emotion | SpellDescriptor.NegativeEmotion;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = WeirdSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.Quicken | Metamagic.Empower | Metamagic.Bolstered | Metamagic.Selective;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("InflictPainAbility.SavingThrow", "Will disbelief, then Fortitude partial");
            });
            var OracleRevelationDwellerInTheDarknessUpgrade = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDwellerInTheDarknessUpgrade", bp => {
                bp.SetName("Dweller in the Darkness");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDwellerInTheDarknessAbility2.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDwellerInTheDarknessAbility2.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });

                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDwellerInTheDarknessFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDwellerInTheDarknessFeature", bp => {
                bp.SetName("Dweller in the Darkness");
                bp.SetDescription("Once per day, you cast your psyche into the void of space to attract the attention of a terrible otherworldly being. The dweller in darkness behaves as if you had " +
                    "cast phantasmal killer. At 17th level, the dweller in darkness can be perceived by more than one creature, as if you had cast weird. You must be at least 11th level to choose " +
                    "this revelation.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDwellerInTheDarknessAbility1.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDwellerInTheDarknessAbility1.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 17;
                    c.m_Feature = OracleRevelationDwellerInTheDarknessUpgrade.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDwellerInTheDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoHeavensMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDwellerInTheDarknessFeature.ToReference<BlueprintFeatureReference>());
            //Guiding Star
            var GuidingStarIcon = AssetLoader.LoadInternal("Skills", "Icon_GuidingStar.png");
            var OracleSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("6c03364712b415941a98f74522a81273");
            var RavenerHunterSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("RavenerHunterSpellbook");
            var OracleRevelationGuidingStarSkillBuff = Helpers.CreateBuff("OracleRevelationGuidingStarSkillBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillLoreNature;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillLoreReligion;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillPerception;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationGuidingStarSkillFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGuidingStarSkillFeature", bp => {
                bp.SetName("Guiding Star - Skill Bonus");
                bp.SetDescription("The stars themselves hold many answers, you may add your Charisma modifier to your Wisdom modifier on all Wisdom-based checks.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationGuidingStarSkillBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationGuidingStarResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationGuidingStarResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            var OracleRevelationGuidingStarMetamagicBuffEmpower = Helpers.CreateBuff("OracleRevelationGuidingStarMetamagicBuffEmpower", bp => {
                bp.SetName("Guiding Star - Empower Spell");
                bp.SetDescription("Once per day you can cast one spell from the oracle spellbook as if it were modified by the Empower Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Empower;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = true;
                    c.m_Spellbook = OracleSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Empower;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = true;
                    c.m_Spellbook = RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = true;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { 
                        OracleSpellbook.ToReference<BlueprintSpellbookReference>(),
                        RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var OracleRevelationGuidingStarMetamagicBuffExtend = Helpers.CreateBuff("OracleRevelationGuidingStarMetamagicBuffExtend", bp => {
                bp.SetName("Guiding Star - Extend Spell");
                bp.SetDescription("Once per day you can cast one spell from the oracle spellbook as if it were modified by the Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Extend;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = true;
                    c.m_Spellbook = OracleSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Extend;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = true;
                    c.m_Spellbook = RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = true;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        OracleSpellbook.ToReference<BlueprintSpellbookReference>(),
                        RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var OracleRevelationGuidingStarMetamagicAbilityBase = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationGuidingStarMetamagicAbilityBase", bp => {
                bp.SetName("Guiding Star");
                bp.SetDescription("Once per day you can cast one spell from the oracle spellbook as if it were modified by the Empower Spell or Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationGuidingStarMetamagicAbilityEmpower = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationGuidingStarMetamagicAbilityEmpower", bp => {
                bp.SetName("Guiding Star - Empower Spell");
                bp.SetDescription("Once per day you can cast one spell from the oracle spellbook as if it were modified by the Empower Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationGuidingStarMetamagicBuffEmpower.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationGuidingStarMetamagicBuffEmpower.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationGuidingStarMetamagicBuffExtend.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationGuidingStarMetamagicAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationGuidingStarMetamagicAbilityExtend = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationGuidingStarMetamagicAbilityExtend", bp => {
                bp.SetName("Guiding Star - Extend Spell");
                bp.SetDescription("Once per day you can cast one spell from the oracle spellbook as if it were modified by the Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationGuidingStarMetamagicBuffExtend.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationGuidingStarMetamagicBuffEmpower.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationGuidingStarMetamagicBuffExtend.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationGuidingStarMetamagicAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleRevelationGuidingStarMetamagicAbilityBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleRevelationGuidingStarMetamagicAbilityEmpower.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationGuidingStarMetamagicAbilityExtend.ToReference<BlueprintAbilityReference>()
                };
            });
            var OracleRevelationGuidingStarFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGuidingStarFeature", bp => {
                bp.SetName("Guiding Star");
                bp.SetDescription("The stars themselves hold many answers, you may add your Charisma modifier to your Wisdom modifier on all Wisdom-based checks. In addition, once per day you can " +
                    "cast one spell from the oracle spellbook as if it were modified by the Empower Spell or Extend Spell feat without increasing the spell’s casting time or level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        OracleRevelationGuidingStarSkillFeature.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationGuidingStarMetamagicAbilityBase.ToReference<BlueprintUnitFactReference>()
                    };
                });                
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationGuidingStarFeature.ToReference<BlueprintFeatureReference>());
            //Interstallar Void
            var FreezingNothingness = Resources.GetBlueprint<BlueprintAbility>("89bc94bd06dcf5847bb9e4d6ba1b9767");
            var FatiguedBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("e6f2fc5d73d88064583cb828801212f4");
            var ExhaustedBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("46d1b9cc3d0fd36469a471b047d773a2");
            var OracleRevelationInterstallarVoidResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationInterstallarVoidResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    StartingLevel = 1,
                    LevelStep = 9,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
                bp.m_UseMax = true;
                bp.m_Max = 2;
            });
            var OracleRevelationInterstallarVoidAbility1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationInterstallarVoidAbility1", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("You call upon the frigid depths of outer space to bring a terrible chill to your enemies. As a standard action, one target within 30 feet is cloaked in the void " +
                    "and takes 1d6 points of cold damage per level. A successful Fortitude save halves this damage. At 10th level, the interstellar void is so extreme that enemies who fail their " +
                    "saving throw are fatigued. At 15th level, creatures who fail their save are exhausted and stunned for 1 round. You can use this ability once per day plus one additional time " +
                    "per day at 10th level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Cold
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = false,
                            HalfIfSaved = true,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationInterstallarVoidResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = FreezingNothingness.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationInterstallarVoidAbility1.SavingThrow", "Fortitude partial");
            });
            var OracleRevelationInterstallarVoidAbility2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationInterstallarVoidAbility2", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("You call upon the frigid depths of outer space to bring a terrible chill to your enemies. As a standard action, one target within 30 feet is cloaked in the void " +
                    "and takes 1d6 points of cold damage per level. A successful Fortitude save halves this damage. At 10th level, the interstellar void is so extreme that enemies who fail their " +
                    "saving throw are fatigued. At 15th level, creatures who fail their save are exhausted and stunned for 1 round. You can use this ability once per day plus one additional time " +
                    "per day at 10th level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Cold
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = false,
                            HalfIfSaved = true,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = FatiguedBuff,
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue(),
                                    DurationSeconds = 0
                                }
                            )
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationInterstallarVoidResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = FreezingNothingness.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationInterstallarVoidAbility2.SavingThrow", "Fortitude partial");
            });
            var OracleRevelationInterstallarVoidAbility3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationInterstallarVoidAbility3", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("You call upon the frigid depths of outer space to bring a terrible chill to your enemies. As a standard action, one target within 30 feet is cloaked in the void " +
                    "and takes 1d6 points of cold damage per level. A successful Fortitude save halves this damage. At 10th level, the interstellar void is so extreme that enemies who fail their " +
                    "saving throw are fatigued. At 15th level, creatures who fail their save are exhausted and stunned for 1 round. You can use this ability once per day plus one additional time " +
                    "per day at 10th level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Cold
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = false,
                            HalfIfSaved = true,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ExhaustedBuff,
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue(),
                                    DurationSeconds = 0
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = Stunned,
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage
                                        }
                                    },
                                    DurationSeconds = 0
                                }
                            )
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationInterstallarVoidResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = FreezingNothingness.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationInterstallarVoidAbility3.SavingThrow", "Fortitude partial");
            });
            var OracleRevelationInterstallarVoidFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationInterstallarVoidFeature1", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationInterstallarVoidAbility1.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationInterstallarVoidAbility1.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });

                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationInterstallarVoidFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationInterstallarVoidFeature2", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationInterstallarVoidAbility2.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationInterstallarVoidAbility2.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });

                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationInterstallarVoidFeature3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationInterstallarVoidFeature3", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationInterstallarVoidAbility3.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationInterstallarVoidAbility3.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });

                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationInterstallarVoidFeature12 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationInterstallarVoidFeature12", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] { InquisitorClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Archetypes = new BlueprintArchetypeReference[] { RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.Level = 10;
                    c.m_Feature = OracleRevelationInterstallarVoidFeature1.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] { InquisitorClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Archetypes = new BlueprintArchetypeReference[] { RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.Level = 10;
                    c.m_Feature = OracleRevelationInterstallarVoidFeature2.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationInterstallarVoidFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationInterstallarVoidFeature", bp => {
                bp.SetName("Interstallar Void");
                bp.SetDescription("You call upon the frigid depths of outer space to bring a terrible chill to your enemies. As a standard action, one target within 30 feet is cloaked in the void " +
                    "and takes 1d6 points of cold damage per level. A successful Fortitude save halves this damage. At 10th level, the interstellar void is so extreme that enemies who fail their " +
                    "saving throw are fatigued. At 15th level, creatures who fail their save are exhausted and stunned for 1 round. You can use this ability once per day plus one additional time " +
                    "per day at 10th level.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] { InquisitorClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Archetypes = new BlueprintArchetypeReference[] { RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.Level = 15;
                    c.m_Feature = OracleRevelationInterstallarVoidFeature12.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] { InquisitorClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Archetypes = new BlueprintArchetypeReference[] { RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.Level = 15;
                    c.m_Feature = OracleRevelationInterstallarVoidFeature3.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationInterstallarVoidResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationInterstallarVoidFeature.ToReference<BlueprintFeatureReference>());
            //Lure of the Heavens
            var LureOfTheHeavensIcon = AssetLoader.LoadInternal("Skills", "Icon_LureOfTheHeavens.png");
            var OracleRevelationLureOfTheHeavensResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationLureOfTheHeavensResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    LevelIncrease = 1,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 1,
                    StartingIncrease = 1,
                    LevelStep = 0,
                    PerStepIncrease = 1,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                    m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>()
                    }
                };
                bp.m_UseMax = true;
                bp.m_Max = 20;
            });
            var OracleRevelationLureOfTheHeavensHoverFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationLureOfTheHeavensHoverFeature", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("");
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Ground;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationLureOfTheHeavensFlyBuff = Helpers.CreateBuff("OracleRevelationLureOfTheHeavensFlyBuff", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your oracle level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = LureOfTheHeavensIcon;
                bp.AddComponent<ACBonusAgainstAttacks>(c => {
                    c.AgainstMeleeOnly = true;
                    c.AgainstRangedOnly = false;
                    c.OnlySneakAttack = false;
                    c.NotTouch = false;
                    c.IsTouch = false;
                    c.OnlyAttacksOfOpportunity = false;
                    c.Value = new ContextValue();
                    c.ArmorClassBonus = 3;
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.CheckArmorCategory = false;
                    c.NoShield = false;
                });
                bp.AddComponent<FormationACBonus>(c => {
                    c.UnitProperty = false;
                    c.Bonus = 3;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationLureOfTheHeavensFlyAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationLureOfTheHeavensFlyAbility", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your oracle level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = LureOfTheHeavensIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationLureOfTheHeavensResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationLureOfTheHeavensFlyBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationLureOfTheHeavensFlyFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationLureOfTheHeavensFlyFeature", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your oracle level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationLureOfTheHeavensFlyAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationLureOfTheHeavensFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationLureOfTheHeavensFeature", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your oracle level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillStealth;
                    c.Value = 1;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] { InquisitorClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Archetypes = new BlueprintArchetypeReference[] { RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.Level = 5;
                    c.m_Feature = OracleRevelationLureOfTheHeavensHoverFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] { InquisitorClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Archetypes = new BlueprintArchetypeReference[] { RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.Level = 10;
                    c.m_Feature = OracleRevelationLureOfTheHeavensFlyFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationLureOfTheHeavensResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationLureOfTheHeavensFeature.ToReference<BlueprintFeatureReference>());
            //Spray of Shooting Stars
            var MoltenOrbSpell = Resources.GetBlueprint<BlueprintAbility>("42a65895ba0cb3a42b6019039dd2bff1");
            var MoltenOrbProjectile = Resources.GetBlueprint<BlueprintProjectile>("49c812020338e90479b54cfc5b1f6305");
            var OracleRevelationSprayOfShootingStarsResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationSprayOfShootingStarsResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 5,
                    PerStepIncrease = 1,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>()
                    }
                };
                bp.m_UseMax = true;
                bp.m_Max = 5;
            });
            var OracleRevelationSprayOfShootingStarsImmunity = Helpers.CreateBuff("OracleRevelationSprayOfShootingStarsImmunity", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var OracleRevelationSprayOfShootingStarsFreeUnlock = Helpers.CreateBuff("OracleRevelationSprayOfShootingStarsFreeUnlock", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Spray of Shooting Stars");
                bp.SetDescription("You may use spray of shooting stars as a free action this round as many times as you wish, until you run out of daily uses. Unused uses of this ability will be available next round if not used.");
                //bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var OracleRevelationSprayOfShootingStarsBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationSprayOfShootingStarsBaseAbility", bp => {
                bp.SetName("Spray of Shooting Stars");
                bp.SetDescription("As a standard action, you can unleash a ball of energy that explodes in a 5-foot radius burst dealing 1d4 points of fire damage per level. A successful Reflex save halves this damage. " +
                    "You can fire one explosive ball per day, plus one additional ball per day at 5th level and for every 5 levels thereafter. You can fire more than one ball at a time, but creatures caught inside more than " +
                    "one explosion per round only take damage once. \nOther energy balls shot by this ability are used with the \"Free Action\" version of this ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationSprayOfShootingStarsFreeUnlock.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = OracleRevelationSprayOfShootingStarsImmunity.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Energy,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Fire
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    EnergyDrainType = EnergyDrainType.Temporary,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.DamageDice,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = true,
                                    HalfIfSaved = true,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                }
                                )
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationSprayOfShootingStarsImmunity.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        MoltenOrbProjectile.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Simple;
                    c.IsHandOfTheApprentice = false;
                    c.m_Length = 0.Feet();
                    c.m_LineWidth = 5.Feet();
                    c.NeedAttackRoll = false;
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 5.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationSprayOfShootingStarsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = MoltenOrbSpell.m_Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Thrown;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationSprayOfShootingStarsBaseAbility.SavingThrow", "Reflex partial");
            });
            var OracleRevelationSprayOfShootingStarsFreeAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationSprayOfShootingStarsFreeAbility", bp => {
                bp.SetName("Spray of Shooting Stars - Free Action");
                bp.SetDescription("As a free action, you can unleash a ball of energy that explodes in a 5-foot radius burst dealing 1d4 points of fire damage per level. A successful Reflex save halves this damage. " +
                    "You can fire one explosive ball per day, plus one additional ball per day at 5th level and for every 5 levels thereafter. You can fire more than one ball at a time, but creatures caught inside more than " +
                    "one explosion per round only take damage once. \nUsing spray of shooting stars - free action requires using the standard version at least once in the same round.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = OracleRevelationSprayOfShootingStarsImmunity.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Energy,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Fire
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    EnergyDrainType = EnergyDrainType.Temporary,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.DamageDice,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = true,
                                    HalfIfSaved = true,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                }
                                )
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationSprayOfShootingStarsImmunity.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        MoltenOrbProjectile.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Simple;
                    c.IsHandOfTheApprentice = false;
                    c.m_Length = 0.Feet();
                    c.m_LineWidth = 5.Feet();
                    c.NeedAttackRoll = false;
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 5.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationSprayOfShootingStarsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.NeedsAll = false;
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationSprayOfShootingStarsFreeUnlock.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_Icon = MoltenOrbSpell.m_Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Thrown;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationSprayOfShootingStarsFreeAbility.SavingThrow", "Reflex partial");
            });
            var OracleRevelationSprayOfShootingStarsFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationSprayOfShootingStarsFeature", bp => {
                bp.SetName("Spray of Shooting Stars");
                bp.SetDescription("As a standard action, you can unleash a ball of energy that explodes in a 5-foot radius burst dealing 1d4 points of fire damage per level. A successful Reflex save halves this damage. " +
                    "This attack has a range of 60 feet. You can fire one explosive ball per day, plus one additional ball per day at 5th level and for every 5 levels thereafter. You can fire more than one ball at a time, " +
                    "but creatures caught inside more than one simultaneous explosions only take damage once.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        OracleRevelationSprayOfShootingStarsBaseAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationSprayOfShootingStarsFreeAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { 
                        OracleRevelationSprayOfShootingStarsBaseAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationSprayOfShootingStarsFreeAbility.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationSprayOfShootingStarsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoHeavensMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationSprayOfShootingStarsFeature.ToReference<BlueprintFeatureReference>());

            //Ravener Hunter Cont.
            var RavenerHunterHeavensRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterHeavensRevelationSelection", bp => {
                bp.SetName("Heavens Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(OracleRevelationAwesomeDisplay, OracleRevelationCoatOfManyStarsFeature, OracleRevelationGuidingStarFeature, OracleRevelationInterstallarVoidFeature, 
                    OracleRevelationLureOfTheHeavensFeature, OracleRevelationSprayOfShootingStarsFeature);
            });
            RavenerHunterHeavensMysteryProgression.LevelEntries = new LevelEntry[] {
                 Helpers.LevelEntry(1, RavenerHunterHeavensRevelationSelection),
                 Helpers.LevelEntry(8, RavenerHunterHeavensRevelationSelection)
            };
            RavenerHunterHeavensMysteryProgression.UIGroups = new UIGroup[] {
                Helpers.CreateUIGroup(RavenerHunterHeavensRevelationSelection, RavenerHunterHeavensRevelationSelection)
            };
            var RavenerHunterChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("RavenerHunterChargedByNatureSelection");
            var SecondChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("SecondChargedByNatureSelection");
            RavenerHunterChargedByNatureSelection.m_AllFeatures = RavenerHunterChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>());
            SecondChargedByNatureSelection.m_AllFeatures = SecondChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterHeavensMysteryProgression.ToReference<BlueprintFeatureReference>());

            MysteryTools.RegisterMystery(OracleHeavensMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleHeavensMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherHeavensMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherHeavensMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistHeavensMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistHeavensMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoHeavensMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoHeavensMysteryFeature);
        }
    }
}
