using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Tweaks.Deities;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Encyclopedia;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TabletopTweaks.Core.NewComponents;
using UnityEngine;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class GodclawMystery {

        public static void AddGodclawMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var GodclawMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleGodclawMystery.png");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");

            #region Spelllist
            var CauseFearSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("bd81a3931aa285a4f9844585b5d97e51");
            var DazeMonsterAbility = Resources.GetModBlueprint<BlueprintAbility>("DazeMonsterAbility");
            var HoldPersonSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c7104f7526c4c524f91474614054547e");
            var OrdersWrathSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1ec8f035d8329134d96cdc7b90fdc2e1");
            var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
            var BladeBarrierSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("36c8971e91f1745418cc3ffdfac17b74");//maybe temp
            var DictumSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("302ab5e241931a94881d323a7844ae8f");
            var ShieldOfLawSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("73e7728808865094b8892613ddfaf7f5");
            var DominateMonsterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3c17035ec4717674cae2e841a190e757");
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
            var OracleGodclawSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleGodclawSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CauseFearSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DazeMonsterAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPersonSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OrdersWrathSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BreakEnchantmentSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BladeBarrierSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DictumSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShieldOfLawSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DominateMonsterSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherGodclawSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherGodclawSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CauseFearSpell;
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
            var OceansEchoGodclawSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoGodclawSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CauseFearSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPersonSpell;
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
                    c.m_Spell = ShieldOfLawSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DominateMonsterSpell;
                    c.SpellLevel = 9;
                });
            });
            #endregion
            #region Final Revelation
            var ClashingRocksSpell = Resources.GetBlueprint<BlueprintAbility>("01300baad090d634cb1a1b2defe068d6");
            var OracleGodclawFinalRevelationResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleGodclawFinalRevelationResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
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
            var OracleGodclawFinalRevelationAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleGodclawFinalRevelationAbility", bp => {
                bp.SetName("Clashing Rocks - Godclaw");
                bp.SetDescription("You create two Colossal-sized masses of rock, dirt, and stone and slam them together against a single creature between them. " +
                    "The clashing rocks appear up to 30 feet away from the target on opposite sides and rush toward it with a mighty grinding crash. " +
                    "You must make a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} to hit the target with the rocks. The clashing rocks ignore " +
                    "{g|Encyclopedia:Concealment}concealment{/g}. A creature struck by the clashing rocks takes {g|Encyclopedia:Dice}20d6{/g} points of " +
                    "{g|Encyclopedia:Damage_Type}bludgeoning damage{/g} and is knocked {g|ConditionProne}prone{/g}. If the clashing rocks miss the target, " +
                    "the target still takes 10d6 points of bludgeoning damage from falling rocks and is knocked prone. A successful { g | Encyclopedia:Saving_Throw}Reflex save{/g } " +
                    "reduces this damage to half and the target remains standing. Creatures other than the target that occupy the spaces where the clashing rocks appear or " +
                    "within their path must also make Reflex saves or take 10d6 points of bludgeoning damage and be knocked prone (save for half and remain standing).");
                bp.AddComponent(ClashingRocksSpell.GetComponent<AbilityEffectRunAction>());
                bp.AddComponent(ClashingRocksSpell.GetComponent<AbilityDeliverClashingRocks>());
                bp.AddComponent(ClashingRocksSpell.GetComponent<AbilityEffectMiss>());
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.None;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleGodclawFinalRevelationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = ClashingRocksSpell.Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.Harmful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Persistent | Metamagic.Bolstered;
                bp.LocalizedDuration = Helpers.CreateString("OracleGodclawFinalRevelationAbility.Duration", "{g|Encyclopedia:Saving_Throw}Reflex{/g} partial, see text");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleGodclawFinalRevelationAbility.SavingThrow", "{g|Encyclopedia:Saving_Throw}Reflex{/g} partial, see text");
            });

            var OracleGodclawFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleGodclawFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you no longer take armor check penalties for wearing armor, and your armor’s maximum Dexterity bonus increases by 5. " +
                    "\nTwice per day, you can call down the crushing weight of the Godclaw down on your foes, casting {g|SpellsClashingRocks}clashing rocks{/g} as a spell-like ability, " +
                    "treating your oracle level as your caster level.");
                bp.AddComponent<MaxDexBonusIncrease>(c => {
                    c.Bonus = 5;
                    c.BonesPerRank = 0;
                    c.CheckCategory = false;
                    c.Category = ArmorProficiencyGroup.None;
                });
                bp.AddComponent<IgnoreArmorCheckPenalty>(c => {
                    c.CheckCategory = false;
                    c.Categorys = new ArmorProficiencyGroup[] { };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleGodclawFinalRevelationAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleGodclawFinalRevelationAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = OracleGodclawFinalRevelationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            #endregion
            //Main Mystery Feature
            var OracleGodclawMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleGodclawMysteryFeature", bp => {
                bp.m_Icon = GodclawMysteryIcon;
                bp.SetName("Godclaw");
                bp.SetDescription("An oracle with the godclaw mystery adds {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Perception}Perception{/g} " +
                    "and {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleGodclawFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleGodclawSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherGodclawMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherGodclawMysteryFeature", bp => {
                bp.m_Icon = GodclawMysteryIcon;
                bp.SetName("Godclaw");
                bp.SetDescription("An oracle with the godclaw mystery adds {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Perception}Perception{/g} " +
                    "and {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherGodclawSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistGodclawMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistGodclawMysteryFeature", bp => {
                bp.m_Icon = GodclawMysteryIcon;
                bp.SetName("Godclaw");
                bp.SetDescription("Gain access to the spells and revelations of the godclaw mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleGodclawFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleGodclawSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoGodclawMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoGodclawMysteryFeature", bp => {
                bp.m_Icon = GodclawMysteryIcon;
                bp.SetName("Godclaw");
                bp.SetDescription("Gain access to the spells and revelations of the Godclaw mystery. \nDue to the ocean's echo archetype the class skills gained from this archetype " +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleGodclawFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoGodclawSpells.ToReference<BlueprintFeatureReference>();
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


            #region Armored Mind 
            //Maybe change to all heavy armor
            #endregion
            #region Boon of Bravery 




            var OracleRevelationBoonOfBraveryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBoonOfBraveryFeature", bp => {
                bp.SetName("Boon of Bravery");
                bp.SetDescription("As a move action, you can call upon your deities to grant you courage. " +
                    "You gain a +1 morale bonus on attack rolls, damage rolls, and Will saving throws against fear effects for a number of rounds equal to your Charisma bonus. " +
                    "At 7th level, this bonus increases to +2, and at 14th level this bonus increases to +3. You can use this ability once per day, plus one additional time per " +
                    "day at 5th level, and every 5 levels thereafter.");
                

                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoGodclawMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBoonOfBraveryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Boon of the Defender 

            var OracleRevelationBoonOfDefenderFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBoonOfDefenderFeature", bp => {
                bp.SetName("Boon of Defender");
                bp.SetDescription("As a standard action, you can form a shield around you that blocks incoming attacks for a number of minutes equal to " +
                    "1/2 your oracle level (minimum 1). The shield grants a +4 deflection bonus to your Armor Class. At 7th level, and again at 11th level and 15th level, " +
                    "this bonus increases by +1. At 19th level, the shield also grants you DR 2/chaos. The shield’s duration does not need to be consecutive, but it must be spent in 1-minute increments.");


                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoGodclawMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBoonOfDefenderFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Boon of the Guide 

            var OracleRevelationBoonOfGuideFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBoonOfGuideFeature", bp => {
                bp.SetName("Boon of Guide");
                bp.SetDescription("Once per day as an free action you may gain the boon of the guide, the next time you fail a saving throw that causes you to become blinded, deafened, frightened, panicked, " +
                    "paralyzed, shaken, or stunned, you can attempt that saving throw again with a +4 insight bonus on the roll. You must take the second result, even if it is worse. " +
                    "At 7th and 15th level, you can use this ability one additional time per day.");


                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoGodclawMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBoonOfGuideFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Boon of Terror [Needs Testing]
            var ShakenBuff = Resources.GetBlueprint<BlueprintBuff>("25ec6cb6ab1845c48a95f9c20b034220");
            //use combat only aura deploying a buff, and then a AddInitiatorSavingThrowTriggerExpanded to check for the Revelation while reading spell level
            var OracleRevelationBoonOfTerrorEffectBuff = Helpers.CreateBuff("OracleRevelationBoonOfTerrorEffectBuff", bp => {
                bp.SetName("Boon of Terror Effect Buff");
                bp.SetDescription("");
                //Added below Revelation Feture as checked fact is not yet declared                
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var OracleRevelationBoonOfTerrorArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("OracleRevelationBoonOfTerrorArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.AggroEnemies = false;
                bp.AffectEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 30.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = OracleRevelationBoonOfTerrorEffectBuff.ToReference<BlueprintBuffReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationBoonOfTerrorEffectBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Minutes,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue(),
                                        BonusValue = new ContextValue(),
                                        m_IsExtendable = true
                                    },
                                    IsFromSpell = false,
                                    IsNotDispelable = true,
                                }
                            )
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = OracleRevelationBoonOfTerrorEffectBuff.ToReference<BlueprintBuffReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = OracleRevelationBoonOfTerrorEffectBuff.ToReference<BlueprintBuffReference>()
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }
                        );
                    c.Round = Helpers.CreateActionList();
                    c.UnitMove = Helpers.CreateActionList();
                });
            });
            var OracleRevelationBoonOfTerrorAuraBuff = Helpers.CreateBuff("OracleRevelationBoonOfTerrorAuraBuff", bp => {
                bp.SetName("Boon of Terror Combat Buff");
                bp.SetDescription("");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = OracleRevelationBoonOfTerrorArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });

            var OracleRevelationBoonOfTerrorFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBoonOfTerrorFeature", bp => {
                bp.SetName("Boon of Terror");
                bp.SetDescription("Whenever a creature within 30 feet fails a saving throw from one of your spells, it is shaken for a number of rounds equal to the spell’s level. " +
                    "Spells that do not allow saves do not cause creatures to become shaken. This does not stack with other fear effects.");
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationBoonOfTerrorAuraBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsNotDispelable = true
                        }
                        );
                    c.CombatEndActions = Helpers.CreateActionList();
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistGodclawMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoGodclawMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #region Spell Level Components
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 1;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 1,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//1
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 2;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 2,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//2
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 3;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 3,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//3
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 4;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 4,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//4
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 5;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 5,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//5
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 6;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 6,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//6
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 7;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 7,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//7
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 8;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 8,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//8
            OracleRevelationBoonOfTerrorEffectBuff.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                c.OnlyPass = false;
                c.OnlyFail = true;
                c.SpecificSave = false;
                c.ChooseSave = SavingThrowType.Fortitude;
                c.SpecificDescriptor = false;
                c.SpellDescriptor = SpellDescriptor.MindAffecting;
                c.OnlyFromCaster = false;
                c.OnlyFromFact = true;
                c.m_CheckedFact = OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintUnitFactReference>();
                c.SpellsOnly = true;
                c.FromExactSpellLevel = true;
                c.SpellLevel = 9;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 9,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                    }
                    );
            });//9
            #endregion
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBoonOfTerrorFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Iron Order
            //Use Command as the template instead
            #endregion
            #region Resiliency
            //needs simplifying to match CRPG downstate
            #endregion

            MysteryTools.RegisterMystery(OracleGodclawMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleGodclawMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherGodclawMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherGodclawMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistGodclawMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistGodclawMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoGodclawMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoGodclawMysteryFeature);
            MysteryTools.RegisterHermitMystery(OracleGodclawMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleGodclawMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleGodclawMysteryFeature);
        }

    }
}
