using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Utilities;
using Kingmaker.Craft;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using System.Collections.Generic;
using Kingmaker.Formations.Facts;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Blueprints.Items;
using static TabletopTweaks.Core.MechanicsChanges.AdditionalModifierDescriptors;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using UnityEngine;

namespace ExpandedContent.Tweaks.Spirits {
    internal class WoodSpirit {
        public static void AddSlumsSprit() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var UnswornShamanArchetype = Resources.GetBlueprint<BlueprintArchetype>("556590a43467a27459ac1a80324c9f9f");
            var ShamanHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4223fe18c75d4d14787af196a04e14e7");
            var PossessedShamanSharedSkillSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("9d0477ebd71d43041b419c216b5d6cff");



            #region Spelllist
            var ShillelaghAbility = Resources.GetModBlueprint<BlueprintAbility>("ShillelaghAbility");
            var BarkskinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5b77d7cc65b8ab74688e74a37fc2f553");
            var ThornBodySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2daf9c5112f16d54ab3cd6904c705c59"); 
            var ThirstingEntangleSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b844a29343974aeab1d37d38db2f530e");
            var PlantShapeIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIAbility");
            var PlantShapeIIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIAbility");
            var ChangestaffSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("26be70c4664d07446bdfe83504c1d757");
            var PlantShapeIIIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIIAbility");
            var WoodenPhalanxAbility = Resources.GetModBlueprint<BlueprintAbility>("WoodenPhalanxAbility");

