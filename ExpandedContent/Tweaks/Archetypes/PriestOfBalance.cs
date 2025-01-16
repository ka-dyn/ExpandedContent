using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints;

namespace ExpandedContent.Tweaks.Archetypes {
    
    internal class PriestOfBalance {
        public static void PatchPriestOfBalanceArchetype()
        {
            Main.Log("UI cleanup for PoB");
            var PriestOfBalanceArchetype = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            //PriestOfBalanceArchetype.RemoveComponents<PrerequisiteFeaturesFromList>();
            PriestOfBalanceArchetype.GetComponent<PrerequisiteFeaturesFromList>().HideInUI = true;
           
        }
    }
}