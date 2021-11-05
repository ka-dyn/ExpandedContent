using HarmonyLib;
using kadynsTweaks.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsTweaks.Tweaks.Deities {

    internal class AllowForbiddenDeities {
        private static readonly BlueprintFeature LichDeityMythicFeature = Resources.GetBlueprint<BlueprintFeature>("d633cf9ebcdc8ed4e8f2546c3e08742e");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;


                AllowForbiddenDeities();

                static void AllowForbiddenDeities() {
                    BlueprintFeature GreenFaithFeature = Resources.GetBlueprint<BlueprintFeature>("99a7a8f13c1300c42878558fa9471e2f");
                    GreenFaithFeature.RemoveComponents<PrerequisiteNoFeature>();
                    GreenFaithFeature.RemoveComponents<PrerequisiteNoClassLevel>();
                    BlueprintFeature BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
                    BaphometFeature.RemoveComponents<PrerequisiteNoFeature>();
                    BlueprintFeature AreshkegalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
                    AreshkegalFeature.RemoveComponents<PrerequisiteNoFeature>();
                    BlueprintFeature DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
                    DeskariFeature.RemoveComponents<PrerequisiteNoFeature>();
                    BlueprintFeature KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");
                    KabririFeature.RemoveComponents<PrerequisiteNoFeature>();
                    BlueprintFeature LichDeityFeature = Resources.GetBlueprint<BlueprintFeature>("b4153b422d02d4f48b3f8f0ceb6a10eb");
                    LichDeityFeature.RemoveComponents<PrerequisiteNoFeature>();
                    LichDeityFeature.RemoveComponents<PrerequisiteNoClassLevel>();
                    LichDeityFeature.RemoveComponents<PrerequisiteNoArchetype>();
                    LichDeityFeature.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[] { LichDeityMythicFeature.ToReference<BlueprintUnitFactReference>() };
                    });
                    BlueprintFeature PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");
                    PuluraFeature.RemoveComponents<PrerequisiteNoArchetype>();
                    PuluraFeature.RemoveComponents<PrerequisiteNoClassLevel>();
                    PuluraFeature.RemoveComponents<PrerequisiteNoFeature>();
                    PuluraFeature.RemoveComponents<AddStartingEquipment>();
                    PuluraFeature.RemoveComponents<ForbidSpellbookOnAlignmentDeviation>();



                }











            }


        }



    }
}
    


