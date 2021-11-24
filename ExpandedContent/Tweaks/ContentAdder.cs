using HarmonyLib;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;

namespace ExpandedContent.Tweaks
{
    class ContentAdder {




        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;

             
                Archetypes.LivingScripture.AddLivingScripture();

                RacialArchetypes.Cruoromancer.AllowCruoromancerArchetype();
                RacialArchetypes.CavalierOfThePaw.AllowCavalierOfThePaw();
                RacialArchetypes.Imitator.AllowImitatorArchetype();
                RacialArchetypes.MasterOfAll.AllowMasterOfAllArchetype();
                RacialArchetypes.NineTailedHeir.AllowNineTailedHeirArchetype();
                RacialArchetypes.Purifier.AllowPurifierArchetype();
                RacialArchetypes.ReformedFiend.AllowReformedFiendArchetype();
                RacialArchetypes.SpellDancer.AllowSpellDancerArchetype();
                RacialArchetypes.Stonelord.AllowStonelordArchetype();
                RacialArchetypes.StudentOfStone.AllowStudentOfStoneArchetype();
                RacialArchetypes.WildlandShaman.AllowWildlandShamanArchetype();
                RacialArchetypes.PhantasmalMage.AllowPhantasmalMageArchetype();
        
                Backgrounds.ArchdukeOfCheliax.AddBackgroundArchdukeOfCheliax();
                
                

                Archdevils.Dispater.AddDispater();
                Archdevils.Mephistopheles.AddMephistopheles();

                DemonLords.Areshkegal.AddAreshkegal();
                DemonLords.Deskari.AddDeskari();
                DemonLords.Kabriri.AddKabriri();
                DemonLords.Baphomet.AddBaphomet();
                DemonLords.Zura.AddZura();




                Deities.Apsu.AddApsu();

                Deities.Dahak.AddDahakFeature();
                Deities.Daikitsu.AddDaikitsuFeature();
                Deities.Wukong.AddWukongFeature();
                Deities.Fumeiyoshi.AddFumeiyoshiFeature();
                Deities.GreenFaith.AddGreenFaith();
               
                Deities.PatchPulura.AddPulura();
                Deities.MilaniSacredWeaponFeature.AddMilaniSacredWeaponFeature();
                Deities.Ragathiel.AddRagathielFeature();
                Deities.Arshea.AddArsheaFeature();
                Deities.Milani.AddMilaniFeature();
                Deities.DeitySelectionFeature.PatchDeitySelection();
                Deities.PatchLichDeity.AddLichDeity();







            }
        }
    }
}
