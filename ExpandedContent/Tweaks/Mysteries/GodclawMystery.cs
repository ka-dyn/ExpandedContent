using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Classes;
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
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Designers.Mechanics.Buffs;
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
using Kingmaker.UnitLogic.ActivatableAbilities;
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
            //var GodclawMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleGodclawMystery.png"); Use other for testing
            var GodclawMysteryIcon = AssetLoader.LoadInternal("Deities", "Icon_Godclaw.jpg");
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
            #region Final Revelation [Needs Testing]
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


            #region Armored Mind [Needs Testing]
            var DivinationSchool = Resources.GetBlueprint<BlueprintProgression>("d7d18ce5c24bd324d96173fdc3309646");
            var OracleRevelationArmoredMindSubBuff = Helpers.CreateBuff("OracleRevelationArmoredMindSubBuff", bp => {
                bp.SetName("Armored Mind Stat Buff");
                bp.SetDescription("");
                bp.AddComponent<SavingThrowContextBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    bp.AddComponent<ContextRankConfig>(c => {
                        c.m_Type = AbilityRankType.Default;
                        c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                        c.m_Progression = ContextRankProgression.Custom;
                        c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                            new ContextRankConfig.CustomProgressionItem(){ BaseValue = 10, ProgressionValue = 2 },
                            new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                        };
                        c.m_Class = new BlueprintCharacterClassReference[] {
                            OracleClass.ToReference<BlueprintCharacterClassReference>(),
                            ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        };
                        c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    });
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var OracleRevelationArmoredMindBuff = Helpers.CreateBuff("OracleRevelationArmoredMindBuff", bp => {
                bp.SetName("Armored Mind Tracking Buff");
                bp.SetDescription("");
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.AC;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new UnitArmor() {
                                        Not = false,
                                        Unit = new FactOwner(),
                                        IncludeArmorCategories = new ArmorProficiencyGroup[] { ArmorProficiencyGroup.Heavy },
                                        ExcludeArmorCategories = new ArmorProficiencyGroup[0] { }
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new AddFact() {
                                    Unit = new FactOwner(),
                                    m_Fact = OracleRevelationArmoredMindSubBuff.ToReference<BlueprintUnitFactReference>()
                                }
                            ),
                            IfFalse = Helpers.CreateActionList()
                        }
                    );
                    c.Deactivated = Helpers.CreateActionList(
                        new RemoveFact() {
                            Unit = new FactOwner(),
                            m_Fact = OracleRevelationArmoredMindSubBuff.ToReference<BlueprintUnitFactReference>()
                        }
                    );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var OracleRevelationArmoredMindResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationArmoredMindResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Charisma,
                };
            });

            var OracleRevelationArmoredMindAbilityBuff = Helpers.CreateBuff("OracleRevelationArmoredMindAbilityBuff", bp => {
                bp.SetName("Armored Mind");
                bp.SetDescription("Once per day, as a free action you may activate your armored mind, you reroll the next failed Will saving throw against a mind-affecting effect and choose the " +
                    "more favorable result.");
                bp.m_Icon = DivinationSchool.m_Icon;
                bp.AddComponent<ModifyD20>(c => {
                    c.Rule = RuleType.SavingThrow;
                    c.DispellMagicCheckType = RuleDispelMagic.CheckType.None;
                    c.RollsAmount = 1;
                    c.TakeBest = true;
                    c.RollResult = new ContextValue();
                    c.AddBonus = false;
                    c.Bonus = new ContextValue();
                    c.BonusDescriptor = ModifierDescriptor.None;
                    c.WithChance = false;
                    c.Chance = new ContextValue();
                    c.RerollOnlyIfFailed = true;
                    c.RerollOnlyIfSuccess = false;
                    c.RollCondition = ModifyD20.RollConditionType.None;
                    c.ValueToCompareRoll = new ContextValue();
                    c.DispellOnRerollFinished = true;
                    c.DispellOn20 = false;
                    c.AgainstAlignment = false;
                    c.Alignment = AlignmentComponent.None;
                    c.TargetAlignment = false;
                    c.SpecificSkill = false;
                    c.Skill = new StatType[] { };
                    c.Value = new ContextValue();
                    c.m_SavingThrowType = FlaggedSavingThrowType.Will;
                    c.SpecificDescriptor = true;
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var OracleRevelationArmoredMindAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationArmoredMindAbility", bp => {
                bp.SetName("Armored Mind");
                bp.SetDescription("Once per day, as a free action you may activate your armored mind, you reroll the next failed Will saving throw against a mind-affecting effect and choose the " +
                    "more favorable result.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationArmoredMindAbilityBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationArmoredMindResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = DivinationSchool.Icon;
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

            var OracleRevelationArmoredMindFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationArmoredMindFeature", bp => {
                bp.SetName("Armored Mind");
                bp.SetDescription("While you are wearing heavy armor, you gain a +2 bonus on Will saving throws to resist mind-affecting effects. " +
                    "\nOnce per day at 7th level, you can reroll the next Will saving throw against a mind-affecting effect and choose the more favorable result. " +
                    "\nAt 11th level, the bonus on Will saving throws increases to +4.");
                bp.AddComponent<HasArmorFeatureUnlock>(c => {
                    c.m_NewFact = OracleRevelationArmoredMindBuff.ToReference<BlueprintUnitFactReference>();
                    c.FilterByBlueprintArmorTypes = false;
                    c.m_BlueprintArmorTypes = new BlueprintArmorTypeReference[] { };
                    c.FilterByArmorProficiencyGroup = true;
                    c.m_ArmorProficiencyGroupEntries = ArmorProficiencyGroupFlag.Heavy;
                    c.m_DisableWhenHasShield = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 7;
                    c.m_Feature = OracleRevelationArmoredMindAbility.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationArmoredMindResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
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
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationArmoredMindFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Boon of Bravery [Needs Testing]
            var RemoveFearBuffIcon = Resources.GetBlueprint<BlueprintBuff>("c5c86809a1c834e42a2eb33133e90a28").m_Icon;
            var OracleRevelationBoonOfBraveryResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationBoonOfBraveryResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 0,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                    LevelStep = 5
                };
            });

            var OracleRevelationBoonOfBraveryBuff = Helpers.CreateBuff("OracleRevelationBoonOfBraveryBuff", bp => {
                bp.SetName("Boon of Bravery");
                bp.SetDescription("You gain a +1 morale bonus on attack rolls, damage rolls, and will saving throws against fear effects for a number of rounds equal to your Charisma bonus. " +
                    "At 7th level, this bonus increases to +2, and at 14th level this bonus increases to +3.");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.Morale;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.AdditionalDamage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.Morale;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Fear;
                    c.ModifierDescriptor = ModifierDescriptor.Morale;
                    c.Value = 0;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 7;
                    c.m_UseMax = true;
                    c.m_Max = 3;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { };
                });
                bp.m_Icon = RemoveFearBuffIcon;
            });

            var OracleRevelationBoonOfBraveryAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationBoonOfBraveryAbility", bp => {
                bp.SetName("Boon of Bravery");
                bp.SetDescription("As a move action, you can call upon your deities to grant you courage. " +
                    "You gain a +1 morale bonus on attack rolls, damage rolls, and will saving throws against fear effects for a number of rounds equal to your Charisma bonus. " +
                    "At 7th level, this bonus increases to +2, and at 14th level this bonus increases to +3. You can use this ability once per day, plus one additional time per " +
                    "day at 5th level, and every 5 levels thereafter.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationBoonOfBraveryBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 0;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationArmoredMindResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = RemoveFearBuffIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationBoonOfBraveryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBoonOfBraveryFeature", bp => {
                bp.SetName("Boon of Bravery");
                bp.SetDescription("As a move action, you can call upon your deities to grant you courage. " +
                    "You gain a +1 morale bonus on attack rolls, damage rolls, and will saving throws against fear effects for a number of rounds equal to your Charisma bonus. " +
                    "At 7th level, this bonus increases to +2, and at 14th level this bonus increases to +3. You can use this ability once per day, plus one additional time per " +
                    "day at 5th level, and every 5 levels thereafter.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationBoonOfBraveryAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationBoonOfBraveryResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
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
                bp.m_Icon = RemoveFearBuffIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBoonOfBraveryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Boon of the Defender [Needs Testing]
            var OracleRevelationBoonOfTheDefenderIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleRevelationBoonOfTheDefender.jpg");
            var OracleRevelationBoonOfTheDefenderResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationBoonOfTheDefenderResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    StartingLevel = 4,
                    LevelStep = 2,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });
            var OracleRevelationBoonOfTheDefenderBuff = Helpers.CreateBuff("OracleRevelationBoonOfTheDefenderBuff", bp => {
                bp.SetName("Boon of the Defender");
                bp.SetDescription("As a standard action, you can form a shield around you that blocks incoming attacks for a number of minutes equal to " +
                    "1/2 your oracle level (minimum 1). The shield grants a +4 deflection bonus to your Armor Class. At 7th level, and again at 11th level and 15th level, " +
                    "this bonus increases by +1. At 19th level, the shield also grants you DR 2/chaos. The shield’s duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = OracleRevelationBoonOfTheDefenderIcon;
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 4;
                    c.m_UseMax = true;
                    c.m_Max = 3;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {//+4
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = 4,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });

                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 19;
                    c.m_StepLevel = 2;
                    c.m_UseMax = true;
                    c.m_Max = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {//X2
                    c.ValueType = AbilitySharedValue.DamageBonus;
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
                            ValueRank = AbilityRankType.DamageDice,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                    c.UsePool = false;
                    c.Pool = new ContextValue();
                    c.Or = false;
                    c.BypassedByMaterial = false;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.BypassedByForm = false;
                    c.Form = PhysicalDamageForm.Slashing;
                    c.BypassedByMagic = false;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = DamageAlignment.Chaotic;
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



            var OracleRevelationBoonOfTheDefenderAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationBoonOfTheDefenderAbility", bp => {
                bp.SetName("Boon of the Defender");
                bp.SetDescription("As a standard action, you can form a shield around you that blocks incoming attacks for a number of minutes equal to " +
                    "1/2 your oracle level (minimum 1). The shield grants a +4 deflection bonus to your Armor Class. At 7th level, and again at 11th level and 15th level, " +
                    "this bonus increases by +1. At 19th level, the shield also grants you DR 2/chaos. The shield’s duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = OracleRevelationBoonOfTheDefenderIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationBoonOfTheDefenderResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationBoonOfTheDefenderBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationBoonOfTheDefenderFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBoonOfTheDefenderFeature", bp => {
                bp.SetName("Boon of the Defender");
                bp.SetDescription("As a standard action, you can form a shield around you that blocks incoming attacks for a number of minutes equal to " +
                    "1/2 your oracle level (minimum 1). The shield grants a +4 deflection bonus to your Armor Class. At 7th level, and again at 11th level and 15th level, " +
                    "this bonus increases by +1. At 19th level, the shield also grants you DR 2/chaos. The shield’s duration does not need to be consecutive, but it must be spent in 1-minute increments.");

                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationBoonOfTheDefenderAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationBoonOfTheDefenderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
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
                bp.m_Icon = OracleRevelationBoonOfTheDefenderIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBoonOfTheDefenderFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Boon of the Guide
            //It's just Battlefield Clarity from Battle Mystery bar the name, so I'm just gonna use it
            var BattlefieldClarity = Resources.GetBlueprint<BlueprintFeature>("c0c2b21d83dd2514c98ae8d3684ad981").GetComponent<PrerequisiteFeaturesFromList>();
            BattlefieldClarity.m_Features = BattlefieldClarity.m_Features.AppendToArray(OracleGodclawMysteryFeature.ToReference<BlueprintFeatureReference>());
            BattlefieldClarity.m_Features = BattlefieldClarity.m_Features.AppendToArray(EnlightnedPhilosopherGodclawMysteryFeature.ToReference<BlueprintFeatureReference>());
            BattlefieldClarity.m_Features = BattlefieldClarity.m_Features.AppendToArray(DivineHerbalistGodclawMysteryFeature.ToReference<BlueprintFeatureReference>());
            BattlefieldClarity.m_Features = BattlefieldClarity.m_Features.AppendToArray(OceansEchoGodclawMysteryFeature.ToReference<BlueprintFeatureReference>());
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
            #region Iron Order [Needs Testing]
            var CommandSpell = Resources.GetBlueprint<BlueprintAbility>("feb70aab86cc17f4bb64432c83737ac2");
            var CommandApproachSpell = Resources.GetBlueprint<BlueprintAbility>("f049fe38f5bb5ae48b252852727ab86a");
            var CommandFallSpell = Resources.GetBlueprint<BlueprintAbility>("9e87cb2778afdc24e9ceb523aca512a8");
            var CommandFleeSpell = Resources.GetBlueprint<BlueprintAbility>("7c1d48449ecf4374497e7009c49f6376");
            var CommandHaltSpell = Resources.GetBlueprint<BlueprintAbility>("a43abe1819699894c94a7cec3ccd3765");
            var CommandGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("cb15cc8d7a5480648855a23b3ba3f93d");
            var CommandGreaterApproachSpell = Resources.GetBlueprint<BlueprintAbility>("305e7eebe6572d44eb44f29b43436d77");
            var CommandGreaterFallSpell = Resources.GetBlueprint<BlueprintAbility>("4cffe11248cb2134d98c9e39a827476a");
            var CommandGreaterFleeSpell = Resources.GetBlueprint<BlueprintAbility>("c0373cd86479df24d9f03bb23a99d57c");
            var CommandGreaterHaltSpell = Resources.GetBlueprint<BlueprintAbility>("138bdf210567a45449151ac47630cecd");

            var OracleRevelationIronOrderResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationIronOrderResource", bp => {
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

            var OracleRevelationIronOrderAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderAbility", bp => {
                bp.SetName("Iron Order");
                bp.SetDescription("Once per day, you can issue an order as per the {g|SpellsCommand}command{/g} spell. " +
                    "Any creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "\nIf you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandSpell.Icon;
                //AbilityVariants added after
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellComponent>(c => { c.School = SpellSchool.Enchantment; });
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderAbility.SavingThrow", "Will negates");
            });
            var OracleRevelationIronOrderGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderGreaterAbility", bp => {
                bp.SetName("Iron Order, Greater");
                bp.SetDescription("Once per day, you can issue an order as per the {g|SpellsGreaterCommand}command, greater{/g} spell. " +
                    "Any creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "\nIf you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandGreaterSpell.Icon;
                //AbilityVariants added after
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellComponent>(c => { c.School = SpellSchool.Enchantment; });
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderGreaterAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderGreaterAbility.SavingThrow", "Will negates");
            });

            var OracleRevelationIronOrderApproachAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderApproachAbility", bp => {
                bp.SetName("Iron Order - Approach");
                bp.SetDescription("On its turn, the subject moves toward you as quickly and directly as possible for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "The creature may do nothing but move during its turn, and it provokes {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g} for this movement as normal. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandApproachSpell.Icon;
                bp.CopyComponentArray(CommandApproachSpell);
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderApproachAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderApproachAbility.SavingThrow", "Will negates");
            });
            var OracleRevelationIronOrderFallAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderFallAbility", bp => {
                bp.SetName("Iron Order - Fall");
                bp.SetDescription("On its turn, the subject falls to the ground and remains {g|ConditionProne}prone{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "It may act normally while prone but takes any appropriate {g|Encyclopedia:Penalty}penalties{/g}. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandFallSpell.Icon;
                bp.CopyComponentArray(CommandFallSpell);
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderFallAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderFallAbility.SavingThrow", "Will negates");
            });
            var OracleRevelationIronOrderFleeAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderFleeAbility", bp => {
                bp.SetName("Iron Order - Flee");
                bp.SetDescription("On its turn, the subject moves away from you as quickly as possible for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "It may do nothing but move during its turn, and it provokes {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g} for this movement as normal. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandFleeSpell.Icon;
                bp.CopyComponentArray(CommandFleeSpell);
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderFleeAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderFleeAbility.SavingThrow", "Will negates");
            });
            var OracleRevelationIronOrderHaltAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderHaltAbility", bp => {
                bp.SetName("Iron Order - Halt");
                bp.SetDescription("On its turn, the subject moves toward you as quickly and directly as possible for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "The creature may do nothing but move during its turn, and it provokes {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g} for this movement as normal. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandHaltSpell.Icon;
                bp.CopyComponentArray(CommandHaltSpell);
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderHaltAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderHaltAbility.SavingThrow", "Will negates");
            });

            var OracleRevelationIronOrderApproachGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderApproachGreaterAbility", bp => {
                bp.SetName("Iron Order, Greater - Approach");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions like command, except this spell affects multiple enemies in a 30-foot radius, " +
                    "and the activities continue beyond 1 {g|Encyclopedia:Combat_Round}round{/g}. At the start of each commanded creature's {g|Encyclopedia:CA_Types}action{/g} " +
                    "after the first, it gets another {g|Encyclopedia:Saving_Throw}Will save{/g} to attempt to break free from the spell. Each creature must receive " +
                    "the same command." +
                    "\r\nApproach: On its turn, the subject moves toward you as quickly and directly as possible for 1 round. The creature may do nothing but move during its turn, " +
                    "and it provokes {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g} for this movement as normal. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandGreaterApproachSpell.Icon;
                bp.CopyComponentArray(CommandGreaterApproachSpell);
                bp.RemoveComponents<ContextRankConfig>();
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderApproachGreaterAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderApproachGreaterAbility.SavingThrow", "Will negates");
            });
            var OracleRevelationIronOrderFallGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderFallGreaterAbility", bp => {
                bp.SetName("Iron Order, Greater - Fall");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions like command, except this spell affects multiple enemies in a 30-foot radius, " +
                    "and the activities continue beyond 1 {g|Encyclopedia:Combat_Round}round{/g}. At the start of each commanded creature's {g|Encyclopedia:CA_Types}action{/g} " +
                    "after the first, it gets another {g|Encyclopedia:Saving_Throw}Will save{/g} to attempt to break free from the spell. Each creature must receive " +
                    "the same command." +
                    "\r\nFall: On its turn, the subject falls to the ground and remains {g|ConditionProne}prone{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "It may act normally while prone but takes any appropriate {g|Encyclopedia:Penalty}penalties{/g}. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandGreaterFallSpell.Icon;
                bp.CopyComponentArray(CommandGreaterFallSpell);
                bp.RemoveComponents<ContextRankConfig>();
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderFallGreaterAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderFallGreaterAbility.SavingThrow", "Will negates");
            });
            var OracleRevelationIronOrderFleeGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderFleeGreaterAbility", bp => {
                bp.SetName("Iron Order, Greater - Flee");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions like command, except this spell affects multiple enemies in a 30-foot radius, " +
                    "and the activities continue beyond 1 {g|Encyclopedia:Combat_Round}round{/g}. At the start of each commanded creature's {g|Encyclopedia:CA_Types}action{/g} " +
                    "after the first, it gets another {g|Encyclopedia:Saving_Throw}Will save{/g} to attempt to break free from the spell. Each creature must receive " +
                    "the same command." +
                    "\r\nFlee: On its turn, the subject moves away from you as quickly as possible for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "It may do nothing but move during its turn, and it provokes {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g} for this movement as normal. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandGreaterFleeSpell.Icon;
                bp.CopyComponentArray(CommandGreaterFleeSpell);
                bp.RemoveComponents<ContextRankConfig>();
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderFleeGreaterAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderFleeGreaterAbility.SavingThrow", "Will negates");
            });
            var OracleRevelationIronOrderHaltGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronOrderHaltGreaterAbility", bp => {
                bp.SetName("Iron Order, Greater - Halt");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions like command, except this spell affects multiple enemies in a 30-foot radius, " +
                    "and the activities continue beyond 1 {g|Encyclopedia:Combat_Round}round{/g}. At the start of each commanded creature's {g|Encyclopedia:CA_Types}action{/g} " +
                    "after the first, it gets another {g|Encyclopedia:Saving_Throw}Will save{/g} to attempt to break free from the spell. Each creature must receive " +
                    "the same command." +
                    "\r\nHalt: On its turn, the subject moves toward you as quickly and directly as possible for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "The creature may do nothing but move during its turn, and it provokes {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g} for this movement as normal. " +
                    "\nAny creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "If you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take).");
                bp.m_Icon = CommandGreaterHaltSpell.Icon;
                bp.CopyComponentArray(CommandGreaterHaltSpell);
                bp.RemoveComponents<ContextRankConfig>();
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronOrderHaltGreaterAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationIronOrderHaltGreaterAbility.SavingThrow", "Will negates");
            });



            OracleRevelationIronOrderAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleRevelationIronOrderApproachAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationIronOrderFallAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationIronOrderFleeAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationIronOrderHaltAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            OracleRevelationIronOrderGreaterAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleRevelationIronOrderApproachGreaterAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationIronOrderFallGreaterAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationIronOrderFleeGreaterAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationIronOrderHaltGreaterAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            var OracleRevelationIronOrderUpgrade = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronOrderUpgrade", bp => {
                bp.SetName("Iron Order");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationIronOrderGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationIronOrderGreaterAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });

                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationIronOrderFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronOrderFeature", bp => {
                bp.SetName("Iron Order");
                bp.SetDescription("Once per day, you can issue an order as per the {g|SpellsCommand}command{/g} spell. " +
                    "Any creature of chaotic alignment has difficulty defying your command, taking a –4 penalty on its saving throw to resist it. " +
                    "At 15th level, your order may function as per {g|SpellsGreaterCommand}command, greater{/g}. " +
                    "\nIf you are wearing heavy armor, your orders’s target takes an additional –2 penalty on its saving throw to resist the order " +
                    "(regardless of the target’s alignment; this stacks with the penalty chaotic creatures take). " +
                    "\nYou must be at least 7th level to select this revelation.");
                bp.m_Icon = CommandGreaterSpell.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationIronOrderAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationIronOrderAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 15;
                    c.m_Feature = OracleRevelationIronOrderUpgrade.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<OwnerAbilityTargetSavingThrowBonusExpanded>(c => {
                    c.Bonus = -4;
                    c.Descriptor = ModifierDescriptor.None;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.SpellLike;
                    c.OnlyTheseAbilities = true;
                    c.m_Spells = new BlueprintAbilityReference[] {
                        OracleRevelationIronOrderApproachAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFallAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFleeAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderHaltAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderApproachGreaterAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFallGreaterAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFleeGreaterAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderHaltGreaterAbility.ToReference<BlueprintAbilityReference>()
                    };
                    c.Conditions = new ConditionsChecker() {
                        Operation = Operation.Or,
                        Conditions = new Condition[] {
                            new ContextConditionAlignment() {
                                Alignment = AlignmentComponent.Chaotic,
                                CheckCaster = false,
                                Not = false
                            }
                        }
                    };                    
                });
                bp.AddComponent<OwnerAbilityTargetSavingThrowBonusExpanded>(c => {
                    c.Bonus = -2;
                    c.Descriptor = ModifierDescriptor.None;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.SpellLike;
                    c.OnlyTheseAbilities = true;
                    c.m_Spells = new BlueprintAbilityReference[] {
                        OracleRevelationIronOrderApproachAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFallAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFleeAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderHaltAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderApproachGreaterAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFallGreaterAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderFleeGreaterAbility.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronOrderHaltGreaterAbility.ToReference<BlueprintAbilityReference>()
                    };
                    c.Conditions = new ConditionsChecker() {
                        Operation = Operation.Or,
                        Conditions = new Condition[] {
                            new ContextConditionCasterArmorCategory() {
                                armorCategory = new ArmorProficiencyGroup[] {ArmorProficiencyGroup.Heavy},
                                Not = false
                            }
                        }
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationIronOrderResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
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
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationIronOrderFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Resiliency
            //needs simplifying to match CRPG downstate         Diehard at lvl 1 - bonus to fort/will saves under 0 health at level 7
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
