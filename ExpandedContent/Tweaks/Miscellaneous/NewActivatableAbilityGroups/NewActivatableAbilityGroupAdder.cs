using HarmonyLib;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous.NewActivatableAbilityGroups {
    public static class NewActivatableAbilityGroupAdder {

        public enum ECActivatableAbilityGroup : int {
            GiftOfClawAndHorn = 2801,
            BeastformMutagen = 2802
        }

        private static bool IsECGroup(this ActivatableAbilityGroup group) {
            return Enum.IsDefined(typeof(ECActivatableAbilityGroup), (int)group);
        }

        [HarmonyPatch(typeof(UnitPartActivatableAbility), nameof(UnitPartActivatableAbility.IncreaseGroupSize), new Type[] { typeof(ActivatableAbilityGroup) })]
        static class UnitPartActivatableAbility_IncreaseGroupSize_Patch {

            static bool Prefix(UnitPartActivatableAbility __instance, ActivatableAbilityGroup group) {
                if (group.IsECGroup()) {
                    var extensionPart = __instance.Owner.Ensure<UnitPartActivatableAbilityGroupEC>();
                    extensionPart.IncreaseGroupSize((ECActivatableAbilityGroup)group);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(UnitPartActivatableAbility), nameof(UnitPartActivatableAbility.DecreaseGroupSize), new Type[] { typeof(ActivatableAbilityGroup) })]
        static class UnitPartActivatableAbility_DecreaseGroupSize_Patch {

            static bool Prefix(UnitPartActivatableAbility __instance, ActivatableAbilityGroup group) {
                if (group.IsECGroup()) {
                    var extensionPart = __instance.Owner.Ensure<UnitPartActivatableAbilityGroupEC>();
                    extensionPart.DecreaseGroupSize((ECActivatableAbilityGroup)group);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(UnitPartActivatableAbility), nameof(UnitPartActivatableAbility.GetGroupSize), new Type[] { typeof(ActivatableAbilityGroup) })]
        static class UnitPartActivatableAbility_GetGroupSize_Patch {

            static bool Prefix(UnitPartActivatableAbility __instance, ActivatableAbilityGroup group, ref int __result) {
                if (group.IsECGroup()) {
                    var extensionPart = __instance.Owner.Ensure<UnitPartActivatableAbilityGroupEC>();
                    __result = extensionPart.GetGroupSize((ECActivatableAbilityGroup)group);
                    return false;
                }
                return true;
            }
        }
    }
}
