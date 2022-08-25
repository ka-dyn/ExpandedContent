using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class Slipstream {
        public static void AddSlipstream() {
            var SlipstreamIcon = AssetLoader.LoadInternal("Skills", "Icon_Slipstream.jpg");

            var SlipstreamBuff = Helpers.CreateBuff("SlipstreamBuff", bp => {
                bp.SetName("Slipstream");
                bp.SetDescription("You create low-cresting waves of water under the feet of the target, able to propel them while moving. The target’s speed increases by 10 feet.");
                bp.m_Icon = SlipstreamIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });
            });

            var SlipstreamAbility = Helpers.CreateBlueprint<BlueprintAbility>("SlipstreamAbility", bp => {
                bp.SetName("Slipstream");
                bp.SetDescription("You create low-cresting waves of water under the feet of the target, able to propel them while moving. The target’s speed increases by 10 feet.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = SlipstreamBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,                                
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = SlipstreamIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("SlipstreamAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            SlipstreamAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 2);
            SlipstreamAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 2);
            SlipstreamAbility.AddToSpellList(SpellTools.SpellList.RangerSpellList, 2);
            SlipstreamAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 2);
        }
    }
}
