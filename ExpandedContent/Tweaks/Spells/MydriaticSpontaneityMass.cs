using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
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
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class MydriaticSpontaneityMass {
        public static void AddMydriaticSpontaneityMass() {
            var MydriaticSpontaneityMassIcon = AssetLoader.LoadInternal("Skills", "Icon_MydriaticSpontaneityMass.jpg");
            //var Icon_ScrollOfMydriaticSpontaneityMass = AssetLoader.LoadInternal("Items", "Icon_ScrollOfMydriaticSpontaneityMass.png");

            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var UndeadType = Resources.GetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");

            var MydriaticSpontaneityBuff = Resources.GetModBlueprint<BlueprintBuff>("MydriaticSpontaneityBuff");

            var MydriaticSpontaneityMassAbility = Helpers.CreateBlueprint<BlueprintAbility>("MydriaticSpontaneityMassAbility", bp => {
                bp.SetName("Mass Mydriatic Spontaneity");
                bp.SetDescription("This spell functions as mydriatic spontaneity, except it can affect multiple creatures." +
                    "\nYou overstimulate the target with alternating flashes of light and shadow within its eyes, causing its pupils to rapidly dilate and contract. " +
                    "While under the effects of this spell, the target is racked by splitting headaches and unable to see clearly, becoming nauseated for the spell’s duration. " +
                    "Each round, the target’s pupils randomly become dilated or contracted for 1 round, causing them to become either blinded or dazzled until next round." +
                    "\nEach new round, the target may make a will save to remove the nauseated condition.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = ConstructType.ToReference<BlueprintUnitFactReference>(),
                                    },
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = PlantType.ToReference<BlueprintUnitFactReference>(),
                                    },
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = UndeadType.ToReference<BlueprintUnitFactReference>(),
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    Type = SavingThrowType.Will,
                                    FromBuff = false,
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
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
                //bp.AddComponent<AbilityTargetHasFact>(c => {
                //    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                //        ConstructType.ToReference<BlueprintUnitFactReference>(),
                //        PlantType.ToReference<BlueprintUnitFactReference>(),
                //        UndeadType.ToReference<BlueprintUnitFactReference>()
                //    };
                //    c.Inverted = true;
                //});
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 15.Feet();
                    c.m_TargetType = TargetType.Enemy;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = MydriaticSpontaneityMassIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("MydriaticSpontaneityMassAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("MydriaticSpontaneityMassAbility.SavingThrow", "Will negates");
            });
            //var MydriaticSpontaneityMassScroll = ItemTools.CreateScroll("ScrollOfMydriaticSpontaneityMass", Icon_ScrollOfMydriaticSpontaneityMass, MydriaticSpontaneityMassAbility, 7, 13);
            //VenderTools.AddScrollToLeveledVenders(MydriaticSpontaneityMassScroll);
            MydriaticSpontaneityMassAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 5);
            MydriaticSpontaneityMassAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 7);
            MydriaticSpontaneityMassAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 7);
        }
    }
}
