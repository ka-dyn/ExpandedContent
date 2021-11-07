using kadynsWOTRMods.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Deities {
    internal class DeitySelectionFeature {
        
        public static void PatchDeitySelection() {

            var ApsuFeature = Resources.GetModBlueprint<BlueprintFeature>("ApsuFeature");
            var MephistophelesFeature = Resources.GetModBlueprint<BlueprintFeature>("MephistophelesFeature");
            var DispaterFeature = Resources.GetModBlueprint<BlueprintFeature>("DispaterFeature");
            var ArsheaFeature = Resources.GetModBlueprint<BlueprintFeature>("ArsheaFeature");
            var RagathielFeature = Resources.GetModBlueprint<BlueprintFeature>("RagathielFeature");
            var MilaniFeature = Resources.GetModBlueprint<BlueprintFeature>("MilaniFeature");
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AppendToArray(MilaniFeature.ToReference<BlueprintFeatureReference>());
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AppendToArray(RagathielFeature.ToReference<BlueprintFeatureReference>());
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AppendToArray(ArsheaFeature.ToReference<BlueprintFeatureReference>());
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AppendToArray(DispaterFeature.ToReference<BlueprintFeatureReference>());
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AppendToArray(MephistophelesFeature.ToReference<BlueprintFeatureReference>());
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AppendToArray(ApsuFeature.ToReference<BlueprintFeatureReference>());
        }
    }
}
