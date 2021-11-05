using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsTweaks.Config;
using kadynsTweaks.Utilities;
using UnityModManagerNet;

namespace kadynsTweaks {
    static class Main {
        public static bool Enabled;

        public static bool Load(UnityModManager.ModEntry modEntry) {
            ModSettings.ModEntry = modEntry;
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();
            return true;
        }

    }
}