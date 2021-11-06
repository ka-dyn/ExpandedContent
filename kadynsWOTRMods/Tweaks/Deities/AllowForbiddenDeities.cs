using HarmonyLib;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Deities {

    internal class AllowForbiddenDeities {
        private static readonly BlueprintFeature LichDeityMythicFeature = Resources.GetBlueprint<BlueprintFeature>("d633cf9ebcdc8ed4e8f2546c3e08742e");
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;


                AllowForbiddenDeities();

                static void AllowForbiddenDeities() {
                    var GreenFaithIcon = AssetLoader.LoadInternal("Deities", "Icon_GreenFaith.jpg");
                    var PuluraIcon = AssetLoader.LoadInternal("Deities", "Icon_Pulura.jpg");
                    var BaphometIcon = AssetLoader.LoadInternal("Deities", "Icon_Baphomet.jpg");
                    var DeskariIcon = AssetLoader.LoadInternal("Deities", "Icon_Deskari.png");
                    var KabririIcon = AssetLoader.LoadInternal("Deities", "Icon_Kabriri.png");
                    var AreshkagalIcon = AssetLoader.LoadInternal("Deities", "Icon_Areshkagal.png");

                    BlueprintFeature GreenFaithFeature = Resources.GetBlueprint<BlueprintFeature>("99a7a8f13c1300c42878558fa9471e2f");
                    GreenFaithFeature.SetDescription("The Green Faith is a naturalistic philosophy based on the belief that natural forces are worthy of attention and respect. Followers of the Green Faith meditate daily, " +
                        "commune with natural forms of power, and show respect to nature in all things.  Sarkoris, prior to its destruction at the hands of demons, was formerly the Green Faith's greatest bastion; " +
                        "since then, Green Faith holdouts continue their fight to protect nature from the corruption of the Worldwound. \nDomains: Air, Earth, Animal, Fire, Plant. \nFavoured Weapons: Sickle, Quarterstaff");
                    GreenFaithFeature.m_Icon = GreenFaithIcon;
                    GreenFaithFeature.RemoveComponents<PrerequisiteNoFeature>();

                    BlueprintFeature BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
                    BaphometFeature.RemoveComponents<PrerequisiteNoFeature>();
                    BaphometFeature.m_Icon = BaphometIcon;
                    
                    BlueprintFeature AreshkegalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
                    AreshkegalFeature.RemoveComponents<PrerequisiteNoFeature>();
                    AreshkegalFeature.m_Icon = AreshkagalIcon;
                    
                    BlueprintFeature DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
                    DeskariFeature.RemoveComponents<PrerequisiteNoFeature>();
                    DeskariFeature.m_Icon = DeskariIcon;
                    
                    BlueprintFeature KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");
                    KabririFeature.RemoveComponents<PrerequisiteNoFeature>();
                    KabririFeature.m_Icon = KabririIcon;
                    
                    BlueprintFeature LichDeityFeature = Resources.GetBlueprint<BlueprintFeature>("b4153b422d02d4f48b3f8f0ceb6a10eb");
                    LichDeityFeature.RemoveComponents<PrerequisiteNoFeature>();
                    LichDeityFeature.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[] { LichDeityMythicFeature.ToReference<BlueprintUnitFactReference>() };
                    });
                   
                    BlueprintFeature PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");
                    PuluraFeature.RemoveComponents<PrerequisiteNoFeature>();
                    PuluraFeature.RemoveComponents<PrerequisiteAlignment>();
                    
                    PuluraFeature.AddComponent<PrerequisiteAlignment>(c => {
                        c.Alignment = AlignmentMaskType.Good | AlignmentMaskType.ChaoticNeutral;
                        
                    });
                    PuluraFeature.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[] { GoodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                    });
                    PuluraFeature.m_Icon = PuluraIcon;

                }



                }











            }


        }



    }

    


