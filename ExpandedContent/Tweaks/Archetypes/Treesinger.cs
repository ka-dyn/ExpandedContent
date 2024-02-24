using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.Enums;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Treesinger {
        public static void AddTreesinger() {

            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var DruidAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("3853d5405ebfc0f4a86930bb7082b43b");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("65af7290b4efd5f418132141aaa36c1b");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");
            var DruidBondSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("3830f3630a33eba49b60f511b4c8f2a8");

            var CompanionSaplingTreantFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionSaplingTreantFeature");
            var CompanionCrawlingMoundFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionCrawlingMoundFeature");

            var WildShapeIWolfFeature = Resources.GetBlueprint<BlueprintFeature>("19bb148cb92db224abb431642d10efeb");
            var WildShapeIILeopardFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("c4d651bc0d4eabd41b08ee81bfe701d8");
            var WildShapeElementalSmallFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("bddd46a6f6a3e6e4b99008dcf5271c3b");
            var WildShapeIVBearFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("1368c7ce69702444893af5ffd3226e19");
            var WildShapeElementalFeatureAddMediumFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("6e4b88e2a044c67469c038ac2f09d061");
            var WildShapeIIISmilodonFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("253c0c0d00e50a24797445f20af52dc8");
            var WildShapeElementalFeatureAddLargeFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("e66154511a6f9fc49a9de644bd8922db");
            var WildShapeIVShamblingMoundFeature = Resources.GetBlueprint<BlueprintFeature>("0f31b23c2ab39354bbde4e33e8151495");
            var WildShapeElementalHugeFeatureFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("fe58dd496a36e274b86958f4677071b2");
            var WildShapeResource = Resources.GetBlueprint<BlueprintAbilityResource>("ae6af4d58b70a754d868324d1a05eda4");
            var MasterShapeshifter = Resources.GetBlueprint<BlueprintFeature>("934670ef88b281b4da5596db8b00df2f");
            var EnergizedWildShapeDamageFeature = Resources.GetBlueprint<BlueprintFeature>("d808863c4bd44fd8bd9cf5892460705d");
            var FrightfulShapeFeature = Resources.GetBlueprint<BlueprintFeature>("8e8a34c754d649aa9286fe8ee5cc3f10");
            var FrightfulShapeAttackBuff = Resources.GetBlueprint<BlueprintBuff>("1a5a2ce6793a4458957f45517662bb0e");

            var PlantDomainProgressionDruid = Resources.GetBlueprint<BlueprintProgression>("ee77836d10835fd49a8831adb3a14640");
            var GrowthDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("GrowthDomainProgressionDruid");

            var TreesingerArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("TreesingerArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"TreesingerArchetype.Name", "Treesinger");
                bp.LocalizedDescription = Helpers.CreateString($"TreesingerArchetype.Description", "Elves live far longer than other common races, and a single elf may see whole " +
                    "empires rise and fall. Given the impermanence of the cultures around them, it’s small wonder that some elves turn to the timeless growth of nature for solace, " +
                    "finding allies among the great trees themselves, and even leading the forest’s plants into combat.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"TreesingerArchetype.Description", "Elves live far longer than other common races, and a single elf may see " +
                    "whole empires rise and fall. Given the impermanence of the cultures around them, it’s small wonder that some elves turn to the timeless growth of nature for " +
                    "solace, finding allies among the great trees themselves, and even leading the forest’s plants into combat.");
                
            });




            var TreesingerCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("TreesingerCompanionSelection", bp => {
                bp.SetName("Plant Companion");
                bp.SetDescription("A treesinger may begin play with a plant companion. This plant is a loyal companion that accompanies the treesinger on her adventures. " +
                    "Except for the companion being a creature of the plant type, drawn from the list of plant companions, this ability otherwise works like the standard " +
                    "druid’s animal companion ability.");
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DruidAnimalCompanionProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(CompanionSaplingTreantFeature, CompanionCrawlingMoundFeature);
            });


            var TreesingerDomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("TreesingerDomainSelection", bp => {
                bp.SetName("Treesingers Domain");
                bp.SetDescription("A treesinger may begin play with a plant companion. This plant is a loyal companion that accompanies the treesinger on her adventures. " +
                    "Except for the companion being a creature of the plant type, drawn from the list of plant companions, this ability otherwise works like the standard " +
                    "druid’s animal companion ability. \nA treesinger may instead may choose the plant or growth domain.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    PlantDomainProgressionDruid.ToReference<BlueprintFeatureReference>(),
                    GrowthDomainProgressionDruid.ToReference<BlueprintFeatureReference>()
                };
            });

            var TreesingerPlantBondSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("TreesingerPlantBondSelection", bp => {
                bp.SetName("Treesingers Bond");
                bp.SetDescription("A treesinger may begin play with a plant companion. This plant is a loyal companion that accompanies the treesinger on her adventures. " +
                    "Except for the companion being a creature of the plant type, drawn from the list of plant companions, this ability otherwise works like the standard " +
                    "druid’s animal companion ability. \nA treesinger may instead may choose the plant or growth domain.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(TreesingerCompanionSelection, TreesingerDomainSelection);
            });


            var PlantShapeIBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIBuff");
            var TreesingerWildShapeMandragoraBuff = Helpers.CreateBuff("TreesingerWildShapeMandragoraBuff", bp => {
                bp.SetName("Wild Shape (Mandragora)");
                bp.m_Description = PlantShapeIBuff.m_Description;
                bp.Components = PlantShapeIBuff.Components;
                bp.m_Icon = PlantShapeIBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = PlantShapeIBuff.FxOnStart;
            });
            var TreesingerWildShapeMandragoraPolymorph = Resources.GetModBlueprint<BlueprintBuff>("TreesingerWildShapeMandragoraBuff").GetComponent<Polymorph>();
            TreesingerWildShapeMandragoraPolymorph.m_Facts = TreesingerWildShapeMandragoraPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            TreesingerWildShapeMandragoraBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                            new ContextConditionHasFact() {
                                m_Fact = FrightfulShapeFeature.ToReference<BlueprintUnitFactReference>(),
                                Not = false
                            }
                        }
                        },
                        IfTrue = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FrightfulShapeAttackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = false,
                            },
                            AsChild = true
                        }),
                        IfFalse = Helpers.CreateActionList()
                    });
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList();
            });


            var PlantShapeIIShamblingMoundBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIBuff");
            var TreesingerWildShapeShamblingMoundBuff = Helpers.CreateBuff("TreesingerWildShapeShamblingMoundBuff", bp => {
                bp.SetName("Wild Shape (Shambling Mound)");
                bp.m_Description = PlantShapeIIShamblingMoundBuff.m_Description;
                bp.Components = PlantShapeIIShamblingMoundBuff.Components;
                bp.m_Icon = PlantShapeIIShamblingMoundBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = PlantShapeIIShamblingMoundBuff.FxOnStart;
            });
            var TreesingerWildShapeShamblingMoundPolymorph = Resources.GetModBlueprint<BlueprintBuff>("TreesingerWildShapeShamblingMoundBuff").GetComponent<Polymorph>();
            TreesingerWildShapeShamblingMoundPolymorph.m_Facts = TreesingerWildShapeShamblingMoundPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            TreesingerWildShapeShamblingMoundBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                            new ContextConditionHasFact() {
                                m_Fact = FrightfulShapeFeature.ToReference<BlueprintUnitFactReference>(),
                                Not = false
                            }
                        }
                        },
                        IfTrue = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FrightfulShapeAttackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = false,
                            },
                            AsChild = true
                        }),
                        IfFalse = Helpers.CreateActionList()
                    });
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList();
            });


            var PlantShapeIIQuickwoodBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIQuickwoodBuff");
            var TreesingerWildShapeQuickwoodBuff = Helpers.CreateBuff("TreesingerWildShapeQuickwoodBuff", bp => {
                bp.SetName("Wild Shape (Quickwood)");
                bp.m_Description = PlantShapeIIQuickwoodBuff.m_Description;
                bp.Components = PlantShapeIIQuickwoodBuff.Components;
                bp.m_Icon = PlantShapeIIQuickwoodBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = PlantShapeIIQuickwoodBuff.FxOnStart;
            });
            var TreesingerWildShapeQuickwoodPolymorph = Resources.GetModBlueprint<BlueprintBuff>("TreesingerWildShapeQuickwoodBuff").GetComponent<Polymorph>();
            TreesingerWildShapeQuickwoodPolymorph.m_Facts = TreesingerWildShapeQuickwoodPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            TreesingerWildShapeQuickwoodBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                            new ContextConditionHasFact() {
                                m_Fact = FrightfulShapeFeature.ToReference<BlueprintUnitFactReference>(),
                                Not = false
                            }
                        }
                        },
                        IfTrue = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FrightfulShapeAttackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = false,
                            },
                            AsChild = true
                        }),
                        IfFalse = Helpers.CreateActionList()
                    });
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList();
            });


            var PlantShapeIIITreantBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIITreantBuff");
            var TreesingerWildShapeTreantBuff = Helpers.CreateBuff("TreesingerWildShapeTreantBuff", bp => {
                bp.SetName("Wild Shape (Treant)");
                bp.m_Description = PlantShapeIIITreantBuff.m_Description;
                bp.Components = PlantShapeIIITreantBuff.Components;
                bp.m_Icon = PlantShapeIIITreantBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = PlantShapeIIITreantBuff.FxOnStart;
            });
            var TreesingerWildShapeTreantPolymorph = Resources.GetModBlueprint<BlueprintBuff>("TreesingerWildShapeTreantBuff").GetComponent<Polymorph>();
            TreesingerWildShapeTreantPolymorph.m_Facts = TreesingerWildShapeTreantPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            TreesingerWildShapeTreantBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                            new ContextConditionHasFact() {
                                m_Fact = FrightfulShapeFeature.ToReference<BlueprintUnitFactReference>(),
                                Not = false
                            }
                        }
                        },
                        IfTrue = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FrightfulShapeAttackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = false,
                            },
                            AsChild = true
                        }),
                        IfFalse = Helpers.CreateActionList()
                    });
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList();
            });


            var PlantShapeIIIGiantFlytrapBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIIGiantFlytrapBuff");
            var TreesingerWildShapeGiantFlytrapBuff = Helpers.CreateBuff("TreesingerWildShapeGiantFlytrapBuff", bp => {
                bp.SetName("Wild Shape (Giant Flytrap)");
                bp.m_Description = PlantShapeIIIGiantFlytrapBuff.m_Description;
                bp.Components = PlantShapeIIIGiantFlytrapBuff.Components;
                bp.m_Icon = PlantShapeIIIGiantFlytrapBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = PlantShapeIIIGiantFlytrapBuff.FxOnStart;
            });
            var TreesingerWildShapeGiantFlytrapPolymorph = Resources.GetModBlueprint<BlueprintBuff>("TreesingerWildShapeGiantFlytrapBuff").GetComponent<Polymorph>();
            TreesingerWildShapeGiantFlytrapPolymorph.m_Facts = TreesingerWildShapeGiantFlytrapPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            TreesingerWildShapeGiantFlytrapBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                            new ContextConditionHasFact() {
                                m_Fact = FrightfulShapeFeature.ToReference<BlueprintUnitFactReference>(),
                                Not = false
                            }
                        }
                        },
                        IfTrue = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FrightfulShapeAttackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = false,
                            },
                            AsChild = true
                        }),
                        IfFalse = Helpers.CreateActionList()
                    });
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList();
            });



            var TreesingerWildShapeMandragoraAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreesingerWildShapeMandragoraAbility", bp => {
                bp.SetName("Wild Shape (Mandragora)");
                bp.SetDescription("You become a small mandragora. You gain a +2 size bonus to your Dexterity and Constitution and a +2 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You also gain one 1d6 bite attack, two 1d4 slams and poison ability.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TreesingerWildShapeMandragoraBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = true,
                            ToCaster = true,
                            AsChild = false,
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WildShapeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.BonusValue;
                    c.m_StepLevel = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        DruidClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { TreesingerWildShapeMandragoraBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreesingerWildShapeShamblingMoundAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreesingerWildShapeShamblingMoundAbility", bp => {
                bp.SetName("Wild Shape (Shambling Mound)");
                bp.SetDescription("You become a large shambling mound. You gain a +4 size bonus to your Strength, a +2 size bonus to your Constitution, +4 natural " +
                    "armor bonus, resist fire 20, and resist electricity 20. Your movement speed is reduced by 10 feet. You also have two 2d6 slam attacks, the constricting " +
                    "vines ability, and the poison ability.\nConstricting Vines: A shambling mound's vines coil around any creature it hits with a slam attack. The shambling " +
                    "mound attempts a grapple maneuver check against its target, and on a successful check its vines deal 2d6+5 damage and the foe is grappled.\nGrappled " +
                    "characters cannot move, and take a -2 penalty on all attack rolls and a -4 penalty to Dexterity. Grappled characters attempt to escape every round by " +
                    "making a successful combat maneuver, Strength, Athletics, or Mobility check. The DC of this check is the shambling mound's CMD.\nEach round, creatures " +
                    "grappled by a shambling mound suffer 4d6+Strength modifier × 2 damage.\nA shambling mound receives a +4 bonus on grapple maneuver checks.\nPoison:\nSlam; " +
                    "Save: Fortitude\nFrequency: 1/round for 2 rounds\nEffect: 1d2 Strength and 1d2 Dexterity damage\nCure: 1 save\nThe save DC is Constitution-based.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TreesingerWildShapeShamblingMoundBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = true,
                            ToCaster = true,
                            AsChild = false,
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WildShapeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.BonusValue;
                    c.m_StepLevel = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        DruidClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { TreesingerWildShapeShamblingMoundBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreesingerWildShapeQuickwoodAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreesingerWildShapeQuickwoodAbility", bp => {
                bp.SetName("Wild Shape (Quickwood)");
                bp.SetDescription("You become a huge quickwood. You gain a +4 size bonus to your Strength, a +2 size bonus to your Constitution, +4 natural " +
                    "armor bonus, resist fire 20, resist electricity 20, and spell resistance equal to 10 + half your {g|Encyclopedia:Caster_Level}caster level{/g}. " +
                    "Your movement speed is reduced by 10 feet. You also have one 2d6 bite attack and two 1d4 root (tentacle) attacks.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TreesingerWildShapeQuickwoodBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = true,
                            ToCaster = true,
                            AsChild = false,
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WildShapeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.BonusValue;
                    c.m_StepLevel = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        DruidClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { TreesingerWildShapeQuickwoodBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreesingerWildShapeGiantFlytrapAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreesingerWildShapeGiantFlytrapAbility", bp => {
                bp.SetName("Wild Shape (Giant Flytrap)");
                bp.SetDescription("You become a huge giant flytrap. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain four 1d8 bite attacks, acid Resistance 20 and blindsight and poison ability.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TreesingerWildShapeGiantFlytrapBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = true,
                            ToCaster = true,
                            AsChild = false,
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WildShapeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.BonusValue;
                    c.m_StepLevel = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        DruidClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { TreesingerWildShapeGiantFlytrapBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreesingerWildShapeTreantAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreesingerWildShapeTreantAbility", bp => {
                bp.SetName("Wild Shape (Treant)");
                bp.SetDescription("You become a huge treant. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun ability.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TreesingerWildShapeTreantBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = true,
                            ToCaster = true,
                            AsChild = false,
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WildShapeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.BonusValue;
                    c.m_StepLevel = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        DruidClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { TreesingerWildShapeTreantBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var TreesingerWildShapeMandragoraFeature = Helpers.CreateBlueprint<BlueprintFeature>("TreesingerWildShapeMandragoraFeature", bp => {
                bp.SetName("Wild Shape (Mandragora)");
                bp.SetDescription("At 6th level, a treesinger can use wildshape to change into a mandragora and back twice a day. The effect lasts for 1 hour per druid level, or until she " +
                    "changes back. \nChanging form is a {g|Encyclopedia:Standard_Actions}standard action{/g} and doesn't provoke an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}. " +
                    "A druid can use this ability an additional time per day at 8th level and every two levels thereafter, for a total of eight times at 18th level. At 9th level, a treesinger " +
                    "can use wild shape to change into a shambling mound ro a quickwood. At 12th level, a treesinger can use wild shape to change into a giant flytrap or a treant.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TreesingerWildShapeMandragoraAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WildShapeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.IsPrerequisiteFor = WildShapeIWolfFeature.IsPrerequisiteFor;
            });
            var TreesingerWildShapeShamblingMoundFeature = Helpers.CreateBlueprint<BlueprintFeature>("TreesingerWildShapeShamblingMoundFeature", bp => {
                bp.SetName("Wild Shape (Shambling Mound)");
                bp.SetDescription("At 6th level, a treesinger can use wildshape to change into a mandragora and back twice a day. The effect lasts for 1 hour per druid level, or until she " +
                    "changes back. \nChanging form is a {g|Encyclopedia:Standard_Actions}standard action{/g} and doesn't provoke an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}. " +
                    "A druid can use this ability an additional time per day at 8th level and every two levels thereafter, for a total of eight times at 18th level. At 9th level, a treesinger " +
                    "can use wild shape to change into a shambling mound ro a quickwood. At 12th level, a treesinger can use wild shape to change into a giant flytrap or a treant.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TreesingerWildShapeShamblingMoundAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var TreesingerWildShapeQuickwoodFeature = Helpers.CreateBlueprint<BlueprintFeature>("TreesingerWildShapeQuickwoodFeature", bp => {
                bp.SetName("Wild Shape (Quickwood)");
                bp.SetDescription("At 6th level, a treesinger can use wildshape to change into a mandragora and back twice a day. The effect lasts for 1 hour per druid level, or until she " +
                    "changes back. \nChanging form is a {g|Encyclopedia:Standard_Actions}standard action{/g} and doesn't provoke an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}. " +
                    "A druid can use this ability an additional time per day at 8th level and every two levels thereafter, for a total of eight times at 18th level. At 9th level, a treesinger " +
                    "can use wild shape to change into a shambling mound ro a quickwood. At 12th level, a treesinger can use wild shape to change into a giant flytrap or a treant.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TreesingerWildShapeQuickwoodAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var TreesingerWildShapeGiantFlytrapFeature = Helpers.CreateBlueprint<BlueprintFeature>("TreesingerWildShapeGiantFlytrapFeature", bp => {
                bp.SetName("Wild Shape (Giant Flytrap)");
                bp.SetDescription("At 6th level, a treesinger can use wildshape to change into a mandragora and back twice a day. The effect lasts for 1 hour per druid level, or until she " +
                    "changes back. \nChanging form is a {g|Encyclopedia:Standard_Actions}standard action{/g} and doesn't provoke an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}. " +
                    "A druid can use this ability an additional time per day at 8th level and every two levels thereafter, for a total of eight times at 18th level. At 9th level, a treesinger " +
                    "can use wild shape to change into a shambling mound ro a quickwood. At 12th level, a treesinger can use wild shape to change into a giant flytrap or a treant.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TreesingerWildShapeGiantFlytrapAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var TreesingerWildShapeTreantFeature = Helpers.CreateBlueprint<BlueprintFeature>("TreesingerWildShapeTreantFeature", bp => {
                bp.SetName("Wild Shape (Treant)");
                bp.SetDescription("At 6th level, a treesinger can use wildshape to change into a mandragora and back twice a day. The effect lasts for 1 hour per druid level, or until she " +
                    "changes back. \nChanging form is a {g|Encyclopedia:Standard_Actions}standard action{/g} and doesn't provoke an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}. " +
                    "A druid can use this ability an additional time per day at 8th level and every two levels thereafter, for a total of eight times at 18th level. At 9th level, a treesinger " +
                    "can use wild shape to change into a shambling mound ro a quickwood. At 12th level, a treesinger can use wild shape to change into a giant flytrap or a treant.");
                bp.m_Icon = WildShapeIVShamblingMoundFeature.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TreesingerWildShapeTreantAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            // NaturalSpell patch
            var NaturalSpell = Resources.GetBlueprint<BlueprintFeature>("c806103e27cce6f429e5bf47067966cf");
            NaturalSpell.AddComponent<PrerequisiteFeature>(c => {
                c.Group = Prerequisite.GroupType.Any;
                c.m_Feature = TreesingerWildShapeMandragoraFeature.ToReference<BlueprintFeatureReference>();
            });


            TreesingerArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DruidBondSelection),
                    Helpers.LevelEntry(4, WildShapeIWolfFeature),
                    Helpers.LevelEntry(6, WildShapeIILeopardFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(8, WildShapeIVBearFeature, WildShapeElementalFeatureAddMediumFeature),
                    Helpers.LevelEntry(10, WildShapeElementalFeatureAddLargeFeature, WildShapeIIISmilodonFeature, WildShapeIVShamblingMoundFeature),
                    Helpers.LevelEntry(12, WildShapeElementalHugeFeatureFeature)
            };
            TreesingerArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, TreesingerPlantBondSelection),
                    Helpers.LevelEntry(6, TreesingerWildShapeMandragoraFeature),
                    Helpers.LevelEntry(9, TreesingerWildShapeShamblingMoundFeature, TreesingerWildShapeQuickwoodFeature),
                    Helpers.LevelEntry(12, TreesingerWildShapeGiantFlytrapFeature, TreesingerWildShapeTreantFeature)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Treesinger")) { return; }
            DruidClass.m_Archetypes = DruidClass.m_Archetypes.AppendToArray(TreesingerArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
