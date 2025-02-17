using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using UnityEngine;
using Kingmaker.Visual.Decals;
using System.Linq;

namespace ExpandedContent.Tweaks.Spells {
    internal class Detonate {
        public static void AddDetonate() {
            var DetonateIcon = Resources.GetBlueprint<BlueprintAbility>("9afdc3eeca49c594aa7bf00e8e9803ac").Icon;
            //var Icon_ScrollOfDetonate = AssetLoader.LoadInternal("Items", "Icon_ScrollOfDetonate.png");


            #region Buffs 1
            var DetonateAcidBuff = Helpers.CreateBuff("DetonateAcidBuff", bp => {
                bp.SetName("Detonate - Acid");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.m_Icon = DetonateIcon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            var DetonateColdBuff = Helpers.CreateBuff("DetonateColdBuff", bp => {
                bp.SetName("Detonate - Cold");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.m_Icon = DetonateIcon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            var DetonateElectricityBuff = Helpers.CreateBuff("DetonateElectricityBuff", bp => {
                bp.SetName("Detonate - Electricity");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.m_Icon = DetonateIcon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            var DetonateFireBuff = Helpers.CreateBuff("DetonateFireBuff", bp => {
                bp.SetName("Detonate - Fire");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.m_Icon = DetonateIcon;                
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            #endregion
            #region Explosions
            var DetonateAcidExplosion = Helpers.CreateBlueprint<BlueprintAbility>("DetonateAcidExplosion", bp => {
                bp.SetName("Detonate - Acid");
                bp.SetDescription("The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Energy,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Acid
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    EnergyDrainType = EnergyDrainType.Temporary,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    ReadPreRolledFromSharedValue = true,
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D8,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.DamageDice,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    Half = true,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    WriteResultToSharedValue = false,
                                    ResultSharedValue = AbilitySharedValue.Damage
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    Type = SavingThrowType.Reflex,
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Acid
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Acid
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                            Failed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Acid
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Acid
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                        }
                                        )
                                }
                                )
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = DetonateAcidBuff.ToReference<BlueprintBuffReference>(),
                            ToCaster = true
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "13eb28db3412f894795b434673d6bbd4" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 10;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D8,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = 0
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Acid;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateAcidExplosion.SavingThrow", "Relex half");
            });
            var DetonateColdExplosion = Helpers.CreateBlueprint<BlueprintAbility>("DetonateColdExplosion", bp => {
                bp.SetName("Detonate - Cold");
                bp.SetDescription("The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Energy,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Cold
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    EnergyDrainType = EnergyDrainType.Temporary,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    ReadPreRolledFromSharedValue = true,
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D8,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.DamageDice,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    Half = true,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    WriteResultToSharedValue = false,
                                    ResultSharedValue = AbilitySharedValue.Damage
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    Type = SavingThrowType.Reflex,
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Cold
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Cold
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                            Failed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Cold
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Cold
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                        }
                                        )
                                }
                                )
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = DetonateColdBuff.ToReference<BlueprintBuffReference>(),
                            ToCaster = true
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "941ea458189aeeb4e8edf9a2dcd9659c" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 10;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D8,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = 0
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Cold;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateColdExplosion.SavingThrow", "Relex half");
            });
            var DetonateElectricityExplosion = Helpers.CreateBlueprint<BlueprintAbility>("DetonateElectricityExplosion", bp => {
                bp.SetName("Detonate - Electricity");
                bp.SetDescription("The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Energy,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Electricity
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    EnergyDrainType = EnergyDrainType.Temporary,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    ReadPreRolledFromSharedValue = true,
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D8,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.DamageDice,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    Half = true,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    WriteResultToSharedValue = false,
                                    ResultSharedValue = AbilitySharedValue.Damage
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    Type = SavingThrowType.Reflex,
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Electricity
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Electricity
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                            Failed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Electricity
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Electricity
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                        }
                                        )
                                }
                                )
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = DetonateElectricityBuff.ToReference<BlueprintBuffReference>(),
                            ToCaster = true
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "90494fc296c45c743ab2b90f864228ca" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 10;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D8,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = 0
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Electricity;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateElectricityExplosion.SavingThrow", "Relex half");
            });
            var DetonateFireExplosion = Helpers.CreateBlueprint<BlueprintAbility>("DetonateFireExplosion", bp => {
                bp.SetName("Detonate - Fire");
                bp.SetDescription("The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Energy,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Fire
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    EnergyDrainType = EnergyDrainType.Temporary,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    ReadPreRolledFromSharedValue = true,
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D8,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.DamageDice,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    Half = true,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    WriteResultToSharedValue = false,
                                    ResultSharedValue = AbilitySharedValue.Damage
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    Type = SavingThrowType.Reflex,
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Fire
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Fire
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                            Failed = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.Or,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionDistanceToTarget() {
                                                                DistanceGreater = new Feet() { m_Value = 15 },
                                                                Not = true
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Fire
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = false,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            m_Type = ContextActionDealDamage.Type.Damage,
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData() {
                                                                    Reality = 0,
                                                                    Alignment = 0,
                                                                    Precision = false
                                                                },
                                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                                    Material = 0,
                                                                    Form = 0,
                                                                    Enhancement = 0,
                                                                    EnhancementTotal = 0
                                                                },
                                                                Energy = DamageEnergyType.Fire
                                                            },
                                                            Drain = false,
                                                            AbilityType = StatType.Unknown,
                                                            EnergyDrainType = EnergyDrainType.Temporary,
                                                            Duration = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true,
                                                            },
                                                            ReadPreRolledFromSharedValue = true,//!
                                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.D8,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.DamageDice,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true,
                                                            Half = true,
                                                            UseMinHPAfterDamage = false,
                                                            MinHPAfterDamage = 0,
                                                            WriteResultToSharedValue = false,
                                                            ResultSharedValue = AbilitySharedValue.Damage
                                                        }
                                                        )
                                                }
                                            ),
                                        }
                                        )
                                }
                                )
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = DetonateFireBuff.ToReference<BlueprintBuffReference>(),
                            ToCaster = true
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "808355a0f2a83344a918f89ea66d99a2" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 10;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D8,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = 0
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateFireExplosion.SavingThrow", "Relex half");
            });
            #endregion
            #region FX
            var DetonateAcidExplosionSpawnFx = DetonateAcidExplosion.GetComponent<AbilitySpawnFx>();
            DetonateAcidExplosionSpawnFx.PrefabLink = DetonateAcidExplosionSpawnFx.PrefabLink.CreateDynamicProxy(pfl => {
                //Main.Log($"Editing: {pfl}");
                pfl.name = "DetonateAcid_30feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                var Rescale = pfl.transform.Find("Rescale (1)");
                Rescale.transform.localScale = new(5f, 5f, 5f);
                var Decal = pfl.transform.Find("Rescale (1)/RandomRotate/Decal").GetComponent<FxDecal>().Animations.Last();
                Decal.Lifetime = 5.5f;
            });
            var DetonateColdExplosionSpawnFx = DetonateColdExplosion.GetComponent<AbilitySpawnFx>();
            DetonateColdExplosionSpawnFx.PrefabLink = DetonateColdExplosionSpawnFx.PrefabLink.CreateDynamicProxy(pfl => {
                //Main.Log($"Editing: {pfl}");
                pfl.name = "DetonateCold_30feetAoE";
                pfl.transform.localScale = new(3.5f, 3f, 3.5f);
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                //var Rescale = pfl.transform.Find("Rescale (1)");
                //Rescale.transform.localScale = new(5f, 5f, 5f);
                //var Decal = pfl.transform.Find("Rescale (1)/RandomRotate/Decal").GetComponent<FxDecal>().Animations.Last();
                //Decal.Lifetime = 5.5f;
            });
            var DetonateElectricityExplosionSpawnFx = DetonateElectricityExplosion.GetComponent<AbilitySpawnFx>();
            DetonateElectricityExplosionSpawnFx.PrefabLink = DetonateElectricityExplosionSpawnFx.PrefabLink.CreateDynamicProxy(pfl => {
                //Main.Log($"Editing: {pfl}");
                pfl.name = "DetonateElectricity_30feetAoE";
                pfl.transform.localScale = new(3.7f, 3.7f, 3.7f);
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                Object.DestroyImmediate(pfl.transform.Find("Rescale/Lightning_starter").gameObject);
            });
            var DetonateFireExplosionSpawnFx = DetonateFireExplosion.GetComponent<AbilitySpawnFx>();
            DetonateFireExplosionSpawnFx.PrefabLink = DetonateFireExplosionSpawnFx.PrefabLink.CreateDynamicProxy(pfl => {
                //Main.Log($"Editing: {pfl}");
                pfl.name = "DetonateFire_30feetAoE";
                pfl.transform.localScale = new(3.7f, 3.7f, 3.7f);
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                //var Rescale = pfl.transform.Find("Rescale (1)");
                //Rescale.transform.localScale = new(5f, 5f, 5f);
                //var Decal = pfl.transform.Find("Rescale (1)/RandomRotate/Decal").GetComponent<FxDecal>().Animations.Last();
                //Decal.Lifetime = 5.5f;
            });
            #endregion
            #region Buffs 2
            DetonateAcidBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList();
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList(
                    new ContextActionCastSpell() {
                        m_Spell = DetonateAcidExplosion.ToReference<BlueprintAbilityReference>(),
                        OverrideDC = false,
                        DC = 0,
                        OverrideSpellLevel = false,
                        SpellLevel = 0,
                        CastByTarget = false,
                        LogIfCanNotTarget = false,
                        MarkAsChild = false//?
                    }
                    );
            });
            DetonateAcidBuff.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                c.m_Ability = DetonateAcidExplosion.ToReference<BlueprintAbilityReference>();
            });
            DetonateColdBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList();
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList(
                    new ContextActionCastSpell() {
                        m_Spell = DetonateColdExplosion.ToReference<BlueprintAbilityReference>(),
                        OverrideDC = false,
                        DC = 0,
                        OverrideSpellLevel = false,
                        SpellLevel = 0,
                        CastByTarget = false,
                        LogIfCanNotTarget = false,
                        MarkAsChild = false//?
                    }
                    );
            });
            DetonateColdBuff.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                c.m_Ability = DetonateColdExplosion.ToReference<BlueprintAbilityReference>();
            });
            DetonateElectricityBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList();
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList(
                    new ContextActionCastSpell() {
                        m_Spell = DetonateElectricityExplosion.ToReference<BlueprintAbilityReference>(),
                        OverrideDC = false,
                        DC = 0,
                        OverrideSpellLevel = false,
                        SpellLevel = 0,
                        CastByTarget = false,
                        LogIfCanNotTarget = false,
                        MarkAsChild = false//?
                    }
                    );
            });
            DetonateElectricityBuff.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                c.m_Ability = DetonateElectricityExplosion.ToReference<BlueprintAbilityReference>();
            });
            DetonateFireBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList();
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList(
                    new ContextActionCastSpell() {
                        m_Spell = DetonateFireExplosion.ToReference<BlueprintAbilityReference>(),
                        OverrideDC = false,
                        DC = 0,
                        OverrideSpellLevel = false,
                        SpellLevel = 0,
                        CastByTarget = false,
                        LogIfCanNotTarget = false,
                        MarkAsChild = false//?
                    }
                    );
            });
            DetonateFireBuff.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                c.m_Ability = DetonateFireExplosion.ToReference<BlueprintAbilityReference>();
            });
            #endregion
            #region Variants
            var DetonateAcidAbility = Helpers.CreateBlueprint<BlueprintAbility>("DetonateAcidAbility", bp => {
                bp.SetName("Detonate - Acid");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DetonateAcidBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Acid;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("DetonateAcidAbility.Duration", "1 round, then instantaneous");
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateAcidAbility.SavingThrow", "Reflex half");
            });
            var DetonateColdAbility = Helpers.CreateBlueprint<BlueprintAbility>("DetonateColdAbility", bp => {
                bp.SetName("Detonate - Cold");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DetonateColdBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Cold;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("DetonateColdAbility.Duration", "1 round, then instantaneous");
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateColdAbility.SavingThrow", "Reflex half");
            });
            var DetonateElectricityAbility = Helpers.CreateBlueprint<BlueprintAbility>("DetonateElectricityAbility", bp => {
                bp.SetName("Detonate - Electricity");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DetonateElectricityBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Electricity;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("DetonateElectricityAbility.Duration", "1 round, then instantaneous");
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateElectricityAbility.SavingThrow", "Reflex half");
            });
            var DetonateFireAbility = Helpers.CreateBlueprint<BlueprintAbility>("DetonateFireAbility", bp => {
                bp.SetName("Detonate - Fire");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DetonateFireBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("DetonateFireAbility.Duration", "1 round, then instantaneous");
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateFireAbility.SavingThrow", "Reflex half");
            });
            #endregion
            var DetonateBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("DetonateBaseAbility", bp => {
                bp.SetName("Detonate");
                bp.SetDescription("You flood yourself with a potent surge of elemental energy. One round after completing the casting of the spell, the energy explodes from your body. " +
                    "\nChoose one of the following four energy types: acid, cold, electricity, or fire. The explosion inflicts 1d8 points of damage of that energy type per caster level " +
                    "(maximum 10d8) to all creatures within 15 feet, and half that amount to targets past 15 feet but within 30 feet. You automatically take half damage from the explosion, " +
                    "without a saving throw, but any other energy resistance or energy immunity effects you may have in place can prevent or lessen this overflow damage caused by the explosion.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        DetonateAcidAbility.ToReference<BlueprintAbilityReference>(),
                        DetonateColdAbility.ToReference<BlueprintAbilityReference>(),
                        DetonateElectricityAbility.ToReference<BlueprintAbilityReference>(),
                        DetonateFireAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = DetonateIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("DetonateAbility.Duration", "1 round, then instantaneous");
                bp.LocalizedSavingThrow = Helpers.CreateString("DetonateAbility.SavingThrow", "Reflex half");
            });
            DetonateAcidAbility.m_Parent = DetonateBaseAbility.ToReference<BlueprintAbilityReference>();
            DetonateColdAbility.m_Parent = DetonateBaseAbility.ToReference<BlueprintAbilityReference>();
            DetonateElectricityAbility.m_Parent = DetonateBaseAbility.ToReference<BlueprintAbilityReference>();
            DetonateFireAbility.m_Parent = DetonateBaseAbility.ToReference<BlueprintAbilityReference>();
            //var DetonateScroll = ItemTools.CreateScroll("ScrollOfDetonate", Icon_ScrollOfDetonate, DetonateAbility, 4, 7);
            //VenderTools.AddScrollToLeveledVenders(DetonateScroll);
            DetonateBaseAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 4);
            DetonateBaseAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 4);
            DetonateBaseAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 4);
            DetonateBaseAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
        }
    }
}
