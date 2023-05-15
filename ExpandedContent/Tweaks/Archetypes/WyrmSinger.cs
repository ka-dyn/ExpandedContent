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
using UnityEngine.Assertions.Must;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.Settings;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class WyrmSinger {
        public static void AddWyrmSinger() {

            var SkaldClass = Resources.GetBlueprint<BlueprintCharacterClass>("6afa347d804838b48bda16acb0573dc0");
            var RagingSongResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("4a2302c4ec2cfb042bba67d825babfec");
            var InspiredRageFeature = Resources.GetBlueprint<BlueprintFeature>("1a639eadc2c3ed546bc4bb236864cd0c");
            var SkaldRagePowerSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2476514e31791394fa140f1a07941c96");
            var SongOfTheFallenFeature = Resources.GetBlueprint<BlueprintFeature>("9fc5d126524dbc84a90b1856707e2d87");
            var InspiredRageAllyToggleSwitchBuff = Resources.GetBlueprint<BlueprintBuff>("9fd2dd27d838f0049ab56e9da3508b25");
            var FormOfTheDragon1BlackBuff = Resources.GetBlueprint<BlueprintBuff>("268fafac0a5b78c42a58bd9c1ae78bcf");
            var FormOfTheDragon1BlueBuff = Resources.GetBlueprint<BlueprintBuff>("b117bc8b41735924dba3fb23318f39ff");
            var FormOfTheDragon1BrassBuff = Resources.GetBlueprint<BlueprintBuff>("17d330af03f5b3042a4417ab1d45e484");
            var FormOfTheDragon1RedBuff = Resources.GetBlueprint<BlueprintBuff>("294cbb3e1d547f341a5d7ec8500ffa44");
            var FormOfTheDragon1GreenBuff = Resources.GetBlueprint<BlueprintBuff>("02611a12f38bed340920d1d427865917");
            var FormOfTheDragon1SilverBuff = Resources.GetBlueprint<BlueprintBuff>("feb2ab7613e563e45bcf9f7ffe4e05c6");
            var FormOfTheDragon1WhiteBuff = Resources.GetBlueprint<BlueprintBuff>("a6acd3ad1e9fa6c45998d43fd5dcd86d");
            var FireCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("acf144d4da2638e4eadde1bb9dac29b4");
            var ColdCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c202b61bf074a7442bf335b27721853f");
            var SonicCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c7fd792125b79904881530dbc2ff83de");
            var AcidCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("155104dfdc285f3449610e625fa85729");
            var FireLine00 = Resources.GetBlueprint<BlueprintProjectile>("ecf79fc871f15074e95698a3fef47aee");
            var ColdLine00 = Resources.GetBlueprint<BlueprintProjectile>("df0464dbf5b83804d9980eb42ed37462");
            var LightningBolt00 = Resources.GetBlueprint<BlueprintProjectile>("c7734162c01abdc478418bfb286ed7a5");
            var AcidLine00 = Resources.GetBlueprint<BlueprintProjectile>("33af0c7694f8d734397bd03e6d4b72f1");

            //Spells for Resist Call of the Wild
            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var FeeblemindSpell = Resources.GetBlueprint<BlueprintAbility>("444eed6e26f773a40ab6e4d160c67faa");
            var DazeSpell = Resources.GetBlueprint<BlueprintAbility>("55f14bc84d7c85446b07a1b5dd6b2b4c");
            var InsanitySpell = Resources.GetBlueprint<BlueprintAbility>("2b044152b3620c841badb090e01ed9de");

            var WyrmSingerArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("WyrmSingerArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"WyrmSingerArchetype.Name", "WyrmSinger");
                bp.LocalizedDescription = Helpers.CreateString($"WyrmSingerArchetype.Description", "Wyrm singers spin fragments of the story of the ongoing struggle between noble Apsu and wicked Dahak.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"WyrmSingerArchetype.Description", "Wyrm singers spin fragments of the story of the ongoing struggle between noble Apsu and wicked Dahak.");                
            });

            var WyrmSingerBreathWeaponBuffAcid = Helpers.CreateBuff("WyrmSingerBreathWeaponBuffAcid", bp => {
                bp.SetName("Steam Ray Fusillade");
                bp.SetDescription("Once per round as a standard action you may fire three rays, plus one additional ray for every four levels beyond 11th (to a maximum of " +
                    "five rays at 19th level). Each ray requires a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} to hit and deals {g|Encyclopedia:Dice}4d6{/g} points of " +
                    "{g|Encyclopedia:Energy_Damage}fire damage{/g}.");
                bp.m_Icon = DazeSpell.m_Icon;
                //Components added later                
            });
            var WyrmSingerBreathWeaponBaseAcid = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponBaseAcid", bp => {
                bp.SetName("Wyrm Singers Acid Breath");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of acid damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.m_Icon = DazeSpell.m_Icon;
                //Variants added after
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerBreathWeaponConeAcid = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponConeAcid", bp => {
                bp.SetName("Wyrm Singers Acid Breath - Cone");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of acid damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.m_Icon = DazeSpell.m_Icon;
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
                                    ValueType = ContextValueType.Rank,
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
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerBreathWeaponBuffAcid.ToReference<BlueprintBuffReference>()
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.m_Parent = WyrmSingerBreathWeaponBaseAcid.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerBreathWeaponLineAcid = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponLineAcid", bp => {
                bp.SetName("Wyrm Singers Acid Breath - Line");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of acid damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.m_Icon = DazeSpell.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidLine00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
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
                                    ValueType = ContextValueType.Rank,
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
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerBreathWeaponBuffAcid.ToReference<BlueprintBuffReference>()
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.m_Parent = WyrmSingerBreathWeaponBaseAcid.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            WyrmSingerBreathWeaponBaseAcid.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WyrmSingerBreathWeaponConeAcid.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponLineAcid.ToReference<BlueprintAbilityReference>()
                };
            });
            WyrmSingerBreathWeaponBuffAcid.TemporaryContext(bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WyrmSingerBreathWeaponBaseAcid.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        WyrmSingerBreathWeaponBaseAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineAcid.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
            });
            


            var WyrmSingerBreathWeaponResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("WyrmSingerBreathWeaponResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
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
            var WyrmSingerBreathWeaponAbility = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponAbility", bp => {
                bp.SetName("Grant Breath Weapon");
                bp.SetDescription("At 12th level, once per day as a swift action, a wyrm singer can grant a breath weapon attack to himself or an ally affected by his " +
                    "draconic rage raging song. Using the breath weapon is a standard action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapons " +
                    "deal 1d6 points of damage per 2 character levels, and are of an energy type of the wyrm singer’s choice (acid, cold, electricity, or fire). " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                //variants added after
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerBreathWeaponAbilityAcid = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponAbilityAcid", bp => {
                bp.SetName("Grant Breath Weapon - Acid");
                bp.SetDescription("At 12th level, once per day as a swift action, a wyrm singer can grant a breath weapon attack to himself or an ally affected by his " +
                    "draconic rage raging song. Using the breath weapon is a standard action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon " +
                    "deals 1d6 points of acid damage per 2 character levels. Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters " +
                    "level + their Constiution modifier) to halve the damage.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    new ContextActionApplyBuff() {
                        m_Buff = WyrmSingerBreathWeaponBuffAcid.ToReference<BlueprintBuffReference>(),
                        Permanent = true,
                        DurationValue = new ContextDurationValue() {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = 0,
                            BonusValue = 0,
                            m_IsExtendable = true,
                        },
                        IsFromSpell = false,
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerBreathWeaponAbilityCold = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponAbilityCold", bp => {
                bp.SetName("Grant Breath Weapon - Cold");
                bp.SetDescription("At 12th level, once per day as a swift action, a wyrm singer can grant a breath weapon attack to himself or an ally affected by his " +
                    "draconic rage raging song. Using the breath weapon is a standard action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon " +
                    "deals 1d6 points of cold damage per 2 character levels. Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters " +
                    "level + their Constiution modifier) to halve the damage.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    new ContextActionApplyBuff() {
                        m_Buff = WyrmSingerBreathWeaponBuffCold.ToReference<BlueprintBuffReference>(),
                        Permanent = true,
                        DurationValue = new ContextDurationValue() {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = 0,
                            BonusValue = 0,
                            m_IsExtendable = true,
                        },
                        IsFromSpell = false,
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerBreathWeaponAbilityElectricity = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponAbilityElectricity", bp => {
                bp.SetName("Grant Breath Weapon - Electricity");
                bp.SetDescription("At 12th level, once per day as a swift action, a wyrm singer can grant a breath weapon attack to himself or an ally affected by his " +
                    "draconic rage raging song. Using the breath weapon is a standard action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon " +
                    "deals 1d6 points of electricity damage per 2 character levels. Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters " +
                    "level + their Constiution modifier) to halve the damage.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    new ContextActionApplyBuff() {
                        m_Buff = WyrmSingerBreathWeaponBuffElectricity.ToReference<BlueprintBuffReference>(),
                        Permanent = true,
                        DurationValue = new ContextDurationValue() {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = 0,
                            BonusValue = 0,
                            m_IsExtendable = true,
                        },
                        IsFromSpell = false,
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerBreathWeaponAbilityFire = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponAbilityFire", bp => {
                bp.SetName("Grant Breath Weapon - Fire");
                bp.SetDescription("At 12th level, once per day as a swift action, a wyrm singer can grant a breath weapon attack to himself or an ally affected by his " +
                    "draconic rage raging song. Using the breath weapon is a standard action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon " +
                    "deals 1d6 points of fire damage per 2 character levels. Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters " +
                    "level + their Constiution modifier) to halve the damage.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    new ContextActionApplyBuff() {
                        m_Buff = WyrmSingerBreathWeaponBuffFire.ToReference<BlueprintBuffReference>(),
                        Permanent = true,
                        DurationValue = new ContextDurationValue() {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = 0,
                            BonusValue = 0,
                            m_IsExtendable = true,
                        },
                        IsFromSpell = false,
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            WyrmSingerBreathWeaponAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WyrmSingerBreathWeaponAbilityAcid.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponAbilityCold.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponAbilityElectricity.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponAbilityFire.ToReference<BlueprintAbilityReference>()
                };
            });
            var WyrmSingerBreathWeaponFeature = Helpers.CreateBlueprint<BlueprintFeature>("WyrmSingerBreathWeaponFeature", bp => {
                bp.SetName("Breath Weapon");
                bp.SetDescription("At 12th level, once per day as a swift action, a wyrm singer can grant a breath weapon attack to himself or an ally affected by his " +
                    "draconic rage raging song. Using the breath weapon is a standard action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapons " +
                    "deal 1d6 points of damage per 2 skald levels the wyrm singer has, and are of an energy type of the wyrm singer’s choice (acid, cold, electricity, or fire). " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WyrmSingerBreathWeaponAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            var InspiredRageAddFactContextActions = Resources.GetBlueprint<BlueprintBuff>("75b3978757908d24aaaecaf2dc209b89").GetComponents<AddFactContextActions>();
            var InspiredRageAddFactsFromCaster = Resources.GetBlueprint<BlueprintBuff>("75b3978757908d24aaaecaf2dc209b89").GetComponent<AddFactsFromCaster>();            
            var WyrmSingerDraconicRageEffectBuff = Helpers.CreateBuff("WyrmSingerDraconicRageEffectBuff", bp => {
                bp.SetName("Draconic Rage");
                bp.SetDescription("At 1st level, a wyrm singer can kindle an echo of ancient rage felt between warring dragon clans in his allies. This ability acts as inspired rage, except those affected gain " +
                    "a +2 bonus on melee attack and damage rolls and a +2 bonus on saving throws against paralysis and sleep effects (but they still take a –1 penalty to their AC), rather than " +
                    "inspired rage’s normal bonuses. At 4th level and every 4 skald levels thereafter, the song’s bonuses on saves against paralysis and sleep effects increase by 1. At 8th and 16th levels, " +
                    "the song’s bonus on melee attack and damage rolls increases by 1.");
                bp.m_Icon = DazeSpell.Icon;
                bp.AddComponents(InspiredRageAddFactContextActions);
                bp.AddComponent(InspiredRageAddFactsFromCaster);
                bp.AddComponent<WeaponAttackTypeDamageBonus>(c => {
                    c.Type = WeaponRangeType.Melee;
                    c.AttackBonus = 0;
                    c.Descriptor = ModifierDescriptor.Rage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default,
                        m_AbilityParameter = AbilityParameterType.Level
                    };
                });
                bp.AddComponent<AttackTypeAttackBonus>(c => {
                    c.Type = WeaponRangeType.Melee;
                    c.AttackBonus = 0;
                    c.Descriptor = ModifierDescriptor.Rage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default,
                        m_AbilityParameter = AbilityParameterType.Level
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 15, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        SkaldClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<SavingThrowContextBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Paralysis | SpellDescriptor.Sleep;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus,
                        m_AbilityParameter = AbilityParameterType.Level
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 3, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 11, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 15, ProgressionValue = 5 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 19, ProgressionValue = 6 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 7 }
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        SkaldClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateAbilityParams>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Charisma;
                    c.StatTypeFromCustomProperty = false;
                    c.ReplaceCasterLevel = true;
                    c.CasterLevel = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.DamageDice,
                        m_AbilityParameter = AbilityParameterType.Level
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Div2;                    
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        SkaldClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffAcid.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffCold.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffElectricity.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffFire.ToReference<BlueprintBuffReference>()
                        });
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            WyrmSingerBreathWeaponAbilityAcid.AddComponent<AbilityTargetHasFact>(c => {
                c.m_CheckedFacts = new BlueprintUnitFactReference[] { WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintUnitFactReference>() };
                c.Inverted = false;
            });
            WyrmSingerBreathWeaponAbilityCold.AddComponent<AbilityTargetHasFact>(c => {
                c.m_CheckedFacts = new BlueprintUnitFactReference[] { WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintUnitFactReference>() };
                c.Inverted = false;
            });
            WyrmSingerBreathWeaponAbilityElectricity.AddComponent<AbilityTargetHasFact>(c => {
                c.m_CheckedFacts = new BlueprintUnitFactReference[] { WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintUnitFactReference>() };
                c.Inverted = false;
            });
            WyrmSingerBreathWeaponAbilityFire.AddComponent<AbilityTargetHasFact>(c => {
                c.m_CheckedFacts = new BlueprintUnitFactReference[] { WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintUnitFactReference>() };
                c.Inverted = false;
            });
            var WyrmSingerDraconicRageArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("WyrmSingerDraconicRageArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = InspiredRageAllyToggleSwitchBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionIsAlly() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>(),
                                    RemoveRank = false,
                                    ToCaster = false
                                }
                                )
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        }
                        );
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = InspiredRageAllyToggleSwitchBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionIsAlly() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>(),
                                    RemoveRank = false,
                                    ToCaster = false
                                }
                                )
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Ally;
                bp.SpellResistance = false;
                bp.AffectEnemies = false;
                bp.AggroEnemies = false;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 50.Feet();
                bp.Fx = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "948e6476e9d49d0429452ce1db0c224d" };
            });
            var WyrmSingerDraconicRageBuff = Helpers.CreateBuff("WyrmSingerDraconicRageBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = WyrmSingerDraconicRageArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var WyrmSingerDraconicRageAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("WyrmSingerDraconicRageAbility", bp => {
                bp.SetName("Draconic Rage");
                bp.SetDescription("At 1st level, a wyrm singer can kindle an echo of ancient rage felt between warring dragon clans in his allies. This ability acts as inspired rage, except those affected gain " +
                    "a +2 bonus on melee attack and damage rolls and a +2 bonus on saving throws against paralysis and sleep effects (but they still take a –1 penalty to their AC), rather than " +
                    "inspired rage’s normal bonuses. At 4th level and every 4 skald levels thereafter, the song’s bonuses on saves against paralysis and sleep effects increase by 1. At 8th and 16th levels, " +
                    "the song’s bonus on melee attack and damage rolls increases by 1.");
                bp.m_Icon = ConfusionSpell.Icon;
                bp.m_Buff = WyrmSingerDraconicRageBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = RagingSongResource;
                });
                bp.Group = ActivatableAbilityGroup.BardicPerformance;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateAfterFirstRound = false;
                bp.DeactivateImmediately = false;
                bp.IsTargeted = false;
                bp.DeactivateIfOwnerDisabled = true;
                bp.DeactivateIfOwnerUnconscious = true;
                bp.OnlyInCombat = false;
                bp.DoNotTurnOffOnRest = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
            });
            var WyrmSingerDraconicRageFeature = Helpers.CreateBlueprint<BlueprintFeature>("WyrmSingerDraconicRageFeature", bp => {
                bp.SetName("Draconic Rage");
                bp.SetDescription("At 1st level, a wyrm singer can kindle an echo of ancient rage felt between warring dragon clans in his allies. This ability acts as inspired rage, except those affected gain " +
                    "a +2 bonus on melee attack and damage rolls and a +2 bonus on saving throws against paralysis and sleep effects (but they still take a –1 penalty to their AC), rather than " +
                    "inspired rage’s normal bonuses. At 4th level and every 4 skald levels thereafter, the song’s bonuses on saves against paralysis and sleep effects increase by 1. At 8th and 16th levels, " +
                    "the song’s bonus on melee attack and damage rolls increases by 1.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerDraconicRageAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;                
            });

            var WyrmSingerWyrmSagaTargetBuff = Helpers.CreateBuff("WyrmSingerWyrmSagaTargetBuff", bp => {
                bp.SetName("Wyrm Saga Target");
                bp.SetDescription("This ally is selected to take on a draconic aspect (as per form of the dragon I) of a type of the wyrm singer’s choice when under the effect of the Wyrm Saga performance. " +
                    "This ally cannot use the breath weapon attack provided by form of the dragon. The wyrm singer must expend 1 round of raging song each round to maintain wyrm saga, and can affect only a " +
                    "single ally at a time.");
                bp.m_Icon = DazeSpell.Icon;
                bp.AddComponent<UniqueBuff>();
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaEffectBlack = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectBlack", bp => {
                bp.SetName("Wyrm Saga - Black");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1BlackBuff.Icon;
                bp.Components = FormOfTheDragon1BlackBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragon1BlackBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffectBlue = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectBlue", bp => {
                bp.SetName("Wyrm Saga - Blue");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "electricity 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1BlueBuff.Icon;
                bp.Components = FormOfTheDragon1BlueBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragon1BlueBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffectBrass = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectBrass", bp => {
                bp.SetName("Wyrm Saga - Brass");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1BrassBuff.Icon;
                bp.Components = FormOfTheDragon1BrassBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragon1BrassBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffectRed = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectRed", bp => {
                bp.SetName("Wyrm Saga - Red");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1RedBuff.Icon;
                bp.Components = FormOfTheDragon1RedBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragon1RedBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffectGreen = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectGreen", bp => {
                bp.SetName("Wyrm Saga - Green");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "acid 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1GreenBuff.Icon;
                bp.Components = FormOfTheDragon1GreenBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragon1GreenBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffectSilver = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectSilver", bp => {
                bp.SetName("Wyrm Saga - Silver");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1SilverBuff.Icon;
                bp.Components = FormOfTheDragon1SilverBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragon1SilverBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffectWhite = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectWhite", bp => {
                bp.SetName("Wyrm Saga - White");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "cold 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1WhiteBuff.Icon;
                bp.Components = FormOfTheDragon1WhiteBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = FormOfTheDragon1WhiteBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffects = new BlueprintBuff[] {
                        Resources.GetModBlueprint<BlueprintBuff>("WyrmSingerWyrmSagaEffectBlack"),
                        Resources.GetModBlueprint<BlueprintBuff>("WyrmSingerWyrmSagaEffectBlue"),
                        Resources.GetModBlueprint<BlueprintBuff>("WyrmSingerWyrmSagaEffectBrass"),
                        Resources.GetModBlueprint<BlueprintBuff>("WyrmSingerWyrmSagaEffectRed"),
                        Resources.GetModBlueprint<BlueprintBuff>("WyrmSingerWyrmSagaEffectGreen"),
                        Resources.GetModBlueprint<BlueprintBuff>("WyrmSingerWyrmSagaEffectSilver"),
                        Resources.GetModBlueprint<BlueprintBuff>("WyrmSingerWyrmSagaEffectWhite")
                    };
            foreach (var WyrmSingerWyrmSagaEffect in WyrmSingerWyrmSagaEffects) {
                WyrmSingerWyrmSagaEffect.RemoveComponents<AddFactContextActions>();
                WyrmSingerWyrmSagaEffect.RemoveComponents<ReplaceAbilityParamsWithContext>();
            }
            var WyrmSingerWyrmSagaFlagBlack = Helpers.CreateBuff("WyrmSingerWyrmSagaFlagBlack", bp => {
                bp.SetName("Wyrm Saga Selection - Black");
                bp.SetDescription("When performing the wyrm saga raging song, the selected ally will be effected by the black dragon variant of form of the dragon I.");
                bp.m_Icon = FormOfTheDragon1BlackBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaFlagBlue = Helpers.CreateBuff("WyrmSingerWyrmSagaFlagBlue", bp => {
                bp.SetName("Wyrm Saga Selection - Blue");
                bp.SetDescription("When performing the wyrm saga raging song, the selected ally will be effected by the blue dragon variant of form of the dragon I.");
                bp.m_Icon = FormOfTheDragon1BlueBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaFlagBrass = Helpers.CreateBuff("WyrmSingerWyrmSagaFlagBrass", bp => {
                bp.SetName("Wyrm Saga Selection - Brass");
                bp.SetDescription("When performing the wyrm saga raging song, the selected ally will be effected by the brass dragon variant of form of the dragon I.");
                bp.m_Icon = FormOfTheDragon1BrassBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaFlagRed = Helpers.CreateBuff("WyrmSingerWyrmSagaFlagRed", bp => {
                bp.SetName("Wyrm Saga Selection - Red");
                bp.SetDescription("When performing the wyrm saga raging song, the selected ally will be effected by the red dragon variant of form of the dragon I.");
                bp.m_Icon = FormOfTheDragon1RedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaFlagGreen = Helpers.CreateBuff("WyrmSingerWyrmSagaFlagGreen", bp => {
                bp.SetName("Wyrm Saga Selection - Green");
                bp.SetDescription("When performing the wyrm saga raging song, the selected ally will be effected by the green dragon variant of form of the dragon I.");
                bp.m_Icon = FormOfTheDragon1GreenBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaFlagSilver = Helpers.CreateBuff("WyrmSingerWyrmSagaFlagSilver", bp => {
                bp.SetName("Wyrm Saga Selection - Silver");
                bp.SetDescription("When performing the wyrm saga raging song, the selected ally will be effected by the silver dragon variant of form of the dragon I.");
                bp.m_Icon = FormOfTheDragon1SilverBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaFlagWhite = Helpers.CreateBuff("WyrmSingerWyrmSagaFlagWhite", bp => {
                bp.SetName("Wyrm Saga Selection - White");
                bp.SetDescription("When performing the wyrm saga raging song, the selected ally will be effected by the white dragon variant of form of the dragon I.");
                bp.m_Icon = FormOfTheDragon1WhiteBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });
            var WyrmSingerWyrmSagaTargetSelectorAbility = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbility", bp => {
                bp.SetName("Wyrm Saga Selector");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, along with which variant of form of the dragon I the saga will imbue them with.");
                //variants added after
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerWyrmSagaTargetSelectorAbilityBlack = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbilityBlack", bp => {
                bp.SetName("Wyrm Saga Selector - Black");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, gaining the effect of the black dragon variant of form of the dragon I " +
                    "while able to hear the raging song.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true,
                                    }
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintBuffReference>()
                                }
                                )
                        });
                });
                bp.m_Parent = WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerWyrmSagaTargetSelectorAbilityBlue = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbilityBlue", bp => {
                bp.SetName("Wyrm Saga Selector - Blue");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, gaining the effect of the blue dragon variant of form of the dragon I " +
                    "while able to hear the raging song.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true,
                                    }
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintBuffReference>()
                                }
                                )
                        });
                });
                bp.m_Parent = WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerWyrmSagaTargetSelectorAbilityBrass = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbilityBrass", bp => {
                bp.SetName("Wyrm Saga Selector - Brass");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, gaining the effect of the brass dragon variant of form of the dragon I " +
                    "while able to hear the raging song.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true,
                                    }
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintBuffReference>()
                                }
                                )
                        });
                });
                bp.m_Parent = WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerWyrmSagaTargetSelectorAbilityRed = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbilityRed", bp => {
                bp.SetName("Wyrm Saga Selector - Red");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, gaining the effect of the red dragon variant of form of the dragon I " +
                    "while able to hear the raging song.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true,
                                    }
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintBuffReference>()
                                }
                                )
                        });
                });
                bp.m_Parent = WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerWyrmSagaTargetSelectorAbilityGreen = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbilityGreen", bp => {
                bp.SetName("Wyrm Saga Selector - Green");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, gaining the effect of the green dragon variant of form of the dragon I " +
                    "while able to hear the raging song.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true,
                                    }
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintBuffReference>()
                                }
                                )
                        });
                });
                bp.m_Parent = WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerWyrmSagaTargetSelectorAbilitySilver = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbilitySilver", bp => {
                bp.SetName("Wyrm Saga Selector - Silver");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, gaining the effect of the silver dragon variant of form of the dragon I " +
                    "while able to hear the raging song.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true,
                                    }
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintBuffReference>()
                                }
                                )
                        });
                });
                bp.m_Parent = WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WyrmSingerWyrmSagaTargetSelectorAbilityWhite = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerWyrmSagaTargetSelectorAbilityWhite", bp => {
                bp.SetName("Wyrm Saga Selector - White");
                bp.SetDescription("Choose the ally to be affected by wyrm saga, gaining the effect of the white dragon variant of form of the dragon I " +
                    "while able to hear the raging song.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true,
                                    }
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintBuffReference>()
                                },
                                new ContextActionRemoveBuff() {
                                    m_Buff = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintBuffReference>()
                                }
                                )
                        });
                });
                bp.m_Parent = WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            WyrmSingerWyrmSagaTargetSelectorAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WyrmSingerWyrmSagaTargetSelectorAbilityBlack.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerWyrmSagaTargetSelectorAbilityBlue.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerWyrmSagaTargetSelectorAbilityBrass.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerWyrmSagaTargetSelectorAbilityRed.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerWyrmSagaTargetSelectorAbilityGreen.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerWyrmSagaTargetSelectorAbilitySilver.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerWyrmSagaTargetSelectorAbilityWhite.ToReference<BlueprintAbilityReference>()
                };
            });
            var WyrmSingerWyrmSagaArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("WyrmSingerWyrmSagaArea", bp => { //Need to load up all the conditionals
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaFlagBlack.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaEffectBlack.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaFlagBlue.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaEffectBlue.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaFlagBrass.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaEffectBrass.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaFlagRed.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaEffectRed.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaFlagGreen.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaEffectGreen.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaFlagSilver.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaEffectSilver.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaFlagWhite.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WyrmSingerWyrmSagaEffectWhite.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerWyrmSagaEffectBlack.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerWyrmSagaEffectBlue.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerWyrmSagaEffectBrass.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerWyrmSagaEffectRed.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerWyrmSagaEffectGreen.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerWyrmSagaEffectSilver.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerWyrmSagaEffectWhite.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        }
                        );
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList();
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Ally;
                bp.SpellResistance = false;
                bp.AffectEnemies = false;
                bp.AggroEnemies = false;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 50.Feet();
                bp.Fx = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "529ab6c69cefa6a4bb1e542b68ce5ad9" };
            });
            var WyrmSingerWyrmSagaBuff = Helpers.CreateBuff("WyrmSingerWyrmSagaBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = WyrmSingerWyrmSagaArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var WyrmSingerWyrmSagaAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("WyrmSingerWyrmSagaAbility", bp => {
                bp.SetName("Wyrm Saga");
                bp.SetDescription("At 14th level, a wyrm singer embraces the essence of the draconic histories, allowing his allies to manifest aspects of a dragon in their physical forms. The wyrm singer " +
                    "selects a single ally within 50 feet to take on a draconic aspect (as per form of the dragon I) of a type of the wyrm singer’s choice. The ally cannot use the breath weapon attack " +
                    "provided by form of the dragon. The wyrm singer must expend 1 round of raging song each round to maintain wyrm saga, and can affect only a single ally at a time.");
                bp.m_Icon = ConfusionSpell.Icon;
                bp.m_Buff = WyrmSingerWyrmSagaBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = RagingSongResource;
                });
                bp.Group = ActivatableAbilityGroup.BardicPerformance;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateAfterFirstRound = false;
                bp.DeactivateImmediately = false;
                bp.IsTargeted = false;
                bp.DeactivateIfOwnerDisabled = true;
                bp.DeactivateIfOwnerUnconscious = true;
                bp.OnlyInCombat = false;
                bp.DoNotTurnOffOnRest = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
            });
            var WyrmSingerWyrmSagaFeature = Helpers.CreateBlueprint<BlueprintFeature>("WyrmSingerWyrmSagaFeature", bp => {
                bp.SetName("Wyrm Saga");
                bp.SetDescription("At 14th level, a wyrm singer embraces the essence of the draconic histories, allowing his allies to manifest aspects of a dragon in their physical forms. The wyrm singer " +
                    "selects a single ally within 50 feet to take on a draconic aspect (as per form of the dragon I) of a type of the wyrm singer’s choice. The ally cannot use the breath weapon attack " +
                    "provided by form of the dragon. The wyrm singer must expend 1 round of raging song each round to maintain wyrm saga, and can affect only a single ally at a time.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        WyrmSingerWyrmSagaAbility.ToReference<BlueprintUnitFactReference>(),
                        WyrmSingerWyrmSagaTargetSelectorAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            WyrmSingerArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, InspiredRageFeature),
                    Helpers.LevelEntry(12, SkaldRagePowerSelection),
                    Helpers.LevelEntry(14, SongOfTheFallenFeature)
            };
            WyrmSingerArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, WyrmSingerDraconicRageFeature),
                    Helpers.LevelEntry(12, WyrmSingerBreathWeaponFeature),
                    Helpers.LevelEntry(14, WyrmSingerWyrmSagaFeature)
            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Wyrm Singer")) { return; }
            SkaldClass.m_Archetypes = SkaldClass.m_Archetypes.AppendToArray(WyrmSingerArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
