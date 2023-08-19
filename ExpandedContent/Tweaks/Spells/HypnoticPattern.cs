using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class HypnoticPattern {
        public static void AddHypnoticPattern() {
            var HypnoticPatternIcon = AssetLoader.LoadInternal("Skills", "Icon_HypnoticPattern.png");
            var Icon_ScrollOfHypnoticPattern = AssetLoader.LoadInternal("Items", "Icon_ScrollOfHypnoticPattern.png");
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var UndeadType = Resources.GetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");
            var UndeadMindAffection = Resources.GetBlueprint<BlueprintFeature>("7853143d87baea1429bb409b023edb6b");

            var HypnoticPatternBuff = Helpers.CreateBuff("HypnoticPatternBuff", bp => {
                bp.SetName("Hypnotic Pattern");
                bp.SetDescription("A twisting pattern of subtle, shifting colors weaves through the air, fascinating creatures within it. Roll 2d4 and add your caster level " +
                    "(maximum 10) to determine the total number of HD of creatures affected. Creatures who are closest to the spell’s point of origin are affected first. HD that " +
                    "are not sufficient to affect a creature are wasted. Sightless creatures are not affected.");
                bp.m_Icon = HypnoticPatternIcon;                
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.Dazed;
                });
                bp.AddComponent<AddIncomingDamageTrigger>(c => {
                    c.Actions = Helpers.CreateActionList( new ContextActionRemoveSelf() );
                    c.TriggerOnStatDamageOrEnergyDrain = true;
                    c.IgnoreDamageFromThisFact = false;
                    c.ReduceBelowZero = false;
                    c.CheckDamageDealt = false;
                    c.CompareType = CompareOperation.Type.Equal;
                    c.TargetValue = 0; //?
                    c.CheckWeaponAttackType = false;
                    c.AttackType = 0;
                    c.CheckEnergyDamageType = false;
                    c.EnergyType = Kingmaker.Enums.Damage.DamageEnergyType.Fire;
                    c.CheckDamagePhysicalTypeNot = false;
                    c.DamagePhysicalTypeNot = 0;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Daze;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionSpawnFx() {
                            PrefabLink = new PrefabLink() { AssetId = "28b3cd92c1fdc194d9ee1e378c23be6b" }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<RemoveWhenCombatEnded>();
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var HypnoticPatternAbility = Helpers.CreateBlueprint<BlueprintAbility>("HypnoticPatternAbility", bp => {
                bp.SetName("Hypnotic Pattern");
                bp.SetDescription("A twisting pattern of subtle, shifting colors weaves through the air, fascinating creatures within it. Roll 2d4 and add your caster level " +
                    "(maximum 10) to determine the total number of HD of creatures affected. Creatures who are closest to the spell’s point of origin are affected first. HD that " +
                    "are not sufficient to affect a creature are wasted. Sightless creatures are not affected.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = ConstructType.ToReference<BlueprintUnitFactReference>(),
                                    },
                                    new ContextConditionIsUnconscious() {
                                        Not = false
                                    },
                                    new ContextConditionHitDice() {
                                        Not = true,
                                        HitDice = 0,
                                        AddSharedValue = true,
                                        SharedValue = AbilitySharedValue.Damage
                                    },
                                    new ContextConditionHasBuffWithDescriptor() {
                                        Not = false,
                                        SpellDescriptor = SpellDescriptor.Blindness
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.Or,
                                        Conditions = new Condition[] {
                                            new ContextConditionHasFact() {
                                                Not = false,
                                                m_Fact = UndeadType.ToReference<BlueprintUnitFactReference>()
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Operation = Operation.And,
                                                Conditions = new Condition[] {
                                                    new ContextConditionCasterHasFact() {
                                                        Not = false,
                                                        m_Fact = UndeadMindAffection.ToReference<BlueprintUnitFactReference>()
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionSavingThrow() {
                                                    Type = SavingThrowType.Will,
                                                    FromBuff = false,
                                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                                    HasCustomDC = false,
                                                    CustomDC = new ContextValue(),
                                                    Actions = Helpers.CreateActionList(
                                                        new ContextActionConditionalSaved() {
                                                            Succeed = new ActionList(),
                                                            Failed = Helpers.CreateActionList(
                                                                new ContextActionApplyBuff() {
                                                                    m_Buff = HypnoticPatternBuff.ToReference<BlueprintBuffReference>(),
                                                                    Permanent = false,
                                                                    DurationValue = new ContextDurationValue() {
                                                                        Rate = DurationRate.Rounds,
                                                                        DiceType = DiceType.Zero,
                                                                        DiceCountValue = new ContextValue(),
                                                                        BonusValue = new ContextValue() {
                                                                            ValueType = ContextValueType.Simple,
                                                                            Value = 3,
                                                                            ValueRank = AbilityRankType.Default,
                                                                            ValueShared = AbilitySharedValue.Damage,
                                                                            Property = UnitProperty.None
                                                                        },
                                                                        m_IsExtendable = true
                                                                    },
                                                                    IsFromSpell = true,
                                                                }),
                                                        },
                                                        new ContextActionChangeSharedValue() {
                                                            SharedValue = AbilitySharedValue.Damage,
                                                            Type = SharedValueChangeType.SubHD,
                                                            AddValue = new ContextValue(),
                                                            SetValue = new ContextValue(),
                                                            MultiplyValue = new ContextValue()
                                                        }
                                                        ),
                                                }
                                                ),
                                            IfFalse= Helpers.CreateActionList()
                                        }
                                        ),
                                    IfFalse= Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            Type = SavingThrowType.Will,
                                            FromBuff = false,
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            HasCustomDC = false,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = HypnoticPatternBuff.ToReference<BlueprintBuffReference>(),
                                                            Permanent = false,
                                                            DurationValue = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 3,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = true
                                                            },
                                                            IsFromSpell = true,
                                                        }),
                                                },
                                                new ContextActionChangeSharedValue() {
                                                    SharedValue = AbilitySharedValue.Damage,
                                                    Type = SharedValueChangeType.SubHD,
                                                    AddValue = new ContextValue(),
                                                    SetValue = new ContextValue(),
                                                    MultiplyValue = new ContextValue()
                                                }
                                                ),
                                            }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
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
                        DiceType = DiceType.D4,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 2,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                    };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() {  AssetId = "bbd6decdae32bce41ae8f06c6c5eb893" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = true;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 10.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Illusion;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = HypnoticPatternIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("HypnoticPatternAbility.Duration", "3 rounds");
                bp.LocalizedSavingThrow = Helpers.CreateString("HypnoticPatternAbility.SavingThrow", "Will negates");
            });
            var HypnoticPatternScroll = ItemTools.CreateScroll("ScrollOfHypnoticPattern", Icon_ScrollOfHypnoticPattern, HypnoticPatternAbility, 2, 3);
            VenderTools.AddScrollToLeveledVenders(HypnoticPatternScroll);
            HypnoticPatternAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 2);
            HypnoticPatternAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 2);
        }
    }
}
