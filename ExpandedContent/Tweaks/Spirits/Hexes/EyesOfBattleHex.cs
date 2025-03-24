using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spirits.Hexes
{
    internal class EyesOfBattleHex {
        public static void AddEyesOfBattleHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");






            var ShamanHexEyesOfBattleResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHexEyesOfBattleResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    LevelIncrease = 1,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 1,
                    StartingIncrease = 1,
                    LevelStep = 0,
                    PerStepIncrease = 1,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    }
                };
                bp.m_UseMax = true;
                bp.m_Max = 20;
            });










            var ShamanHexEyesOfBattleFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexEyesOfBattleFeature", bp => {
                bp.SetName("Eyes of Battle");
                bp.SetDescription("The shaman’s senses become magically heightened in the heat of battle. As a swift action, she can grant herself " +
                    "a +10 insight bonus for 1 round on Perception checks made to notice and pinpoint invisible creatures within 30 feet. She can " +
                    "instead use this ability as a swift action to ignore the affects of cover or partial cover (but not total cover) on her next " +
                    "attack, as long as that attack is made before the end of her next turn. The shaman can use this ability a number of times " +
                    "per day equal to her shaman level.");
                bp.m_Icon = MageLightBuff.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexEyesOfBattleAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanHexEyesOfBattleResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBattleSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBattleSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBattleSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexEyesOfBattleFeature);
        }
    }
}
