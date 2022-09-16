using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class DrakeWarden {
        public static void AddDrakeWarden() {

            var RangerClass = Resources.GetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var HuntersBondSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b705c5184a96a84428eeb35ae2517a14");
            var DrakeCompanionFeatureGreen = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGreen");
            var DrakeCompanionFeatureSilver = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureSilver");
            var DrakeCompanionFeatureBlack = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlack");
            var DrakeCompanionFeatureBlue = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlue");
            var DrakeCompanionFeatureBrass = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBrass");
            var DrakeCompanionFeatureBronze = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBronze");
            var DrakeCompanionFeatureGold = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGold");
            var DrakeCompanionFeatureRed = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureRed");
            var DrakeCompanionFeatureWhite = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureWhite");
            var DrakeCompanionFeatureCopper = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureCopper");
            var FavoriteEnemySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("16cc2c937ea8d714193017780e7d4fc6");
            var FavoriteEnemyRankUp = Resources.GetBlueprint<BlueprintFeatureSelection>("c1be13839472aad46b152cf10cf46179");
            var FavoriteTerrainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("a6ea422d7308c0d428a541562faedefd");
            var DrakeWardenArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DrakeWardenArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DrakeWardenArchetype.Name", "Drake Warden");
                bp.LocalizedDescription = Helpers.CreateString($"DrakeWardenArchetype.Description", "Some rangers specialize in dealing with rambunctious younger drakes, " +
                    "protecting them and teaching them to tolerate, and even trust, humanoid creatures. These drake wardens follow and pass along secret techniques for " +
                    "raising drakes effectively, and thanks to their methods, their drakes are both fiercely loyal and extremely useful for scouting and stealth missions.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DrakeWardenArchetype.Description", "Some rangers specialize in dealing with rambunctious younger drakes, " +
                    "protecting them and teaching them to tolerate, and even trust, humanoid creatures. These drake wardens follow and pass along secret techniques for " +
                    "raising drakes effectively, and thanks to their methods, their drakes are both fiercely loyal and extremely useful for scouting and stealth missions.");
            });
            var DrakeWardenBondFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DrakeWardenBondFeature", bp => {
                bp.SetName("Drake Wardens Bond");
                bp.SetDescription("At 4th level, a drake warden gains a drake companion instead of an animal companion, but his drake’s level is equal to his ranger level – 3.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DrakeCompanionFeatureBlack.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBlue.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBrass.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBronze.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureCopper.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureGold.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureGreen.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureRed.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureSilver.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureWhite.ToReference<BlueprintFeatureReference>()
                };
                bp.AddComponent<AddFacts>(c => {                    
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeCompanionSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });
            DrakeWardenArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, HuntersBondSelection),
                    Helpers.LevelEntry(5, FavoriteEnemySelection, FavoriteEnemyRankUp),
                    Helpers.LevelEntry(8, FavoriteTerrainSelection),
                    Helpers.LevelEntry(13, FavoriteTerrainSelection),
                    Helpers.LevelEntry(15, FavoriteEnemySelection, FavoriteEnemyRankUp),
                    Helpers.LevelEntry(20, FavoriteEnemySelection, FavoriteEnemyRankUp),
            };
            DrakeWardenArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, DrakeWardenBondFeature)    
            };
            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = DrakeWardenArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = -3
                });

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Drake Warden")) { return; }
            RangerClass.m_Archetypes = RangerClass.m_Archetypes.AppendToArray(DrakeWardenArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
