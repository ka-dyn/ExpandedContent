using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents;

namespace ExpandedContent.Tweaks.Blessings {
    internal class CommunityBlessing {
        public static void AddCommunityBlessing() {

            var CommunityDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("c87004460f3328c408d22c5ead05291f");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestAspectOfWarBuff = Resources.GetBlueprint<BlueprintBuff>("27d14b07b52c2df42a4dcd6bfb840425");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");










            var CommunityBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("CommunityBlessingFeature", bp => {
                bp.SetName("Community");
                bp.SetDescription("At 1st level, you can touch an ally and grant it the blessing of community. For the next minute, whenever " +
                    "that ally uses the aid another action, the bonus granted increases to +4. You can instead use this ability on yourself as " +
                    "a swift action. \nAt 10th level, you can rally your allies to fight together. For 1 minute, whenever you make a successful " +
                    "melee or ranged attack against a foe, allies within 10 feet of you gain a +2 insight bonus on attacks of the same type " +
                    "you made against that foe—melee attacks if you made a melee attack, or ranged attacks if you made a ranged attack. If you " +
                    "score a critical hit, this bonus increases to +4 until the start of your next turn.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CommunityBlessingMinorAbilitySelf.ToReference<BlueprintUnitFactReference>(),
                        CommunityBlessingMinorAbilityOthers.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WarpriestClass;
                    c.Level = 10;
                    c.m_Feature = CommunityBlessingMajorFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.CheckInProgression = true;
                    c.HideInUI = true;
                    c.m_Feature = CommunityDomainAllowed;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WarpriestBlessing };
            });
            BlessingTools.RegisterBlessing(CommunityBlessingFeature);
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerCommunityBlessingFeature", CommunityBlessingFeature, "At 1st level, you can touch an ally and grant it the blessing of community. For the next minute, whenever that ally uses the aid another action, the bonus granted increases to +4. You can instead use this ability on yourself as a swift action. \nAt 10th level, you can rally your allies to fight together. For 1 minute, whenever you make a successful melee or ranged attack against a foe, allies within 10 feet of you gain a +2 insight bonus on attacks of the same type you made against that foe—melee attacks if you made a melee attack, or ranged attacks if you made a ranged attack. If you score a critical hit, this bonus increases to +4 until the start of your next turn.");

            //Added in ModSupport
            var DivineTrackerCommunityBlessingFeature = Resources.GetModBlueprint<BlueprintFeature>("DivineTrackerCommunityBlessingFeature");
            var QuickenBlessingCommunityFeature = Helpers.CreateBlueprint<BlueprintFeature>("QuickenBlessingCommunityFeature", bp => {
                bp.SetName("Quicken Blessing — Community");
                bp.SetDescription("Choose one of your blessings that normally requires a standard action to use. You can expend two of your daily uses of blessings " +
                    "to deliver that blessing (regardless of whether it’s a minor or major effect) as a swift action instead.");
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Feat };
                bp.AddComponent<AbilityActionTypeConversion>(c => {
                    c.m_Abilities = new BlueprintAbilityReference[] {
                        Resources.GetModBlueprint<BlueprintAbility>("CommunityBlessingMinorAbilityOthers").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("CommunityBlessingMajorAbility").ToReference<BlueprintAbilityReference>()
                    };
                    c.ResourceMultiplier = 2;
                    c.ActionType = UnitCommand.CommandType.Swift;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.Amount = 1;
                    c.m_Features = new BlueprintFeatureReference[] {
                        CommunityBlessingFeature.ToReference<BlueprintFeatureReference>(),
                        DivineTrackerCommunityBlessingFeature.ToReference<BlueprintFeatureReference>()
                    };
                });
            });

        }
    }
}
