using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using ExpandedContent.Tweaks.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Components;

namespace ExpandedContent.Tweaks.Curses {
    internal class GodMeddled {

        public static void AddGodMeddledCurse() {

            var OracleCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b0a5118b4fb793241bc7042464b23fab");
            var BeneficialCurse = Resources.GetBlueprint<BlueprintFeatureSelection>("2dda67424ee8e0b4d83ef01a73ca6bff");
            var MysteryGiftFeatureCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4e7265c0ae1345db90d3375f4ced94cc");
            var DualCursedSecondCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cc6fda79e8c340b88c84689414a9abbe");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var GodMeddledWitchArchetype = Resources.GetBlueprint<BlueprintArchetype>("c5f6e53e71059fb4d802ce81a277a12d");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var IllOmenSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ca577309cedc4f1daf6fe5795fb2619b");
            var BestowCurseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("989ab5c44240907489aba0a8568d0603");
            var SlowBuff = Resources.GetBlueprint<BlueprintBuff>("0bc608c3f2b548b44b7146b7530613ac");
            var HasteBuff = Resources.GetBlueprint<BlueprintBuff>("03464790f40c3c24aa684b57155f3280");
            var BlindBuff = Resources.GetBlueprint<BlueprintBuff>("0ec36e7596a4928489d2049e1e1c76a7");
            var ProneBuff = Resources.GetBlueprint<BlueprintBuff>("24cf3deb078d3df4d92ba24b176bda97");
            var ReducePersonBuff = Resources.GetBlueprint<BlueprintBuff>("b0793973c61a19744a8630468e8f4174");
            var EnlargePersonBuff = Resources.GetBlueprint<BlueprintBuff>("4f139d125bb602f48bfaec3d3e1937cb");
            var RecentlyDeanBuff = Resources.GetBlueprint<BlueprintBuff>("38bb8a5d773243843bbaaa2c340cc19c");
            var GodMeddledIcon = AssetLoader.LoadInternal("Skills", "Icon_GodMeddledCurse.png");




