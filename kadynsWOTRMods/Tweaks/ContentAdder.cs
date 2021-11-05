using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;

namespace kadynsWOTRMods.Tweaks {
    class ContentAdder {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;

                

                
            }
        }
    }
}
