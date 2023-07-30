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
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UI.GenericSlot;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.Blueprints.Items.Weapons;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class DraconicDruid {
        public static void AddDraconicDruid() {


            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var DruidBondSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("3830f3630a33eba49b60f511b4c8f2a8");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var ResistNaturesLureFeature = Resources.GetBlueprint<BlueprintFeature>("ad6a5b0e1a65c3540986cf9a7b006388");
            var VenomImmunityFeature = Resources.GetBlueprint<BlueprintFeature>("5078622eb5cecaf4683fa16a9b948c2c");
            var PurityOfBodyFeature = Resources.GetBlueprint<BlueprintFeature>("9b02f77c96d6bba4daf9043eff876c76");
            var NaturalSpell = Resources.GetBlueprint<BlueprintFeature>("c806103e27cce6f429e5bf47067966cf");
            var MasterShapeshifter = Resources.GetBlueprint<BlueprintFeature>("934670ef88b281b4da5596db8b00df2f");
            var NatureSense = Resources.GetBlueprint<BlueprintFeature>("3a859e435fdd6d343b80d4970a7664c1");
            var WildShapeIWolfFeature = Resources.GetBlueprint<BlueprintFeature>("19bb148cb92db224abb431642d10efeb");
            var WildShapeIILeopardFeature = Resources.GetBlueprint<BlueprintFeature>("c4d651bc0d4eabd41b08ee81bfe701d8");
            var WildShapeElementalSmallFeature = Resources.GetBlueprint<BlueprintFeature>("bddd46a6f6a3e6e4b99008dcf5271c3b");
            var WildShapeIVBearFeature = Resources.GetBlueprint<BlueprintFeature>("1368c7ce69702444893af5ffd3226e19");
            var WildShapeElementalFeatureAddMedium = Resources.GetBlueprint<BlueprintFeature>("6e4b88e2a044c67469c038ac2f09d061");
            var WildShapeIIISmilodonFeature = Resources.GetBlueprint<BlueprintFeature>("253c0c0d00e50a24797445f20af52dc8");
            var WildShapeElementalFeatureAddLarge = Resources.GetBlueprint<BlueprintFeature>("e66154511a6f9fc49a9de644bd8922db");
            var WildShapeIVShamblingMoundFeature = Resources.GetBlueprint<BlueprintFeature>("0f31b23c2ab39354bbde4e33e8151495");
            var WildShapeElementalHugeFeature = Resources.GetBlueprint<BlueprintFeature>("fe58dd496a36e274b86958f4677071b2");
            var WildShapeElementalAirSmallAbility = Resources.GetBlueprint<BlueprintAbility>("2f38f491888c89140969a1dc7af8c66e");
            var EnergizedWildShapeDamageFeature = Resources.GetBlueprint<BlueprintFeature>("d808863c4bd44fd8bd9cf5892460705d");
            var FrightfulShapeFeature = Resources.GetBlueprint<BlueprintFeature>("8e8a34c754d649aa9286fe8ee5cc3f10");
            var FrightfulShapeAttackBuff = Resources.GetBlueprint<BlueprintBuff>("1a5a2ce6793a4458957f45517662bb0e");


            var DragonType = Resources.GetBlueprint<BlueprintFeature>("455ac88e22f55804ab87c2467deff1d6");
            var WildShapeResource = Resources.GetBlueprint<BlueprintAbilityResource>("ae6af4d58b70a754d868324d1a05eda4");



            var DraconicDruidArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DraconicDruidArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DraconicDruidArchetype.Name", "Draconic Druid");
                bp.LocalizedDescription = Helpers.CreateString($"DraconicDruidArchetype.Description", "Some druids believe that dragons are the ultimate expression of nature, combining elemental fury with majestic beauty. " +
                    "These druids consort with dragons and eventually transform into draconic forms.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DraconicDruidArchetype.Description", "Some druids believe that dragons are the ultimate expression of nature, combining elemental fury with majestic " +
                    "beauty. These druids consort with dragons and eventually transform into draconic forms.");                
            });
            var DragonSenseFeature = Helpers.CreateBlueprint<BlueprintFeature>("DragonSenseFeature", bp => {
                bp.SetName("Dragon Sense");
                bp.SetDescription("A draconic druid studies dragons and their history. She gains a + 2 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and {g|Encyclopedia:Knowledge_Arcana}Knowledge (Arcana){/g} checks.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillKnowledgeArcana;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 2;
                });
            });
            var ResistDragonsMightFeature = Helpers.CreateBlueprint<BlueprintFeature>("ResistDragonsMightFeature", bp => {
                bp.SetName("Resist Dragon's Might");
                bp.SetDescription("At 4th level, a draconic druid gains a +4 bonus on saving throws gainst the, spell-like, and supernatural abilities of creatures with the dragon subtype.");
                bp.AddComponent<SavingThrowBonusAgainstFact>(c => {
                    c.m_CheckedFact = DragonType.ToReference<BlueprintFeatureReference>();
                    c.Value = 4;
                });                
            });
            //Dragon Shape
            var Bite1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("a000716f88c969c499a535dadcf09286");
            var AcidMawAbility = Resources.GetBlueprint<BlueprintAbility>("75de4ded3e731dc4f84d978fe947dc67");
            var WildShapeDragonShapeBiteBuff = Helpers.CreateBuff("WildShapeDragonShapeBiteBuff", bp => {
                bp.SetName("Dragon Shape - Dragon Fangs");
                bp.SetDescription("You can use wild shape to change into a dragon-scaled version of yourself with long fangs, gaining a +1 natural armor bonus to your AC and a bite attack appropriate for your size " +
                    "(1d6 points of damage for a Medium druid) but otherwise retaining your usual form.");
                bp.m_Icon = AcidMawAbility.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.AC;
                    c.Value = 1;
                    c.Descriptor = ModifierDescriptor.NaturalArmorEnhancement;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<PolymorphBonuses>(c => {
                    c.m_Flags = 0;
                    c.masterShifterBonus = 4;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var WildShapeDragonShapeBiteAbility = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeBiteAbility", bp => {
                bp.SetName("Dragon Shape - Dragon Fangs");
                bp.SetDescription("You can use wild shape to change into a dragon-scaled version of yourself with long fangs, gaining a +1 natural armor bonus to your AC and a bite attack appropriate for your size " +
                    "(1d6 points of damage for a Medium druid) but otherwise retaining your usual form.");
                bp.m_Icon = AcidMawAbility.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeBiteBuff.ToReference<BlueprintBuffReference>(),
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { WildShapeDragonShapeBiteBuff.ToReference<BlueprintUnitFactReference>() };
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
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WildShapeDragonShapeBiteFeature= Helpers.CreateBlueprint<BlueprintFeature>("WildShapeDragonShapeBiteFeature", bp => {
                bp.SetName("Dragon Shape - Dragon Fangs");
                bp.SetDescription("You can use wild shape to change into a dragon-scaled version of yourself with long fangs, gaining a +1 natural armor bonus to your AC and a bite attack appropriate for your size " +
                    "(1d6 points of damage for a Medium druid) but otherwise retaining your usual form.");
                bp.m_Icon = AcidMawAbility.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WildShapeDragonShapeBiteAbility.ToReference<BlueprintUnitFactReference>()
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

            var FormOfTheDragonGreenBuff = Resources.GetBlueprint<BlueprintBuff>("02611a12f38bed340920d1d427865917");
            var WildShapeDragonShapeGreenBuff = Helpers.CreateBuff("WildShapeDragonShapeGreenBuff", bp => {
                bp.SetName("Dragon Shape - Green Dragon");
                bp.m_Description = FormOfTheDragonGreenBuff.m_Description;
                bp.Components = FormOfTheDragonGreenBuff.Components;
                bp.m_Icon = FormOfTheDragonGreenBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonGreenBuff.FxOnStart;
            });
            var WildShapeDragonShapeGreenBuffPolymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeGreenBuff").GetComponent<Polymorph>();
            WildShapeDragonShapeGreenBuffPolymorph.m_Facts = WildShapeDragonShapeGreenBuffPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeGreenBuffAddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeGreenBuff").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeGreenBuffAddFactContextActions.Activated.Actions = WildShapeDragonShapeGreenBuffAddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonSilverBuff = Resources.GetBlueprint<BlueprintBuff>("feb2ab7613e563e45bcf9f7ffe4e05c6");
            var WildShapeDragonShapeSilverBuff = Helpers.CreateBuff("WildShapeDragonShapeSilverBuff", bp => {
                bp.SetName("Dragon Shape - Silver Dragon");
                bp.m_Description = FormOfTheDragonSilverBuff.m_Description;
                bp.Components = FormOfTheDragonSilverBuff.Components;
                bp.m_Icon = FormOfTheDragonSilverBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonSilverBuff.FxOnStart;
            });
            var WildShapeDragonShapeSilverBuffPolymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeSilverBuff").GetComponent<Polymorph>();
            WildShapeDragonShapeSilverBuffPolymorph.m_Facts = WildShapeDragonShapeSilverBuffPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeSilverBuffAddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeSilverBuff").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeSilverBuffAddFactContextActions.Activated.Actions = WildShapeDragonShapeSilverBuffAddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonBlackBuff = Resources.GetBlueprint<BlueprintBuff>("268fafac0a5b78c42a58bd9c1ae78bcf");
            var WildShapeDragonShapeBlackBuff = Helpers.CreateBuff("WildShapeDragonShapeBlackBuff", bp => {
                bp.SetName("Dragon Shape - Black Dragon");
                bp.m_Description = FormOfTheDragonBlackBuff.m_Description;
                bp.Components = FormOfTheDragonBlackBuff.Components;
                bp.m_Icon = FormOfTheDragonBlackBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonBlackBuff.FxOnStart;
            });
            var WildShapeDragonShapeBlackBuffPolymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlackBuff").GetComponent<Polymorph>();
            WildShapeDragonShapeBlackBuffPolymorph.m_Facts = WildShapeDragonShapeBlackBuffPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeBlackBuffAddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlackBuff").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeBlackBuffAddFactContextActions.Activated.Actions = WildShapeDragonShapeBlackBuffAddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonBlueBuff = Resources.GetBlueprint<BlueprintBuff>("b117bc8b41735924dba3fb23318f39ff");
            var WildShapeDragonShapeBlueBuff = Helpers.CreateBuff("WildShapeDragonShapeBlueBuff", bp => {
                bp.SetName("Dragon Shape - Blue Dragon");
                bp.m_Description = FormOfTheDragonBlueBuff.m_Description;
                bp.Components = FormOfTheDragonBlueBuff.Components;
                bp.m_Icon = FormOfTheDragonBlueBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonBlueBuff.FxOnStart;
            });
            var WildShapeDragonShapeBlueBuffPolymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlueBuff").GetComponent<Polymorph>();
            WildShapeDragonShapeBlueBuffPolymorph.m_Facts = WildShapeDragonShapeBlueBuffPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeBlueBuffAddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlueBuff").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeBlueBuffAddFactContextActions.Activated.Actions = WildShapeDragonShapeBlueBuffAddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonBrassBuff = Resources.GetBlueprint<BlueprintBuff>("17d330af03f5b3042a4417ab1d45e484");
            var WildShapeDragonShapeBrassBuff = Helpers.CreateBuff("WildShapeDragonShapeBrassBuff", bp => {
                bp.SetName("Dragon Shape - Brass Dragon");
                bp.m_Description = FormOfTheDragonBrassBuff.m_Description;
                bp.Components = FormOfTheDragonBrassBuff.Components;
                bp.m_Icon = FormOfTheDragonBrassBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonBrassBuff.FxOnStart;
            });
            var WildShapeDragonShapeBrassBuffPolymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBrassBuff").GetComponent<Polymorph>();
            WildShapeDragonShapeBrassBuffPolymorph.m_Facts = WildShapeDragonShapeBrassBuffPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeBrassBuffAddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBrassBuff").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeBrassBuffAddFactContextActions.Activated.Actions = WildShapeDragonShapeBrassBuffAddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonRedBuff = Resources.GetBlueprint<BlueprintBuff>("294cbb3e1d547f341a5d7ec8500ffa44");
            var WildShapeDragonShapeRedBuff = Helpers.CreateBuff("WildShapeDragonShapeRedBuff", bp => {
                bp.SetName("Dragon Shape - Red Dragon");
                bp.m_Description = FormOfTheDragonRedBuff.m_Description;
                bp.Components = FormOfTheDragonRedBuff.Components;
                bp.m_Icon = FormOfTheDragonRedBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonRedBuff.FxOnStart;
            });
            var WildShapeDragonShapeRedBuffPolymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeRedBuff").GetComponent<Polymorph>();
            WildShapeDragonShapeRedBuffPolymorph.m_Facts = WildShapeDragonShapeRedBuffPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeRedBuffAddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeRedBuff").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeRedBuffAddFactContextActions.Activated.Actions = WildShapeDragonShapeRedBuffAddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonWhiteBuff = Resources.GetBlueprint<BlueprintBuff>("a6acd3ad1e9fa6c45998d43fd5dcd86d");
            var WildShapeDragonShapeWhiteBuff = Helpers.CreateBuff("WildShapeDragonShapeWhiteBuff", bp => {
                bp.SetName("Dragon Shape - White Dragon");
                bp.m_Description = FormOfTheDragonWhiteBuff.m_Description;
                bp.Components = FormOfTheDragonWhiteBuff.Components;
                bp.m_Icon = FormOfTheDragonWhiteBuff.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonWhiteBuff.FxOnStart;
            });
            var WildShapeDragonShapeWhiteBuffPolymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeWhiteBuff").GetComponent<Polymorph>();
            WildShapeDragonShapeWhiteBuffPolymorph.m_Facts = WildShapeDragonShapeWhiteBuffPolymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeWhiteBuffAddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeWhiteBuff").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeWhiteBuffAddFactContextActions.Activated.Actions = WildShapeDragonShapeWhiteBuffAddFactContextActions.Activated.Actions.AppendToArray(
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

            var WildShapeDragonShapeGreen = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeGreen", bp => {
                bp.SetName("Dragon Shape - Green Dragon");
                bp.SetDescription("You become a medium green dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeGreenBuff.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonGreenBuff.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeSilver = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeSilver", bp => {
                bp.SetName("Dragon Shape - Silver Dragon");
                bp.SetDescription("You become a medium black dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a cold breath weapon, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeSilverBuff.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonSilverBuff.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeBlack = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeBlack", bp => {
                bp.SetName("Dragon Shape - Black Dragon");
                bp.SetDescription("You become a medium black dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeBlackBuff.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlackBuff.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeBlue = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeBlue", bp => {
                bp.SetName("Dragon Shape - Blue Dragon");
                bp.SetDescription("You become a medium blue dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a electricity breath weapon, and resistance to electricity 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeBlueBuff.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlueBuff.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeBrass = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeBrass", bp => {
                bp.SetName("Dragon Shape - Brass Dragon");
                bp.SetDescription("You become a medium brass dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeBrassBuff.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBrassBuff.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeRed = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeRed", bp => {
                bp.SetName("Dragon Shape - Red Dragon");
                bp.SetDescription("You become a medium red dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeRedBuff.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonRedBuff.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeWhite = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeWhite", bp => {
                bp.SetName("Dragon Shape - White Dragon");
                bp.SetDescription("You become a medium white dragon. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a cold breath weapon, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), " +
                    "two claws (1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeWhiteBuff.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonWhiteBuff.ToReference<BlueprintUnitFactReference>() };
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

            var WildShapeDragonShapeFeature = Helpers.CreateBlueprint<BlueprintFeature>("WildShapeDragonShapeFeature", bp => {
                bp.SetName("Wild Shape (Dragon)");
                bp.SetDescription("You become a medium dragon-like creature. You gain a +4 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +2 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, a breath " +
                    "weapon, and resistance to one element. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws " +
                    "(1d6), and two wing {g|Encyclopedia:Attack}attacks{/g} (1d4). Your breath weapon and resistance depend on the type of your dragon prototype. You can only use the breath weapon " +
                    "once per casting of this {g|Encyclopedia:Spell}spell{/g}. All breath weapons deal 6d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex " +
                    "save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeIWolfFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WildShapeDragonShapeGreen.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeSilver.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeBlack.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeBlue.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeBrass.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeRed.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeWhite.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var FormOfTheDragonGreenBuff2 = Resources.GetBlueprint<BlueprintBuff>("070543328d3e9af49bb514641c56911d");
            var WildShapeDragonShapeGreenBuff2 = Helpers.CreateBuff("WildShapeDragonShapeGreenBuff2", bp => {
                bp.SetName("Dragon Shape - Large Green Dragon");
                bp.m_Description = FormOfTheDragonGreenBuff2.m_Description;
                bp.Components = FormOfTheDragonGreenBuff2.Components;
                bp.m_Icon = FormOfTheDragonGreenBuff2.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonGreenBuff2.FxOnStart;
            });
            var WildShapeDragonShapeGreenBuff2Polymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeGreenBuff2").GetComponent<Polymorph>();
            WildShapeDragonShapeGreenBuff2Polymorph.m_Facts = WildShapeDragonShapeGreenBuff2Polymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeGreenBuff2AddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeGreenBuff2").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeGreenBuff2AddFactContextActions.Activated.Actions = WildShapeDragonShapeGreenBuff2AddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonSilverBuff2 = Resources.GetBlueprint<BlueprintBuff>("16857109dafc2b94eafd1e888552ef76");
            var WildShapeDragonShapeSilverBuff2 = Helpers.CreateBuff("WildShapeDragonShapeSilverBuff2", bp => {
                bp.SetName("Dragon Shape - Large Silver Dragon");
                bp.m_Description = FormOfTheDragonSilverBuff2.m_Description;
                bp.Components = FormOfTheDragonSilverBuff2.Components;
                bp.m_Icon = FormOfTheDragonSilverBuff2.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonSilverBuff2.FxOnStart;
            });
            var WildShapeDragonShapeSilverBuff2Polymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeSilverBuff2").GetComponent<Polymorph>();
            WildShapeDragonShapeSilverBuff2Polymorph.m_Facts = WildShapeDragonShapeSilverBuff2Polymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeSilverBuff2AddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeSilverBuff2").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeSilverBuff2AddFactContextActions.Activated.Actions = WildShapeDragonShapeSilverBuff2AddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonBlackBuff2 = Resources.GetBlueprint<BlueprintBuff>("9eb5ba8c396d2c74c8bfabd3f5e91050");
            var WildShapeDragonShapeBlackBuff2 = Helpers.CreateBuff("WildShapeDragonShapeBlackBuff2", bp => {
                bp.SetName("Dragon Shape - Large Black Dragon");
                bp.m_Description = FormOfTheDragonBlackBuff2.m_Description;
                bp.Components = FormOfTheDragonBlackBuff2.Components;
                bp.m_Icon = FormOfTheDragonBlackBuff2.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonBlackBuff2.FxOnStart;
            });
            var WildShapeDragonShapeBlackBuff2Polymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlackBuff2").GetComponent<Polymorph>();
            WildShapeDragonShapeBlackBuff2Polymorph.m_Facts = WildShapeDragonShapeBlackBuff2Polymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeBlackBuff2AddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlackBuff2").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeBlackBuff2AddFactContextActions.Activated.Actions = WildShapeDragonShapeBlackBuff2AddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonBlueBuff2 = Resources.GetBlueprint<BlueprintBuff>("cf8b4e861226e0545a6805036ab2a21b");
            var WildShapeDragonShapeBlueBuff2 = Helpers.CreateBuff("WildShapeDragonShapeBlueBuff2", bp => {
                bp.SetName("Dragon Shape - Large Blue Dragon");
                bp.m_Description = FormOfTheDragonBlueBuff2.m_Description;
                bp.Components = FormOfTheDragonBlueBuff2.Components;
                bp.m_Icon = FormOfTheDragonBlueBuff2.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonBlueBuff2.FxOnStart;
            });
            var WildShapeDragonShapeBlueBuff2Polymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlueBuff2").GetComponent<Polymorph>();
            WildShapeDragonShapeBlueBuff2Polymorph.m_Facts = WildShapeDragonShapeBlueBuff2Polymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeBlueBuff2AddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlueBuff2").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeBlueBuff2AddFactContextActions.Activated.Actions = WildShapeDragonShapeBlueBuff2AddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonBrassBuff2 = Resources.GetBlueprint<BlueprintBuff>("f7fdc15aa0219104a8b38c9891cac17b");
            var WildShapeDragonShapeBrassBuff2 = Helpers.CreateBuff("WildShapeDragonShapeBrassBuff2", bp => {
                bp.SetName("Dragon Shape - Large Brass Dragon");
                bp.m_Description = FormOfTheDragonBrassBuff2.m_Description;
                bp.Components = FormOfTheDragonBrassBuff2.Components;
                bp.m_Icon = FormOfTheDragonBrassBuff2.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonBrassBuff2.FxOnStart;
            });
            var WildShapeDragonShapeBrassBuff2Polymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBrassBuff2").GetComponent<Polymorph>();
            WildShapeDragonShapeBrassBuff2Polymorph.m_Facts = WildShapeDragonShapeBrassBuff2Polymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeBrassBuff2AddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBrassBuff2").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeBrassBuff2AddFactContextActions.Activated.Actions = WildShapeDragonShapeBrassBuff2AddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonRedBuff2 = Resources.GetBlueprint<BlueprintBuff>("40a96969339f3c241b4d989910f255e1");
            var WildShapeDragonShapeRedBuff2 = Helpers.CreateBuff("WildShapeDragonShapeRedBuff2", bp => {
                bp.SetName("Dragon Shape - Large Red Dragon");
                bp.m_Description = FormOfTheDragonRedBuff2.m_Description;
                bp.Components = FormOfTheDragonRedBuff2.Components;
                bp.m_Icon = FormOfTheDragonRedBuff2.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonRedBuff2.FxOnStart;
            });
            var WildShapeDragonShapeRedBuff2Polymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeRedBuff2").GetComponent<Polymorph>();
            WildShapeDragonShapeRedBuff2Polymorph.m_Facts = WildShapeDragonShapeRedBuff2Polymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeRedBuff2AddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeRedBuff2").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeRedBuff2AddFactContextActions.Activated.Actions = WildShapeDragonShapeRedBuff2AddFactContextActions.Activated.Actions.AppendToArray(
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
            var FormOfTheDragonWhiteBuff2 = Resources.GetBlueprint<BlueprintBuff>("2652c61dff50a24479520c84005ede8b");
            var WildShapeDragonShapeWhiteBuff2 = Helpers.CreateBuff("WildShapeDragonShapeWhiteBuff2", bp => {
                bp.SetName("Dragon Shape - Large White Dragon");
                bp.m_Description = FormOfTheDragonWhiteBuff2.m_Description;
                bp.Components = FormOfTheDragonWhiteBuff2.Components;
                bp.m_Icon = FormOfTheDragonWhiteBuff2.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragonWhiteBuff2.FxOnStart;
            });
            var WildShapeDragonShapeWhiteBuff2Polymorph = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeWhiteBuff2").GetComponent<Polymorph>();
            WildShapeDragonShapeWhiteBuff2Polymorph.m_Facts = WildShapeDragonShapeWhiteBuff2Polymorph.m_Facts.AppendToArray(EnergizedWildShapeDamageFeature.ToReference<BlueprintUnitFactReference>());
            var WildShapeDragonShapeWhiteBuff2AddFactContextActions = Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeWhiteBuff2").GetComponent<AddFactContextActions>();
            WildShapeDragonShapeWhiteBuff2AddFactContextActions.Activated.Actions = WildShapeDragonShapeWhiteBuff2AddFactContextActions.Activated.Actions.AppendToArray(
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
            var WildShapeDragonShapeGreen2 = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeGreen2", bp => {
                bp.SetName("Dragon Shape - Large Green Dragon");
                bp.SetDescription("You become a large green dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, damage reduction 5/magic, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeGreenBuff2.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonGreenBuff2.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeSilver2 = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeSilver2", bp => {
                bp.SetName("Dragon Shape - Large Silver Dragon");
                bp.SetDescription("You become a large silver dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a cold breath weapon, damage reduction 5/magic, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeSilverBuff2.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonSilverBuff2.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeBlack2 = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeBlack2", bp => {
                bp.SetName("Dragon Shape - Large Black Dragon");
                bp.SetDescription("You become a large black dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a acid breath weapon, damage reduction 5/magic, and resistance to acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeBlackBuff2.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlackBuff2.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeBlue2 = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeBlue2", bp => {
                bp.SetName("Dragon Shape - Large Blue Dragon");
                bp.SetDescription("You become a large blue dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a electricity breath weapon, damage reduction 5/magic, and resistance to electricity 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeBlueBuff2.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBlueBuff2.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeBrass2 = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeBrass2", bp => {
                bp.SetName("Dragon Shape - Large Brass Dragon");
                bp.SetDescription("You become a large brass dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, damage reduction 5/magic, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeBrassBuff2.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonBrassBuff2.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeRed2 = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeRed2", bp => {
                bp.SetName("Dragon Shape - Large Red Dragon");
                bp.SetDescription("You become a large red dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a fire breath weapon, damage reduction 5/magic, and resistance to fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeRedBuff2.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonRedBuff2.ToReference<BlueprintUnitFactReference>() };
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
            var WildShapeDragonShapeWhite2 = Helpers.CreateBlueprint<BlueprintAbility>("WildShapeDragonShapeWhite2", bp => {
                bp.SetName("Dragon Shape - Large White Dragon");
                bp.SetDescription("You become a large white dragon. You gain a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +4 size bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, " +
                    "a cold breath weapon, damage reduction 5/magic, and resistance to cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), " +
                    "two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap {g|Encyclopedia:Attack}attacks{/g} (1d8). You can only use the breath weapon once per casting of this {g|Encyclopedia:Spell}spell{/g}. " +
                    "All breath weapons deal 8d8 points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeElementalAirSmallAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WildShapeDragonShapeWhiteBuff2.ToReference<BlueprintBuffReference>(),
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
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>(), MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { FormOfTheDragonWhiteBuff2.ToReference<BlueprintUnitFactReference>() };
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

            var WildShapeDragonShapeFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("WildShapeDragonShapeFeature2", bp => {
                bp.SetName("Wild Shape (Large Dragon)");
                bp.SetDescription("This ability functions as Wild Shape (Dragon) except that it also allows you to assume the form of a large dragon-like creature. You gain the following " +
                    "abilities: a +6 {g|Encyclopedia:Size}size{/g} {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +4 size bonus to " +
                    "{g|Encyclopedia:Constitution}Constitution{/g}, a +6 form natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, a breath weapon, " +
                    "{g|Encyclopedia:Damage_Reduction}damage reduction{/g} 5/magic, and resistance to one element. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. " +
                    "You also gain one bite ({g|Encyclopedia:Dice}2d6{/g}), two claws (1d8), two wing {g|Encyclopedia:Attack}attacks{/g} (1d6), and one tail slap attack (1d8). You can " +
                    "only use the breath weapon twice per casting of this spell (when you use it, the duration of your breath weapon ability is reduced by 1/2 " +
                    "{g|Encyclopedia:Caster_Level}caster level{/g} minutes), and you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. All breath weapons deal 8d8 " +
                    "points of {g|Encyclopedia:Damage}damage{/g} and allow a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half damage. Line breath weapons increase to 80-foot lines " +
                    "and cones increase to 40-foot cones." +
                    "\nThis ability costs 2 uses of your Wild Shape resource.");
                bp.m_Icon = WildShapeIWolfFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WildShapeDragonShapeGreen2.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeSilver2.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeBlack2.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeBlue2.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeBrass2.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeRed2.ToReference<BlueprintUnitFactReference>(),
                        WildShapeDragonShapeWhite2.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WildShapeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;

                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // NaturalSpell patch
            NaturalSpell.AddComponent<PrerequisiteFeature>(c => {
                c.Group = Prerequisite.GroupType.Any;
                c.m_Feature = WildShapeDragonShapeBiteFeature.ToReference<BlueprintFeatureReference>();
            });
            DraconicDruidArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DruidBondSelection, NatureSense),
                    Helpers.LevelEntry(4, ResistNaturesLureFeature, WildShapeIWolfFeature),
                    Helpers.LevelEntry(6, WildShapeIILeopardFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(8, WildShapeIVBearFeature, WildShapeElementalFeatureAddMedium),
                    Helpers.LevelEntry(9, VenomImmunityFeature),
                    Helpers.LevelEntry(10, WildShapeIIISmilodonFeature, WildShapeElementalFeatureAddLarge, WildShapeIVShamblingMoundFeature),
                    Helpers.LevelEntry(12, WildShapeElementalHugeFeature)
            };
            DraconicDruidArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DrakeCompanionSelection, DragonSenseFeature),
                    Helpers.LevelEntry(4, ResistDragonsMightFeature, WildShapeDragonShapeBiteFeature),
                    Helpers.LevelEntry(10, WildShapeDragonShapeFeature),
                    Helpers.LevelEntry(12, WildShapeDragonShapeFeature2)

            };

            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = DraconicDruidArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Draconic Druid")) { return; }
            DruidClass.m_Archetypes = DruidClass.m_Archetypes.AppendToArray(DraconicDruidArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
