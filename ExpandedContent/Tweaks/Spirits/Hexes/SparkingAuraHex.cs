using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules;

namespace ExpandedContent.Tweaks.Spirits.Hexes {
    internal class SparkingAuraHex {
        public static void AddSparkingAuraHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var ShamanWindSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("eae7cee2a5da93442a7563dfeda33432");
            var ShamanWindSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("cc05b2fff3e20c64e968f490a5160c08");
            var ShamanWindSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("5d810adea03fb644582eb74de32c75ec");

            var ShamanHexSparkingAuraCooldown = Helpers.CreateBuff("ShamanHexSparkingAuraCooldown", bp => {
                bp.SetName("Already targeted by this hex today");
                bp.SetDescription("");
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var StunningBarrierIcon = Resources.GetBlueprint<BlueprintAbility>("a5ec7892fb1c2f74598b3a82f3fd679f").Icon;

            var ShamanHexSparkingAuraBuff = Helpers.CreateBuff("ShamanHexSparkingAuraBuff", bp => {
                bp.SetName("Sparking Aura");
                bp.SetDescription("The shaman causes a creature within 30 feet to spark and shimmer with electrical energy. " +
                    "Though this does not harm the creature, it does cause the creature to emit light like a torch, preventing it from gaining any benefit from concealment or invisibility. " +
                    "Furthermore, while the aura lasts, whenever the target is hit with a metal melee weapon, it also takes an amount of electricity damage equal to the shaman’s Charisma modifier. " +
                    "The sparking aura lasts a 1 round for every 2 shaman levels the shaman possesses. A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = StunningBarrierIcon;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.Invisible;
                });
                bp.AddComponent<DoNotBenefitFromConcealment>();
                bp.AddComponent<AddTargetAttackRollTrigger>(c => {
                    c.OnlyHit = true;
                    c.CriticalHit = false;
                    c.OnlyMelee = true;
                    c.NotReach = false;
                    c.CheckCategory = true;
                    c.Not = false;
                    c.Categories = new WeaponCategory[] {
                        WeaponCategory.Dagger,
                        WeaponCategory.LightMace,
                        WeaponCategory.PunchingDagger,
                        WeaponCategory.Sickle,
                        WeaponCategory.HeavyMace,
                        WeaponCategory.Dart,
                        WeaponCategory.Handaxe,
                        WeaponCategory.Kukri,
                        WeaponCategory.LightHammer,
                        WeaponCategory.LightPick,
                        WeaponCategory.Shortsword,
                        WeaponCategory.Starknife,
                        WeaponCategory.WeaponLightShield,
                        WeaponCategory.SpikedLightShield,
                        WeaponCategory.Battleaxe,
                        WeaponCategory.Flail,
                        WeaponCategory.HeavyPick,
                        WeaponCategory.Longsword,
                        WeaponCategory.Rapier,
                        WeaponCategory.Scimitar,
                        WeaponCategory.Warhammer,
                        WeaponCategory.WeaponHeavyShield,
                        WeaponCategory.SpikedHeavyShield,
                        WeaponCategory.EarthBreaker,
                        WeaponCategory.Falchion,
                        WeaponCategory.Glaive,
                        WeaponCategory.Greataxe,
                        WeaponCategory.Greatsword,
                        WeaponCategory.HeavyFlail,
                        WeaponCategory.Scythe,
                        WeaponCategory.Sai,
                        WeaponCategory.Siangham,//??
                        WeaponCategory.BastardSword,
                        WeaponCategory.DuelingSword,
                        WeaponCategory.DwarvenWaraxe,
                        WeaponCategory.Estoc,
                        WeaponCategory.Falcata,
                        WeaponCategory.Tongi,
                        WeaponCategory.ElvenCurvedBlade,
                        WeaponCategory.Fauchard,
                        WeaponCategory.Shuriken,
                        WeaponCategory.Bardiche,
                        WeaponCategory.DoubleSword,
                        WeaponCategory.DoubleAxe,
                        WeaponCategory.Urgrosh,
                        WeaponCategory.HookedHammer,
                        WeaponCategory.ThrowingAxe,
                        WeaponCategory.SawtoothSabre
                    };
                    c.AffectFriendlyTouchSpells = false;
                    c.ActionsOnAttacker = Helpers.CreateActionList(
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
                                    Form = PhysicalDamageForm.Piercing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Electricity
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
                            },
                            IgnoreCritical = true,
                        }
                        );
                    c.ActionOnSelf = Helpers.CreateActionList();
                    c.DoNotPassAttackRoll = false;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                //bp.FxOnStart = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "6035a889bae45f242908569a7bc25c93" }; //KhorElectricityVisualBuff
            });


            var ShamanHexSparkingAuraAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexSparkingAuraAbility", bp => {
                bp.SetName("Sparking Aura");
                bp.SetDescription("The shaman causes a creature within 30 feet to spark and shimmer with electrical energy. " +
                    "Though this does not harm the creature, it does cause the creature to emit light like a torch, preventing it from gaining any benefit from concealment or invisibility. " +
                    "Furthermore, while the aura lasts, whenever the target is hit with a metal melee weapon, it also takes an amount of electricity damage equal to the shaman’s Charisma modifier. " +
                    "The sparking aura lasts a 1 round for every 2 shaman levels the shaman possesses. A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = StunningBarrierIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexSparkingAuraBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexSparkingAuraCooldown.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Days,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ShamanHexSparkingAuraCooldown.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                    c.FromCaster = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                //bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                //    c.StatType = StatType.Wisdom;
                //    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                //});
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanHexSparkingAuraAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });



            var ShamanHexSparkingAuraFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexSparkingAuraFeature", bp => {
                bp.SetName("Sparking Aura");
                bp.SetDescription("The shaman causes a creature within 30 feet to spark and shimmer with electrical energy. " +
                    "Though this does not harm the creature, it does cause the creature to emit light like a torch, preventing it from gaining any benefit from concealment or invisibility. " +
                    "Furthermore, while the aura lasts, whenever the target is hit with a metal melee weapon, it also takes an amount of electricity damage equal to the shaman’s Charisma modifier. " +
                    "The sparking aura lasts a 1 round for every 2 shaman levels the shaman possesses. A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = StunningBarrierIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexSparkingAuraAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWindSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWindSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanWindSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexSparkingAuraFeature);

            ShamanWindSpiritProgression.IsPrerequisiteFor.Add(ShamanHexSparkingAuraFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
