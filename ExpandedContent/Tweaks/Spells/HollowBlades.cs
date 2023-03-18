using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class HollowBlades {
        public static void AddHollowBlades() {
            var HollowBladesIcon = AssetLoader.LoadInternal("Skills", "Icon_HollowBlades.jpg");
            var Icon_ScrollOfHollowBlades = AssetLoader.LoadInternal("Items", "Icon_ScrollOfHollowBlades.png");

            var HollowBladesBuff = Helpers.CreateBuff("HollowBladesBuff", bp => {
                bp.SetName("Hollow Blades");
                bp.SetDescription("Hollow Blades lowers the momentum and density of the targets melee weapons the moment they would land an attack. All melee weapons used by the target deal damage" +
                    "as if they are one size category smaller than they actually are. For instance, a Large longsword normally deals {g|Encyclopedia:Dice}2d6{/g} points of damage, but it would " +
                    "instead deal 1d8 points of damage if effected by hollow blades.");
                bp.m_Icon = HollowBladesIcon;
                bp.AddComponent<MeleeWeaponSizeChange>(c => {
                    c.SizeCategoryChange = -1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var HollowBladesAbility = Helpers.CreateBlueprint<BlueprintAbility>("HollowBladesAbility", bp => {
                bp.SetName("Hollow Blades");
                bp.SetDescription("Hollow Blades lowers the momentum and density of the targets melee weapons the moment they would land an attack. All melee weapons used by the target deal damage" +
                    "as if they are one size category smaller than they actually are. For instance, a Large longsword normally deals {g|Encyclopedia:Dice}2d6{/g} points of damage, but it would " +
                    "instead deal 1d8 points of damage if effected by hollow blades.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = HollowBladesBuff.ToReference<BlueprintBuffReference>(),
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
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = HollowBladesIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("HollowBladesAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("HollowBladesAbility.SavingThrow", "Fortitude negates");
            });
            var HollowBladesScroll = ItemTools.CreateScroll("ScrollOfHollowBlades", Icon_ScrollOfHollowBlades, HollowBladesAbility, 3, 5);
            VenderTools.AddScrollToLeveledVenders(HollowBladesScroll);
            HollowBladesAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 2);
            HollowBladesAbility.AddToSpellList(SpellTools.SpellList.HunterSpelllist, 1);
            HollowBladesAbility.AddToSpellList(SpellTools.SpellList.RangerSpellList, 1);
            HollowBladesAbility.AddToSpellList(SpellTools.SpellList.ShamanSpelllist, 3);
            HollowBladesAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 3);
            HollowBladesAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 3);
        }
    }
}
