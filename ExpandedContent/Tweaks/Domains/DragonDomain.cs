using System.Collections.Generic;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.RuleSystem.Rules;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class DragonDomain {

        public static void AddDragonDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var SacredHuntsmasterArchetype = Resources.GetBlueprint<BlueprintArchetype>("46eb929c8b6d7164188eb4d9bcd0a012");
            
            var FascinateEffectBuff = Resources.GetBlueprint<BlueprintBuff>("2d4bd347dec7d8648afd502ee40ae661");
            var Hypnotism = Resources.GetBlueprint<BlueprintAbility>("88367310478c10b47903463c5d0152b0");
            var BloodlineDraconicRedBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("3f31704e595e78942b3640cdc9b95d8b");
            var BloodlineDraconicWhiteBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("84be529914c90664aa948d8266bb3fa6");
            var BloodlineDraconicBlueBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("60a3047f434f38544a2878c26955d3ad");
            var BloodlineDraconicBlackBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("1e65b0b2db777e24db96d8bc52cc9207");
            var FireCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("6dfc5e4c7d9ae3048984744222dbd0fa");
            var ColdCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("5af8b717a209fd444a1e4d077ed776f0");
            var SonicCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("868f9126707bdc5428528dd492524d52");
            var AcidCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("f6544caac8fe528489327cd86a84b025");


            //Spelllist
            var MagicFangSpell = Resources.GetBlueprint<BlueprintAbility>("403cf599412299a4f9d5d925c7b9fb33");
            var PerniciousPoisonSpell = Resources.GetBlueprint<BlueprintAbility>("dee3074b2fbfb064b80b973f9b56319e");
            var MagicFangGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("f1100650705a69c4384d3edd88ba0f52");
            var DragonBreathSpell = Resources.GetBlueprint<BlueprintAbility>("5e826bcdfde7f82468776b55315b2403");
            var AnimalGrowthSpell = Resources.GetBlueprint<BlueprintAbility>("56923211d2ac95e43b8ac5031bab74d8");
            var FormOfTheDragonISpell = Resources.GetBlueprint<BlueprintAbility>("f767399367df54645ac620ef7b2062bb");
            var CreepingDoomSpell = Resources.GetBlueprint<BlueprintAbility>("b974af13e45639a41a04843ce1c9aa12");
            var AnimalShapesSpell = Resources.GetBlueprint<BlueprintAbility>("cf689244b2c7e904eb85f26fd6e81552");
            var ShapechangeSpell = Resources.GetBlueprint<BlueprintAbility>("22b9044aa229815429d57d0a30e4b739");
            var DragonDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("DragonDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicFangSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PerniciousPoisonSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicFangGreaterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DragonBreathSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AnimalGrowthSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FormOfTheDragonISpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CreepingDoomSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AnimalShapesSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShapechangeSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var DragonDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainSpellListFeature", bp => {
                bp.m_Icon = FormOfTheDragonISpell.Icon;
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DragonDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            //DragonDomainGreaterResource
            var DragonDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("DragonDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 4,
                    LevelStep = 5,
                    StartingIncrease = 1,
                    PerStepIncrease = 1                    
                };
                bp.m_UseMax = true;
                bp.m_Max = 3;
            });

            var DragonDomainGreaterFireAbility = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterFireAbility", bp => {
                bp.SetName("Dragonbreath - Fire");
                bp.SetDescription("At 4th level, you may use a fire breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of fire damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 6;
                    c.m_StepLevel = 2;
                });                
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterFireFeature = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterFireFeature", bp => {
                bp.SetName("Dragonbreath - Fire");
                bp.SetDescription("At 4th level, you may use a fire breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of fire damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterFireAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterFireAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DragonDomainGreaterColdAbility = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterColdAbility", bp => {
                bp.SetName("Dragonbreath - Cold");
                bp.SetDescription("At 4th level, you may use a ice breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of cold damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 6;
                    c.m_StepLevel = 2;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterColdFeature = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterColdFeature", bp => {
                bp.SetName("Dragonbreath - Cold");
                bp.SetDescription("At 4th level, you may use a ice breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of cold damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterColdAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterColdAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DragonDomainGreaterElectricityAbility = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterElectricityAbility", bp => {
                bp.SetName("Dragonbreath - Electricity");
                bp.SetDescription("At 4th level, you may use a electricity breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of electricity damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 6;
                    c.m_StepLevel = 2;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterElectricityFeature = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterElectricityFeature", bp => {
                bp.SetName("Dragonbreath - Electricity");
                bp.SetDescription("At 4th level, you may use a electricity breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of electricity damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterElectricityAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterElectricityAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DragonDomainGreaterAcidAbility = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterAcidAbility", bp => {
                bp.SetName("Dragonbreath - Acid");
                bp.SetDescription("At 4th level, you may use a acid breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of acid damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 6;
                    c.m_StepLevel = 2;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterAcidFeature = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterAcidFeature", bp => {
                bp.SetName("Dragonbreath - Acid");
                bp.SetDescription("At 4th level, you may use a acid breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of acid damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterAcidAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterAcidAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DragonDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DragonDomainGreaterFeature", bp => {
                bp.SetName("Dragonbreath");
                bp.SetDescription("At 4th level, you may use a breath weapon once per day as a standard action. When you gain this ability, choose acid, cold, fire, " +
                    "or electricity—this determines what kind of damage your breath weapon deals. Your breath weapon fills a 15-foot cone, and deals 3d6 points of " +
                    "damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by your dragonbreath attack " +
                    "can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, you can use this ability " +
                    "two times per day, and at 14th level you can use it three times per day.");
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DragonDomainGreaterFireFeature.ToReference<BlueprintFeatureReference>(),
                    DragonDomainGreaterColdFeature.ToReference<BlueprintFeatureReference>(),
                    DragonDomainGreaterElectricityFeature.ToReference<BlueprintFeatureReference>(),
                    DragonDomainGreaterAcidFeature.ToReference<BlueprintFeatureReference>()
                };                
            });

            var ScalykindDomainBaseResource = Resources.GetModBlueprint<BlueprintAbilityResource>("ScalykindDomainBaseResource");
            // DragonDomainBaseAbility
            var DragonDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("DragonBaseAbility", bp => {
                bp.SetName("Venomous Stare");
                bp.SetDescription("This gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Untyped
                                    },
                                    AbilityType = StatType.Unknown,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero
                                    },
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank
                                        }
                                    }
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = FascinateEffectBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = true,
                                    DurationSeconds = 6
                                }
                            )
                        }
                    );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScalykindDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            }); 
            var DragonDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainBaseFeature", bp => {
                bp.SetName("Dragon Subdomain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nDragonbreath: At 4th level, you may use a breath weapon once per day as a standard action. When you gain this ability, choose acid, cold, fire, " +
                    "or electricity—this determines what kind of damage your breath weapon deals. Your breath weapon fills a 15-foot cone, and deals 3d6 points of " +
                    "damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by your dragonbreath attack " +
                    "can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, you can use this ability " +
                    "two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = ScalykindDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DragonDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                
                bp.IsClassFeature = true;
            });
            //Deity plug
            var DragonDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = DragonDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var DragonDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("DragonDomainProgression", bp => {
                bp.SetName("Dragon Subdomain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nDragonbreath: At 4th level, you may use a breath weapon once per day as a standard action. When you gain this ability, choose acid, cold, fire, " +
                    "or electricity—this determines what kind of damage your breath weapon deals. Your breath weapon fills a 15-foot cone, and deals 3d6 points of " +
                    "damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by your dragonbreath attack " +
                    "can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, you can use this ability " +
                    "two times per day, and at 14th level you can use it three times per day.\nDomain Spells: magic fang, pernicious poison, greater magic fang, " +
                    "dragon breath, animal growth, form of the dragon, creeping doom, animal shapes, shapechange.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SacredHuntsmasterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DragonDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DragonDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;                
                bp.Groups = new FeatureGroup[] { FeatureGroup.Domain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = -2
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };                
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, DragonDomainBaseFeature),
                    Helpers.LevelEntry(4, DragonDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DragonDomainBaseFeature, DragonDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var DragonDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("DragonDomainProgressionSecondary", bp => {
                bp.SetName("Dragon Subdomain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nDragonbreath: At 4th level, you may use a breath weapon once per day as a standard action. When you gain this ability, choose acid, cold, fire, " +
                    "or electricity—this determines what kind of damage your breath weapon deals. Your breath weapon fills a 15-foot cone, and deals 3d6 points of " +
                    "damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by your dragonbreath attack " +
                    "can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, you can use this ability " +
                    "two times per day, and at 14th level you can use it three times per day.\nDomain Spells: magic fang, pernicious poison, greater magic fang, " +
                    "dragon breath, animal growth, form of the dragon, creeping doom, animal shapes, shapechange.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SacredHuntsmasterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.ClericSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, DragonDomainBaseFeature),
                    Helpers.LevelEntry(4, DragonDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DragonDomainBaseFeature, DragonDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var ScalykindDomainBaseResourceSeparatist = Resources.GetModBlueprint<BlueprintAbilityResource>("ScalykindDomainBaseResourceSeparatist");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");

            var DragonDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DragonDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("DragonDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 6,
                    LevelStep = 5,
                    StartingIncrease = 1,
                    PerStepIncrease = 1
                };
                bp.m_UseMax = true;
                bp.m_Max = 3;
            });


            var DragonDomainGreaterFireAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterFireAbilitySeparatist", bp => {
                bp.SetName("Dragonbreath - Fire");
                bp.SetDescription("At 4th level, you may use a fire breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of fire damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 8;
                    c.m_StepLevel = 2;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterFireFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterFireFeatureSeparatist", bp => {
                bp.SetName("Dragonbreath - Fire");
                bp.SetDescription("At 4th level, you may use a fire breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of fire damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterFireAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterFireAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DragonDomainGreaterColdAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterColdAbilitySeparatist", bp => {
                bp.SetName("Dragonbreath - Cold");
                bp.SetDescription("At 4th level, you may use a ice breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of cold damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 8;
                    c.m_StepLevel = 2;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterColdFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterColdFeatureSeparatist", bp => {
                bp.SetName("Dragonbreath - Cold");
                bp.SetDescription("At 4th level, you may use a ice breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of cold damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterColdAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterColdAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DragonDomainGreaterElectricityAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterElectricityAbilitySeparatist", bp => {
                bp.SetName("Dragonbreath - Electricity");
                bp.SetDescription("At 4th level, you may use a electricity breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of electricity damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 8;
                    c.m_StepLevel = 2;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterElectricityFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterElectricityFeatureSeparatist", bp => {
                bp.SetName("Dragonbreath - Electricity");
                bp.SetDescription("At 4th level, you may use a electricity breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of electricity damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterElectricityAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterElectricityAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DragonDomainGreaterAcidAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("DragonDomainGreaterAcidAbilitySeparatist", bp => {
                bp.SetName("Dragonbreath - Acid");
                bp.SetDescription("At 4th level, you may use a acid breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of acid damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
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
                                    ValueType = ContextValueType.Shared,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 3
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_StartLevel = 8;
                    c.m_StepLevel = 2;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
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
            var DragonDomainGreaterAcidFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainGreaterAcidFeatureSeparatist", bp => {
                bp.SetName("Dragonbreath - Acid");
                bp.SetDescription("At 4th level, you may use a acid breath weapon once per day as a standard action. Your breath weapon fills a 15-foot cone, and " +
                    "deals 3d6 points of acid damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by " +
                    "your dragonbreath attack can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, " +
                    "you can use this ability two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DragonDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainGreaterAcidAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainGreaterAcidAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var DragonDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DragonDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Dragonbreath");
                bp.SetDescription("At 4th level, you may use a breath weapon once per day as a standard action. When you gain this ability, choose acid, cold, fire, " +
                    "or electricity—this determines what kind of damage your breath weapon deals. Your breath weapon fills a 15-foot cone, and deals 3d6 points of " +
                    "damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by your dragonbreath attack " +
                    "can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, you can use this ability " +
                    "two times per day, and at 14th level you can use it three times per day.");
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DragonDomainGreaterFireFeatureSeparatist.ToReference<BlueprintFeatureReference>(),
                    DragonDomainGreaterColdFeatureSeparatist.ToReference<BlueprintFeatureReference>(),
                    DragonDomainGreaterElectricityFeatureSeparatist.ToReference<BlueprintFeatureReference>(),
                    DragonDomainGreaterAcidFeatureSeparatist.ToReference<BlueprintFeatureReference>()
                };
            });

            var DragonDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("DragonBaseAbilitySeparatist", bp => {
                bp.SetName("Venomous Stare");
                bp.SetDescription("This gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Untyped
                                    },
                                    AbilityType = StatType.Unknown,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero
                                    },
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = FascinateEffectBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = true,
                                    DurationSeconds = 6
                                }
                            )
                        }
                    );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 4;
                    c.m_StepLevel = 2;
                    c.m_CustomProperty = SeparatistAsIsProperty.ToReference<BlueprintUnitPropertyReference>();
                });                
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScalykindDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var DragonDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DragonDomainBaseFeatureSeparatist", bp => {
                bp.SetName("Dragon Subdomain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nDragonbreath: At 4th level, you may use a breath weapon once per day as a standard action. When you gain this ability, choose acid, cold, fire, " +
                    "or electricity—this determines what kind of damage your breath weapon deals. Your breath weapon fills a 15-foot cone, and deals 3d6 points of " +
                    "damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by your dragonbreath attack " +
                    "can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, you can use this ability " +
                    "two times per day, and at 14th level you can use it three times per day.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DragonDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ScalykindDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DragonDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DragonDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;

                bp.IsClassFeature = true;
            });

            var DragonDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("DragonDomainProgressionSeparatist", bp => {
                bp.SetName("Dragon Subdomain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nDragonbreath: At 4th level, you may use a breath weapon once per day as a standard action. When you gain this ability, choose acid, cold, fire, " +
                    "or electricity—this determines what kind of damage your breath weapon deals. Your breath weapon fills a 15-foot cone, and deals 3d6 points of " +
                    "damage—this damage increases by 1d6 points at every even-numbered level you gain beyond 4th level. A creature hit by your dragonbreath attack " +
                    "can attempt a Reflex save (DC 10 + 1/2 your cleric level + your Wisdom modifier) to take half damage. At 9th level, you can use this ability " +
                    "two times per day, and at 14th level you can use it three times per day.\nDomain Spells: magic fang, pernicious poison, greater magic fang, " +
                    "dragon breath, animal growth, form of the dragon, creeping doom, animal shapes, shapechange.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DragonDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {  };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, DragonDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(6, DragonDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DragonDomainBaseFeatureSeparatist, DragonDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            DragonDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                DragonDomainProgression.ToReference<BlueprintFeatureReference>(),
                DragonDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            DragonDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DragonDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            DragonDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DragonDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            DragonDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DragonDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            DragonDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DragonDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            DragonDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DragonDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(DragonDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterFireAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterColdAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterElectricityAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterAcidAbility.ToReference<BlueprintAbilityReference>()); 
            DomainMastery.Abilities.Add(DragonDomainGreaterAcidAbility.ToReference<BlueprintAbilityReference>()); 
            DomainMastery.Abilities.Add(DragonDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterFireAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterColdAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterElectricityAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(DragonDomainGreaterAcidAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Dragon Subdomain")) { return; }
            DomainTools.RegisterDomain(DragonDomainProgression);
            DomainTools.RegisterSecondaryDomain(DragonDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(DragonDomainProgression);
            DomainTools.RegisterTempleDomain(DragonDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(DragonDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(DragonDomainProgression, DragonDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(DragonDomainProgressionSeparatist);

        }
    }
}
