using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
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

namespace ExpandedContent.Tweaks.Spells {
    internal class SteamRayFusillade {
        public static void AddSteamRayFusillade() {

            var ScorchingRay = Resources.GetBlueprint<BlueprintAbility>("cdb106d53c65bbc4086183d54c3b97c7");
            var SteamBlastBase = Resources.GetBlueprint<BlueprintAbility>("3baf01649a92ae640927b0f633db7c11");
            var Kinetic_Steam00_Projectile = Resources.GetBlueprint<BlueprintProjectile>("36e5df234b905d34f8f5ff542b1f21b8");
            var RayWeapon = Resources.GetBlueprint<BlueprintItemWeapon>("f6ef95b1f7bb52b408a5b345a330ffe8");
            var SteamRayFusilladeIcon = AssetLoader.LoadInternal("Skills", "Icon_SteamRayFusillade.jpg");


            // Buff Ability
            var SteamRayFusilladeBuffAbility = Helpers.CreateBlueprint<BlueprintAbility>("SteamRayFusilladeBuffAbility", bp => {
                bp.SetName("Steam Ray Fusillade");
                bp.SetDescription("Once per round as a standard action you may fire three rays, plus one additional ray for every four levels beyond 11th (to a maximum of " +
                    "five rays at 19th level). Each ray requires a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} to hit and deals {g|Encyclopedia:Dice}4d6{/g} points of " +
                    "{g|Encyclopedia:Energy_Damage}fire damage{/g}.");
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[5] {
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Simple;
                    c.IsHandOfTheApprentice = false;
                    c.m_Length = 0.Feet();
                    c.m_LineWidth = 5.Feet();
                    c.NeedAttackRoll = true;
                    c.m_Weapon = RayWeapon.ToReference<BlueprintItemWeaponReference>();
                    c.ReplaceAttackRollBonusStat = false;
                    c.AttackRollBonusStat = StatType.Unknown;
                    c.UseMaxProjectilesCount = true;
                    c.MaxProjectilesCountRank = AbilityRankType.ProjectilesCount;
                    c.DelayBetweenProjectiles = 0.3f;
                    c.m_ControlledProjectileHolderBuff = null; //?
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
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
                            IsAoE = false,
                            HalfIfSaved = false,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 4;
                    c.m_UseMax = true;
                    c.m_Max = 5;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Fire | SpellDescriptor.Cold;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = SteamRayFusilladeIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            // Buff
            var SteamRayFusilladeBuff = Helpers.CreateBuff("SteamRayFusilladeBuff", bp => {
                bp.SetName("Steam Ray Fusillade");
                bp.SetDescription("Once per round as a standard action you may fire three rays, plus one additional ray for every four levels beyond 11th (to a maximum of " +
                    "five rays at 19th level). Each ray requires a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} to hit and deals {g|Encyclopedia:Dice}4d6{/g} points of " +
                    "{g|Encyclopedia:Energy_Damage}fire damage{/g}.");
                bp.m_Icon = SteamBlastBase.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SteamRayFusilladeBuffAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                    c.m_Ability = SteamRayFusilladeBuffAbility.ToReference<BlueprintAbilityReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            // Ability
            var SteamRayFusilladeAbility = Helpers.CreateBlueprint<BlueprintAbility>("SteamRayFusilladeAbility", bp => {
                bp.SetName("Steam Ray Fusillade");
                bp.SetDescription("For the duration of the spell, you can fire rays of steam once per round as a standard action (and you also fire these rays as part of casting this " +
                    "spell). You may fire three rays, plus one additional ray for every four levels beyond 11th (to a maximum of five rays at 19th level). Each ray requires a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g} to hit and deals {g|Encyclopedia:Dice}4d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g}.");
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[5] {
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>(),
                        Kinetic_Steam00_Projectile.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Simple;
                    c.IsHandOfTheApprentice = false;
                    c.m_Length = 0.Feet();
                    c.m_LineWidth = 5.Feet();
                    c.NeedAttackRoll = true;
                    c.m_Weapon = RayWeapon.ToReference<BlueprintItemWeaponReference>();
                    c.ReplaceAttackRollBonusStat = false;
                    c.AttackRollBonusStat = StatType.Unknown;
                    c.UseMaxProjectilesCount = true;
                    c.MaxProjectilesCountRank = AbilityRankType.ProjectilesCount;
                    c.DelayBetweenProjectiles = 0.3f;
                    c.m_ControlledProjectileHolderBuff = null; //?
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SteamRayFusilladeBuff.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
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
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    IsFromSpell = true,
                                }
                            )
                        },
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
                            IsAoE = false,
                            HalfIfSaved = false,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 4;
                    c.m_UseMax = true;
                    c.m_Max = 5;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Fire | SpellDescriptor.Cold;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = SteamRayFusilladeIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("SteamRayFusilladeAbility.Duration", "Buff 1 round/level ");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            SteamRayFusilladeAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 7);

        }
    }
}