            var GodMeddlingTeleportAbility = Helpers.CreateBlueprint<BlueprintAbility>("GodMeddlingTeleportAbility", bp => {
                bp.SetName("God Meddled - Teleport");
                bp.SetDescription("");
                bp.AddComponent<LineOfSightIgnorance>();
                bp.AddComponent<AbilityCustomTeleportation>(c => {
                    c.m_Projectile = null;
                    c.DisappearFx = new PrefabLink() { AssetId = "f1f41fef03cb5734e95db1342f0c605e" };
                    c.DisappearDuration = 0.5f;
                    c.AppearFx = new PrefabLink();
                    c.AppearDuration = 0;
                    c.AlongPath = false;
                    c.AlongPathDistanceMuliplier = 1;
                });
                bp.AddComponent<AdditionalAbilityEffectRunActionOnClickedTarget>(c => {
                    c.Action = Helpers.CreateActionList(
                        new ContextActionProvokeAttackOfOpportunity() { ApplyToCaster =  true }
                        );
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Custom;
                bp.CanTargetPoint = false;
                bp.CanTargetFriends = false;
                bp.CanTargetEnemies = true;
                bp.SpellResistance = false;
                bp.CustomRange = new Feet() { m_Value = 50 };
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Immediate;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.DisableLog = true;
            });

            var GodMeddlingPushAbility = Helpers.CreateBlueprint<BlueprintAbility>("GodMeddlingPushAbility", bp => {
                bp.SetName("God Meddled - Push");
                bp.SetDescription("");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionPush() {
                            Distance = 10,
                            ProvokeAttackOfOpportunity = false
                        });
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 10.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = false;
                bp.CanTargetFriends = false;
                bp.CanTargetEnemies = false;
                bp.SpellResistance = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Immediate;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.DisableLog = true;
            });


            var GodMeddlingCasterLevelBuff = Helpers.CreateBuff("GodMeddlingCasterLevelBuff", bp => {
                bp.SetName("God Meddled - Quicken Spell");
                bp.SetDescription("Your caster level is treated as 1 higher for 1 round.");
                bp.AddComponent<IncreaseCasterLevel>(c => {
                    c.Value = 1;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.m_Icon = GodMeddledIcon;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });

            var GodMeddlingQuickenSpellBuff = Helpers.CreateBuff("GodMeddlingQuickenSpellBuff", bp => {
                bp.SetName("God Meddled - Quicken Spell");
                bp.SetDescription("The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Quicken;
                    c.Abilities = new List<BlueprintAbilityReference> { };
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = true;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = false;
                    c.m_Spellbook = new BlueprintSpellbookReference();
                });
                bp.m_Icon = GodMeddledIcon;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });


            var GodMeddlingActionBuff = Helpers.CreateBuff("GodMeddlingActionBuff", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("You have been affected by one of the below effects, and are immune to the gods' meddling for one round." +
                    "\n1: You are teleported to a random enemy, and then you provoke an attack of opportunity." +
                    "\n2-3: You are affected by the penalties of the slow spell for 1 round." +
                    "\n4-5: You are blinded for 1 round." +
                    "\n6-7: You are knocked prone." +
                    "\n8-10: You shrink by one size category for 1 round, as reduce person." +
                    "\n11-13: You grow by one size category for 1 round, as enlarge person." +
                    "\n14-15: Your caster level is treated as 1 higher for 1 round." +
                    "\n16-17: Creatures adjacent to you are pushed 10 feet away from the space you occupy." +
                    "\n18-19: You are affected by the benefits of haste for 1 round." +
                    "\n20: The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                            new ContextActionRandomize() {
                                m_Actions = new ContextActionRandomize.ActionWrapper[] {
                                    //1
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 1,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionOnRandomTargetsAround() {
                                                OnEnemies = true,
                                                NumberOfTargets = 1,
                                                m_FilterNoFact = RecentlyDeanBuff.ToReference<BlueprintUnitFactReference>(),
                                                Radius = new Kingmaker.Utility.Feet() { m_Value = 50 },
                                                Actions = Helpers.CreateActionList(
                                                    new ContextActionCastSpell() {
                                                        m_Spell = GodMeddlingTeleportAbility.ToReference<BlueprintAbilityReference>(),
                                                        OverrideDC = false,
                                                        DC = 0,
                                                        OverrideSpellLevel = false,
                                                        SpellLevel = 0,
                                                        CastByTarget = false,
                                                        LogIfCanNotTarget = false,
                                                        MarkAsChild = false
                                                    }
                                                    )
                                            }
                                            )
                                    },
                                    //2-3
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 2,
                                        Action = Helpers.CreateActionList(
                                            new Conditional() {
                                                ConditionsChecker = new ConditionsChecker() {
                                                    Operation = Operation.And,
                                                    Conditions = new Condition[] {
                                                        new ContextConditionHasFact() {
                                                            Not = false,
                                                            m_Fact = HasteBuff.ToReference<BlueprintUnitFactReference>()
                                                        }
                                                    }
                                                },
                                                IfTrue = Helpers.CreateActionList(
                                                    new ContextActionRemoveBuffSingleStack() {
                                                        m_TargetBuff = HasteBuff.ToReference<BlueprintBuffReference>()
                                                    }
                                                    ),
                                                IfFalse = Helpers.CreateActionList(
                                                    new ContextActionApplyBuff() {
                                                        m_Buff = SlowBuff.ToReference<BlueprintBuffReference>(),
                                                        Permanent = false,
                                                        UseDurationSeconds = false,
                                                        DurationValue = new ContextDurationValue() {
                                                            Rate = DurationRate.Rounds,
                                                            DiceType = DiceType.Zero,
                                                            DiceCountValue = 0,
                                                            BonusValue = 1
                                                        },
                                                        DurationSeconds = 0
                                                    }
                                                    )
                                            }                                            
                                            )
                                    },
                                    //4-5
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 2,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = BlindBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = false,
                                                UseDurationSeconds = false,
                                                DurationValue = new ContextDurationValue() {
                                                    Rate = DurationRate.Rounds,
                                                    DiceType = DiceType.Zero,
                                                    DiceCountValue = 0,
                                                    BonusValue = 1
                                                },
                                                DurationSeconds = 0
                                            }
                                            )
                                    },
                                    //6-7
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 2,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = ProneBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = false,
                                                UseDurationSeconds = false,
                                                DurationValue = new ContextDurationValue() {
                                                    Rate = DurationRate.Rounds,
                                                    DiceType = DiceType.Zero,
                                                    DiceCountValue = 0,
                                                    BonusValue = 1
                                                },
                                                DurationSeconds = 0
                                            }
                                            )
                                    },
                                    //8-10
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 3,
                                        Action = Helpers.CreateActionList(
                                            new Conditional() {
                                                ConditionsChecker = new ConditionsChecker() {
                                                    Operation = Operation.And,
                                                    Conditions = new Condition[] {
                                                        new ContextConditionHasFact() {
                                                            Not = false,
                                                            m_Fact = EnlargePersonBuff.ToReference<BlueprintUnitFactReference>()
                                                        }
                                                    }
                                                },
                                                IfTrue = Helpers.CreateActionList(
                                                    new ContextActionRemoveBuff() {
                                                        m_Buff = EnlargePersonBuff.ToReference<BlueprintBuffReference>()
                                                    }
                                                    ),
                                                IfFalse = Helpers.CreateActionList(
                                                    new ContextActionApplyBuff() {
                                                        m_Buff = ReducePersonBuff.ToReference<BlueprintBuffReference>(),
                                                        Permanent = false,
                                                        UseDurationSeconds = false,
                                                        DurationValue = new ContextDurationValue() {
                                                            Rate = DurationRate.Rounds,
                                                            DiceType = DiceType.Zero,
                                                            DiceCountValue = 0,
                                                            BonusValue = 1
                                                        },
                                                        DurationSeconds = 0
                                                    }
                                                    )
                                            }
                                            )
                                    },
                                    //11-13
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 3,
                                        Action = Helpers.CreateActionList(
                                            new Conditional() {
                                                ConditionsChecker = new ConditionsChecker() {
                                                    Operation = Operation.And,
                                                    Conditions = new Condition[] {
                                                        new ContextConditionHasFact() {
                                                            Not = false,
                                                            m_Fact = ReducePersonBuff.ToReference<BlueprintUnitFactReference>()
                                                        }
                                                    }
                                                },
                                                IfTrue = Helpers.CreateActionList(
                                                    new ContextActionRemoveBuff() {
                                                        m_Buff = ReducePersonBuff.ToReference<BlueprintBuffReference>()
                                                    }
                                                    ),
                                                IfFalse = Helpers.CreateActionList(
                                                    new ContextActionApplyBuff() {
                                                        m_Buff = EnlargePersonBuff.ToReference<BlueprintBuffReference>(),
                                                        Permanent = false,
                                                        UseDurationSeconds = false,
                                                        DurationValue = new ContextDurationValue() {
                                                            Rate = DurationRate.Rounds,
                                                            DiceType = DiceType.Zero,
                                                            DiceCountValue = 0,
                                                            BonusValue = 1
                                                        },
                                                        DurationSeconds = 0
                                                    }
                                                    )
                                            }
                                            )
                                    },
                                    //14-15
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 2,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = GodMeddlingCasterLevelBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = false,
                                                UseDurationSeconds = false,
                                                DurationValue = new ContextDurationValue() {
                                                    Rate = DurationRate.Rounds,
                                                    DiceType = DiceType.Zero,
                                                    DiceCountValue = 0,
                                                    BonusValue = 1
                                                },
                                                DurationSeconds = 0
                                            }
                                            )
                                    },
                                    //16-17
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 1,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionCastSpell() {
                                                m_Spell = GodMeddlingPushAbility.ToReference<BlueprintAbilityReference>(),
                                                OverrideDC = false,
                                                DC = 0,
                                                OverrideSpellLevel = false,
                                                SpellLevel = 0,
                                                CastByTarget = false,
                                                LogIfCanNotTarget = false,
                                                MarkAsChild = false
                                            }
                                            )
                                    },
                                    //18-19
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 2,
                                        Action = Helpers.CreateActionList(
                                            new Conditional() {
                                                ConditionsChecker = new ConditionsChecker() {
                                                    Operation = Operation.And,
                                                    Conditions = new Condition[] {
                                                        new ContextConditionHasFact() {
                                                            Not = false,
                                                            m_Fact = SlowBuff.ToReference<BlueprintUnitFactReference>()
                                                        }
                                                    }
                                                },
                                                IfTrue = Helpers.CreateActionList(
                                                    new ContextActionRemoveBuff() {
                                                        m_Buff = SlowBuff.ToReference<BlueprintBuffReference>()
                                                    }
                                                    ),
                                                IfFalse = Helpers.CreateActionList(
                                                    new ContextActionApplyBuff() {
                                                        m_Buff = HasteBuff.ToReference<BlueprintBuffReference>(),
                                                        Permanent = false,
                                                        UseDurationSeconds = false,
                                                        DurationValue = new ContextDurationValue() {
                                                            Rate = DurationRate.Rounds,
                                                            DiceType = DiceType.Zero,
                                                            DiceCountValue = 0,
                                                            BonusValue = 1
                                                        },
                                                        DurationSeconds = 0
                                                    }
                                                    )
                                            }
                                            )
                                    },
                                    //20
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 1,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = GodMeddlingQuickenSpellBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = false,
                                                UseDurationSeconds = false,
                                                DurationValue = new ContextDurationValue() {
                                                    Rate = DurationRate.Rounds,
                                                    DiceType = DiceType.Zero,
                                                    DiceCountValue = 0,
                                                    BonusValue = 1
                                                },
                                                DurationSeconds = 0
                                            }
                                            )
                                    }
                                }
                            }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.m_Icon = GodMeddledIcon;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });

            var GodMeddlingBeneficialActionBuff = Helpers.CreateBuff("GodMeddlingBeneficialActionBuff", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("You have been affected by one of the below effects, and are immune to the gods' meddling for one round." +
                    "\n1-7: No effect." +
                    "\n8-10: You shrink by one size category for 1 round, as reduce person." +
                    "\n11-13: You grow by one size category for 1 round, as enlarge person." +
                    "\n14-15: Your caster level is treated as 1 higher for 1 round." +
                    "\n16-17: Creatures adjacent to you are pushed 10 feet away from the space you occupy." +
                    "\n18-19: You are affected by the benefits of haste for 1 round." +
                    "\n20: The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                            new ContextActionRandomize() {
                                m_Actions = new ContextActionRandomize.ActionWrapper[] {
                                    //1-7
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 7,
                                        Action = Helpers.CreateActionList()
                                    },                                    
                                    //8-10
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 3,
                                        Action = Helpers.CreateActionList(
                                            new Conditional() {
                                                ConditionsChecker = new ConditionsChecker() {
                                                    Operation = Operation.And,
                                                    Conditions = new Condition[] {
                                                        new ContextConditionHasFact() {
                                                            Not = false,
                                                            m_Fact = EnlargePersonBuff.ToReference<BlueprintUnitFactReference>()
                                                        }
                                                    }
                                                },
                                                IfTrue = Helpers.CreateActionList(
                                                    new ContextActionRemoveBuff() {
                                                        m_Buff = EnlargePersonBuff.ToReference<BlueprintBuffReference>()
                                                    }
                                                    ),
                                                IfFalse = Helpers.CreateActionList(
                                                    new ContextActionApplyBuff() {
                                                        m_Buff = ReducePersonBuff.ToReference<BlueprintBuffReference>(),
                                                        Permanent = false,
                                                        UseDurationSeconds = false,
                                                        DurationValue = new ContextDurationValue() {
                                                            Rate = DurationRate.Rounds,
                                                            DiceType = DiceType.Zero,
                                                            DiceCountValue = 0,
                                                            BonusValue = 1
                                                        },
                                                        DurationSeconds = 0
                                                    }
                                                    )
                                            }
                                            )
                                    },
                                    //11-13
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 3,
                                        Action = Helpers.CreateActionList(
                                            new Conditional() {
                                                ConditionsChecker = new ConditionsChecker() {
                                                    Operation = Operation.And,
                                                    Conditions = new Condition[] {
                                                        new ContextConditionHasFact() {
                                                            Not = false,
                                                            m_Fact = ReducePersonBuff.ToReference<BlueprintUnitFactReference>()
                                                        }
                                                    }
                                                },
                                                IfTrue = Helpers.CreateActionList(
                                                    new ContextActionRemoveBuff() {
                                                        m_Buff = ReducePersonBuff.ToReference<BlueprintBuffReference>()
                                                    }
                                                    ),
                                                IfFalse = Helpers.CreateActionList(
                                                    new ContextActionApplyBuff() {
                                                        m_Buff = EnlargePersonBuff.ToReference<BlueprintBuffReference>(),
                                                        Permanent = false,
                                                        UseDurationSeconds = false,
                                                        DurationValue = new ContextDurationValue() {
                                                            Rate = DurationRate.Rounds,
                                                            DiceType = DiceType.Zero,
                                                            DiceCountValue = 0,
                                                            BonusValue = 1
                                                        },
                                                        DurationSeconds = 0
                                                    }
                                                    )
                                            }
                                            )
                                    },
                                    //14-15
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 2,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = GodMeddlingCasterLevelBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = false,
                                                UseDurationSeconds = false,
                                                DurationValue = new ContextDurationValue() {
                                                    Rate = DurationRate.Rounds,
                                                    DiceType = DiceType.Zero,
                                                    DiceCountValue = 0,
                                                    BonusValue = 1
                                                },
                                                DurationSeconds = 0
                                            }
                                            )
                                    },
                                    //16-17
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 1,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionCastSpell() {
                                                m_Spell = GodMeddlingPushAbility.ToReference<BlueprintAbilityReference>(),
                                                OverrideDC = false,
                                                DC = 0,
                                                OverrideSpellLevel = false,
                                                SpellLevel = 0,
                                                CastByTarget = false,
                                                LogIfCanNotTarget = false,
                                                MarkAsChild = false
                                            }
                                            )
                                    },
                                    //18-19
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 2,
                                        Action = Helpers.CreateActionList(
                                            new Conditional() {
                                                ConditionsChecker = new ConditionsChecker() {
                                                    Operation = Operation.And,
                                                    Conditions = new Condition[] {
                                                        new ContextConditionHasFact() {
                                                            Not = false,
                                                            m_Fact = SlowBuff.ToReference<BlueprintUnitFactReference>()
                                                        }
                                                    }
                                                },
                                                IfTrue = Helpers.CreateActionList(
                                                    new ContextActionRemoveBuff() {
                                                        m_Buff = SlowBuff.ToReference<BlueprintBuffReference>()
                                                    }
                                                    ),
                                                IfFalse = Helpers.CreateActionList(
                                                    new ContextActionApplyBuff() {
                                                        m_Buff = HasteBuff.ToReference<BlueprintBuffReference>(),
                                                        Permanent = false,
                                                        UseDurationSeconds = false,
                                                        DurationValue = new ContextDurationValue() {
                                                            Rate = DurationRate.Rounds,
                                                            DiceType = DiceType.Zero,
                                                            DiceCountValue = 0,
                                                            BonusValue = 1
                                                        },
                                                        DurationSeconds = 0
                                                    }
                                                    )
                                            }
                                            )
                                    },
                                    //20
                                    new ContextActionRandomize.ActionWrapper{
                                        Weight = 1,
                                        Action = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = GodMeddlingQuickenSpellBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = false,
                                                UseDurationSeconds = false,
                                                DurationValue = new ContextDurationValue() {
                                                    Rate = DurationRate.Rounds,
                                                    DiceType = DiceType.Zero,
                                                    DiceCountValue = 0,
                                                    BonusValue = 1
                                                },
                                                DurationSeconds = 0
                                            }
                                            )
                                    }
                                }
                            }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.m_Icon = GodMeddledIcon;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });

            var GodMeddlingInCombatBuff = Helpers.CreateBuff("GodMeddlingInCombatBuff", bp => {
                bp.SetName("God Meddled Combat Buff");
                bp.SetDescription("");
                bp.AddComponent<ContextActionOnApplyingSpell>(c => {
                    c.m_AffectedSpellSource = ContextActionOnApplyingSpell.AffectedSpellSource.Divine;
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = GodMeddlingActionBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = GodMeddlingActionBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 1,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                        }
                        );
                });
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });

            var GodMeddlingBeneficialInCombatBuff = Helpers.CreateBuff("GodMeddlingBeneficialInCombatBuff", bp => {
                bp.SetName("God Meddled Beneficial Combat Buff");
                bp.SetDescription("");
                bp.AddComponent<ContextActionOnApplyingSpell>(c => {
                    c.m_AffectedSpellSource = ContextActionOnApplyingSpell.AffectedSpellSource.Divine;
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        Not = false,
                                        m_Buff = GodMeddlingBeneficialActionBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = GodMeddlingBeneficialActionBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 1,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                })
                        }
                        );
                });
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });

            var GodMeddledCurseFeatureLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("GodMeddledCurseFeatureLevel1", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("The gods’ interference in your life has left you with strange, unpredictable powers dependent on divine whim. " +
                    "Bizarre side effects occur whenever you are targeted by a spell from a divine caster—for better or for worse. Once per round during combat, " +
                    "when a creature casts a divine spell including you as a target, roll 1d20 and consult the table below." +
                    "\n1: You are teleported to a random enemy, and then you provoke an attack of opportunity." +
                    "\n2-3: You are affected by the penalties of the slow spell for 1 round." +
                    "\n4-5: You are blinded for 1 round." +
                    "\n6-7: You are knocked prone." +
                    "\n8-10: You shrink by one size category for 1 round, as reduce person." +
                    "\n11-13: You grow by one size category for 1 round, as enlarge person." +
                    "\n14-15: Your caster level is treated as 1 higher for 1 round." +
                    "\n16-17: Creatures adjacent to you are pushed 10 feet away from the space you occupy." +
                    "\n18-19: You are affected by the benefits of haste for 1 round." +
                    "\n20: The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = GodMeddlingInCombatBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsNotDispelable = true
                        }
                        );
                    c.CombatEndActions = Helpers.CreateActionList();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var GodMeddledCurseBeneficialFeatureLevel1 = Helpers.CreateBlueprint<BlueprintFeature>("GodMeddledCurseBeneficialFeatureLevel1", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("The gods’ interference in your life has left you with strange, unpredictable powers dependent on divine whim. " +
                    "Bizarre side effects occur whenever you are targeted by a spell from a divine caster—for better or for worse. Once per round during combat, " +
                    "when a creature casts a divine spell including you as a target, roll 1d20 and consult the table below." +
                    "\n1-7: No effect." +
                    "\n8-10: You shrink by one size category for 1 round, as reduce person." +
                    "\n11-13: You grow by one size category for 1 round, as enlarge person." +
                    "\n14-15: Your caster level is treated as 1 higher for 1 round." +
                    "\n16-17: Creatures adjacent to you are pushed 10 feet away from the space you occupy." +
                    "\n18-19: You are affected by the benefits of haste for 1 round." +
                    "\n20: The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = GodMeddlingBeneficialInCombatBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsNotDispelable = true
                        }
                        );
                    c.CombatEndActions = Helpers.CreateActionList();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var GodMeddledCurseFeatureLevel5 = Helpers.CreateBlueprint<BlueprintFeature>("GodMeddledCurseFeatureLevel5", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("At 5th level, you gain a +2 competence bonus on saving throws to resist divine spells.");
                bp.AddComponent<SavingThrowBonusAgainstMagicSource>(c => {
                    c.m_AffectedSpellSource = SavingThrowBonusAgainstMagicSource.AffectedSpellSource.Divine;
                    c.Descriptor = ModifierDescriptor.Competence;
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var GodMeddledCurseFeatureLevel10 = Helpers.CreateBlueprint<BlueprintFeature>("GodMeddledCurseFeatureLevel10", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("At 10th level, you become immune to the confused condition.");
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Confusion;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var GodMeddledCurseFeatureLevel15 = Helpers.CreateBlueprint<BlueprintFeature>("GodMeddledCurseFeatureLevel15", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("At 15th level, you gain a +4 competence bonus on saving throws to resist mind-affecting effects.");
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                    c.ModifierDescriptor = ModifierDescriptor.Competence;
                    c.Value = 4;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var GodMeddledCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("GodMeddledCurseProgression", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("The gods’ interference in your life has left you with strange, unpredictable powers dependent on divine whim. " +
                    "Bizarre side effects occur whenever you are affected by a spell from a divine caster—for better or for worse. Once per round during combat, " +
                    "when a creature casts a divine spell including you as a target, roll 1d20 and consult the table below." +
                    "\nAt 5th level, you gain a +2 competence bonus on saving throws to resist divine spells. " +
                    "\nAt 10th level, you become immune to the confused condition. " +
                    "\nAt 15th level, you gain a +4 competence bonus on saving throws to resist mind-affecting effects." +
                    "\n\nGod Meddled Effects" +
                    "\n1: You are teleported to a random enemy, and then you provoke an attack of opportunity." +
                    "\n2-3: You are affected by the penalties of the slow spell for 1 round." +
                    "\n4-5: You are blinded for 1 round." +
                    "\n6-7: You are knocked prone." +
                    "\n8-10: You shrink by one size category for 1 round, as reduce person." +
                    "\n11-13: You grow by one size category for 1 round, as enlarge person." +
                    "\n14-15: Your caster level is treated as 1 higher for 1 round." +
                    "\n16-17: Creatures adjacent to you are pushed 10 feet away from the space you occupy." +
                    "\n18-19: You are affected by the benefits of haste for 1 round." +
                    "\n20: The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.m_Icon = GodMeddledIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleCurse };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = GodMeddledWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, GodMeddledCurseFeatureLevel1),
                    Helpers.LevelEntry(5, GodMeddledCurseFeatureLevel5),
                    Helpers.LevelEntry(10, GodMeddledCurseFeatureLevel10),
                    Helpers.LevelEntry(15, GodMeddledCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            GodMeddledCurseProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.CheckInProgression = true;
                c.m_Feature = GodMeddledCurseProgression.ToReference<BlueprintFeatureReference>();
                c.HideInUI = true;
            });
            var BeneficialGodMeddledCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("BeneficialGodMeddledCurseProgression", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("The gods’ interference in your life has left you with strange, unpredictable powers dependent on divine whim. " +
                    "Bizarre side effects occur whenever you are affected by a spell from a divine caster—for better or for worse. Once per round during combat, " +
                    "when a creature casts a divine spell including you as a target, roll 1d20 and consult the table below." +
                    "\nAt 5th level, you gain a +2 competence bonus on saving throws to resist divine spells. " +
                    "\nAt 10th level, you become immune to the confused condition. " +
                    "\nAt 15th level, you gain a +4 competence bonus on saving throws to resist mind-affecting effects." +
                    "\n\nGod Meddled Effects" +
                    "\n1-7: No effect." +
                    "\n8-10: You shrink by one size category for 1 round, as reduce person." +
                    "\n11-13: You grow by one size category for 1 round, as enlarge person." +
                    "\n14-15: Your caster level is treated as 1 higher for 1 round." +
                    "\n16-17: Creatures adjacent to you are pushed 10 feet away from the space you occupy." +
                    "\n18-19: You are affected by the benefits of haste for 1 round." +
                    "\n20: The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.m_Icon = GodMeddledIcon;
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = GodMeddledWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = GodMeddledCurseProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, GodMeddledCurseBeneficialFeatureLevel1),
                    Helpers.LevelEntry(5, GodMeddledCurseFeatureLevel5),
                    Helpers.LevelEntry(10, GodMeddledCurseFeatureLevel10),
                    Helpers.LevelEntry(15, GodMeddledCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var GodMeddledCurseNoProgression = Helpers.CreateBlueprint<BlueprintProgression>("GodMeddledCurseNoProgression", bp => {
                bp.SetName("God Meddled");
                bp.SetDescription("The gods’ interference in your life has left you with strange, unpredictable powers dependent on divine whim. " +
                    "Bizarre side effects occur whenever you are affected by a spell from a divine caster—for better or for worse. Once per round during combat, " +
                    "when a creature casts a divine spell including you as a target, roll 1d20 and consult the table below." +
                    "\nAt 5th level, you gain a +2 competence bonus on saving throws to resist divine spells. " +
                    "\nAt 10th level, you become immune to the confused condition. " +
                    "\nAt 15th level, you gain a +4 competence bonus on saving throws to resist mind-affecting effects." +
                    "\n\nGod Meddled Effects" +
                    "\n1: You are teleported to a random enemy, and then you provoke an attack of opportunity." +
                    "\n2-3: You are affected by the penalties of the slow spell for 1 round." +
                    "\n4-5: You are blinded for 1 round." +
                    "\n6-7: You are knocked prone." +
                    "\n8-10: You shrink by one size category for 1 round, as reduce person." +
                    "\n11-13: You grow by one size category for 1 round, as enlarge person." +
                    "\n14-15: Your caster level is treated as 1 higher for 1 round." +
                    "\n16-17: Creatures adjacent to you are pushed 10 feet away from the space you occupy." +
                    "\n18-19: You are affected by the benefits of haste for 1 round." +
                    "\n20: The next spell cast may be cast as a swift action as if modified by the Quicken Spell feat.");
                bp.m_Icon = GodMeddledIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleCurse };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = GodMeddledWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, GodMeddledCurseFeatureLevel1)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = GodMeddledCurseProgression.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
            });

            OracleCurseSelection.m_AllFeatures = OracleCurseSelection.m_AllFeatures.AppendToArray(GodMeddledCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_AllFeatures = BeneficialCurse.m_AllFeatures.AppendToArray(BeneficialGodMeddledCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_Features = BeneficialCurse.m_Features.AppendToArray(BeneficialGodMeddledCurseProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_AllFeatures = MysteryGiftFeatureCurseSelection.m_AllFeatures.AppendToArray(GodMeddledCurseNoProgression.ToReference<BlueprintFeatureReference>());
            MysteryGiftFeatureCurseSelection.m_Features = MysteryGiftFeatureCurseSelection.m_Features.AppendToArray(GodMeddledCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_AllFeatures = DualCursedSecondCurseSelection.m_AllFeatures.AppendToArray(GodMeddledCurseNoProgression.ToReference<BlueprintFeatureReference>());
            DualCursedSecondCurseSelection.m_Features = DualCursedSecondCurseSelection.m_Features.AppendToArray(GodMeddledCurseNoProgression.ToReference<BlueprintFeatureReference>());

        }
    }
}
