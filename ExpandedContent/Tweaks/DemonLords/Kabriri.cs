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

namespace ExpandedContent.Tweaks.DemonLords {
    internal class Kabriri {


        
                
        public static void AddKabriri() {

            var KabririIcon = AssetLoader.LoadInternal("Deities", "Icon_Kabriri.jpg");
            var KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");
            var DemonDomainChaosAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowed");
            var DemonDomainEvilAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowed");
            var UndeadDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("UndeadDomainAllowed");


            KabririFeature.m_Icon = KabririIcon;
                    KabririFeature.RemoveComponents<PrerequisiteNoFeature>();
            KabririFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            KabririFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            KabririFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { UndeadDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });


        }
                
            
    }
        
}
    
  