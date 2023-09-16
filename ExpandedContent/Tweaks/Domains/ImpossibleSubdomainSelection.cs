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
                bp.m_Features = new BlueprintFeatureReference[] { };
                bp.m_AllFeatures = new BlueprintFeatureReference[] { };
            });
            FeatTools.AddAsMythicAbility(ImpossibleSubdomainSelection);
        }
    }
}
