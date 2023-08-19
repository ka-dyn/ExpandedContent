using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.RacialArchetypes {
    internal class PhantasmalMage {

        


                public static void AllowPhantasmalMageArchetype() {

                    if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("PhantasmalMageArchetype")) { return; }
                    BlueprintArchetype PhantasmalMageArchetype = Resources.GetBlueprint<BlueprintArchetype>("e9d0ee69305049fe8400a066010dbcd1");
                    PhantasmalMageArchetype.RemoveComponents<PrerequisiteFeature>();
                  



                }

            
    }
       
}
    

