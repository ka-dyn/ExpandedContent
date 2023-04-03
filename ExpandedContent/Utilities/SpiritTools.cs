using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Extensions;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;

namespace ExpandedContent.Utilities {
    internal class SpiritTools {
        //Base Spirit
        public static void RegisterSpirit(BlueprintFeature spirit) {
            BlueprintFeatureSelection ShamanSpiritSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("00c8c566d1825dd4a871250f35285982");
            ShamanSpiritSelection.m_AllFeatures = ShamanSpiritSelection.m_AllFeatures.AddToArray(spirit.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterSecondSpirit(BlueprintFeature secondspirit) {
            BlueprintFeatureSelection ShamanSecondSpiritSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2faa80662a56ab644aec2f875a68597f");
            ShamanSecondSpiritSelection.m_AllFeatures = ShamanSecondSpiritSelection.m_AllFeatures.AddToArray(secondspirit.ToReference<BlueprintFeatureReference>());
        }
        //Wandering Spirit
        public static void RegisterWanderingSpirit(BlueprintFeature wanderingspirit) {
            AbilityApplyFact ShamanWanderingSpiritAbility = Resources.GetBlueprint<BlueprintAbility>("709db7ec794271b43b9c416b520d765c").GetComponent<AbilityApplyFact>();
            ShamanWanderingSpiritAbility.m_Facts = ShamanWanderingSpiritAbility.m_Facts.AppendToArray(wanderingspirit.ToReference<BlueprintUnitFactReference>());
        }
        //Unsworn Shaman Wandering Spirits
        public static void RegisterUnswornSpirit1(BlueprintFeature unswornspirit1) {
            AbilityApplyFact UnswornShamanWanderingSpiritAbility1 = Resources.GetBlueprint<BlueprintAbility>("88032ba214e97a442849ef0d15ce1485").GetComponent<AbilityApplyFact>();
            UnswornShamanWanderingSpiritAbility1.m_Facts = UnswornShamanWanderingSpiritAbility1.m_Facts.AppendToArray(unswornspirit1.ToReference<BlueprintUnitFactReference>());
        }
        public static void RegisterUnswornSpirit2(BlueprintFeature unswornspirit2) {
            AbilityApplyFact UnswornShamanWanderingSpiritAbility2 = Resources.GetBlueprint<BlueprintAbility>("77e02067a4f59ba4793c79a34391a658").GetComponent<AbilityApplyFact>();
            UnswornShamanWanderingSpiritAbility2.m_Facts = UnswornShamanWanderingSpiritAbility2.m_Facts.AppendToArray(unswornspirit2.ToReference<BlueprintUnitFactReference>());
        }
    }
}
