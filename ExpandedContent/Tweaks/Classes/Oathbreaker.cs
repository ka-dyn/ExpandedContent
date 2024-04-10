using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.Utility;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Classes {

    internal class OathbreakerClass {
        public static void AddOathbreakerClass() {
            
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
            // Progression
            var OathbreakerProgression = Helpers.CreateBlueprint<BlueprintProgression>("OathbreakerProgression", bp => {
                bp.SetName("Oathbreaker");
                bp.SetDescription("While paladins often collaborate with less righteous adventurers in order to further their causes, those who " +
                    "spend too much time around companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies " +
                    "and methods. Such an Oathbreaker, as these fallen paladins are known, strikes out for retribution and revenge, far more interested " +
                    "in tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Main Class
            var OathbreakerClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("OathbreakerClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"OathbreakerClass.Name", "Oathbreaker");
                bp.LocalizedDescription = Helpers.CreateString($"OathbreakerClass.Description", "While paladins often " +
                    "collaborate with less righteous adventurers in order to further their causes, those who spend too much time around " +
                    "companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies and methods. Such an " +
                    "Oathbreaker, as these fallen paladins are known, strikes out for retribution and revenge, far more interested in " +
                    "tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"OathbreakerClass.Description", "While paladins often " +
                    "collaborate with less righteous adventurers in order to further their causes, those who spend too much time around " +
                    "companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies and methods. Such an " +
                    "Oathbreaker, as these fallen paladins are known, strikes out for retribution and revenge, far more interested in " +
                    "tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");
                bp.HitDie = Kingmaker.RuleSystem.DiceType.D10;
                bp.m_BaseAttackBonus = PaladinClass.m_BaseAttackBonus;
                bp.m_FortitudeSave = PaladinClass.m_FortitudeSave;
                bp.m_PrototypeId = PaladinClass.m_PrototypeId;
                bp.m_ReflexSave = PaladinClass.m_ReflexSave;
                bp.m_WillSave = PaladinClass.m_WillSave;
                bp.m_Progression = OathbreakerProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.NotRecommendedAttributes = PaladinClass.NotRecommendedAttributes;
                bp.m_Spellbook = null;
                bp.m_EquipmentEntities = PaladinClass.m_EquipmentEntities;
                bp.m_StartingItems = PaladinClass.StartingItems;                
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.m_DefaultBuild = null;
                bp.m_Archetypes = new BlueprintArchetypeReference[] {};
                bp.SkillPoints = 3;
                bp.ClassSkills = new StatType[4] {
                    StatType.SkillKnowledgeWorld, 
                    StatType.SkillLoreNature,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillLoreReligion
                };
                bp.IsDivineCaster = true;
                bp.IsArcaneCaster = false;
                bp.StartingGold = 411;
                bp.PrimaryColor = 6;
                bp.SecondaryColor = 11;
                bp.MaleEquipmentEntities = PaladinClass.MaleEquipmentEntities;
                bp.FemaleEquipmentEntities = PaladinClass.FemaleEquipmentEntities;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.AddComponent<PrerequisiteNoClassLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Not = true;
                    c.HideInUI = true;
                });
            });
            OathbreakerProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                new BlueprintProgression.ClassWithLevel { 
                    m_Class = OathbreakerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                }
            };
            // Proficiencies
            var OBProf = AssetLoader.LoadInternal("Skills", "Icon_OBProf.png");
            var OathbreakerProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakerProficiencies", bp => {
                bp.SetName("Oathbreaker Proficiences");
                bp.SetDescription("Oathbreakers are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with all types of armor " +
                    "(heavy, medium, and light), and with shields (except tower shields).");
                bp.m_Icon = OBProf;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PaladinClassProficiencies.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            // Oathbreakers Bane
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
                            ValueShared = AbilitySharedValue.DamageBonus
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
                        OathbreakersBaneAbility.ToReference<BlueprintUnitFactReference>()
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
            // Oathbreakers Direction
            var ODIcon = AssetLoader.LoadInternal("Skills", "Icon_OD.png");
            var OathbreakersDirectionBuffAllies = Helpers.CreateBuff("OathbreakersDirectionBuffAllies", bp => { //Removed but still here to not break saves
                bp.SetName("Oathbreaker's Direction");
                bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_Icon = ODIcon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.CheckCasterFriend = true;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCasterFriend = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
            });
            var HellSealVariantDevouringFlamesBuff = Resources.GetBlueprint<BlueprintBuff>("5617dbbb3890e2f4b96b47318c5c438b");
            var OathbreakersDirectionBuff = Helpers.CreateBuff("OathbreakersDirectionBuff", bp => {
                bp.SetName("Oathbreaker's Direction");
                bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_Icon = ODIcon;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {                                       
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.DamageBonus,
                        Property = UnitProperty.None,
                    };
                    c.CheckCaster = true;
                    c.CheckCasterFriend = true;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.DamageBonus,
                        Property = UnitProperty.None,
                    };
                    c.CheckCaster = true;
                    c.CheckCasterFriend = true;
                    c.ApplyToSpellDamage = false;
                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<UniqueBuff>();
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Replace;
                bp.Ranks = 0;
                bp.TickEachSecond = false;
                bp.FxOnStart = HellSealVariantDevouringFlamesBuff.FxOnStart;
            });
            var OathbreakersDirectionAbility = Helpers.CreateBlueprint<BlueprintAbility>("OathbreakersDirectionAbility", bp => {
                bp.SetName("Oathbreaker's Direction");
                bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_Icon = ODIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OathbreakersDirectionBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() { m_IsExtendable = true },
                            AsChild = true,
                        });
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.DamageBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus,
                            ValueShared = AbilitySharedValue.DamageBonus,
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 1, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 2, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 3, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 4, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 5, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 6, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 9, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 10, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 11, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 12, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 13, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 15, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 16, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 17, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 18, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 19, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 5 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 5 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[1] { 
                        OathbreakerClass.ToReference<BlueprintCharacterClassReference>() 
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Until the directed target is dead");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetFriends = false;
                bp.CanTargetEnemies = true;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = Metamagic.Heighten | Metamagic.Reach;
            });
            var OathbreakersDirectionAbilitySwift = Helpers.CreateBlueprint<BlueprintAbility>("OathbreakersDirectionAbilitySwift", bp => {
                bp.SetName("Oathbreaker's Direction (Swift)");
                bp.SetDescription("At 11th level, an Oathbreaker can active her Oathbreaker's Direction ability " +
                    "as a swift action.");
                bp.m_Icon = ODIcon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Medium;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.CanTargetEnemies = true;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.AvailableMetamagic = Metamagic.Heighten | Metamagic.Reach;
                bp.ComponentsArray = OathbreakersDirectionAbility.ComponentsArray;
            });
            var OathbreakersDirectionSwiftFeature = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersDirectionSwiftFeature", bp => {
                bp.SetName("Oathbreaker's Direction (Swift)");
                bp.SetDescription("At 11th level, the Oathbreaker can, as a swift action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_Icon = ODIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { OathbreakersDirectionAbilitySwift.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var OathbreakersDirectionFeature = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersDirectionFeature", bp => {
                bp.SetName("Oathbreaker's Direction");
                bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_DescriptionShort = Helpers.CreateString("$OathbreakersDirection.DescriptionShort", "At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = ODIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { OathbreakersDirectionAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var OathbreakersDirectionIncrease = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersDirectionIncrease", bp => {
                bp.SetName("Oathbreaker's Direction - Bonus Increase");
                bp.SetDescription("At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_DescriptionShort = Helpers.CreateString("$OathbreakersDirection.DescriptionShort", "At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target.");
                bp.Ranks = 4;
                bp.IsClassFeature = true;
                bp.m_Icon = ODIcon;
            });
            // Faded Grace
            var FadedGraceIcon = AssetLoader.LoadInternal("Skills", "Icon_FadedGrace.png");
            var IronWill = Resources.GetBlueprint<BlueprintFeature>("175d1577bb6c9a04baf88eec99c66334").ToReference<BlueprintFeatureReference>();
            var GreatFortitude = Resources.GetBlueprint<BlueprintFeature>("79042cb55f030614ea29956177977c52").ToReference<BlueprintFeatureReference>();
            var LightningReflexes = Resources.GetBlueprint<BlueprintFeature>("15e7da6645a7f3d41bdad7c8c4b9de1e").ToReference<BlueprintFeatureReference>();            
            var FadedGrace = Helpers.CreateBlueprint<BlueprintFeatureSelection>("FadedGrace", bp => {
                bp.SetName("Faded Grace");
                bp.SetDescription("At 2nd level, an Oathbreaker gains one of the following as a bonus feat: Great Fortitude, " +
                    "Iron Will, or Lightning Reflexes.");
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.HideNotAvailibleInUI = true;
                bp.m_Icon = FadedGraceIcon;
                bp.m_Features = new BlueprintFeatureReference[] {
                    GreatFortitude,
                    IronWill,
                    LightningReflexes
                    };
                bp.m_AllFeatures = bp.m_Features;
            });
            // Solo Tactics
            var BattleMeditationAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a7a3303c8ab81914e8ecca76aedc70ec");
            var OathbreakerSoloTactics = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakerSoloTactics", bp => {
                bp.SetName("Solo Tactics");
                bp.SetDescription("At 2nd level, all of the Oathbreaker's allies are treated as if they possessed the same teamwork {g|Encyclopedia:Feat}feats{/g} as the Oathbreaker " +
                    "for the purpose of determining whether the Oathbreaker receives a {g|Encyclopedia:Bonus}bonus{/g} from her teamwork feats. Her allies do not receive any bonuses " +
                    "from these feats unless they actually possess the feats themselves. The allies' positioning and {g|Encyclopedia:CA_Types}actions{/g} must still meet the " +
                    "prerequisites listed in the teamwork feat for the Oathbreaker to receive the listed bonus.");
                bp.m_DescriptionShort = Helpers.CreateString("$OathbreakersSoloTactics.DescriptionShort", "At 2nd level, all of the Oathbreaker's allies are treated as if they " +
                    "possessed the same teamwork {g|Encyclopedia:Feat}feats{/g} as the Oathbreaker " +
                    "for the purpose of determining whether the Oathbreaker receives a {g|Encyclopedia:Bonus}bonus{/g} from her teamwork feats. Her allies do not receive any bonuses " +
                    "from these feats unless they actually possess the feats themselves. The allies' positioning and {g|Encyclopedia:CA_Types}actions{/g} must still meet the " +
                    "prerequisites listed in the teamwork feat for the Oathbreaker to receive the listed bonus.");
                bp.m_Icon = BattleMeditationAbility.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.SoloTactics;
                });
            });
            // Spiteful Tenacity
            var SpitefulIcon = AssetLoader.LoadInternal("Skills", "Icon_SpitefulTenacity.png");
            var Diehard = Resources.GetBlueprint<BlueprintFeature>("86669ce8759f9d7478565db69b8c19ad");
            var SpitefulTenacity = Helpers.CreateBlueprint<BlueprintFeature>("SpitefulTenacity", bp => {
                bp.SetName("Spiteful Tenacity");
                bp.SetDescription("At 3rd level the Oathbreaker receives the Diehard feat for free.");
                bp.m_Icon = SpitefulIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { Diehard.ToReference<BlueprintUnitFactReference>() };
                });
            });
            // Oathbreaker Teamwork Feats
            var AlliedSpellcaster = Resources.GetBlueprint<BlueprintFeature>("9093ceeefe9b84746a5993d619d7c86f");
            var BackToBack = Resources.GetBlueprint<BlueprintFeature>("c920f2cd2244d284aa69a146aeefcb2c");
            var CoordinatedDefense = Resources.GetBlueprint<BlueprintFeature>("992fd59da1783de49b135ad89142c6d7");
            var CoordinatedManuevers = Resources.GetBlueprint<BlueprintFeature>("b186cea78dce3a04aacff0a81786008c");
            var Outflank = Resources.GetBlueprint<BlueprintFeature>("422dab7309e1ad343935f33a4d6e9f11");
            var PreciseStrike = Resources.GetBlueprint<BlueprintFeature>("5662d1b793db90c4b9ba68037fd2a768");
            var ShakeItOff = Resources.GetBlueprint<BlueprintFeature>("6337b37f2a7c11b4ab0831d6780bce2a");
            var ShieldedCaster = Resources.GetBlueprint<BlueprintFeature>("0b707584fc2ea724aa72c396c2230dc7");
            var ShieldWall = Resources.GetBlueprint<BlueprintFeature>("8976de442862f82488a4b138a0a89907");
            var SiezeTheMoment = Resources.GetBlueprint<BlueprintFeature>("1191ef3065e6f8e4f9fbe1b7e3c0f760");
            var TandemTrip = Resources.GetBlueprint<BlueprintFeature>("d26eb8ab2aabd0e45a4d7eec0340bbce");
            var TeamworkFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("d87e2f6a9278ac04caeb0f93eff95fcb");
            var OathbreakerTeamworkFeat = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OathbreakerTeamworkFeat", bp => {
                bp.SetName("Teamwork Feat");
                bp.SetDescription("At 3rd level and every 6 levels thereafter, an Oathbreaker gains a bonus feat in addition to those gained from normal advancement. " +
                    "These bonus feats must be selected from those listed as teamwork feats. " +
                    "The Oathbreaker must meet the prerequisites of the selected bonus feat.");
                bp.m_Icon = TeamworkFeat.Icon;
                bp.Group = FeatureGroup.TeamworkFeat;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AlliedSpellcaster.ToReference<BlueprintFeatureReference>(),
                    BackToBack.ToReference<BlueprintFeatureReference>(),
                    CoordinatedDefense.ToReference<BlueprintFeatureReference>(),
                    CoordinatedManuevers.ToReference<BlueprintFeatureReference>(),
                    Outflank.ToReference<BlueprintFeatureReference>(),
                    PreciseStrike.ToReference<BlueprintFeatureReference>(),
                    ShakeItOff.ToReference<BlueprintFeatureReference>(),
                    ShieldedCaster.ToReference<BlueprintFeatureReference>(),
                    ShieldWall.ToReference<BlueprintFeatureReference>(),
                    SiezeTheMoment.ToReference<BlueprintFeatureReference>(),
                    TandemTrip.ToReference<BlueprintFeatureReference>()
                    };
                bp.IsClassFeature = true;
            });
            // Feat Selection
            var FeatSelectionIcon = AssetLoader.LoadInternal("Skills", "Icon_FeatSelection.png");
            var FeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("303fd456ddb14437946e344bad9a893b");
            FeatSelection.m_Icon = FeatSelectionIcon;
            FeatSelection.SetDescription("At 3rd level and every 3 levels thereafter, this class gains a bonus {g|Encyclopedia:Feat}feat{/g} in addition to " +
                "those gained from normal advancement. These bonus feats must be selected from those listed as Combat Feats, sometimes also called " +
                "\"fighter bonus feats.\"");
            // Dreadful Calm
            var DreadfulIcon = AssetLoader.LoadInternal("Skills", "Icon_DreadfulCalm.png");
            var DefensiveStanceFeature = Resources.GetBlueprint<BlueprintFeature>("2a6a2f8e492ab174eb3f01acf5b7c90a");
            var DreadfulCalm = Helpers.CreateBlueprint<BlueprintFeature>("DreadfulCalm", bp => {
                bp.SetName("Dreadful Calm");
                bp.SetDescription("At 4th level, an Oathbreaker can enter a dreadfully calm rage against those who have harmed her or her allies. The Oathbreaker " +
                    "gains the Defensive Stance ability as per the Stalwart Defender.");
                bp.m_DescriptionShort = Helpers.CreateString("$DreadfulCalm.DescriptionShort", "At 4th level, an Oathbreaker can enter a dreadfully calm rage against those who have harmed her or her allies. The Oathbreaker " +
                    "gains the Defensive Stance ability as per the Stalwart Defender.");
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.m_Icon = DreadfulIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DefensiveStanceFeature.ToReference<BlueprintUnitFactReference>() };
                });
            });
            // Defensive Powers
            var DefensivePowers = AssetLoader.LoadInternal("Skills", "Icon_DefensivePowers.png");
            var StalwartDefenderDefensivePowerSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2cd91c501bda80b47ac2df0d51b02973");
            StalwartDefenderDefensivePowerSelection.m_Icon = DefensivePowers;
            // Defensive power patch
            var IncreasedDamageReductionDefensivePower = Resources.GetBlueprint<BlueprintFeature>("d10496e92d0799a40bb3930b8f4fda0d");
            IncreasedDamageReductionDefensivePower.AddComponent<PrerequisiteClassLevel>(c => {
                c.Group = Prerequisite.GroupType.Any;
                c.m_CharacterClass = OathbreakerClass.ToReference<BlueprintCharacterClassReference>(); 
                c.Level = 12; 
            });
            var FearlessDefenseDefensivePower = Resources.GetBlueprint<BlueprintFeature>("2c13bd43a7ed4844b9f4dcc919fd74f8");
            FearlessDefenseDefensivePower.AddComponent<PrerequisiteClassLevel>(c => {
                c.Group = Prerequisite.GroupType.Any;
                c.m_CharacterClass = OathbreakerClass.ToReference<BlueprintCharacterClassReference>();
                c.Level = 8; 
            });
            // Oathbreaker Stalwart
            var StalwartIcon = AssetLoader.LoadInternal("Skills", "Icon_Stalwart.png");
            var Stalwart = Resources.GetBlueprint<BlueprintFeature>("ec9dbc9a5fa26e446a54fe5df6779088");
            Stalwart.m_Icon = StalwartIcon;
            var OathbreakerStalwart = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakerStalwart", bp => {
                bp.SetName("Stalwart");
                bp.SetDescription("At 14th level, an Oathbreaker gains Stalwart, as per the Inquisitor class feature, except she can also benefit from this ability while wearing heavy armor.");
                bp.m_Icon = StalwartIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { Stalwart.ToReference<BlueprintUnitFactReference>() };
                });
            });
            // Aura of Righteousness
            var AOSRIcon = AssetLoader.LoadInternal("Skills", "Icon_AOSR.png");
            var AuraOfSelfRighteousnessEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSelfRighteousnessEffectBuff", bp => {
                bp.SetName("Aura of Self-Righteousness");
                bp.SetDescription("At 17th level, an Oathbreaker gains DR 5/lawful or good and immunity to compulsion " +
                        "spells and spell-like abilities. Each ally within 10 feet of her gains a +4 morale bonus on saving throws " +
                        "against compulsion effects. Aura of Self-Righteousness functions only while the Oathbreaker is conscious, " +
                        "not if she is unconscious or dead.");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.m_Icon = AOSRIcon;
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.m_DisablingFeature = null;
                    c.SpellDescriptor = SpellDescriptor.Compulsion;
                    c.ModifierDescriptor = ModifierDescriptor.Morale;
                    c.Value = 4;
                });
            });
            var AuraOfSelfRighteousnessArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfSelfRighteousnessArea", bp => {
                bp.AggroEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 13 };
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.m_Buff = AuraOfSelfRighteousnessEffectBuff.ToReference<BlueprintBuffReference>();
                    c.Condition = new ConditionsChecker() {
                        Conditions = new Condition[] {
                            new ContextConditionIsAlly()
                        }
                    };
                });
            });
            var AuraOfSelfRighteousnessBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSelfRighteousnessBuff", bp => {
                bp.SetName("Aura of Self-Righteousness");
                bp.SetDescription("At 17th level, an Oathbreaker gains DR 5/lawful or good and immunity to compulsion " +
                    "spells and spell-like abilities. Each ally within 10 feet of her gains a +4 morale bonus on saving throws " +
                    "against compulsion effects. Aura of Self-Righteousness functions only while the Oathbreaker is conscious, " +
                    "not if she is unconscious or dead.");
                bp.m_Icon = AOSRIcon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfSelfRighteousnessArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });
            var AuraOfSelfRighteousnessFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfSelfRighteousnessFeature", bp => {
                bp.SetName("Aura of Self-Righteousness");
                bp.SetDescription("At 17th level, an Oathbreaker gains DR 10/Good and immunity to compulsion " +
                    "spells and spell-like abilities. Each ally within 10 feet of her gains a +4 morale bonus on saving throws " +
                    "against compulsion effects. Aura of Self-Righteousness functions only while the Oathbreaker is conscious, " +
                    "not if she is unconscious or dead.");
                bp.m_Icon = AOSRIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.m_IgnoreFeature = null;
                    c.m_FactToCheck = null;
                    c.Descriptor = SpellDescriptor.Compulsion;
                });
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfSelfRighteousnessBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.m_WeaponType = null;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = DamageAlignment.Good;
                    c.Reality = DamageRealityType.Ghost;
                    c.m_CheckedFactMythic = null;
                    c.Value = 10;
                    c.Pool = 12;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.m_CasterIgnoreImmunityFact = null;
                    c.Descriptor = SpellDescriptor.Compulsion;
                });
            });
            // Breaker of Oaths
            var BOOIcon = AssetLoader.LoadInternal("Skills", "Icon_BOO.png");
            var BreakerOfOaths = Helpers.CreateBlueprint<BlueprintFeature>("BreakerOfOaths", bp => {
                bp.SetName("Breaker of Oaths");
                bp.SetDescription("At 20th level, an Oathbreaker becomes a champion of her own ambition. Her {g|Encyclopedia:Damage_Reduction}DR{/g} increases to 15/lawful or good and whenever " +
                    "she makes a saving throw, she adds her charisma modifier as a bonus.");
                bp.m_Icon = BOOIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.m_WeaponType = null;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = DamageAlignment.Good;
                    c.Reality = DamageRealityType.Ghost;
                    c.m_CheckedFactMythic = null;
                    c.Value = 5;
                    c.Pool = 12;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveFortitude;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveWill;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveReflex;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
                });
            });
            // Signature Abilities
            OathbreakerClass.m_SignatureAbilities = new BlueprintFeatureReference[4] {
                    OathbreakersBaneFeature.ToReference<BlueprintFeatureReference>(),
                    OathbreakerSoloTactics.ToReference<BlueprintFeatureReference>(),
                    OathbreakersDirectionFeature.ToReference<BlueprintFeatureReference>(),
                    DreadfulCalm.ToReference<BlueprintFeatureReference>(),
            };
            OathbreakerProgression.LevelEntries = new LevelEntry[20] {
                Helpers.LevelEntry(1, OathbreakersBaneFeature, OathbreakersDirectionFeature, OathbreakerProficiencies),
                Helpers.LevelEntry(2, FadedGrace, OathbreakerSoloTactics),
                Helpers.LevelEntry(3, SpitefulTenacity, OathbreakerTeamworkFeat, FeatSelection),
                Helpers.LevelEntry(4, OathbreakersBaneUse, DreadfulCalm),
                Helpers.LevelEntry(5, OathbreakersDirectionIncrease),
                Helpers.LevelEntry(6, StalwartDefenderDefensivePowerSelection, FeatSelection),
                Helpers.LevelEntry(7, OathbreakersBaneUse),
                Helpers.LevelEntry(8, StalwartDefenderDefensivePowerSelection),
                Helpers.LevelEntry(9, OathbreakerTeamworkFeat, FeatSelection),
                Helpers.LevelEntry(10, OathbreakersBaneUse, OathbreakersDirectionIncrease, StalwartDefenderDefensivePowerSelection),
                Helpers.LevelEntry(11, OathbreakersDirectionSwiftFeature),
                Helpers.LevelEntry(12, StalwartDefenderDefensivePowerSelection, FeatSelection),
                Helpers.LevelEntry(13, OathbreakersBaneUse),
                Helpers.LevelEntry(14, OathbreakerStalwart, StalwartDefenderDefensivePowerSelection),
                Helpers.LevelEntry(15, OathbreakerTeamworkFeat, OathbreakersDirectionIncrease, FeatSelection),
                Helpers.LevelEntry(16, OathbreakersBaneUse, StalwartDefenderDefensivePowerSelection),
                Helpers.LevelEntry(17, AuraOfSelfRighteousnessFeature),
                Helpers.LevelEntry(18, FeatSelection),
                Helpers.LevelEntry(19, OathbreakersBaneUse),
                Helpers.LevelEntry(20, BreakerOfOaths, OathbreakersDirectionIncrease)
            };
            OathbreakerProgression.UIGroups = new UIGroup[] {
                 Helpers.CreateUIGroup(OathbreakersBaneFeature, OathbreakersBaneUse),
                 Helpers.CreateUIGroup(OathbreakersDirectionFeature, OathbreakersDirectionIncrease, OathbreakersDirectionSwiftFeature),
                 Helpers.CreateUIGroup(FadedGrace, SpitefulTenacity, OathbreakerStalwart, AuraOfSelfRighteousnessFeature, BreakerOfOaths),
                 Helpers.CreateUIGroup(OathbreakerSoloTactics, OathbreakerTeamworkFeat),
                 Helpers.CreateUIGroup(DreadfulCalm, StalwartDefenderDefensivePowerSelection),
                 Helpers.CreateUIGroup(FeatSelection)                
            };
            if (ModSettings.AddedContent.Classes.IsDisabled("Oathbreaker")) { return; }
            Helpers.RegisterClass(OathbreakerClass);
        }
    }
}


























