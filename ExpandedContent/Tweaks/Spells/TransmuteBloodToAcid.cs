using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
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
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using static HarmonyLib.Code;
using static Kingmaker.Armies.TacticalCombat.Grid.TacticalCombatGrid;
using static Kingmaker.EntitySystem.EntityDataBase;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.TimedProbabilityCurve;
using System.Security.Policy;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;

namespace ExpandedContent.Tweaks.Spells {
    internal class TransmuteBloodToAcid {
        public static void AddTransmuteBloodToAcid() {
            var TransmuteBloodToAcidIcon = AssetLoader.LoadInternal("Skills", "Icon_TransmuteBloodToAcid.jpg");
            var Icon_ScrollOfTransmuteBloodToAcid = AssetLoader.LoadInternal("Items", "Icon_ScrollOfTransmuteBloodToAcid.png");

            var StaggeredBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("df3950af5a783bd4d91ab73eb8fa0fd3");
            var BleedImmunity = Resources.GetBlueprint<BlueprintBuff>("3f6038d75ccffaa40b338f4b13f9e4b6");
            var CritImmunity = Resources.GetBlueprint<BlueprintFeature>("df3950af5a783bd4d91ab73eb8fa0fd3");


            var TransmuteBloodToAcidBuff = Helpers.CreateBuff("TransmuteBloodToAcidBuff", bp => {
                bp.SetName("Transmute Blood To Acid");
                bp.SetDescription("Your transmute blood in the target’s body to acid, dealing 1d6 points of acid damage/2 levels (maximum 12d6) each round. " +
                    "The creature is staggered and sickened by the debilitating pain. A successful Fortitude save each round halves the damage and negates the " +
                    "staggered condition for 1 round. If this damage reduces the creature to 0 or fewer hit points, it dissolves, leaving only the barest trace of remains. " +
                    "A dissolved creature’s equipment is unaffected. Anyone who strikes the target with a non - reach melee weapon, natural weapon, or unarmed attack takes 3d6 " +
                    "points of acid damage as the acidic blood sprays on the attacker.If the attack is from a piercing or slashing manufactured weapon, the weapon also takes this damage. " +
                    "This spell has no effect on creatures immune to critical hits or bleed effects.");
                bp.m_Icon = TransmuteBloodToAcidIcon;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue() { },
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
                                                Energy = DamageEnergyType.Acid
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
                                            Half = true,
                                            IsAoE = false,
                                            HalfIfSaved = false,
                                            ResultSharedValue = AbilitySharedValue.Damage,
                                            CriticalSharedValue = AbilitySharedValue.Damage
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
                                                Energy = DamageEnergyType.Acid
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
                                            Half = false,
                                            IsAoE = false,
                                            HalfIfSaved = false,
                                            ResultSharedValue = AbilitySharedValue.Damage,
                                            CriticalSharedValue = AbilitySharedValue.Damage
                                        },
                                        new ContextActionApplyBuff() {
                                            m_Buff = StaggeredBuff,
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 1,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage
                                                }
                                            },
                                            DurationSeconds = 0
                                        }
                                        )
                                }
                        )
                        }

                    );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 12;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var TransmuteBloodToAcidAbility = Helpers.CreateBlueprint<BlueprintAbility>("TransmuteBloodToAcidAbility", bp => {
                bp.SetName("Transmute Blood To Acid");
                bp.SetDescription("You transmute blood in the target’s body to acid, dealing 1d6 points of acid damage/2 levels (maximum 12d6) each round. " +
                    "The creature is staggered and sickened by the debilitating pain. A successful Fortitude save each round halves the damage and negates the " +
                    "staggered condition for 1 round. Anyone who strikes the target with a non-reach melee weapon, natural weapon, or unarmed attack takes 3d6 " +
                    "points of acid damage as the acidic blood sprays on the attacker. \nThis spell has no effect on creatures immune to critical hits or bleed effects.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TransmuteBloodToAcidBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = StaggeredBuff,
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        }
                    );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 5;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Acid;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        CritImmunity.ToReference<BlueprintUnitFactReference>(),
                        BleedImmunity.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = TransmuteBloodToAcidIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("TransmuteBloodToAcidAbility.Duration", "1 round/5 levels");
                bp.LocalizedSavingThrow = Helpers.CreateString("TransmuteBloodToAcidAbility.SavingThrow", "Will partial");
            });
            var TransmuteBloodToAcidScroll = ItemTools.CreateScroll("ScrollOfTransmuteBloodToAcid", Icon_ScrollOfTransmuteBloodToAcid, TransmuteBloodToAcidAbility, 9, 17);
            VenderTools.AddScrollToLeveledVenders(TransmuteBloodToAcidScroll);
            TransmuteBloodToAcidAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 9);
        }
    }
}
