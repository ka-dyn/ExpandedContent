using HarmonyLib;
using kadynsWOTRMods;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Config;

namespace kadynsWOTRMods.Tweaks.RacialArchetypes {
    internal class Cruoromancer {


                public static void AllowCruoromancerArchetype() {

                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("CruoromancerArchetype")) { return; }
                    var CruoromancerArchetype = Resources.GetBlueprint<BlueprintArchetype>("8789dcfc6f8e4fe49d80b90472ea6993");
                    CruoromancerArchetype.RemoveComponents<PrerequisiteFeature>();
                    
                   

                }
                
            }
        }
     
        
    
