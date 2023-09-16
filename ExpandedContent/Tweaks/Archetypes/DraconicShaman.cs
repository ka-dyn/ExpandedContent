using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class DraconicShaman {
        public static void AddDraconicShaman() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var ShamanSpritSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("00c8c566d1825dd4a871250f35285982");
            var ShamanSpritAnimalSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("d22f319fefac4ca4b90f03ac5cb9c714");
            var ShamanHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4223fe18c75d4d14787af196a04e14e7");
            var AnimalCompanionEmpty = Resources.GetBlueprintReference<BlueprintFeatureReference>("472091361cf118049a2b4339c4ea836a");
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
            var DrakeCompanionFeatureUmbral = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureUmbral");



            var DragonType = Resources.GetBlueprint<BlueprintFeature>("455ac88e22f55804ab87c2467deff1d6");



            var DraconicShamanArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DraconicShamanArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DraconicShamanArchetype.Name", "Draconic Shaman");
                bp.LocalizedDescription = Helpers.CreateString($"DraconicShamanArchetype.Description", "Shaman often have strong ties to dragon gods and imperial dragons who act as mentors. " +
                    "Some of these shamans draw their powers from the might of dragons, rather than from spirits. These shamans each gain a powerful drake as an ally and view caring for that " +
                    "drake as a sacred duty.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DraconicShamanArchetype.Description", "Shaman often have strong ties to dragon gods and imperial dragons who act as mentors. " +
                    "Some of these shamans draw their powers from the might of dragons, rather than from spirits. These shamans each gain a powerful drake as an ally and view caring for that " +
                    "drake as a sacred duty.");                
            });
            var ShamanDrakeCompanionFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ShamanDrakeCompanionFeature", bp => {
                bp.SetName("Drake Companion");
                bp.SetDescription("A draconic shaman gains a drake companion instead of a spirit animal, and she communes with the drake to prepare her spells just as other shamans commune with " +
                    "their spirit animals. She doesn’t gain a primary spirit, but she still gains wandering spirit at 4th level. She must select all her hexes (other than her wandering hexes) from " +
                    "the list of shaman hexes. She doesn’t gain spirit magic slots until 4th level when she gains her wandering spirit. \nThis ability replaces " +
                    "spirit, spirit animal, and the hexes gained at 4th and 10th levels and alters spirit magic.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AnimalCompanionEmpty,
                    DrakeCompanionFeatureBlack.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBlue.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBrass.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBronze.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureCopper.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureGold.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureGreen.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureRed.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureSilver.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureUmbral.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureWhite.ToReference<BlueprintFeatureReference>()
                };
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeCompanionSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });



            DraconicShamanArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, ShamanSpritSelection, ShamanSpritAnimalSelection),
                    Helpers.LevelEntry(4, ShamanHexSelection),
                    Helpers.LevelEntry(10, ShamanHexSelection)
            };
            DraconicShamanArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, ShamanDrakeCompanionFeature)

            };

            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = DraconicShamanArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Draconic Shaman")) { return; }
            ShamanClass.m_Archetypes = ShamanClass.m_Archetypes.AppendToArray(DraconicShamanArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
