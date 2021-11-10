using HarmonyLib;
using kadynsWOTRMods.Tweaks;
using Kingmaker.Blueprints.JsonSystem;

namespace kadynsWOTRMods.Tweaks
{
    class ContentAdder {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;

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





                Classes.ClassFeatures.OathbreakersBaneBuff.AddOathbreakersBaneBuff();
                Classes.ClassFeatures.OathbreakersBaneAbility.AddOathbreakersBaneResource();                
                Classes.ClassFeatures.OathbreakersBaneAbility.AddOathbreakersBaneAbility();
                Classes.ClassFeatures.OathbreakersBaneFeature.AddOathbreakersBaneFeature();
                Classes.ClassFeatures.OathbreakersDirection.AddOathbreakersDirection();
                Classes.ClassFeatures.OathbreakerSoloTactics.AddOathbreakerSoloTactics();
                Classes.ClassFeatures.OathbreakerStalwart.AddStalwartFeature();
                Classes.ClassFeatures.SpitefulTenacity.AddSpitefulTenacity();
                Classes.ClassFeatures.AuraOfSelfRighteousness.AddAuraOfSelfRighteousnessFeature();
                Classes.ClassFeatures.FadedGrace.AddFadedGrace();
                
                Classes.ClassFeatures.BreakerOfOaths.AddBreakerOfOaths();
                Classes.ClassFeatures.OathbreakerProgression.AddOathbreakerProgression();
                Classes.Oathbreaker.AddOathbreakerClass();



                Archdevils.Dispater.AddDispater();
                Archdevils.Mephistopheles.AddMephistopheles();

                DemonLords.Areshkegal.AddAreshkegal();
                DemonLords.Deskari.AddDeskari();
                DemonLords.Kabriri.AddKabriri();
                DemonLords.Baphomet.AddBaphomet();
                DemonLords.Zura.AddZura();




                Deities.Apsu.AddApsu();
                
                
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
