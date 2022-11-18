using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using HarmonyLib;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.Enums;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous {
    public class NewModifierDescriptors {
        //Turns out I did not need any of this as the new abilities use existing modifiers
        public enum ECModifierDescriptor : int {
            Channel = 1229
        }

        [PostPatchInitialize]
        static void Update_ModifierDescriptorComparer_SortedValues() {
            InsertAfter(ECModifierDescriptor.Channel, ModifierDescriptor.UntypedStackable);

            void InsertAfter(Enum value, ModifierDescriptor after) {
                ModifierDescriptorComparer.SortedValues = ModifierDescriptorComparer
                    .SortedValues.InsertAfterElement((ModifierDescriptor)value, after);
            };
        }

        [HarmonyPatch(typeof(ModifierDescriptorComparer), "Compare", new Type[] { typeof(ModifierDescriptor), typeof(ModifierDescriptor) })]
        static class ModifierDescriptorComparer_Compare_Patch {
            static SortedDictionary<ModifierDescriptor, int> order;

            static bool Prefix(ModifierDescriptorComparer __instance, ModifierDescriptor x, ModifierDescriptor y, ref int __result) {
                if (IsTTTDescriptor(x) || IsTTTDescriptor(y)) {
                    if (order == null) {
                        order = new SortedDictionary<ModifierDescriptor, int>();
                        int i = 0;
                        for (i = 0; i < ModifierDescriptorComparer.SortedValues.Length; i++) {
                            order[ModifierDescriptorComparer.SortedValues[i]] = i;
                        }
                    }
                    __result = order.Get(x).CompareTo(order.Get(y));
                    return false;
                }
                return true;
            }

            private static bool IsTTTDescriptor(ModifierDescriptor desc) {
                return Enum.IsDefined(typeof(ModifierDescriptor), (int)desc);
            }
        }

        [HarmonyPatch(typeof(AbilityModifiersStrings), "GetName", new Type[] { typeof(ModifierDescriptor) })]
        static class AbilityModifierStrings_GetName_Patch {
            static void Postfix(AbilityModifiersStrings __instance, ModifierDescriptor descriptor, ref string __result) {
                if (__result != __instance.DefaultName) { return; }
                switch (descriptor) {
                    case (ModifierDescriptor)ECModifierDescriptor.Channel:
                        __result = "Channel";
                        break;                    
                }
            }
        }
    }
}
