using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesOathbreaker
{
    internal class FadedGrace
    {
        public static void AddFadedGrace() {
            var FadedGraceIcon = AssetLoader.LoadInternal("Skills", "Icon_FadedGrace.png");
            var IronWill = Resources.GetBlueprint<BlueprintFeature>("175d1577bb6c9a04baf88eec99c66334").ToReference<BlueprintFeatureReference>();
            var GreatFortitude = Resources.GetBlueprint<BlueprintFeature>("79042cb55f030614ea29956177977c52").ToReference<BlueprintFeatureReference>();
            var LightningReflexes = Resources.GetBlueprint<BlueprintFeature>("15e7da6645a7f3d41bdad7c8c4b9de1e").ToReference<BlueprintFeatureReference>();
            var FadedGrace = Helpers.CreateBlueprint<BlueprintFeatureSelection>("FadedGrace", bp => {
                bp.SetName("Faded Grace");
                bp.SetDescription("At 2nd level, an Oathbreaker gains one of the following as a bonus feat: Great Fortitude, " +
                    "Iron Will, or Lightning Reflexes.");
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.HideNotAvailibleInUI = true;
                bp.m_Icon = FadedGraceIcon;
                bp.m_Features = new BlueprintFeatureReference[] {
                    GreatFortitude,
                    IronWill,
                    LightningReflexes
                    };
                bp.m_AllFeatures = bp.m_Features;
            });
        }
        
    }
}
