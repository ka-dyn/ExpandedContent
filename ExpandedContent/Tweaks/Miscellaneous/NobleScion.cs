using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Designers.Mechanics.Facts;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class NobleScion {
        public static void AddNobleScion() {










            var NobleScionFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("NobleScionFeatureSelection", bp => {
                bp.SetName("Noble Scion");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks. \nWhen you select this feat, choose one of the benefits that matches the flavor of your noble family.");
                bp.AddComponent<PrerequisiteCharacterIsFirstLevel>(c => { c.HideInUI = true; });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Stat = StatType.Charisma;
                    c.Value = 13;                    
                });                
                bp.m_Features = new BlueprintFeatureReference[] { 

                };
                bp.m_AllFeatures = new BlueprintFeatureReference[] {

                };
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Feat,
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
            });

            if (ModSettings.AddedContent.Feats.IsDisabled("Noble Scion")) { return; }
            FeatTools.AddAsFeat(NobleScionFeatureSelection);
        }
    }
}
