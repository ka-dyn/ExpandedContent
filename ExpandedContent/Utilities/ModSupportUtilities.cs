using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager;

namespace ExpandedContent.Utilities {
    class ModSupportUtilities {

        public static ModEntry ModEntry;
        public static bool GetShiftersRushTTTBaseSetting() {
            try {
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, @"..\TabletopTweaks-Base\UserSettings\Fixes.json");
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["ShifterRush"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
        public static bool GetEnergizedWildShapePrerequisitesTTTBaseSetting() {
            try {
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, @"..\TabletopTweaks-Base\UserSettings\Fixes.json");
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["EnergizedWildShapePrerequisites"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
        public static bool GetRakingClawsTTTBaseSetting() { //This one is not it TTT yet
            try {
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, @"..\TabletopTweaks-Base\UserSettings\Fixes.json");
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["RakingClaws"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
        public static bool GetFrightfulShapeTTTBaseSetting() {
            try {
                var xks = System.IO.Path.Combine(Config.ModSettings.ModEntry.Path, @"..\TabletopTweaks-Base\UserSettings\Fixes.json");
                JObject o1 = JObject.Parse(File.ReadAllText(xks));
                return (bool)o1["Feats"]["Settings"]["FrightfulShape"]["Enabled"];
            } catch (Exception ex) {
                Main.Log(ex.Message);
                return false;
            }
        }
    }
}
