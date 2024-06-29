using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.TempMapCode.Ambush;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using static TabletopTweaks.Core.MechanicsChanges.AdditionalModifierDescriptors;
using Kingmaker.AI.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Facts;
using System.Collections.Generic;
using Kingmaker.AI.Blueprints.Considerations;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;



namespace ExpandedContent.Tweaks.Mysteries {
    internal class ShadowMystery {
        public static void AddShadowMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var ShadowMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleShadowMystery.png");

            //Spelllist
            var VanishSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f001c73999fb5a543a199f890108d936");
            var InvisibilitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("89940cde01689fb46946b2f8cd7b66b7");
            var SeeInvisibilityCommuninalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1a045f845778dc54db1c2be33a8c3c0a");
            //var ShadowStepSpell = Resources.GetBlueprint<BlueprintAbility>("");//Need to make this
            var VampiricShadowShieldSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a34921035f2a6714e9be5ca76c5e34b5");
            var DispelMagicGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0f761b808dc4b149b08eaf44b99f633");
            var InvisibilityMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("98310a099009bbd4dbdf66bcef58b4cd");
            var ShadowEvocationGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3c4a2d4181482e84d9cd752ef8edc3b6");
            var ShadesSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("70e12191790f69a439ea0132c75f83aa");
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
            var OracleShadowSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleShadowSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = VanishSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = InvisibilitySpell;//Replace is CO+ is installed
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibilityCommuninalSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShadowStepSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = VampiricShadowShieldSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreaterSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = InvisibilityMassSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShadowEvocationGreaterSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShadesSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherShadowSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherShadowSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = VanishSpell;
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
            var OceansEchoShadowSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoShadowSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = VanishSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibilityCommuninalSpell;
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
                    c.m_Spell = ShadowEvocationGreaterSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShadesSpell;
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation            
            var OracleShadowFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleShadowFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, your body becomes permanently suffused with the essence of the Shadow Plane. You gain  immunity to cold, critical hits, and sneak attacks. " +
                    "You also gain regeneration 5 as long as you are not affected by any abilities that cause you to emit light. " +
                    "In addition, any spells you cast of the shadow subschool or with the darkness descriptor are automatically extended without affecting their spell level");
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Cold;
                });
                bp.AddComponent<AddImmunityToCriticalHits>();
                bp.AddComponent<AddImmunityToPrecisionDamage>();
                bp.AddComponent<AddEffectFastHealing>(c => {
                    c.Heal = 5;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Extend;
                    c.Abilities = new List<BlueprintAbilityReference> { 
                    //Fill later
                    };
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = false;
                    c.m_Spellbook = new BlueprintSpellbookReference();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Main Mystery Feature
            var OracleShadowMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleShadowMysteryFeature", bp => {
                bp.m_Icon = ShadowMysteryIcon;
                bp.SetName("Shadow");
                bp.SetDescription("An oracle with the shadow mystery adds {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Stealth}Stealth{/g} " +
                    "and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleShadowFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleShadowSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
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
            var EnlightnedPhilosopherShadowMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherShadowMysteryFeature", bp => {
                bp.m_Icon = ShadowMysteryIcon;
                bp.SetName("Shadow");
                bp.SetDescription("An oracle with the shadow mystery adds {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Stealth}Stealth{/g} " +
                    "and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherShadowSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
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
            var DivineHerbalistShadowMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistShadowMysteryFeature", bp => {
                bp.m_Icon = ShadowMysteryIcon;
                bp.SetName("Shadow");
                bp.SetDescription("Gain access to the spells and revelations of the shadow mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleShadowFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleShadowSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoShadowMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoShadowMysteryFeature", bp => {
                bp.m_Icon = ShadowMysteryIcon;
                bp.SetName("Shadow");
                bp.SetDescription("Gain access to the spells and revelations of the shadow mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleShadowFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoShadowSpells.ToReference<BlueprintFeatureReference>();
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

            //Army of Darkness
            var MinorNightglassShadowTemplateBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("a651737f5a4d46bc93c5f96fd3fa3848");
            var SubtypeAngel = Resources.GetBlueprintReference<BlueprintFeatureReference>("65d9b6889df167044abb624e2160c43b");
            var SubtypeAzata = Resources.GetBlueprintReference<BlueprintFeatureReference>("e422746933151f3469f4c2484f9263db");
            var SubtypeDaemon = Resources.GetBlueprintReference<BlueprintFeatureReference>("75c3a26f37be0c74196deaf64d81ee1a");
            var SubtypeDemodand = Resources.GetBlueprintReference<BlueprintFeatureReference>("0d112671041420340b5ce7e9ab7b4320");
            var SubtypeDemon = Resources.GetBlueprintReference<BlueprintFeatureReference>("dc960a234d365cb4f905bdc5937e623a");
            var SubtypeDevil = Resources.GetBlueprintReference<BlueprintFeatureReference>("6026db1a1a84ac14d9e94404af3baf0a");
            var OracleRevelationArmyOfDarknessFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationArmyOfDarknessFeature", bp => {
                bp.SetName("Army of Darkness");
                bp.SetDescription("Whenever you cast a summon monster spell and summon a good or evil outsider creature, you also summon it with the shadow creature template. " +
                    "This revelation counts as having the Spell Focus (conjuration) feat for the purpose of meeting the prerequisites of the Augment Summoning feat, as well " +
                    "as any feat that lists Augment Summoning as a prerequisite. \r\nA shadow creature gains {g|Encyclopedia:Spell_Resistance}spell resistance{/g} equal to its Hit Dice +6. " +
                    "It also gains:\r\n1 — 4 {g|Encyclopedia:Hit_Dice}HD{/g}: {g|Encyclopedia:Energy_Resistance}resistance{/g} 5 to cold and electricity\r\n5 — 10 HD: resistance 10 " +
                    "to cold and electricity, {g|Encyclopedia:Damage_Reduction}DR{/g} 5/magic\r\n11+ HD: resistance 15 to cold and electricity, DR 10/magic\r\nShadow Blend: A shadow " +
                    "creature blends into the shadows, gaining {g|Encyclopedia:Concealment}concealment{/g} (20% miss chance).");
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = null;
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = SubtypeAngel;
                    c.m_buff = MinorNightglassShadowTemplateBuff;
                    c.IsInfinity = true;
                });
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = null;
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = SubtypeAzata;
                    c.m_buff = MinorNightglassShadowTemplateBuff;
                    c.IsInfinity = true;
                });
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = null;
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = SubtypeDaemon;
                    c.m_buff = MinorNightglassShadowTemplateBuff;
                    c.IsInfinity = true;
                });
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = null;
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = SubtypeDemodand;
                    c.m_buff = MinorNightglassShadowTemplateBuff;
                    c.IsInfinity = true;
                });
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = null;
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = SubtypeDemon;
                    c.m_buff = MinorNightglassShadowTemplateBuff;
                    c.IsInfinity = true;
                });
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = null;
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = SubtypeDevil;
                    c.m_buff = MinorNightglassShadowTemplateBuff;
                    c.IsInfinity = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoShadowMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
                //Patching the Augment Summoning Feat
            var AugmentSummoningFeat = Resources.GetBlueprint<BlueprintFeature>("38155ca9e4055bb48a89240a2055dcc3");
            AugmentSummoningFeat.AddComponent<PrerequisiteFeature>(c => { 
                c.m_Feature = OracleRevelationArmyOfDarknessFeature.ToReference<BlueprintFeatureReference>();
                c.Group = Prerequisite.GroupType.Any;
            });
            AugmentSummoningFeat.GetComponent<PrerequisiteParametrizedFeature>().TemporaryContext(c => {
                c.Group = Prerequisite.GroupType.Any;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationArmyOfDarknessFeature.ToReference<BlueprintFeatureReference>());



            MysteryTools.RegisterMystery(OracleShadowMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleShadowMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherShadowMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherShadowMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistShadowMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistShadowMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoShadowMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoShadowMysteryFeature);
            MysteryTools.RegisterHermitMystery(OracleShadowMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleShadowMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleShadowMysteryFeature);

        }
    }
}
