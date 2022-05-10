using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Extensions;


namespace ExpandedContent.Utilities {
    public static class DomainTools {

        public static void RegisterDomain(BlueprintProgression domain) {
            BlueprintFeatureSelection DomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("48525e5da45c9c243a343fc6545dbdb9");
            DomainSelection.m_AllFeatures = DomainSelection.m_AllFeatures.AddToArray(domain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterSecondaryDomain(BlueprintProgression secondaryDomain) {
            BlueprintFeatureSelection SecondDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("43281c3d7fe18cc4d91928395837cd1e");
            SecondDomainSelection.m_AllFeatures = SecondDomainSelection.m_AllFeatures.AddToArray(secondaryDomain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterDruidDomain(BlueprintProgression secondaryDomain) {
            BlueprintFeatureSelection DruidDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5edfe84c93823d04f8c40ca2b4e0f039");
            DruidDomainSelection.m_AllFeatures = DruidDomainSelection.m_AllFeatures.AddToArray(secondaryDomain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterBlightDruidDomain(BlueprintProgression secondaryDomain)
        {
            BlueprintFeatureSelection BlightDruidDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("096fc02f6cc817a43991c4b437e12b8e");
            BlightDruidDomainSelection.m_AllFeatures = BlightDruidDomainSelection.m_AllFeatures.AddToArray(secondaryDomain.ToReference<BlueprintFeatureReference>());

        }
    }
}
