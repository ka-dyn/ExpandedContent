using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class MydriaticSpontaneity {
        public static void AddMydriaticSpontaneity() {
            var MydriaticSpontaneityIcon = AssetLoader.LoadInternal("Skills", "Icon_MydriaticSpontaneity.jpg");
            //var Icon_ScrollOfMydriaticSpontaneity = AssetLoader.LoadInternal("Items", "Icon_ScrollOfMydriaticSpontaneity.png");

            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var UndeadType = Resources.GetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");

            var NauseatedBuff = Resources.GetBlueprint<BlueprintBuff>("956331dba5125ef48afe41875a00ca0e");
            var BlindedBuff = Resources.GetBlueprint<BlueprintBuff>("0ec36e7596a4928489d2049e1e1c76a7");
            var DazzledBuff = Resources.GetBlueprint<BlueprintBuff>("df6d1025da07524429afbae248845ecc");


            var MydriaticSpontaneityNauseatedBuff = Helpers.CreateBuff("MydriaticSpontaneityNauseatedBuff", bp => {
                bp.SetName("Mydriatic Spontaneity - Nauseated");
                bp.SetDescription("While under the effects of this spell, the target is racked by splitting headaches and unable to see clearly, becoming nauseated for the spell’s duration. " +
                    "Each round, the target’s pupils randomly become dilated or contracted for 1 round, causing them to become either blinded or dazzled until next round.");
                bp.m_Icon = NauseatedBuff.Icon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Nauseated;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Nauseated;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Will,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue() { },
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(
                                        new ContextActionRemoveSelf()
                                        ),
                                    Failed = Helpers.CreateActionList()
                                }
                            )
                        }
                    );
                });
                bp.AddComponent(NauseatedBuff.GetComponent<CombatStateTrigger>());
                bp.AddComponent(NauseatedBuff.GetComponent<RemoveWhenCombatEnded>());
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var MydriaticSpontaneityBlindedBuff = Helpers.CreateBuff("MydriaticSpontaneityBlindedBuff", bp => {
                bp.SetName("Mydriatic Spontaneity - Blinded");
                bp.SetDescription("While under the effects of this spell, the target is racked by splitting headaches and unable to see clearly, becoming nauseated for the spell’s duration. " +
                    "Each round, the target’s pupils randomly become dilated or contracted for 1 round, causing them to become either blinded or dazzled until next round.");
                bp.m_Icon = BlindedBuff.Icon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Blindness;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Blindness;
                });
                bp.AddComponent(BlindedBuff.GetComponent<CombatStateTrigger>());
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var MydriaticSpontaneityDazzledBuff = Helpers.CreateBuff("MydriaticSpontaneityDazzledBuff", bp => {
                bp.SetName("Mydriatic Spontaneity - Dazzled");
                bp.SetDescription("While under the effects of this spell, the target is racked by splitting headaches and unable to see clearly, becoming nauseated for the spell’s duration. " +
                    "Each round, the target’s pupils randomly become dilated or contracted for 1 round, causing them to become either blinded or dazzled until next round.");
                bp.m_Icon = DazzledBuff.Icon;
                bp.AddComponent<BuffStatusCondition>(c => {
                    c.Condition = UnitCondition.Dazzled;
                    c.SaveEachRound = false;
                    c.SaveType = SavingThrowType.Fortitude;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Blindness | SpellDescriptor.SightBased;
                });
                bp.AddComponent(DazzledBuff.GetComponent<AddFactContextActions>());
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });



            var MydriaticSpontaneityBuff = Helpers.CreateBuff("MydriaticSpontaneityBuff", bp => {
                bp.SetName("Mydriatic Spontaneity");
                bp.SetDescription("While under the effects of this spell, the target is racked by splitting headaches and unable to see clearly, becoming nauseated for the spell’s duration. " +
                    "Each round, the target’s pupils randomly become dilated or contracted for 1 round, causing them to become either blinded or dazzled until next round. " +
                    "\nEach new round, the target may make a will save to remove the nauseated condition.");
                bp.m_Icon = MydriaticSpontaneityIcon;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MydriaticSpontaneityNauseatedBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
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
                            DurationSeconds = 0,
                            AsChild = true,
                            IsNotDispelable = true
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionRandomize() {
                            m_Actions = new ContextActionRandomize.ActionWrapper[2] {
                                new ContextActionRandomize.ActionWrapper{
                                    Weight = 1,
                                    Action = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = MydriaticSpontaneityBlindedBuff.ToReference<BlueprintBuffReference>(),
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
                                            DurationSeconds = 0,
                                            AsChild = true,
                                            IsNotDispelable = true
                                        }
                                        )
                                },
                                new ContextActionRandomize.ActionWrapper{
                                    Weight = 1,
                                    Action = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = MydriaticSpontaneityDazzledBuff.ToReference<BlueprintBuffReference>(),
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
                                            DurationSeconds = 0,
                                            AsChild = true,
                                            IsNotDispelable = true
                                        }
                                        )
                                }
                            }
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var MydriaticSpontaneityAbility = Helpers.CreateBlueprint<BlueprintAbility>("MydriaticSpontaneityAbility", bp => {
                bp.SetName("Mydriatic Spontaneity");
                bp.SetDescription("You overstimulate the target with alternating flashes of light and shadow within its eyes, causing its pupils to rapidly dilate and contract. " +
                    "While under the effects of this spell, the target is racked by splitting headaches and unable to see clearly, becoming nauseated for the spell’s duration. " +
                    "Each round, the target’s pupils randomly become dilated or contracted for 1 round, causing them to become either blinded or dazzled until next round. " +
                    "\nEach new round, the target may make a will save to remove the nauseated condition.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = MydriaticSpontaneityBuff.ToReference<BlueprintBuffReference>(),
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
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.SightBased;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ConstructType.ToReference<BlueprintUnitFactReference>(),
                        PlantType.ToReference<BlueprintUnitFactReference>(),
                        UndeadType.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = MydriaticSpontaneityIcon;
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
                bp.AvailableMetamagic =  Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("MydriaticSpontaneityAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("MydriaticSpontaneityAbility.SavingThrow", "Will negates");
            });
            //var MydriaticSpontaneityScroll = ItemTools.CreateScroll("ScrollOfMydriaticSpontaneity", Icon_ScrollOfMydriaticSpontaneity, MydriaticSpontaneityAbility, 4, 7);
            //VenderTools.AddScrollToLeveledVenders(MydriaticSpontaneityScroll);
            MydriaticSpontaneityAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 3);
            MydriaticSpontaneityAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
            MydriaticSpontaneityAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 4);
        }
    }
}
