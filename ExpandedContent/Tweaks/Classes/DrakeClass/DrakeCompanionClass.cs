using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.DrakeClass {
    internal class DrakeCompanionClass {

        public static void AddDrakeCompanionClass() {

            var BABMedium = Resources.GetBlueprint<BlueprintStatProgression>("4c936de4249b61e419a3fb775b9f2581");
            var DragonType = Resources.GetBlueprint<BlueprintFeature>("455ac88e22f55804ab87c2467deff1d6");
            var SavesHigh = Resources.GetBlueprint<BlueprintStatProgression>("ff4662bde9e75f145853417313842751");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var RangerClass = Resources.GetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var RogueClass = Resources.GetBlueprint<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");
            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var DrakeCompanionProgression = Helpers.CreateBlueprint<BlueprintProgression>("DrakeCompanionProgression", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("");
                bp.SetDescription("");
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = RangerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = RogueClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ShamanClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {};
                bp.LevelEntries = Enumerable.Range(2, 20)
                    .Select(i => new LevelEntry {
                        Level = i,
                        m_Features = new List<BlueprintFeatureBaseReference> {
                            AnimalCompanionRank.ToReference<BlueprintFeatureBaseReference>()
                        },
                    })
                    .ToArray();
                bp.ForAllOtherClasses = false;
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var DrakeCompanionClassProgression = Helpers.CreateBlueprint<BlueprintProgression>("DrakeCompanionClassProgression", bp => {
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeCompanionClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("DrakeCompanionClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"DrakeCompanionClass.Name", "Drake Companion");
                bp.LocalizedDescription = Helpers.CreateString($"DrakeCompanionClass.Description", "Drakes are brutish lesser kindred of true dragons. Though they aren’t particularly intelligent, " +
                    "drakes’ significantly faster breeding allows their kind to survive in harsh environments. While a young drake is weaker than a standard animal " +
                    "companion, as they grow they will start to resemble their draconic cousins more and more until they rival them in power.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DrakeCompanionClass.Description", "Drakes are brutish lesser kindred of true dragons. Though they aren’t particularly intelligent, " +
                    "drakes’ significantly faster breeding allows their kind to survive in harsh environments. While a young drake is weaker than a standard animal " +
                    "companion, as they grow they will start to resemble their draconic cousins more and more until they rival them in power.");
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.HideInUI = true;
                    c.m_Feature = DragonType.ToReference<BlueprintFeatureReference>();
                });
                bp.SkillPoints = 5;
                bp.HitDie = DiceType.D12;
                bp.HideIfRestricted = true;
                bp.m_BaseAttackBonus = BABMedium.ToReference<BlueprintStatProgressionReference>();
                bp.m_FortitudeSave = SavesHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_ReflexSave = SavesHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_WillSave = SavesHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_Progression = DrakeCompanionClassProgression.ToReference<BlueprintProgressionReference>();
                bp.ClassSkills = new StatType[] {
                    StatType.SkillAthletics,
                    StatType.SkillStealth,
                    StatType.SkillPersuasion,
                    StatType.SkillMobility,
                    StatType.SkillPerception,
                    StatType.SkillUseMagicDevice
                };
                bp.IsArcaneCaster = false;
                bp.IsDivineCaster = false;
            });
            var DrakeSubtypeFire = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSubtypeFire", bp => {
                bp.SetName("Drake Subtype - Fire");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var DrakeSubtypeCold = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSubtypeCold", bp => {
                bp.SetName("Drake Subtype - Cold");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var DrakeSubtypeAir = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSubtypeAir", bp => {
                bp.SetName("Drake Subtype - Air");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var DrakeSubtypeEarth = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSubtypeEarth", bp => {
                bp.SetName("Drake Subtype - Earth");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DrakeVisionFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeVisionFeature", bp => {
                bp.SetName("Drake Vision");
                bp.SetDescription("Drakes have both natraly clear eye sight, and near perfect night vision. This gives drakes a +4 to all " +
                    "Perception rolls");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Value = 4;
                });
            });
            var DrakeImmunitiesFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeImmunitiesFeature", bp => {
                bp.SetName("Drake Immunities");
                bp.SetDescription("Drakes have all the immunities of their older draconic cousins, along with the ability to fly over difficult terrain");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonType.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DrakeCompanionSlotFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeCompanionSlotFeature", bp => {
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.MainHand;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.OffHand;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Boots;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon1;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon2;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon3;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon4;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon5;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon6;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon7;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon8;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Ring2;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Glasses;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Shirt;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Gloves;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Armor;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Headgear;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Cloak;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
            });
            var DrakeNaturalArmorFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor", bp => {
                bp.SetName("Drake Natural Armor");
                bp.SetDescription("The drake’s natural armor bonus to its AC increases by 2 when the charge reaches 3rd level and every 3 levels thereafter.");
            });
            var DrakeBreathCooldown = Helpers.CreateBuff("DrakeBreathCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Breath Weapon - Ability is not ready yet");
                bp.SetDescription("");
                //bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var DrakeBreathAbilityResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("DrakeBreathAbilityResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    LevelIncrease = 1,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            var FireCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("6dfc5e4c7d9ae3048984744222dbd0fa");
            var ColdCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("5af8b717a209fd444a1e4d077ed776f0");
            var SonicCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("868f9126707bdc5428528dd492524d52");
            var AcidCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("f6544caac8fe528489327cd86a84b025");
            var BloodlineDraconicRedBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("3f31704e595e78942b3640cdc9b95d8b");
            var BloodlineDraconicWhiteBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("84be529914c90664aa948d8266bb3fa6");
            var BloodlineDraconicBlueBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("60a3047f434f38544a2878c26955d3ad");
            var BloodlineDraconicBlackBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("1e65b0b2db777e24db96d8bc52cc9207");
            var DrakeBreathWeaponFire1 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponFire1", bp => {
                bp.SetName("Drake Fire Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireCone15Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 15 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Fire
                            },
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
                                    Value = 4,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );                    
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponFire1Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponFire1Feature", bp => {
                bp.SetName("Drake Fire Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {DrakeBreathWeaponFire1.ToReference<BlueprintUnitFactReference>()};
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {DrakeBreathWeaponFire1.ToReference<BlueprintAbilityReference>()};
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeFire.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeBreathWeaponCold1 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponCold1", bp => {
                bp.SetName("Drake Cold Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdCone15Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 15 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Cold
                            },
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
                                    Value = 4,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    //c.ResourceCostDecreasingFacts = Final Breath Upgrade
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponCold1Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponCold1Feature", bp => {
                bp.SetName("Drake Cold Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponCold1.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponCold1.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeCold.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeBreathWeaponElectricity1 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponElectricity1", bp => {
                bp.SetName("Drake Electricity Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        SonicCone15Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 15 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Electricity
                            },
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
                                    Value = 4,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    //c.ResourceCostDecreasingFacts = Final Breath Upgrade
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponElectricity1Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponElectricity1Feature", bp => {
                bp.SetName("Drake Electricity Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponElectricity1.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponElectricity1.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeAir.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeBreathWeaponAcid1 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponAcid1", bp => {
                bp.SetName("Drake Acid Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidCone15Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 15 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Acid
                            },
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
                                    Value = 4,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    //c.ResourceCostDecreasingFacts = Final Breath Upgrade
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponAcid1Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponAcid1Feature", bp => {
                bp.SetName("Drake Acid Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} in a 15-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this twice a day.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponAcid1.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponAcid1.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeEarth.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var FireCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("acf144d4da2638e4eadde1bb9dac29b4");
            var ColdCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c202b61bf074a7442bf335b27721853f");
            var SonicCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c7fd792125b79904881530dbc2ff83de");
            var AcidCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("155104dfdc285f3449610e625fa85729");
            var DrakeBreathWeaponFire2 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponFire2", bp => {
                bp.SetName("Greater Drake Fire Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Fire
                            },
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
                                    Value = 8,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    //c.ResourceCostDecreasingFacts = Final Breath Upgrade
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponFire2Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponFire2Feature", bp => {
                bp.SetName("Greater Drake Fire Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your lesser breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponFire2.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponFire2.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeFire.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponFire1Feature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Level = 11;
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            DrakeBreathWeaponFire1.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { 
                DrakeBreathWeaponFire2Feature.ToReference<BlueprintUnitFactReference>() 
            };
            var DrakeBreathWeaponCold2 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponCold2", bp => {
                bp.SetName("Greater Drake Cold Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Cold
                            },
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
                                    Value = 8,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    //c.ResourceCostDecreasingFacts = Final Breath Upgrade
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponCold2Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponCold2Feature", bp => {
                bp.SetName("Greater Drake Cold Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your lesser breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponCold2.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponCold2.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeCold.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponCold1Feature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Level = 11;
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            DrakeBreathWeaponCold1.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                DrakeBreathWeaponCold2Feature.ToReference<BlueprintUnitFactReference>()
            };
            var DrakeBreathWeaponElectricity2 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponElectricity2", bp => {
                bp.SetName("Greater Drake Electricity Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        SonicCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Electricity
                            },
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
                                    Value = 8,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    //c.ResourceCostDecreasingFacts = Final Breath Upgrade
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponElectricity2Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponElectricity2Feature", bp => {
                bp.SetName("Greater Drake Electricity Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your lesser breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponElectricity2.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponElectricity2.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeAir.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponElectricity1Feature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Level = 11;
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            DrakeBreathWeaponElectricity1.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                DrakeBreathWeaponElectricity2Feature.ToReference<BlueprintUnitFactReference>()
            };
            var DrakeBreathWeaponAcid2 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponAcid2", bp => {
                bp.SetName("Greater Drake Acid Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Acid
                            },
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
                                    Value = 8,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    //c.ResourceCostDecreasingFacts = Final Breath Upgrade
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DrakeBreathWeaponAcid2Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponAcid2Feature", bp => {
                bp.SetName("Greater Drake Acid Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}8d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} in a 30-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. Until upgraded you can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your lesser breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponAcid2.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponAcid2.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeSubtypeEarth.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponAcid1Feature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Level = 11;
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            DrakeBreathWeaponAcid1.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                DrakeBreathWeaponAcid2Feature.ToReference<BlueprintUnitFactReference>()
            };
            var DrakeIntellectFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeIntellectFeature", bp => {
                bp.SetName("Drake Intellect");
                bp.SetDescription("The drake’s Intelligence score increases by 4.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Intelligence;
                    c.Value = 4;
                });
            });
            var DrakeKeenMindFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeKeenMindFeature", bp => {
                bp.SetName("Drake Keen Mind");
                bp.SetDescription("The drake’s Intelligence, Wisdom, and Charisma scores each increase by 2. A drake must have intellect to select keen mind." +
                    "\nUnlocks the Spellcasting component of the Mythic Drake Ability.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeIntellectFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Intelligence;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Wisdom;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Charisma;
                    c.Value = 2;
                });
            });
            var DrakeAgilityFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeAgilityFeature", bp => {
                bp.SetName("Drake Agility");
                bp.SetDescription("The drake companions speed increases as it becomes used to moving and reacting in combat, movement speed increases by an " +
                    "additional 15 feet.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 15;
                });
            });
            var DrakeGreaterAgilityFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeGreaterAgilityFeature", bp => {
                bp.SetName("Greater Drake Agility");
                bp.SetDescription("The drake’s confidence on the battlefield let it move at full speed without worry of stumbling or harming allies, movement speed " +
                    "increases by an additional 15 feet.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeAgilityFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 15;
                });
            });
            var ClawHuge1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("96cb163919afd3445a4b863c677f95a1");
            var DrakenClawsBuff = Helpers.CreateBuff("DrakenClawsBuff", bp => {
                bp.SetName("Draken Claws");
                bp.SetDescription("The drake gains two natural claw attacks that deal {g|Encyclopedia:Dice}1d2{/g} damage from a tiny drake, but increase " +
                    "damage dice size each time the drake grows");
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawHuge1d8.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawHuge1d8.ToReference<BlueprintItemWeaponReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.IsClassFeature = true;
            });
            var DrakenClawsFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakenClawsFeature", bp => {
                bp.SetName("Draken Claws");
                bp.SetDescription("The drake gains two natural claw attacks that deal {g|Encyclopedia:Dice}1d2{/g} damage from a tiny drake, but increase " +
                    "damage dice size each time the drake grows" +
                    "\nUnlocks the extra Wing attacks component of the Mythic Drake Ability.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeAgilityFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakenClawsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;

            });
            var DrakenScalesFire = Helpers.CreateBlueprint<BlueprintFeature>("DrakenScalesFire", bp => {
                bp.HideInUI = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 25,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Fire;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Electricity;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Acid;
                });
            });
            var DrakenScalesCold = Helpers.CreateBlueprint<BlueprintFeature>("DrakenScalesCold", bp => {
                bp.HideInUI = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 25,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Cold;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Electricity;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Acid;
                });
            });
            var DrakenScalesElectricity = Helpers.CreateBlueprint<BlueprintFeature>("DrakenScalesElectricity", bp => {
                bp.HideInUI = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 25,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Electricity;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Fire;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Cold;
                });
            });
            var DrakenScalesAcid = Helpers.CreateBlueprint<BlueprintFeature>("DrakenScalesAcid", bp => {
                bp.HideInUI = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 25,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Acid;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Fire;

                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.Type = DamageEnergyType.Cold;

                });
            });
            var DrakenScalesFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakenScalesFeature", bp => {
                bp.SetName("Draken Scales");
                bp.SetDescription("The drake gains elemental resistances based on its subtype, along with minor resistance to elemental damage from non-opposing elements. " +
                    "\nFire subtype - Fire resistance 25, Eletricity resistance 5, Acid resistance 5." +
                    "\nCold subtype - Cold resistance 25, Eletricity resistance 5, Acid resistance 5." +
                    "\nAir subtype - Eletricity resistance 25, Fire resistance 5, Cold resistance 5." +
                    "\nAcid subtype - Acid resistance 25, Fire resistance 5, Cold resistance 5.");
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeSubtypeFire.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakenScalesFire.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeSubtypeCold.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakenScalesCold.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeSubtypeAir.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakenScalesElectricity.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeSubtypeEarth.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakenScalesAcid.ToReference<BlueprintUnitFactReference>();
                });
            });

            var DrakePowersSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DrakePowersSelection", bp => {
                bp.SetName("Drake Powers Selection");
                bp.SetDescription("Drake companions can select from the following drake powers. First at 3rd level and then every 4th level after.");
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllowNonContextActions = false;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DrakeIntellectFeature.ToReference<BlueprintFeatureReference>(),
                    DrakeKeenMindFeature.ToReference<BlueprintFeatureReference>(),
                    DrakeAgilityFeature.ToReference<BlueprintFeatureReference>(),
                    DrakeGreaterAgilityFeature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponFire1Feature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponCold1Feature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponElectricity1Feature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponAcid1Feature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponFire2Feature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponCold2Feature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponElectricity2Feature.ToReference<BlueprintFeatureReference>(),
                    DrakeBreathWeaponAcid2Feature.ToReference<BlueprintFeatureReference>(),
                    DrakenClawsFeature.ToReference<BlueprintFeatureReference>(),
                    DrakenScalesFeature.ToReference<BlueprintFeatureReference>()
                };
            });
            //Size stuff
            ///Tiny
            var DrakeSizeTinyBuff = Helpers.CreateBuff("DrakeSizeTinyBuff", bp => {
                bp.SetName("Drake Size - Tiny");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -4;
                    c.Size = Size.Fine;
                });
            });
            var DrakeSizeTiny = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeTiny", bp => {
                bp.HideInUI = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeSizeTinyBuff.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DrakeSizeTinyFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeTinyFeature", bp => {
                bp.SetName("Tiny Drake");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 5;
                    c.m_Feature = DrakeSizeTiny.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
            });
            ///Small
            var DrakeSizeSmallBuff = Helpers.CreateBuff("DrakeSizeSmallBuff", bp => {
                bp.SetName("Drake Size - Small");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -3;
                    c.Size = Size.Fine;
                });

            });
            var DrakeSizeSmall = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeSmall", bp => {
                bp.HideInUI = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeSizeSmallBuff.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DrakeSizeSmallFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeSmallFeature", bp => {
                bp.SetName("Small Drake");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 9;
                    c.m_Feature = DrakeSizeSmall.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Strength;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Dexterity;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                });                
            });
            ///Medium
            var DrakeSizeMediumBuff = Helpers.CreateBuff("DrakeSizeMediumBuff", bp => {
                bp.SetName("Drake Size - Medium");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -2;
                    c.Size = Size.Fine;
                });

            });
            var DrakeSizeMedium = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeMedium", bp => {
                bp.HideInUI = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeSizeMediumBuff.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DrakeSizeMediumFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeMediumFeature", bp => {
                bp.SetName("Medium Drake");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 13;
                    c.m_Feature = DrakeSizeMedium.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Strength;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Dexterity;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                });                
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });
            });
            ///Large
            var DrakeSizeLargeBuff = Helpers.CreateBuff("DrakeSizeLargeBuff", bp => {
                bp.SetName("Drake Size - Large");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -1;
                    c.Size = Size.Fine;
                });
            });
            var DrakeSizeLarge = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeLarge", bp => {
                bp.HideInUI = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeSizeLargeBuff.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DrakeSizeLargeFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeLargeFeature", bp => {
                bp.SetName("Large Drake");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 17;
                    c.m_Feature = DrakeSizeLarge.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Strength;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Dexterity;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                });                
            });
            ///Final
            var DrakeSizeHugeFeature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeSizeHugeFeature", bp => {
                bp.SetName("Huge Drake");
                bp.SetDescription("The drake matures further and advances a size category when the charge reaches 5th level and every 4 levels thereafter. Each " +
                    "time this occurs, the drake’s natural armor bonus to its AC increases by 2, its natural attacks increase in damage based on the new size " +
                    "category, and it gains the following ability scores adjustments: Str +4, Dex –2, Con +2. When the drake reaches Medium size, its speed " +
                    "increases from 20 feet to 30 feet");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Strength;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Dexterity;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                });                
            });
            var DrakeNaturalArmor1 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor1", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 2;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor2 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor2", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 4;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor3 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor3", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 6;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor4 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor4", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 10;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor5 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor5", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 12;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor6 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor6", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 14;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor7 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor7", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 16;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor8 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor8", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 18;
                });
                bp.HideInUI = true;
            });
            var DrakeNaturalArmor9 = Helpers.CreateBlueprint<BlueprintFeature>("DrakeNaturalArmor9", bp => {
                bp.SetName("Drake Natural Armor");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 20;
                });
                bp.HideInUI = true;
            });

            //Copact Drake Stuff
            var ReducePerson = Resources.GetBlueprint<BlueprintAbility>("4e0e9aba6447d514f88eff1464cc4763");
            var CompactDrakeBuff = Helpers.CreateBuff("CompactDrakeBuff", bp => {
                bp.SetName("Compact Drake");
                bp.SetDescription("After the drake grows to a large size it gains the ability to shrink back down to their medium size. When this " +
                    "is active the drakes stats are modified; -4 Strength, +2 Dexterity, and the range on attacks is reduced to the medium size range.");
                bp.m_Icon = ReducePerson.m_Icon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Strength;
                    c.Value = -4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Dexterity;
                    c.Value = 2;
                });
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -2;
                    c.Size = Size.Fine;
                });
            });            
            var CompactDrake = Helpers.CreateBlueprint<BlueprintActivatableAbility>("CompactDrake", bp => {
                bp.SetName("Compact Drake");
                bp.SetDescription("After the drake grows to a large size it gains the ability to shrink back down to their medium size. When this " +
                    "is active the drakes stats are modified; -4 Strength, +2 Dexterity, and the range on attacks is reduced to the medium size range.");
                bp.m_Icon = ReducePerson.m_Icon;
                bp.m_Buff = CompactDrakeBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.m_ActivateWithUnitCommand = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
            });
            var CompactDrakeFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompactDrakeFeature", bp => {
                bp.SetName("Compact Drake");
                bp.SetDescription("After the drake grows to a large size it gains the ability to shrink back down to their medium size. When this " +
                    "is active the drakes stats are modified; -4 Strength, +2 Dexterity, and the range on attacks is reduced to the medium size range.");
                bp.m_Icon = ReducePerson.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {CompactDrake.ToReference<BlueprintUnitFactReference>()};
                });
            });

            //Feat Lock
            var LightArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("6d3728d4e9c9898458fe5e9532951132");
            var MediumArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("46f4fb320f35704488ba3d513397789d");
            var HeavyArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("1b0f68188dcc435429fb87a022239681");
            LightArmorProficiency.AddComponent<PrerequisiteNoFeature>(c => { c.m_Feature = DrakeCompanionSlotFeature.ToReference<BlueprintFeatureReference>(); c.HideInUI = true; });
            MediumArmorProficiency.AddComponent<PrerequisiteNoFeature>(c => { c.m_Feature = DrakeCompanionSlotFeature.ToReference<BlueprintFeatureReference>(); c.HideInUI = true; });
            HeavyArmorProficiency.AddComponent<PrerequisiteNoFeature>(c => { c.m_Feature = DrakeCompanionSlotFeature.ToReference<BlueprintFeatureReference>(); c.HideInUI = true; });


            DrakeCompanionClassProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                new BlueprintProgression.ClassWithLevel {
                    m_Class = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0,
                }
            };
            DrakeCompanionClassProgression.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, DrakeVisionFeature, DrakeImmunitiesFeature, DrakeCompanionSlotFeature, DrakeSizeTinyFeature),///Subtype handled in DrakeCompanionSelection
                    Helpers.LevelEntry(3, DrakeNaturalArmorFeature, DrakeNaturalArmor1, DrakePowersSelection),
                    Helpers.LevelEntry(5, DrakeSizeSmallFeature, DrakeNaturalArmor2),
                    Helpers.LevelEntry(6, DrakeNaturalArmorFeature, DrakeNaturalArmor3),
                    Helpers.LevelEntry(7, DrakePowersSelection),
                    Helpers.LevelEntry(9, DrakeNaturalArmorFeature, DrakeSizeMediumFeature, DrakeNaturalArmor4),
                    Helpers.LevelEntry(11, DrakePowersSelection),
                    Helpers.LevelEntry(12, DrakeNaturalArmorFeature, DrakeNaturalArmor5),
                    Helpers.LevelEntry(13, DrakeSizeLargeFeature, DrakeNaturalArmor6, CompactDrakeFeature),
                    Helpers.LevelEntry(15, DrakeNaturalArmorFeature, DrakeNaturalArmor7, DrakePowersSelection),
                    Helpers.LevelEntry(17, DrakeSizeHugeFeature, DrakeNaturalArmor8),
                    Helpers.LevelEntry(18, DrakeNaturalArmorFeature, DrakeNaturalArmor9),
                    Helpers.LevelEntry(19, DrakePowersSelection),
                };
            var AnimalCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("26b10d4340839004f960f9816f6109fe");
            AnimalCompanionClass.AddComponent<PrerequisiteNoFeature>(c => {
                c.HideInUI = true;
                c.m_Feature = DragonType.ToReference<BlueprintFeatureReference>();
            });
            var BlueprintRoot = Resources.GetBlueprint<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
            BlueprintRoot.Instance.Progression.m_PetClasses = BlueprintRoot.Instance.Progression.m_PetClasses
                    .AppendToArray(DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>());
        }
    }
}
