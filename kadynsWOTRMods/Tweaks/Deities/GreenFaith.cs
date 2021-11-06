using HarmonyLib;
using kadynsWOTRMods.Config;
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

    internal class GreenFaith {

        
                
        public static void AddGreenFaith() {

                    var GreenFaithIcon = AssetLoader.LoadInternal("Deities", "Icon_GreenFaith.jpg");
                    var GreenFaithFeature = Resources.GetBlueprint<BlueprintFeature>("99a7a8f13c1300c42878558fa9471e2f");
                    GreenFaithFeature.SetDescription("The Green Faith is a naturalistic philosophy based on the belief that natural forces are worthy of attention and respect. Followers of the Green Faith meditate daily, " +
                        "commune with natural forms of power, and show respect to nature in all things.  Sarkoris, prior to its destruction at the hands of demons, was formerly the Green Faith's greatest bastion; " +
                        "since then, Green Faith holdouts continue their fight to protect nature from the corruption of the Worldwound. \nDomains: Air, Earth, Animal, Fire, Plant. \nFavoured Weapons: Sickle, Quarterstaff");
                    GreenFaithFeature.m_Icon = GreenFaithIcon;
                    if (ModSettings.AddedContent.Deities.IsDisabled("GreenFaith")) { return; }
                    GreenFaithFeature.RemoveComponents<PrerequisiteNoFeature>();
                
        }
                
            
    }
        
}
    


    


