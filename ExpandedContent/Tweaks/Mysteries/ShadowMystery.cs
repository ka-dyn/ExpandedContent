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
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;



namespace ExpandedContent.Tweaks.Mysteries {
    internal class ShadowMystery {
        public static void AddShadowMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var ShadowMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleShadowMystery.png");
            var OracleRevelationCloakOfDarknessIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleRevelationCloakOfDarkness.jpg");


            //Spelllist
            var DoomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fbdd8c455ac4cde4a9a3e18c84af9485");
            var InvisibilitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("89940cde01689fb46946b2f8cd7b66b7");
            var SeeInvisibilityCommuninalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1a045f845778dc54db1c2be33a8c3c0a");
            var ShadowJauntAbility = Resources.GetModBlueprint<BlueprintAbility>("ShadowJauntAbility");
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
                    c.m_Spell = DoomSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = InvisibilitySpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibilityCommuninalSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShadowJauntAbility.ToReference<BlueprintAbilityReference>();
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
                    c.m_Spell = DoomSpell;
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
                    c.m_Spell = DoomSpell;
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

            //Spelllist for Dark Secrets
            //1
            var VanishSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f001c73999fb5a543a199f890108d936");
            //ShadowTrapSpell
            //TouchOfBlindness
            //2
            var BlurSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("14ec7a4e52e90fa47a4c8d63c69fd5c1");
            var DustOfTwilightAbility = Resources.GetModBlueprint<BlueprintAbility>("DustOfTwilightAbility");
            var HauntingMistsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ed22aa8751c049fa915dabfa29712c08");
            //ShadowClawsAbility (If I add it)
            //3
            var DisplacementSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("903092f6488f9ce45a80943923576ab3");
            var ShadowStepAbility = Resources.GetModBlueprint<BlueprintAbility>("ShadowStepAbility");
            //ShadowEnchantment
            //4
            var ShadowConjurationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("caac251ca7601324bbe000372a0a1005");
            //ShadowJaunt
            //MydriaticSpontaneity
            //5
            var ShadowEvocationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("237427308e48c3341b3d532b9d3a001f");
            //Vamp Shadow Shield
            //CloakOfShadows (If I add it)
            //6
            var PhantasmalPutrefactionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1f2e6019ece86d64baa5effa15e81ecc");
            //ShadowEnchantmentGreater
            //7
            var ShadowConjurationGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("08255ea4cdd812341af93f9cd113acb9");
            var UmbrallStrikeSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("474ed0aa656cc38499cc9a073d113716");
            //HungryDarkness (If I add it)
            //MydriaticSpontaneityMass
            //8
            //ShadowEvocationGreaterSpell
            //9
            //Shades
            var FormOfTheExoticDragonAbilityUmbral = Resources.GetModBlueprint<BlueprintAbility>("FormOfTheExoticDragonAbilityUmbral");


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
                        VanishSpell,
                        BlurSpell,
                        HauntingMistsSpell,
                        DisplacementSpell,
                        ShadowConjurationSpell,
                        ShadowEvocationSpell,
                        VampiricShadowShieldSpell,
                        PhantasmalPutrefactionSpell,
                        ShadowConjurationGreaterSpell,
                        UmbrallStrikeSpell,
                        ShadowEvocationGreaterSpell,
                        ShadesSpell,
                        DustOfTwilightAbility.ToReference<BlueprintAbilityReference>(),
                        FormOfTheExoticDragonAbilityUmbral.ToReference<BlueprintAbilityReference>()
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

