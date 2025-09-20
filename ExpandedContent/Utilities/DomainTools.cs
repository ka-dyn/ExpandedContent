﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Extensions;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.FactLogic;


namespace ExpandedContent.Utilities {
    public static class DomainTools {

        public static void RegisterDomain(BlueprintProgression domain) {
            BlueprintFeatureSelection DomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("48525e5da45c9c243a343fc6545dbdb9");
            DomainSelection.m_AllFeatures = DomainSelection.m_AllFeatures.AddToArray(domain.ToReference<BlueprintFeatureReference>());
            BlueprintFeatureSelection FakeDivineSparkSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("e6d337e709b4478cb6e5389d3f85b601");
            FakeDivineSparkSelection.m_AllFeatures = FakeDivineSparkSelection.m_AllFeatures.AddToArray(domain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterSecondaryDomain(BlueprintProgression secondarydomain) {
            BlueprintFeatureSelection SecondDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("43281c3d7fe18cc4d91928395837cd1e");
            SecondDomainSelection.m_AllFeatures = SecondDomainSelection.m_AllFeatures.AddToArray(secondarydomain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterDruidDomain(BlueprintProgression domain) {
            BlueprintFeatureSelection DruidDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5edfe84c93823d04f8c40ca2b4e0f039");
            DruidDomainSelection.m_AllFeatures = DruidDomainSelection.m_AllFeatures.AddToArray(domain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterBlightDruidDomain(BlueprintProgression secondarydomain)
        {
            BlueprintFeatureSelection BlightDruidDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("096fc02f6cc817a43991c4b437e12b8e");
            BlightDruidDomainSelection.m_AllFeatures = BlightDruidDomainSelection.m_AllFeatures.AddToArray(secondarydomain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterDivineHunterDomain(BlueprintProgression domain) {
            BlueprintFeatureSelection DivineHunterDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("72909a37a1ed5344f88ec9d1d31f5c5b");
            DivineHunterDomainSelection.m_AllFeatures = DivineHunterDomainSelection.m_AllFeatures.AddToArray(domain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterTempleDomain(BlueprintProgression domain) {
            BlueprintFeatureSelection DomainChampionFeature = Resources.GetModBlueprint<BlueprintFeatureSelection>("DomainChampionFeature");
            DomainChampionFeature.m_AllFeatures = DomainChampionFeature.m_AllFeatures.AddToArray(domain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterSecondaryTempleDomain(BlueprintProgression secondarydomain) {
            BlueprintFeatureSelection DomainChampionFeatureSecondary = Resources.GetModBlueprint<BlueprintFeatureSelection>("DomainChampionFeatureSecondary");
            DomainChampionFeatureSecondary.m_AllFeatures = DomainChampionFeatureSecondary.m_AllFeatures.AddToArray(secondarydomain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterImpossibleDomain(BlueprintProgression domain, BlueprintProgression secondarydomain) {
            BlueprintFeatureSelection ImpossibleDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("213a8480d22206b45acbfa0619ca5aaf");
            ImpossibleDomainSelection.m_AllFeatures = ImpossibleDomainSelection.m_AllFeatures.AddToArray(domain.ToReference<BlueprintFeatureReference>());
            ImpossibleDomainSelection.m_Features = ImpossibleDomainSelection.m_Features.AddToArray(secondarydomain.ToReference<BlueprintFeatureReference>());

        }
        public static void RegisterImpossibleSubdomain(BlueprintProgression subdomain, BlueprintProgression secondarysubdomain) {
            BlueprintFeatureSelection ImpossibleSubdomainSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("ImpossibleSubdomainSelection");
            ImpossibleSubdomainSelection.m_AllFeatures = ImpossibleSubdomainSelection.m_AllFeatures.AddToArray(subdomain.ToReference<BlueprintFeatureReference>());
            ImpossibleSubdomainSelection.m_Features = ImpossibleSubdomainSelection.m_Features.AddToArray(secondarysubdomain.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterSeparatistDomain(BlueprintProgression separatistdomain) {
            BlueprintFeatureSelection SeparatistDomainSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("42b781e4375d499383b2602d90661283");
            SeparatistDomainSelection.m_AllFeatures = SeparatistDomainSelection.m_AllFeatures.AddToArray(separatistdomain.ToReference<BlueprintFeatureReference>());
        }

        public static void AllowFakeDivineSpark(BlueprintFeature domainallowedfeature) {
            var FakeDivineSparkFeatureAddFactsComponent = Resources.GetBlueprint<BlueprintFeature>("bcf3ea648bd94612b8e8f1a69e9026e5").GetComponent<AddFacts>();
            FakeDivineSparkFeatureAddFactsComponent.m_Facts = FakeDivineSparkFeatureAddFactsComponent.m_Facts.AppendToArray(domainallowedfeature.ToReference<BlueprintUnitFactReference>());

        }
    }
}
