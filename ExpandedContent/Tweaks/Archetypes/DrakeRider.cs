using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class DrakeRider {
        public static void AddDrakeRider() {

            var CavalierClass = Resources.GetBlueprint<BlueprintCharacterClass>("3adc3439f98cb534ba98df59838f02c7");
            var AnimalCompanionEmpty = Resources.GetBlueprintReference<BlueprintFeatureReference>("472091361cf118049a2b4339c4ea836a");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var DrakeCompanionFeatureGreen = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGreen");
            var DrakeCompanionFeatureSilver = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureSilver");
            var DrakeCompanionFeatureBlack = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlack");
            var DrakeCompanionFeatureBlue = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlue");
            var DrakeCompanionFeatureBrass = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBrass");
            var DrakeCompanionFeatureBronze = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBronze");
            var DrakeCompanionFeatureCopper = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureCopper");
            var DrakeCompanionFeatureGold = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGold");
            var DrakeCompanionFeatureRed = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureRed");
            var DrakeCompanionFeatureWhite = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureWhite");
            var DrakeCompanionFeatureUmbral = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureUmbral");
            var CavalierChargeFeature = Resources.GetBlueprint<BlueprintFeature>("30da5120b692d0d4f8a6840dff777abf");
            var CavalierTacticianFeature = Resources.GetBlueprint<BlueprintFeature>("404f6e3da48b87f4e9fca21150d47f71");
            var CavalierTacticianFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("7bc55b5e381358c45b42153b8b2603a6");
            var CavalierMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("0605927df6e2fdd42af6ee2424eb89f2");
            var CavalierBannerFeature = Resources.GetBlueprint<BlueprintFeature>("2d957edad0adb3d49991cfcd3ac4cbf8");
            var CavalierTacticianGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("c7913a6c300cb994b8e800e1096f9280");
            var CavalierBannerGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("457a2fcd5c27c504caeb6a61e6060702");



            var DrakeRiderArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DrakeRiderArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DrakeRiderArchetype.Name", "Drake Rider");
                bp.LocalizedDescription = Helpers.CreateString($"DrakeRiderArchetype.Description", "While many cavaliers dream of riding a dragon into battle, drake riders come to " +
                    "learn that the reality involves far more effort than they had expected. Unlike other cavaliers, drake riders must train their mounts from hatchlings, fighting " +
                    "and toiling alongside their drakes through countless struggles before the proud dragons are willing to accept them as riders.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DrakeRiderArchetype.Description", "While many cavaliers dream of riding a dragon into battle, drake riders come " +
                    "to learn that the reality involves far more effort than they had expected. Unlike other cavaliers, drake riders must train their mounts from hatchlings, fighting " +
                    "and toiling alongside their drakes through countless struggles before the proud dragons are willing to accept them as riders.");
            });
            var DrakeRiderBondFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DrakeRiderBondFeature", bp => {
                bp.SetName("Drake Selection");
                bp.SetDescription("Instead of an animal companion, the drake rider receives a drake companion to train into a suitable mount. \nThe drake riders levels are treated as " +
                    "druid levels for determining the level and abilities of the drake companion.");
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
            var ShakeItOff = Resources.GetBlueprint<BlueprintFeature>("6337b37f2a7c11b4ab0831d6780bce2a");
            var DrakeRiderShakeItOffFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeRiderShakeItOffFeature", bp => {
                bp.SetName("Shared Shake It Off Feat");
                bp.SetDescription("At level 3 both the drake rider and the drake have trained in fighting together through distraction, both gain the Shake It Off teamwork feat.");
                bp.m_Icon = ShakeItOff.m_Icon;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShakeItOff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFeatureToPet>(c => {
                    c.m_Feature = ShakeItOff.ToReference<BlueprintFeatureReference>();
                    c.m_PetType = Kingmaker.Enums.PetType.AnimalCompanion;
                });
            });
            var DrakenMountFeature = Resources.GetModBlueprint<BlueprintFeature>("DrakenMountFeature");
            var DrakeRiderMountTrainingFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeRiderMountTrainingFeature", bp => {
                bp.SetName("Completed Training");
                bp.SetDescription("By level 7 the drake rider successfully completes their drakes mount training, allowing them to be riden as soon as they reach medium size.");
                bp.IsClassFeature = true;
                bp.AddComponent<AddFeatureToPet>(c => {
                    c.m_Feature = DrakenMountFeature.ToReference<BlueprintFeatureReference>();
                });
            });
            DrakeRiderArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, CavalierTacticianFeature, CavalierTacticianFeatSelection, CavalierMountSelection),
                    Helpers.LevelEntry(3, CavalierChargeFeature),
                    Helpers.LevelEntry(5, CavalierBannerFeature),
                    Helpers.LevelEntry(9, CavalierTacticianFeatSelection, CavalierTacticianGreaterFeature),
                    Helpers.LevelEntry(14, CavalierBannerGreaterFeature),
                    Helpers.LevelEntry(17, CavalierTacticianFeatSelection)
            };
            DrakeRiderArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DrakeRiderBondFeature),
                    Helpers.LevelEntry(3, DrakeRiderShakeItOffFeature),
                    Helpers.LevelEntry(7, DrakeRiderMountTrainingFeature),
                    Helpers.LevelEntry(9, CavalierChargeFeature)
            };
            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = DrakeRiderArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Drake Rider")) { return; }
            CavalierClass.m_Archetypes = CavalierClass.m_Archetypes.AppendToArray(DrakeRiderArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
