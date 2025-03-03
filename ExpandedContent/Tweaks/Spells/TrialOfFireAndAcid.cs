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
    internal class TrialOfFireAndAcid {
        public static void AddTrialOfFireAndAcid() {
            var TrialOfFireAndAcidIcon = AssetLoader.LoadInternal("Skills", "Icon_TrialOfFireAndAcid.jpg");
            var Icon_ScrollOfTrialOfFireAndAcid = AssetLoader.LoadInternal("Items", "Icon_ScrollOfTrialOfFireAndAcid.png");

            var TrialOfFireAndAcidBuff = Helpers.CreateBuff("TrialOfFireAndAcidBuff", bp => {
                bp.SetName("Trial of Fire and Acid");
                bp.SetDescription("The target creature is covered in burning acid that deals 1d6 points of acid damage and 1d6 points of fire damage each round. " +
                    "The subject can attempt a Fortitude saving throw each round to reduce the damage by half.");
                bp.m_Icon = TrialOfFireAndAcidIcon;
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
                                                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire,
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
                                            Half = true
                                        },
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
                                                DiceCountValue = 1,
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
                                                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire,
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
                                            Half = false
                                        },
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
                                                DiceCountValue = 1,
                                                BonusValue = 0,
                                            },
                                            ResultSharedValue = AbilitySharedValue.Damage,
                                            CriticalSharedValue = AbilitySharedValue.Damage,
                                            Half = false
                                        }
                                    )
                                }
                            )
                        }                        
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var TrialOfFireAndAcidAbility = Helpers.CreateBlueprint<BlueprintAbility>("TrialOfFireAndAcidAbility", bp => {
                bp.SetName("Trial of Fire and Acid");
                bp.SetDescription("The target creature is covered in burning acid that deals 1d6 points of acid damage and 1d6 points of fire damage each round. " +
                    "The subject can attempt a Fortitude saving throw each round to reduce the damage by half.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TrialOfFireAndAcidBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
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
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Acid | SpellDescriptor.Fire;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = TrialOfFireAndAcidIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("TrialOfFireAndAcidAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("TrialOfFireAndAcidAbility.SavingThrow", "Fortitude half (see text)");
            });
            //var TrialOfFireAndAcidScroll = ItemTools.CreateScroll("ScrollOfTrialOfFireAndAcid", Icon_ScrollOfTrialOfFireAndAcid, TrialOfFireAndAcidAbility, 3, 5);
            //VenderTools.AddScrollToLeveledVenders(TrialOfFireAndAcidScroll);
            TrialOfFireAndAcidAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 3);
            TrialOfFireAndAcidAbility.AddToSpellList(SpellTools.SpellList.ClericSpellList,3);
            TrialOfFireAndAcidAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 3);
            TrialOfFireAndAcidAbility.AddToSpellList(SpellTools.SpellList.ShamanSpelllist, 3);
            TrialOfFireAndAcidAbility.AddToSpellList(SpellTools.SpellList.WarpriestSpelllist, 3);
            TrialOfFireAndAcidAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 3);
            TrialOfFireAndAcidAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 3);
        }
    }
}
