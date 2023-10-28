using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents;
using static Kingmaker.RuleSystem.Rules.Abilities.RuleApplySpell;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using Kingmaker.Designers.Mechanics.Buffs;

namespace ExpandedContent.Tweaks.Blessings {
    internal class CommunityBlessing {
        public static void AddCommunityBlessing() {

            var CommunityDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("c87004460f3328c408d22c5ead05291f");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestAspectOfWarBuff = Resources.GetBlueprint<BlueprintBuff>("27d14b07b52c2df42a4dcd6bfb840425");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var FiendishQuarryIcon = Resources.GetBlueprint<BlueprintAbility>("d9660af97d116f94ab98dbec15dbc704").Icon;
            var ImprovedFiendishQuarryIcon = Resources.GetBlueprint<BlueprintAbility>("fded23ca2b2a6094cbdc54811f309b0c").Icon;

            var CommunityRangedNormalIcon = AssetLoader.LoadInternal("Skills", "Icon_CommunityRangedNormal.jpg");
            var CommunityMeleeNormalIcon = AssetLoader.LoadInternal("Skills", "Icon_CommunityMeleeNormal.jpg");
            var CommunityRangedCritIcon = AssetLoader.LoadInternal("Skills", "Icon_CommunityRangedCrit.jpg");
            var CommunityMeleeCritIcon = AssetLoader.LoadInternal("Skills", "Icon_CommunityMeleeCrit.jpg");

            var CommunityBlessingMinorBuff = Helpers.CreateBuff("CommunityBlessingMinorBuff", bp => {//Empty, used as a flag in AidAnother
                bp.SetName("Communal Aid");
                bp.SetDescription("For the next minute, whenever you use the aid another action, the bonus granted increases by 2.");
                bp.m_Icon = FiendishQuarryIcon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var CommunityBlessingMinorAbilitySelf = Helpers.CreateBlueprint<BlueprintAbility>("CommunityBlessingMinorAbilitySelf", bp => {
                bp.SetName("Communal Aid - Self");
                bp.SetDescription("As a swift action, grant yourself the blessing of community. For the next minute, whenever you use the aid another action, the bonus granted increases by 2.");
                bp.m_Icon = FiendishQuarryIcon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityBlessingMinorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("CommunityBlessingMinorAbilitySelf.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var CommunityBlessingMinorAbilityOthers = Helpers.CreateBlueprint<BlueprintAbility>("CommunityBlessingMinorAbilityOthers", bp => {
                bp.SetName("Communal Aid - Allies");
                bp.SetDescription("Grant an ally the blessing of community. For the next minute, whenever the ally uses the aid another action, the bonus granted increases by 2.");
                bp.m_Icon = FiendishQuarryIcon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityBlessingMinorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("CommunityBlessingMinorAbilityOthers.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var CommunityBlessingMajorMeleeCrit = Helpers.CreateBuff("CommunityBlessingMajorMeleeCrit", bp => {
                bp.SetName("Fight as One - Melee Crit");
                bp.SetDescription("The insight bonus to hit against this foe is increased to +4 for one round.");
                bp.m_Icon = CommunityMeleeCritIcon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var CommunityBlessingMajorMeleeNormal = Helpers.CreateBuff("CommunityBlessingMajorMeleeNormal", bp => { //Needs Testing!!!
                bp.SetName("Fight as One - Melee");
                bp.SetDescription("Melee attacks against this foe have a +2 insight bonus to hit.");
                bp.m_Icon = CommunityMeleeNormalIcon;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.CheckCaster = false;
                    c.CheckCasterFriend = true;
                    c.CheckRangeType = true;
                    c.RangeType = WeaponRangeType.Melee;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.TargetBuffRank;
                    c.m_Buff = CommunityBlessingMajorMeleeCrit.ToReference<BlueprintBuffReference>();
                    c.m_BuffRankMultiplier = 1;
                    c.m_Progression = ContextRankProgression.MultiplyByModifier;
                    c.m_StepLevel = 4;
                    c.m_UseMin = true;
                    c.m_Min = 2;
                    c.m_UseMax = true;
                    c.m_Max = 4;
                });                
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var CommunityBlessingMajorRangedCrit = Helpers.CreateBuff("CommunityBlessingMajorRangedCrit", bp => {
                bp.SetName("Fight as One - Ranged Crit");
                bp.SetDescription("The insight bonus to hit against this foe is increased to +4 for one round.");
                bp.m_Icon = CommunityRangedCritIcon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var CommunityBlessingMajorRangedNormal = Helpers.CreateBuff("CommunityBlessingMajorRangedNormal", bp => { //Needs Testing!!!
                bp.SetName("Fight as One - Ranged");
                bp.SetDescription("Ranged attacks against this foe have a +2 insight bonus to hit.");
                bp.m_Icon = CommunityRangedNormalIcon;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.CheckCaster = false;
                    c.CheckCasterFriend = true;
                    c.CheckRangeType = true;
                    c.RangeType = WeaponRangeType.Ranged;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.TargetBuffRank;
                    c.m_Buff = CommunityBlessingMajorRangedCrit.ToReference<BlueprintBuffReference>();
                    c.m_BuffRankMultiplier = 3;
                    c.m_Progression = ContextRankProgression.MultiplyByModifier;
                    c.m_StepLevel = 1;
                    c.m_UseMin = true;
                    c.m_Min = 2;
                    c.m_UseMax = true;
                    c.m_Max = 4;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });



            var CommunityBlessingMajorBuff = Helpers.CreateBuff("CommunityBlessingMajorBuff", bp => {
                bp.SetName("Fight as One");
                bp.SetDescription("Whenever you make a successful melee or ranged attack against a foe, allies " +
                    "gain a +2 insight bonus on attacks of the same type you made against that foe—melee attacks if you made a melee attack, or ranged " +
                    "attacks if you made a ranged attack. If you score a critical hit, this bonus increases to +4 until the start of your next turn.");
                bp.m_Icon = ImprovedFiendishQuarryIcon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {//Normal Melee
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
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityBlessingMajorMeleeNormal.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {//Crit Melee
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityBlessingMajorMeleeCrit.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {//Normal Ranged
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
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityBlessingMajorRangedNormal.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {//Crit Ranged
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityBlessingMajorRangedCrit.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var CommunityBlessingMajorAbility = Helpers.CreateBlueprint<BlueprintAbility>("CommunityBlessingMajorAbility", bp => {
                bp.SetName("Fight as One");
                bp.SetDescription("For 1 minute, whenever you make a successful melee or ranged attack against a foe, allies within 10 feet of you " +
                    "gain a +2 insight bonus on attacks of the same type you made against that foe—melee attacks if you made a melee attack, or ranged " +
                    "attacks if you made a ranged attack. If you score a critical hit, this bonus increases to +4 until the start of your next turn.");
                bp.m_Icon = ImprovedFiendishQuarryIcon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityBlessingMajorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("CommunityBlessingMajorAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });





            var CommunityBlessingMajorFeature = Helpers.CreateBlueprint<BlueprintFeature>("CommunityBlessingMajorFeature", bp => {
                bp.SetName("Fight as One");
                bp.SetDescription("At 10th level, you can rally your allies to fight together. For 1 minute, whenever you make a successful " +
                    "melee or ranged attack against a foe, allies within 10 feet of you gain a +2 insight bonus on attacks of the same type " +
                    "you made against that foe—melee attacks if you made a melee attack, or ranged attacks if you made a ranged attack. If you " +
                    "score a critical hit, this bonus increases to +4 until the start of your next turn.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CommunityBlessingMajorAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInCharacterSheetAndLevelUp = true;
            });

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
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerCommunityBlessingFeature", CommunityBlessingFeature, "At 1st level, you can touch an ally and grant it the blessing of community. For the next minute, whenever that ally uses the aid another action, the bonus granted increases by 2. You can instead use this ability on yourself as a swift action. \nAt 13th level, you can rally your allies to fight together. For 1 minute, whenever you make a successful melee or ranged attack against a foe, allies within 10 feet of you gain a +2 insight bonus on attacks of the same type you made against that foe—melee attacks if you made a melee attack, or ranged attacks if you made a ranged attack. If you score a critical hit, this bonus increases to +4 until the start of your next turn.");

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
            CommunityBlessingFeature.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { QuickenBlessingCommunityFeature.ToReference<BlueprintFeatureReference>() };
        }
    }
}
