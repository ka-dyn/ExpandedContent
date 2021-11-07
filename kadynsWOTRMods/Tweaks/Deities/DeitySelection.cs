using kadynsWOTRMods.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Deities {
    internal class DeitySelectionFeature {
        
        public static void PatchDeitySelection() {

            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");

            var PaladinDeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("a7c8b73528d34c2479b4bd638503da1d");
            var AbadarFeature = Resources.GetBlueprint<BlueprintFeature>("6122dacf418611540a3c91e67197ee4e");
            var ErastilFeature = Resources.GetBlueprint<BlueprintFeature>("afc775188deb7a44aa4cbde03512c671");
            var IomedaeFeature = Resources.GetBlueprint<BlueprintFeature>("88d5da04361b16746bf5b65795e0c38c");
            var IroriFeature = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234");
            var SarenraeFeature = Resources.GetBlueprint<BlueprintFeature>("c1c4f7f64842e7e48849e5e67be11a1b");
            var ShelynFeature = Resources.GetBlueprint<BlueprintFeature>("b382afa31e4287644b77a8b30ed4aa0b");
            var ToragFeature = Resources.GetBlueprint<BlueprintFeature>("d2d5c5a58885a6b489727467e13c3337");
            var ZuraFeature = Resources.GetModBlueprint<BlueprintFeature>("ZuraFeature");
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
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AppendToArray(ZuraFeature.ToReference<BlueprintFeatureReference>());



            PaladinClass.RemoveComponents<PrerequisiteFeaturesFromList>();
            
            PaladinClass.AddComponents<PrerequisiteFeaturesFromList>(c => {
                c.m_Features = new BlueprintFeatureReference[11] { ApsuFeature.ToReference<BlueprintFeatureReference>(),
                    MilaniFeature.ToReference<BlueprintFeatureReference>(),
                    RagathielFeature.ToReference<BlueprintFeatureReference>(),
                    ArsheaFeature.ToReference<BlueprintFeatureReference>(),
                    AbadarFeature.ToReference<BlueprintFeatureReference>(),
                    IroriFeature.ToReference<BlueprintFeatureReference>(),
                    IomedaeFeature.ToReference<BlueprintFeatureReference>(),
                    SarenraeFeature.ToReference<BlueprintFeatureReference>(),
                    ShelynFeature.ToReference<BlueprintFeatureReference>(),
                    ToragFeature.ToReference<BlueprintFeatureReference>(),
                     ErastilFeature.ToReference<BlueprintFeatureReference>() };


            });















        }
    }
}
