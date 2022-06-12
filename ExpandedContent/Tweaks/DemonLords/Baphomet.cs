using HarmonyLib;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints;

namespace ExpandedContent.Tweaks.DemonLords
{
    internal class Baphomet
    {




        public static void AddBaphomet()
        {

            var BaphometIcon = AssetLoader.LoadInternal("Deities", "Icon_Baphomet.jpg");
            var BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
            var DemonDomainChaosAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowed");
            var DemonDomainEvilAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowed");

            BaphometFeature.m_Icon = BaphometIcon;
            BaphometFeature.RemoveComponents<PrerequisiteNoFeature>();
            BaphometFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            BaphometFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
            });
        }


    }

}


