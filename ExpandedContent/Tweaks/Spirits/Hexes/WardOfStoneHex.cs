using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Spirits.Hexes {
    internal class WardOfStoneHex {
        public static void AddWardOfStoneHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var ShamanStoneSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("acff6b0cf279a31439010afea01df912");
            var ShamanStoneSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("e6468a6fac9e1074897b2487a0659e96");
            var ShamanStoneSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("5c3ccab7cb27f4a408531197eb2abd3f");

            var ClaySkinIcon = AssetLoader.LoadInternal("Skills", "Icon_ClaySkin.jpg");

            var ShamanHexWardOfStoneCooldown = Helpers.CreateBuff("ShamanHexWardOfStoneCooldown", bp => {
                bp.SetName("Already targeted by this hex today");
                bp.SetDescription("");
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var ShamanHexWardOfStoneBuff1 = Helpers.CreateBuff("ShamanHexWardOfStoneBuff1", bp => {
                bp.SetName("Ward of Stone");
                bp.SetDescription("The shaman touches a willing creature (including herself) and grants a ward of stoene. " +
                    "The next time the warded creature is struck with a melee attack, it is treated as if it has DR 5/adamantine. " +
                    "This ward lasts for 1 minute, after which it fades away if not already expended. At 8th and 16th levels, the ward lasts for one additional attack. " +
                    "A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = ClaySkinIcon;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 5;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                }); 
                bp.AddComponent<AddTargetAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
                    c.OnlyHit = true;
                    c.CriticalHit = false;
                    c.OnlyMelee = false;
                    c.NotReach = false;
                    c.CheckCategory = false;
                    c.Not = false;
                    c.Categories = new WeaponCategory[] { };
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                    c.DoNotPassAttackRoll = false;
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var ShamanHexWardOfStoneBuff2 = Helpers.CreateBuff("ShamanHexWardOfStoneBuff2", bp => {
                bp.SetName("Ward of Stone - 2 Strikes");
                bp.SetDescription("The shaman touches a willing creature (including herself) and grants a ward of stoene. " +
                    "The next time the warded creature is struck with a melee attack, it is treated as if it has DR 5/adamantine. " +
                    "This ward lasts for 1 minute, after which it fades away if not already expended. At 8th and 16th levels, the ward lasts for one additional attack. " +
                    "A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = ClaySkinIcon;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 5;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                });
                bp.AddComponent<AddTargetAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
                    c.OnlyHit = true;
                    c.CriticalHit = false;
                    c.OnlyMelee = false;
                    c.NotReach = false;
                    c.CheckCategory = false;
                    c.Not = false;
                    c.Categories = new WeaponCategory[] { };
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexWardOfStoneBuff1.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                    c.DoNotPassAttackRoll = false;
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var ShamanHexWardOfStoneBuff3 = Helpers.CreateBuff("ShamanHexWardOfStoneBuff3", bp => {
                bp.SetName("Ward of Stone - 3 Strikes");
                bp.SetDescription("The shaman touches a willing creature (including herself) and grants a ward of stoene. " +
                    "The next time the warded creature is struck with a melee attack, it is treated as if it has DR 5/adamantine. " +
                    "This ward lasts for 1 minute, after which it fades away if not already expended. At 8th and 16th levels, the ward lasts for one additional attack. " +
                    "A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = ClaySkinIcon;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 5;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                });
                bp.AddComponent<AddTargetAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
                    c.OnlyHit = true;
                    c.CriticalHit = false;
                    c.OnlyMelee = false;
                    c.NotReach = false;
                    c.CheckCategory = false;
                    c.Not = false;
                    c.Categories = new WeaponCategory[] { };
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexWardOfStoneBuff2.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                    c.DoNotPassAttackRoll = false;
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });


            var ShamanHexWardOfStoneAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexWardOfStoneAbility", bp => {
                bp.SetName("Ward of Stone");
                bp.SetDescription("The shaman touches a willing creature (including herself) and grants a ward of stoene. " +
                    "The next time the warded creature is struck with a melee attack, it is treated as if it has DR 5/adamantine. " +
                    "This ward lasts for 1 minute, after which it fades away if not already expended. At 8th and 16th levels, the ward lasts for one additional attack. " +
                    "A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = ClaySkinIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexWardOfStoneCooldown.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Days,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 16,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanHexWardOfStoneBuff3.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Minutes,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    ToCaster = false,
                                    AsChild = true,
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionSharedValueHigher() {
                                                Not = false,
                                                SharedValue = AbilitySharedValue.Damage,
                                                HigherOrEqual = 8,
                                                Inverted = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = ShamanHexWardOfStoneBuff2.ToReference<BlueprintBuffReference>(),
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Minutes,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1,
                                                m_IsExtendable = true
                                            },
                                            DurationSeconds = 0,
                                            IsFromSpell = false,
                                            ToCaster = false,
                                            AsChild = true,
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = ShamanHexWardOfStoneBuff1.ToReference<BlueprintBuffReference>(),
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Minutes,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1,
                                                m_IsExtendable = true
                                            },
                                            DurationSeconds = 0,
                                            IsFromSpell = false,
                                            ToCaster = false,
                                            AsChild = true,
                                        }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ShamanHexWardOfStoneCooldown.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                    c.FromCaster = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ShamanHexWardOfStoneFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexWardOfStoneFeature", bp => {
                bp.SetName("Ward of Stone");
                bp.SetDescription("The shaman touches a willing creature (including herself) and grants a ward of stoene. " +
                    "The next time the warded creature is struck with a melee attack, it is treated as if it has DR 5/adamantine. " +
                    "This ward lasts for 1 minute, after which it fades away if not already expended. At 8th and 16th levels, the ward lasts for one additional attack. " +
                    "A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = ClaySkinIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexWardOfStoneAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanStoneSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanStoneSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanStoneSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexWardOfStoneFeature);

            ShamanStoneSpiritProgression.IsPrerequisiteFor.Add(ShamanHexWardOfStoneFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
