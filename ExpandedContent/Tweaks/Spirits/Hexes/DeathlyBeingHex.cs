using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spirits.Hexes {
    internal class DeathlyBeingHex {
        public static void AddDeathlyBeingHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var ShamanBonesSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("65b5e7f17a1b928418fc9e8a6b55eafa");
            var ShamanBonesSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("a3862152ae6010445bc25915ac58fc8e");
            var ShamanBonesSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("f556c44606e5eaa47bac31aae9ebc96d");

            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");

            var BoneshakerIcon = Resources.GetBlueprint<BlueprintAbility>("b7731c2b4fa1c9844a092329177be4c3").Icon;

            var ChannelPositiveHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("279447a6bf2d3544d93a0a39c3b8e91d");
            var ChannelEnergyHospitalerHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("cc17243b2185f814aa909ac6b6599eaa");
            var ChannelEnergyPaladinHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("4937473d1cfd7774a979b625fb833b47");
            var ChannelEnergyEmpyrealHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("e1536ee240c5d4141bf9f9485a665128");

            var SunDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("3d8e38c9ed54931469281ab0cec506e9");

            var ShamanHexDeathlyBeingFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexDeathlyBeingFeature", bp => {
                bp.SetName("Deathly Being");
                bp.SetDescription("If the shaman is a living creature, she reacts to positive and negative energy as if she were undead—positive energy harms her, while negative energy heals her. " +
                    "If she’s an undead creature or a creature with the negative energy affinity ability, she gains a +1 bonus to her channel resistance. " +
                    "At 8th level, she gains a +4 bonus on saves against death effects and effects that drain energy, her bonus to channel resistance increases to +2.");
                bp.m_Icon = BoneshakerIcon;
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.AddComponent<SavingThrowContextBonusAgainstSpecificSpells>(c => {
                    c.m_Spells = new BlueprintAbilityReference[] { ChannelPositiveHarm, ChannelEnergyHospitalerHarm, ChannelEnergyPaladinHarm, ChannelEnergyEmpyrealHarm };
                    c.m_BypassFeatures = new BlueprintUnitFactReference[] { SunDomainBaseFeature.ToReference<BlueprintUnitFactReference>() };
                    c.ModifierDescriptor = ModifierDescriptor.None;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 8;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_UseMax = true;
                    c.m_Max = 2;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Death | SpellDescriptor.NegativeLevel;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 0;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 0 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBonesSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBonesSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBonesSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexDeathlyBeingFeature);

            ShamanBonesSpiritProgression.IsPrerequisiteFor.Add(ShamanHexDeathlyBeingFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
