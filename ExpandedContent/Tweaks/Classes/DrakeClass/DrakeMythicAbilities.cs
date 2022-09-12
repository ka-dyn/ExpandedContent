using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.DrakeClass {
    internal class DrakeMythicAbilities {
        public static void AddDrakeMythicAbilities() {

            var DrakeCompanionClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DrakeCompanionClass");
            var MythicalBeastMaster = Resources.GetBlueprint<BlueprintFeature>("89096871a6fdadd43ad31f5046696727");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var WingLarge1n6 = Resources.GetBlueprint<BlueprintItemWeapon>("06c210c77c30a36478cb8dbf225fb364");
            var DrakeBreathCooldown = Resources.GetModBlueprint<BlueprintBuff>("DrakeBreathCooldown");
            var DrakeBreathAbilityResource = Resources.GetModBlueprint<BlueprintAbilityResource>("DrakeBreathAbilityResource");
            var BloodlineDraconicRedBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("3f31704e595e78942b3640cdc9b95d8b");
            var BloodlineDraconicWhiteBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("84be529914c90664aa948d8266bb3fa6");
            var BloodlineDraconicBlueBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("60a3047f434f38544a2878c26955d3ad");
            var BloodlineDraconicBlackBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("1e65b0b2db777e24db96d8bc52cc9207");
            var FireCone50Feet00 = Resources.GetBlueprint<BlueprintProjectile>("4c272644b29989a40bcf1e6003cfe708");
            var ColdCone50Feet00 = Resources.GetBlueprint<BlueprintProjectile>("79a66a3766ae87146beb6000a73e8213");
            var SonicCone40Feet00 = Resources.GetBlueprint<BlueprintProjectile>("f899d93a411796b4685afc000c3466b0");
            var AcidCone50Feet00 = Resources.GetBlueprint<BlueprintProjectile>("214036a0c1b35464780ad140324c249c");
            var DrakeBreathWeaponFire2Feature = Resources.GetModBlueprint<BlueprintFeature>("DrakeBreathWeaponFire2Feature");
            var DrakeBreathWeaponCold2Feature = Resources.GetModBlueprint<BlueprintFeature>("DrakeBreathWeaponCold2Feature");
            var DrakeBreathWeaponElectricity2Feature = Resources.GetModBlueprint<BlueprintFeature>("DrakeBreathWeaponElectricity2Feature");
            var DrakeBreathWeaponAcid2Feature = Resources.GetModBlueprint<BlueprintFeature>("DrakeBreathWeaponAcid2Feature");
            var BloodlineSilverDraconicProgression = Resources.GetBlueprint<BlueprintProgression>("c7d2f393e6574874bb3fc728a69cc73a");
            var DrakenClawsFeature = Resources.GetModBlueprint<BlueprintFeature>("DrakenClawsFeature");
            var Multiattack = Resources.GetBlueprint<BlueprintFeature>("8ac319e47057e2741b42229210eb43ed");
            //Spell Support
            var DrakeKeenMindFeature = Resources.GetModBlueprint<BlueprintFeature>("DrakeKeenMindFeature");
            var DrakeBloodGreen = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodGreen");
            var DrakeBloodGreenSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodGreenSpelllist");
            var DrakeBloodSilver = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodSilver");
            var DrakeBloodSilverSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodSilverSpelllist");
            var DrakeBloodBlack = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBlack");
            var DrakeBloodBlackSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBlackSpelllist");
            var DrakeBloodBlue = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBlue");
            var DrakeBloodBlueSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBlueSpelllist");
            var DrakeBloodBrass = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBrass");
            var DrakeBloodBrassSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBrassSpelllist");
            var DrakeBloodRed = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodRed");
            var DrakeBloodRedSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodRedSpelllist");
            var DrakeBloodWhite = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodWhite");
            var DrakeBloodWhiteSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodWhiteSpelllist");
            var DrakeBloodGold = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodGold");
            var DrakeBloodGoldSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodGoldSpelllist");
            var DrakeBloodBronze = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBronze");
            var DrakeBloodBronzeSpelllist = Resources.GetModBlueprint<BlueprintFeature>("DrakeBloodBronzeSpelllist");


            var DrakeBreathWeaponFire3 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponFire3", bp => {
                bp.SetName("Mythic Drake Fire Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} per drake companion level in a 50-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireCone50Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 50 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
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
                                        DiceType = DiceType.D8,
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
                                }),
                            IfFalse = Helpers.CreateActionList()
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
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
            var DrakeBreathWeaponFire3Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponFire3Feature", bp => {
                bp.SetName("Mythic Drake Fire Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} per drake companion level in a 50-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponFire3.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponFire3.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponFire2Feature.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeBreathWeaponFire2 = Resources.GetModBlueprint<BlueprintAbility>("DrakeBreathWeaponFire2");
            DrakeBreathWeaponFire2.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                DrakeBreathWeaponFire3Feature.ToReference<BlueprintUnitFactReference>()
            };
            var DrakeBreathWeaponCold3 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponCold3", bp => {
                bp.SetName("Mythic Drake Cold Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} per drake companion level in a 50-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdCone50Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 50 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
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
                                        DiceType = DiceType.D8,
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
                                }),
                            IfFalse = Helpers.CreateActionList()
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
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
            var DrakeBreathWeaponCold3Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponCold3Feature", bp => {
                bp.SetName("Mythic Drake Cold Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} per drake companion level in a 50-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponCold3.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponCold3.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponCold2Feature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeBreathWeaponCold2 = Resources.GetModBlueprint<BlueprintAbility>("DrakeBreathWeaponCold2");
            DrakeBreathWeaponCold2.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                DrakeBreathWeaponCold3Feature.ToReference<BlueprintUnitFactReference>()
            };
            var DrakeBreathWeaponElectricity3 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponElectricity3", bp => {
                bp.SetName("Mythic Drake Electricity Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} per drake companion level in a 40-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        SonicCone40Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 40 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
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
                                        DiceType = DiceType.D8,
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
                                }),
                            IfFalse = Helpers.CreateActionList()
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
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
            var DrakeBreathWeaponElectricity3Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponElectricity3Feature", bp => {
                bp.SetName("Mythic Drake Electricity Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} per drake companion level in a 40-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponElectricity3.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponElectricity3.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponElectricity2Feature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeBreathWeaponElectricity2 = Resources.GetModBlueprint<BlueprintAbility>("DrakeBreathWeaponElectricity2");
            DrakeBreathWeaponElectricity2.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                DrakeBreathWeaponElectricity3Feature.ToReference<BlueprintUnitFactReference>()
            };
            var DrakeBreathWeaponAcid3 = Helpers.CreateBlueprint<BlueprintAbility>("DrakeBreathWeaponAcid3", bp => {
                bp.SetName("Mythic Drake Acid Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} per drake companion level in a 50-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidCone50Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 50 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
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
                                        DiceType = DiceType.D8,
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
                                }),
                            IfFalse = Helpers.CreateActionList()
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathCooldown.ToReference<BlueprintUnitFactReference>() };
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
            var DrakeBreathWeaponAcid3Feature = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBreathWeaponAcid3Feature", bp => {
                bp.SetName("Mythic Drake Acid Breath");
                bp.SetDescription("This breath weapon deals {g|Encyclopedia:Dice}1d8{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} per drake companion level in a 50-foot cone. " +
                    "Those caught in the area of the breath receive a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. " +
                    "The {g|Encyclopedia:DC}DC{/g} of this save is equal to 10 + 1/2 your drake companion level + your {g|Encyclopedia:Constitution}Constitution{/g} modifier." +
                    "After use this ability takes {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. You can use this 5 times a day. \nUnlocking this " +
                    "allows you to use your greater breath weapon without spending any resources, however you must wait {g|Encyclopedia:Dice}1d4{/g} rounds to recharge. " +
                    "\nUnlike non-mythic breath attacks, this does not damage allies.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeBreathWeaponAcid3.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DrakeBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DrakeBreathWeaponAcid3.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeBreathWeaponAcid2Feature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var DrakeBreathWeaponAcid2 = Resources.GetModBlueprint<BlueprintAbility>("DrakeBreathWeaponAcid2");
            DrakeBreathWeaponAcid2.GetComponent<AbilityResourceLogic>().ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                DrakeBreathWeaponAcid3Feature.ToReference<BlueprintUnitFactReference>()
            };

            
            var DraconicBodyBuff = Helpers.CreateBuff("DraconicBodyBuff", bp => {
                bp.SetName("Draconic Body");
                bp.SetDescription("The drake gains two natural claw attacks that deal {g|Encyclopedia:Dice}1d2{/g} damage from a tiny drake, but increase " +
                    "damage dice size each time the drake grows");                
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = WingLarge1n6.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = WingLarge1n6.ToReference<BlueprintItemWeaponReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.IsClassFeature = true;
            });
            var DraconicBodyFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicBodyFeature", bp => {
                bp.SetName("Draconic Body");
                bp.SetDescription("The drake gains two natural Wing attacks that increase damage dice size each time the drake grows, it also gains the Multiattack Feat");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        DraconicBodyBuff.ToReference<BlueprintUnitFactReference>(),
                        Multiattack.ToReference<BlueprintUnitFactReference>() 
                    };
                });
                bp.IsClassFeature = true;
            });
            var DraconicSoulFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicSoulFeature", bp => {
                bp.SetName("Draconic Soul");
                bp.SetDescription("The drake gains a Charisma bonus equal to its level minus 5, along with a limited spellbook as its draconic bloodline awakens. " +
                    "\nSpells granted this way are spontaneously cast and scale using Charisma. \nSpells granted are dependent on which type of dragon the drake is descended from.");
                bp.IsClassFeature = true;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Charisma;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });                
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.BonusValue;
                    c.m_StepLevel = -5;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>()                        
                    };
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodGreen.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodGreenSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodSilver.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodSilverSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodBlack.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodBlackSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodBlue.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodBlueSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodBrass.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodBrassSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodRed.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodRedSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodWhite.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodWhiteSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodGold.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodGoldSpelllist.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBloodBronze.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBloodBronzeSpelllist.ToReference<BlueprintUnitFactReference>();
                });
            });
            var MythicalDrakePet = Helpers.CreateBlueprint<BlueprintFeature>("MythicalDrakePet", bp => {
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBreathWeaponFire2Feature.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBreathWeaponFire3Feature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBreathWeaponCold2Feature.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBreathWeaponCold3Feature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBreathWeaponElectricity2Feature.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBreathWeaponElectricity3Feature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeBreathWeaponAcid2Feature.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DrakeBreathWeaponAcid3Feature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakenClawsFeature.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DraconicBodyFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = DrakeKeenMindFeature.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DraconicSoulFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.ReapplyOnLevelUp = true;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var MythicalDrakeMaster = Helpers.CreateBlueprint<BlueprintFeature>("MythicalDrakeMaster", bp => {
                bp.SetName("Mythical Drake");
                bp.SetDescription("Your mythic powers unlock the latent draconic might in your drake companion, granting it new abilities based off its own Drake Power choices." +
                    "\nIf your drake has a greater breath weapon it gains access to the mythic variant, dealing {g|Encyclopedia:Dice}1d8{/g} damage per class level. It also deals no damage to allies. " +
                    "\nIf your drake has the draken claws ability it gains 2 natural wing attacks and the Multiattack feat." +
                    "\nIf your drake has the keen mind ability it gains a Charisma bonus equal to its level minus 5, along with a limited spellbook. " +
                    "Spells granted this way are spontaneously cast and scale using Charisma. Spells granted are dependent on which type of dragon the drake is descended from.");
                bp.m_Icon = BloodlineSilverDraconicProgression.m_Icon;
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = MythicalBeastMaster.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = DrakeCompanionSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureToPet>(c => {
                    c.m_Feature = MythicalDrakePet.ToReference<BlueprintFeatureReference>();
                });
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;

            });
            FeatTools.AddAsMythicAbility(MythicalDrakeMaster);
        }
    }
}
