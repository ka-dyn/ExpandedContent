using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class DanceOfAThousandCuts {
        public static void AddDanceOfAThousandCuts() {
            var DanceOfAThousandCutsIcon = AssetLoader.LoadInternal("Skills", "Icon_DanceOfAThousandCuts.jpg");
            var Icon_ScrollOfDanceOfAThousandCuts = AssetLoader.LoadInternal("Items", "Icon_ScrollOfDanceOfAThousandCuts.png");

            var SlowBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("0bc608c3f2b548b44b7146b7530613ac");
            var DanceOfAHundredCutsBuff = Resources.GetModBlueprint<BlueprintBuff>("DanceOfAHundredCutsBuff");
            var DanceOfAHundredCutsAbility = Resources.GetModBlueprint<BlueprintAbility>("DanceOfAHundredCutsAbility").GetComponent<AbilityEffectRunAction>();
            var DanceOfAThousandCutsToken = Helpers.CreateBuff("DanceOfAThousandCutsToken", bp => {
                bp.SetName("Token");
                bp.SetDescription("");
                bp.m_Icon = DanceOfAThousandCutsIcon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.IsFromSpell;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var DanceOfAThousandCutsBuff = Helpers.CreateBuff("DanceOfAThousandCutsBuff", bp => {
                bp.SetName("Dance of a Thousand Cuts");
                bp.SetDescription("This spell functions as dance of a hundred cuts, except you also gain the benefits of haste. \nYou become a lethal combat dancer, swirling and spinning " +
                    "with grace and precision. You gain a morale bonus on melee attack rolls, melee damage rolls, and Mobility checks, and to Armor Class. This bonus is equal to +1 per 3 " +
                    "caster levels (maximum +5 at 15th level). You must remain moving for the spell to stay in effect. If in any round you do not either move at least 10 feet or make a " +
                    "melee attack, the spell’s duration ends.");
                bp.m_Icon = DanceOfAThousandCutsIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.Stat = StatType.AC;
                    c.Value = 1;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 1;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = 1;
                });
                bp.AddComponent<BuffExtraAttack>(c => {
                    c.Number = 1;
                    c.Haste = true;
                });
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Value = 30;
                    c.ContextBonus = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 0
                    };
                    c.CappedOnMultiplier = true;
                    c.MultiplierCap = 2;
                    c.CappedMinimum = false;
                });
                bp.AddComponent<ChangeImpatience>(c => {
                    c.Delta = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Stat = StatType.SkillMobility;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage
                    };
                });
                bp.AddComponent<WeaponAttackTypeDamageBonus>(c => {
                    c.Type = WeaponRangeType.Melee;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.AttackBonus = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage
                    };
                });
                bp.AddComponent<AttackTypeAttackBonus>(c => {
                    c.Type = WeaponRangeType.Melee;
                    c.AllTypesExcept = false;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.AttackBonus = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage
                    };
                    c.CheckFact = false;
                    c.m_RequiredFact = null;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 3;
                    c.m_UseMax = true;
                    c.m_Max = 5;
                });
                bp.AddComponent<MovementDistanceTrigger>(c => {
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = DanceOfAThousandCutsToken.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.DistanceInFeet = new ContextValue() { Value = 10 };
                    c.LimitTiggerCountInOneRound = true;
                    c.TiggerCountMaximumInOneRound = 1;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = true;
                    c.OnlyHit = false;
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
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = DanceOfAThousandCutsToken.ToReference<BlueprintBuffReference>()
                                })
                        });
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DanceOfAThousandCutsToken.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = DanceOfAThousandCutsToken.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRemoveSelf()
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DanceOfAThousandCutsToken.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 1,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                }
                                )
                        }
                        );
                });
                bp.FxOnStart = new PrefabLink() { AssetId = "91ef30ab58fa0d3449d4d2ccc20cb0f8" };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var DanceOfAThousandCutsAbility = Helpers.CreateBlueprint<BlueprintAbility>("DanceOfAThousandCutsAbility", bp => {
                bp.SetName("Dance of a Thousand Cuts");
                bp.SetDescription("This spell functions as dance of a hundred cuts, except you also gain the benefits of haste. \nYou become a lethal combat dancer, swirling and spinning " +
                    "with grace and precision. You gain a morale bonus on melee attack rolls, melee damage rolls, and Mobility checks, and to Armor Class. This bonus is equal to +1 per 3 " +
                    "caster levels (maximum +5 at 15th level). You must remain moving for the spell to stay in effect. If in any round you do not either move at least 10 feet or make a " +
                    "melee attack, the spell’s duration ends.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = DanceOfAHundredCutsBuff.ToReference<BlueprintBuffReference>() 
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = SlowBuff
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DanceOfAHundredCutsBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        },
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DanceOfAThousandCutsBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        },
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
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
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = DanceOfAThousandCutsIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("DanceOfAThousandCutsAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            DanceOfAHundredCutsAbility.Actions.Actions = DanceOfAHundredCutsAbility.Actions.Actions.AppendToArray(new ContextActionRemoveBuff() {
                m_Buff = DanceOfAThousandCutsBuff.ToReference<BlueprintBuffReference>()
            });

            var DanceOfAThousandCutsScroll = ItemTools.CreateScroll("ScrollOfDanceOfAThousandCuts", Icon_ScrollOfDanceOfAThousandCuts, DanceOfAThousandCutsAbility, 6, 16);
            VenderTools.AddScrollToLeveledVenders(DanceOfAThousandCutsScroll);
            DanceOfAThousandCutsAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 6);
        }
    }
}
