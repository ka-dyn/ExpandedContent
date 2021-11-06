using HarmonyLib;
using kadynsWOTRMods.Config;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Deities {
    internal class Kabriri {


        
                
        public static void AddKabriri() {

                    var KabririIcon = AssetLoader.LoadInternal("Deities", "Icon_Kabriri.png");
                    var KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");
                    KabririFeature.m_Icon = KabririIcon;
                    if (ModSettings.AddedContent.Deities.IsDisabled("Kabriri")) { return; }
                    KabririFeature.RemoveComponents<PrerequisiteNoFeature>();

                
        }
                
            
    }
        
}
    
  