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
    internal class ClutchThief {
        public static void AddClutchThief() {

            var RogueClass = Resources.GetBlueprint<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var DrakeCompanionFeatureGreen = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGreen");
            var DrakeCompanionFeatureSilver = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureSilver");
            var DrakeCompanionFeatureBlack = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlack");
            var DrakeCompanionFeatureBlue = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlue");
            var DrakeCompanionFeatureBrass = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBrass");
            var DrakeCompanionFeatureRed = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureRed");
            var DrakeCompanionFeatureWhite = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureWhite");
            var SneakAttack = Resources.GetBlueprint<BlueprintFeature>("9b9eac6709e1c084cb18c3a366e0ec87");
            var RogueTalentSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c074a5d615200494b8f2a9c845799d93");
            var DangerSenseRogue = Resources.GetBlueprint<BlueprintFeature>("0bcbe9e450b0e7b428f08f66c53c5136");
            var DebilitatingInjury = Resources.GetBlueprint<BlueprintFeature>("def114eb566dfca448e998969bf51586");
            var FinesseTrainingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b78d146cea711a84598f0acef69462ea");



            var ClutchThiefArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("ClutchThiefArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"ClutchThiefArchetype.Name", "Clutch Thief");
                bp.LocalizedDescription = Helpers.CreateString($"ClutchThiefArchetype.Description", "Whether to stop it falling into the wrong hands, or to hold the power " +
                    "for themselves, it is common for an aspiring thief to see a drake egg as a valuable score. Less common is for the egg to hatch while in the thieves care. " +
                    "With a bit of knowledge borrowed from the drake wardens on raising young drakes, the clutch thief gains a new and interesting partner in crime.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"ClutchThiefArchetype.Description", "Whether to stop it falling into the wrong hands, or to hold the power " +
                    "for themselves, it is common for an aspiring thief to see a drake egg as a valuable score. Less common is for the egg to hatch while in the thieves care. " +
                    "With a bit of knowledge borrowed from the drake wardens on raising young drakes, the clutch thief gains a new and interesting partner in crime.");
            });
            var ClutchThiefBondFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ClutchThiefBondFeature", bp => {
                bp.SetName("Clutch Thieves Bond");
                bp.SetDescription("At 4th level, the clutch thieves drake companion is finally strong enough to help as a animal companion, but his drake’s level is equal to his rogue level – 3.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DrakeCompanionFeatureBlack.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBlue.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBrass.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureRed.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureGreen.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureSilver.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureWhite.ToReference<BlueprintFeatureReference>()
                };
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeCompanionSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });
            ClutchThiefArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, RogueTalentSelection),
                    Helpers.LevelEntry(3, DangerSenseRogue),
                    Helpers.LevelEntry(4, DebilitatingInjury),
                    Helpers.LevelEntry(5, SneakAttack),
                    Helpers.LevelEntry(6, DangerSenseRogue),
                    Helpers.LevelEntry(8, RogueTalentSelection),
                    Helpers.LevelEntry(9, DangerSenseRogue),
                    Helpers.LevelEntry(11, SneakAttack),
                    Helpers.LevelEntry(12, DangerSenseRogue),
                    Helpers.LevelEntry(14, RogueTalentSelection),
                    Helpers.LevelEntry(15, DangerSenseRogue),
                    Helpers.LevelEntry(17, SneakAttack),
                    Helpers.LevelEntry(18, DangerSenseRogue),
                    Helpers.LevelEntry(19, FinesseTrainingSelection),
                    Helpers.LevelEntry(20, RogueTalentSelection)
            };
            ClutchThiefArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, ClutchThiefBondFeature)
            };
            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = ClutchThiefArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = -3
                });
            RogueClass.m_Archetypes = RogueClass.m_Archetypes.AppendToArray(ClutchThiefArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
