using HarmonyLib;
using ExpandedContent;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.RacialArchetypes {
    internal class Cruoromancer {


                public static void AllowCruoromancerArchetype() {

                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("CruoromancerArchetype")) { return; }
                    var CruoromancerArchetype = Resources.GetBlueprint<BlueprintArchetype>("8789dcfc6f8e4fe49d80b90472ea6993");
                    CruoromancerArchetype.RemoveComponents<PrerequisiteFeature>();
                    
                   

                }
                
            }
        }
     
        
    
