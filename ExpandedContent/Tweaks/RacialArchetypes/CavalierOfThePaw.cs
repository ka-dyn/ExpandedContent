using HarmonyLib;
using ExpandedContent;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using ExpandedContent.Extensions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.RacialArchetypes {
    internal class CavalierOfThePaw {



        public static void AllowCavalierOfThePaw() {


            if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("CavalierOfThePawArchetype")) { return; }
            BlueprintArchetype CavalierOfThePawArchetype = Resources.GetBlueprint<BlueprintArchetype>("8d95dc9edd5740aeadb5906198a9925a");
            CavalierOfThePawArchetype.RemoveComponents<PrerequisiteFeature>();
            


        }
    }

}


