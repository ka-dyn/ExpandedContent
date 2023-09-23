using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;


namespace ExpandedContent.Utilities {
    public static class BlessingTools {
        public static void RegisterBlessing(BlueprintFeature blessing) {
            BlueprintFeatureSelection BlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("6d9dcc2a59210a14891aeedb09d406aa");
            BlueprintFeatureSelection SecondBlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b7ce4a67287cda746a59b31c042305cf");
            BlessingSelection.m_AllFeatures = BlessingSelection.m_AllFeatures.AddToArray(blessing.ToReference<BlueprintFeatureReference>());
            SecondBlessingSelection.m_AllFeatures = SecondBlessingSelection.m_AllFeatures.AddToArray(blessing.ToReference<BlueprintFeatureReference>());
        }

        public static void CreateDivineTrackerBlessing(string blueprintoutputname, BlueprintFeature warpriestblessing, string description) {

            var RangerClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("cda0615668a6df14eb36ba19ee881af6");
            var DivineTrackerBlessingSelectionFirst = Resources.GetModBlueprint<BlueprintFeatureSelection>("DivineTrackerBlessingSelectionFirst");
            var DivineTrackerBlessingSelectionSecond = Resources.GetModBlueprint<BlueprintFeatureSelection>("DivineTrackerBlessingSelectionSecond");

            var blueprintfeature = Helpers.CreateBlueprint<BlueprintFeature>($"{blueprintoutputname}", bp => {
                bp.m_DisplayName = warpriestblessing.m_DisplayName;
                bp.SetDescription(description);
                bp.Components = warpriestblessing.Components;
                bp.IsClassFeature = false;
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = true;
                    c.m_Feature = warpriestblessing.ToReference<BlueprintFeatureReference>();
                });
            });

            var AddFeatureOnClassLevel = blueprintfeature.GetComponent<AddFeatureOnClassLevel>();
            AddFeatureOnClassLevel.m_Class = RangerClass;
            AddFeatureOnClassLevel.Level = 13;

            warpriestblessing.AddComponent<PrerequisiteNoFeature>(c => {
                c.HideInUI = true;
                c.m_Feature = blueprintfeature.ToReference<BlueprintFeatureReference>();
            });

            DivineTrackerBlessingSelectionFirst.m_AllFeatures = DivineTrackerBlessingSelectionFirst.m_AllFeatures.AppendToArray(blueprintfeature.ToReference<BlueprintFeatureReference>());
            DivineTrackerBlessingSelectionFirst.m_Features = DivineTrackerBlessingSelectionFirst.m_Features.AppendToArray(blueprintfeature.ToReference<BlueprintFeatureReference>());
            DivineTrackerBlessingSelectionSecond.m_AllFeatures = DivineTrackerBlessingSelectionSecond.m_AllFeatures.AppendToArray(blueprintfeature.ToReference<BlueprintFeatureReference>());
            DivineTrackerBlessingSelectionSecond.m_Features = DivineTrackerBlessingSelectionSecond.m_Features.AppendToArray(blueprintfeature.ToReference<BlueprintFeatureReference>());
        }
    }
}
