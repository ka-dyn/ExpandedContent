using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Enums;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using kadynsWOTRMods.Utilities;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods;

namespace kadynsTweaks.NewWIP.VindictiveBastard {
    class VindictiveSmiteAbility {

        private static readonly BlueprintFeature SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
        private static readonly BlueprintAbility SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
        private static readonly BlueprintBuff SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");
        private static readonly BlueprintArchetype VindictiveBastardArchetype = Resources.GetModBlueprint<BlueprintArchetype>("VindictiveBastardArchetype");
        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintArchetype VindictiveBastardVindictiveSmiteBuff = Resources.GetModBlueprint<BlueprintArchetype>("VindictiveBastardVindictiveSmiteBuff");
        private static readonly BlueprintAbilityResource VindictiveBastardVindictiveSmiteResource = Resources.GetModBlueprint<BlueprintAbilityResource>("VindictiveBastardVindictiveSmiteResource");

        public static void AddVindictiveSmiteAbility() {
            var SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
            var VindictiveBastardVindictiveSmiteAbility = Helpers.CreateBlueprint<BlueprintAbility>("VindictiveBastardVindictiveSmiteAbility", bp => {
                bp.SetName("Vindictive Smite");
                bp.SetDescription("A vindictive bastard is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can smite one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her paladin level to damage rolls against the target of her smite. " +
              "In addition, while vindictive smite is in effect, the vindictive bastard gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite. If the target of vindictive smite has rendered an ally of the vindictive bastard " +
              "unconscious or dead within the last 24 hours, the bonus on damage rolls on the first attack that hits increases by 2 for every paladin " +
              "level she has. The vindictive smite effect remains until the target of the smite is dead or the next time the vindictive bastard rests " +
              "and regains her uses of this ability.At 4th level and every 3 levels thereafter, the vindictive bastard can invoke her vindictive smite " +
              "one additional time per day, to a maximum of seven times per day at 19th level." +
              "This replaces smite evil.");
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Until the target of the smite is dead");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "");
                bp.m_Icon = SmiteEvilAbility.Icon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Medium;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.ResourceAssetIds = SmiteEvilAbility.ResourceAssetIds;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.AvailableMetamagic = Metamagic.Heighten | Metamagic.Reach;
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.MaxClassLevelWithArchetype;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 3;
                    c.m_Max = 19;
                    c.m_Class = new BlueprintCharacterClassReference[] { PaladinClass.ToReference<BlueprintCharacterClassReference>()
                        };
                }));
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = VindictiveBastardVindictiveSmiteResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = SmiteEvilAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = SmiteEvilAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = SmiteEvilAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = SmiteEvilAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[]
                            {
                                new ContextConditionTargetIsEngaged(),
                                new ContextConditionHasBuffFromCaster()
                                {
                                    m_Buff = VindictiveBastardVindictiveSmiteBuff.ToReference<BlueprintBuffReference>(),
                                    Not = true
                                }
                            }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = VindictiveBastardVindictiveSmiteBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        m_IsExtendable = true,
                                        DiceCountValue = new ContextValue(),
                                        BonusValue = new ContextValue()

                                    }
                                }
                            ),
                            IfFalse = Helpers.CreateActionList(),

                        }
                   );
                });
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Stat = StatType.Charisma;
                    c.m_Min = 0;
                    c.m_UseMin = true;
                }));
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.StatBonus
                        },

                    };
                    c.Modifier = 1;
                });
            });

        }
        public static void AddVindictiveSmiteResource() {

            var VindictiveBastardVindictiveSmiteResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("VindictiveBastardVindictiveSmiteResource", bp => {
                bp.m_Min = 1;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        PaladinClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        VindictiveBastardArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 4,
                    LevelStep = 3,
                    PerStepIncrease = 1,
                    StartingIncrease = 1

                };

            });

        }
    }
}
