using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.RacialArchetypes {
    internal class Purifier {

        
                
        public static void AllowPurifierArchetype() {


                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("PurifierArchetype")) { return; }
                    BlueprintArchetype PurifierArchetype = Resources.GetBlueprint<BlueprintArchetype>("c9df67160a77ecd4a97928f2455545d7");
                    PurifierArchetype.RemoveComponents<PrerequisiteFeature>();
                   



                
        }

            
    }
        
}

    
