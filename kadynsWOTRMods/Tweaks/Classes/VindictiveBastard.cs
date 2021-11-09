using BlueprintCore.Blueprints;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items.Equipment;
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

namespace kadynsWOTRMods.Tweaks.Classes
{
    internal class VindictiveBastard
    {
        private static readonly BlueprintAbility VindictiveBastardVindictiveSmiteAbility = Resources.GetModBlueprint<BlueprintAbility>("VindictiveBastardVindictiveSmiteAbility");
        private static readonly BlueprintArchetype VindictiveBastardArchetype = Resources.GetModBlueprint<BlueprintArchetype>("VindictiveBastardArchetype");
        private static readonly BlueprintFeature VindictiveBastardVindictiveSmiteFeature = Resources.GetModBlueprint<BlueprintFeature>("VindictiveBastardVindictiveSmiteFeature");

        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintFeature SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
        private static readonly BlueprintAbility SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
        private static readonly BlueprintBuff SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");

        private static readonly BlueprintActivatableAbility BattleMeditationAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a7a3303c8ab81914e8ecca76aedc70ec");
        private static readonly BlueprintFeature InquisitorSoloTacticsFeature = Resources.GetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
        private static readonly BlueprintAbilityResource VindictiveBastardVindictiveSmiteResource = Resources.GetModBlueprint<BlueprintAbilityResource>("VindictiveBastardVindictiveSmiteResource");
        private static readonly BlueprintBuff VindictiveBastardVindictiveSmiteBuff = Resources.GetModBlueprint<BlueprintBuff>("VindictiveBastardVindictiveSmiteBuff");

        private static readonly BlueprintFeature PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
        private static readonly BlueprintFeature PaladinLayOnHands = Resources.GetBlueprint<BlueprintFeature>("a1adf65aad7a4f3ba9a7a18e6075a2ec");
        private static readonly BlueprintFeature PaladinDivineBond = Resources.GetBlueprint<BlueprintFeature>("bf8a4b51ff7b41c3b5aa139e0fe16b34");

        
        public static void AddVindictiveBastardClass()
        {

            var VindictiveBastardProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("VindictiveBastardProficiencies", bp =>
            {
                bp.SetName("Vindictive Bastard Proficiences");
                bp.SetDescription("Vindictive Bastards are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with all types of armor " +
                    "(heavy, medium, and light), and with shields (except tower shields).");
                bp.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[] { PaladinClassProficiencies.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });


            

            var VindictiveBastardProgression = Helpers.CreateBlueprint<BlueprintProgression>("VindictiveBastardProgression", bp =>
            {
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[1];
                { VindictiveBastardClass}
                bp.m_UIDeterminatorsGroup = new BlueprintFeatureBaseReference[3]
                {
                    VindictiveBastardProficiencies.ToReference<BlueprintFeatureBaseReference>(),
                    VindictiveBastardVindictiveSmiteFeature.ToReference<BlueprintFeatureBaseReference>(),
                    InquisitorSoloTacticsFeature.ToReference<BlueprintFeatureBaseReference>() };
            });








            var BABFull = Resources.GetBlueprint<BlueprintStatProgression>("b3057560ffff3514299e8b93e7648a9d");
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");
            var PaladinEquipment = Resources.GetBlueprint<BlueprintFeature>("98d34b949234954429bfd93e508734be");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var VindictiveBastardClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("VindictiveBastardClass", bp =>
            {
                bp.HitDie = PaladinClass.HitDie;
                bp.m_BaseAttackBonus = PaladinClass.ToReference<BlueprintStatProgressionReference>();
                bp.m_FortitudeSave = PaladinClass.ToReference<BlueprintStatProgressionReference>();
                bp.m_ReflexSave = PaladinClass.ToReference<BlueprintStatProgressionReference>();
                bp.m_WillSave = PaladinClass.ToReference<BlueprintStatProgressionReference>();
                bp.m_Progression = VindictiveBastardProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Spellbook = null;
                bp.m_StartingItems = PaladinClass.StartingItems;
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.m_Archetypes = new BlueprintArchetypeReference[0];
                bp.LocalizedName = Helpers.CreateString($"VindictiveBastardClass.Name", "Vindictive Bastard");
                bp.LocalizedDescription = Helpers.CreateString($"VindictiveBastardClass.Description", "While paladins often " +
                    "collaborate with less righteous adventurers in order to further their causes, those who spend too much time around " +
                    "companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies and methods. Such a " +
                    "vindictive bastard, as these fallen paladins are known, strikes out for retribution and revenge, far more interested in " +
                    "tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");
                bp.SkillPoints = 2;
                bp.ClassSkills = new StatType[4] {

                StatType.SkillKnowledgeWorld, StatType.SkillLoreNature, StatType.SkillKnowledgeArcana, StatType.SkillLoreReligion
            };
                bp.IsDivineCaster = false;
                bp.IsArcaneCaster = false;
                bp.StartingGold = 411;
                bp.PrimaryColor = 7;
                bp.SecondaryColor = 12;
                bp.MaleEquipmentEntities = PaladinClass.MaleEquipmentEntities;
                bp.FemaleEquipmentEntities = PaladinClass.FemaleEquipmentEntities;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.ComponentsArray = PaladinClass.ComponentsArray;
                bp.AddComponent<PrerequisiteNoClassLevel>(c =>
                {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<PrerequisiteIsPet>();


            });
        }
    }
}














