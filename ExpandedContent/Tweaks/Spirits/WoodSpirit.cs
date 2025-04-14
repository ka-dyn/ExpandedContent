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
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;

namespace ExpandedContent.Tweaks.Spirits {
    internal class WoodSpirit {
        public static void AddWoodSprit() {

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

            var Slam1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("5ea80d97dcfc81f46a1b9b2f256340f2");
            var SlamReachEnchantment = Helpers.CreateBlueprint<BlueprintWeaponEnchantment>("SlamReachEnchantment", bp => {
                bp.SetName("Reach");
                bp.SetDescription("The reach of these slam attacks is increased by 5 feet.");
                bp.AddComponent<AddStatBonusEquipment>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Reach;
                    c.Value = 5;
                });
                bp.SetPrefix("Reach");
                bp.SetSuffix("");
            });
            var Slam1d8WithReach = Helpers.CreateBlueprint<BlueprintItemWeapon>("Slam1d8WithReach", bp => {
                bp.m_DisplayNameText = Slam1d8.m_DisplayNameText;
                bp.m_DescriptionText = Slam1d8.m_DescriptionText;
                bp.m_FlavorText = Slam1d8.m_FlavorText;
                bp.m_NonIdentifiedNameText = Slam1d8.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Slam1d8.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Slam1d8.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Slam1d8.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Slam1d8.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Slam1d8.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Slam1d8.m_VisualParameters;
                bp.m_Type = Slam1d8.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { SlamReachEnchantment.ToReference<BlueprintWeaponEnchantmentReference>() };
                bp.m_OverrideDamageDice = true;
                bp.m_DamageDice = Slam1d8.m_DamageDice;
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Slam1d8.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });

