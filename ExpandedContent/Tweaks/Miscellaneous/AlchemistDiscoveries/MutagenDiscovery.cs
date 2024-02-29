using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous.AlchemistDiscoveries {
    internal class MutagenDiscovery {
        public static void AddMutagenDiscovery() {

            var DicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cd86c437488386f438dcc9ae727ea2a6");
            var ExtraDicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("537965879fc24ad3948aaffa7a1a3a66");

            var AlchemistClass = Resources.GetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var IncenseSynthesizer = Resources.GetBlueprint<BlueprintArchetype>("6faf49766d8156c419d08f556a40374f");

            var MutagenFeature = Resources.GetBlueprint<BlueprintFeature>("cee8f65448ce71c4b8b8ca13751dd8ea");
            var MutationWarriorMutagenFeature = Resources.GetBlueprint<BlueprintFeature>("20b8ea6975554b347b43c6c5f5e65ca8");

            var MutagenDiscoveryFeature = Helpers.CreateBlueprint<BlueprintFeature>("MutagenDiscoveryFeature", bp => {
                bp.SetName("Mutagen");
                bp.SetDescription("This discovery gives the alchemist the mutagen class ability. (This discovery exists so alchemist archetypes who have variant " +
                    "mutagens, such as the mindchemist, can learn how to make standard mutagens.)");
                bp.m_Icon = MutagenFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { MutagenFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = MutagenFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = MutationWarriorMutagenFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Discovery
                };
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = IncenseSynthesizer.ToReference<BlueprintArchetypeReference>();
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideNotAvailibleInUI = true;
            });

            DicoverySelection.m_AllFeatures = DicoverySelection.m_AllFeatures.AppendToArray(MutagenDiscoveryFeature.ToReference<BlueprintFeatureReference>());
            ExtraDicoverySelection.m_AllFeatures = ExtraDicoverySelection.m_AllFeatures.AppendToArray(MutagenDiscoveryFeature.ToReference<BlueprintFeatureReference>());
        }
    }
}
