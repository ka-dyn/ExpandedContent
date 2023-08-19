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

namespace ExpandedContent.Tweaks.Spells {
    internal class InflictPain {
        public static void AddInflictPain() {
            var InflictPainIcon = AssetLoader.LoadInternal("Skills", "Icon_InflictPain.jpg");
            var Icon_ScrollOfInflictPain = AssetLoader.LoadInternal("Items", "Icon_ScrollOfInflictPain.png");

            var InflictPainBuff = Helpers.CreateBuff("InflictPainBuff", bp => {
                bp.SetName("Inflict Pain");
                bp.SetDescription("The target’s mind and body are wracked with agonizing pain that imposes a –4 penalty on attack rolls, skill checks, and combat maneuver checks.");
                bp.m_Icon = InflictPainIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = -4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.AdditionalCMB;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = -4;
                });
                bp.AddComponent<BuffAllSkillsBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = -4;
                    c.Multiplier = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 1,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var InflictPainAbility = Helpers.CreateBlueprint<BlueprintAbility>("InflictPainAbility", bp => {
                bp.SetName("Inflict Pain");
                bp.SetDescription("You telepathically wrack the target’s mind and body with agonizing pain that imposes a –4 penalty on attack rolls, skill checks, and combat maneuver checks. " +
                    "A successful Will save reduces the duration to 1 round.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = InflictPainBuff.ToReference<BlueprintBuffReference>(),
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
                                }),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = InflictPainBuff.ToReference<BlueprintBuffReference>(),
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
                                }
                            )
                        });
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
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = InflictPainIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("InflictPainAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("InflictPainAbility.SavingThrow", "Will partial");
            });
            var InflictPainScroll = ItemTools.CreateScroll("ScrollOfInflictPain", Icon_ScrollOfInflictPain, InflictPainAbility, 3, 5);
            VenderTools.AddScrollToLeveledVenders(InflictPainScroll);
            InflictPainAbility.AddToSpellList(SpellTools.SpellList.InquisitorSpellList, 2);
            InflictPainAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 3);
            InflictPainAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 3);
        }
    }
}
