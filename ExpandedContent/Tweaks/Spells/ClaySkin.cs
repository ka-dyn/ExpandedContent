using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class ClaySkin {
        public static void AddClaySkin() {
            var ClaySkinIcon = AssetLoader.LoadInternal("Skills", "Icon_ClaySkin.jpg");
            var Icon_ScrollOfClaySkin = AssetLoader.LoadInternal("Items", "Icon_ScrollOfClaySkin.png");


            var ClaySkinBuff = Helpers.CreateBuff("ClaySkinBuff", bp => {
                bp.SetName("Clay Skin");
                bp.SetDescription("The target’s skin becomes as thick and tough as clay, granting the target DR 5/adamantine. Once the {g|Encyclopedia:Spell}spell{/g} has prevented a total of 5 " +
                    "points of damage per {g|Encyclopedia:Caster_Level}caster level{/g} (maximum 50 points), it is discharged and the spell ends.");
                bp.m_Icon = ClaySkinIcon;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage
                    };
                    c.UsePool = true;
                    c.Pool = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        m_AbilityParameter = AbilityParameterType.Level
                    };
                    c.Or = false;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.MultiplyByModifier;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 5;
                    c.m_UseMax = true;
                    c.m_Max = 50;
                });
                bp.FxOnStart = new PrefabLink() { AssetId = "eec87237ed7b61149a952f56da85bbb1" };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var ClaySkinAbility = Helpers.CreateBlueprint<BlueprintAbility>("ClaySkinAbility", bp => {
                bp.SetName("Clay Skin");
                bp.SetDescription("The target’s skin becomes as thick and tough as clay, granting the target DR 5/adamantine. Once the {g|Encyclopedia:Spell}spell{/g} has prevented a total of 5 " +
                    "points of damage per {g|Encyclopedia:Caster_Level}caster level{/g} (maximum 50 points), it is discharged and the spell ends.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ClaySkinBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,                                
                                m_IsExtendable = true
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
                });
                bp.AddComponent<AbilityTargetIsPartyMember>(c => {
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Abjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = ClaySkinIcon;
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
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ClaySkinAbility.Duration", "10 minutes/level or until discharged");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ClaySkinScroll = ItemTools.CreateScroll("ScrollOfClaySkin", Icon_ScrollOfClaySkin, ClaySkinAbility, 3, 5);
            VenderTools.AddScrollToLeveledVenders(ClaySkinScroll);
            ClaySkinAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 3);
            ClaySkinAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 3);
            ClaySkinAbility.AddToSpellList(SpellTools.SpellList.InquisitorSpellList, 3);
            ClaySkinAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 3);
            ClaySkinAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 3);
        }
    }
}
