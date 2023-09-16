using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using ExpandedContent.Extensions;

namespace ExpandedContent.Tweaks.Archetypes {
    //UI cleanup for PoB
    internal class PriestOfBalance
    {
                public static void PatchPriestOfBalanceArchetype()
        {
            var PriestOfBalanceArchetype = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            PriestOfBalanceArchetype.RemoveComponents<PrerequisiteFeaturesFromList>();
           
        }
    }
}