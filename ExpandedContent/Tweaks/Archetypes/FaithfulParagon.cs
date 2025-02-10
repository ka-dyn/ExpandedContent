using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System.Collections.Generic;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Craft;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using ExpandedContent.Tweaks.Components;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Blueprints.Classes.Prerequisites;
using UnityEngine;
using Kingmaker.UnitLogic.ActivatableAbilities;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class FaithfulParagon {
        public static void AddFaithfulParagon() {

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestSpelllist = Resources.GetBlueprint<BlueprintSpellList>("c5a1b8df32914d74c9b44052ba3e686a");
            var WarpriestSpellLevels = Resources.GetBlueprint<BlueprintSpellsTable>("c73a394ec54adc243aef8ac967e39324");
            var WarpriestFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("303fd456ddb14437946e344bad9a893b");
            var WarpriestChannelSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("1a871294410971745bba12ef11e41f6e");
            var ScriptureIcon = AssetLoader.LoadInternal("Skills", "Icon_Scripture.png");
            var DivineBodyguardIcon = Resources.GetBlueprint<BlueprintActivatableAbility>("14a9c13a2fb063b4ca9f883e00e0c3d0").Icon;
            var DivineInHarmsWayIcon = Resources.GetBlueprint<BlueprintActivatableAbility>("d98c7711030ab6b4fa8347fc933fec72").Icon;


            var FaithfulParagonArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("FaithfulParagonArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"FaithfulParagonArchetype.Name", "Faithful Paragon");
                bp.LocalizedDescription = Helpers.CreateString($"FaithfulParagonArchetype.Description", "For warprieists that find themselves fighting back to back with paladins, " +
                    "the call to take up additional oaths in service to their god is often expected. Those who do gain the power to manifest their faith in battle granting them " +
                    "enhanced vitality. Their strong devotion grants them the title of faithful paragon.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"FaithfulParagonArchetype.Description", "For warprieists that find themselves fighting back to back with paladins, " +
                    "the call to take up additional oaths in service to their god is often expected. Those who do gain the power to manifest their faith in battle granting them " +
                    "enhanced vitality. Their strong devotion grants them the title of faithful paragon.");

                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = true;
                    c.HideInUI = false;
                    c.Alignment = Kingmaker.UnitLogic.Alignments.AlignmentMaskType.LawfulGood;
                });
            });

            #region SpellBook
            var FaithfulParagonSpelllist = Helpers.CreateBlueprint<BlueprintSpellList>("FaithfulParagonSpelllist", bp => {//Fill in mod support to get all spells
                bp.IsMythic = false;
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                };
            });
            var FaithfulParagonSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("FaithfulParagonSpellbook", bp => {
                bp.Name = Helpers.CreateString($"FaithfulParagonSpellbook.Name", "Faithful Paragon");
                bp.m_SpellsPerDay = WarpriestSpellLevels.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = FaithfulParagonSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Wisdom;
                bp.AllSpellsKnown = true;
                bp.CantripsType = CantripsType.Orisions;
                bp.CasterLevelModifier = 0;
                bp.IsArcane = false;
            });
            FaithfulParagonArchetype.m_ReplaceSpellbook = FaithfulParagonSpellbook.ToReference<BlueprintSpellbookReference>();
            var FaithfulParagonSpellsFeature = Helpers.CreateBlueprint<BlueprintFeature>("FaithfulParagonSpellsFeature", bp => {
                bp.SetName("Paragon Spells");
                bp.SetDescription("A faithful paragon casts spell drawn from the paladin list in addition to the spells from the cleric list normally available to a warpriest. " +
                    "If a spell appears on both lists the faithful paragon may prepare it at the lowest of the two spell levels presented.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = ScriptureIcon;
            });
            #endregion
            #region Fervor
            var WarpriestFervorResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("da0fb35828917f344b1cd72c98b70498");
            var FaithfulParagonFervorFeature = Helpers.CreateBlueprint<BlueprintFeature>("FaithfulParagonFervorFeature", bp => {
                bp.SetName("Paragons Fervor");
                bp.SetDescription("A faithful paragon adds his Wisdom bonus (minimum 1) to his daily uses of fervor. ");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmountBySharedValue>(c => {
                    c.m_Resource = WarpriestFervorResource;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.Decrease = false;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Stat = StatType.Wisdom;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
            });
            #endregion
            #region Faith Sustains 
            var FaithfulParagonFaithSustainsBuff = Helpers.CreateBuff("FaithfulParagonFaithSustainsBuff", bp => {
                bp.SetName("Faith Sustains");
                bp.SetDescription("At 3rd level, a faithful paragon draws upon his convictions to sustain him in battle. As a move action, by expending a use of fervor " +
                    "he gains a +2 morale bonus to constitution for a number of rounds equal to his faithful paragon level + his constitution modifier. " +
                    "The morale bonus increases to +4 at 9th level, and to +6 at 15th level.");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.Constitution;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.Morale;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 8, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 4 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 6 }
                    };
                });
                bp.m_Icon = DivineBodyguardIcon;
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });

            var FaithfulParagonFaithSustainsAbility = Helpers.CreateBlueprint<BlueprintAbility>("FaithfulParagonFaithSustainsAbility", bp => {
                bp.SetName("Faith Sustains");
                bp.SetDescription("At 3rd level, a faithful paragon draws upon his convictions to sustain him in battle. As a move action, by expending a use of fervor " +
                    "he gains a +2 morale bonus to constitution for a number of rounds equal to his faithful paragon level + his constitution modifier. " +
                    "The morale bonus increases to +4 at 9th level, and to +6 at 15th level.");
                bp.m_Icon = DivineBodyguardIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FaithfulParagonFaithSustainsBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Shared,
                                    ValueShared = AbilitySharedValue.Duration
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WarpriestFervorResource;
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Stat = StatType.Constitution;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Duration;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.StatBonus
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("FaithfulParagonFaithSustainsAbility.Duration", "1 round/level + constitution modifier");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var FaithfulParagonFaithSustainsFeature = Helpers.CreateBlueprint<BlueprintFeature>("FaithfulParagonFaithSustainsFeature", bp => {
                bp.SetName("Faith Sustains");
                bp.SetDescription("At 3rd level, a faithful paragon draws upon his convictions to sustain him in battle. As a move action, by expending a use of fervor " +
                    "he gains a +2 morale bonus to constitution for a number of rounds equal to his faithful paragon level + his constitution modifier. " +
                    "The morale bonus increases to +4 at 9th level, and to +6 at 15th level.");
                bp.m_Icon = DivineBodyguardIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FaithfulParagonFaithSustainsAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            #endregion

            #region Divine Conviction


            var FaithfulParagonDivineConvictionBuff = Helpers.CreateBuff("FaithfulParagonDivineConvictionBuff", bp => {
                bp.SetName("Divine Conviction");
                bp.SetDescription("At 4th level, a faithful paragon may expend a use of fervor as a free action to add his charisma bonus (if any) " +
                    "to the next saving throw he makes. \nThis effect is expended after a saving throw, or during a long rest.");
                bp.AddComponent<ModifyD20>(c => {
                    c.Rule = RuleType.SavingThrow;
                    c.DispellMagicCheckType = RuleDispelMagic.CheckType.None;
                    c.RollsAmount = 0;
                    c.TakeBest = false;
                    c.RollResult = new ContextValue();
                    c.AddBonus = true;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.BonusDescriptor = ModifierDescriptor.UntypedStackable;
                    c.WithChance = false;
                    c.Chance = new ContextValue();
                    c.RerollOnlyIfFailed = false;
                    c.RerollOnlyIfSuccess = false;
                    c.RollCondition = ModifyD20.RollConditionType.None;
                    c.ValueToCompareRoll = new ContextValue();
                    c.DispellOnRerollFinished = true;
                    c.DispellOn20 = false;
                    c.AgainstAlignment = false;
                    c.Alignment = AlignmentComponent.None;
                    c.TargetAlignment = false;
                    c.SpecificSkill = true;
                    c.Skill = new StatType[] { StatType.SkillPersuasion };
                    c.Value = new ContextValue();
                    c.ActionsToTrigger = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Min = 0;
                    c.m_UseMin = true;
                });
                bp.m_Icon = DivineInHarmsWayIcon;
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            });

            var FaithfulParagonDivineConvictionAbility = Helpers.CreateBlueprint<BlueprintAbility>("FaithfulParagonDivineConvictionAbility", bp => {
                bp.SetName("Divine Conviction");
                bp.SetDescription("At 4th level, a faithful paragon may expend a use of fervor as a free action to add his charisma bonus (if any) " +
                    "to the next saving throw he makes. \nThis effect is expended after a saving throw, or during a long rest.");
                bp.m_Icon = DivineInHarmsWayIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FaithfulParagonDivineConvictionBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {                                
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WarpriestFervorResource;
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var FaithfulParagonDivineConvictionFeature = Helpers.CreateBlueprint<BlueprintFeature>("FaithfulParagonDivineConvictionFeature", bp => {
                bp.SetName("Divine Conviction");
                bp.SetDescription("At 4th level, a faithful paragon may expend a use of fervor as a free action to add his charisma bonus (if any) " +
                    "to the next saving throw he makes. \nThis effect is expended after a saving throw, or during a long rest.");
                bp.m_Icon = DivineInHarmsWayIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FaithfulParagonDivineConvictionAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            #endregion


            FaithfulParagonArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(3, WarpriestFeatSelection),
                    Helpers.LevelEntry(4, WarpriestChannelSelection),
                    Helpers.LevelEntry(9, WarpriestFeatSelection),
                    Helpers.LevelEntry(15, WarpriestFeatSelection)
            };
            FaithfulParagonArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, FaithfulParagonSpellsFeature),
                    Helpers.LevelEntry(2, FaithfulParagonFervorFeature),
                    Helpers.LevelEntry(3, FaithfulParagonFaithSustainsFeature),
                    Helpers.LevelEntry(4, FaithfulParagonDivineConvictionFeature)
            };

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Faithful Paragon")) { return; }
            WarpriestClass.m_Archetypes = WarpriestClass.m_Archetypes.AppendToArray(FaithfulParagonArchetype.ToReference<BlueprintArchetypeReference>());
        }

    }
}
