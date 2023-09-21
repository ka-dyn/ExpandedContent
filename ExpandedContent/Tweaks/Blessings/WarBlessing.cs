using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Blessings {
    internal class WarBlessing {
        public static void AddWarBlessing() {

            var WarDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("3795653d6d3b291418164b27be88cb43");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");




            var WarBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("WarBlessingFeature", bp => {
                bp.SetName("War");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WarBlessingMinorAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WarpriestClass;
                    c.Level = 10;
                    c.m_Feature = WarBlessingMajorFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.CheckInProgression = true;
                    c.HideInUI = true;
                    c.m_Feature = WarDomainAllowed;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WarpriestBlessing };
            });
        }
    }
}
