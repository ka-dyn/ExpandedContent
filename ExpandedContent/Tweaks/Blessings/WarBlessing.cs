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

namespace ExpandedContent.Tweaks.Blessings {
    internal class WarBlessing {
        public static void AddWarBlessing() {

            var WarDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("3795653d6d3b291418164b27be88cb43");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var ViciousEnchantment = Resources.GetBlueprintReference<BlueprintItemEnchantmentReference>("a1455a289da208144981e4b1ef92cc56");
            var ShamanBattleSpiritAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("7eda685d53423de4281d8bc0f1197442");
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
                    "dodge bonus to AC, +1 insight bonus on attack rolls, or a +1 luck bonus on saving throws.");
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
                //Variants added later
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
                bp.m_Icon = ShamanBattleSpiritAbility.Icon;
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
            WarBlessingMinorAbilityBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WarBlessingMinorAbilitySpeed.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilityAC.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilityAttack.ToReference<BlueprintAbilityReference>(),
                    WarBlessingMinorAbilitySaves.ToReference<BlueprintAbilityReference>()
                };
            });

            var WarBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("WarBlessingFeature", bp => {
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

        }
    }
}
