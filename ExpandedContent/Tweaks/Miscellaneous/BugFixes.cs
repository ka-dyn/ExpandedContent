using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class BugFixes {
        //This mod does not normally contain bug fixes for base game stuff as I just don't what to have to deal with that, however sometimes I'll need to
        //otherwise people might not be able to use mod features.
        //
        //Please delete fixes after Owlcat fixes them

        public static void ShamblingMoundGrabDamage() {
            var ShamblingMoundGrappledTargetBuff = Resources.GetBlueprint<BlueprintBuff>("2b5743ae1c3e478ab99defebcc881019");
            ShamblingMoundGrappledTargetBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList(
                    new ContextActionDealDamage() {
                        m_Type = ContextActionDealDamage.Type.Damage,
                        DamageType = new DamageTypeDescription() {
                            Type = DamageType.Physical,
                            Common = new DamageTypeDescription.CommomData() {
                                Reality = 0,
                                Alignment = 0,
                                Precision = false
                            },
                            Physical = new DamageTypeDescription.PhysicalData() {
                                Material = 0,
                                Form = PhysicalDamageForm.Bludgeoning | PhysicalDamageForm.Slashing,
                                Enhancement = 0,
                                EnhancementTotal = 0
                            },
                            Energy = DamageEnergyType.Fire
                        },
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
                        PreRolledSharedValue = AbilitySharedValue.Damage,
                        Value = new ContextDiceValue() {
                            DiceType = DiceType.D6,
                            DiceCountValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 2,
                                ValueRank = AbilityRankType.DamageDice,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            BonusValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 5,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                        },
                        IsAoE = false,
                        HalfIfSaved = false,
                        ResultSharedValue = AbilitySharedValue.Damage,
                        CriticalSharedValue = AbilitySharedValue.Damage
                    }
                    );
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList(
                    new ContextActionDealDamage() {
                        m_Type = ContextActionDealDamage.Type.Damage,
                        DamageType = new DamageTypeDescription() {
                            Type = DamageType.Physical,
                            Common = new DamageTypeDescription.CommomData() {
                                Reality = 0,
                                Alignment = 0,
                                Precision = false
                            },
                            Physical = new DamageTypeDescription.PhysicalData() {
                                Material = 0,
                                Form = PhysicalDamageForm.Bludgeoning | PhysicalDamageForm.Slashing,
                                Enhancement = 0,
                                EnhancementTotal = 0
                            },
                            Energy = DamageEnergyType.Fire
                        },
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
                        PreRolledSharedValue = AbilitySharedValue.Damage,
                        Value = new ContextDiceValue() {
                            DiceType = DiceType.D6,
                            DiceCountValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 4,
                                ValueRank = AbilityRankType.DamageDice,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            BonusValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 10,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                        },
                        IsAoE = false,
                        HalfIfSaved = false,
                        ResultSharedValue = AbilitySharedValue.Damage,
                        CriticalSharedValue = AbilitySharedValue.Damage
                    }
                    );
            });
        }
    }
}
