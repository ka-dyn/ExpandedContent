using HarmonyLib;
using kadynsWOTRMods.Config;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.DemonLords
{
    internal class Areshkegal
    {



        public static void AddAreshkegal()
        {

            var AreshkagalIcon = AssetLoader.LoadInternal("Deities", "Icon_Areshkagal.png");
            var AreshkegalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
            AreshkegalFeature.m_Icon = AreshkagalIcon;
            if (ModSettings.AddedContent.DemonLords.IsDisabled("Areshkegal")) { return; }
            AreshkegalFeature.RemoveComponents<PrerequisiteNoFeature>();

        }


    }

}

