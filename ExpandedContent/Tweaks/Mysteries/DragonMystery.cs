using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
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
            var BuffWingsMutagen = Resources.GetBlueprint<BlueprintBuff>("e4979934bdb39d842b28bee614606823");
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
            //Elemental flags for revelations
            var OracleDragonMysteryFlagAcid = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonMysteryFlagAcid", bp => {
                bp.SetName("Selected element - Acid");
                bp.SetDescription("");
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleDragonMysteryFlagFire = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonMysteryFlagFire", bp => {
                bp.SetName("Selected element - Fire");
                bp.SetDescription("");
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleDragonMysteryFlagCold = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonMysteryFlagCold", bp => {
                bp.SetName("Selected element - Cold");
                bp.SetDescription("");
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleDragonMysteryFlagElectric = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonMysteryFlagElectric", bp => {
                bp.SetName("Selected element - Electric");
                bp.SetDescription("");
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            //Final Revelation
            var OracleDragonFinalRevelationAcid = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonFinalRevelationAcid", bp => {
                bp.SetName("Final Revelation - Acid");
                bp.SetDescription("Your draconic destiny unfolds. You gain immunity to paralysis, sleep, and {g|Encyclopedia:Energy_Damage}acid damage{/g}. " +
                    "If you have the breath weapon revelation, you can use your breath weapon an unlimited number of times per day, though no more often than once " +
                    "every 1d4+1 rounds.");
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Paralyzed;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Sleeping;
                });
                bp.AddComponent<AddEnergyDamageImmunity>(c => {
                    c.EnergyType = DamageEnergyType.Acid;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleDragonMysteryFlagAcid.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleDragonFinalRevelationCold = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonFinalRevelationCold", bp => {
                bp.SetName("Final Revelation - Cold");
                bp.SetDescription("Your draconic destiny unfolds. You gain immunity to paralysis, sleep, and {g|Encyclopedia:Energy_Damage}cold damage{/g}. " +
                    "If you have the breath weapon revelation, you can use your breath weapon an unlimited number of times per day, though no more often than once " +
                    "every 1d4+1 rounds.");
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Paralyzed;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Sleeping;
                });
                bp.AddComponent<AddEnergyDamageImmunity>(c => {
                    c.EnergyType = DamageEnergyType.Cold;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleDragonMysteryFlagCold.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleDragonFinalRevelationElectric = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonFinalRevelationElectric", bp => {
                bp.SetName("Final Revelation - Electric");
                bp.SetDescription("Your draconic destiny unfolds. You gain immunity to paralysis, sleep, and {g|Encyclopedia:Energy_Damage}electrical damage{/g}. " +
                    "If you have the breath weapon revelation, you can use your breath weapon an unlimited number of times per day, though no more often than once " +
                    "every 1d4+1 rounds.");
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Paralyzed;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Sleeping;
                });
                bp.AddComponent<AddEnergyDamageImmunity>(c => {
                    c.EnergyType = DamageEnergyType.Electricity;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleDragonMysteryFlagElectric.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleDragonFinalRevelationFire = Helpers.CreateBlueprint<BlueprintFeature>("OracleDragonFinalRevelationFire", bp => {
                bp.SetName("Final Revelation - Fire");
                bp.SetDescription("Your draconic destiny unfolds. You gain immunity to paralysis, sleep, and {g|Encyclopedia:Energy_Damage}fire damage{/g}. " +
                    "If you have the breath weapon revelation, you can use your breath weapon an unlimited number of times per day, though no more often than once " +
                    "every 1d4+1 rounds.");
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Paralyzed;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Sleeping;
                });
                bp.AddComponent<AddEnergyDamageImmunity>(c => {
                    c.EnergyType = DamageEnergyType.Fire;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleDragonMysteryFlagFire.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleDragonFinalRevelation = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleDragonFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, your draconic destiny unfolds. You gain immunity to paralysis, sleep, and damage of your energy type. " +
                    "If you have the breath weapon revelation, you can use your breath weapon an unlimited number of times per day, though no more often than once " +
                    "every 1d4+1 rounds.");
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    OracleDragonFinalRevelationAcid.ToReference<BlueprintFeatureReference>(),
                    OracleDragonFinalRevelationCold.ToReference<BlueprintFeatureReference>(),
                    OracleDragonFinalRevelationElectric.ToReference<BlueprintFeatureReference>(),
                    OracleDragonFinalRevelationFire.ToReference<BlueprintFeatureReference>()
                };
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
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
            //Talons of the Dragon
            var BloodlineDraconicClawsResource = Resources.GetBlueprint<BlueprintAbilityResource>("5be91334e3de5aa458ade509cc16daff");
            var BloodlineDraconicBlackClawsFeatureLevel1 = Resources.GetBlueprint<BlueprintFeature>("2594e96fb980fdc49b139fdf88bc7679");
            var BloodlineDraconicBlackClawsFeatureLevel2 = Resources.GetBlueprint<BlueprintFeature>("838044e2a5853bb41b9e0a123a44432d");
            var BloodlineDraconicBlackClawsFeatureLevel3 = Resources.GetBlueprint<BlueprintFeature>("0a5e2d4cdbaed5742893ce52cd7235c3");
            var BloodlineDraconicBlackClawsFeatureLevel4 = Resources.GetBlueprint<BlueprintFeature>("d8b6482887e899a4199b99c6a3de106a");
            var OracleRevelationDragonTalonsAcidLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsAcidLevel1", bp => {
                bp.SetName("Talons of the Dragon - Acid");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}acid damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 5;
                    c.m_Feature = BloodlineDraconicBlackClawsFeatureLevel1.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = BloodlineDraconicClawsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonTalonsAcidLevel2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsAcidLevel2", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.m_Feature = BloodlineDraconicBlackClawsFeatureLevel2.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsAcidLevel3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsAcidLevel3", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicBlackClawsFeatureLevel3.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicBlackClawsFeatureLevel4.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsAcid = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationDragonTalonsAcid", bp => {
                bp.SetName("Talons of the Dragon - Acid");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}acid damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleDragonMysteryFlagAcid, OracleRevelationDragonTalonsAcidLevel1),
                    Helpers.LevelEntry(5, OracleRevelationDragonTalonsAcidLevel2),
                    Helpers.LevelEntry(7, OracleRevelationDragonTalonsAcidLevel3)
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var BloodlineDraconicWhiteClawsFeatureLevel1 = Resources.GetBlueprint<BlueprintFeature>("6345ba12562497e45946ef4a449d3c58");
            var BloodlineDraconicWhiteClawsFeatureLevel2 = Resources.GetBlueprint<BlueprintFeature>("50433478eb919a440b2a2bac0123a5ef");
            var BloodlineDraconicWhiteClawsFeatureLevel3 = Resources.GetBlueprint<BlueprintFeature>("6e2692eda02ef6f479e7bf88b30c1c84");
            var BloodlineDraconicWhiteClawsFeatureLevel4 = Resources.GetBlueprint<BlueprintFeature>("6fdf3508d90de0149ad82feebd5f91f9");
            var OracleRevelationDragonTalonsColdLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsColdLevel1", bp => {
                bp.SetName("Talons of the Dragon - Cold");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}cold damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 5;
                    c.m_Feature = BloodlineDraconicWhiteClawsFeatureLevel1.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = BloodlineDraconicClawsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonTalonsColdLevel2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsColdLevel2", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.m_Feature = BloodlineDraconicWhiteClawsFeatureLevel2.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsColdLevel3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsColdLevel3", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicWhiteClawsFeatureLevel3.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicWhiteClawsFeatureLevel4.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsCold = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationDragonTalonsCold", bp => {
                bp.SetName("Talons of the Dragon - Cold");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}cold damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleDragonMysteryFlagCold, OracleRevelationDragonTalonsColdLevel1),
                    Helpers.LevelEntry(5, OracleRevelationDragonTalonsColdLevel2),
                    Helpers.LevelEntry(7, OracleRevelationDragonTalonsColdLevel3)
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var BloodlineDraconicBlueClawsFeatureLevel1 = Resources.GetBlueprint<BlueprintFeature>("9b5ee3a442801524bba4ddbf6aa92b8b");
            var BloodlineDraconicBlueClawsFeatureLevel2 = Resources.GetBlueprint<BlueprintFeature>("ee0b3610a8c9d5845b9728b4b42c75e9");
            var BloodlineDraconicBlueClawsFeatureLevel3 = Resources.GetBlueprint<BlueprintFeature>("8d612c8deea04674db808e953852b693");
            var BloodlineDraconicBlueClawsFeatureLevel4 = Resources.GetBlueprint<BlueprintFeature>("008f202bdeeca9d4fac96b49fcb10021");
            var OracleRevelationDragonTalonsElectricLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsElectricLevel1", bp => {
                bp.SetName("Talons of the Dragon - Electric");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}electrical damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 5;
                    c.m_Feature = BloodlineDraconicBlueClawsFeatureLevel1.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = BloodlineDraconicClawsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonTalonsElectricLevel2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsElectricLevel2", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.m_Feature = BloodlineDraconicBlueClawsFeatureLevel2.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsElectricLevel3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsElectricLevel3", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicBlueClawsFeatureLevel3.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicBlueClawsFeatureLevel4.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsElectric = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationDragonTalonsElectric", bp => {
                bp.SetName("Talons of the Dragon - Electric");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}electrical damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleDragonMysteryFlagElectric, OracleRevelationDragonTalonsElectricLevel1),
                    Helpers.LevelEntry(5, OracleRevelationDragonTalonsElectricLevel2),
                    Helpers.LevelEntry(7, OracleRevelationDragonTalonsElectricLevel3)
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var BloodlineDraconicRedClawsFeatureLevel1 = Resources.GetBlueprint<BlueprintFeature>("d35ebe618dbbb9a4db90c90b74327a80");
            var BloodlineDraconicRedClawsFeatureLevel2 = Resources.GetBlueprint<BlueprintFeature>("70b45ae7236040947a43dbdf8c01d3ee");
            var BloodlineDraconicRedClawsFeatureLevel3 = Resources.GetBlueprint<BlueprintFeature>("0e17d11f7c2fac04e9aacae441907417");
            var BloodlineDraconicRedClawsFeatureLevel4 = Resources.GetBlueprint<BlueprintFeature>("d908b429d0bae9043a250c83266bc890");
            var OracleRevelationDragonTalonsFireLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsFireLevel1", bp => {
                bp.SetName("Talons of the Dragon - Fire");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}fire damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 5;
                    c.m_Feature = BloodlineDraconicRedClawsFeatureLevel1.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = BloodlineDraconicClawsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonTalonsFireLevel2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsFireLevel2", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.m_Feature = BloodlineDraconicRedClawsFeatureLevel2.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsFireLevel3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonTalonsFireLevel3", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicRedClawsFeatureLevel3.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = BloodlineDraconicRedClawsFeatureLevel4.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });
            var OracleRevelationDragonTalonsFire = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationDragonTalonsFire", bp => {
                bp.SetName("Talons of the Dragon - Fire");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of {g|Encyclopedia:Energy_Damage}fire damage{/g} on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleDragonMysteryFlagFire, OracleRevelationDragonTalonsFireLevel1),
                    Helpers.LevelEntry(5, OracleRevelationDragonTalonsFireLevel2),
                    Helpers.LevelEntry(7, OracleRevelationDragonTalonsFireLevel3)
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var OracleRevelationDragonTalonsSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationDragonTalonsSelection", bp => {
                bp.SetName("Talons of the Dragon");
                bp.SetDescription("You fight with the fearsome talons of dragonkind. You can grow claws as a {g|Encyclopedia:Free_Action}free action{/g}. These claws are treated as " +
                    "{g|Encyclopedia:NaturalAttack}natural weapons{/g}, allowing you to make two claw {g|Encyclopedia:Attack}attacks{/g} as a full attack using your full " +
                    "{g|Encyclopedia:BAB}base attack bonus{/g}. Each of these attacks deals {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Damage}damage{/g} plus your " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier (1d3 if you are Small). At 5th level, these claws are considered magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g}. At 7th level, the damage increases by one step to 1d6 points of damage (1d4 if you are Small). At 11th level, these claws " +
                    "deal an additional 1d6 points of your chosen energy type on a successful hit. You can use your claws for a number of {g|Encyclopedia:Combat_Round}rounds{/g} " +
                    "per day equal to 3 + your {g|Encyclopedia:Charisma}Charisma{/g} modifier. These rounds do not need to be consecutive.");
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    OracleRevelationDragonTalonsAcid.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonTalonsCold.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonTalonsElectric.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonTalonsFire.ToReference<BlueprintFeatureReference>()
                };
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
            OracleRevelationDragonTalonsSelection.AddComponent<PrerequisiteNoFeature>(c => { c.m_Feature = OracleRevelationDragonTalonsSelection.ToReference<BlueprintFeatureReference>(); });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDragonTalonsSelection.ToReference<BlueprintFeatureReference>());
            //Draconic Resistances
            var OracleRevelationDraconicResistancesAcid = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDraconicResistancesAcid", bp => {
                bp.SetName("Draconic Resistances - Acid");
                bp.SetDescription("Like the great dragons, you are not easily harmed by common means of attack. You gain acid resistance 5 and a +1 natural armor bonus. " +
                    "At 9th level, your acid resistance increases to 10 and your natural armor bonus increases to +2. At 15th level, your acid resistance increases to 20 and your natural armor " +
                    "bonus increases to +4.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Acid;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 5 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 10 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 20 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {                        
                        OracleDragonMysteryFlagAcid.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDraconicResistancesCold = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDraconicResistancesCold", bp => {
                bp.SetName("Draconic Resistances - Cold");
                bp.SetDescription("Like the great dragons, you are not easily harmed by common means of attack. You gain cold resistance 5 and a +1 natural armor bonus. " +
                    "At 9th level, your cold resistance increases to 10 and your natural armor bonus increases to +2. At 15th level, your cold resistance increases to 20 and your natural armor " +
                    "bonus increases to +4.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Cold;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 5 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 10 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 20 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleDragonMysteryFlagCold.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDraconicResistancesElectric = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDraconicResistancesElectric", bp => {
                bp.SetName("Draconic Resistances - Electric");
                bp.SetDescription("Like the great dragons, you are not easily harmed by common means of attack. You gain electric resistance 5 and a +1 natural armor bonus. " +
                    "At 9th level, your electric resistance increases to 10 and your natural armor bonus increases to +2. At 15th level, your electric resistance increases to 20 and your natural armor " +
                    "bonus increases to +4.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Electricity;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 5 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 10 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 20 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleDragonMysteryFlagElectric.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDraconicResistancesFire = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDraconicResistancesFire", bp => {
                bp.SetName("Draconic Resistances - Fire");
                bp.SetDescription("Like the great dragons, you are not easily harmed by common means of attack. You gain fire resistance 5 and a +1 natural armor bonus. " +
                    "At 9th level, your fire resistance increases to 10 and your natural armor bonus increases to +2. At 15th level, your fire resistance increases to 20 and your natural armor " +
                    "bonus increases to +4.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Fire;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 5 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 10 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 20 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleDragonMysteryFlagFire.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDraconicResistancesSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationDraconicResistancesSelection", bp => {
                bp.SetName("Draconic Resistances");
                bp.SetDescription("Like the great dragons, you are not easily harmed by common means of attack. You gain resistance 5 against your chosen energy type and a +1 natural armor bonus. " +
                    "At 9th level, your energy resistance increases to 10 and your natural armor bonus increases to +2. At 15th level, your energy resistance increases to 20 and your natural armor " +
                    "bonus increases to +4.");
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    OracleRevelationDraconicResistancesAcid.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDraconicResistancesFire.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDraconicResistancesCold.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDraconicResistancesElectric.ToReference<BlueprintFeatureReference>()
                };
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
            OracleRevelationDraconicResistancesSelection.AddComponent<PrerequisiteNoFeature>(c => { c.m_Feature = OracleRevelationDraconicResistancesSelection.ToReference<BlueprintFeatureReference>(); });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDraconicResistancesSelection.ToReference<BlueprintFeatureReference>());
            //Breath Weapon
            var BloodlineDraconicRedBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("3f31704e595e78942b3640cdc9b95d8b");
            var BloodlineDraconicWhiteBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("84be529914c90664aa948d8266bb3fa6");
            var BloodlineDraconicBlueBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("60a3047f434f38544a2878c26955d3ad");
            var BloodlineDraconicBlackBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("1e65b0b2db777e24db96d8bc52cc9207");
            var FireCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("acf144d4da2638e4eadde1bb9dac29b4");
            var ColdCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c202b61bf074a7442bf335b27721853f");
            var SonicCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c7fd792125b79904881530dbc2ff83de");
            var AcidCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("155104dfdc285f3449610e625fa85729");
            var FireLine00Breath = Resources.GetBlueprint<BlueprintProjectile>("fb88746261028a1468e60b9bbfe00a35");
            var AcidLine00Breath = Resources.GetBlueprint<BlueprintProjectile>("216f05939a74d634d8ec7d88f836c5c5");
            var ColdLine00Breath = Resources.GetBlueprint<BlueprintProjectile>("fe327abf15980eb458ced542260794e2");
            var LightningBolt00 = Resources.GetBlueprint<BlueprintProjectile>("c7734162c01abdc478418bfb286ed7a5");
            var OracleRevelationDragonBreathWeaponResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationDragonBreathWeaponResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    },                    
                    StartingLevel = 5,
                    LevelStep = 5,
                    StartingIncrease = 1,
                    PerStepIncrease = 1
                };                
            });
            var OracleRevelationDragonBreathCooldown = Helpers.CreateBuff("OracleRevelationDragonBreathCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Breath Weapon - Ability is not ready yet");
                bp.SetDescription("");
                //bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var OracleRevelationDragonBreathWeaponFireConeAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponFireConeAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Fire Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                                Energy = DamageEnergyType.Fire
                            },
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathWeaponFireLineAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponFireLineAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Fire Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireLine00Breath.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                                Energy = DamageEnergyType.Fire
                            },
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathWeaponAcidConeAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponAcidConeAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Acid Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                                Energy = DamageEnergyType.Acid
                            },
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathWeaponAcidLineAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponAcidLineAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Acid Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidLine00Breath.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                                Energy = DamageEnergyType.Acid
                            },
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathWeaponColdConeAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponColdConeAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Ice Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathWeaponColdLineAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponColdLineAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Ice Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdLine00Breath.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathWeaponElectricConeAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponElectricConeAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Electric Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        SonicCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                                Energy = DamageEnergyType.Electricity
                            },
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathWeaponElectricLineAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDragonBreathWeaponElectricLineAbility", bp => {
                bp.SetName("Oracle Dragon Breath - Electric Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        LightningBolt00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
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
                                Energy = DamageEnergyType.Electricity
                            },
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
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDragonBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
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
                                        m_IsExtendable = true,
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDragonBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { OracleDragonFinalRevelation.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDragonBreathFireConeFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathFireConeFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Fire Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        OracleRevelationDragonBreathWeaponFireConeAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagFire.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponFireConeAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathFireLineFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathFireLineFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Fire Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDragonBreathWeaponFireLineAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagFire.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponFireLineAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathAcidConeFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathAcidConeFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Acid Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDragonBreathWeaponAcidConeAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagAcid.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponAcidConeAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathAcidLineFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathAcidLineFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Acid Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDragonBreathWeaponAcidLineAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagAcid.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponAcidLineAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathColdConeFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathColdConeFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Ice Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDragonBreathWeaponColdConeAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagCold.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponColdConeAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathColdLineFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathColdLineFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Ice Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDragonBreathWeaponColdLineAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagCold.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponColdLineAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagElectric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathElectricConeFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathElectricConeFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Electric Cone");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 30-foot cone. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDragonBreathWeaponElectricConeAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagElectric.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponElectricConeAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathElectricLineFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDragonBreathElectricLineFeature", bp => {
                bp.SetName("Oracle Dragon Breath - Electric Line");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} per 2 oracle levels you have (minimum " +
                    "{g|Encyclopedia:Dice}1d6{/g})in a 60-foot line. Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half " +
                    "{g|Encyclopedia:Damage}damage{/g}. The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your oracle level + your {g|Encyclopedia:Charisma}Charisma{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDragonBreathWeaponElectricLineAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleDragonMysteryFlagElectric.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDragonBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationDragonBreathWeaponElectricLineAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagAcid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = OracleDragonMysteryFlagFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationDragonBreathWeaponSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationDragonBreathWeaponSelection", bp => {
                bp.SetName("Dragon Breath Weapon");
                bp.SetDescription("The primal power of dragonkind seethes within you. You gain a breath weapon. This breath weapon deals 1d6 points of damage of your energy type per 2 oracle " +
                    "levels you have (minimum 1d6; Reflex half ). The shape of the breath weapon is either a 30-foot cone or a 60- foot line, selected when choosing this revelation. You can use " +
                    "this ability once per day at 1st level, plus one additional time at 5th level and one additional time per day for every 5 levels beyond 5th.");                
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    OracleRevelationDragonBreathFireLineFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonBreathAcidLineFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonBreathColdLineFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonBreathElectricLineFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonBreathFireConeFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonBreathAcidConeFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonBreathColdConeFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationDragonBreathElectricConeFeature.ToReference<BlueprintFeatureReference>()
                };
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
            OracleRevelationDragonBreathWeaponSelection.AddComponent<PrerequisiteNoFeature>(c => { c.m_Feature = OracleRevelationDragonBreathWeaponSelection.ToReference<BlueprintFeatureReference>(); });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDragonBreathWeaponSelection.ToReference<BlueprintFeatureReference>());
            //Form of the Dragon
            var OracleRevelationFormOfTheDragonResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationFormOfTheDragonResource", bp => {
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
                    ResourceBonusStat = StatType.Unknown
                };
            });
            var FormOfTheDragonGreenBuff = Resources.GetBlueprint<BlueprintBuff>("02611a12f38bed340920d1d427865917");
            var FormOfTheDragonSilverBuff = Resources.GetBlueprint<BlueprintBuff>("feb2ab7613e563e45bcf9f7ffe4e05c6");
            var FormOfTheDragonBlackBuff = Resources.GetBlueprint<BlueprintBuff>("268fafac0a5b78c42a58bd9c1ae78bcf");
            var FormOfTheDragonBlueBuff = Resources.GetBlueprint<BlueprintBuff>("b117bc8b41735924dba3fb23318f39ff");
            var FormOfTheDragonBrassBuff = Resources.GetBlueprint<BlueprintBuff>("17d330af03f5b3042a4417ab1d45e484");
            var FormOfTheDragonRedBuff = Resources.GetBlueprint<BlueprintBuff>("294cbb3e1d547f341a5d7ec8500ffa44");
            var FormOfTheDragonWhiteBuff = Resources.GetBlueprint<BlueprintBuff>("a6acd3ad1e9fa6c45998d43fd5dcd86d");
            var FormOfTheDragonI = Resources.GetBlueprint<BlueprintAbility>("f767399367df54645ac620ef7b2062bb");
            var FormOfTheDragonGreenBuff2 = Resources.GetBlueprint<BlueprintBuff>("070543328d3e9af49bb514641c56911d");
            var FormOfTheDragonSilverBuff2 = Resources.GetBlueprint<BlueprintBuff>("16857109dafc2b94eafd1e888552ef76");
            var FormOfTheDragonBlackBuff2 = Resources.GetBlueprint<BlueprintBuff>("9eb5ba8c396d2c74c8bfabd3f5e91050");
            var FormOfTheDragonBlueBuff2 = Resources.GetBlueprint<BlueprintBuff>("cf8b4e861226e0545a6805036ab2a21b");
            var FormOfTheDragonBrassBuff2 = Resources.GetBlueprint<BlueprintBuff>("f7fdc15aa0219104a8b38c9891cac17b");
            var FormOfTheDragonRedBuff2 = Resources.GetBlueprint<BlueprintBuff>("40a96969339f3c241b4d989910f255e1");
            var FormOfTheDragonWhiteBuff2 = Resources.GetBlueprint<BlueprintBuff>("2652c61dff50a24479520c84005ede8b");
            var FormOfTheDragonII = Resources.GetBlueprint<BlueprintAbility>("666556ded3a32f34885e8c318c3a0ced");
            var FormOfTheDragonGreenBuff3 = Resources.GetBlueprint<BlueprintBuff>("2d294863adf81f944a7558f7ae248448");
            var FormOfTheDragonSilverBuff3 = Resources.GetBlueprint<BlueprintBuff>("80babfb32011f384ea865d768857da79");
            var FormOfTheDragonBlackBuff3 = Resources.GetBlueprint<BlueprintBuff>("c231e0cf7c203644d81e665d6115ae69");
            var FormOfTheDragonBlueBuff3 = Resources.GetBlueprint<BlueprintBuff>("a4993affb4c4ad6429eca6daeb7b86a8");
            var FormOfTheDragonBrassBuff3 = Resources.GetBlueprint<BlueprintBuff>("8acd6ac6f89c73b4180191eb63768009");
            var FormOfTheDragonRedBuff3 = Resources.GetBlueprint<BlueprintBuff>("782d09044e895fa44b9b6d9cca3a52b5");
            var FormOfTheDragonWhiteBuff3 = Resources.GetBlueprint<BlueprintBuff>("8dae421e48035a044a4b1a7b9208c5db");
            var FormOfTheDragonIII = Resources.GetBlueprint<BlueprintAbility>("1cdc4ad4c208246419b98a35539eafa6");
            var OracleRevelationFormOfTheDragon1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragon1", bp => {
                bp.SetName("Oracles Form of the Dragon");
                bp.SetDescription("You become a medium dragon-like creature. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to " +
                    "{g|Encyclopedia:Strength}Strength{/g}, a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to " +
                    "{g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, a breath weapon, and resistance to one element. Your movement " +
                    "{g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). Your breath weapon and resistance depend on the type of your dragon prototype. You can only " +
                    "use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} " +
                    "and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonI.m_Icon;

                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonGreen1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonGreen1", bp => {
                bp.SetName("Form of the Dragon - Green");
                bp.SetDescription("You become a medium green dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonGreenBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonGreenBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;                    
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonGreenBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonSilver1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonSilver1", bp => {
                bp.SetName("Form of the Dragon - Silver");
                bp.SetDescription("You become a medium silver dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a ice breath weapon, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonSilverBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonSilverBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonSilverBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBlack1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBlack1", bp => {
                bp.SetName("Form of the Dragon - Black");
                bp.SetDescription("You become a medium black dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonBlackBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBlackBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlackBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBlue1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBlue1", bp => {
                bp.SetName("Form of the Dragon - Blue");
                bp.SetDescription("You become a medium blue dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a electric breath weapon, and resistance to electricity 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonBlueBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBlueBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlueBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBrass1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBrass1", bp => {
                bp.SetName("Form of the Dragon - Brass");
                bp.SetDescription("You become a medium brass dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonBrassBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBrassBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBrassBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonRed1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonRed1", bp => {
                bp.SetName("Form of the Dragon - Red");
                bp.SetDescription("You become a medium red dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonRedBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonRedBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonRedBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonWhite1 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonWhite1", bp => {
                bp.SetName("Form of the Dragon - White");
                bp.SetDescription("You become a medium white dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a ice breath weapon, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonWhiteBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonWhiteBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonWhiteBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleRevelationFormOfTheDragon1.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleRevelationFormOfTheDragonGreen1.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonSilver1.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBlack1.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBlue1.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBrass1.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonRed1.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonWhite1.ToReference<BlueprintAbilityReference>()
                };
            });
            var OracleRevelationFormOfTheDragon1Feature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationFormOfTheDragon1Feature", bp => {
                bp.SetName("Oracles Form of the Dragon");
                bp.SetDescription("You become a medium dragon-like creature. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to " +
                    "{g|Encyclopedia:Strength}Strength{/g}, a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to " +
                    "{g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, a breath weapon, and resistance to one element. Your movement " +
                    "{g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). Your breath weapon and resistance depend on the type of your dragon prototype. You can only " +
                    "use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} " +
                    "and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonI.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationFormOfTheDragon1.ToReference<BlueprintUnitFactReference>()
                    };
                });                
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });            
            var OracleRevelationFormOfTheDragon2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragon2", bp => {
                bp.SetName("Oracles Form of the Dragon II");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions as dragonkind I except that it also allows you to assume the form of a large " +
                    "dragon-like creature. You gain the following abilities: a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to " +
                    "{g|Encyclopedia:Strength}Strength{/g}, a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to " +
                    "{g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, a breath weapon, {g|Encyclopedia:Damage_Reduction}damage reduction{/g} " +
                    "5/magic, and resistance to one element. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite " +
                    "({g|Encyclopedia:Dice}2d6{/g}), two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap attack (1d8). You can " +
                    "only use the breath weapon twice per casting of this spell (when you use it, the duration of your breath weapon ability is reduced by 1/2 " +
                    "{g|Encyclopedia:Caster_Level}caster level{/g} minutes), and you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All " +
                    "breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage. " +
                    "Line breath weapons increase to 80-foot lines and cones increase to 40-foot cones.");
                bp.m_Icon = FormOfTheDragonII.m_Icon;

                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonGreen2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonGreen2", bp => {
                bp.SetName("Form of the Dragon - Green");
                bp.SetDescription("You become a large green dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, damage reduction 5/magic, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonGreenBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonGreenBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonGreenBuff2.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonSilver2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonSilver2", bp => {
                bp.SetName("Form of the Dragon - Silver");
                bp.SetDescription("You become a large silver dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a ice breath weapon, damage reduction 5/magic, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonSilverBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonSilverBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonSilverBuff2.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBlack2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBlack2", bp => {
                bp.SetName("Form of the Dragon - Black");
                bp.SetDescription("You become a large black dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, damage reduction 5/magic, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonBlackBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBlackBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlackBuff2.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBlue2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBlue2", bp => {
                bp.SetName("Form of the Dragon - Blue");
                bp.SetDescription("You become a large blue dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a electricity breath weapon, damage reduction 5/magic, and resistance to electricity 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonBlueBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBlueBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlueBuff2.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBrass2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBrass2", bp => {
                bp.SetName("Form of the Dragon - Brass");
                bp.SetDescription("You become a large brass dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, damage reduction 5/magic, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonBrassBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBrassBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBrassBuff2.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonRed2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonRed2", bp => {
                bp.SetName("Form of the Dragon - Red");
                bp.SetDescription("You become a large red dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, damage reduction 5/magic, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonRedBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonRedBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonRedBuff2.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonWhite2 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonWhite2", bp => {
                bp.SetName("Form of the Dragon - White");
                bp.SetDescription("You become a large white dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a ice breath weapon, damage reduction 5/magic, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage.");
                bp.m_Icon = FormOfTheDragonWhiteBuff2.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonWhiteBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonWhiteBuff2.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleRevelationFormOfTheDragon2.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleRevelationFormOfTheDragonGreen2.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonSilver2.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBlack2.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBlue2.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBrass2.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonRed2.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonWhite2.ToReference<BlueprintAbilityReference>()
                };
            });
            var OracleRevelationFormOfTheDragon2Feature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationFormOfTheDragon2Feature", bp => {
                bp.SetName("Oracles Form of the Dragon II");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions as dragonkind I except that it also allows you to assume the form of a large " +
                    "dragon-like creature. You gain the following abilities: a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to " +
                    "{g|Encyclopedia:Strength}Strength{/g}, a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to " +
                    "{g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, a breath weapon, {g|Encyclopedia:Damage_Reduction}damage reduction{/g} " +
                    "5/magic, and resistance to one element. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite " +
                    "({g|Encyclopedia:Dice}2d6{/g}), two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap attack (1d8). You can " +
                    "only use the breath weapon twice per casting of this spell (when you use it, the duration of your breath weapon ability is reduced by 1/2 " +
                    "{g|Encyclopedia:Caster_Level}caster level{/g} minutes), and you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All " +
                    "breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage. " +
                    "Line breath weapons increase to 80-foot lines and cones increase to 40-foot cones.");
                bp.m_Icon = FormOfTheDragonII.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationFormOfTheDragon2.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });            
            var OracleRevelationFormOfTheDragon3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragon3", bp => {
                bp.SetName("Oracles Form of the Dragon III");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions as dragonkind II {g|Encyclopedia:Saving_Throw}save{/g} that it also allows you to " +
                    "take the form of a huge dragon-like creature. You gain the following abilities: a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} " +
                    "to {g|Encyclopedia:Strength}Strength{/g}, a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to " +
                    "{g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, blindsense with a range of 60 feet, a breath weapon, " +
                    "{g|Encyclopedia:Damage_Reduction}damage reduction{/g} 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), and " +
                    "immunity to one element. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), " +
                    "two claws (2d6), two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap attack (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} " +
                    "and allow a Reflex save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonIII.m_Icon;

                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonGreen3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonGreen3", bp => {
                bp.SetName("Form of the Dragon - Green");
                bp.SetDescription("You become a huge green dragon. You gain a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "blindsense with a range of 60 feet, a acid breath weapon, damage reduction 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), " +
                    "and immunity to acid. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), " +
                    "two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a Reflex " +
                    "save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonGreenBuff3.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonGreenBuff3.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonGreenBuff3.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonSilver3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonSilver3", bp => {
                bp.SetName("Form of the Dragon - Silver");
                bp.SetDescription("You become a huge silver dragon. You gain a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "blindsense with a range of 60 feet, a ice breath weapon, damage reduction 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), " +
                    "and immunity to cold. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), " +
                    "two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a Reflex " +
                    "save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonSilverBuff3.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonSilverBuff3.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonSilverBuff3.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBlack3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBlack3", bp => {
                bp.SetName("Form of the Dragon - Black");
                bp.SetDescription("You become a huge black dragon. You gain a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "blindsense with a range of 60 feet, a acid breath weapon, damage reduction 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), " +
                    "and immunity to acid. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), " +
                    "two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a Reflex " +
                    "save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonBlackBuff3.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBlackBuff3.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlackBuff3.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBlue3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBlue3", bp => {
                bp.SetName("Form of the Dragon - Blue");
                bp.SetDescription("You become a huge blue dragon. You gain a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "blindsense with a range of 60 feet, a electric breath weapon, damage reduction 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), " +
                    "and immunity to electric. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), " +
                    "two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a Reflex " +
                    "save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonBlueBuff3.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBlueBuff3.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlueBuff3.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonBrass3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonBrass3", bp => {
                bp.SetName("Form of the Dragon - Brass");
                bp.SetDescription("You become a huge brass dragon. You gain a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "blindsense with a range of 60 feet, a fire breath weapon, damage reduction 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), " +
                    "and immunity to fire. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), " +
                    "two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a Reflex " +
                    "save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonBrassBuff3.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonBrassBuff3.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBrassBuff3.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonRed3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonRed3", bp => {
                bp.SetName("Form of the Dragon - Red");
                bp.SetDescription("You become a huge red dragon. You gain a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "blindsense with a range of 60 feet, a fire breath weapon, damage reduction 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), " +
                    "and immunity to fire. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), " +
                    "two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a Reflex " +
                    "save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonRedBuff3.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonRedBuff3.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonRedBuff3.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationFormOfTheDragonWhite3 = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationFormOfTheDragonWhite3", bp => {
                bp.SetName("Form of the Dragon - White");
                bp.SetDescription("You become a huge white dragon. You gain a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "blindsense with a range of 60 feet, a ice breath weapon, damage reduction 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), " +
                    "and immunity to cold. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), " +
                    "two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a Reflex " +
                    "save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonWhiteBuff3.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FormOfTheDragonWhiteBuff3.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
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
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonWhiteBuff3.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.m_Parent = OracleRevelationFormOfTheDragon1.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleRevelationFormOfTheDragon3.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleRevelationFormOfTheDragonGreen3.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonSilver3.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBlack3.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBlue3.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonBrass3.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonRed3.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationFormOfTheDragonWhite3.ToReference<BlueprintAbilityReference>()
                };
            });
            var OracleRevelationFormOfTheDragon3Feature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationFormOfTheDragon3Feature", bp => {
                bp.SetName("Oracles Form of the Dragon III");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} functions as dragonkind II {g|Encyclopedia:Saving_Throw}save{/g} that it also allows you to " +
                    "take the form of a huge dragon-like creature. You gain the following abilities: a +10 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} " +
                    "to {g|Encyclopedia:Strength}Strength{/g}, a +8 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 form natural armor bonus to " +
                    "{g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, blindsense with a range of 60 feet, a breath weapon, " +
                    "{g|Encyclopedia:Damage_Reduction}damage reduction{/g} 10/magic, frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), and " +
                    "immunity to one element. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d8{/g}), " +
                    "two claws (2d6), two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap attack (2d6). You can use the breath weapon as often as you like, " +
                    "but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 12d8 points of {g|Encyclopedia:Damage}damage{/g} " +
                    "and allow a Reflex save for half damage. Line breath weapons increase to 100-foot lines and cones increase to 50-foot cones.");
                bp.m_Icon = FormOfTheDragonIII.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationFormOfTheDragon3.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });            
            var OracleRevelationFormOfTheDragonFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationFormOfTheDragonFeature", bp => {
                bp.SetName("Form of the Dragon");
                bp.SetDescription("Your kinship with dragonkind allows you to take on the form of a dragon. As a standard action, you can assume the form " +
                    "of a Medium dragon, as per form of the dragon I. At 15th level, you can assume the form of a Large dragon, as per form of the dragon II. " +
                    "At 19th level, you can assume the form of a Huge dragon, as per form of the dragon III. You can use this ability once per day, but the " +
                    "duration is 10 minutes per oracle level. You must be at least 11th level to select this revelation.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 11;
                    c.m_Feature = OracleRevelationFormOfTheDragon1Feature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = OracleRevelationFormOfTheDragon2Feature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 19;
                    c.m_Feature = OracleRevelationFormOfTheDragon3Feature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationFormOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
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
                    c.Level = 11;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationFormOfTheDragonFeature.ToReference<BlueprintFeatureReference>());
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
                bp.m_Min = 7;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>() },
                    IncreasedByLevel = true,
                    StartingLevel = 7,
                    LevelIncrease = 1,
                    StartingIncrease = 1,
                    LevelStep = 1,
                    PerStepIncrease = 1,
                };
            });
            var OracleRevelationWingsOfTheDragonExtraUse99 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonExtraUse99", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 99;
                });
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonExtraUse9 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonExtraUse9", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 9;
                });
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.Ranks = 3;
            });
            var OracleRevelationWingsOfTheDragonUnlimited = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonUnlimited", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationWingsOfTheDragonProgression", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(11, OracleRevelationWingsOfTheDragonExtraUse99),
                    Helpers.LevelEntry(12, OracleRevelationWingsOfTheDragonExtraUse9),
                    Helpers.LevelEntry(13, OracleRevelationWingsOfTheDragonExtraUse9),
                    Helpers.LevelEntry(14, OracleRevelationWingsOfTheDragonExtraUse9),
                    Helpers.LevelEntry(15, OracleRevelationWingsOfTheDragonUnlimited)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var OracleRevelationWingsOfTheDragonRedAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonRedAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of red wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicRed.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonBlueAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonBlueAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of blue wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicBlue.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonGoldAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonGoldAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of gold wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicGold.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonGreenAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonGreenAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of green wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicGreen.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonBrassAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonBrassAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of brass wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicBrass.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonWhiteAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonWhiteAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of white wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicWhite.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonBlackAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonBlackAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of black wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicBlack.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonCopperAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonCopperAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of copper wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicCopper.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonBronzeAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonBronzeAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of bronze wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicBronze.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonSilverAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWingsOfTheDragonSilverAbility", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of silver wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = BuffWingsMutagen.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.m_Buff = BuffWingsDraconicSilver.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_FreeBlueprint = OracleRevelationWingsOfTheDragonUnlimited.ToReference<BlueprintUnitFactReference>();
                });
                bp.Group = ActivatableAbilityGroup.Wings;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            });
            var OracleRevelationWingsOfTheDragonRedFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonRedFeature", bp => {
                bp.SetName("Wings of the Dragon - Red");
                bp.SetDescription("As a swift action, you can gain a pair of red wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonRedAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonBlueFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonBlueFeature", bp => {
                bp.SetName("Wings of the Dragon - Blue");
                bp.SetDescription("As a swift action, you can gain a pair of blue wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonBlueAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonGoldFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonGoldFeature", bp => {
                bp.SetName("Wings of the Dragon - Gold");
                bp.SetDescription("As a swift action, you can gain a pair of gold wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonGoldAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonGreenFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonGreenFeature", bp => {
                bp.SetName("Wings of the Dragon - Green");
                bp.SetDescription("As a swift action, you can gain a pair of green wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonGreenAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonBrassFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonBrassFeature", bp => {
                bp.SetName("Wings of the Dragon - Brass");
                bp.SetDescription("As a swift action, you can gain a pair of brass wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonBrassAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonWhiteFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonWhiteFeature", bp => {
                bp.SetName("Wings of the Dragon - White");
                bp.SetDescription("As a swift action, you can gain a pair of white wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonWhiteAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonBlackFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonBlackFeature", bp => {
                bp.SetName("Wings of the Dragon - Black");
                bp.SetDescription("As a swift action, you can gain a pair of black wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonBlackAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonCopperFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonCopperFeature", bp => {
                bp.SetName("Wings of the Dragon - Copper");
                bp.SetDescription("As a swift action, you can gain a pair of copper wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonCopperAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonBronzeFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonBronzeFeature", bp => {
                bp.SetName("Wings of the Dragon - Bronze");
                bp.SetDescription("As a swift action, you can gain a pair of bronze wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonBronzeAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonSilverFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWingsOfTheDragonSilverFeature", bp => {
                bp.SetName("Wings of the Dragon - Silver");
                bp.SetDescription("As a swift action, you can gain a pair of silver wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWingsOfTheDragonResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWingsOfTheDragonSilverAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var OracleRevelationWingsOfTheDragonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationWingsOfTheDragonSelection", bp => {
                bp.SetName("Wings of the Dragon");
                bp.SetDescription("As a swift action, you can gain a pair of wings that grant a +3 dodge {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g} " +
                    "against {g|Encyclopedia:MeleeAttack}melee attacks{/g} and an immunity to ground based effects, such as difficult terrain. You can use these wings for 1 minute " +
                    "per day for each oracle level you have. This duration does not need to be consecutive, but it must be spent in 1-minute increments. At 11th level you can " +
                    "use these wings for 10 minutes per day for each oracle level you have. At 15th level, you can use the wings indefinitely. You must be at least 7th level to " +
                    "select this revelation.\nColour of the wings is selected when choosing this revelation.");
                bp.m_Icon = BuffWingsDraconicRed.m_Icon;
                bp.m_Features = new BlueprintFeatureReference[] {
                    OracleRevelationWingsOfTheDragonRedFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBlueFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonGoldFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonGreenFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBrassFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonWhiteFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBlackFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonCopperFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBronzeFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonSilverFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    OracleRevelationWingsOfTheDragonRedFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBlueFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonGoldFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonGreenFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBrassFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonWhiteFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBlackFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonCopperFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonBronzeFeature.ToReference<BlueprintFeatureReference>(),
                    OracleRevelationWingsOfTheDragonSilverFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = OracleRevelationWingsOfTheDragonProgression.ToReference<BlueprintFeatureReference>();
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
            OracleRevelationWingsOfTheDragonSelection.AddComponent<PrerequisiteNoFeature>(c => { c.m_Feature = OracleRevelationWingsOfTheDragonSelection.ToReference<BlueprintFeatureReference>(); });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationWingsOfTheDragonSelection.ToReference<BlueprintFeatureReference>());
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
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDragonSenses.ToReference<BlueprintFeatureReference>());
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
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
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
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationScaledToughness.ToReference<BlueprintFeatureReference>());
            MysteryTools.RegisterMystery(OracleDragonMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleDragonMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherDragonMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherDragonMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistDragonMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistDragonMysteryFeature);
        }
    }
}