            var WoodSpiritSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("WoodSpiritSpellList", bp => {
                bp.IsMythic = false;
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShillelaghAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BarkskinSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ThornBodySpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ThirstingEntangleSpell
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PlantShapeIAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PlantShapeIIAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ChangestaffSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PlantShapeIIIAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            WoodenPhalanxAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });
            var WoodSpiritSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("WoodSpiritSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WoodSpiritSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = true;
                    c.HideInUI = false;
                    c.m_Feature = PossessedShamanSharedSkillSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Spirit Features
            #region Base
            var WoodenFistIcon = AssetLoader.LoadInternal("Skills", "Icon_WoodenFist.jpg");
            var ShamanWoodSpiritBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanWoodSpiritBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                };
            });





            var ShamanWoodSpiritBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanWoodSpiritBaseAbility", bp => {
                bp.SetName("Tree Limb");
                bp.SetDescription("As a swift action, the shaman can turn her arms into heavy, branch-like limbs. " +
                    "Until the beginning of her next turn her unarmed strikes are replaced by slam attacks, these slam attacks deal 1d8 points of damage " +
                    "(for a Medium shaman; 1d6 if Small, 2d6 if Large). A shaman can use this ability a number of times per day equal to 3 + her Charisma modifier." +
                    "\nAt 8th level, the reach of these slam attacks increases by 5 feet.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 8,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritBaseBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritBaseReachBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    }
                                })
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanWoodSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_Icon = WoodenFistIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanWoodSpiritBaseAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanWoodSpiritBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanWoodSpiritBaseFeature", bp => {
                bp.SetName("Tree Limb");
                bp.SetDescription("As a swift action, the shaman can turn her arms into heavy, branch-like limbs. " +
                    "Until the beginning of her next turn her unarmed strikes are replaced by slam attacks, these slam attacks deal 1d8 points of damage " +
                    "(for a Medium shaman; 1d6 if Small, 2d6 if Large). A shaman can use this ability a number of times per day equal to 3 + her Charisma modifier." +
                    "\nAt 8th level, the reach of these slam attacks increases by 5 feet.");
                bp.m_Icon = WoodenFistIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShamanWoodSpiritBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanWoodSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Greater  
            var ShamanWoodSpiritGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanWoodSpiritGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[] { },
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] { },
                    LevelIncrease = 1,
                    StartingLevel = 1,
                    LevelStep = 15,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
                bp.m_UseMax = true;
                bp.m_Max = 2;
            });
            
            

            var ShamanWoodSpiritGreaterBuff = Helpers.CreateBuff("ShamanWoodSpiritGreaterBuff", bp => {
                bp.SetName("Bloody Roots");
                bp.SetDescription("As a standard action, the shaman can cause a field of thick roots to burrow up from the ground, " +
                    "for a number of rounds per day equal to her Charisma modifier. All creatures other than the shaman treat the area as difficult terrain. " +
                    "\nEvery enemy within the area of the spell is the target of a combat maneuver check made to grapple each round at the beginning of your turn, " +
                    "as well as when entering the area. The CMB of this grapple attempt is equal to your shaman level plus 5, increasing to plus 10 when trying to escape " +
                    "the grapple. If a creature is grappled by this ability, or fails to escape the grapple, it takes 1d6+4 points of damage." +
                    "\nThe shaman can use this ability once per day, increasing to twice per day at 16th level.");
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.CantMove;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Entangled;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Direct,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.NegativeEnergy
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 4,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                            UseMinHPAfterDamage = true,
                            MinHPAfterDamage = 1,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionBreakFree() {
                            UseCMB = false,
                            UseCMD = false,
                            UseOverrideDC = true,
                            OverridenDC = new ContextValue() {
                                ValueType = ContextValueType.Shared,
                                ValueShared = AbilitySharedValue.Damage
                            },
                            Success = Helpers.CreateActionList(
                                new ContextActionRemoveSelf()
                                ),
                            Failure = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Direct,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.NegativeEnergy
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    EnergyDrainType = EnergyDrainType.Temporary,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 4,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    UseMinHPAfterDamage = true,
                                    MinHPAfterDamage = 1,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                }
                                ),
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = 10,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_Icon = EntangledBuff.Icon;
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });


            var ShamanWoodSpiritGreaterArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("ShamanWoodSpiritGreaterArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = ShamanWoodSpiritGreaterBuff.ToReference<BlueprintBuffReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionCombatManeuverExpanded() {
                                    Bonus = 5,
                                    HasBABReplacement = true,
                                    BABReplacementValue = new ContextValue() { ValueType = ContextValueType.Rank, ValueRank = AbilityRankType.Default },
                                    OnSuccess = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = ShamanWoodSpiritGreaterBuff.ToReference<BlueprintBuffReference>(),
                                            Permanent = true,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Minutes,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue(),
                                                BonusValue = new ContextValue(),
                                                m_IsExtendable = true
                                            },
                                            IsFromSpell = false,
                                            IsNotDispelable = true,
                                        }
                                        ),
                                    OnFailure = Helpers.CreateActionList()
                                }
                            )
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = ShamanWoodSpiritGreaterBuff.ToReference<BlueprintBuffReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = ShamanWoodSpiritGreaterBuff.ToReference<BlueprintBuffReference>()
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }
                        );
                    c.Round = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = ShamanWoodSpiritGreaterBuff.ToReference<BlueprintBuffReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionCombatManeuverExpanded() {
                                    Bonus = 5,
                                    HasBABReplacement = true,
                                    BABReplacementValue = new ContextValue() { ValueType = ContextValueType.Rank, ValueRank = AbilityRankType.Default },
                                    OnSuccess = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = ShamanWoodSpiritGreaterBuff.ToReference<BlueprintBuffReference>(),
                                            Permanent = true,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Minutes,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue(),
                                                BonusValue = new ContextValue(),
                                                m_IsExtendable = true
                                            },
                                            IsFromSpell = false,
                                            IsNotDispelable = true,
                                        }
                                        ),
                                    OnFailure = Helpers.CreateActionList()
                                }
                            )
                        }
                        );
                    c.UnitMove = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.m_Tags = AreaEffectTags.DestroyableInCutscene;
                bp.SpellResistance = false;
                bp.AffectEnemies = true;
                bp.AggroEnemies = true;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 20 };
                bp.Fx = new PrefabLink() { AssetId = "ec9e5e455d0493148a8ffaa5bf8c0f6a" }; //entangle
                bp.CanBeUsedInTacticalCombat = false;
                bp.m_TickRoundAfterSpawn = false;
            });
            ShamanWoodSpiritGreaterArea.Fx = ShamanWoodSpiritGreaterArea.Fx.CreateDynamicProxy(pfl => {
                //Main.Log($"Editing: {pfl}");
                pfl.name = "ShamanWoodSpiritGreaterArea_20feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/WaveAll_GrassLiana00").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/WaveAll_GrassLiana00_RotatableCopy").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/WaveAll_GrassLianaSingle00").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/WaveAll_GrassLianaSingle00_RotatableCopy").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/Wave00_Ground00").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/Wave01_Ground00").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/Wave02_Ground00").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/Wave00_Ground00_RotatableCopy").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/Wave01_Ground00_RotatableCopy").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("AnimationRoot/Wave02_Ground00_RotatableCopy").gameObject);
                pfl.transform.localScale = new(0.25f, 0.75f, 0.25f);
            });

            var ShamanWoodSpiritGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanWoodSpiritGreaterAbility", bp => {
                bp.SetName("Bloody Roots");
                bp.SetDescription("As a standard action, the shaman can cause a field of thick roots to burrow up from the ground, " +
                    "for a number of rounds per day equal to her Charisma modifier. All creatures other than the shaman treat the area as difficult terrain. " +
                    "\nEvery enemy within the area of the spell is the target of a combat maneuver check made to grapple each round at the beginning of your turn, " +
                    "as well as when entering the area. The CMB of this grapple attempt is equal to your shaman level plus 5, increasing to plus 10 when trying to escape " +
                    "the grapple. If a creature is grappled by this ability, or fails to escape the grapple, it takes 1d6+4 points of damage." +
                    "\nThe shaman can use this ability once per day, increasing to twice per day at 16th level.");
                bp.m_Icon = EntangledBuff.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = ShamanWoodSpiritGreaterArea.ToReference<BlueprintAbilityAreaEffectReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            OnUnit = false
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanWoodSpiritGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;

                });
                bp.AddComponent<AbilityAoERadius>(c => {
                    c.m_Radius = 20.Feet();
                    c.m_TargetType = TargetType.Any;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Stat = StatType.Charisma;
                });
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanWoodSpiritGreaterAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ShamanWoodSpiritGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanWoodSpiritGreaterFeature", bp => {
                bp.SetName("Bloody Roots");
                bp.SetDescription("As a standard action, the shaman can cause a field of thick roots to burrow up from the ground, " +
                    "for a number of rounds per day equal to her Charisma modifier. All creatures other than the shaman treat the area as difficult terrain. " +
                    "\nEvery enemy within the area of the spell is the target of a combat maneuver check made to grapple each round at the beginning of your turn, " +
                    "as well as when entering the area. The CMB of this grapple attempt is equal to your shaman level plus 5, increasing to plus 10 when trying to escape " +
                    "the grapple. If a creature is grappled by this ability, or fails to escape the grapple, it takes 1d6+4 points of damage." +
                    "\nThe shaman can use this ability once per day, increasing to twice per day at 16th level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShamanWoodSpiritGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanWoodSpiritGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region True
            var SneakAttackIcon = Resources.GetBlueprint<BlueprintFeature>("9b9eac6709e1c084cb18c3a366e0ec87").Icon;
            var ShamanWoodSpiritTrueResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanWoodSpiritTrueResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                };
            });
            var ShamanWoodSpiritTrueBuff = Helpers.CreateBuff("ShamanWoodSpiritTrueBuff", bp => {
                bp.SetName("Paragon of the City");
                bp.SetDescription("As a standard action, the shaman assumes a spirit-infused paragon form that makes her a lethal stalker of the alleys and shadows. " +
                    "She gains the ability to make sneak attacks as a rogue of her shaman level for 1 minute. (If she already has sneak attack dice, these stack.) " +
                    "She can use this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.m_Icon = SneakAttackIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SneakAttack;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanWoodSpiritTrueAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanWoodSpiritTrueAbility", bp => {
                bp.SetName("Paragon of the City");
                bp.SetDescription("As a standard action, the shaman assumes a spirit-infused paragon form that makes her a lethal stalker of the alleys and shadows. " +
                    "She gains the ability to make sneak attacks as a rogue of her shaman level for 1 minute. (If she already has sneak attack dice, these stack.) " +
                    "She can use this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.m_Icon = SneakAttackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanWoodSpiritTrueBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0,
                            IsNotDispelable = true
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Reach | Metamagic.Selective;
                bp.LocalizedDuration = Helpers.CreateString("ShamanWoodSpiritTrueAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanWoodSpiritTrueFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanWoodSpiritTrueFeature", bp => {
                bp.SetName("Paragon of the City");
                bp.SetDescription("As a standard action, the shaman assumes a spirit-infused paragon form that makes her a lethal stalker of the alleys and shadows. " +
                    "She gains the ability to make sneak attacks as a rogue of her shaman level for 1 minute. (If she already has sneak attack dice, these stack.) " +
                    "She can use this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.m_Icon = SneakAttackIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanWoodSpiritTrueAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanWoodSpiritTrueResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Manifestation 
            var Corrupter = Resources.GetBlueprint<BlueprintFeature>("55c364c3f02e4fdc8a63125b5a4c256c");
            var ShamanWoodSpiritManifestationFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanWoodSpiritManifestationFeature", bp => {
                bp.SetName("Manifestation");
                bp.SetDescription("Upon reaching 20th level, the shaman becomes a spirit of the slums. She is immune to all diseases and poisons. " +
                    "When in an urban environment, she gains a +4 insight bonus to her AC and on Reflex saves.");
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Poison | SpellDescriptor.Disease;
                    c.m_CasterIgnoreImmunityFact = Corrupter.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Poison | SpellDescriptor.Disease;
                    c.m_IgnoreFeature = Corrupter.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Urban;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.AC;
                    c.Value = 4;
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Urban;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 4;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #endregion
            #region Progression
            var ShamanWoodSpiritProgression = Helpers.CreateBlueprint<BlueprintProgression>("ShamanWoodSpiritProgression", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFeaturesFromSelectionToDescription>(c => {
                    c.SetIntroduction("Additional Hexes:");
                    c.m_FeatureSelection = ShamanHexSelection.ToReference<BlueprintFeatureSelectionReference>();
                    c.OnlyIfRequiresThisFeature = true;
                });
                bp.AddComponent<AddSpellsToDescription>(c => {
                    c.SetIntroduction("Bonus Spells:");
                    c.m_SpellLists = new BlueprintSpellListReference[] { WoodSpiritSpellList.ToReference<BlueprintSpellListReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanSpirit };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ShamanClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] { };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ShamanWoodSpiritBaseFeature, WoodSpiritSpellListFeature),
                    Helpers.LevelEntry(8, ShamanWoodSpiritGreaterFeature),
                    Helpers.LevelEntry(16, ShamanWoodSpiritTrueFeature),
                    Helpers.LevelEntry(20, ShamanWoodSpiritManifestationFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            #endregion



            #region Wandering Spirit
            var ShamanWoodSpiritWanderingTrueBuff = Helpers.CreateBuff("ShamanWoodSpiritWanderingTrueBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanWoodSpiritTrueFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanWoodSpiritWanderingGreaterBuff = Helpers.CreateBuff("ShamanWoodSpiritWanderingGreaterBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanWoodSpiritGreaterFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanWoodSpiritWanderingBaseBuff = Helpers.CreateBuff("ShamanWoodSpiritWanderingBaseBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanWoodSpiritBaseFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanWoodSpiritWanderingFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanWoodSpiritWanderingFeature", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 20,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 12,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanWoodSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WoodSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Unsworn Wandering Spirit
            var UnswornShamanWoodSpiritWanderingFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("UnswornShamanWoodSpiritWanderingFeature1", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 18,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 10,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanWoodSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WoodSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var UnswornShamanWoodSpiritWanderingFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("UnswornShamanWoodSpiritWanderingFeature2", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 20,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 14,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanWoodSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WoodSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion

            #region Hexes
            #region Accident 
            var TouchOfGracelessnessIcon = Resources.GetBlueprint<BlueprintAbility>("5d38c80a819e8084ba19b29a865312c2").Icon;
            var ShamanHexAccidentAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexAccidentAbility", bp => {
                bp.SetName("Accident");
                bp.SetDescription("The shaman causes a target within 30 feet to stumble and fall. The shaman attempts a trip {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} " +
                    "using her {g|Encyclopedia:Caster_Level}caster level{/g} as its {g|Encyclopedia:BAB}base attack bonus{/g} against the target’s CMD. On a successful trip attempt, " +
                    "the target falls prone and takes 1d6 points of damage.");
                bp.m_Icon = TouchOfGracelessnessIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Trip,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Bludgeoning,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = Kingmaker.Enums.Damage.DamageEnergyType.Acid,
                                        Type = DamageType.Physical
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = 1,
                                        BonusValue = 0,
                                    },
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage,
                                    Half = false
                                }
                                ),
                            ReplaceStat = true,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = true,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Wisdom;
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                });
                //bp.AddComponent<ContextCalculateAbilityParams>(c => {
                //    c.UseKineticistMainStat = false;
                //    c.StatType = StatType.Wisdom;
                //    c.StatTypeFromCustomProperty = false;
                //    c.m_CustomProperty = null;
                //    c.ReplaceCasterLevel = true;
                //    c.CasterLevel = new ContextValue() {
                //        ValueType = ContextValueType.Rank,
                //        ValueRank = AbilityRankType.Default
                //    };
                //    c.ReplaceSpellLevel = false;
                //    c.SpellLevel = new ContextValue();
                //});
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHexAccidentFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexAccidentFeature", bp => {
                bp.SetName("Accident");
                bp.SetDescription("The shaman causes a target within 30 feet to stumble and fall. The shaman attempts a trip {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} " +
                    "using her {g|Encyclopedia:Caster_Level}caster level{/g} as its {g|Encyclopedia:BAB}base attack bonus{/g} against the target’s CMD. On a successful trip attempt, " +
                    "the target falls prone and takes 1d6 points of damage.");
                bp.m_Icon = TouchOfGracelessnessIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexAccidentAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexAccidentFeature);
            #endregion
            #region Bad Penny - Need to code getting the coin back in the future
            var BadPennyIcon = AssetLoader.LoadInternal("Skills", "Icon_BadPenny.png");
            var GoldCoins = Resources.GetBlueprint<BlueprintItem>("f2bc0997c24e573448c6c91d2be88afa");
            var TouchItem = Resources.GetBlueprintReference<BlueprintItemWeaponReference>("bb337517547de1a4189518d404ec49d4");
            var ShamanHexBadPennyBuff = Helpers.CreateBuff("ShamanHexBadPennyBuff", bp => {
                bp.SetName("Bad Penny");
                bp.SetDescription("As a standard action, the shaman can curse a coin, and place it in the pockets of a target, attempting to plant the coin is a melee touch attack. " +
                    "The bearer of the cursed coin takes a –2 penalty on all saving throws and skill checks as long he has the coin on his person. " +
                    "Once the coin leaves his person, the curse ends and the coin becomes a mundane piece of tender again. " +
                    "At 8th level, the penalty becomes –4. If the shaman curses a new coin, the previous curse ends. This is a curse effect.");
                bp.m_Icon = BadPennyIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveReflex;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveWill;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = -2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = -4 }
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Curse;
                });
                bp.AddComponent<UniqueBuff>();
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ShamanHexBadPennyAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexBadPennyAbility", bp => {
                bp.SetName("Bad Penny");
                bp.SetDescription("As a standard action, the shaman can curse a coin, and place it in the pockets of a target, attempting to plant the coin is a melee touch attack. " +
                    "The bearer of the cursed coin takes a –2 penalty on all saving throws and skill checks as long he has the coin on his person. " +
                    "Once the coin leaves his person, the curse ends and the coin becomes a mundane piece of tender again. " +
                    "At 8th level, the penalty becomes –4. If the shaman curses a new coin, the previous curse ends. This is a curse effect.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexBadPennyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Curse | SpellDescriptor.Hex;
                });
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 1,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.m_Icon = BadPennyIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHexBadPennyFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexBadPennyFeature", bp => {
                bp.SetName("Bad Penny");
                bp.SetDescription("As a standard action, the shaman can curse a coin, and place it in the pockets of a target, attempting to plant the coin is a melee touch attack. " +
                    "The bearer of the cursed coin takes a –2 penalty on all saving throws and skill checks as long he has the coin on his person. " +
                    "Once the coin leaves his person, the curse ends and the coin becomes a mundane piece of tender again. " +
                    "At 8th level, the penalty becomes –4. If the shaman curses a new coin, the previous curse ends. This is a curse effect.");
                bp.m_Icon = BadPennyIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexBadPennyAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWoodSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexBadPennyFeature);
            #endregion


            ShamanWoodSpiritProgression.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ShamanHexHexOfLignificationFeature.ToReference<BlueprintFeatureReference>(),
                ShamanHexNaturesGiftsFeature.ToReference<BlueprintFeatureReference>()
            };
            #endregion


            SpiritTools.RegisterSpirit(ShamanWoodSpiritProgression);
            SpiritTools.RegisterSecondSpirit(ShamanWoodSpiritProgression);
            SpiritTools.RegisterWanderingSpirit(ShamanWoodSpiritWanderingFeature);
            SpiritTools.RegisterUnswornSpirit1(UnswornShamanWoodSpiritWanderingFeature1);
            SpiritTools.RegisterUnswornSpirit2(UnswornShamanWoodSpiritWanderingFeature2);


        }
    }
}
