using BlueprintCore.Abilities.Restrictions.New;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Deities;
using ExpandedContent.Tweaks.DemonLords;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Visual.Decals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.Designers.Mechanics.Facts;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class PinpointTargeting {
        public static void AddPinpointTargeting() {

            var PreciseShotFeature = Resources.GetBlueprint<BlueprintFeature>("8f3d1e6b4be006f4d896081f2f889665");
            var ImprovedPreciseShotFeature = Resources.GetBlueprint<BlueprintFeature>("46f970a6b9b5d2346b10892673fe6e74");
            var PointBlankShotFeature = Resources.GetBlueprint<BlueprintFeature>("0da0c194d6e1d43419eb8d990b28e0ab");

            var PinpointTargetingIcon = AssetLoader.LoadInternal("Skills", "Icon_PinpointTargeting.png");

            var PinpointTargetingBuff = Helpers.CreateBuff("PinpointTargetingBuff", bp => {
                bp.SetName("Pinpoint Targeting");
                bp.SetDescription("You can target the weak points in your opponent’s armor. \nAs a standard action, make a single ranged attack. The target does not gain any armor, " +
                    "natural armor, or shield bonuses to its Armor Class. You do not gain the benefit of this feat if you move this round.");
                bp.m_Icon = PinpointTargetingIcon;
                bp.AddComponent<AttackTypeChange>(c => {
                    c.NeedsWeapon = true;
                    c.ChangeAllTypes = false;
                    c.OriginalType= AttackType.Ranged;
                    c.NewType = AttackType.Touch;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = false;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.Stacking = StackingType.Replace;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var PinpointTargetingDisabledFlag = Helpers.CreateBuff("PinpointTargetingDisabledFlag", bp => {
                bp.SetName("Pinpoint Targeting Disabled flag");
                bp.SetDescription("");
                bp.m_Icon = PinpointTargetingIcon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.CantMove;
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var PinpointTargetingMovementChecker = Helpers.CreateBuff("PinpointTargetingMovementChecker", bp => {
                bp.SetName("Pinpoint Targeting Move checker");
                bp.SetDescription("");
                bp.m_Icon = PinpointTargetingIcon;
                bp.AddComponent<MovementDistanceTrigger>(c => {
                    c.DistanceInFeet = 1;
                    c.LimitTiggerCountInOneRound = true;
                    c.TiggerCountMaximumInOneRound = 1;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PinpointTargetingDisabledFlag.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });                
                bp.Stacking = StackingType.Replace;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //All of the buffs that reference themselves like this do it this way and not ContextActionRemoveSelf, not sure why but if it works for them it works for me
            PinpointTargetingMovementChecker.AddComponent<CombatStateTrigger>(c => {
                c.CombatStartActions = Helpers.CreateActionList();
                c.CombatEndActions = Helpers.CreateActionList(
                    new ContextActionRemoveBuff() { m_Buff = PinpointTargetingMovementChecker.ToReference<BlueprintBuffReference>() });
            });
            var PinpointTargetingAttackAbility = Helpers.CreateBlueprint<BlueprintAbility>("PinpointTargetingAttackAbility", bp => {
                bp.SetName("Pinpoint Targeting");
                bp.SetDescription("You can target the weak points in your opponent’s armor. \nAs a standard action, make a single ranged attack. The target does not gain any armor, " +
                    "natural armor, or shield bonuses to its Armor Class. You do not gain the benefit of this feat if you move this round.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList();
                });
                bp.AddComponent<AbilityDeliverAttackWithWeapon>();
                bp.m_Icon = PinpointTargetingIcon;
                bp.Type = AbilityType.Physical;
                bp.Range = AbilityRange.Weapon;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.ShouldTurnToTarget = true;
                bp.SpellResistance = false;
                bp.Hidden = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.EnchantWeapon;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PinpointTargetingAbility = Helpers.CreateBlueprint<BlueprintAbility>("PinpointTargetingAbility", bp => {
                bp.SetName("Pinpoint Targeting");
                bp.SetDescription("You can target the weak points in your opponent’s armor. \nAs a standard action, make a single ranged attack. The target does not gain any armor, " +
                    "natural armor, or shield bonuses to its Armor Class. You do not gain the benefit of this feat if you move this round.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PinpointTargetingBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsNotDispelable = true,
                            ToCaster = true
                        },
                        new ContextActionCastSpell() {
                            m_Spell = PinpointTargetingAttackAbility.ToReference<BlueprintAbilityReference>(),
                            OverrideDC = false,
                            DC = 0,
                            OverrideSpellLevel = false,
                            SpellLevel = 0,
                            CastByTarget = false,
                            LogIfCanNotTarget = false,
                            MarkAsChild = false
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PinpointTargetingDisabledFlag.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityTargetIsAlly>(c => {
                    c.Not = true;
                });
                bp.AddComponent<AbilityCasterHasWeaponWithRangeType>(c => {
                    c.RangeType = WeaponRangeType.Ranged;
                });
                bp.m_Icon = PinpointTargetingIcon;
                bp.Type = AbilityType.Physical;
                bp.Range = AbilityRange.Weapon;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Special;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PinpointTargetingFeature = Helpers.CreateBlueprint<BlueprintFeature>("PinpointTargetingFeature", bp => {
                bp.SetName("Pinpoint Targeting");
                bp.SetDescription("You can target the weak points in your opponent’s armor. \nAs a standard action, make a single ranged attack. The target does not gain any armor, " +
                    "natural armor, or shield bonuses to its Armor Class. You do not gain the benefit of this feat if you move this round.");
                bp.m_Icon = PreciseShotFeature.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PinpointTargetingAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddBuffOnCombatStart>(c => {
                    c.CheckParty = true;
                    c.m_Feature = PinpointTargetingMovementChecker.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.BaseAttackBonus;
                    c.Value = 16;
                });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.Dexterity;
                    c.Value = 19;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = PointBlankShotFeature.ToReference<BlueprintFeatureReference>();
                    c.Group = Prerequisite.GroupType.All;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = PreciseShotFeature.ToReference<BlueprintFeatureReference>();
                    c.Group = Prerequisite.GroupType.All;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ImprovedPreciseShotFeature.ToReference<BlueprintFeatureReference>();
                    c.Group = Prerequisite.GroupType.All;
                });
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Feat,
                    FeatureGroup.CombatFeat
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            if (ModSettings.AddedContent.Feats.IsDisabled("Pinpoint Targeting")) { return; }
            FeatTools.AddAsFeat(PinpointTargetingFeature);
        }
    }
}
