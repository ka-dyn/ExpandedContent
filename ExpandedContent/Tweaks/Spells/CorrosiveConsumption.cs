using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class CorrosiveConsumption {
        public static void AddCorrosiveConsumption() {
            var CorrosiveConsumptionIcon = AssetLoader.LoadInternal("Skills", "Icon_CorrosiveConsumption.jpg");
            var Icon_ScrollOfCorrosiveConsumption = AssetLoader.LoadInternal("Items", "Icon_ScrollOfCorrosiveConsumption.png");


            var CorrosiveConsumptionFlagBuff = Helpers.CreateBuff("CorrosiveConsumptionFlagBuff", bp => {
                bp.SetName("CorrosiveConsumptionFlagBuff");
                bp.SetDescription("");
                bp.Stacking = StackingType.Replace;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
            });

            var CorrosiveConsumptionBuff = Helpers.CreateBuff("CorrosiveConsumptionBuff", bp => {
                bp.SetName("Corrosive Consumption");
                bp.SetDescription("With a touch, this spell causes a small, rapidly growing patch of corrosive acid to appear on the target. " +
                    "On application, the acid deals 1 point of acid damage per caster level (maximum 15). " +
                    "\nNext round on the casters turn, the acid patch grows and deals 1d4 points of acid damage per caster level (maximum 15d4). " +
                    "\nDuring the second round on the casters turn, the acid patch covers the entire creature and deals 1d6 points of acid damage per caster level (maximum 15d6), the acid then evaporates. " +
                    "\nA creature can only be affected by one instance of corrosive consumption at a time.");
                bp.m_Icon = CorrosiveConsumptionIcon;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Acid,
                                Type = DamageType.Energy
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
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
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.One,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.DamageDice
                                },
                                BonusValue = 0,
                            },
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage,
                            Half = false
                        });
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = CorrosiveConsumptionFlagBuff.ToReference<BlueprintBuffReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Slashing,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = Kingmaker.Enums.Damage.DamageEnergyType.Acid,
                                        Type = DamageType.Energy
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
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
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.DamageDice
                                        },
                                        BonusValue = 0,
                                    },
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage,
                                    Half = false
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = CorrosiveConsumptionFlagBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 2
                                    },
                                    DurationSeconds = 0
                                }
                                ),
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Slashing,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = Kingmaker.Enums.Damage.DamageEnergyType.Acid,
                                        Type = DamageType.Energy
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
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
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.DamageDice
                                        },
                                        BonusValue = 0,
                                    },
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage,
                                    Half = false
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = CorrosiveConsumptionFlagBuff.ToReference<BlueprintBuffReference>()                                    
                                },
                                new ContextActionRemoveSelf()
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_UseMax = true;
                    c.m_Max = 15;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var CorrosiveConsumptionAbility = Helpers.CreateBlueprint<BlueprintAbility>("CorrosiveConsumptionAbility", bp => {
                bp.SetName("Corrosive Consumption");
                bp.SetDescription("With a touch, this spell causes a small, rapidly growing patch of corrosive acid to appear on the target. " +
                    "On application, the acid deals 1 point of acid damage per caster level (maximum 15). " +
                    "\nNext round on the casters turn, the acid patch grows and deals 1d4 points of acid damage per caster level (maximum 15d4). " +
                    "\nDuring the second round on the casters turn, the acid patch covers the entire creature and deals 1d6 points of acid damage per caster level (maximum 15d6), the acid then evaporates. " +
                    "\nA creature can only be affected by one instance of corrosive consumption at a time.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CorrosiveConsumptionBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Acid;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = CorrosiveConsumptionIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var CorrosiveConsumptionScroll = ItemTools.CreateScroll("ScrollOfCorrosiveConsumption", Icon_ScrollOfCorrosiveConsumption, CorrosiveConsumptionAbility, 5, 9);
            VenderTools.AddScrollToLeveledVenders(CorrosiveConsumptionScroll);
            CorrosiveConsumptionAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 5);
            CorrosiveConsumptionAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 5);
        }
    }
}
