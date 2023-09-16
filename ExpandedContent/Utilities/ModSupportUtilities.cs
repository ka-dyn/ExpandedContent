using Newtonsoft.Json.Linq;
using System;
using System.IO;
using static UnityModManagerNet.UnityModManager;

namespace ExpandedContent.Utilities {
    class ModSupportUtilities {

        public static ModEntry ModEntry;
        //Old Filepath incase something breaks                      var FilePath = $"..{Path.DirectorySeparatorChar}TabletopTweaks-Base{Path.DirectorySeparatorChar}UserSettings{Path.DirectorySeparatorChar}Fixes.json";

        public static bool GetShiftersRushTTTBaseSetting() {
            try {
                var FilePath = Path.Combine("..", "TabletopTweaks-Base", "UserSettings", "Fixes.json");
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, FilePath);
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["ShifterRush"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
        public static bool GetEnergizedWildShapePrerequisitesTTTBaseSetting() {
            try {
                var FilePath = Path.Combine("..", "TabletopTweaks-Base", "UserSettings", "Fixes.json");
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, FilePath);
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["EnergizedWildShapePrerequisites"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
        public static bool GetRakingClawsTTTBaseSetting() {
            try {
                var FilePath = Path.Combine("..", "TabletopTweaks-Base", "UserSettings", "Fixes.json");
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, FilePath); 
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["RakingClaws"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
        public static bool GetFrightfulShapeTTTBaseSetting() {
            try {
                var FilePath = Path.Combine("..", "TabletopTweaks-Base", "UserSettings", "Fixes.json");
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, FilePath); 
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["FrightfulShape"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
    }
}
