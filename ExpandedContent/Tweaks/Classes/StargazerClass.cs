using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Alignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes {
    internal class StargazerClass {
        public static void AddStargazerClass() {


            var BABMedium = Resources.GetBlueprint<BlueprintStatProgression>("4c936de4249b61e419a3fb775b9f2581");
            var SavesPrestigeHigh = Resources.GetBlueprint<BlueprintStatProgression>("1f309006cd2855e4e91a6c3707f3f700");
            var SavesPrestigeLow= Resources.GetBlueprint<BlueprintStatProgression>("dc5257e1100ad0d48b8f3b9798421c72");
            var PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");

            var StargazerProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerProgression", bp => {
                bp.SetName("Stargazer");
                bp.SetDescription("The empyreal lord Pulura, the Shimmering Maiden, is said to dance among the lights " +
                    "of the aurora. Some of her worshipers seek her guidance in the starry skies, attuning themselves to the constellations of the Cosmic Caravan. Beyond the " +
                    "stargazers’ interests in the heavens, though, they also train for the inevitable conflict with demons. Pulura’s bastion of worship in the Inner Sea region " +
                    "has traditionally been in the land of Sarkoris, which was consumed from within by the Worldwound at the advent of the Age of Lost Omens. Many of Pulura’s " +
                    "worshipers perished during that disastrous invasion from the Abyss, but those who survived remained behind and, over the past several decades, have grown in power.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var StargazerClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("StargazerClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"StargazerClass.Name", "Stargazer");
                bp.LocalizedDescription = Helpers.CreateString($"StargazerClass.Description", "The empyreal lord Pulura, the Shimmering Maiden, is said to dance among the lights " +
                    "of the aurora. Some of her worshipers seek her guidance in the starry skies, attuning themselves to the constellations of the Cosmic Caravan. Beyond the " +
                    "stargazers’ interests in the heavens, though, they also train for the inevitable conflict with demons. Pulura’s bastion of worship in the Inner Sea region " +
                    "has traditionally been in the land of Sarkoris, which was consumed from within by the Worldwound at the advent of the Age of Lost Omens. Many of Pulura’s " +
                    "worshipers perished during that disastrous invasion from the Abyss, but those who survived remained behind and, over the past several decades, have grown in power.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"StargazerClass.Description", "The empyreal lord Pulura, the Shimmering Maiden, is said to dance among the lights " +
                    "of the aurora. Some of her worshipers seek her guidance in the starry skies, attuning themselves to the constellations of the Cosmic Caravan. Beyond the " +
                    "stargazers’ interests in the heavens, though, they also train for the inevitable conflict with demons. Pulura’s bastion of worship in the Inner Sea region " +
                    "has traditionally been in the land of Sarkoris, which was consumed from within by the Worldwound at the advent of the Age of Lost Omens. Many of Pulura’s " +
                    "worshipers perished during that disastrous invasion from the Abyss, but those who survived remained behind and, over the past several decades, have grown in power.");
                bp.SkillPoints = 4;
                bp.HitDie = Kingmaker.RuleSystem.DiceType.D6;
                bp.HideIfRestricted = false;
                bp.PrestigeClass = true;
                bp.IsMythic = false;                
                bp.m_BaseAttackBonus = BABMedium.ToReference<BlueprintStatProgressionReference>();
                bp.m_FortitudeSave = SavesPrestigeLow.ToReference<BlueprintStatProgressionReference>();                
                bp.m_ReflexSave = SavesPrestigeHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_WillSave = SavesPrestigeHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_Progression = StargazerProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Spellbook = null;
                bp.ClassSkills = new StatType[] {
                    StatType.SkillMobility,
                    StatType.SkillAthletics,
                    StatType.SkillPersuasion,
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillPerception,
                    StatType.SkillLoreReligion,
                    StatType.SkillLoreNature,
                };
                bp.IsDivineCaster = false;
                bp.IsArcaneCaster = false;
                bp.StartingGold = 0;
                bp.m_StartingItems = new BlueprintItemReference[] {};
                bp.PrimaryColor = 0;
                bp.SecondaryColor = 0;
                bp.m_EquipmentEntities = new KingmakerEquipmentEntityReference[] {};
                bp.MaleEquipmentEntities = new Kingmaker.ResourceLinks.EquipmentEntityLink[] {};
                bp.FemaleEquipmentEntities = new Kingmaker.ResourceLinks.EquipmentEntityLink[] {};
                bp.m_Difficulty = 1;
                bp.RecommendedAttributes = new StatType[] {};
                bp.NotRecommendedAttributes = new StatType[] {};
                bp.m_DefaultBuild = null;
                bp.m_Archetypes = new BlueprintArchetypeReference[] { };
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = PuluraFeature.ToReference<BlueprintFeatureReference>();
                    c.Group = Prerequisite.GroupType.All;
                });
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 5;
                });
                bp.AddComponent<PrerequisiteCasterTypeSpellLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.IsArcane = false;
                    c.OnlySpontaneous = false;
                    c.RequiredSpellLevel = 3;
                });
                bp.AddComponent<PrerequisiteCasterTypeSpellLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.IsArcane = true;
                    c.OnlySpontaneous = false;
                    c.RequiredSpellLevel = 3;
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.Not = true;
                    c.HideInUI = true;
                });
            });


            StargazerProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                new BlueprintProgression.ClassWithLevel {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                }
            };

            //Guiding Light
            var WitchFeatureSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("29a333b7ccad3214ea3a51943fa0d8e9");














            StargazerProgression.LevelEntries = new LevelEntry[10] {
                Helpers.LevelEntry(1, StargazersGuidingLightFeature, StargazersMysteryMagicHexFeature),
                Helpers.LevelEntry(2, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(3, StargazersMysteryMagicStarsDomainFeature),
                Helpers.LevelEntry(4, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(5, StargazersMysteryMagicCOMSFeature),
                Helpers.LevelEntry(6, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(7, StargazersMysteryMagicStarChartFeature),
                Helpers.LevelEntry(8, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(9, StargazersMysteryMagicHexFeature),
                Helpers.LevelEntry(10, StargazerSiderealArcanaSelection, StargazersStarsDanceFeature)
            };
            StargazerProgression.UIGroups = new UIGroup[] {};
            if (ModSettings.AddedContent.Classes.IsDisabled("Stargazer")) { return; }
            Helpers.RegisterClass(StargazerClass);
        }
    }
}
