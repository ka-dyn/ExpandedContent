using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
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
            var Icon_ScrollOfMydriaticSpontaneity = AssetLoader.LoadInternal("Items", "Icon_ScrollOfMydriaticSpontaneity.png");

            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var UndeadType = Resources.GetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");

            var MydriaticSpontaneityBuff = Helpers.CreateBuff("MydriaticSpontaneityBuff", bp => {
                bp.SetName("Inflict Pain");
                bp.SetDescription("The target’s mind and body are wracked with agonizing pain that imposes a –4 penalty on attack rolls, skill checks, and combat maneuver checks.");
                bp.m_Icon = MydriaticSpontaneityIcon;
                




                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var MydriaticSpontaneityAbility = Helpers.CreateBlueprint<BlueprintAbility>("MydriaticSpontaneityAbility", bp => {
                bp.SetName("Mydriatic Spontaneity");
                bp.SetDescription("You telepathically wrack the target’s mind and body with agonizing pain that imposes a –4 penalty on attack rolls, skill checks, and combat maneuver checks. " +
                    "A successful Will save reduces the duration to 1 round.");
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ConstructType.ToReference<BlueprintUnitFactReference>(),
                        PlantType.ToReference<BlueprintUnitFactReference>(),
                        UndeadType.ToReference<BlueprintUnitFactReference>()
                    };
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
            var MydriaticSpontaneityScroll = ItemTools.CreateScroll("ScrollOfMydriaticSpontaneity", Icon_ScrollOfMydriaticSpontaneity, MydriaticSpontaneityAbility, 4, 7);
            VenderTools.AddScrollToLeveledVenders(MydriaticSpontaneityScroll);
            MydriaticSpontaneityAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 3);
            MydriaticSpontaneityAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
            MydriaticSpontaneityAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 4);
        }
    }
}
