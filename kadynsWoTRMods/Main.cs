using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints.JsonSystem;
using System;
using ExpandedContent.Config;
using ExpandedContent.Utilities;
using UnityModManagerNet;

namespace ExpandedContent {
    static class Main {
        public static bool Enabled;
        public static bool Load(UnityModManager.ModEntry modEntry) {
            var harmony = new Harmony(modEntry.Info.Id);
            ModSettings.ModEntry = modEntry;
            ModSettings.LoadAllSettings();
            ModSettings.ModEntry.OnSaveGUI = OnSaveGUI;
            ModSettings.ModEntry.OnGUI = UMMSettingsUI.OnGUI;
            harmony.PatchAll();
            PostPatchInitializer.Initialize();
            return true;
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry) {
            
            ModSettings.SaveSettings("AddedContent.json", ModSettings.AddedContent);
            
        }

        internal static void LogPatch(string v, object coupDeGraceAbility) {
            throw new NotImplementedException();
        }

        public static void Log(string msg) {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void LogDebug(string msg) {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        public static void LogPatch(string action, [NotNull] IScriptableObjectWithAssetId bp) {
            Log($"{action}: {bp.AssetGuid} - {bp.name}");
        }
        public static void LogHeader(string msg) {
            Log($"--{msg.ToUpper()}--");
        }
        public static void Error(Exception e, string message) {
            Log(message);
            Log(e.ToString());
            PFLog.Mods.Error(message);
        }
        public static void Error(string message) {
            Log(message);
            PFLog.Mods.Error(message);
        }

        internal static void LogPatch(string v, Action addPulura) {
            throw new NotImplementedException();
        }
    }
}