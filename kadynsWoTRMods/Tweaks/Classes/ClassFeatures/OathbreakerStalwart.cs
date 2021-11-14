using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeatures
{
    internal class OathbreakerStalwart
    {


        public static void AddStalwartFeature()
        {
            var StalwartIcon = AssetLoader.LoadInternal("Skills", "Icon_Stalwart.png");
            var Stalwart = Resources.GetBlueprint<BlueprintFeature>("ec9dbc9a5fa26e446a54fe5df6779088");
            Stalwart.m_Icon = StalwartIcon;
            var OathbreakerStalwart = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakerStalwart", bp =>
            {
                bp.SetName("Stalwart");
                bp.SetDescription("At 14th level, an Oathbreaker gains Stalwart, as per the Inquisitor class feature, except she can also benefit from this ability while wearing heavy armor.");
                bp.m_Icon = StalwartIcon;
                bp.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[] { Stalwart.ToReference<BlueprintUnitFactReference>() };
                });
            });
        }
    }
}
