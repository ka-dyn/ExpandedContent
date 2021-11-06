using HarmonyLib;
using kadynsWOTRMods;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Config;

namespace kadynsWOTRMods.Tweaks.RacialArchetypes {
    internal class ReformedFiend {

       
                
        public static void AllowReformedFiendArchetype() {


                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("ReformedFiendArchetype")) { return; }
                    BlueprintArchetype ReformedFiendArchetype = Resources.GetBlueprint<BlueprintArchetype>("d55163eed9214367820654f0ebe0ff69");
                    ReformedFiendArchetype.RemoveComponents<PrerequisiteFeature>();
               




                
        }

            
    }
        
}

    
