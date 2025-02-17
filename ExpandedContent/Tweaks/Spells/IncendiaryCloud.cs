using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace ExpandedContent.Tweaks.Spells {
    internal class IncendiaryCloud {
        public static void AddIncendiaryCloud() {
            var IncendiaryCloudIcon = AssetLoader.LoadInternal("Skills", "Icon_IncendiaryCloud.jpg");
            //var Icon_ScrollOfIncendiaryCloud = AssetLoader.LoadInternal("Items", "Icon_ScrollOfIncendiaryCloud.png");

            var BlurBuff = Resources.GetBlueprint<BlueprintBuff>("dd3ad347240624d46a11a092b4dd4674");


            var IncendiaryCloudConcealmentBuff = Helpers.CreateBuff("IncendiaryCloudConcealmentBuff", bp => {
                bp.SetName("Incendiary Cloud Concealment");
                bp.SetDescription("All targets within the incendiary cloud gain concealment.");
                bp.m_Icon = BlurBuff.Icon;
                bp.AddComponent<AddConcealment>(c => {
                    c.Descriptor = ConcealmentDescriptor.Fog;
                    c.Concealment = Concealment.Partial;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.CheckDistance = false;
                    c.DistanceGreater = 0.Feet();
                    c.OnlyForAttacks = false;
                });
                bp.FxOnStart = BlurBuff.FxOnStart; //blur
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            



            var IncendiaryCloudArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("IncendiaryCloudArea", bp => {
                bp.AddComponent<AbilityAreaEffectMovement>(c => {
                    c.DistancePerRound = new Feet() { m_Value = 10 };
                });
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {//Attack and stat damage
                            Type = SavingThrowType.Reflex,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(
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
                                            AbilityType = StatType.Wisdom,
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
                                                    Value = 6,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.DamageBonus,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                            },
                                            IsAoE = false,
                                            HalfIfSaved = false,
                                            UseMinHPAfterDamage = false,
                                            MinHPAfterDamage = 0,
                                            ResultSharedValue = AbilitySharedValue.Damage,
                                            CriticalSharedValue = AbilitySharedValue.Damage,
                                            Half = true
                                        }
                                        ),
                                    Failed = Helpers.CreateActionList(                                        
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
                                            AbilityType = StatType.Wisdom,
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
                                                    Value = 6,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.DamageBonus,
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
                                        })
                                }
                                )
                        },
                        new ContextActionApplyBuff() {//Add Concealment
                            m_Buff = IncendiaryCloudConcealmentBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue(),
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = IncendiaryCloudConcealmentBuff.ToReference<BlueprintBuffReference>()
                        });
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {//Attack and stat damage
                            Type = SavingThrowType.Reflex,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                            new ContextActionConditionalSaved() {
                                Succeed = Helpers.CreateActionList(
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
                                        AbilityType = StatType.Wisdom,
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
                                                Value = 6,
                                                ValueRank = AbilityRankType.Default,
                                                ValueShared = AbilitySharedValue.Damage,
                                                Property = UnitProperty.None
                                            },
                                            BonusValue = new ContextValue() {
                                                ValueType = ContextValueType.Simple,
                                                Value = 0,
                                                ValueRank = AbilityRankType.DamageBonus,
                                                ValueShared = AbilitySharedValue.Damage,
                                                Property = UnitProperty.None
                                            },
                                        },
                                        IsAoE = false,
                                        HalfIfSaved = false,
                                        UseMinHPAfterDamage = false,
                                        MinHPAfterDamage = 0,
                                        ResultSharedValue = AbilitySharedValue.Damage,
                                        CriticalSharedValue = AbilitySharedValue.Damage,
                                        Half = true
                                    }
                                    ),
                                Failed = Helpers.CreateActionList(
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
                                        AbilityType = StatType.Wisdom,
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
                                                Value = 6,
                                                ValueRank = AbilityRankType.Default,
                                                ValueShared = AbilitySharedValue.Damage,
                                                Property = UnitProperty.None
                                            },
                                            BonusValue = new ContextValue() {
                                                ValueType = ContextValueType.Simple,
                                                Value = 0,
                                                ValueRank = AbilityRankType.DamageBonus,
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
                                    })
                            }
                            )
                        }
                        );
                });                
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.m_Tags = AreaEffectTags.DestroyableInCutscene;
                bp.SpellResistance = true;
                bp.AffectEnemies = true;
                bp.AggroEnemies = true;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 20 };
                bp.Fx = new PrefabLink() { AssetId = "ee38b41b2b360b2458ec48f1868ca51b" }; //Cloudkill cloud
                bp.CanBeUsedInTacticalCombat = false;
                bp.m_TickRoundAfterSpawn = false;
            });
            IncendiaryCloudArea.Fx = IncendiaryCloudArea.Fx.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "IncendiaryCloud_20feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                //pfl.transform.localScale = new(1.75f, 1.0f, 1.75f);
                var Fog_Loop = pfl.transform.Find("Root/Fog_Loop (1)/Fog_Loop").GetComponent<ParticleSystem>();
                Fog_Loop.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                //Fog_Loop.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var Fog_Loop_1 = pfl.transform.Find("Root/Fog_Loop (1)").GetComponent<ParticleSystem>();
                Fog_Loop_1.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                //Fog_Loop_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var Smoke_Particles_Loop = pfl.transform.Find("Root/Smoke_Particles_Loop").GetComponent<ParticleSystem>();
                Smoke_Particles_Loop.startColor = new Color(0.06f, 0.0313f, 0.0471f, 1f);
                var StartSmoke_Fill = pfl.transform.Find("Root/StartSmoke_Fill").GetComponent<ParticleSystem>();
                StartSmoke_Fill.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                //StartSmoke_Fill.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var StartSmoke_Fill_1 = pfl.transform.Find("Root/StartSmoke_Fill (1)").GetComponent<ParticleSystem>();
                StartSmoke_Fill_1.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                //StartSmoke_Fill_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var StartSmoke_Particles = pfl.transform.Find("Root/StartSmoke_Particles").GetComponent<ParticleSystem>();
                StartSmoke_Particles.startColor = new Color(0.06f, 0.0313f, 0.0471f, 1f);
                //StartSmoke_Particles.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var Sparks = pfl.transform.Find("Root/Sparks").GetComponent<ParticleSystem>();
                Sparks.startColor = new Color(0.9f, 0.25f, 0f, 1f);
                Sparks.transform.localScale = new(1.75f, 1.0f, 1.75f);
                var Sparks_Loop = pfl.transform.Find("Root/Sparks_Loop").GetComponent<ParticleSystem>();
                Sparks_Loop.startColor = new Color(0.8f, 0.5f, 0f, 0.9f);
                Sparks_Loop.transform.localScale = new(1.75f, 1.0f, 1.75f);
            });

            var IncendiaryCloudAbility = Helpers.CreateBlueprint<BlueprintAbility>("IncendiaryCloudAbility", bp => {
                bp.SetName("Incendiary Cloud");
                bp.SetDescription("An incendiary cloud spell creates a cloud of roiling smoke shot through with white-hot embers. " +
                    "All targets within the mist gain concealment. the white-hot embers within the cloud deal 6d6 points of fire " +
                    "damage to everything within the cloud on your turn each round. All targets can make Reflex saves each round to take half damage." +
                    "\nUnlike a fog cloud, the incendiary cloud moves away from you at 10 feet per {g|Encyclopedia:Combat_Round}round{/g}, " +
                    "{g|Encyclopedia:Dice}rolling{/g} along the surface of the ground.");
                bp.m_Icon = IncendiaryCloudIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = IncendiaryCloudArea.ToReference<BlueprintAbilityAreaEffectReference>(),
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.Type = AbilityType.Spell;
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
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Bolstered | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("IncendiaryCloud.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("IncendiaryCloud.SavingThrow", "Reflex half");
            });

            //var IncendiaryCloudScroll = ItemTools.CreateScroll("ScrollOfIncendiaryCloud", Icon_ScrollOfIncendiaryCloud, IncendiaryCloudAbility, 8, 15);
            //VenderTools.AddScrollToLeveledVenders(IncendiaryCloudScroll);
            IncendiaryCloudAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 8);
        }
    }
}
