using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
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
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class DarkTapestryMystery {
        public static void AddDarkTapestryMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var DarkTapestryMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleDarkTapestryMystery.png");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");

            //Spelllist
            var EntropicShieldAbility = Resources.GetModBlueprint<BlueprintAbility>("EntropicShieldAbility");
            var DustOfTwilightAbility = Resources.GetModBlueprint<BlueprintAbility>("DustOfTwilightAbility");
            var DeepSlumberSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7658b74f626c56a49939d9c20580885e");
            var BestowCurseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("989ab5c44240907489aba0a8568d0603");
            var FeeblemindSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("444eed6e26f773a40ab6e4d160c67faa");
            var SummonMonsterVIBaseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e740afbab0147944dab35d83faa0ae1c");
            var InsanitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2b044152b3620c841badb090e01ed9de");
            var ScintillatingPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4dc60d08c6c4d3c47b413904e4de5ff0");
            var WeirdSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("870af83be6572594d84d276d7fc583e0");
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
            var OracleDarkTapestrySpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleDarkTapestrySpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = EntropicShieldAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DustOfTwilightAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DeepSlumberSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BestowCurseSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FeeblemindSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SummonMonsterVIBaseSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = InsanitySpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ScintillatingPatternSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WeirdSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherDarkTapestrySpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherDarkTapestrySpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = EntropicShieldAbility.ToReference<BlueprintAbilityReference>();
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
            var OceansEchoDarkTapestrySpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoDarkTapestrySpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = EntropicShieldAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DeepSlumberSpell;
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
                    c.m_Spell = ScintillatingPatternSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WeirdSpell;
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var ShapeChangeSpell = Resources.GetBlueprint<BlueprintAbility>("22b9044aa229815429d57d0a30e4b739");
            var OracleRevelationShapeChangeFreeResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationShapeChangeFreeResource", bp => {
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
            var OracleRevelationShapeChangeFreeAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationShapeChangeFreeAbility", bp => {
                bp.m_DisplayName = ShapeChangeSpell.m_DisplayName;
                bp.m_Description = ShapeChangeSpell.m_Description;
                bp.m_DescriptionShort = ShapeChangeSpell.m_DescriptionShort;
                bp.m_Icon = ShapeChangeSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = ShapeChangeSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = ShapeChangeSpell.LocalizedSavingThrow;
                bp.Components = ShapeChangeSpell.Components;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationShapeChangeFreeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
            });
            OracleRevelationShapeChangeFreeAbility.RemoveComponents<SpellListComponent>();
            OracleRevelationShapeChangeFreeAbility.RemoveComponents<RecommendationNoFeatFromGroup>();            
            var OracleDarkTapestryFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleDarkTapestryFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become a truly alien and unnatural creature. You gain damage reduction 5/— and immunity to acid, critical hits, " +
                    "and sneak attacks. Once per day, you can cast shapechange as a spell-like ability without requiring a material component.");
                bp.AddComponent<AddImmunityToCriticalHits>();
                bp.AddComponent<AddImmunityToPrecisionDamage>();
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = Kingmaker.Enums.Damage.DamageEnergyType.Acid;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationShapeChangeFreeAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationShapeChangeFreeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Main Mystery Feature
            var OracleDarkTapestryMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleDarkTapestryMysteryFeature", bp => {
                bp.m_Icon = DarkTapestryMysteryIcon;
                bp.SetName("Dark Tapestry");
                bp.SetDescription("An oracle with the dark tapestry mystery adds {g|Encyclopedia:Knowledge_Arcana}Knowledge (Arcana) {/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Stealth}Stealth{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleDarkTapestryFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleDarkTapestrySpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherDarkTapestryMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherDarkTapestryMysteryFeature", bp => {
                bp.m_Icon = DarkTapestryMysteryIcon;
                bp.SetName("Dark Tapestry");
                bp.SetDescription("An oracle with the dark tapestry mystery adds {g|Encyclopedia:Knowledge_Arcana}Knowledge (Arcana) {/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Stealth}Stealth{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherDarkTapestrySpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistDarkTapestryMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistDarkTapestryMysteryFeature", bp => {
                bp.m_Icon = DarkTapestryMysteryIcon;
                bp.SetName("Dark Tapestry");
                bp.SetDescription("Gain access to the spells and revelations of the dark tapestry mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleDarkTapestryFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleDarkTapestrySpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoDarkTapestryMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoDarkTapestryMysteryFeature", bp => {
                bp.m_Icon = DarkTapestryMysteryIcon;
                bp.SetName("Dark Tapestry");
                bp.SetDescription("Gain access to the spells and revelations of the dark tapestry mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleDarkTapestryFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoDarkTapestrySpells.ToReference<BlueprintFeatureReference>();
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

            #region Brain Drain????

            #endregion
            #region Cloak of Darkness
            var OracleRevelationCloakOfDarknessFeature = Resources.GetModBlueprint<BlueprintFeature>("OracleRevelationCloakOfDarknessFeature").GetComponent<PrerequisiteFeaturesFromList>();
            OracleRevelationCloakOfDarknessFeature.m_Features = OracleRevelationCloakOfDarknessFeature.m_Features.AppendToArray(OracleDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationCloakOfDarknessFeature.m_Features = OracleRevelationCloakOfDarknessFeature.m_Features.AppendToArray(EnlightnedPhilosopherDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationCloakOfDarknessFeature.m_Features = OracleRevelationCloakOfDarknessFeature.m_Features.AppendToArray(DivineHerbalistDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationCloakOfDarknessFeature.m_Features = OracleRevelationCloakOfDarknessFeature.m_Features.AppendToArray(OceansEchoDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Dweller in Darkness
            var OracleRevelationDwellerInTheDarknessFeature = Resources.GetModBlueprint<BlueprintFeature>("OracleRevelationDwellerInTheDarknessFeature").GetComponent<PrerequisiteFeaturesFromList>();
            OracleRevelationDwellerInTheDarknessFeature.m_Features = OracleRevelationDwellerInTheDarknessFeature.m_Features.AppendToArray(OracleDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationDwellerInTheDarknessFeature.m_Features = OracleRevelationDwellerInTheDarknessFeature.m_Features.AppendToArray(EnlightnedPhilosopherDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationDwellerInTheDarknessFeature.m_Features = OracleRevelationDwellerInTheDarknessFeature.m_Features.AppendToArray(DivineHerbalistDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationDwellerInTheDarknessFeature.m_Features = OracleRevelationDwellerInTheDarknessFeature.m_Features.AppendToArray(OceansEchoDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Gift of Madness

            #endregion
            #region Interstellar Void
            var OracleRevelationInterstallarVoidFeature = Resources.GetModBlueprint<BlueprintFeature>("OracleRevelationInterstallarVoidFeature").GetComponent<PrerequisiteFeaturesFromList>();
            OracleRevelationInterstallarVoidFeature.m_Features = OracleRevelationInterstallarVoidFeature.m_Features.AppendToArray(OracleDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationInterstallarVoidFeature.m_Features = OracleRevelationInterstallarVoidFeature.m_Features.AppendToArray(EnlightnedPhilosopherDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationInterstallarVoidFeature.m_Features = OracleRevelationInterstallarVoidFeature.m_Features.AppendToArray(DivineHerbalistDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationInterstallarVoidFeature.m_Features = OracleRevelationInterstallarVoidFeature.m_Features.AppendToArray(OceansEchoDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Many Forms

            #endregion
            #region Pierce the Veil
            var OracleRevelationPierceTheShadowsFeature = Resources.GetModBlueprint<BlueprintFeature>("OracleRevelationPierceTheShadowsFeature").GetComponent<PrerequisiteFeaturesFromList>();
            OracleRevelationPierceTheShadowsFeature.m_Features = OracleRevelationPierceTheShadowsFeature.m_Features.AppendToArray(OracleDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationPierceTheShadowsFeature.m_Features = OracleRevelationPierceTheShadowsFeature.m_Features.AppendToArray(EnlightnedPhilosopherDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationPierceTheShadowsFeature.m_Features = OracleRevelationPierceTheShadowsFeature.m_Features.AppendToArray(DivineHerbalistDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationPierceTheShadowsFeature.m_Features = OracleRevelationPierceTheShadowsFeature.m_Features.AppendToArray(OceansEchoDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Touch of the Void

            #endregion
            #region Wings of Darkness
            var OracleRevelationWingsOfDarknessProgression = Resources.GetModBlueprint<BlueprintProgression>("OracleRevelationWingsOfDarknessProgression").GetComponent<PrerequisiteFeaturesFromList>();
            OracleRevelationWingsOfDarknessProgression.m_Features = OracleRevelationWingsOfDarknessProgression.m_Features.AppendToArray(OracleDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationWingsOfDarknessProgression.m_Features = OracleRevelationWingsOfDarknessProgression.m_Features.AppendToArray(EnlightnedPhilosopherDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationWingsOfDarknessProgression.m_Features = OracleRevelationWingsOfDarknessProgression.m_Features.AppendToArray(DivineHerbalistDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            OracleRevelationWingsOfDarknessProgression.m_Features = OracleRevelationWingsOfDarknessProgression.m_Features.AppendToArray(OceansEchoDarkTapestryMysteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion



            MysteryTools.RegisterMystery(OracleDarkTapestryMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleDarkTapestryMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherDarkTapestryMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherDarkTapestryMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistDarkTapestryMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistDarkTapestryMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoDarkTapestryMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoDarkTapestryMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleDarkTapestryMysteryFeature);
        }
    }
}
