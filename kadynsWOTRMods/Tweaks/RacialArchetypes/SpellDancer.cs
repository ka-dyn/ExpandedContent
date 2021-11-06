using HarmonyLib;
using kadynsWOTRMods;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Config;

namespace kadynsWOTRMods.Tweaks.RacialArchetypes {
    internal class SpellDancer {

        
                
        public static void AllowSpellDancerArchetype() {


                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("SpellDancerArchetype")) { return; }
                    BlueprintArchetype SpellDancerArchetype = Resources.GetBlueprint<BlueprintArchetype>("1125145639129cf45b6b9b674cbd62b1");
                    SpellDancerArchetype.RemoveComponents<PrerequisiteFeature>();
       




                
        }

            
    }
        
}

    
