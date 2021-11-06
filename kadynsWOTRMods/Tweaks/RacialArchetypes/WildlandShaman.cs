using HarmonyLib;
using kadynsWOTRMods;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Config;

namespace kadynsWOTRMods.Tweaks.RacialArchetypes {
    internal class WildlandShaman {

        
                
        public static void AllowWildlandShamanArchetype() {


                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("WildlandShamanArchetype")) { return; }
                    BlueprintArchetype WildlandShamanArchetype = Resources.GetBlueprint<BlueprintArchetype>("bb14b164c2ce4e2bb05434a3218ff73d");
                    WildlandShamanArchetype.RemoveComponents<PrerequisiteFeature>();
         




                
        }

            
    }
        
}

    
