using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Deities {
    internal class MilaniSacredWeaponFeature {

        public static void AddMilaniSacredWeaponFeature() {

            var AsmodeusSacredWeaponFeature = Resources.GetBlueprint<BlueprintFeature>("2eacfd16f6bc0b445813f28473d3e6ba");

            var MilaniSacredWeaponFeature = Helpers.CreateBlueprint<BlueprintFeature>("MilaniSacredWeaponFeature", (bp => {


                bp.SetName("Milani's Sacred Weapon");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { AsmodeusSacredWeaponFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
            }));
            

        }
    }
}
