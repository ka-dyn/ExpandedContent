using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.RacialArchetypes {
    internal class Imitator {

        public static void AllowImitatorArchetype() {


                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("ImitatorArchetype")) { return; }
                    var ImitatorArchetype = Resources.GetBlueprint<BlueprintArchetype>("5947427f10bc497dbc24f66129b43e5d");
                    ImitatorArchetype.RemoveComponents<PrerequisiteFeature>();
                    



                
        }

            
    }
}

    
