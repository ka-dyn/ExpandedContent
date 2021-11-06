using HarmonyLib;
using kadynsWOTRMods.Config;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Deities {
    internal class PatchPulura {


        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        

                
        public static void AddPulura() {

                    var PuluraIcon = AssetLoader.LoadInternal("Deities", "Icon_Pulura.jpg");


                    var PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");

                    PuluraFeature.RemoveComponents<PrerequisiteAlignment>();

                    PuluraFeature.AddComponent<PrerequisiteAlignment>(c => {
                        c.Alignment = AlignmentMaskType.Good | AlignmentMaskType.ChaoticNeutral;

                    });
                    PuluraFeature.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[] { GoodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                    });
                    PuluraFeature.m_Icon = PuluraIcon;
                    if (ModSettings.AddedContent.Deities.IsDisabled("Pulura")) { return; }
                    PuluraFeature.RemoveComponents<PrerequisiteNoFeature>();

                
        }
                
            
    }
        
}
    

