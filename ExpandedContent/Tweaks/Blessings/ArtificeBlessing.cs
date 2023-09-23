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

namespace ExpandedContent.Tweaks.Blessings {
    internal class ArtificeBlessing {
        public static void AddArtificeBlessing() {

            var ArtificeDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("3795653d6d3b291418164b27be88cb43");
            var ArtificepriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var ViciousEnchantment = Resources.GetBlueprintReference<BlueprintItemEnchantmentReference>("a1455a289da208144981e4b1ef92cc56");


            var ArtificeBlessingMajorBuff = Helpers.CreateBuff("ArtificeBlessingMajorBuff", bp => {
                bp.SetName("Battle Lust");
                bp.SetDescription("All attacks are treated as if you had the vicious weapon special ability. In addition, you receive a +4 insight bonus on attack rolls made to " +
                    "confirm critical hits. These benefits last for 1 minute.");
                bp.m_Icon = sdasd;
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
            var ArtificeBlessingMajorAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMajorAbility", bp => {
                bp.SetName("Battle Lust");
                bp.SetDescription("At 10th level, you can touch an ally and grant it a thirst for battle. All of the ally’s melee attacks are treated as if they had the " +
                    "vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm critical hits. These benefits last " +
                    "for 1 minute.");
                bp.m_Icon = sdasd;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMajorBuff.ToReference<BlueprintBuffReference>(),
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
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMajorAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArtificeBlessingMajorFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArtificeBlessingMajorFeature", bp => {
                bp.SetName("Battle Lust");
                bp.SetDescription("At 10th level, you can touch an ally and grant it a thirst for battle. All of the ally’s melee attacks are treated as if they had the " +
                    "vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm critical hits. These benefits last " +
                    "for 1 minute.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArtificeBlessingMajorAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInCharacterSheetAndLevelUp = true;
            });

            var ArtificeBlessingMinorBuffSpeed = Helpers.CreateBuff("ArtificeBlessingMinorBuffSpeed", bp => {
                bp.SetName("Artifice Mind - Speed");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +10 feet to base speed for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });                
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ArtificeBlessingMinorBuffAC = Helpers.CreateBuff("ArtificeBlessingMinorBuffAC", bp => {
                bp.SetName("Artifice Mind - AC");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 dodge bonus to AC for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.Stat = StatType.AC;
                    c.Value = 1;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ArtificeBlessingMinorBuffAttack = Helpers.CreateBuff("ArtificeBlessingMinorBuffAttack", bp => {
                bp.SetName("Artifice Mind - Attack");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 insight bonus on attack rolls for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = 1;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ArtificeBlessingMinorBuffSaves = Helpers.CreateBuff("ArtificeBlessingMinorBuffSaves", bp => {
                bp.SetName("Artifice Mind - Saves");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 luck bonus on saving throws for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<BuffAllSavesBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Value = 1;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ArtificeBlessingMinorAbilityBase = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbilityBase", bp => {
                bp.SetName("Artifice Mind");
                bp.SetDescription("At 1st level, you can touch an ally and grant it a tactical advantage for 1 minute. The ally gets one of the following bonuses: +10 feet to base speed, +1 " +
                    "dodge bonus to AC, +1 insight bonus on attack rolls, or a +1 luck bonus on saving throws.");
                bp.m_Icon = edrghbe;
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
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMinorAbilityBase.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArtificeBlessingMinorAbilitySpeed = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbilitySpeed", bp => {
                bp.SetName("Artifice Mind - Speed");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +10 feet to base speed for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>(),
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
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
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
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMinorAbilitySpeed.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArtificeBlessingMinorAbilityAC = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbilityAC", bp => {
                bp.SetName("Artifice Mind - AC");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 dodge bonus to AC for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMinorBuffAC.ToReference<BlueprintBuffReference>(),
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
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
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
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMinorAbilityAC.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArtificeBlessingMinorAbilityAttack = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbilityAttack", bp => {
                bp.SetName("Artifice Mind - Attack");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 insight bonus on attack rolls for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>(),
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
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>() }
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
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMinorAbilityAttack.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArtificeBlessingMinorAbilitySaves = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbilitySaves", bp => {
                bp.SetName("Artifice Mind - Saves");
                bp.SetDescription("At 1st level, you can touch an ally and grant it +1 luck bonus on saving throws for 1 minute.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMinorBuffSaves.ToReference<BlueprintBuffReference>(),
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
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffSpeed.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffAC.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = ArtificeBlessingMinorBuffAttack.ToReference<BlueprintBuffReference>() }
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
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMinorAbilitySaves.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            ArtificeBlessingMinorAbilityBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    ArtificeBlessingMinorAbilitySpeed.ToReference<BlueprintAbilityReference>(),
                    ArtificeBlessingMinorAbilityAC.ToReference<BlueprintAbilityReference>(),
                    ArtificeBlessingMinorAbilityAttack.ToReference<BlueprintAbilityReference>(),
                    ArtificeBlessingMinorAbilitySaves.ToReference<BlueprintAbilityReference>()
                };
            });

            var ArtificeBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArtificeBlessingFeature", bp => {
                bp.SetName("Artifice");
                bp.SetDescription("At 1st level, you can touch an ally and grant it a tactical advantage for 1 minute. The ally gets one of the following bonuses: +10 feet to base speed, +1 " +
                    "dodge bonus to AC, +1 insight bonus on attack rolls, or a +1 luck bonus on saving throws. \nAt 10th level, you can touch an ally and grant it a thirst for battle. All of " +
                    "the ally’s melee attacks are treated as if they had the vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm " +
                    "critical hits. These benefits last for 1 minute.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArtificeBlessingMinorAbilityBase.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ArtificepriestClass;
                    c.Level = 10;
                    c.m_Feature = ArtificeBlessingMajorFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.CheckInProgression = true;
                    c.HideInUI = true;
                    c.m_Feature = ArtificeDomainAllowed;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WarpriestBlessing };
            });

            //BlessingTools.RegisterBlessing(ArtificeBlessingFeature);
            //BlessingTools.CreateDivineTrackerBlessing("DivineTrackerArtificeBlessingFeature", ArtificeBlessingFeature, "At 1st level, you can touch an ally and grant it a tactical advantage for 1 minute. The ally gets one of the following bonuses: +10 feet to base speed, +1 dodge bonus to AC, +1 insight bonus on attack rolls, or a +1 luck bonus on saving throws. \nAt 13th level, you can touch an ally and grant it a thirst for battle. All of the ally’s melee attacks are treated as if they had the vicious weapon special ability. In addition, the ally receives a +4 insight bonus on attack rolls made to confirm critical hits. These benefits last for 1 minute.");

        }
    }
}
