using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesOathbreaker {
    internal class OathbreakersBaneFeature {

        public static void AddOathbreakersBaneFeature() {
            var HellsDecreeAbilityMagicBuff = Resources.GetBlueprint<BlueprintBuff>("c695587d5307d234cb34f62750ff7616");
            var OBBaneIcon = AssetLoader.LoadInternal("Skills", "Icon_OBBane.png");
            var SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");
            var OathbreakersBaneBuff = Helpers.CreateBlueprint<BlueprintBuff>("OathbreakersBaneBuff", bp => {
                bp.SetName("Oathbreaker's Bane");
                bp.SetDescription("An Oathbreaker is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can channel her anger at one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her Oathbreaker level to damage rolls against the target of her bane. " +
              "In addition, while Oathbreaker's Bane is in effect, the Oathbreaker gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite.  The Oathbreaker's Bane effect remains until the target of the Oathbreaker's Bane is dead or the next time the Oathbreaker rests " +
              "and regains her uses of this ability. At 4th level and every 3 levels thereafter, the Oathbreaker can invoke her Oathbreaker's Bane " +
              "one additional time per day, to a maximum of seven times per day at 19th level.");
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.m_Icon = OBBaneIcon;
                bp.FxOnStart = HellsDecreeAbilityMagicBuff.FxOnStart;
                bp.FxOnRemove = HellsDecreeAbilityMagicBuff.FxOnRemove;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCaster = true;
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCaster = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<ACBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCaster = true;

                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<UniqueBuff>();

            });
            var OathbreakerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("OathbreakerClass");
            var OathbreakersBaneResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OathbreakersBaneResource", bp => {
                bp.m_Min = 1;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,

                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[1] {
                        OathbreakerClass.ToReference<BlueprintCharacterClassReference>() },
                    m_ClassDiv = new BlueprintCharacterClassReference[1] {
                        OathbreakerClass.ToReference<BlueprintCharacterClassReference>() },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0],

                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 4,
                    LevelStep = 3,
                    PerStepIncrease = 1,
                    StartingIncrease = 1

                };
                bp.m_Max = 10;

            });
            var FiendishSmiteGoodAbility = Resources.GetBlueprint<BlueprintAbility>("478cf0e6c5f3a4142835faeae3bd3e04");
            var SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
            var OathbreakersBaneAbility = Helpers.CreateBlueprint<BlueprintAbility>("OathbreakersBaneAbility", bp => {
                bp.SetName("Oathbreaker's Bane");
                bp.SetDescription("An Oathbreaker is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can channel her anger at one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her Oathbreaker level to damage rolls against the target of her bane. " +
              "In addition, while Oathbreaker's Bane is in effect, the Oathbreaker gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite.  The Oathbreaker's Bane effect remains until the target of the Oathbreaker's Bane is dead or the next time the Oathbreaker rests " +
              "and regains her uses of this ability. At 4th level and every 3 levels thereafter, the Oathbreaker can invoke her Oathbreaker's Bane " +
              "one additional time per day, to a maximum of seven times per day at 19th level.");
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Until the target of the bane is dead");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.m_Icon = OBBaneIcon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Medium;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Heighten | Metamagic.Reach;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {
                                    new ContextConditionTargetIsEngaged(),
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = OathbreakersBaneBuff.ToReference<BlueprintBuffReference>(),
                                        Not = true
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OathbreakersBaneBuff.ToReference<BlueprintBuffReference>(),
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
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueShared = AbilitySharedValue.StatBonus
                        },

                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.DamageBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus,
                            ValueShared = AbilitySharedValue.DamageBonus,

                        },

                    };
                    c.Modifier = 1;
                });
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;
                }));
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_Max = 20;
                }));

                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OathbreakersBaneResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });

            var SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
            var OathbreakersBaneFeature = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersBaneFeature", bp => {
                bp.SetName("Oathbreaker's Bane");
                bp.SetDescription("An Oathbreaker is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can channel her anger at one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her Oathbreaker level to damage rolls against the target of her bane. " +
              "In addition, while Oathbreaker's Bane is in effect, the Oathbreaker gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite.  The Oathbreaker's Bane effect remains until the target of the Oathbreaker's Bane is dead or the next time the Oathbreaker rests " +
              "and regains her uses of this ability. At 4th level and every 3 levels thereafter, the Oathbreaker can invoke her Oathbreaker's Bane " +
              "one additional time per day, to a maximum of seven times per day at 19th level.");
                bp.m_DescriptionShort = Helpers.CreateString("$OathbreakersBaneFeature.DescriptionShort", "Once per day as a swift action, she can channel her anger at one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her Oathbreaker level to damage rolls against the target of her bane. " +
              "In addition, while Oathbreaker's Bane is in effect, the Oathbreaker gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = OBBaneIcon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OathbreakersBaneResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OathbreakersBaneAbility.ToReference<BlueprintUnitFactReference>(),

                    };
                });
            });
            var OathbreakersBaneUse = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersBaneUse", bp => {
                bp.SetName("Oathbreaker's Bane - Additional Use");
                bp.SetDescription("An Oathbreaker is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can channel her anger at one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her Oathbreaker level to damage rolls against the target of her bane. " +
              "In addition, while Oathbreaker's Bane is in effect, the Oathbreaker gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite.  The Oathbreaker's Bane effect remains until the target of the Oathbreaker's Bane is dead or the next time the Oathbreaker rests " +
              "and regains her uses of this ability. At 4th level and every 3 levels thereafter, the Oathbreaker can invoke her Oathbreaker's Bane " +
              "one additional time per day, to a maximum of seven times per day at 19th level.");
                bp.m_Icon = OBBaneIcon;
                bp.Ranks = 10;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = OathbreakersBaneResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
            });


        }



    }
}

