using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Config;
using kadynsWOTRMods.Utilities;
using UnityModManagerNet;

namespace kadynsWOTRMods {
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