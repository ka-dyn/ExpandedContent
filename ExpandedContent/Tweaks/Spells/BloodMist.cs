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
    internal class BloodMist {
        public static void AddBloodMist() {
            var BloodMistIcon = AssetLoader.LoadInternal("Skills", "Icon_BloodMist.jpg");
            var Icon_ScrollOfBloodMist = AssetLoader.LoadInternal("Items", "Icon_ScrollOfBloodMist.png");

            var BlurBuff = Resources.GetBlueprint<BlueprintBuff>("dd3ad347240624d46a11a092b4dd4674");


            var BloodMistConcealmentBuff = Helpers.CreateBuff("BloodMistConcealmentBuff", bp => {
                bp.SetName("Blood Mist Concealment");
                bp.SetDescription("Any creature within the blood mist is coated by it, turning the creature a reddish color. All targets within the mist gain concealment.");
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
            //BloodMistConcealmentBuff.FxOnStart = BloodMistConcealmentBuff.FxOnStart.CreateDynamicProxy(pfl => {
            //    Main.Log($"Editing: {pfl}");
            //    pfl.name = "BloodMistConcealmentFX";
            //    Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                
            //});

            var BloodMistAttackBuff = Helpers.CreateBuff("BloodMistAttackBuff", bp => {
                bp.SetName("Blood Mist");
                bp.SetDescription("Any creature within the mist that fails it's save take 1d4 points of Wisdom damage and become enraged, attacking any " +
                    "creatures it detects nearby (as the “attack nearest creature” result of the confused condition). An enraged creature remains so " +
                    "for one minute per caster level. A creature only needs to save once each time it is within the mist (though leaving and returning " +
                    "requires another save).");
                bp.m_Icon = BloodMistIcon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.AttackNearest;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Confusion;
                });
                bp.FxOnStart = new PrefabLink() { AssetId = "602fa850c4a94d84eb8aa1bcc0d008c7" }; //Might change this
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });




            var BloodMistArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("BloodMistArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {//Attack and stat damage
                            Type = SavingThrowType.Fortitude,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = BloodMistAttackBuff.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Minutes,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue(),
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = true
                                            },
                                            IsFromSpell = false,
                                        },
                                        new ContextActionDealDamage() {
                                            m_Type = ContextActionDealDamage.Type.AbilityDamage,
                                            DamageType = new DamageTypeDescription() {
                                                Type = DamageType.Physical,
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
                            m_Buff = BloodMistConcealmentBuff.ToReference<BlueprintBuffReference>(),
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
                            m_Buff = BloodMistConcealmentBuff.ToReference<BlueprintBuffReference>()
                        });
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
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
                bp.Size = new Feet() { m_Value = 40 };
                bp.Fx = new PrefabLink() { AssetId = "ee38b41b2b360b2458ec48f1868ca51b" }; //Cloudkill cloud
                bp.CanBeUsedInTacticalCombat = false;
                bp.m_TickRoundAfterSpawn = false;
            });
            BloodMistArea.Fx = BloodMistArea.Fx.CreateDynamicProxy(pfl => {
                //Main.Log($"Editing: {pfl}");
                pfl.name = "BloodMist_40feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                pfl.transform.localScale = new(1.75f, 1.0f, 1.75f);
                var Fog_Loop = pfl.transform.Find("Root/Fog_Loop (1)/Fog_Loop").GetComponent<ParticleSystem>();
                Fog_Loop.startColor = new Color(1f, 0f, 0f, 1f);
                Fog_Loop.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var Fog_Loop_1 = pfl.transform.Find("Root/Fog_Loop (1)").GetComponent<ParticleSystem>();
                Fog_Loop_1.startColor = new Color(1f, 0f, 0f, 1f);
                Fog_Loop_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var Smoke_Particles_Loop = pfl.transform.Find("Root/Smoke_Particles_Loop").GetComponent<ParticleSystem>();
                Smoke_Particles_Loop.startColor = new Color(1f, 0f, 0f, 1f);
                var StartSmoke_Fill = pfl.transform.Find("Root/StartSmoke_Fill").GetComponent<ParticleSystem>();
                StartSmoke_Fill.startColor = new Color(1f, 0f, 0f, 0.8f);
                StartSmoke_Fill.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var StartSmoke_Fill_1 = pfl.transform.Find("Root/StartSmoke_Fill (1)").GetComponent<ParticleSystem>();
                StartSmoke_Fill_1.startColor = new Color(1f, 0f, 0f, 0.8f);
                StartSmoke_Fill_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var StartSmoke_Particles = pfl.transform.Find("Root/StartSmoke_Particles").GetComponent<ParticleSystem>();
                StartSmoke_Particles.startColor = new Color(1f, 0f, 0f, 0.8f);
                StartSmoke_Particles.scalingMode = ParticleSystemScalingMode.Hierarchy;
            });

            var BloodMistAbility = Helpers.CreateBlueprint<BlueprintAbility>("BloodMistAbility", bp => {
                bp.SetName("Blood Mist");
                bp.SetDescription("This spell summons forth a misty cloud of rust-red toxic algae. Any creature within the mist is coated by it, turning the creature the same reddish color. " +
                    "All targets within the mist gain concealment. Any creature within the mist must save or take 1d4 points of Wisdom damage and become enraged, attacking any creatures it " +
                    "detects nearby (as the “attack nearest creature” result of the confused condition). An enraged creature remains so for one minute per caster level. A creature only " +
                    "needs to save once each time it is within the mist (though leaving and returning requires another save).");
                bp.m_Icon = BloodMistIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = BloodMistArea.ToReference<BlueprintAbilityAreaEffectReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
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
                    c.m_Radius = 40.Feet();
                    c.m_TargetType = TargetType.Any;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Poison;
                });                
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Debuff;
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Bolstered | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("BloodMist.SavingThrow", "Fortitude partial");
            });
            
            var BloodMistScroll = ItemTools.CreateScroll("ScrollOfBloodMist", Icon_ScrollOfBloodMist, BloodMistAbility, 8, 15);
            VenderTools.AddScrollToLeveledVenders(BloodMistScroll);
            BloodMistAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 8);
            BloodMistAbility.AddToSpellList(SpellTools.SpellList.ShamanSpelllist, 8);
        }
    }
}
