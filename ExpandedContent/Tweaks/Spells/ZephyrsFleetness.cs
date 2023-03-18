using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
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
    internal class ZephyrsFleetness {
        public static void AddZephyrsFleetness() {
            var ZephyrsFleetnessIcon = AssetLoader.LoadInternal("Skills", "Icon_ZephyrsFleetness.jpg");
            var Icon_ScrollOfZephyrsFleetness = AssetLoader.LoadInternal("Items", "Icon_ScrollOfZephyrsFleetness.png");

            var ZephyrsFleetnessBuff = Helpers.CreateBuff("ZephyrsFleetnessBuff", bp => {
                bp.SetName("Zephyr's Fleetness");
                bp.SetDescription("You can call upon the spirits of the air to grant agility to your allies. Each target gains a +30 foot enhancement bonus to its speed. " +
                    "Each target can also move (and charge) through difficult terrain without penalty.");
                bp.m_Icon = ZephyrsFleetnessIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Speed;
                    c.Value = 30;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.FxOnStart = new PrefabLink() { AssetId = "91ef30ab58fa0d3449d4d2ccc20cb0f8" };
            });

            var ZephyrsFleetnessAbility = Helpers.CreateBlueprint<BlueprintAbility>("ZephyrsFleetnessAbility", bp => {
                bp.SetName("Zephyr's Fleetness");
                bp.SetDescription("You can call upon the spirits of the air to grant agility to your allies. Each target gains a +30 foot enhancement bonus to its speed. " +
                    "Each target can also move (and charge) through difficult terrain without penalty.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ZephyrsFleetnessBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
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
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 60.Feet();
                    c.m_TargetType = TargetType.Ally;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = ZephyrsFleetnessIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ZephyrsFleetnessAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ZephyrsFleetnessScroll = ItemTools.CreateScroll("ScrollOfZephyrsFleetness", Icon_ScrollOfZephyrsFleetness, ZephyrsFleetnessAbility, 3, 5);
            VenderTools.AddScrollToLeveledVenders(ZephyrsFleetnessScroll);
            ZephyrsFleetnessAbility.AddToSpellList(SpellTools.SpellList.HunterSpelllist, 3);
            ZephyrsFleetnessAbility.AddToSpellList(SpellTools.SpellList.RangerSpellList, 3);
            ZephyrsFleetnessAbility.AddToSpellList(SpellTools.SpellList.TricksterSpelllist, 3);
        }
    }
}
