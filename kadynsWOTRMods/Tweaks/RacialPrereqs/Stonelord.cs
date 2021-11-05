using HarmonyLib;
using kadynsWOTRMods;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using kadynsWOTRMods.Extensions;

namespace kadynsWOTRMods.Tweaks.RacialPrereqs {
    internal class AllowRacialArchetypes {

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            public  static void Postfix() {
                if (Initialized) return;
                Initialized = true;

                AllowRacialArchetypes();

           
                static void AllowRacialArchetypes() {
                    BlueprintArchetype StonelordArchetype = Resources.GetBlueprint<BlueprintArchetype>("cf0f99b3cd7444a48681b1c1c4aa2a41");
                    StonelordArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype PhantasmalMageArchetype = Resources.GetBlueprint<BlueprintArchetype>("e9d0ee69305049fe8400a066010dbcd1");
                    PhantasmalMageArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype ReformedFiendArchetype = Resources.GetBlueprint<BlueprintArchetype>("d55163eed9214367820654f0ebe0ff69");
                    ReformedFiendArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype CavalierOfThePawArchetype = Resources.GetBlueprint<BlueprintArchetype>("8d95dc9edd5740aeadb5906198a9925a");
                    CavalierOfThePawArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype SpellDancerArchetype = Resources.GetBlueprint<BlueprintArchetype>("1125145639129cf45b6b9b674cbd62b1");
                    SpellDancerArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype StudentOfStoneArchetype = Resources.GetBlueprint<BlueprintArchetype>("3b81e57a75299b74ab6b144e830864e9");
                    StudentOfStoneArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype PurifierArchetype = Resources.GetBlueprint<BlueprintArchetype>("c9df67160a77ecd4a97928f2455545d7");
                    PurifierArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype MasterOfAllArchetype = Resources.GetBlueprint<BlueprintArchetype>("bd4e70bfb89a452b876713d61b9b8eb2");
                    MasterOfAllArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype WildlandShamanArchetype = Resources.GetBlueprint<BlueprintArchetype>("bb14b164c2ce4e2bb05434a3218ff73d");
                    WildlandShamanArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype ImitatorArchetype = Resources.GetBlueprint<BlueprintArchetype>("5947427f10bc497dbc24f66129b43e5d");
                    ImitatorArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype NineTailedHeirArchetype = Resources.GetBlueprint<BlueprintArchetype>("65a630aa291f65047b90a2af5df75d83");
                    NineTailedHeirArchetype.RemoveComponents<PrerequisiteFeature>();
                    BlueprintArchetype CruoromancerArchetype = Resources.GetBlueprint<BlueprintArchetype>("8789dcfc6f8e4fe49d80b90472ea6993");
                    CruoromancerArchetype.RemoveComponents<PrerequisiteFeature>();


                }

            }
        }

        
    }
}