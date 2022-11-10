using HarmonyLib;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Utilities {
    [HarmonyPatch(typeof(PortraitData), "get_FullLengthPortrait")]
    public static class FullPortraitInjecotr {
        public static Dictionary<PortraitData, Sprite> Replacements = new();
        public static bool Prefix(PortraitData __instance, ref Sprite __result) {
            if (Replacements.TryGetValue(__instance, out __result))
                return false;
            return true;
        }
    }
    [HarmonyPatch(typeof(PortraitData), "get_HalfLengthPortrait")]
    public static class HalfPortraitInjecotr {
        public static Dictionary<PortraitData, Sprite> Replacements = new();
        public static bool Prefix(PortraitData __instance, ref Sprite __result) {
            if (Replacements.TryGetValue(__instance, out __result))
                return false;
            return true;
        }
    }
    [HarmonyPatch(typeof(PortraitData), "get_SmallPortrait")]
    public static class SmallPortraitInjecotr {
        public static Dictionary<PortraitData, Sprite> Replacements = new();
        public static bool Prefix(PortraitData __instance, ref Sprite __result) {
            if (Replacements.TryGetValue(__instance, out __result))
                return false;
            return true;
        }
    }
    [HarmonyPatch(typeof(PortraitData), "get_PetEyePortrait")]
    public static class EyePortraitInjecotr {
        public static Dictionary<PortraitData, Sprite> Replacements = new();
        public static bool Prefix(PortraitData __instance, ref Sprite __result) {
            if (Replacements.TryGetValue(__instance, out __result))
                return false;
            return true;
        }
    }
}
