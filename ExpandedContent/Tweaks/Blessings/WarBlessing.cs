using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.UI.ServiceWindow;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic.ActivatableAbilities;
using TabletopTweaks.Core.NewComponents;

namespace ExpandedContent.Tweaks.Blessings {
    internal class WarBlessing {//Retired post DLC6
        public static void AddWarBlessing() {

            var WarDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("3795653d6d3b291418164b27be88cb43");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestAspectOfWarBuff = Resources.GetBlueprint<BlueprintBuff>("27d14b07b52c2df42a4dcd6bfb840425");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var ViciousEnchantment = Resources.GetBlueprintReference<BlueprintItemEnchantmentReference>("a1455a289da208144981e4b1ef92cc56");
            var DivineFavor = Resources.GetBlueprint<BlueprintAbility>("9d5d2d3ffdd73c648af3eb3e585b1113");
            var ViciousIcon = AssetLoader.LoadInternal("Skills", "Icon_Vicious.png");

            var WarBlessingMajorBuff = Helpers.CreateBuff("WarBlessingMajorBuff", bp => {
                bp.SetName("Battle Lust");
                bp.SetDescription("All attacks are treated as if you had the vicious weapon special ability. In addition, you receive a +4 insight bonus on attack rolls made to " +
                    "confirm critical hits. These benefits last for 1 minute.");
                bp.m_Icon = ViciousIcon;
                bp.AddComponent<CriticalConfirmationBonus>(c => {
                    c.Value = 4;
                    c.CheckWeaponRangeType = false;
                    c.Type = WeaponRangeType.Melee;
                });
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = ViciousEnchantment;
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = ViciousEnchantment;
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var WarBlessingMajorAbility = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMajorAbility", bp => {
                bp.SetName("Battle Lust");
                bp.SetDescription("At 10th level, you can touch an ally and grant it a thirst for battle. All of the ally’s melee attacks are treated as if they had the " +
                    "vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm critical hits. These benefits last " +
                    "for 1 minute.");
                bp.m_Icon = ViciousIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMajorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMajorAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMajorFeature = Helpers.CreateBlueprint<BlueprintFeature>("WarBlessingMajorFeature", bp => {
                bp.SetName("Battle Lust");
                bp.SetDescription("At 10th level, you can touch an ally and grant it a thirst for battle. All of the ally’s melee attacks are treated as if they had the " +
                    "vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm critical hits. These benefits last " +
                    "for 1 minute.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WarBlessingMajorAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInCharacterSheetAndLevelUp = true;
            });

            var WarBlessingMinorBuffSpeed = Helpers.CreateBuff("WarBlessingMinorBuffSpeed", bp => {
                bp.SetName("War Mind - Speed");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +10 feet to base speed for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });                
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var WarBlessingMinorBuffAC = Helpers.CreateBuff("WarBlessingMinorBuffAC", bp => {
                bp.SetName("War Mind - AC");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 dodge bonus to AC for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.Stat = StatType.AC;
                    c.Value = 1;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var WarBlessingMinorBuffAttack = Helpers.CreateBuff("WarBlessingMinorBuffAttack", bp => {
                bp.SetName("War Mind - Attack");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 insight bonus on attack rolls for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = 1;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var WarBlessingMinorBuffSaves = Helpers.CreateBuff("WarBlessingMinorBuffSaves", bp => {
                bp.SetName("War Mind - Saves");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 luck bonus on saving throws for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<BuffAllSavesBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Value = 1;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var WarBlessingMinorAbilityBase = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilityBase", bp => {
                bp.SetName("War Mind");
                bp.SetDescription("At 1st level, you can touch an ally and grant it a tactical advantage for 1 minute. The ally gets one of the following bonuses: +10 feet to base speed, +1 " +
                    "dodge bonus to AC, +1 insight bonus on attack rolls, or a +1 luck bonus on saving throws. An ally can only benefit from one of these bonuses at a time.");
                bp.m_Icon = DivineFavor.Icon;
                //Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilityBase.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMinorAbilitySpeed = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySpeed", bp => {
                bp.SetName("War Mind - Speed");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +10 feet to base speed for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilitySpeed.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMinorAbilityAC = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAC", bp => {
                bp.SetName("War Mind - AC");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 dodge bonus to AC for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilityAC.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMinorAbilityAttack = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAttack", bp => {
                bp.SetName("War Mind - Attack");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 insight bonus on attack rolls for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilityAttack.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMinorAbilitySaves = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySaves", bp => {
                bp.SetName("War Mind - Saves");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 luck bonus on saving throws for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilitySaves.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            

            var WarBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("WarBlessingFeature", bp => {

                bp.LazyLock();

                bp.SetName("War");
                bp.SetDescription("At 1st level, you can touch an ally and grant it a tactical advantage for 1 minute. The ally gets one of the following bonuses: +10 feet to base speed, +1 " +
                    "dodge bonus to AC, +1 insight bonus on attack rolls, or a +1 luck bonus on saving throws. \nAt 10th level, you can touch an ally and grant it a thirst for battle. All of " +
                    "the ally’s melee attacks are treated as if they had the vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm " +
                    "critical hits. These benefits last for 1 minute.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WarBlessingMinorAbilityBase.ToReference<BlueprintUnitFactReference>() };
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

            BlessingTools.RegisterBlessing(WarBlessingFeature);
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerWarBlessingFeature", WarBlessingFeature, "At 1st level, you can touch an ally and grant it a tactical advantage for 1 minute. The ally gets one of the following bonuses: +10 feet to base speed, +1 dodge bonus to AC, +1 insight bonus on attack rolls, or a +1 luck bonus on saving throws. \nAt 13th level, you can touch an ally and grant it a thirst for battle. All of the ally’s melee attacks are treated as if they had the vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm critical hits. These benefits last for 1 minute.");

            //Added in ModSupport
            var DivineTrackerWarBlessingFeature = Resources.GetModBlueprint<BlueprintFeature>("DivineTrackerWarBlessingFeature");
            DivineTrackerWarBlessingFeature.LazyLock();
            var QuickenBlessingWarFeature = Helpers.CreateBlueprint<BlueprintFeature>("QuickenBlessingWarFeature", bp => {

                bp.LazyLock();

                bp.SetName("Quicken Blessing — War");
                bp.SetDescription("Choose one of your blessings that normally requires a standard action to use. You can expend two of your daily uses of blessings " +
                    "to deliver that blessing (regardless of whether it’s a minor or major effect) as a swift action instead.");
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Feat };
                bp.AddComponent<AbilityActionTypeConversion>(c => {
                    c.m_Abilities = new BlueprintAbilityReference[] {
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMajorAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAC").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAttack").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityBase").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySaves").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySpeed").ToReference<BlueprintAbilityReference>()
                    };
                    c.ResourceMultiplier = 2;
                    c.ActionType = UnitCommand.CommandType.Swift;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.Amount = 1;
                    c.m_Features = new BlueprintFeatureReference[] {
                        WarBlessingFeature.ToReference<BlueprintFeatureReference>(),
                        DivineTrackerWarBlessingFeature.ToReference<BlueprintFeatureReference>()
                    };
                });
            });
            WarBlessingFeature.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { QuickenBlessingWarFeature.ToReference<BlueprintFeatureReference>() };

            var WarBlessingMinorAbilitySpeedSwift = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySpeedSwift", bp => {
                bp.SetName("War Mind - Speed");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +10 feet to base speed for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>(),
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = QuickenBlessingWarFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilitySpeedSwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMinorAbilityACSwift = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilityACSwift", bp => {
                bp.SetName("War Mind - AC");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 dodge bonus to AC for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>(),
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = QuickenBlessingWarFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilityACSwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMinorAbilityAttackSwift = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAttackSwift", bp => {
                bp.SetName("War Mind - Attack");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 insight bonus on attack rolls for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>(),
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = QuickenBlessingWarFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilityAttackSwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WarBlessingMinorAbilitySavesSwift = Helpers.CreateBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySavesSwift", bp => {
                bp.SetName("War Mind - Saves");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 luck bonus on saving throws for 1 minute.");
                bp.m_Icon = DivineFavor.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WarBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>(),
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = QuickenBlessingWarFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = Helpers.CreateString("WarBlessingMinorAbilitySavesSwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            //Written later to add the quick versions
            WarBlessingMinorAbilityBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WarBlessingMinorAbilitySpeed.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilityAC.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilityAttack.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilitySaves.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilitySpeedSwift.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilityACSwift.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilityAttackSwift.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilitySavesSwift.ToReference<BlueprintAbilityReference>()
                };
            });

        }
    }
}
