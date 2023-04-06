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
        //Add Shaman Hex
        public static void RegisterShamanHex(BlueprintFeature shamanhex) {
            BlueprintFeatureSelection ShamanHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4223fe18c75d4d14787af196a04e14e7");
            ShamanHexSelection.m_AllFeatures = ShamanHexSelection.m_AllFeatures.AppendToArray(shamanhex.ToReference<BlueprintFeatureReference>());
            var WanderingHexAbilities = new AbilityApplyFact[] {
                Resources.GetBlueprint<BlueprintAbility>("bff5039c06a4a494b84bbca5c1cf93e5").GetComponent<AbilityApplyFact>(), //Unsworn1
                Resources.GetBlueprint<BlueprintAbility>("8e37166acde792f458c1c02a7392ab7c").GetComponent<AbilityApplyFact>(), //Unsworn2
                Resources.GetBlueprint<BlueprintAbility>("cf57e3971a4f8d340a200794ba586bf9").GetComponent<AbilityApplyFact>(), //Unsworn3
                Resources.GetBlueprint<BlueprintAbility>("4248be819e269724086d8f6b44fafd10").GetComponent<AbilityApplyFact>(), //Unsworn4
                Resources.GetBlueprint<BlueprintAbility>("49c4c45281805fc43aa66cd036825450").GetComponent<AbilityApplyFact>(), //Unsworn5
                Resources.GetBlueprint<BlueprintAbility>("6485fe813137acd41aa56df5a39e0195").GetComponent<AbilityApplyFact>(), //Unsworn6
                Resources.GetBlueprint<BlueprintAbility>("e7be24dd1cf591d4baecd9578705513d").GetComponent<AbilityApplyFact>(), //Normal1
                Resources.GetBlueprint<BlueprintAbility>("1c06611ab264115479d16c0ff3ab0446").GetComponent<AbilityApplyFact>(), //Normal2
            };
            foreach (var WanderingHexAbility in WanderingHexAbilities) {
                WanderingHexAbility.m_Facts = WanderingHexAbility.m_Facts.AppendToArray(shamanhex.ToReference<BlueprintUnitFactReference>());
            }
        }
    }
}
