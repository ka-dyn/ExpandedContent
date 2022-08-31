using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class RigorMortis {
        public static void AddRigorMortis() {

            var FiendTotemFeature = Resources.GetBlueprint<BlueprintFeature>("ce449404eeb4a7c499fbe0248056174f");

            var RigorMortisBuff = Helpers.CreateBuff("RigorMortisBuff", bp => {
                bp.SetName("Rigor Mortis");
                bp.SetDescription("The joints of a creature affected by this spell stiffen and swell, making movement painful and slow. The target takes a –4 penalty to Dexterity and " +
                    "its movement speed decreases by 10 feet; these effects last for 1 minute per level of the caster.");
                bp.m_Icon = FiendTotemFeature.m_Icon;
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Dexterity;
                    c.Value = -4;
                });
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = -10;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });

            var RigorMortisAbility = Helpers.CreateBlueprint<BlueprintAbility>("RigorMortisAbility", bp => {
                bp.SetName("Rigor Mortis");
                bp.SetDescription("The joints of a creature affected by this spell stiffen and swell, making movement painful and slow. The target takes 1d6 points of " +
                    "nonlethal damage per caster level. Additionally, the target takes a –4 penalty to Dexterity and its movement speed decreases by 10 feet; these additional " +
                    "effects last for 1 minute per caster level. A successful save halves the nonlethal damage and negates the penalty to Dexterity and movement.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
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
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = true,
                            UseMinHPAfterDamage = true,
                            MinHPAfterDamage = 1,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(                                
                                new ContextActionApplyBuff() {
                                    m_Buff = RigorMortisBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Minutes,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage
                                        }
                                    },
                                    DurationSeconds = 0
                                }
                            )
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = FiendTotemFeature.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("RigorMortisAbility.SavingThrow", "Fortitude partial");
            });
            RigorMortisAbility.AddToSpellList(SpellTools.SpellList.ClericSpellList, 4);
            RigorMortisAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 4);
            RigorMortisAbility.AddToSpellList(SpellTools.SpellList.ShamanSpelllist, 4);
            RigorMortisAbility.AddToSpellList(SpellTools.SpellList.WarpriestSpelllist, 4);
            RigorMortisAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
            RigorMortisAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 4);

        }
    }
}
