using HarmonyLib;
using BlueprintCore.Blueprints;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;

namespace ExpandedContent.Tweaks.Classes {

    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    public class DreadKnightClassAdder {
        private static bool Initialized;
       



        [HarmonyPriority(Priority.First)]
        public static void Postfix() {
            if (DreadKnightClassAdder.Initialized) return;
            DreadKnightClassAdder.Initialized = true;
            if (ModSettings.AddedContent.Classes.IsDisabled("Dread Knight")) { return; }

            Utilities.AlignmentTemplates.AddFiendishTemplate();
            Utilities.Cavalier.AddCavalierFeatures();
            Classes.ClassFeaturesDreadKnight.TouchOfProfaneCorruption.AddTouchOfProfaneCorruption();
            Classes.ClassFeaturesDreadKnight.AuraOfAbsolution.AddAuraOfAbsolution();
            Classes.ClassFeaturesDreadKnight.AuraOfCowardice.AddAuraOfCowardiceFeature();
            Classes.ClassFeaturesDreadKnight.AuraOfDepravity.AddAuraOfDepravityFeature();
            Classes.ClassFeaturesDreadKnight.AuraOfDespair.AddAuraOfDespairFeature();
            Classes.ClassFeaturesDreadKnight.AuraOfSin.AddAuraOfSinFeature();

            Classes.ClassFeaturesDreadKnight.SinfulAbsolution.AddSinfulAbsolution();
            Classes.ClassFeaturesDreadKnight.DKChannelNegativeEnergy.AddDKChannelNegativeEnery();
            Classes.ClassFeaturesDreadKnight.PlagueBringer.AddPlagueBringer();
            
            Classes.ClassFeaturesDreadKnight.ProfaneChampion.AddProfaneChampion();
            Classes.ClassFeaturesDreadKnight.ProfaneResilience.AddProfaneResilience();
            
            
            Classes.ClassFeaturesDreadKnight.Cruelties.AddCrueltyAbilities();
            Classes.ClassFeaturesDreadKnight.ProfaneBoon.AddProfaneBoon();


            Classes.ClassFeaturesDreadKnight.DreadKnightSpellbook.AddDreadKnightSpellbook();
            DreadKnightClassAdder.AddDreadKnightProgression();
            DreadKnightClassAdder.AddDreadKnightClass();






        }

        public static void AddDreadKnightClass() {

            var DreadKnightSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("DreadKnightSpellbook");
            var WeaponBondProgression = Resources.GetBlueprint<BlueprintProgression>("e08a817f475c8794aa56fdd904f43a57");
            var WarpriestSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("7d7d51be2948d2544b3c2e1596fd7603");
            var ProfaneBoonSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("ProfaneBoonSelection");
            var DreadKnightAnimalCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DreadKnightAnimalCompanionProgression");
            var FiendishWeaponBondProgression = Resources.GetModBlueprint<BlueprintProgression>("FiendishWeaponBondProgression");
            var DreadKnightChannelNegativeEnergyFeature = Resources.GetModBlueprint<BlueprintFeature>("DreadKnightChannelNegativeEnergyFeature");
      
            var CrueltySelection1 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection1");
            var CrueltySelection2 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection2");
            var CrueltySelection3 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection3");
            var CrueltySelection4 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection4");
            var PlagueBringer = Resources.GetModBlueprint<BlueprintFeature>("PlagueBringer");
            var AuraOfCowardiceFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfCowardiceFeature");
            var TouchOfProfaneCorruptionUse = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionUse");
            var TouchOfProfaneCorruptionFeature = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionFeature");
            var ProfaneResilience = Resources.GetModBlueprint<BlueprintFeature>("ProfaneResilience");           
            var SinfulAbsolutionUse = Resources.GetModBlueprint<BlueprintFeature>("SinfulAbsolutionUse");
            var SinfulAbsolutionFeature = Resources.GetModBlueprint<BlueprintFeature>("SinfulAbsolutionFeature");
            var DreadKnightProgression = Resources.GetModBlueprint<BlueprintProgression>("DreadKnightProgression");
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");           
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var DreadKnightClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("DreadKnightClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"DreadKnightClass.Name", "Dread Knight");
                bp.LocalizedDescription = Helpers.CreateString($"DreadKnightClass.Description", "Although it is a rare occurrence, paladins do " +
                    "sometimes stray from the path of righteousness. Most of these wayward holy warriors seek out redemption and forgiveness for their " +
                    "misdeeds, regaining their powers through piety, charity, and powerful magic. Yet there are others, the dark and disturbed few, " +
                    "who turn actively to evil, courting the dark powers they once railed against in order to take vengeance on their former brothers. " +
                    "It’s said that those who climb the farthest have the farthest to fall, and tyrants are living proof of this fact, their pride and " +
                    "hatred blinding them to the glory of their forsaken patrons. " +
                    "\nDread Knights become the antithesis of their former selves. They make pacts with fiends, take the lives of the innocent, " +
                    "and put nothing ahead of their personal power and wealth. Champions of evil, they often lead armies of evil creatures and " +
                    "work with other villains to bring ruin to the holy and tyranny to the weak. Not surprisingly, paladins stop at nothing to " +
                    "put an end to such nefarious antiheroes.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DreadKnightClass.Description", "Dread Knights are villains at their most dangerous. " +
                    "They care nothing for the lives of others and actively seek to bring death and destruction to ordered society. They rarely travel with " +
                    "those that they do not subjugate, unless as part of a ruse to bring ruin from within.");
                bp.HitDie = Kingmaker.RuleSystem.DiceType.D10;
                bp.m_BaseAttackBonus = PaladinClass.m_BaseAttackBonus;
                bp.m_FortitudeSave = PaladinClass.m_FortitudeSave;
                bp.m_PrototypeId = PaladinClass.m_PrototypeId;
                bp.m_ReflexSave = PaladinClass.m_ReflexSave;
                bp.m_WillSave = PaladinClass.m_WillSave;
                bp.m_Progression = DreadKnightProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.NotRecommendedAttributes = PaladinClass.NotRecommendedAttributes;
                bp.m_Spellbook = DreadKnightSpellbook.ToReference<BlueprintSpellbookReference>();
                bp.m_EquipmentEntities = PaladinClass.m_EquipmentEntities;
                bp.m_StartingItems = PaladinClass.StartingItems;
                bp.m_SignatureAbilities = new BlueprintFeatureReference[5]
                {
                    SinfulAbsolutionFeature.ToReference<BlueprintFeatureReference>(),
                    CrueltySelection4.ToReference<BlueprintFeatureReference>(),
                    TouchOfProfaneCorruptionFeature.ToReference<BlueprintFeatureReference>(),
                    DreadKnightChannelNegativeEnergyFeature.ToReference<BlueprintFeatureReference>(),
                    ProfaneBoonSelection.ToReference<BlueprintFeatureReference>(),
                };
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.m_DefaultBuild = null;
                bp.m_Archetypes = new BlueprintArchetypeReference[0];
                bp.SkillPoints = 3;
                bp.ClassSkills = new StatType[4] {

                StatType.SkillKnowledgeWorld, StatType.SkillMobility, StatType.SkillAthletics, StatType.SkillLoreReligion
                };
                bp.IsDivineCaster = true;
                bp.IsArcaneCaster = false;
                bp.StartingGold = 411;
                bp.PrimaryColor = 6;
                bp.SecondaryColor = 11;
                bp.MaleEquipmentEntities = PaladinClass.MaleEquipmentEntities;
                bp.FemaleEquipmentEntities = PaladinClass.FemaleEquipmentEntities;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.AddComponent<PrerequisiteNoClassLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Not = true;
                    c.HideInUI = true;
                });
                bp.AddComponent<PrerequisiteAlignment>(c => { c.Alignment = Kingmaker.UnitLogic.Alignments.AlignmentMaskType.Evil; });
            });
            Helpers.RegisterClass(DreadKnightClass);
            DreadKnightProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] { new BlueprintProgression.ClassWithLevel { m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>() } };
            FiendishWeaponBondProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] { new BlueprintProgression.ClassWithLevel { m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>() } };
            DreadKnightAnimalCompanionProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] { new BlueprintProgression.ClassWithLevel { m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>() } };
        }

        public static void AddDreadKnightProgression() {





            var WeaponBondProgression = Resources.GetBlueprint<BlueprintProgression>("e08a817f475c8794aa56fdd904f43a57");
            WeaponBondProgression.m_DisplayName = Helpers.CreateString("$WeaponBondSwitchAbility.DisplayName", "Divine/Profane Weapon Bond");
            WeaponBondProgression.m_Description = Helpers.CreateString("$WeaponBondSwitchAbility.Description", "Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
            var ProfaneWeaponIcon = AssetLoader.LoadInternal("Skills", "Icon_ProfaneWeapon.png");
            var ProfaneChampion = Resources.GetModBlueprint<BlueprintFeature>("ProfaneChampion");
            var AuraOfDepravityFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfDepravityFeature");
            var AuraOfSinFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfSinFeature");
            var AuraOfAbsolutionFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfAbsolutionFeature");
            var AuraOfDespairFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfDespairFeature");
            var DreadKnightCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DreadKnightCompanionSelection");
            var FiendishWeaponBondProgression = Resources.GetModBlueprint<BlueprintProgression>("FiendishWeaponBondProgression");
            var ChannelTouchOfProfaneCorruptionFeature = Resources.GetModBlueprint<BlueprintFeature>("ChannelTouchOfProfaneCorruptionFeature");
            var DreadKnightChannelNegativeEnergyFeature = Resources.GetModBlueprint<BlueprintFeature>("DreadKnightChannelNegativeEnergyFeature");
            var CrueltySelection1 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection1");
            var CrueltySelection2 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection2");
            var CrueltySelection3 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection3");
            var CrueltySelection4 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection4");
            var PlagueBringer = Resources.GetModBlueprint<BlueprintFeature>("PlagueBringer");
            var AuraOfCowardiceFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfCowardiceFeature");
            var TouchOfProfaneCorruptionUse = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionUse");
            var TouchOfProfaneCorruptionFeature = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionFeature");
            var ProfaneResilience = Resources.GetModBlueprint<BlueprintFeature>("ProfaneResilience");
            var SinfulAbsolutionFeature = Resources.GetModBlueprint<BlueprintFeature>("SinfulAbsolutionFeature");
            var SinfulAbsolutionUse = Resources.GetModBlueprint<BlueprintFeature>("SinfulAbsolutionUse");
            var PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
            var ProfIcon = AssetLoader.LoadInternal("Skills", "Icon_DKProf.png");
            var DreadKnightProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("DreadKnightProficiencies", bp => {
                bp.SetName("Dread Knight Proficiences");
                bp.SetDescription("Dread Knights are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with all types of armor " +
                    "(heavy, medium, and light), and with shields (except tower shields).");
                bp.m_Icon = ProfIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PaladinClassProficiencies.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            var ProfaneBoonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ProfaneBoonSelection", bp => {
                bp.SetName("Profane Boon");
                bp.SetDescription("Upon reaching 5th level, a dread knight receives a boon from their dark patrons. This boon can take one of two forms. Once the form is chosen, it cannot be changed. " +
                    "The first type of bond allows the dread knight to enhance their weapon as a standard action by calling upon the aid of a fiendish spirit for 1 " +
                    "minute per dread knight level. When called, the spirit causes the weapon to shed unholy light as a torch. At 5th level, this spirit grants the " +
                    "weapon a + 1 enhancement bonus.For every three levels beyond 5th, the weapon gains another + 1 enhancement bonus, to a maximum of + 6 at 20th level. ");
                bp.m_DescriptionShort = Helpers.CreateString("$ProfaneBoonSelection.DescriptionShort", "Upon reaching 5th level, a dread knight receives a boon from their dark patrons. This boon can take one of two forms. Once the form is chosen, it cannot be changed. " +
                    "The first type of bond allows the dread knight to enhance their weapon as a standard action by calling upon the aid of a fiendish spirit for 1 " +
                    "minute per dread knight level. When called, the spirit causes the weapon to shed unholy light as a torch. At 5th level, this spirit grants the " +
                    "weapon a + 1 enhancement bonus.For every three levels beyond 5th, the weapon gains another + 1 enhancement bonus, to a maximum of + 6 at 20th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Features = new BlueprintFeatureReference[2] {
                DreadKnightCompanionSelection.ToReference<BlueprintFeatureReference>(),
                FiendishWeaponBondProgression.ToReference<BlueprintFeatureReference>() };
                bp.m_AllFeatures = new BlueprintFeatureReference[2] {
                DreadKnightCompanionSelection.ToReference<BlueprintFeatureReference>(),
                FiendishWeaponBondProgression.ToReference<BlueprintFeatureReference>() };
            });




            var DreadKnightProgression = Helpers.CreateBlueprint<BlueprintProgression>("DreadKnightProgression");
            DreadKnightProgression.SetName("Dread Knight");
            DreadKnightProgression.SetDescription("Dread Knights are villains at their most dangerous. " +
                    "They care nothing for the lives of others and actively seek to bring death and destruction to ordered society. They rarely travel with " +
                    "those that they do not subjugate, unless as part of a ruse to bring ruin from within.");



            DreadKnightProgression.LevelEntries = new LevelEntry[20]

            {
        Helpers.LevelEntry(1, (BlueprintFeatureBase) SinfulAbsolutionFeature, (BlueprintFeatureBase) DreadKnightProficiencies),
        Helpers.LevelEntry(2, (BlueprintFeatureBase) ProfaneResilience, (BlueprintFeatureBase) TouchOfProfaneCorruptionFeature),
        Helpers.LevelEntry(3, (BlueprintFeatureBase) AuraOfCowardiceFeature, (BlueprintFeatureBase) PlagueBringer, (BlueprintFeatureBase) CrueltySelection1),
        Helpers.LevelEntry(4, (BlueprintFeatureBase) SinfulAbsolutionUse, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse, (BlueprintFeatureBase) DreadKnightChannelNegativeEnergyFeature),
        Helpers.LevelEntry(5, (BlueprintFeatureBase) ProfaneBoonSelection),
        Helpers.LevelEntry(6, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse, (BlueprintFeatureBase) CrueltySelection2),
        Helpers.LevelEntry(7, (BlueprintFeatureBase) SinfulAbsolutionUse, (BlueprintFeatureBase) ChannelTouchOfProfaneCorruptionFeature),
        Helpers.LevelEntry(8, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse, (BlueprintFeatureBase) AuraOfDespairFeature),
        Helpers.LevelEntry(9, (BlueprintFeatureBase) CrueltySelection3),
        Helpers.LevelEntry(10, (BlueprintFeatureBase) SinfulAbsolutionUse, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse),
        Helpers.LevelEntry(11, (BlueprintFeatureBase) AuraOfAbsolutionFeature),
        Helpers.LevelEntry(12, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse, (BlueprintFeatureBase) CrueltySelection4),
        Helpers.LevelEntry(13, (BlueprintFeatureBase) SinfulAbsolutionUse),
        Helpers.LevelEntry(14, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse, (BlueprintFeatureBase) AuraOfSinFeature),
        Helpers.LevelEntry(15, (BlueprintFeatureBase) CrueltySelection4),
        Helpers.LevelEntry(16, (BlueprintFeatureBase) SinfulAbsolutionUse, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse),
        Helpers.LevelEntry(17, (BlueprintFeatureBase) AuraOfDepravityFeature),
        Helpers.LevelEntry(18, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse, (BlueprintFeatureBase) CrueltySelection4),
        Helpers.LevelEntry(19, (BlueprintFeatureBase) SinfulAbsolutionUse),
        Helpers.LevelEntry(20, (BlueprintFeatureBase) TouchOfProfaneCorruptionUse, (BlueprintFeatureBase) ProfaneChampion)
            };

            DreadKnightProgression.UIGroups = new UIGroup[]
            {
                 Helpers.CreateUIGroup(SinfulAbsolutionFeature, SinfulAbsolutionUse),
                 Helpers.CreateUIGroup(DreadKnightProficiencies, TouchOfProfaneCorruptionFeature, TouchOfProfaneCorruptionUse),
                 Helpers.CreateUIGroup(ProfaneResilience, AuraOfCowardiceFeature, ProfaneBoonSelection, AuraOfDespairFeature, AuraOfSinFeature, AuraOfDepravityFeature, ProfaneChampion),
                 Helpers.CreateUIGroup(CrueltySelection1, CrueltySelection2, CrueltySelection3, CrueltySelection4),                              
                 Helpers.CreateUIGroup(PlagueBringer, DreadKnightChannelNegativeEnergyFeature, ChannelTouchOfProfaneCorruptionFeature, AuraOfSinFeature, AuraOfAbsolutionFeature)

             };
        }
    }
}