            #region Army of Darkness
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
            #endregion
            #region Cloak of Darkness
            var OracleRevelationCloakOfDarknessResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationCloakOfDarknessResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    LevelIncrease = 1,
                    StartingIncrease = 1,
                };
            });
            var OracleRevelationCloakOfDarknessBuff = Helpers.CreateBuff("OracleRevelationCloakOfDarknessBuff", bp => {
                bp.SetName("Coat of Darkness");
                bp.SetDescription("You conjure a cloak of shadowy darkness that grants you a +4 armor bonus and a +2 circumstance bonus on Stealth checks. " +
                    "At 7th level, and every four levels thereafter, these bonuses increase by +2. You can use this cloak for 1 hour per day per oracle level. " +
                    "The duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationCloakOfDarknessIcon;
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
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {//X2
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
                bp.AddComponent<ContextCalculateSharedValue>(c => {//AC
                    c.ValueType = AbilitySharedValue.StatBonus;
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
                bp.AddComponent<ContextCalculateSharedValue>(c => {//Stealth
                    c.ValueType = AbilitySharedValue.DurationSecond;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 2,
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
                        ValueShared = AbilitySharedValue.StatBonus,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Circumstance;
                    c.Stat = StatType.SkillStealth;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.DurationSecond,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
            });
            var OracleRevelationCloakOfDarknessAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationCloakOfDarknessAbility", bp => {
                bp.SetName("Coat of Darkness");
                bp.SetDescription("You conjure a cloak of shadowy darkness that grants you a +4 armor bonus and a +2 circumstance bonus on Stealth checks. " +
                    "At 7th level, and every four levels thereafter, these bonuses increase by +2. You can use this cloak for 1 hour per day per oracle level. " +
                    "The duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationCloakOfDarknessIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = OracleRevelationCloakOfDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationCloakOfDarknessBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationCloakOfDarknessFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationCloakOfDarknessFeature", bp => {
                bp.SetName("Coat of Darkness");
                bp.SetDescription("You conjure a cloak of shadowy darkness that grants you a +4 armor bonus and a +2 circumstance bonus on Stealth checks. " +
                    "At 7th level, and every four levels thereafter, these bonuses increase by +2. You can use this cloak for 1 hour per day per oracle level. " +
                    "The duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationCloakOfDarknessIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationCloakOfDarknessAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationCloakOfDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
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
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationCloakOfDarknessFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Dark Secrets
            var OracleRevelationDarkSecretsSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("OracleRevelationDarkSecretsSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            VanishSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BlurSpell,
                            HauntingMistsSpell,
                            DustOfTwilightAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DisplacementSpell,
                            ShadowStepAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShadowConjurationSpell,
                            ShadowJauntAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShadowEvocationSpell,
                            VampiricShadowShieldSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PhantasmalPutrefactionSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShadowConjurationGreaterSpell,
                            UmbrallStrikeSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShadowEvocationGreaterSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShadesSpell,
                            FormOfTheExoticDragonAbilityUmbral.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });

            var OracleRevelationDarkSecretsSpellLevel1 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel1", bp => {
                bp.SetName("Dark Secret (1st Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 1;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 1;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel2 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel2", bp => {
                bp.SetName("Dark Secret (2nd Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 2;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 2;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel3 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel3", bp => {
                bp.SetName("Dark Secret (3rd Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 3;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 3;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel4 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel4", bp => {
                bp.SetName("Dark Secret (4th Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 4;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 4;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel5 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel5", bp => {
                bp.SetName("Dark Secret (5th Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 5;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 5;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel6 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel6", bp => {
                bp.SetName("Dark Secret (6th Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 6;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 6;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel7 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel7", bp => {
                bp.SetName("Dark Secret (7th Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 7;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 7;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel8 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel8", bp => {
                bp.SetName("Dark Secret (8th Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 8;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 8;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsSpellLevel9 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("OracleRevelationDarkSecretsSpellLevel9", bp => {
                bp.SetName("Dark Secret (9th Level)");
                bp.SetDescription("Choose a shadow or darkness spell.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 9;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = OracleRevelationDarkSecretsSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 9;
                bp.DisallowSpellsInSpellList = false;
            });
            var OracleRevelationDarkSecretsProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationDarkSecretsProgression", bp => {
                bp.SetName("Dark Secrets");
                bp.SetDescription("You learn the hidden secrets surrounding the casting of shadow spells. This revelation grants a 1st level shadow spell from the wizard spell list, at level 4 and every even level after, " +
                    "you may select another shadow spell that you are the appropriate level to cast (at level 4 you may pick up to 2nd level spells, level 6 3rd level spells, ect...). \nIf this revelation is taken at a " +
                    "later level, spells are granted retroactively, with spells selected still being level appropriate.");
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleRevelationDarkSecretsSpellLevel1),
                    Helpers.LevelEntry(4, OracleRevelationDarkSecretsSpellLevel2),
                    Helpers.LevelEntry(6, OracleRevelationDarkSecretsSpellLevel3),
                    Helpers.LevelEntry(8, OracleRevelationDarkSecretsSpellLevel4),
                    Helpers.LevelEntry(10, OracleRevelationDarkSecretsSpellLevel5),
                    Helpers.LevelEntry(12, OracleRevelationDarkSecretsSpellLevel6),
                    Helpers.LevelEntry(14, OracleRevelationDarkSecretsSpellLevel7),
                    Helpers.LevelEntry(16, OracleRevelationDarkSecretsSpellLevel8),
                    Helpers.LevelEntry(18, OracleRevelationDarkSecretsSpellLevel9),
                    Helpers.LevelEntry(20, OracleRevelationDarkSecretsSpellLevel9)
                };
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoShadowMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.GiveFeaturesForPreviousLevels = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDarkSecretsProgression.ToReference<BlueprintFeatureReference>());

            #endregion
            //Pierce the Shadows??
            #region Shadow Mastery
            var OracleRevelationShadowMasteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationShadowMasteryFeature", bp => {
                bp.SetName("Shadow Mastery");
                bp.SetDescription("Whenever you cast an illusion spell from the shadow subschool, increase the strength of such spells by 1% per oracle " +
                    "level you have. You must be at least 7th level to choose this revelation.");
                bp.m_Icon = OracleRevelationCloakOfDarknessIcon;
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoShadowMysteryFeature.ToReference<BlueprintFeatureReference>()
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
                bp.Ranks = 1;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationShadowMasteryCustomProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("OracleRevelationShadowMasteryCustomProperty", bp => {
                bp.AddComponent<FactRankGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.m_Fact = OracleRevelationShadowMasteryFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<SummClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_Negate = false
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {                        
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.BaseValue = 0;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Multiply;
            });
            var ShadowUnitProperties = new BlueprintUnitProperty[] {
                Resources.GetBlueprint<BlueprintUnitProperty>("a6500531899940c2803695f26f513caa"),//ShadowEvo
                Resources.GetBlueprint<BlueprintUnitProperty>("0f813eb338594c5bb840c5583fd29c3d"),//ShadowEvo Greater
                Resources.GetBlueprint<BlueprintUnitProperty>("fd0106a7d062420f8b3e5bcf03f099db")//Shades
            };
            foreach (var prop in ShadowUnitProperties) {
                prop.AddComponent<CustomPropertyGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_StartLevel = 0,
                        m_StepLevel = 0,
                        m_Negate = false,
                        m_LimitType = PropertySettings.LimitType.None,
                        m_Min = 0,
                        m_Max = 0,
                    };
                    c.m_Property = OracleRevelationShadowMasteryCustomProperty.ToReference<BlueprintUnitPropertyReference>();
                });
            }
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationShadowMasteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Stealth Mastery
            var SkillFocusStealth = Resources.GetBlueprintReference<BlueprintFeatureReference>("3a8d34905eae4a74892aae37df3352b9");
            var FastStealth = Resources.GetBlueprintReference<BlueprintFeatureReference>("97a6aa2b64dd21a4fac67658a91067d7");
            var AssassinHideInPlainSight = Resources.GetBlueprintReference<BlueprintFeatureReference>("fa113a54bc69daf4485ad89315c6cfb6");
            var OracleRevelationStealthMasteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationStealthMasteryProgression", bp => {
                bp.SetName("Stealth Mastery");
                bp.SetDescription("You gain Skill Focus (Stealth). \nAt 8th level, you gain the fast stealth talent, allowing you to move at full speed while stealthed. " +
                    "\nAt 16th level, you gain the hide in plain sight assassin class feature. \n An assassin can use the " +
                    "{g|Encyclopedia:Stealth}Stealth{/g} {g|Encyclopedia:Skills}skill{/g} even while being observed. An assassin can hide himself from view in the open " +
                    "without having anything to actually hide behind.");
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, SkillFocusStealth),
                    Helpers.LevelEntry(8, FastStealth),
                    Helpers.LevelEntry(16, AssassinHideInPlainSight)
                };
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoShadowMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.GiveFeaturesForPreviousLevels = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationStealthMasteryProgression.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Wings of Darkness
            var BuffWingsDraconicBlack = Resources.GetBlueprint<BlueprintBuff>("ddfe6e85e1eed7a40aa911280373c228");
            var OracleRevelationWingsOfDarknessBuff = Helpers.CreateBuff("OracleRevelationWingsOfDarknessBuff", bp => {
                bp.SetName("Wings of Darkness");
                bp.SetDescription("As a swift action, you can manifest a set of wings that grant you a speed of 60 feet. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicBlack.m_Icon;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.DifficultTerrain;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Ground;
                });
                bp.AddComponent<AddEquipmentEntity>(c => {
                    c.EquipmentEntity = new EquipmentEntityLink() { AssetId = "7cd58db91c36cae46b25efdba2a23f24" };
                });
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var OracleRevelationWingsOfDarknessResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationWingsOfDarknessResource", bp => {
                bp.m_Min = 7;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    IncreasedByLevel = true,
                    StartingLevel = 7,
                    LevelIncrease = 1,
                    StartingIncrease = 1,
                    LevelStep = 1,
                    PerStepIncrease = 1,
                };
            });
            var OracleRevelationWingsOfDarknessExtraUse99 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfDarknessExtraUse99", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = OracleRevelationWingsOfDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 99;
                });
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfDarknessExtraUse9 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfDarknessExtraUse9", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = OracleRevelationWingsOfDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 9;
                });
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.Ranks = 3;
            });
            var OracleRevelationWingsOfDarknessUnlimited = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfDarknessUnlimited", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfDarknessAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfDarknessAbility", bp => {
                bp.SetName("Wings of Darkness");
                bp.SetDescription("As a swift action, you can manifest a set of wings that grant you a speed of 60 feet. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicBlack.m_Icon;                
                bp.m_Buff = OracleRevelationWingsOfDarknessBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfDarknessUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfDarknessFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfDarknessFeature", bp => {
                bp.SetName("Wings of Darkness");
                bp.SetDescription("As a swift action, you can manifest a set of wings that grant you a speed of 60 feet. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicBlack.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfDarknessResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfDarknessAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfDarknessProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationWingsOfDarknessProgression", bp => {
                bp.SetName("Wings of Darkness");
                bp.SetDescription("As a swift action, you can manifest a set of wings that grant you a speed of 60 feet. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(7, OracleRevelationWingsOfDarknessFeature),
                    Helpers.LevelEntry(11, OracleRevelationWingsOfDarknessExtraUse99),
                    Helpers.LevelEntry(12, OracleRevelationWingsOfDarknessExtraUse9),
                    Helpers.LevelEntry(13, OracleRevelationWingsOfDarknessExtraUse9),
                    Helpers.LevelEntry(14, OracleRevelationWingsOfDarknessExtraUse9),
                    Helpers.LevelEntry(15, OracleRevelationWingsOfDarknessUnlimited)
                };
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistShadowMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoShadowMysteryFeature.ToReference<BlueprintFeatureReference>()
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
                bp.GiveFeaturesForPreviousLevels = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationWingsOfDarknessProgression.ToReference<BlueprintFeatureReference>());
            #endregion

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
