using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Craft;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using UnityEngine;
using ExpandedContent.Tweaks.Components;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Owlcat.Runtime.Visual.RenderPipeline.RendererFeatures.Fluid;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class SoldierOfGaia {
        public static void AddSoldierOfGaia() {

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestSpelllist = Resources.GetBlueprint<BlueprintSpellList>("c5a1b8df32914d74c9b44052ba3e686a");
            var WarpriestSpellLevels = Resources.GetBlueprint<BlueprintSpellsTable>("c73a394ec54adc243aef8ac967e39324");
            var WarpriestSpontaneousSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2d26a3364d65c4e4e9fb470172f638a9");
            var DruidSpontaneousSummonIcon = Resources.GetBlueprint<BlueprintFeature>("b296531ffe013c8499ad712f8ae97f6b").Icon;
            var SecondBlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b7ce4a67287cda746a59b31c042305cf");
            var SacredArmorFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("35e2d9525c240ce4c8ae47dd387b6e53");
            var SacredArmorEnchantPlus2 = Resources.GetBlueprintReference<BlueprintFeatureReference>("ec327c67f6a6b2f49a8ca218466a8818");
            var SacredArmorEnchantPlus3 = Resources.GetBlueprintReference<BlueprintFeatureReference>("bd292463fa74d664086f0a3e4e425c47");
            var SacredArmorEnchantPlus4 = Resources.GetBlueprintReference<BlueprintFeatureReference>("ee65ff63443ce42488515db6a43fee5e");
            var SacredArmorEnchantPlus5 = Resources.GetBlueprintReference<BlueprintFeatureReference>("1e560784dfcb00f4da1415bbad3226c3");

            var SoldierOfGaiaArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("SoldierOfGaiaArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"SoldierOfGaiaArchetype.Name", "Soldier of Gaia");
                bp.LocalizedDescription = Helpers.CreateString($"SoldierOfGaiaArchetype.Description", "Every war needs soldiers. Most people don’t know it, but a war rages " +
                    "every day between the forces of the natural world and those that would defile and destroy it. Whether these enemies be the obscene monstrosities of other " +
                    "planes or the foulness known as “civilization”, Golarion calls upon a few mortal emissaries to serve as her warriors in this fight.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"SoldierOfGaiaArchetype.Description", "Every war needs soldiers. Most people don’t know it, but a war " +
                    "rages every day between the forces of the natural world and those that would defile and destroy it. Whether these enemies be the obscene monstrosities " +
                    "of other planes or the foulness known as “civilization”, Golarion calls upon a few mortal emissaries to serve as her warriors in this fight.");
            });

            #region SpellBook
            var SoldierOfGaiaSpelllist = Helpers.CreateBlueprint<BlueprintSpellList>("SoldierOfGaiaSpelllist", bp => {//Fill in mod support to get all spells
                bp.IsMythic = false;
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                };
            });
            var SoldierOfGaiaSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("SoldierOfGaiaSpellbook", bp => {
                bp.Name = Helpers.CreateString($"SoldierOfGaiaSpellbook.Name", "Soldier of Gaia");
                bp.m_SpellsPerDay = WarpriestSpellLevels.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = SoldierOfGaiaSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Wisdom;
                bp.AllSpellsKnown = true;
                bp.CantripsType = CantripsType.Orisions;
                bp.CasterLevelModifier = 0;
                bp.IsArcane = false;
            });
            SoldierOfGaiaArchetype.m_ReplaceSpellbook = SoldierOfGaiaSpellbook.ToReference<BlueprintSpellbookReference>();
            #endregion
            #region Spon Casting
            var SummonNatureISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("c6147854641924442a3bb736080cfeb6");
            var SummonNatureIISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("848bd9df8b2643143a7020be7cde8800");
            var SummonNatureIIISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("6db23a29c0c55c546a0feaef0c8d33d6");
            var SummonNatureIVSingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("71dfb899a04db614e9db458ed4e43f56");
            var SummonNatureVSingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("28ea1b2e0c4a9094da208b4c186f5e4f");
            var SummonNatureVISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("060afb9e13d8a3547ad0dd20c407c0a5");
            var SummonNatureIId3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("b8ac9c653789b2a46ad85a075734c0e2");
            var SummonNatureIIId3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("06d11dfa15e63bd41b01e09d5464ee8f");
            var SummonNatureIVd3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("eb259941d7c2c5144844a31e72810642");
            var SummonNatureVd3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("03e8e9605925b7140bdd331232b78d25");
            var SummonNatureVId3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("2aab2a0c280ed3e408a09967ec6bb281");
            var SummonNatureIIId4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("bb1bac85be6b1e44eafdc54a3b757c3e");
            var SummonNatureIVd4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("3050599c1ca9a9b40940a9426d4f861b");
            var SummonNatureVd4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("87c64591b0e6f7542807429d14bb1723");
            var SummonNatureVId4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("7aefdbd7e0933b744b9c85566d16e504");
            var SoldierOfGaiaSpontaneousCasting = Helpers.CreateBlueprint<BlueprintFeature>("SoldierOfGaiaSpontaneousCasting", bp => {
                bp.SetName("Spontaneous Casting");
                bp.SetDescription("A soldier of gaia can expend any prepared spell of 1st level or higher to cast a summon nature’s ally spell of that level.");
                bp.m_Icon = DruidSpontaneousSummonIcon;
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        SummonNatureISingle,
                        SummonNatureIISingle,
                        SummonNatureIIISingle,
                        SummonNatureIVSingle,
                        SummonNatureVSingle,
                        SummonNatureVISingle,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonNatureIId3,
                        SummonNatureIIId3,
                        SummonNatureIVd3,
                        SummonNatureVd3,
                        SummonNatureVId3,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonNatureIIId4Plus1,
                        SummonNatureIVd4Plus1,
                        SummonNatureVd4Plus1,
                        SummonNatureVId4Plus1,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            #endregion
            #region Base Class PreReq Patch
            Main.Log("Patching Warpriest for Soldier of Gaia");
            var ChannelPositiveAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("8c769102f3996684fb6e09a2c4e7e5b9");
            var WarpriestPosFeatures = new BlueprintFeature[] {
                Resources.GetBlueprint<BlueprintFeature>("bf6e072089989444d8f3dddf84677798"), //Fervor Pos
                Resources.GetBlueprint<BlueprintFeature>("bd588bc544d2f8547a02bb82ad9f466a"), //Channel Pos
            };
            foreach (var feature in WarpriestPosFeatures) {
                feature.GetComponents<PrerequisiteFeature>().ForEach(c => {
                    c.Group = Prerequisite.GroupType.Any;
                });
                feature.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.HideInUI = true;
                    c.Amount = 2;
                    c.m_Features = new BlueprintFeatureReference[] {
                        SoldierOfGaiaSpontaneousCasting.ToReference<BlueprintFeatureReference>(),
                        ChannelPositiveAllowed
                    };
                });
            }
            var ChannelNegativeAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("dab5255d809f77c4395afc2b713e9cd6");
            var WarpriestNegFeatures = new BlueprintFeature[] {
                Resources.GetBlueprint<BlueprintFeature>("689fd51f02e3c5f40918b6a6e830bbc8"), //Fervor Neg
                Resources.GetBlueprint<BlueprintFeature>("e02c8a7336a542f4baffa116b6506950"), //Channel Neg
            };
            foreach (var feature in WarpriestNegFeatures) {
                feature.GetComponents<PrerequisiteFeature>().ForEach(c => {
                    c.Group = Prerequisite.GroupType.Any;
                });
                feature.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.HideInUI = true;
                    c.Amount = 2;
                    c.m_Features = new BlueprintFeatureReference[] {
                        SoldierOfGaiaSpontaneousCasting.ToReference<BlueprintFeatureReference>(),
                        ChannelNegativeAllowed
                    };
                });
            }
            #endregion
            #region Blessings
            var AirBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");
            var AnimalBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("9d991f8374c3def4cb4a6287f370814d");
            var EarthBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("73c37a22bc9a523409a47218d507acf6");
            var FireBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("2368212fa3856d74589e924d3e2074d8");
            var PlantBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("4cd28bbb761f490fa418d471383e38c7");
            var WaterBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("0f457943bb99f9b48b709c90bfc0467e");
            var WeatherBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("4172d92c598de1d47aa2c0dd51c05e24");
            var SoldierOfGaiaBlessingSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("", bp => {
                bp.SetName("Soldier's Blessing");
                bp.SetDescription("At least one of a soldier’s blessings must be drawn from the following list: Air, Animal, Earth, Fire, Plant, Water, or Weather. " +
                    "\nThe soldier cannot worship a deity that does not offer at least one of those blessings. ");
                bp.AddFeatures(AirBlessingFeature, AnimalBlessingFeature, EarthBlessingFeature, FireBlessingFeature, PlantBlessingFeature, WaterBlessingFeature, WeatherBlessingFeature);
                bp.m_Icon = SecondBlessingSelection.Icon;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Group = FeatureGroup.WarpriestBlessing;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
            });
            #endregion
            #region FriendOfTheForest
            var EntangledBuff = Resources.GetBlueprint<BlueprintBuff>("d1aea643c260c5e4ea66012876f2b7f5");
            var SoldierOfGaiaFriendOfTheForestBuff = Helpers.CreateBuff("SoldierOfGaiaFriendOfTheForestBuff", bp => {
                bp.SetName("Friend of the Forest");
                bp.SetDescription("A target grappled by an arrow can attempt to escape every {g|Encyclopedia:Combat_Round}round{/g} by making a successful combat " +
                    "maneuver, {g|Encyclopedia:Strength}Strength{/g}, {g|Encyclopedia:Athletics}Athletics{/g}, or {g|Encyclopedia:Mobility}Mobility check{/g} against the archers " +
                    "CMD. The pinned target gets a +4 bonus on this check.");
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
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>()
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

            var SoldierOfGaiaFriendOfTheForestArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("SoldierOfGaiaFriendOfTheForestArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = SoldierOfGaiaFriendOfTheForestBuff.ToReference<BlueprintBuffReference>(),
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
                                            m_Buff = SoldierOfGaiaFriendOfTheForestBuff.ToReference<BlueprintBuffReference>(),
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
                                        m_Buff = SoldierOfGaiaFriendOfTheForestBuff.ToReference<BlueprintBuffReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = SoldierOfGaiaFriendOfTheForestBuff.ToReference<BlueprintBuffReference>()
                                }
                                )
                        }
                        );
                    c.Round = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuff() {
                                        m_Buff = SoldierOfGaiaFriendOfTheForestBuff.ToReference<BlueprintBuffReference>(),
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
                                            m_Buff = SoldierOfGaiaFriendOfTheForestBuff.ToReference<BlueprintBuffReference>(),
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
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>()
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
            SoldierOfGaiaFriendOfTheForestArea.Fx = SoldierOfGaiaFriendOfTheForestArea.Fx.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "FriendOfTheForest_20feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                pfl.transform.localScale = new(0.25f, 1.0f, 0.25f);                
            });

            var SoldierOfGaiaFriendOfTheForestAbility = Helpers.CreateBlueprint<BlueprintAbility>("SoldierOfGaiaFriendOfTheForestAbility", bp => {
                bp.SetName("Friend of the Forest");
                bp.SetDescription("This spell summons forth a misty cloud of rust-red toxic algae. Any creature within the mist is coated by it, turning the creature the same reddish color. " +
                    "All targets within the mist gain concealment. Any creature within the mist must save or take 1d4 points of Wisdom damage and become enraged, attacking any creatures it " +
                    "detects nearby (as the “attack nearest creature” result of the confused condition). An enraged creature remains so for one minute per caster level. A creature only " +
                    "needs to save once each time it is within the mist (though leaving and returning requires another save).");
                bp.m_Icon = EntangledBuff.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = SoldierOfGaiaFriendOfTheForestArea.ToReference<BlueprintAbilityAreaEffectReference>(),
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
                bp.AddComponent<AbilityAoERadius>(c => {
                    c.m_Radius = 20.Feet();
                    c.m_TargetType = TargetType.Any;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { 
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>() 
                    };
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
                bp.LocalizedDuration = Helpers.CreateString("SoldierOfGaiaFriendOfTheForestAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SoldierOfGaiaFriendOfTheForestFeature = Helpers.CreateBlueprint<BlueprintFeature>("SoldierOfGaiaFriendOfTheForestFeature", bp => {
                bp.SetName("Friend of the Forest");
                bp.SetDescription("At 7th level, once per day, a soldier of gaia can call upon their connection with nature to receive aid from the natural world. " +
                    "\nEvery creature within the area of the spell is the target of a combat maneuver check made to grapple each round at the beginning of your turn, " +
                    "as well as when entering the area.");
                bp.m_Icon = EntangledBuff.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SoldierOfGaiaFriendOfTheForestAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            #endregion

            SoldierOfGaiaArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, WarpriestSpontaneousSelection, SecondBlessingSelection),
                    Helpers.LevelEntry(7, SacredArmorFeature),
                    Helpers.LevelEntry(10, SacredArmorEnchantPlus2),
                    Helpers.LevelEntry(13, SacredArmorEnchantPlus3),
                    Helpers.LevelEntry(16, SacredArmorEnchantPlus4),
                    Helpers.LevelEntry(19, SacredArmorEnchantPlus5)
            };
            SoldierOfGaiaArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, SoldierOfGaiaSpontaneousCasting, SoldierOfGaiaBlessingSelection),
                    Helpers.LevelEntry(7, SoldierOfGaiaFriendOfTheForestFeature )
            };

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Soldier of Gaia")) { return; }
            WarpriestClass.m_Archetypes = WarpriestClass.m_Archetypes.AppendToArray(SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>());
        }

    }
}
