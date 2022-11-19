using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
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
    internal class PurgingFinale {
        public static void AddPurgingFinale() {
            var PerformanceCooldown = Resources.GetModBlueprint<BlueprintBuff>("PerformanceCooldown");
            var PurgingFinaleIcon = AssetLoader.LoadInternal("Skills", "Icon_PurgingFinale.jpg");
            var Icon_ScrollOfPurgingFinale = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPurgingFinale.png");
            var RestorationLesserFX = Resources.GetBlueprint<BlueprintAbility>("e84fc922ccf952943b5240293669b171").GetComponent<AbilitySpawnFx>();
            //Main
            var PurgingFinale = Helpers.CreateBlueprint<BlueprintAbility>("PurgingFinale", bp => {
                bp.SetName("Purging Finale");
                bp.SetDescription("You must have a bardic performance in effect to cast this spell. With a flourish, you immediately end your bardic performance, " +
                    "removing one of the following conditions on a creature within range affected by your bardic performance: dazzled, exhausted, paralyzed, shaken, or stunned." +
                    "\nYou cannot activate another bardic performance this round. If you have the lingering performance feat the effect lingers as normal.");
                bp.m_Icon = PurgingFinaleIcon;                
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PerformanceCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent(FinaleTools.HasPerformance());                
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //Exhausted
            var PurgingFinaleExhausted = Helpers.CreateBlueprint<BlueprintAbility>("PurgingFinaleExhausted", bp => {
                bp.SetName("Purging Finale - Exhausted");
                bp.SetDescription("You must have a bardic performance in effect to cast this spell. With a flourish, you immediately end your bardic performance, " +
                    "removing the exhausted condition on a creature within range affected by your bardic performance." +
                    "\nYou cannot activate another bardic performance this round. If you have the lingering performance feat the effect lingers as normal.");
                bp.m_Icon = PurgingFinaleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionDispelMagic() {
                            m_StopAfterCountRemoved = false,
                            m_CountToRemove = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 1,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_BuffType = ContextActionDispelMagic.BuffType.All,
                            m_MaxSpellLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_UseMaxCasterLevel = false,
                            m_MaxCasterLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_CheckType = RuleDispelMagic.CheckType.None,
                            m_Skill = StatType.Unknown,
                            CheckBonus = 0,
                            ContextBonus = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            Descriptor = SpellDescriptor.Exhausted,
                            OnSuccess = Helpers.CreateActionList(),
                            OnFail = Helpers.CreateActionList(),
                            OnlyTargetEnemyBuffs = false,
                            CheckSchoolOrDescriptor = false
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerformanceCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.One,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                },
                                FinaleTools.RemovePerformance()
                            )
                        }
                        );
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PerformanceCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent(FinaleTools.HasPerformance());
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = RestorationLesserFX.PrefabLink;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.m_Parent = PurgingFinale.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //Paralyzed
            var PurgingFinaleParalyzed = Helpers.CreateBlueprint<BlueprintAbility>("PurgingFinaleParalyzed", bp => {
                bp.SetName("Purging Finale - Paralyzed");
                bp.SetDescription("You must have a bardic performance in effect to cast this spell. With a flourish, you immediately end your bardic performance, " +
                    "removing the paralyzed condition on a creature within range affected by your bardic performance." +
                    "\nYou cannot activate another bardic performance this round. If you have the lingering performance feat the effect lingers as normal.");
                bp.m_Icon = PurgingFinaleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDispelMagic() {
                            m_StopAfterCountRemoved = false,
                            m_CountToRemove = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 1,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_BuffType = ContextActionDispelMagic.BuffType.All,
                            m_MaxSpellLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_UseMaxCasterLevel = false,
                            m_MaxCasterLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_CheckType = RuleDispelMagic.CheckType.None,
                            m_Skill = StatType.Unknown,
                            CheckBonus = 0,
                            ContextBonus = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            Descriptor = SpellDescriptor.Paralysis,
                            OnSuccess = Helpers.CreateActionList(),
                            OnFail = Helpers.CreateActionList(),
                            OnlyTargetEnemyBuffs = false,
                            CheckSchoolOrDescriptor = false
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerformanceCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.One,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                },
                                FinaleTools.RemovePerformance()
                            )
                        }
                        );
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PerformanceCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent(FinaleTools.HasPerformance());
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = RestorationLesserFX.PrefabLink;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.m_Parent = PurgingFinale.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //Shaken
            var PurgingFinaleShaken = Helpers.CreateBlueprint<BlueprintAbility>("PurgingFinaleShaken", bp => {
                bp.SetName("Purging Finale - Shaken");
                bp.SetDescription("You must have a bardic performance in effect to cast this spell. With a flourish, you immediately end your bardic performance, " +
                    "removing the shaken condition on a creature within range affected by your bardic performance." +
                    "\nYou cannot activate another bardic performance this round. If you have the lingering performance feat the effect lingers as normal.");
                bp.m_Icon = PurgingFinaleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDispelMagic() {
                            m_StopAfterCountRemoved = false,
                            m_CountToRemove = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 1,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_BuffType = ContextActionDispelMagic.BuffType.All,
                            m_MaxSpellLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_UseMaxCasterLevel = false,
                            m_MaxCasterLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_CheckType = RuleDispelMagic.CheckType.None,
                            m_Skill = StatType.Unknown,
                            CheckBonus = 0,
                            ContextBonus = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            Descriptor = SpellDescriptor.Shaken,
                            OnSuccess = Helpers.CreateActionList(),
                            OnFail = Helpers.CreateActionList(),
                            OnlyTargetEnemyBuffs = false,
                            CheckSchoolOrDescriptor = false
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerformanceCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.One,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                },
                                FinaleTools.RemovePerformance()
                            )
                        }
                        );
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PerformanceCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent(FinaleTools.HasPerformance());
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = RestorationLesserFX.PrefabLink;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.m_Parent = PurgingFinale.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //Stunned
            var PurgingFinaleStunned = Helpers.CreateBlueprint<BlueprintAbility>("PurgingFinaleStunned", bp => {
                bp.SetName("Purging Finale - Stunned");
                bp.SetDescription("You must have a bardic performance in effect to cast this spell. With a flourish, you immediately end your bardic performance, " +
                    "removing the stunned condition on a creature within range affected by your bardic performance." +
                    "\nYou cannot activate another bardic performance this round. If you have the lingering performance feat the effect lingers as normal.");
                bp.m_Icon = PurgingFinaleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDispelMagic() {
                            m_StopAfterCountRemoved = false,
                            m_CountToRemove = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 1,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_BuffType = ContextActionDispelMagic.BuffType.All,
                            m_MaxSpellLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_UseMaxCasterLevel = false,
                            m_MaxCasterLevel = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_CheckType = RuleDispelMagic.CheckType.None,
                            m_Skill = StatType.Unknown,
                            CheckBonus = 0,
                            ContextBonus = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            Descriptor = SpellDescriptor.Stun,
                            OnSuccess = Helpers.CreateActionList(),
                            OnFail = Helpers.CreateActionList(),
                            OnlyTargetEnemyBuffs = false,
                            CheckSchoolOrDescriptor = false
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerformanceCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.One,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                },
                                FinaleTools.RemovePerformance()
                            )
                        }
                        );
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PerformanceCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent(FinaleTools.HasPerformance());
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = RestorationLesserFX.PrefabLink;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.m_Parent = PurgingFinale.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //Dazzled
            var DazzledBuff = Resources.GetBlueprint<BlueprintBuff>("df6d1025da07524429afbae248845ecc");
            var PurgingFinaleDazzled = Helpers.CreateBlueprint<BlueprintAbility>("PurgingFinaleDazzled", bp => {
                bp.SetName("Purging Finale - Dazzled");
                bp.SetDescription("You must have a bardic performance in effect to cast this spell. With a flourish, you immediately end your bardic performance, " +
                    "removing the dazzled condition on a creature within range affected by your bardic performance." +
                    "\nYou cannot activate another bardic performance this round. If you have the lingering performance feat the effect lingers as normal.");
                bp.m_Icon = PurgingFinaleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = DazzledBuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerformanceCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.One,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    }
                                },
                                FinaleTools.RemovePerformance()
                            )
                        }
                        );
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PerformanceCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent(FinaleTools.HasPerformance());
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = RestorationLesserFX.PrefabLink;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.m_Parent = PurgingFinale.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            PurgingFinale.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    PurgingFinaleDazzled.ToReference<BlueprintAbilityReference>(),
                    PurgingFinaleExhausted.ToReference<BlueprintAbilityReference>(),
                    PurgingFinaleParalyzed.ToReference<BlueprintAbilityReference>(),
                    PurgingFinaleShaken.ToReference<BlueprintAbilityReference>(),
                    PurgingFinaleStunned.ToReference<BlueprintAbilityReference>()
                };
            });
            var PurgingFinaleScroll = ItemTools.CreateScroll("ScrollOfPurgingFinale", Icon_ScrollOfPurgingFinale, PurgingFinale, 3, 7);
            VenderTools.AddScrollToLeveledVenders(PurgingFinaleScroll);
            PurgingFinale.AddToSpellList(SpellTools.SpellList.BardSpellList, 3);
        }
    }
}
