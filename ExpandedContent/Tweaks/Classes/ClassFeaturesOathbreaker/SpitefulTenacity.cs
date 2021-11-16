using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesOathbreaker
{
    internal class SpitefulTenacity
    {

        public static void AddSpitefulTenacity()
        {
            var SpitefulIcon = AssetLoader.LoadInternal("Skills", "Icon_SpitefulTenacity.png");
            var Diehard = Resources.GetBlueprint<BlueprintFeature>("86669ce8759f9d7478565db69b8c19ad");
            var SpitefulTenacity = Helpers.CreateBlueprint<BlueprintFeature>("SpitefulTenacity", bp =>
            {
                bp.SetName("Spiteful Tenacity");
                bp.SetDescription("At 3rd level the Oathbreaker receives the Diehard feat for free.");
                bp.m_Icon = SpitefulIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[] { Diehard.ToReference<BlueprintUnitFactReference>() };
                });
            });

        }
    }
}
