using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
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
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Craft;

namespace ExpandedContent.Tweaks.Spirits.Hexes {
    internal class ErosionCurseHex {
        public static void AddErosionCurseHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var ShamanNatureSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("379d33ef16e812146b5517601ab25e66");
            var ShamanNatureSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("54943a544ec1b31438366aaf73e5f310");
            var ShamanNatureSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("0a7166e9c6ea5874e9fc4984f30f2d8d");

            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var OracleRevelationErosionTouchIcon = Resources.GetBlueprint<BlueprintAbility>("f9fa310c8c0f8784e94b6ae265f7b921").Icon;

            var ShamanHexErosionCurseCooldown = Helpers.CreateBuff("ShamanHexErosionCurseCooldown", bp => {
                bp.SetName("Already damaged by this hex today");
                bp.SetDescription("");
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var ShamanHexErosionCurseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexErosionCurseAbility", bp => {
                bp.SetName("Erosion Curse");
                bp.SetDescription("The shaman summons the powers of nature to erode a construct or object within 30 feet. " +
                    "This erosion deals 1d6 points of damage per 2 shaman levels, ignoring hardness and damage reduction. " +
                    "The Target can attempt a Reflex saving throw to halve the damage. Once an object or a construct is " +
                    "damaged by this erosion, it cannot be the target of this hex again for 24 hours.");
                bp.m_Icon = OracleRevelationErosionTouchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Reflex,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(
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
                                                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Cold,
                                                Type = DamageType.Direct
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
                                            Half = true
                                        }
                                    ),
                                    Failed = Helpers.CreateActionList(
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
                                                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Cold,
                                                Type = DamageType.Direct
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
                                        }
                                    )
                                }
                                )
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexErosionCurseCooldown.ToReference<BlueprintBuffReference>(),
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
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ShamanHexErosionCurseCooldown.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                    c.FromCaster = true;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ConstructType.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Wisdom;
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ShamanHexErosionCurseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexErosionCurseFeature", bp => {
                bp.SetName("Erosion Curse");
                bp.SetDescription("The shaman summons the powers of nature to erode a construct or object within 30 feet. " +
                    "This erosion deals 1d6 points of damage per 2 shaman levels, ignoring hardness and damage reduction. " +
                    "The Target can attempt a Reflex saving throw to halve the damage. Once an object or a construct is " +
                    "damaged by this erosion, it cannot be the target of this hex again for 24 hours.");
                bp.m_Icon = OracleRevelationErosionTouchIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexErosionCurseAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanNatureSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanNatureSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanNatureSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexErosionCurseFeature);

            ShamanNatureSpiritProgression.IsPrerequisiteFor.Add(ShamanHexErosionCurseFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
