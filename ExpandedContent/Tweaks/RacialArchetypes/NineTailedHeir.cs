using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.RacialArchetypes {
    internal class NineTailedHeir {

        
                
        public static void AllowNineTailedHeirArchetype() {


                    
            if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("NineTailedHeirArchetype")) { return; }
                    
            BlueprintArchetype NineTailedHeirArchetype = Resources.GetBlueprint<BlueprintArchetype>("65a630aa291f65047b90a2af5df75d83");
                    
            NineTailedHeirArchetype.RemoveComponents<PrerequisiteFeature>();
                    
            




                
        }

           
    }
        
}

    
