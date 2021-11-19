using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class TouchOfProfaneCorruption {




        public static void AddTouchOfProfaneCorruption() {
            var TOCIcon = AssetLoader.LoadInternal("Skills", "Icon_TouchCorrupt.png");
            var ChannelTOCIcon = AssetLoader.LoadInternal("Skills", "Icon_ChannelTOC.png");
            var AbsoluteDeathAbility = Resources.GetBlueprint<BlueprintAbility>("7d721be6d74f07f4d952ee8d6f8f44a0");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var TouchOfProfaneCorruptionAbilityFatigued = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityFatigued");
            var TouchOfProfaneCorruptionAbilityShaken = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityShaken");
            var TouchOfProfaneCorruptionAbilitySickened = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilitySickened");
            var TouchOfProfaneCorruptionAbilityDazed = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityDazed");
            var TouchOfProfaneCorruptionAbilityDiseased = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityDiseased");
            var TouchOfProfaneCorruptionAbilityStaggered = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityStaggered");
            var TouchOfProfaneCorruptionAbilityCursed = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityCursed");
            var TouchOfProfaneCorruptionAbilityExhausted = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityExhausted");
            var TouchOfProfaneCorruptionAbilityFrightened = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityFrightened");
            var TouchOfProfaneCorruptionAbilityNauseated = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityNauseated");
            var TouchOfProfaneCorruptionAbilityPoisoned = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityPoisoned");
            var TouchOfProfaneCorruptionAbilityBlinded = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityBlinded");
            var TouchOfProfaneCorruptionAbilityDeafened = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityDeafened");
            var TouchOfProfaneCorruptionAbilityParalyzed = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityParalyzed");
            var TouchOfProfaneCorruptionAbilityStunned = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityStunned");

            var TouchOfProfaneCorruptionResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("TouchOfProfaneCorruptionResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    m_Class = new BlueprintCharacterClassReference[1] {
                        DreadKnightClass.ToReference<BlueprintCharacterClassReference>() },
                    m_ClassDiv = new BlueprintCharacterClassReference[1] {
                        DreadKnightClass.ToReference<BlueprintCharacterClassReference>() },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0],
                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 2,
                    LevelStep = 2,
                    PerStepIncrease = 1,
                    StartingIncrease = 1,
                    IncreasedByStat = true,
                    ResourceBonusStat = Kingmaker.EntitySystem.Stats.StatType.Charisma
                };
                bp.m_Max = 20;
            });

            var CrueltyFatiguedBuff = Resources.GetModBlueprint<BlueprintFeature>("CrueltyFatiguedBuff");
            var FatiguedBuff = Resources.GetBlueprint<BlueprintBuff>("e6f2fc5d73d88064583cb828801212f4");
            var CrueltyFatigued = Resources.GetModBlueprint<BlueprintFeature>("CrueltyFatigued");
            var TouchItem = Resources.GetBlueprint<BlueprintItemWeapon>("bb337517547de1a4189518d404ec49d4");
            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");
            var TouchOfProfaneCorruptionAbility = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbility", bp => {
                bp.SetName("Touch of Profane Corruption");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.m_Icon = TOCIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
                bp.AddContextRankConfig(c => {
                    c.m_Type = Kingmaker.Enums.AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;
                    c.m_Min = 1;
                    c.m_UseMin = true;
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();

                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                      new Conditional() {
                          ConditionsChecker = new ConditionsChecker() {
                              Conditions = new Condition[] {
                              new ContextConditionHasFact() {
                                  m_Fact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>()

                              }
                              }
                          },
                          IfTrue = Helpers.CreateActionList(
                              new ContextActionHealTarget() {
                                  Value = new ContextDiceValue() {
                                      DiceType = Kingmaker.RuleSystem.DiceType.D6,
                                      DiceCountValue = new ContextValue() {
                                          ValueType = ContextValueType.Rank
                                      },
                                      BonusValue = new ContextValue()
                                  }
                              }),
                          IfFalse = Helpers.CreateActionList(
                                                new ContextActionDealDamage() {
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData(),
                                                        Physical = new DamageTypeDescription.PhysicalData(),
                                                        Energy = Kingmaker.Enums.Damage.DamageEnergyType.NegativeEnergy,
                                                    },
                                                    Duration = new ContextDurationValue() {
                                                        m_IsExtendable = true,
                                                        DiceCountValue = new ContextValue(),
                                                        BonusValue = new ContextValue(),
                                                    },
                                                    Value = new ContextDiceValue() {
                                                        DiceType = Kingmaker.RuleSystem.DiceType.D6,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Rank,
                                                        },
                                                        BonusValue = new ContextValue(),
                                                    }
                                                })

                      });
                });

                bp.AddComponent<AbilityEffectMiss>();


                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });



            });
            var FiendishQuarryAbility = Resources.GetBlueprint<BlueprintAbility>("d9660af97d116f94ab98dbec15dbc704");
            var FiendishSmiteGoodAbility = Resources.GetBlueprint<BlueprintAbility>("478cf0e6c5f3a4142835faeae3bd3e04");
            var ChannelTouchOfProfaneCorruptionAbility = Helpers.CreateBlueprint<BlueprintAbility>("ChannelTouchOfProfaneCorruptionAbility", bp => {
                bp.SetName("Channel Profane Corruption");
                bp.SetDescription("Beginning at 7th level as a swift action, a Dread Knight can spend three uses of their profane corruption to " +
                    "surround their weapon with a profane flame and deliver an attack with their weapon, causing 1d6 points of damage plus an additional " +
                    "1d6 points of damage for every two Dread Knight levels they possesses, as well as applying the cruelty selected. ");
                bp.m_Icon = ChannelTOCIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Weapon;


                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");

                bp.AddContextRankConfig(c => {
                    c.m_Type = Kingmaker.Enums.AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;
                    c.m_Min = 1;
                    c.m_UseMin = true;
                });                           
               
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                      new Conditional() {
                          ConditionsChecker = new ConditionsChecker() {
                              Conditions = new Condition[] {
                                  new ContextConditionHasFact() {
                                      m_Fact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>()

                                  }
                              }
                          },
                          IfTrue = Helpers.CreateActionList(
                              new ContextActionHealTarget() {
                                  Value = new ContextDiceValue() {
                                      DiceType = Kingmaker.RuleSystem.DiceType.D6,
                                      DiceCountValue = new ContextValue() {
                                          ValueType = ContextValueType.Rank
                                      },
                                      BonusValue = new ContextValue()
                                  }
                              }),
                          IfFalse = Helpers.CreateActionList(
                                                new ContextActionDealDamage() {
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData(),
                                                        Physical = new DamageTypeDescription.PhysicalData(),
                                                        Energy = Kingmaker.Enums.Damage.DamageEnergyType.NegativeEnergy,
                                                    },
                                                    Duration = new ContextDurationValue() {
                                                        m_IsExtendable = true,
                                                        DiceCountValue = new ContextValue(),
                                                        BonusValue = new ContextValue(),
                                                    },
                                                    Value = new ContextDiceValue() {
                                                        DiceType = Kingmaker.RuleSystem.DiceType.D6,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Rank,
                                                        },
                                                        BonusValue = new ContextValue(),
                                                    }
                                                })

                      });
                });
                bp.AddComponent<AbilityDeliverAttackWithWeapon>();
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 3;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });





            });

            var ChannelTouchOfProfaneCorruptionFeature = Helpers.CreateBlueprint<BlueprintFeature>("ChannelTouchOfProfaneCorruptionFeature", bp => {
                bp.SetName("Channel Profane Corruption");
                bp.SetDescription("Beginning at 7th level as a swift action, a Dread Knight can spend two uses of their touch of profane corruption to " +
                    "surround their weapon with a profane flame, causing terrible wounds to open on those they attacks. This ability works with any weapon.");
                bp.m_DescriptionShort = Helpers.CreateString("$TouchOfProfaneCorruptionFeature.DescriptionShort", "Beginning at 7th level as a swift action, a Dread Knight can spend two uses of their touch of profane corruption to " +
                    "surround their weapon with a profane flame, causing terrible wounds to open on those they attacks. This ability works with any weapon.");
                bp.m_Icon = ChannelTOCIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionFact = Helpers.CreateBlueprint<BlueprintUnitFact>("TouchOfProfaneCorruptionFact", bp => {
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });


            });
            var CrueltyResource = Resources.GetModBlueprint<BlueprintUnitFact>("CrueltyResource");
            var CrueltyFact = Resources.GetModBlueprint<BlueprintUnitFact>("CrueltyFact");

            var TouchOfProfaneCorruptionFeature = Helpers.CreateBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionFeature", bp => {
                bp.SetName("Profane Corruption");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds her hand with a profane flame, causing terrible wounds to open on those she touches. " +
               "Each day she can use this ability a number of times equal to 1/2 his Dread Knight level + her Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels she possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.m_DescriptionShort = Helpers.CreateString("$TouchOfProfaneCorruptionFeature.DescriptionShort", "Beginning at 2nd level, a Dread Knight can surround her hand " +
                    "with a profane flame, causing terrible wounds to open on those she touches. Alternatively, a Dread Knight can use this power to heal undead creatures.");
                bp.m_Icon = TOCIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbility.ToReference<BlueprintUnitFactReference>(), TouchOfProfaneCorruptionFact.ToReference<BlueprintUnitFactReference>(), CrueltyFact.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = CrueltyResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
            });
            var TouchOfProfaneCorruptionUse = Helpers.CreateBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionUse", bp => {
                bp.SetName("Profane Corruption - Additional Use");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.m_Icon = TOCIcon;
                bp.Ranks = 10;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
            });
            var ExtraLayOnHands = Resources.GetBlueprint<BlueprintFeature>("a2b2f20dfb4d3ed40b9198e22be82030");
            ExtraLayOnHands.AddComponent<IncreaseResourceAmount>(c => { c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>(); c.Value = 2; });
        }





    }
}
