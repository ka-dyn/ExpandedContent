using HarmonyLib;
using kadynsTweaks;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsTweaks.Extensions;

namespace kadynsTweaks.Tweaks.RacialPrereqs {
    internal class Stonelord {

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            public  static void Postfix() {
                if (Initialized) return;
                Initialized = true;

           
                PatchStonelordArchetype();

                static void PatchStonelordArchetype() {
                    BlueprintArchetype StonelordArchetype = Resources.GetBlueprint<BlueprintArchetype>("cf0f99b3cd7444a48681b1c1c4aa2a41");
                    StonelordArchetype.RemoveComponents<PrerequisiteFeature>();

               
                }

            }
        }

        internal static void PatchStonelordArchetype() {
            throw new NotImplementedException();
        }
    }
}