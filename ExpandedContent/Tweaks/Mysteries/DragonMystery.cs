using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
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
        }
    }
}
