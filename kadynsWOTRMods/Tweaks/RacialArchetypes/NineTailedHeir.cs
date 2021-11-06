using HarmonyLib;
using kadynsWOTRMods;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Config;

namespace kadynsWOTRMods.Tweaks.RacialArchetypes {
    internal class NineTailedHeir {

        
                
        public static void AllowNineTailedHeirArchetype() {


                    
            if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("NineTailedHeirArchetype")) { return; }
                    
            BlueprintArchetype NineTailedHeirArchetype = Resources.GetBlueprint<BlueprintArchetype>("65a630aa291f65047b90a2af5df75d83");
                    
            NineTailedHeirArchetype.RemoveComponents<PrerequisiteFeature>();
                    
            




                
        }

           
    }
        
}

    
