using HarmonyLib;
using ExpandedContent;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.RacialArchetypes {
    internal class StudentOfStone {

        
                
        public static void AllowStudentOfStoneArchetype() {



                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("StudentOfStoneArchetype")) { return; }
                    BlueprintArchetype StudentOfStoneArchetype = Resources.GetBlueprint<BlueprintArchetype>("3b81e57a75299b74ab6b144e830864e9");
                    StudentOfStoneArchetype.RemoveComponents<PrerequisiteFeature>();
                



                
        }

            
    }
        
}

    
