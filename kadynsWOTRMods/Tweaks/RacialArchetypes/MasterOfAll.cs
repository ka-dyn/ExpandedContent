using HarmonyLib;
using kadynsWOTRMods;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Config;

namespace kadynsWOTRMods.Tweaks.RacialArchetypes {
    internal class MasterOfAll {




        public static void AllowMasterOfAllArchetype() {


            if (ModSettings.AddedContent.RacialArchetypes.IsDisabled("MasterOfAllArchetype")) { return; }
            BlueprintArchetype MasterOfAllArchetype = Resources.GetBlueprint<BlueprintArchetype>("bd4e70bfb89a452b876713d61b9b8eb2");
            MasterOfAllArchetype.RemoveComponents<PrerequisiteFeature>();
            



        }

    }
}

    
