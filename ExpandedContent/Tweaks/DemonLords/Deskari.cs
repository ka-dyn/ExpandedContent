using HarmonyLib;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.DemonLords {
    internal class Deskari {


        
                
        public static void AddDeskari() {

                    var DeskariIcon = AssetLoader.LoadInternal("Deities", "Icon_Deskari.png");
                    var DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("ddf913858bdf43b4da3b731e082fbcc0");
                    DeskariFeature.m_Icon = DeskariIcon;
                    if (ModSettings.AddedContent.DemonLords.IsDisabled("Deskari")) { return; }
                    DeskariFeature.RemoveComponents<PrerequisiteNoFeature>();
                
        }
               
            
    }
       
}
    


