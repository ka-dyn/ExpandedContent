using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Extensions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Enums;
using Kingmaker.EntitySystem.Stats;

namespace ExpandedContent.Tweaks.Miscellaneous.AlchemistDiscoveries {
    internal class PheromonesDiscovery {
        public static void AddPheromonesDiccovery() {

            var DicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cd86c437488386f438dcc9ae727ea2a6");
            var ExtraDicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("537965879fc24ad3948aaffa7a1a3a66");
            var VivisectionistDicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("67f499218a0e22944abab6fe1c9eaeee");
            var ExtraVivisectionistSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("10287e7b8cee479e82ea88bd6d2d4dae");


            var PheromonesDiscoveryFeature = Helpers.CreateBlueprint<BlueprintFeature>("PheromonesDiscoveryFeature", bp => {
                bp.SetName("Pheromones");
                bp.SetDescription("The alchemist exudes an imperceptible musk that grants him a permanent +3 competence bonus on all {g|Encyclopedia:Check}checks{/g} " +
                    "involving {g|Encyclopedia:Persuasion}Persuasion{/g}.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Competence;
                    c.Stat = StatType.SkillPersuasion;
                    c.Value = 3;
                });                
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Discovery
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });





            DicoverySelection.m_AllFeatures = DicoverySelection.m_AllFeatures.AppendToArray(PheromonesDiscoveryFeature.ToReference<BlueprintFeatureReference>());
            ExtraDicoverySelection.m_AllFeatures = ExtraDicoverySelection.m_AllFeatures.AppendToArray(PheromonesDiscoveryFeature.ToReference<BlueprintFeatureReference>());
            VivisectionistDicoverySelection.m_AllFeatures = VivisectionistDicoverySelection.m_AllFeatures.AppendToArray(PheromonesDiscoveryFeature.ToReference<BlueprintFeatureReference>());
            ExtraVivisectionistSelection.m_AllFeatures = ExtraVivisectionistSelection.m_AllFeatures.AppendToArray(PheromonesDiscoveryFeature.ToReference<BlueprintFeatureReference>());
        }
    }
}
