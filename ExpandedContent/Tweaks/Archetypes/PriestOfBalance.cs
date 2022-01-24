using HarmonyLib;
using ExpandedContent;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using System;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Archetypes
{
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