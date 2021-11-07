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
    internal class Baphomet
    {




        public static void AddBaphomet()
        {

            var BaphometIcon = AssetLoader.LoadInternal("Deities", "Icon_Baphomet.jpg");
            var BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
            BaphometFeature.m_Icon = BaphometIcon;
            if (ModSettings.AddedContent.DemonLords.IsDisabled("Areshkegal")) { return; }
            BaphometFeature.RemoveComponents<PrerequisiteNoFeature>();

        }


    }

}


