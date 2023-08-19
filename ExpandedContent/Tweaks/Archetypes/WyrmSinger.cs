using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
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
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Linq;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.Settings;
using Kingmaker.ResourceLinks;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class WyrmSinger {
        public static void AddWyrmSinger() {

            var SkaldClass = Resources.GetBlueprint<BlueprintCharacterClass>("6afa347d804838b48bda16acb0573dc0");
            var RagingSongResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("4a2302c4ec2cfb042bba67d825babfec");
            var InspiredRageFeature = Resources.GetBlueprint<BlueprintFeature>("1a639eadc2c3ed546bc4bb236864cd0c");
            var SkaldRagePowerSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2476514e31791394fa140f1a07941c96");
            var SongOfTheFallenFeature = Resources.GetBlueprint<BlueprintFeature>("9fc5d126524dbc84a90b1856707e2d87");
            var BloodragerBloodlineSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("62b33ac8ceb18dd47ad4c8f06849bc01");
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
            var ShamanflameSpiritAbility = Resources.GetBlueprint<BlueprintAbility>("457cc54f39b8d2c4cad5499ec88a19d2");
            var FormOfTheDragon1BrassAbility = Resources.GetBlueprint<BlueprintAbility>("2271bc6960317164aa61363ebe7c0228");
            var FormOfTheDragonII = Resources.GetBlueprint<BlueprintAbility>("666556ded3a32f34885e8c318c3a0ced");
            var BloodlineDraconicRedBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("3f31704e595e78942b3640cdc9b95d8b");
            var BloodlineDraconicWhiteBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("84be529914c90664aa948d8266bb3fa6");
            var BloodlineDraconicBlueBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("60a3047f434f38544a2878c26955d3ad");
            var BloodlineDraconicBlackBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("1e65b0b2db777e24db96d8bc52cc9207");
            var InspiredRageBuff = Resources.GetBlueprint<BlueprintBuff>("75b3978757908d24aaaecaf2dc209b89");

            var WyrmSagaIcon = AssetLoader.LoadInternal("Skills", "Icon_WyrmSaga.jpg");

            var WyrmSingerArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("WyrmSingerArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"WyrmSingerArchetype.Name", "Wyrm Singer");
                bp.LocalizedDescription = Helpers.CreateString($"WyrmSingerArchetype.Description", "Wyrm singers spin fragments of the story of the ongoing struggle between noble Apsu and wicked Dahak.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"WyrmSingerArchetype.Description", "Wyrm singers spin fragments of the story of the ongoing struggle between noble Apsu and wicked Dahak.");                
            });

            //Change forbidden casting on rage to work with all Skald archetypes
            var InspiredRageEffectBuffBeforeMasterSkaldForbidCasting = Resources.GetBlueprint<BlueprintBuff>("345d36cd45f5614409824209f26d0130").GetComponent<ForbidSpellCasting>();
            var SkaldCantripsFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("aea30d43abf590142a750e72db08df7b");
            InspiredRageEffectBuffBeforeMasterSkaldForbidCasting.m_IgnoreFeature = SkaldCantripsFeature;




            var WyrmSingerBreathWeaponBuffAcid = Helpers.CreateBuff("WyrmSingerBreathWeaponBuffAcid", bp => {
                bp.SetName("Grant Breath Weapon - Acid");
                bp.SetDescription("A wyrm singer can grant a breath weapon attack to himself or an ally affected by his draconic rage raging song. Using the breath weapon is a standard " +
                    "action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of acid damage per 2 character levels. Creatures caught " +
                    "in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage. This ability is lost either when used, or " +
                    "when no longer effected by the wyrm singers draconic rage raging song.");
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
                //Components added later
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var WyrmSingerBreathWeaponBuffCold = Helpers.CreateBuff("WyrmSingerBreathWeaponBuffCold", bp => {
                bp.SetName("Grant Breath Weapon - Cold");
                bp.SetDescription("A wyrm singer can grant a breath weapon attack to himself or an ally affected by his draconic rage raging song. Using the breath weapon is a standard " +
                    "action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of cold damage per 2 character levels. Creatures caught " +
                    "in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage. This ability is lost either when used, or " +
                    "when no longer effected by the wyrm singers draconic rage raging song.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                //Components added later
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var WyrmSingerBreathWeaponBuffElectricity = Helpers.CreateBuff("WyrmSingerBreathWeaponBuffElectricity", bp => {
                bp.SetName("Grant Breath Weapon - Electricity");
                bp.SetDescription("A wyrm singer can grant a breath weapon attack to himself or an ally affected by his draconic rage raging song. Using the breath weapon is a standard " +
                    "action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of electricity damage per 2 character levels. Creatures caught " +
                    "in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage. This ability is lost either when used, or " +
                    "when no longer effected by the wyrm singers draconic rage raging song.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                //Components added later
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var WyrmSingerBreathWeaponBuffFire = Helpers.CreateBuff("WyrmSingerBreathWeaponBuffFire", bp => {
                bp.SetName("Grant Breath Weapon - Fire");
                bp.SetDescription("A wyrm singer can grant a breath weapon attack to himself or an ally affected by his draconic rage raging song. Using the breath weapon is a standard " +
                    "action, and it affects creatures in a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of fire damage per 2 character levels. Creatures caught " +
                    "in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage. This ability is lost either when used, or " +
                    "when no longer effected by the wyrm singers draconic rage raging song.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                //Components added later
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var WyrmSingerBreathWeaponBase = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponBaseAcid", bp => {
                bp.SetName("Wyrm Singers Acid Breath");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of elemental damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.m_Icon = ShamanflameSpiritAbility.m_Icon;
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffAcid.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.m_Icon;
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffAcid.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
            var WyrmSingerBreathWeaponConeCold = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponConeCold", bp => {
                bp.SetName("Wyrm Singers Cold Breath - Cone");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of cold damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
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
                                    m_Buff = WyrmSingerBreathWeaponBuffCold.ToReference<BlueprintBuffReference>()
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffCold.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
            var WyrmSingerBreathWeaponLineCold = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponLineCold", bp => {
                bp.SetName("Wyrm Singers Cold Breath - Line");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of cold damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdLine00.ToReference<BlueprintProjectileReference>()
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
                                    m_Buff = WyrmSingerBreathWeaponBuffCold.ToReference<BlueprintBuffReference>()
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffCold.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
            var WyrmSingerBreathWeaponConeElectricity = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponConeElectricity", bp => {
                bp.SetName("Wyrm Singers Electricity Breath - Cone");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of electricity damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
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
                                    m_Buff = WyrmSingerBreathWeaponBuffElectricity.ToReference<BlueprintBuffReference>()
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffElectricity.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
            var WyrmSingerBreathWeaponLineElectricity = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponLineElectricity", bp => {
                bp.SetName("Wyrm Singers Electricity Breath - Line");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of electricity damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        LightningBolt00.ToReference<BlueprintProjectileReference>()
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
                                    m_Buff = WyrmSingerBreathWeaponBuffElectricity.ToReference<BlueprintBuffReference>()
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffElectricity.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
            var WyrmSingerBreathWeaponConeFire = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponConeFire", bp => {
                bp.SetName("Wyrm Singers Fire Breath - Cone");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of fire damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
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
                                    m_Buff = WyrmSingerBreathWeaponBuffFire.ToReference<BlueprintBuffReference>()
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffFire.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
            var WyrmSingerBreathWeaponLineFire = Helpers.CreateBlueprint<BlueprintAbility>("WyrmSingerBreathWeaponLineFire", bp => {
                bp.SetName("Wyrm Singers Fire Breath - Line");
                bp.SetDescription("Your breath weapon may be either a 30-foot cone or a 60-foot line. The breath weapon deals 1d6 points of fire damage per 2 character levels. " +
                    "Creatures caught in the area can attempt a Reflex save (DC = 10 + 1/2 the characters level + their Constiution modifier) to halve the damage.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireLine00.ToReference<BlueprintProjectileReference>()
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
                                    m_Buff = WyrmSingerBreathWeaponBuffFire.ToReference<BlueprintBuffReference>()
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
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerBreathWeaponBuffFire.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = WyrmSingerBreathWeaponBase.ToReference<BlueprintAbilityReference>();
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
            WyrmSingerBreathWeaponBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WyrmSingerBreathWeaponConeAcid.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponLineAcid.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponConeCold.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponLineCold.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponConeElectricity.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponLineElectricity.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponConeFire.ToReference<BlueprintAbilityReference>(),
                    WyrmSingerBreathWeaponLineFire.ToReference<BlueprintAbilityReference>()
                };
            });
            WyrmSingerBreathWeaponBuffAcid.TemporaryContext(bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WyrmSingerBreathWeaponBase.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        WyrmSingerBreathWeaponConeAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeFire.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineFire.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
            });
            WyrmSingerBreathWeaponBuffCold.TemporaryContext(bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WyrmSingerBreathWeaponBase.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        WyrmSingerBreathWeaponConeAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeFire.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineFire.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
            });
            WyrmSingerBreathWeaponBuffElectricity.TemporaryContext(bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WyrmSingerBreathWeaponBase.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        WyrmSingerBreathWeaponConeAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeFire.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineFire.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
            });
            WyrmSingerBreathWeaponBuffFire.TemporaryContext(bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WyrmSingerBreathWeaponBase.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        WyrmSingerBreathWeaponConeAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineAcid.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineCold.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineElectricity.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponConeFire.ToReference<BlueprintAbilityReference>(),
                        WyrmSingerBreathWeaponLineFire.ToReference<BlueprintAbilityReference>()
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
                bp.m_Icon = ShamanflameSpiritAbility.Icon;
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
                bp.ActionType = UnitCommand.CommandType.Swift;
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
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
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
                        }
                        );                    
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = BloodlineDraconicBlackBreathWeaponAbility.Icon;
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
                bp.ActionType = UnitCommand.CommandType.Swift;
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
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
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
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.Icon;
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
                bp.ActionType = UnitCommand.CommandType.Swift;
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
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
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
                        }
                        );                    
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.Icon;
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
                bp.ActionType = UnitCommand.CommandType.Swift;
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
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
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
                        }
                        );
                    
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WyrmSingerBreathWeaponResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WyrmSingerBreathWeaponAbility.ToReference<BlueprintAbilityReference>();
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.Icon;
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
                bp.ActionType = UnitCommand.CommandType.Swift;
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
                bp.m_Icon = ShamanflameSpiritAbility.Icon;
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
            var WyrmSingerDraconicRageEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("WyrmSingerDraconicRageEffectBuff", bp => {
                bp.SetName("Draconic Rage");
                bp.SetDescription("At 1st level, a wyrm singer can kindle an echo of ancient rage felt between warring dragon clans in his allies. This ability acts as inspired rage, except those affected gain " +
                    "a +2 bonus on melee attack and damage rolls and a +2 bonus on saving throws against paralysis and sleep effects (but they still take a –1 penalty to their AC), rather than " +
                    "inspired rage’s normal bonuses. At 4th level and every 4 skald levels thereafter, the song’s bonuses on saves against paralysis and sleep effects increase by 1. At 8th and 16th levels, " +
                    "the song’s bonus on melee attack and damage rolls increases by 1.");
                bp.m_Icon = BloodragerBloodlineSelection.Icon;                
                bp.AddComponent(InspiredRageAddFactsFromCaster);
                bp.AddComponents(InspiredRageAddFactContextActions);
                bp.AddComponent<WeaponAttackTypeDamageBonus>(c => {
                    c.Type = WeaponRangeType.Melee;
                    c.AttackBonus = 1;
                    c.Descriptor = ModifierDescriptor.Rage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default,
                        m_AbilityParameter = AbilityParameterType.Level
                    };
                });
                bp.AddComponent<AttackTypeAttackBonus>(c => {
                    c.Type = WeaponRangeType.Melee;
                    c.AllTypesExcept = false;
                    c.AttackBonus = 1;
                    c.Descriptor = ModifierDescriptor.Rage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default,
                        m_AbilityParameter = AbilityParameterType.Level
                    };
                    c.CheckFact = false;
                    c.m_RequiredFact = null;
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
                    c.ReplaceSpellLevel = false;
                    c.SpellLevel = 0;
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
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Ignore;
                bp.TickEachSecond = false;
                bp.Frequency = DurationRate.Rounds;
                bp.FxOnStart = new PrefabLink();
                bp.FxOnRemove = new PrefabLink();
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
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffAcid.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffCold.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffElectricity.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = WyrmSingerBreathWeaponBuffFire.ToReference<BlueprintBuffReference>(),
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
                bp.CanBeUsedInTacticalCombat = false;
            });
            var WyrmSingerDraconicRageBuff = Helpers.CreateBuff("WyrmSingerDraconicRageBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = WyrmSingerDraconicRageArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.TickEachSecond = false;
            });
            var WyrmSingerDraconicRageAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("WyrmSingerDraconicRageAbility", bp => {
                bp.SetName("Draconic Rage");
                bp.SetDescription("At 1st level, a wyrm singer can kindle an echo of ancient rage felt between warring dragon clans in his allies. This ability acts as inspired rage, except those affected gain " +
                    "a +2 bonus on melee attack and damage rolls and a +2 bonus on saving throws against paralysis and sleep effects (but they still take a –1 penalty to their AC), rather than " +
                    "inspired rage’s normal bonuses. At 4th level and every 4 skald levels thereafter, the song’s bonuses on saves against paralysis and sleep effects increase by 1. At 8th and 16th levels, " +
                    "the song’s bonus on melee attack and damage rolls increases by 1.");
                bp.m_Icon = BloodragerBloodlineSelection.Icon;
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
                //bp.m_Icon = BloodragerBloodlineSelection.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WyrmSingerDraconicRageAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;                
            });

            #region Rage Plugins
            var AllyRageAddFactContextActions = InspiredRageAllyToggleSwitchBuff.GetComponent<AddFactContextActions>();
            AllyRageAddFactContextActions.Deactivated.Actions = AllyRageAddFactContextActions.Deactivated.Actions.AppendToArray(
                new ContextActionRemoveBuff() { m_Buff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>() }
                );

            var LethalStanceSwitchBuff = Resources.GetBlueprint<BlueprintBuff>("16649b2e80602eb48bbeaad77f9f365f").GetComponent<AddFactContextActions>();
            LethalStanceSwitchBuff.Activated.Actions.OfType<Conditional>().FirstOrDefault().ConditionsChecker.Conditions =
                    LethalStanceSwitchBuff.Activated.Actions.OfType<Conditional>().FirstOrDefault().ConditionsChecker.Conditions.AppendToArray(
                        new ContextConditionHasFact() {
                            Not = false,
                            m_Fact = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintUnitFactReference>()
                        }
                        );
            var GuardedStanceSwitchBuff = Resources.GetBlueprint<BlueprintBuff>("fd0fb6aef4000a443bdc45363410e377").GetComponent<AddFactContextActions>();
            GuardedStanceSwitchBuff.Activated.Actions.OfType<Conditional>().FirstOrDefault().ConditionsChecker.Conditions =
                    GuardedStanceSwitchBuff.Activated.Actions.OfType<Conditional>().FirstOrDefault().ConditionsChecker.Conditions.AppendToArray(
                        new ContextConditionHasFact() {
                            Not = false,
                            m_Fact = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintUnitFactReference>()
                        }
                        );
            var RecklessStanceSwitchBuff = Resources.GetBlueprint<BlueprintBuff>("c52e4fdad5df5d047b7ab077a9907937").GetComponent<AddFactContextActions>();
            RecklessStanceSwitchBuff.Activated.Actions.OfType<Conditional>().FirstOrDefault().ConditionsChecker.Conditions =
                    RecklessStanceSwitchBuff.Activated.Actions.OfType<Conditional>().FirstOrDefault().ConditionsChecker.Conditions.AppendToArray(
                        new ContextConditionHasFact() {
                            Not = false,
                            m_Fact = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintUnitFactReference>()
                        }
                        );

            var PowerfulStanceSwitchBuff = Resources.GetBlueprint<BlueprintBuff>("539e480bcfe6d6f48bdd90418240b50f");
            var PowerfulStanceEffectBuff = Resources.GetBlueprint<BlueprintBuff>("aabad91034e5c7943986fe3e83bfc78e");
            PowerfulStanceSwitchBuff.AddComponent<BuffExtraEffects>(c => {
                c.m_CheckedBuff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>();
                c.m_ExtraEffectBuff = PowerfulStanceEffectBuff.ToReference<BlueprintBuffReference>();
            });
            var ManglingFrenzy = Resources.GetBlueprint<BlueprintFeature>("29e2f51e6dd7427099b015de88718990");
            var ManglingFrenzyBuff = Resources.GetBlueprint<BlueprintBuff>("1581c5ceea24418cadc9f26ce4d391a9");
            ManglingFrenzy.AddComponent<BuffExtraEffects>(c => {
                c.m_CheckedBuff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>();
                c.m_ExtraEffectBuff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>();
            });
            var CloakOfBlood = Resources.GetBlueprint<BlueprintFeature>("9db109eee03524f4aa89881c4539a14d");
            var CloakOfBloodBuff = Resources.GetBlueprint<BlueprintBuff>("4e850d3b0c68a6045abd10e386d07af5");
            CloakOfBlood.AddComponent<BuffExtraEffects>(c => {
                c.m_CheckedBuff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>();
                c.m_ExtraEffectBuff = CloakOfBloodBuff.ToReference<BlueprintBuffReference>();
            });
            var DemonicResentment = Resources.GetBlueprint<BlueprintFeature>("4a83b67d4f24bba4a8d881340185bff0");
            var DemonicResentmentBuff = Resources.GetBlueprint<BlueprintBuff>("9984f9709f6d8e6428dfa5a52faeabc4");
            DemonicResentment.AddComponent<BuffExtraEffects>(c => {
                c.m_CheckedBuff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>();
                c.m_ExtraEffectBuff = DemonicResentmentBuff.ToReference<BlueprintBuffReference>();
            });
            var CallToViolence = Resources.GetBlueprint<BlueprintBuff>("421eec56d618765499c8b9e6f8e153a1");
            var CallToViolenceBuff = Resources.GetBlueprint<BlueprintBuff>("6aae86b192b0d1e4da1dcf6936f97f98");
            ManglingFrenzy.AddComponent<BuffExtraEffects>(c => {
                c.m_CheckedBuff = WyrmSingerDraconicRageEffectBuff.ToReference<BlueprintBuffReference>();
                c.m_ExtraEffectBuff = CallToViolenceBuff.ToReference<BlueprintBuffReference>();
            });
            #endregion

            var WyrmSingerWyrmSagaInAreaFlag = Helpers.CreateBuff("WyrmSingerWyrmSagaInAreaFlag", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest| BlueprintBuff.Flags.HiddenInUi;
            });
            var WyrmSingerWyrmSagaTargetBuff = Helpers.CreateBuff("WyrmSingerWyrmSagaTargetBuff", bp => {
                bp.SetName("Wyrm Saga Target");
                bp.SetDescription("This ally is selected to take on a draconic aspect (as per form of the dragon I) of a type of the wyrm singer’s choice when under the effect of the Wyrm Saga performance. " +
                    "This ally cannot use the breath weapon attack provided by form of the dragon. The wyrm singer must expend 1 round of raging song each round to maintain wyrm saga, and can affect only a " +
                    "single ally at a time.");
                bp.m_Icon = WyrmSagaIcon;
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
                bp.Stacking = StackingType.Prolong;
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
                bp.Stacking = StackingType.Ignore;
                bp.FxOnStart = FormOfTheDragon1BlueBuff.FxOnStart;
            });
            var WyrmSingerWyrmSagaEffectBrass = Helpers.CreateBuff("WyrmSingerWyrmSagaEffectBrass", bp => {
                bp.SetName("Wyrm Saga - Brass");
                bp.SetDescription("You are in a dragon-like creature form now. You have a +4 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, a +2 polymorph " +
                    "bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +4 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to difficult terrain, and resistance to " +
                    "fire 20. Your movement {g|Encyclopedia:Speed}speed{/g} is increased by 10 feet. You also gain one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and two " +
                    "wing {g|Encyclopedia:Attack}attacks{/g} (1d4). You do not gain the breath weapon normally gained from dragonkind spells.");
                bp.m_Icon = FormOfTheDragon1BrassAbility.Icon;
                bp.Components = FormOfTheDragon1BrassBuff.Components;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Ignore;
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
                bp.Stacking = StackingType.Ignore;
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
                bp.Stacking = StackingType.Ignore;
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
                bp.Stacking = StackingType.Ignore;
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
                bp.Stacking = StackingType.Ignore;
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
                WyrmSingerWyrmSagaEffect.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaInAreaFlag.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRemoveSelf()
                                )
                        }
                        );
                });
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
                bp.m_Icon = FormOfTheDragon1BrassAbility.Icon;
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
                bp.m_Icon = FormOfTheDragonII.Icon;
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
                bp.m_Icon = FormOfTheDragon1BlackBuff.Icon;
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
                bp.m_Icon = FormOfTheDragon1BlueBuff.Icon;
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
                bp.m_Icon = FormOfTheDragon1BrassAbility.Icon;
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
                bp.m_Icon = FormOfTheDragon1RedBuff.Icon;
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
                bp.m_Icon = FormOfTheDragon1GreenBuff.Icon;
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
                bp.m_Icon = FormOfTheDragon1SilverBuff.Icon;
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
                bp.m_Icon = FormOfTheDragon1WhiteBuff.Icon;
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
            var WyrmSingerWyrmSagaArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("WyrmSingerWyrmSagaArea", bp => { 
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WyrmSingerWyrmSagaInAreaFlag.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = false,
                            AsChild = true,
                            SameDuration = false
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaTargetBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaEffectBlack.ToReference<BlueprintUnitFactReference>(),
                                        Not = true
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
                                        BonusValue = 0,
                                        m_IsExtendable = true
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
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaEffectBlue.ToReference<BlueprintUnitFactReference>(),
                                        Not = true
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
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaEffectBrass.ToReference<BlueprintUnitFactReference>(),
                                        Not = true
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
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaEffectRed.ToReference<BlueprintUnitFactReference>(),
                                        Not = true
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
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaEffectGreen.ToReference<BlueprintUnitFactReference>(),
                                        Not = true
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
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaEffectSilver.ToReference<BlueprintUnitFactReference>(),
                                        Not = true
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
                                    new ContextConditionHasFact() {
                                        m_Fact = WyrmSingerWyrmSagaEffectWhite.ToReference<BlueprintUnitFactReference>(),
                                        Not = true
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
                            m_Buff = WyrmSingerWyrmSagaInAreaFlag.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = WyrmSagaIcon;
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
                bp.m_Icon = WyrmSagaIcon;
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
