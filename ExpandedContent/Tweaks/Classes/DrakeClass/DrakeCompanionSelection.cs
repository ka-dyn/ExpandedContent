using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.DrakeClass {
    internal class DrakeCompanionSelection {

        public static void AddDrakeCompanionSelection() {

            var AnimalCompanionEmpty = Resources.GetBlueprintReference<BlueprintFeatureReference>("472091361cf118049a2b4339c4ea836a");
            var DrakeCompanionFeatureGreen = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGreen");
            var DrakeCompanionFeatureSilver = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureSilver");
            var DrakeCompanionFeatureBlack = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlack");
            var DrakeCompanionFeatureBlue = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlue");
            var DrakeCompanionFeatureBrass = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBrass");
            var DrakeCompanionFeatureRed = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureRed");
            var DrakeCompanionFeatureWhite = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureWhite");
            var DrakeCompanionFeatureGold = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGold");
            var DrakeCompanionFeatureBronze = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBronze");
            var DrakeCompanionFeatureCopper = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureCopper");
            var DrakeCompanionFeatureUmbral = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureUmbral");


            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");
            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");

            var DrakeCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection", bp => {
                bp.SetName("Drake Companion");
                bp.SetDescription("A draconic druid gains a drake companion instead of an animal companion. This ability replaces nature bond, " +
                    "wild empathy, woodland stride, venom immunity, a thousand faces, and timeless body.");
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DrakeCompanionProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                //No Animations 
                //bp.AddComponent<AddFeatureOnApply>(c => {
                //    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                //});
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
            });
        }
    }
}
