using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class OpeningVolley {
        public static void AddOpeningVolley() {

            var PreciseShotFeature = Resources.GetBlueprint<BlueprintFeature>("8f3d1e6b4be006f4d896081f2f889665");

            var OpeningVolleyTargetBuff = Helpers.CreateBuff("OpeningVolleyTargetBuff", bp => {
                bp.SetName("Opening Volley Targeted");
                bp.SetDescription("Your ranged assault leaves your foe disoriented and vulnerable to your melee attack. \nWhenever you deal damage with a ranged attack, " +
                    "you gain a +4 circumstance bonus on the next melee attack roll you make against the opponent. This attack must occur before the end of your next turn.");
                bp.m_Icon = PreciseShotFeature.Icon;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Value = 4;
                    c.Descriptor = ModifierDescriptor.Circumstance;
                    c.CheckCaster = true;
                    c.CheckCasterFriend = false;
                    c.CheckRangeType = true;
                    c.RangeType = WeaponRangeType.Melee;
                });                
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.Stacking = StackingType.Stack;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var OpeningVolleyFeature = Helpers.CreateBlueprint<BlueprintFeature>("OpeningVolleyFeature", bp => {
                bp.SetName("Opening Volley");
                bp.SetDescription("Your ranged assault leaves your foe disoriented and vulnerable to your melee attack. \nWhenever you deal damage with a ranged attack, " +
                    "you gain a +4 circumstance bonus on the next melee attack roll you make against the opponent. This attack must occur before the end of your next turn.");
                bp.m_Icon = PreciseShotFeature.Icon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
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
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = OpeningVolleyTargetBuff.ToReference<BlueprintBuffReference>(),
                                        Not = true
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OpeningVolleyTargetBuff.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceCountValue = 0,
                                        DiceType = DiceType.Zero,
                                        BonusValue = 2,
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }

                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
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
                    c.RangeType = WeaponRangeType.RangedTouch;
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
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = OpeningVolleyTargetBuff.ToReference<BlueprintBuffReference>(),
                                        Not = true
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OpeningVolleyTargetBuff.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceCountValue = 0,
                                        DiceType = DiceType.Zero,
                                        BonusValue = 2,
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }

                        );
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
                    c.RangeType = WeaponRangeType.Melee;
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
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = OpeningVolleyTargetBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() { 
                                    m_Buff = OpeningVolleyTargetBuff.ToReference<BlueprintBuffReference>(),
                                    OnlyFromCaster = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }
                        );
                });
                bp.AddComponent<RecommendationHasFeature>(c => {
                    c.m_Feature = PreciseShotFeature.ToReference<BlueprintUnitFactReference>();
                    c.Mandatory = true;
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

            if (ModSettings.AddedContent.Feats.IsDisabled("Opening Volley")) { return; }
            FeatTools.AddAsFeat(OpeningVolleyFeature);
        }
    }
}
