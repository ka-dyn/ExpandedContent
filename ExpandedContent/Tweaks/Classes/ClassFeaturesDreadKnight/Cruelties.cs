using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
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
using Kingmaker.UnitLogic.ActivatableAbilities;
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

            var StunnedBuff = Resources.GetBlueprint<BlueprintBuff>("09d39b38bb7c6014394b6daced9bacd3");
            var ParalyzedBuff = Resources.GetBlueprint<BlueprintBuff>("af1e2d232ebbb334aaf25e2a46a92591");
            var BlindedBuff = Resources.GetBlueprint<BlueprintBuff>("187f88d96a0ef464280706b63635f2af");
            var PoisonedBuff = Resources.GetBlueprint<BlueprintBuff>("ba1ae42c58e228c4da28328ea6b4ae34");
            var NauseatedBuff = Resources.GetBlueprint<BlueprintBuff>("956331dba5125ef48afe41875a00ca0e");
            var FrightenedBuff = Resources.GetBlueprint<BlueprintBuff>("f08a7239aa961f34c8301518e71d4cdf");
            var ExhaustedBuff = Resources.GetBlueprint<BlueprintBuff>("46d1b9cc3d0fd36469a471b047d773a2");
            var CursedBuff = Resources.GetBlueprint<BlueprintBuff>("caae9592917719a41b601b678a8e6ddf");
            var StaggeredBuff = Resources.GetBlueprint<BlueprintBuff>("df3950af5a783bd4d91ab73eb8fa0fd3");
            var DiseasedBuff = Resources.GetBlueprint<BlueprintBuff>("103aac6bc1cfd454492cee1fd680db05");
            var DazedBuff = Resources.GetBlueprint<BlueprintBuff>("d2e35b870e4ac574d9873b36402487e5");
            var SickenedBuff = Resources.GetBlueprint<BlueprintBuff>("4e42460798665fd4cb9173ffa7ada323");
            var ShakenBuff = Resources.GetBlueprint<BlueprintBuff>("25ec6cb6ab1845c48a95f9c20b034220");

            var CrueltySelectIcon = AssetLoader.LoadInternal("Skills", "Icon_CrueltySelect.png");
            var FatigueIcon = Resources.GetBlueprint<BlueprintBuff>("e6f2fc5d73d88064583cb828801212f4");
            var ChannelTouchOfProfaneCorruptionAbility = Resources.GetModBlueprint<BlueprintAbility>("ChannelTouchOfProfaneCorruptionAbility");
            var TouchOfProfaneCorruptionAbility = Resources.GetModBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbility");
            var TouchOfProfaneCorruptionResource = Resources.GetModBlueprint<BlueprintAbilityResource>("TouchOfProfaneCorruption");
            var BestowCurseFeeblyBody = Resources.GetBlueprint<BlueprintAbility>("0c853a9f35a7bf749821ebe5d06fade7");
            var TouchItem = Resources.GetBlueprint<BlueprintItemWeapon>("bb337517547de1a4189518d404ec49d4");
            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadknightClass");
            var FatiguedBuff = Resources.GetBlueprint<BlueprintBuff>("e6f2fc5d73d88064583cb828801212f4");

            var FiendishSmiteGoodBuff = Resources.GetBlueprint<BlueprintBuff>("a9035e49d6d79a64eaec321f2cb629a8");
            var CrueltyFatiguedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyFatiguedBuff", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("The next use of profane corruption will be enhanced with the fatigued cruelty.");
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                            m_Buff = FatiguedBuff.ToReference<BlueprintBuffReference>(),
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

                            IfFalse = Helpers.CreateActionList(),



                        });
                });
                bp.AddComponent<UniqueBuff>();
                


            });
            var CrueltyShakenBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyShakenBuff", bp => {
                bp.SetName("Cruelty - Shaken");
                bp.SetDescription("The next use of profane corruption will be enhanced with the shaken cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                                DiceCountValue = 0,
                                                                BonusValue = new ContextValue()



                                                            },
                                                            IsFromSpell = true,


                                                        }),


                                                }),
                            }),

                            IfFalse = Helpers.CreateActionList(),



                        });


                });
                bp.AddComponent<UniqueBuff>();
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = ShakenBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });

            });


            var CrueltySickenedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltySickenedBuff", bp => {
                bp.SetName("Cruelty - Sickened");
                bp.SetDescription("The next use of profane corruption will be enhanced with the fatigued cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                                DiceCountValue = 0,
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
                bp.AddComponent<UniqueBuff>();
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = SickenedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });

            });
            var CrueltyDazedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyDazedBuff", bp => {
                bp.SetName("Cruelty - Dazed");
                bp.SetDescription("The next use of profane corruption will be enhanced with the dazed cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = DazedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                                DiceCountValue = 0,
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


            });
            var CrueltyDiseasedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyDiseasedBuff", bp => {
                bp.SetName("Cruelty - Diseased");
                bp.SetDescription("The next use of profane corruption will be enhanced with the diseased cruelty.");
                bp.m_Icon = DiseasedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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

                            IfFalse = Helpers.CreateActionList(),



                        });
                });


            });
            var CrueltyStaggeredBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyStaggeredBuff", bp => {
                bp.SetName("Cruelty - Staggered");
                bp.SetDescription("The next use of profane corruption will be enhanced with the staggered cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                                DiceCountValue = 0,
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



                        });
                });


                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = StaggeredBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 10;

                });

            });
            var CrueltyCursedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyCursedBuff", bp => {
                bp.SetName("Select Cruelty - Cursed");
                bp.SetDescription("The next use of profane corruption will be enhanced with the cursed cruelty.");
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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

                            IfFalse = Helpers.CreateActionList(),



                        });
                });


            });
            var CrueltyExhaustedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyExhaustedBuff", bp => {
                bp.SetName("Cruelty - Exhausted");
                bp.SetDescription("The next use of profane corruption will be enhanced with the exhausted cruelty.");
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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

                            IfFalse = Helpers.CreateActionList(),



                        });
                });

            });
            var CrueltyFrightenedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyFrightenedBuff", bp => {
                bp.SetName("Cruelty - Frightened");
                bp.SetDescription("The next use of profane corruption will be enhanced with the frightened cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                            m_Buff = FrightenedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = 0,
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

                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = FrightenedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 10;

                });

            });
            var CrueltyNauseatedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyNauseatedBuff", bp => {
                bp.SetName("Cruelty - Nauseated");
                bp.SetDescription("The next use of profane corruption will be enhanced with the nauseated cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = NauseatedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                            AsChild = true,
                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = 0,
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

                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = StaggeredBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 7;


                });
            });
            var CrueltyPoisonedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyPoisonedBuff", bp => {
                bp.SetName("Cruelty - Poisoned");
                bp.SetDescription("The next use of profane corruption will be enhanced with the poisoned cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                                DiceCountValue = 0,
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

                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = PoisonedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });

            });
            var CrueltyBlindedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyBlindedBuff", bp => {
                bp.SetName("Cruelty - Blinded");
                bp.SetDescription("The next use of profane corruption will be enhanced with the blinded cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                                DiceCountValue = 0,
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

                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = BlindedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;

                });

            });
            var CrueltyParalyzedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyParalyzedBuff", bp => {
                bp.SetName("Cruelty - Paralyzed");
                bp.SetDescription("The next use of profane corruption will be enhanced with the paralyzed cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = ParalyzedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {

                                    new ContextConditionIsCaster() { Not = true }

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
                                                                DiceCountValue = 0,
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
                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = ParalyzedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 1;

                });


            });
            var CrueltyStunnedBuff = Helpers.CreateBlueprint<BlueprintBuff>("CrueltyStunnedBuff", bp => {
                bp.SetName("Cruelty - Stunned");
                bp.SetDescription("The next use of profane corruption will be enhanced with the stunned cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.m_Ability = TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.m_Ability = ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>();
                    c.ActionsOnTarget = true;
                    c.Action = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {
                                    
                                    new ContextConditionIsCaster() { Not = true }

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
                                                            m_Buff = StunnedBuff.ToReference<BlueprintBuffReference>(),

                                                            DurationValue = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = 0,
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

                bp.AddContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = StunnedBuff.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 5;

                });

            });
            var TouchOfProfaneCorruptionAbilityFatigued = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityFatigued", bp => {
                bp.SetName("Select Cruelty - Fatigued");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become fatigued.");
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Buff = CrueltyFatiguedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;


            });
            var CrueltyFatigued = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFatigued", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become fatigued upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { TouchOfProfaneCorruptionAbilityFatigued.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var TouchOfProfaneCorruptionAbilityShaken = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityShaken", bp => {
                bp.SetName("Select Cruelty - Shaken");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become shaken for 1 round/dread knight level upon a failed fortitude save.");
                bp.m_Buff = CrueltyShakenBuff.ToReference<BlueprintBuffReference>();
                bp.m_Icon = ShakenBuff.Icon;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;




            });
            var CrueltyShaken = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyShaken", bp => {
                bp.SetName("Cruelty - Shaken");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time, and must be used once before selecting another cruelty. " +
                    "This cruelty causes the target to become shaken upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityShaken.ToReference<BlueprintUnitFactReference>() };
                });
            });



            var TouchOfProfaneCorruptionAbilitySickened = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilitySickened", bp => {
                bp.SetName("Select Cruelty - Sickened");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become sickened for 1 round/dread knight level upon a failed fortitude save.");
                bp.m_Icon = SickenedBuff.Icon;
                bp.m_Buff = CrueltySickenedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;           
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltySickened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltySickened", bp => {
                bp.SetName("Cruelty - Sickened");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time, and must be used once before selecting another cruelty. " +
                    "This cruelty causes the target to become sickened upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
               
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilitySickened.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var TouchOfProfaneCorruptionAbilityDazed = Helpers.CreateBlueprint <BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityDazed", bp => {
                bp.SetName("Select Cruelty - Dazed");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become dazed for 1 round upon a failed fortitude save.");
                bp.m_Icon = DazedBuff.Icon;
                bp.m_Buff = CrueltyDazedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyDazed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDazed", bp => {
                bp.SetName("Cruelty - Dazed");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become dazed for 1 round upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDazed.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var TouchOfProfaneCorruptionAbilityDiseased = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityDiseased", bp => {
                bp.SetName("Select Cruelty - Diseased");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to contract the bubonic plague upon a failed fortitude save.");
                bp.m_Icon = DiseasedBuff.Icon;
                bp.m_Buff = CrueltyDiseasedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyDiseased = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDiseased", bp => {
                bp.SetName("Cruelty - Diseased");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to contract the bubonic plague upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDiseased.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var TouchOfProfaneCorruptionAbilityStaggered = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityStaggered", bp => {
                bp.SetName("Select Cruelty - Staggered");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become staggered for 1 round/two levels of dread knight upon a failed fortitude save.");
                bp.m_Icon = StaggeredBuff.Icon;
                bp.m_Buff = CrueltyStaggeredBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyStaggered = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStaggered", bp => {
                bp.SetName("Cruelty - Staggered");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to contract the bubonic plague upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStaggered.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var TouchOfProfaneCorruptionAbilityCursed = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityCursed", bp => {
                bp.SetName("Select Cruelty - Cursed");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty bestows a curse of deteroriation upon a failed fortitude save.");               
                bp.m_Icon = CursedBuff.Icon;
                bp.m_Buff = CrueltyCursedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyCursed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyCursed", bp => {
                bp.SetName("Cruelty - Cursed");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty bestows a curse of deteroriation upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityCursed.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var TouchOfProfaneCorruptionAbilityExhausted = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityExhausted", bp => {
                bp.SetName("Select Cruelty - Exhausted");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty forces exhaustion on the target upon a failed fortitude save.");
                bp.m_Icon = ExhaustedBuff.Icon;
                bp.m_Buff = CrueltyExhaustedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyExhausted = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyExhausted", bp => {
                bp.SetName("Cruelty - Exhausted");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty forces exhaustion on the target upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityExhausted.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyFatigued.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });



            var TouchOfProfaneCorruptionAbilityFrightened = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityFrightened", bp => {
                bp.SetName("Select Cruelty - Frightened");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be frightened for 1 round/2 levels of dread knight upon a failed fortitude save.");
                bp.m_Icon = FrightenedBuff.Icon;
                bp.m_Buff = CrueltyFrightenedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyFrightened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFrightened", bp => {
                bp.SetName("Cruelty - Frightened");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be frightened for 1 round/2 levels of dread knight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityFrightened.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyShaken.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });



            var TouchOfProfaneCorruptionAbilityNauseated = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityNauseated", bp => {
                bp.SetName("Select Cruelty - Nauseated");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be nauseated for 1 round/3 levels of dread knight upon a failed fortitude save.");
                bp.m_Icon = NauseatedBuff.Icon;
                bp.m_Buff = CrueltyNauseatedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyNauseated = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyNauseated", bp => {
                bp.SetName("Cruelty - Nauseated");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be nauseated for 1 round/3 levels of dread knight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityNauseated.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltySickened.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });


            var TouchOfProfaneCorruptionAbilityPoisoned = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityPoisoned", bp => {
                bp.SetName("Select Cruelty - Poisoned");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be poisoned for 1 round/level of dread knight upon a failed fortitude save.");
                bp.m_Icon = PoisonedBuff.Icon;
                bp.m_Buff = CrueltyPoisonedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyPoisoned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyPoisoned", bp => {
                bp.SetName("Cruelty - Poisoned");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be poisoned for 1 round/level of dread knight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityPoisoned.ToReference<BlueprintUnitFactReference>() };
                });
            });



            var TouchOfProfaneCorruptionAbilityBlinded = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityBlinded", bp => {
                bp.SetName("Select Cruelty - Blinded");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become blinded for 1 round/level of dread knight upon a failed fortitude save.");
                bp.m_Icon = BlindedBuff.Icon;
                bp.m_Buff = CrueltyBlindedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyBlinded = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyBlinded", bp => {
                bp.SetName("Cruelty - Blinded");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "\nThis cruelty causes the target to be become blinded for 1 round/level of dread knight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityBlinded.ToReference<BlueprintUnitFactReference>() };
                });
            });



            var TouchOfProfaneCorruptionAbilityParalyzed = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityParalyzed", bp => {
                bp.SetName("Select Cruelty - Paralyzed");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become paralyzed for 1 round upon a failed fortitude save.");
                bp.m_Icon = ParalyzedBuff.Icon;
                bp.m_Buff = CrueltyParalyzedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyParalyzed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyParalyzed", bp => {
                bp.SetName("Cruelty - Paralyzed");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become paralyzed for 1 round upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityParalyzed.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var TouchOfProfaneCorruptionAbilityStunned = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityStunned", bp => {
                bp.SetName("Select Cruelty - Stunned");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become stunned for 1 round/4 levels of Dread Knight upon a failed fortitude save.");
                bp.m_Icon = StunnedBuff.Icon;
                bp.m_Buff = CrueltyStunnedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyStunned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStunned", bp => {
                bp.SetName("Cruelty - Stunned");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "\nThis cruelty causes the target to be become stunned for 1 round/4 levels of Dread Knight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStunned.ToReference<BlueprintUnitFactReference>() };
                });
            });



            var CrueltySelection1 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection1", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's profane corruption ability. Whenever the Dread Knight uses " +
                    "any from of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. The Dread Knight selects this cruelty before the attack. The target receives a Fortitude " +
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
                    "profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_DescriptionShort = Helpers.CreateString("$CrueltySelection4.DescriptionShort", "At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight.");
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
            var TouchOfProfaneCorruptionFeature = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionFeature");
            var MythicAbilitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
            var MasteredCruelty = Helpers.CreateBlueprint<BlueprintFeature>("MasteredCruelty", bp => {
                bp.SetName("Cruel Master");
                bp.SetDescription("You have found a way to force dark powers to grant you the ability to apply one additional cruelty when using profane corruption. " +
                    "\nBenefit: You can select one additional cruelty to use with profane corruption.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => {
                    c.Group = ActivatableAbilityGroup.MasterHealingTechnique;                    
                });                
                bp.AddComponent<PrerequisiteFeature>(c => { c.m_Feature = TouchOfProfaneCorruptionFeature.ToReference<BlueprintFeatureReference>(); });

            });
            MythicAbilitySelection.m_AllFeatures = MythicAbilitySelection.m_AllFeatures.AppendToArray(MasteredCruelty.ToReference<BlueprintFeatureReference>());
        }
    }
}




















