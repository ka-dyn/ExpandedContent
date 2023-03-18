using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
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

namespace ExpandedContent.Tweaks.Spells {
    internal class GloomblindBolts {
        public static void AddGloomblindBolts() {

            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");
            var MagicMissile00 = Resources.GetBlueprint<BlueprintProjectile>("2e3992d1695960347a7f9bdf8122966f");
            var MagicMissile01 = Resources.GetBlueprint<BlueprintProjectile>("741743ccd287a854fbb68ce70f75fa05");
            var MagicMissile02 = Resources.GetBlueprint<BlueprintProjectile>("674e6d958be63ff4a85a7e5fdc1e818a");
            var RayWeapon = Resources.GetBlueprint<BlueprintItemWeapon>("f6ef95b1f7bb52b408a5b345a330ffe8");
            var Blind = Resources.GetBlueprint<BlueprintBuff>("0ec36e7596a4928489d2049e1e1c76a7");
            var GloomblindBoltsIcon = AssetLoader.LoadInternal("Skills", "Icon_GloomblindBolts.jpg");
            var Icon_ScrollOfGloomblindBolts = AssetLoader.LoadInternal("Items", "Icon_ScrollOfGloomblindBolts.png");


            var GloomblindBoltsAbility = Helpers.CreateBlueprint<BlueprintAbility>("GloomblindBoltsAbility", bp => {
                bp.SetName("Gloomblind Bolts");
                bp.SetDescription("You create one or more bolts of negative energy infused with shadow pulled from the Shadow Plane. You can fire one bolt, plus one " +
                    "for every four levels beyond 5th (to a maximum of three bolts at 13th level) at the same target, each requiring a ranged touch attack to hit. Each " +
                    "bolt deals 4d6 points of damage to a living creature or heals 4d6 points of damage to an undead creature. Furthermore, the bolt’s energy spreads " +
                    "over the skin of creature, possibly blinding it for a short time. Any creature struck by a bolt must succeed at a Reflex saving throw or become " +
                    "blinded for 1 round.");
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[3] {
                        MagicMissile00.ToReference<BlueprintProjectileReference>(),
                        MagicMissile01.ToReference<BlueprintProjectileReference>(),
                        MagicMissile02.ToReference<BlueprintProjectileReference>()
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
                    c.DelayBetweenProjectiles = 0.0f;
                    c.m_ControlledProjectileHolderBuff = null; //?
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
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
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
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
                                }
                                )
                        },
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Reflex,
                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                            UseDCFromContextSavingThrow = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = Blind.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage
                                                },
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 1,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage
                                                }
                                            },
                                            DurationSeconds = 0
                                        }
                                        ),
                                }
                                )
                        }                        
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 5;
                    c.m_StepLevel = 4;
                    c.m_UseMax = true;
                    c.m_Max = 3;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = GloomblindBoltsIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("GloomblindBoltsAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("GloomblindBoltsAbility.SavingThrow", "Relex partial");
            });
            var GloomblindBoltsScroll = ItemTools.CreateScroll("ScrollOfGloomblindBolts", Icon_ScrollOfGloomblindBolts, GloomblindBoltsAbility, 3, 5);
            VenderTools.AddScrollToLeveledVenders(GloomblindBoltsScroll);
            GloomblindBoltsAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 3);
            GloomblindBoltsAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 3);
            GloomblindBoltsAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 3);
            GloomblindBoltsAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 3);
            GloomblindBoltsAbility.AddToSpellList(SpellTools.SpellList.LichWizardSpelllist, 3);
        }
    }
}
