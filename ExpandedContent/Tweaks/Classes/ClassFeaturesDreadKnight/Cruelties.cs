using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.Base;
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
    internal class Cruelties {



        public static void AddCrueltyAbilities() {






















            var TouchOfProfaneCorruptionAbility = Resources.GetModBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbility");
            var TouchOfProfaneCorruptionResource = Resources.GetModBlueprint<BlueprintAbilityResource>("TouchOfProfaneCorruption");
            var BestowCurseFeeblyBody = Resources.GetBlueprint<BlueprintAbility>("0c853a9f35a7bf749821ebe5d06fade7");
            var TouchItem = Resources.GetBlueprint<BlueprintItemWeapon>("bb337517547de1a4189518d404ec49d4");
            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadknightClass");
            var FatiguedBuff = Resources.GetBlueprint<BlueprintBuff>("e6f2fc5d73d88064583cb828801212f4");


            var CrueltyFatiguedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyFatiguedBuff", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("The target of this cruelty becomes fatigued on a failed saving throw.");
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(                                                   
                                                        new ContextActionApplyBuff() {                                                            
                                                            m_Buff = FatiguedBuff.ToReference<BlueprintBuffReference>(),
                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue()



                                                            },
                                                            IsFromSpell = true,
                                                            

                                                        }),


                                                }),

                                        });

                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();

                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf());
                });
            });

            var TouchOfProfaneCorruptionAbilityFatigued = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityFatigued", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("A Dread Knight may enhance their Touch of Profane Corruption with cruelties. Only one may be selected at a time.");
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {
                                  
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = CrueltyFatiguedBuff.ToReference<BlueprintBuffReference>(),
                                        Not = true
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = CrueltyFatiguedBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    DurationValue = new ContextDurationValue() {
                                        m_IsExtendable = true,
                                        DiceCountValue = new ContextValue(),
                                        BonusValue = new ContextValue()
                                    }
                                }
                            ),
                            IfFalse = Helpers.CreateActionList(),
                        });

                });
            });
            var CrueltyFatigued = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFatigued", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("The target is fatigued.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { TouchOfProfaneCorruptionAbilityFatigued.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var ShakenBuff = Resources.GetBlueprint<BlueprintBuff>("25ec6cb6ab1845c48a95f9c20b034220");
            var CrueltyShakenedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyShakenedBuff", bp => {
                bp.SetName("Cruelty - Shaken");
                bp.SetDescription("The target becomes shaken for 1 round/dread knight level.");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.Action = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy()
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,
                                                                    ValueType = ContextValueType.Rank
                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf());
                    c.AfterCast = true;
                    c.CheckAbilityType = true;
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = ShakenBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });

            });
            var TouchOfProfaneCorruptionAbilityShaken = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityShaken", bp => {
                bp.SetName("Touch of Profane Corruption - Shaken");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
                bp.AddComponent<AbilityEffectRunAction>(c => {

                    c.Actions = Helpers.CreateActionList(
                       new ContextActionApplyBuff() {
                           m_Buff = CrueltyFatiguedBuff.ToReference<BlueprintBuffReference>(),
                           Permanent = true,
                           DurationValue = new ContextDurationValue() {
                               m_IsExtendable = true,
                               DiceCountValue = new ContextValue(),
                               BonusValue = new ContextValue()
                           }
                       });
                });

            });
            var CrueltyShaken = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyShaken", bp => {
                bp.SetName("Cruelty - Shaken");
                bp.SetDescription("The target is shaken for 1 round per level of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityShaken.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var SickenedBuff = Resources.GetBlueprint<BlueprintBuff>("4e42460798665fd4cb9173ffa7ada323");
            var TouchOfProfaneCorruptionAbilitySickened = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilitySickened", bp => {
                bp.SetName("Touch of Profane Corruption - Sickened");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = SickenedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,
                                                                    ValueType = ContextValueType.Rank
                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = SickenedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltySickened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltySickened", bp => {
                bp.SetName("Cruelty - Sickened");
                bp.SetDescription("The target is sickened for 1 round per level of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilitySickened.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var DazedBuff = Resources.GetBlueprint<BlueprintBuff>("9934fedff1b14994ea90205d189c8759");
            var TouchOfProfaneCorruptionAbilityDazed = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityDazed", bp => {
                bp.SetName("Touch of Profane Corruption - Dazed");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = DazedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,

                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });

            });
            var CrueltyDazed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDazed", bp => {
                bp.SetName("Cruelty - Dazed");
                bp.SetDescription("The target is dazed for 1 round.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDazed.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DiseasedBuff = Resources.GetBlueprint<BlueprintBuff>("b523ff6c5db9a9c489daff7aae41afb9");
            var TouchOfProfaneCorruptionAbilityDiseased = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityDiseased", bp => {
                bp.SetName("Touch of Profane Corruption - Diseased");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty applies the Bubonic Plague. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = DiseasedBuff.ToReference<BlueprintBuffReference>(),
                                                            IsFromSpell = true,
                                                            Permanent = true,
                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,

                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue()




                                                            },


                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });

            });
            var CrueltyDiseased = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDiseased", bp => {
                bp.SetName("Cruelty - Diseased");
                bp.SetDescription("The target contracts a disease, as if the Dread Knight had cast contagion, using his Dread Knight level as his caster level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDiseased.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var StaggeredBuff = Resources.GetBlueprint<BlueprintBuff>("df3950af5a783bd4d91ab73eb8fa0fd3");
            var TouchOfProfaneCorruptionAbilityStaggered = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityStaggered", bp => {
                bp.SetName("Touch of Profane Corruption - Staggered");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty staggers the enemy. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = StaggeredBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,
                                                                    ValueType = ContextValueType.Rank
                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = StaggeredBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 10;

                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyStaggered = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStaggered", bp => {
                bp.SetName("Cruelty - Staggered");
                bp.SetDescription("The target is staggered for 1 round per two levels of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStaggered.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CursedBuff = Resources.GetBlueprint<BlueprintBuff>("caae9592917719a41b601b678a8e6ddf");
            var TouchOfProfaneCorruptionAbilityCursed = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityCursed", bp => {
                bp.SetName("Touch of Profane Corruption - Cursed");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty bestows a curse of deterioration on the enemy. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = CursedBuff.ToReference<BlueprintBuffReference>(),
                                                            Permanent = true,
                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue()

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyCursed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyCursed", bp => {
                bp.SetName("Cruelty - Cursed");
                bp.SetDescription("The target is cursed, as if the Dread Knight had cast bestow curse, using his Dread Knight level as his caster level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityCursed.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var ExhaustedBuff = Resources.GetBlueprint<BlueprintBuff>("46d1b9cc3d0fd36469a471b047d773a2");
            var TouchOfProfaneCorruptionAbilityExhausted = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityExhausted", bp => {
                bp.SetName("Touch of Profane Corruption - Exhausted");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty bestows a curse of exhaustion on the enemy. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = ExhaustedBuff.ToReference<BlueprintBuffReference>(),
                                                            Permanent = true,
                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue()

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });

            var CrueltyExhausted = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyExhausted", bp => {
                bp.SetName("Cruelty - Exhausted");
                bp.SetDescription("The target is exhausted. The Dread Knight must have the fatigue cruelty before selecting this cruelty.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityExhausted.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyFatigued.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });


            var FrightenedBuff = Resources.GetBlueprint<BlueprintBuff>("f08a7239aa961f34c8301518e71d4cdf");
            var TouchOfProfaneCorruptionAbilityFrightened = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityFrightened", bp => {
                bp.SetName("Touch of Profane Corruption - Frightened");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty terrifies the enemy. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = StaggeredBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,
                                                                    ValueType = ContextValueType.Rank
                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = FrightenedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 10;

                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyFrightened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFrightened", bp => {
                bp.SetName("Cruelty - Frightened");
                bp.SetDescription("The target is frightened for 1 round per two levels of the Dread Knight. The Dread Knight must have the shaken cruelty before selecting this cruelty.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityFrightened.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyShaken.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });


            var NauseatedBuff = Resources.GetBlueprint<BlueprintBuff>("956331dba5125ef48afe41875a00ca0e");
            var TouchOfProfaneCorruptionAbilityNauseated = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityNauseated", bp => {
                bp.SetName("Touch of Profane Corruption - Nauseated");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty induces nausea in the enemy. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = NauseatedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,
                                                                    ValueType = ContextValueType.Rank
                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = NauseatedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 7;

                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyNauseated = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyNauseated", bp => {
                bp.SetName("Cruelty - Nauseated");
                bp.SetDescription("The target is nauseated for 1 round per three levels of the Dread Knight. The Dread Knight must have the sickened cruelty before selecting this cruelty.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityNauseated.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltySickened.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });

            var PoisonedBuff = Resources.GetBlueprint<BlueprintBuff>("ba1ae42c58e228c4da28328ea6b4ae34");
            var TouchOfProfaneCorruptionAbilityPoisoned = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityPoisoned", bp => {
                bp.SetName("Touch of Profane Corruption - Nauseated");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty poisons the enemy. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = PoisonedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,
                                                                    ValueType = ContextValueType.Rank
                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = PoisonedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyPoisoned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyPoisoned", bp => {
                bp.SetName("Cruelty - Poisoned");
                bp.SetDescription("The target is poisoned, as if the Dread Knight had cast poison, using the Dread Knight’s level as the caster level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityPoisoned.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var BlindedBuff = Resources.GetBlueprint<BlueprintBuff>("ba1ae42c58e228c4da28328ea6b4ae34");
            var TouchOfProfaneCorruptionAbilityBlinded = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityBlinded", bp => {
                bp.SetName("Touch of Profane Corruption - Blinded");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty blinds the enemy. " +
               "\nAlternatively, a Dread Knight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a Dread Knight 2 additional uses of the touch of corruption class feature.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = BlindedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,
                                                                    ValueType = ContextValueType.Rank
                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = BlindedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyBlinded = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyBlinded", bp => {
                bp.SetName("Cruelty - Blinded");
                bp.SetDescription("The target is blinded for 1 round per level of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityBlinded.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var ParalyzedBuff = Resources.GetBlueprint<BlueprintBuff>("af1e2d232ebbb334aaf25e2a46a92591");
            var TouchOfProfaneCorruptionAbilityParalyzed = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityParalyzed", bp => {
                bp.SetName("Touch of Profane Corruption - Paralyzed");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty paralyzes the enemy.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = ParalyzedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,

                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyParalyzed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyParalyzed", bp => {
                bp.SetName("Cruelty - Paralyzed");
                bp.SetDescription("The target is paralyzed for 1 round.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityParalyzed.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var StunnedBuff = Resources.GetBlueprint<BlueprintBuff>("af1e2d232ebbb334aaf25e2a46a92591");
            var TouchOfProfaneCorruptionAbilityStunned = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbilityStunned", bp => {
                bp.SetName("Touch of Profane Corruption - Stunned");
                bp.SetDescription("Beginning at 2nd level, a Dread Knight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a Dread Knight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. This cruelty Paralyzes the enemy.");
                bp.Type = AbilityType.Supernatural;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Fortitude");
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
                                                }),
                      },
                      new ContextActionProvokeAttackOfOpportunity() {
                          ApplyToCaster = false
                      },
                          new Conditional() {
                              ConditionsChecker = new ConditionsChecker() {
                                  Conditions = new Condition[] {
                                      new ContextConditionCasterHasFact() {
                                          m_Fact = CrueltyShaken.ToReference<BlueprintUnitFactReference>()
                                      }
                                  }
                              },
                              IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy()
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSavingThrow() {
                                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                            Type = SavingThrowType.Fortitude,
                                            CustomDC = new ContextValue(),
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionConditionalSaved() {
                                                    Succeed = new ActionList(),
                                                    Failed = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = ParalyzedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue() {
                                                                    Value = 1,

                                                                }

                                                            },
                                                            IsFromSpell = true,

                                                        }),


                                                }),

                                        }),
                                    IfFalse = Helpers.CreateActionList()
                                }),
                              IfFalse = Helpers.CreateActionList(),
                          });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = StunnedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 5;

                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var CrueltyStunned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStunned", bp => {
                bp.SetName("Cruelty - Stunned");
                bp.SetDescription("The target is stunned for 1 round per four levels of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStunned.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var CrueltySelectIcon = AssetLoader.LoadInternal("Skills", "Icon_CrueltySelect.png");
            var CrueltySelection1 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection1", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;



            });
            var CrueltySelection2 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection2", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[]  {
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.IsClassFeature = true;



            });
            var CrueltySelection3 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection3", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.IsClassFeature = true;
                bp.Ranks = 1;

            });
            var CrueltySelection4 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection4", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyBlinded.ToReference<BlueprintFeatureReference>(),
                CrueltyParalyzed.ToReference<BlueprintFeatureReference>(),
                CrueltyStunned.ToReference<BlueprintFeatureReference>(),
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyBlinded.ToReference<BlueprintFeatureReference>(),
                CrueltyParalyzed.ToReference<BlueprintFeatureReference>(),
                CrueltyStunned.ToReference<BlueprintFeatureReference>(),
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.IsClassFeature = true;


            });
        }
    }
}












