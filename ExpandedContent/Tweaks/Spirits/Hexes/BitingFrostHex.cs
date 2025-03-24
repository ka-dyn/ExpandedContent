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

namespace ExpandedContent.Tweaks.Spirits.Hexes {
    internal class BitingFrostHex {
        public static void AddBitingFrostHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var ShamanFrostSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("44c130a12d49ebf4a95945feffe5aae7");
            var ShamanFrostSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("6aeab860502ca2643affcbf1a83187e0");
            var ShamanFrostSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("30dc785861c12374c910e5ede5b87ef4");

            var BurningHandsColdIcon = Resources.GetBlueprint<BlueprintAbility>("83ed16546af22bb43bd08734a8b51941").Icon;

            var ShamanHexBitingFrostCooldown = Helpers.CreateBuff("ShamanHexBitingFrostCooldown", bp => {
                bp.SetName("Already targeted by this hex today");
                bp.SetDescription("");
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var ShamanHexBitingFrostBuff = Helpers.CreateBuff("ShamanHexBitingFrostBuff", bp => {
                bp.SetName("Biting Frost");
                bp.SetDescription("The target must attempt a Fortitude saving throw at the beginning of each turn or be damaged by exposure to the extreme cold. " +
                    "On a failed save, the target takes 1d6 points of nonlethal damage. On a successful save, the effect ends immediately. " +
                    "Whether or not the initial save is successful, the creature cannot be the target of this hex again for 24 hours.");
                bp.m_Icon = BurningHandsColdIcon;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue() { },
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(
                                        new ContextActionRemoveSelf()
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
                                                DiceCountValue = 1,
                                                BonusValue = 0,
                                            },
                                            ResultSharedValue = AbilitySharedValue.Damage,
                                            CriticalSharedValue = AbilitySharedValue.Damage,
                                            Half = false,
                                            MinHPAfterDamage = 1
                                        }
                                    )
                                }
                            )
                        }
                        );
                });
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });


            var ShamanHexBitingFrostAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexBitingFrostAbility", bp => {
                bp.SetName("Biting Frost");
                bp.SetDescription("The shaman turns the air frigid around a target within medium range for a number of rounds equal to the shaman’s Charisma modifier (minimum 1). " +
                    "The target must attempt a Fortitude saving throw at the beginning of each turn or be damaged by exposure to the extreme cold. " +
                    "On a failed save, the target takes 1d6 points of nonlethal damage. On a successful save, the effect ends immediately. " +
                    "Whether or not the initial save is successful, the creature cannot be the target of this hex again for 24 hours.");
                bp.m_Icon = BurningHandsColdIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexBitingFrostBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.StatBonus,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexBitingFrostCooldown.ToReference<BlueprintBuffReference>(),
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
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ShamanHexBitingFrostCooldown.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                    c.FromCaster = true;
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

            var ShamanHexBitingFrostFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexBitingFrostFeature", bp => {
                bp.SetName("Biting Frost");
                bp.SetDescription("The shaman turns the air frigid around a target within medium range for a number of rounds equal to the shaman’s Charisma modifier (minimum 1). " +
                    "The target must attempt a Fortitude saving throw at the beginning of each turn or be damaged by exposure to the extreme cold. " +
                    "On a failed save, the target takes 1d6 points of nonlethal damage. On a successful save, the effect ends immediately. " +
                    "Whether or not the initial save is successful, the creature cannot be the target of this hex again for 24 hours.");
                bp.m_Icon = BurningHandsColdIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexBitingFrostAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanFrostSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanFrostSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanFrostSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexBitingFrostFeature);

            ShamanFrostSpiritProgression.IsPrerequisiteFor.Add(ShamanHexBitingFrostFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
