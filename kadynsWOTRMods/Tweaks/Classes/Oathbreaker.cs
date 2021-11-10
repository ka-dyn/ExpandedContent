using BlueprintCore.Blueprints;
using kadynsWOTRMods.Config;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items.Equipment;
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

namespace kadynsWOTRMods.Tweaks.Classes
{
    internal class Oathbreaker
    {

        private static readonly BlueprintFeature OathbreakersBaneFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneFeature");

        private static readonly BlueprintFeature OathbreakerSoloTacticsFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");
        private static readonly BlueprintFeature OathbreakersDirection = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirection");


        private static readonly BlueprintFeature PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");

        private static readonly BlueprintProgression OathbreakerProgression = Resources.GetModBlueprint<BlueprintProgression>("OathbreakerProgression");
        
        public static void AddOathbreakerClass()
        {
            
            var BOOIcon = AssetLoader.LoadInternal("Skills", "Icon_BOO.png");
            var OathbreakerSoloTactics = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");
            var OathbreakersDirection = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirection");
            var OathbreakersBaneFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneFeature");
            var OathbreakerProgression = Resources.GetModBlueprint<BlueprintProgression>("OathbreakerProgression");
            var BABFull = Resources.GetBlueprint<BlueprintStatProgression>("b3057560ffff3514299e8b93e7648a9d");
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");
            
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var OathbreakerClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("OathbreakerClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"OathbreakerClass.Name", "Oathbreaker");
                bp.LocalizedDescription = Helpers.CreateString($"OathbreakerClass.Description", "While paladins often " +
                    "collaborate with less righteous adventurers in order to further their causes, those who spend too much time around " +
                    "companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies and methods. Such an " +
                    "Oathbreaker, as these fallen paladins are known, strikes out for retribution and revenge, far more interested in " +
                    "tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"OathbreakerClass.Description", "Oathbreakers are fallen Paladins.");
                bp.HitDie = PaladinClass.HitDie;
                bp.m_BaseAttackBonus = BABFull.ToReference<BlueprintStatProgressionReference>();
                bp.m_FortitudeSave = PaladinClass.ToReference<BlueprintStatProgressionReference>();
                bp.m_ReflexSave = PaladinClass.ToReference<BlueprintStatProgressionReference>();
                bp.m_WillSave = PaladinClass.ToReference<BlueprintStatProgressionReference>();
                bp.m_Progression = OathbreakerProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.m_DefaultBuild = null;
                bp.m_Icon = BOOIcon;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.NotRecommendedAttributes = PaladinClass.NotRecommendedAttributes;
                bp.m_Spellbook = null;
                bp.m_StartingItems = PaladinClass.StartingItems;
                bp.m_SignatureAbilities = new BlueprintFeatureReference[3]
                {
                    OathbreakersBaneFeature.ToReference<BlueprintFeatureReference>(),
                    OathbreakerSoloTactics.ToReference<BlueprintFeatureReference>(),
                    OathbreakersDirection.ToReference<BlueprintFeatureReference>(),
                };
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.m_Archetypes = null;

                bp.SkillPoints = 2;
                bp.ClassSkills = new StatType[4] {

                StatType.SkillKnowledgeWorld, StatType.SkillLoreNature, StatType.SkillKnowledgeArcana, StatType.SkillLoreReligion
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
            });
                BlueprintRoot root = Resources.GetBlueprint<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
                root.Progression.m_CharacterClasses = root.Progression.m_CharacterClasses.AppendToArray(OathbreakerClass.ToReference<BlueprintCharacterClassReference>());
                
                

           
        }
    }
}














