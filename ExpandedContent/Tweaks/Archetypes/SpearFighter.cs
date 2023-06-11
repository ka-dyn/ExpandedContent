using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UI.GenericSlot;
using Kingmaker.Blueprints.Items.Ecnchantments;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using ExpandedContent.Tweaks.Components;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class SpearFighter {
        public static void AddSpearFighter() {

            var FighterClass = Resources.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var FighterProficiencies = Resources.GetBlueprint<BlueprintFeature>("a23591cc77086494ba20880f87e73970");
            var BraveryFeature = Resources.GetBlueprint<BlueprintFeature>("f6388946f9f472f4585591b80e9f2452");
            var ArmorTrainingFeature = Resources.GetBlueprint<BlueprintFeature>("3c380607706f209499d951b29d3c44f3");
            var WeaponTrainingSpears = Resources.GetBlueprint<BlueprintFeature>("d5c04077fc063e44784384a00377b7cf");
            var WeaponTrainingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b8cecf4e5e464ad41b79d5b42b76b399");
            var WeaponTrainingRankUpSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5f3cc7b9a46b880448275763fe70c0b0");
            var ArmorMasteryFeature = Resources.GetBlueprint<BlueprintFeature>("ae177f17cfb45264291d4d7c2cb64671");
            var DefensiveSpinBuff = Resources.GetBlueprint<BlueprintBuff>("3e0146b786c064441834fc025d09a67c");

            var LightArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("6d3728d4e9c9898458fe5e9532951132");
            var MediumArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("46f4fb320f35704488ba3d513397789d");
            var SimpleWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("e70ecf1ed95ca2f40b754f1adb22bbdd");
            var MartialWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629");
            var ShieldsProficiency = Resources.GetBlueprint<BlueprintFeature>("cb8686e7357a68c42bdd9d4e65334633");
            var DodgeFeature = Resources.GetBlueprint<BlueprintFeature>("97e216dbb46ae3c4faef90cf6bbe6fd5");

            var SpearParryIcon = AssetLoader.LoadInternal("Skills", "Icon_SpearParry.png");


            var SpearFighterArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("SpearFighterArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"SpearFighterArchetype.Name", "Spear Fighter");
                bp.LocalizedDescription = Helpers.CreateString($"SpearFighterArchetype.Description", "The spear is one of the oldest weapons known to most humanoid races, " +
                    "and no weapon has seen as much use. Many martial arts traditions consider the spear to be the ultimate weapon.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"SpearFighterArchetype.Description", "The spear is one of the oldest weapons known to most humanoid " +
                    "races, and no weapon has seen as much use. Many martial arts traditions consider the spear to be the ultimate weapon.");

            });

            var SpearFighterProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("SpearFighterProficiencies", bp => {
                bp.SetName("Spear Fighter Proficiencies");
                bp.SetDescription("A spear fighter is proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with light and medium armor, and with shields (excluding tower shields). " +
                    "\nHe gains dodge as a bonus feat, even if he doesn’t fulfill the prerequisites.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LightArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        MediumArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        SimpleWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        MartialWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        ShieldsProficiency.ToReference<BlueprintUnitFactReference>(),
                        DodgeFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var BalancedStrideFeature = Helpers.CreateBlueprint<BlueprintFeature>("BalancedStrideFeature", bp => {
                bp.SetName("Balanced Stride");
                bp.SetDescription("At 2nd level, the spear fighter gains a +1 AC bonus on checks to avoid attacks of opportunity. This bonus increases by 1 for every 4 levels he has beyond 2nd.");
                bp.AddComponent<ACBonusAgainstAttackOfOpportunity>(c => {
                    c.NotAttackOfOpportunity = false;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 2;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] { FighterClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                

            });

            var SpearParryPenaltyBuff = Helpers.CreateBuff("SpearParryPenaltyBuff", bp => {
                bp.SetName("Spear Parry Penalty");
                bp.SetDescription("When an incoming attack is successfully parried using the spear parry feature gain a cumulative –4 penalty on " +
                    "all attacks (including further parry attempts) for a full round. At 7th, 11th, and 15th levels, the penalty decreases by 1. At 19th level, a spear fighter can use " +
                    "spear parry without penalty.");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };                    
                    c.Multiplier = -1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { FighterClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 6, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 10, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 18, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 0 }
                    };
                });
                bp.m_Icon = DefensiveSpinBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Stack;
            });

            var SpearParryBuff = Helpers.CreateBuff("SpearParryBuff", bp => {
                bp.SetName("Spear Parry");
                bp.SetDescription("When attacked in melee, the spear fighter makes an attack roll as if he were making an attack of opportunity; for each size category the attacking creature is " +
                    "larger than the spear fighter, he takes a –2 penalty on this roll. If his result is greater than the attacking creature’s result, the creature’s attack automatically misses. " +
                    "Upon performing a successful parry, he makes an attack of opportunity against the creature whose attack he parried, provided that creature is within his reach and the spear " +
                    "fighter has remaining attacks of opportunity." +
                    "\nWhen an incoming attack is successfully parried gain a cumulative –4 penalty on all attacks (including further parry attempts) for a full round. " +
                    "At 7th, 11th, and 15th levels, the penalty decreases by 1. At 19th level, a spear fighter can use spear parry without penalty.");
                bp.AddComponent<SpearParry>(c => {
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = SpearParryPenaltyBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = false
                            },
                            AsChild = false,
                            IsNotDispelable = true,
                            DurationSeconds = 0
                        }
                        );
                });
                bp.m_Icon = SpearParryIcon;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });

            var SpearParryActivatableAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("SpearParryActivatableAbility", bp => {
                bp.SetName("Spear Parry");
                bp.SetDescription("When attacked in melee, the spear fighter makes an attack roll as if he were making an attack of opportunity; for each size category the attacking creature is " +
                    "larger than the spear fighter, he takes a –2 penalty on this roll. If his result is greater than the attacking creature’s result, the creature’s attack automatically misses. " +
                    "Upon performing a successful parry, he makes an attack of opportunity against the creature whose attack he parried, provided that creature is within his reach and the spear " +
                    "fighter has remaining attacks of opportunity." +
                    "\nWhen an incoming attack is successfully parried gain a cumulative –4 penalty on all attacks (including further parry attempts) for a full round. " +
                    "At 7th, 11th, and 15th levels, the penalty decreases by 1. At 19th level, a spear fighter can use spear parry without penalty.");
                bp.m_Icon = SpearParryIcon;
                bp.m_Buff = SpearParryBuff.ToReference<BlueprintBuffReference>();
                bp.Group = ActivatableAbilityGroup.None;
                bp.DeactivateIfCombatEnded = false;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
            });

            var SpearParryFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpearParryFeature", bp => {
                bp.SetName("Spear Parry");
                bp.SetDescription("At 3rd level, a spear fighter learns to parry his opponents’ attacks with his spear. When attacked in melee, the spear fighter makes an attack roll as " +
                    "if he were making an attack of opportunity; for each size category the attacking creature is larger than the spear fighter, he takes a –2 penalty on this roll. If his result is greater than " +
                    "the attacking creature’s result, the creature’s attack automatically misses. Upon performing a successful parry, he makes an attack of opportunity against the creature whose " +
                    "attack he parried, provided that creature is within his reach and the spear fighter has remaining attacks of opportunity." +
                    "\nWhen an incoming attack is successfully parried gain a cumulative –4 penalty on all attacks (including further parry attempts) for a full round. " +
                    "At 7th, 11th, and 15th levels, the penalty decreases by 1. At 19th level, a spear fighter can use spear parry without penalty.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpearParryActivatableAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });


            SpearFighterArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, FighterProficiencies),
                    Helpers.LevelEntry(2, BraveryFeature),
                    Helpers.LevelEntry(3, ArmorTrainingFeature),
                    Helpers.LevelEntry(5, WeaponTrainingSelection),
                    Helpers.LevelEntry(6, BraveryFeature),
                    Helpers.LevelEntry(7, ArmorTrainingFeature),
                    Helpers.LevelEntry(10, BraveryFeature),
                    Helpers.LevelEntry(11, ArmorTrainingFeature),
                    Helpers.LevelEntry(14, BraveryFeature),
                    Helpers.LevelEntry(15, ArmorTrainingFeature),
                    Helpers.LevelEntry(18, BraveryFeature),
                    Helpers.LevelEntry(19, ArmorMasteryFeature)
            };
            SpearFighterArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, SpearFighterProficiencies),
                    Helpers.LevelEntry(2, BalancedStrideFeature),
                    Helpers.LevelEntry(3, SpearParryFeature),
                    Helpers.LevelEntry(5, WeaponTrainingSpears)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Spear Fighter")) { return; }
            FighterClass.m_Archetypes = FighterClass.m_Archetypes.AppendToArray(SpearFighterArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
