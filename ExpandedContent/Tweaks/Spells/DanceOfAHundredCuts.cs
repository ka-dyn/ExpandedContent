using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
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
    internal class DanceOfAHundredCuts {
        public static void AddDanceOfAHundredCuts() {
            var DanceOfAHundredCutsIcon = AssetLoader.LoadInternal("Skills", "Icon_DanceOfAHundredCuts.jpg");
            var Icon_ScrollOfDanceOfAHundredCuts = AssetLoader.LoadInternal("Items", "Icon_ScrollOfDanceOfAHundredCuts.png");



            var DanceOfAHundredCutsToken = Helpers.CreateBuff("DanceOfAHundredCutsToken", bp => {
                bp.SetName("Token");
                bp.SetDescription("");
                bp.m_Icon = DanceOfAHundredCutsIcon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });


            var DanceOfAHundredCutsBuff = Helpers.CreateBuff("DanceOfAHundredCutsBuff", bp => {
                bp.SetName("Dance of a Hundred Cuts");
                bp.SetDescription("You become a lethal combat dancer, swirling and spinning with grace and precision. You gain a morale bonus on melee attack rolls, melee damage rolls, and " +
                    "Mobility checks, and to Armor Class. This bonus is equal to +1 per 3 caster levels (maximum +5 at 15th level). You must remain moving for the spell to stay in effect. " +
                    "If in any round you do not either move at least 10 feet or make a melee attack, the spell’s duration ends.");
                bp.m_Icon = DanceOfAHundredCutsIcon;
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
                            m_Buff = DanceOfAHundredCutsToken.ToReference<BlueprintBuffReference>()
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
                    c.CheckWeaponBlueprint = false;
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
                        new ContextActionRemoveBuff() {
                            m_Buff = DanceOfAHundredCutsToken.ToReference<BlueprintBuffReference>(),
                            ToCaster = true
                        });
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DanceOfAHundredCutsToken.ToReference<BlueprintBuffReference>(),
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
                                        m_Buff = DanceOfAHundredCutsToken.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRemoveSelf()
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DanceOfAHundredCutsToken.ToReference<BlueprintBuffReference>(),
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
            var DanceOfAHundredCutsAbility = Helpers.CreateBlueprint<BlueprintAbility>("DanceOfAHundredCutsAbility", bp => {
                bp.SetName("Dance of a Hundred Cuts");
                bp.SetDescription("You become a lethal combat dancer, swirling and spinning with grace and precision. You gain a morale bonus on melee attack rolls, melee damage rolls, and " +
                    "Mobility checks, and to Armor Class. This bonus is equal to +1 per 3 caster levels (maximum +5 at 15th level). You must remain moving for the spell to stay in effect. " +
                    "If in any round you do not either move at least 10 feet or make a melee attack, the spell’s duration ends.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
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
                bp.m_Icon = DanceOfAHundredCutsIcon;
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
                bp.LocalizedDuration = Helpers.CreateString("DanceOfAHundredCutsAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DanceOfAHundredCutsScroll = ItemTools.CreateScroll("ScrollOfDanceOfAHundredCuts", Icon_ScrollOfDanceOfAHundredCuts, DanceOfAHundredCutsAbility, 4, 10);
            VenderTools.AddScrollToLeveledVenders(DanceOfAHundredCutsScroll);
            DanceOfAHundredCutsAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 4);
        }
    }
}
