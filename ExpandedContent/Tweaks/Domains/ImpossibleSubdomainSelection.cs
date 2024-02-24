using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Tweaks.Domains {
    internal class ImpossibleSubdomainSelection {
        public static void AddImpossibleSubdomainSelection() {

            var ImpossibleDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("213a8480d22206b45acbfa0619ca5aaf");

            var IceSubdomainProgression = Resources.GetBlueprint<BlueprintProgression>("2108d8e7019e4c1faa094d2d6725e936");
            var IceSubdomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("09431040db0c4b36af11cdc8834cabfb");
            var UndeadSubdomainProgression = Resources.GetBlueprint<BlueprintProgression>("4f0332ac85174cdcb47e2d866a7948c3");
            var UndeadSubdomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("fb3811120baf4913a296e1991469fa88");

            var ImpossibleSubdomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ImpossibleSubdomainSelection", bp => {
                bp.SetName("Impossible Subdomain");
                bp.SetDescription("Your mastery over domains grants you access to more niche areas of divine power.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ImpossibleDomainSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.IgnorePrerequisites = true;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.m_Features = new BlueprintFeatureReference[] {
                    IceSubdomainProgressionSecondary.ToReference<BlueprintFeatureReference>(),
                    UndeadSubdomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
                };
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    IceSubdomainProgression.ToReference<BlueprintFeatureReference>(),
                    UndeadSubdomainProgression.ToReference<BlueprintFeatureReference>()
                };
            });
            FeatTools.AddAsMythicAbility(ImpossibleSubdomainSelection);
        }
    }
}