            var ShamanWoodSpiritBaseBuff = Helpers.CreateBuff("ShamanWoodSpiritBaseBuff", bp => {
                bp.SetName("Tree Limb");
                bp.SetDescription("As a swift action, the shaman can turn her arms into heavy, branch-like limbs. " +
                    "Until the beginning of her next turn her unarmed strikes are replaced by slam attacks, these slam attacks deal 1d8 points of damage " +
                    "(for a Medium shaman; 1d6 if Small, 2d6 if Large). A shaman can use this ability a number of times per day equal to 3 + her Charisma modifier." +
                    "\nAt 8th level, the reach of these slam attacks increases by 5 feet.");
                bp.m_Icon = WoodenFistIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = Slam1d8.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
            });
            var ShamanWoodSpiritBaseReachBuff = Helpers.CreateBuff("ShamanWoodSpiritBaseReachBuff", bp => {
                bp.SetName("Tree Limb");
                bp.SetDescription("As a swift action, the shaman can turn her arms into heavy, branch-like limbs. " +
                    "Until the beginning of her next turn her unarmed strikes are replaced by slam attacks, these slam attacks deal 1d8 points of damage " +
                    "(for a Medium shaman; 1d6 if Small, 2d6 if Large). A shaman can use this ability a number of times per day equal to 3 + her Charisma modifier." +
                    "\nAt 8th level, the reach of these slam attacks increases by 5 feet.");
                bp.m_Icon = WoodenFistIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = Slam1d8WithReach.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
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
            var EntangledBuff = Resources.GetBlueprint<BlueprintBuff>("d1aea643c260c5e4ea66012876f2b7f5");

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

            var ShamanWoodSpiritGreaterTerrainBuff = Helpers.CreateBuff("ShamanWoodSpiritGreaterTerrainBuff", bp => {
                bp.SetName("ShamanWoodSpiritGreaterTerrainBuff");
                bp.SetDescription("Affected by difficult terrain");
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
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
                                    new ContextConditionIsCaster() { Not = false }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanWoodSpiritGreaterTerrainBuff.ToReference<BlueprintBuffReference>(),
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
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.Or,
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy() { Not = false }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
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
                                        ),
                                    IfFalse = Helpers.CreateActionList()
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
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanWoodSpiritGreaterTerrainBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.Round = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy() { Not = false }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
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
                                ),
                            IfFalse = Helpers.CreateActionList()
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
            var PlantShapeIIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeIII.jpg");
            var PlantShapeIIITreantBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIITreantBuff");
            var PlantShapeIIIGiantFlytrapBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIIGiantFlytrapBuff");
            var ShamanWoodSpiritTrueResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanWoodSpiritTrueResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Charisma
                };
            });

            var ShamanWoodSpiritTrueTreantAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanWoodSpiritTrueTreantAbility", bp => {
                bp.SetName("Tree Form (Treant)");
                bp.SetDescription("You become a huge treant. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun ability.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanWoodSpiritTrueResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIITreantBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIITreantBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
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
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("ShamanWoodSpiritTrueTreantAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ShamanWoodSpiritTrueGiantFlytrapAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanWoodSpiritTrueGiantFlytrapAbility", bp => {
                bp.SetName("Tree Form (Giant Flytrap)");
                bp.SetDescription("You become a huge giant flytrap. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain four 1d8 bite attacks, acid Resistance 20 and blindsight and poison ability.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanWoodSpiritTrueResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
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
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("ShamanWoodSpiritTrueGiantFlytrapAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ShamanWoodSpiritTrueAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanWoodSpiritTrueAbility", bp => {
                bp.SetName("Tree Form");
                bp.SetDescription("You become a Huge Treant or a Huge Giant Flytrap. \nTreant: You become a huge treant. You gain a +8 size bonus to your Strength, " +
                    "+4 to Constitution, -2 penalty to Dexterity and a +6 natural armor bonus. You also gain two 2d6 slam attacks, damage reduction 10/slashing, " +
                    "vulnerability to fire and overrun ability. Giant Flytrap: You become a huge giant flytrap. You gain a +8 size bonus to your Strength, +4 to " +
                    "Constitution, -2 penalty to Dexterity and a +6 natural armor bonus. You also gain four 1d8 bite attacks, acid Resistance 20 and blindsight " +
                    "and poison ability.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        ShamanWoodSpiritTrueTreantAbility.ToReference<BlueprintAbilityReference>(),
                        ShamanWoodSpiritTrueGiantFlytrapAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanWoodSpiritTrueResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
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
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("ShamanWoodSpiritTrueAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            ShamanWoodSpiritTrueTreantAbility.m_Parent = ShamanWoodSpiritTrueAbility.ToReference<BlueprintAbilityReference>();
            ShamanWoodSpiritTrueGiantFlytrapAbility.m_Parent = ShamanWoodSpiritTrueAbility.ToReference<BlueprintAbilityReference>();

            var ShamanWoodSpiritTrueFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanWoodSpiritTrueFeature", bp => {
                bp.SetName("Tree Form");
                bp.SetDescription("As a standard action, the shaman can assume the form of a plant creature as per plant shape III with a duration of 1 hour per level. " +
                    "She can use this ability once per day. \nYou become a Huge Treant or a Huge Giant Flytrap. \\nTreant: You become a huge treant. You gain a +8 size bonus to your Strength, " +
                    "+4 to Constitution, -2 penalty to Dexterity and a +6 natural armor bonus. You also gain two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun " +
                    "ability. Giant Flytrap: You become a huge giant flytrap. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 natural armor bonus. " +
                    "You also gain four 1d8 bite attacks, acid Resistance 20 and blindsight and poison ability.");
                bp.m_Icon = PlantShapeIIIIcon;
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
            var ImmunityToParalysis = Resources.GetBlueprint<BlueprintFeature>("4b152a7bc5bab5042b437b955fea46cd");
            var ImmunityToPoison = Resources.GetBlueprint<BlueprintFeature>("7e3f3228be49cce49bda37f7901bf246");
            var ImmunityToStunning = Resources.GetBlueprint<BlueprintFeature>("bd9df2d4a4cef274285b8827b6769bde");
            var ImmunityToSleep = Resources.GetBlueprint<BlueprintFeature>("c263f44f72df009489409af122b5eefc");
            var BalefulPolymorphBuff = Resources.GetBlueprint<BlueprintBuff>("0a52d8761bfd125429842103aed48b90");

            var ShamanWoodSpiritManifestationFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanWoodSpiritManifestationFeature", bp => {
                bp.SetName("Manifestation");
                bp.SetDescription("Upon reaching 20th level, the shaman becomes a living creature of wood. " +
                    "She gains a +4 natural armor bonus to her Armor Class and damage reduction 10/— against wooden weapons. " +
                    "She gains immunity to paralysis, poison, sleep, and stun, along with enemy polymorph spells.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ImmunityToParalysis.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToPoison.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToSleep.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToStunning.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = BalefulPolymorphBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 4;
                });
                bp.AddComponent<DamageReductionAgainstWeaponCategory>(c => {
                    c.Reduction = 10;
                    c.Categories = new WeaponCategory[] {
                        WeaponCategory.Greatclub,
                        WeaponCategory.Club,
                        WeaponCategory.Javelin,
                        WeaponCategory.Kama,
                        WeaponCategory.Longbow,
                        WeaponCategory.Longspear,
                        WeaponCategory.Nunchaku,
                        WeaponCategory.Quarterstaff,
                        WeaponCategory.Shortbow,
                        WeaponCategory.Shortspear,
                        WeaponCategory.SlingStaff,
                        WeaponCategory.Spear,
                        WeaponCategory.Trident,
                        WeaponCategory.LightCrossbow,
                        WeaponCategory.HeavyCrossbow
                    };
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
                bp.SetName("Wood");
                bp.SetDescription("A shaman who selects the wood spirit has a skin tone similar to the coloration of trees in her home region. " +
                    "Her vibrant hair is fragrant and resembles leaves and blossoms.");
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
                bp.SetName("Wood");
                bp.SetDescription("A shaman who selects the wood spirit has a skin tone similar to the coloration of trees in her home region. " +
                    "Her vibrant hair is fragrant and resembles leaves and blossoms.");
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
                bp.SetName("Wood");
                bp.SetDescription("A shaman who selects the wood spirit has a skin tone similar to the coloration of trees in her home region. " +
                    "Her vibrant hair is fragrant and resembles leaves and blossoms.");
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
                bp.SetName("Wood");
                bp.SetDescription("A shaman who selects the wood spirit has a skin tone similar to the coloration of trees in her home region. " +
                    "Her vibrant hair is fragrant and resembles leaves and blossoms.");
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
            #region Hex of Lignification 
            var LignificationIcon = AssetLoader.LoadInternal("Skills", "Icon_Lignification.jpg");
            var ShamanHexHexOfLignificationCooldown = Helpers.CreateBuff("ShamanHexHexOfLignificationCooldown", bp => {
                bp.SetName("Already targeted by this hex today");
                bp.SetDescription("");
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });


            var ShamanHexHexOfLignificationBuff = Helpers.CreateBuff("ShamanHexHexOfLignificationBuff", bp => {
                bp.SetName("Hex of Lignification");
                bp.SetDescription("The shaman causes a creature within 30 feet to turn into a twisted, treelike shape for 2 rounds. " +
                    "The target gains hardness 5 but is staggered, and can negate the effect with a successful Fortitude saving throw. " +
                    "Whether or not the target succeeds at its save, it can’t be the target of this hex again for 24 hours.");
                bp.m_Icon = LignificationIcon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Staggered;
                });
                bp.AddComponent<AddDamageResistanceHardness>(c => {
                    c.Value = 5;
                    c.UsePool = false;
                    c.Pool = new ContextValue();
                    c.m_ExcludedTypes = 0;
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = new PrefabLink() { AssetId = "f684f2a037e944f4a894037c86e4194b" };
            });

            var ShamanHexHexOfLignificationAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexHexOfLignificationAbility", bp => {
                bp.SetName("Hex of Lignification");
                bp.SetDescription("The shaman causes a creature within 30 feet to turn into a twisted, treelike shape for 2 rounds. " +
                    "The target gains hardness 5 but is staggered, and can negate the effect with a successful Fortitude saving throw. " +
                    "Whether or not the target succeeds at its save, it can’t be the target of this hex again for 24 hours.");
                bp.m_Icon = LignificationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = ShamanHexHexOfLignificationBuff.ToReference<BlueprintBuffReference>(),
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    ValueRank = AbilityRankType.Default
                                                },
                                                m_IsExtendable = true
                                            },
                                            DurationSeconds = 0,
                                            IsFromSpell = false,
                                            ToCaster = false,
                                            AsChild = true,
                                        }
                                    )
                                }
                                )
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexHexOfLignificationCooldown.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Days,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ShamanHexHexOfLignificationCooldown.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                    c.FromCaster = true;
                });
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
                bp.LocalizedDuration = Helpers.CreateString("ShamanHexHexOfLignificationAbility.Duration", "2 rounds");
                bp.LocalizedSavingThrow = Helpers.CreateString("ShamanHexHexOfLignificationAbility.SavingThrow", "Fortitude negates");
            });
            var ShamanHexHexOfLignificationFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexHexOfLignificationFeature", bp => {
                bp.SetName("Hex of Lignification");
                bp.SetDescription("The shaman causes a creature within 30 feet to turn into a twisted, treelike shape for 2 rounds. " +
                    "The target gains hardness 5 but is staggered, and can negate the effect with a successful Fortitude saving throw. " +
                    "Whether or not the target succeeds at its save, it can’t be the target of this hex again for 24 hours.");
                bp.m_Icon = LignificationIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexHexOfLignificationAbility.ToReference<BlueprintUnitFactReference>()
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
            SpiritTools.RegisterShamanHex(ShamanHexHexOfLignificationFeature);
            #endregion
            #region Verdant Path
            var DruidWoodlandStrideIcon = Resources.GetBlueprint<BlueprintFeature>("4c1419ef6cfc430a9071405788da4a73").Icon;
            
            var ShamanHexVerdantPathFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexVerdantPathFeature", bp => {
                bp.SetName("Verdant Path");
                bp.SetDescription("Even the most tangled briars make way for the shaman, and suitable roots and branches appear to support her feet. " +
                    "The shaman gains woodland stride, as per the druid ability of the same name. " +
                    "\nYou can move through any sort difficult terrain at your normal {g|Encyclopedia:Speed}speed{/g} and without " +
                    "taking {g|Encyclopedia:Damage}damage{/g} or suffering any other impairment.");
                bp.m_Icon = DruidWoodlandStrideIcon;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
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
            SpiritTools.RegisterShamanHex(ShamanHexVerdantPathFeature);
            #endregion


            ShamanWoodSpiritProgression.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ShamanHexHexOfLignificationFeature.ToReference<BlueprintFeatureReference>(),
                //ShamanHexNaturesGiftsFeature.ToReference<BlueprintFeatureReference>(),
                ShamanHexVerdantPathFeature.ToReference<BlueprintFeatureReference>()
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
