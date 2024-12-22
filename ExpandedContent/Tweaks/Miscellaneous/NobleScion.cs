using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Designers.Mechanics.Facts;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class NobleScion {
        public static void AddNobleScion() {


            var BardicPerformanceResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("e190ba276831b5c4fa28737e5e49e6a6");
            var RagingSongResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("4a2302c4ec2cfb042bba67d825babfec");
            var FakeCelebrityResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("63c6df02067d42dd94fe65a0fc4ec696");
            var ScribingScrollsFeature = Resources.GetBlueprint<BlueprintFeature>("a8a385bf53ee3454593ce9054375a2ec");
            var RemoveFearSpell = Resources.GetBlueprint<BlueprintAbility>("55a037e514c0ee14a8e3ed14b47061de");


            var NobleScionOfTheArtsFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfTheArtsFeature", bp => {
                bp.SetName("Noble Scion of the Arts");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks, and this skill is always considered a class skill for you. " +
                    "\r\nIf you have the bardic performance ability, you can use that ability for an additional 3 rounds per day.");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = BardicPerformanceResource;
                    c.Value = 3;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = RagingSongResource;
                    c.Value = 3;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = FakeCelebrityResource;
                    c.Value = 3;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionOfLoreFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfLoreFeature", bp => {
                bp.SetName("Noble Scion of Lore");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks, and this skill is always considered a class skill for you. " +
                    "\r\nYou gain a +1 bonus on all Knowledge and Lore skills.");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 3;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 1;
                    c.Stat = StatType.SkillKnowledgeArcana;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 1;
                    c.Stat = StatType.SkillLoreNature;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 1;
                    c.Stat = StatType.SkillLoreReligion;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionOfMagicFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfMagicFeature", bp => {
                bp.SetName("Noble Scion of Magic");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks, and this skill is always considered a class skill for you. " +
                    "\r\nYou gain a +2 bonus on {g|Encyclopedia:Use_Magic_Device}Use Magic Device{/g} checks, along with the ability to craft scrolls during rest.");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillUseMagicDevice;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ScribingScrollsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionOfPrideSuppresion = Helpers.CreateBuff("NobleScionOfPrideSuppresion", bp => {
                bp.SetName("Noble Scion of Pride Rest Buff");
                bp.SetDescription("Once per day when affected by a fear effect, you suppress any fear effects affecting you for a number of rounds equal to half your character level (minimum 1) " +
                    "even if you could not normally take actions due to your fear.");
                bp.m_Icon = RemoveFearSpell.m_Icon;
                bp.AddComponent<SuppressBuffs>(c => {
                    c.Descriptor = SpellDescriptor.Frightened | SpellDescriptor.Shaken;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;

            });
            var NobleScionOfPrideBuff = Helpers.CreateBuff("NobleScionOfPrideBuff", bp => {
                bp.SetName("Noble Scion of Pride Rest Buff");
                bp.SetDescription("");
                bp.AddComponent<AddConditionTrigger>(c => {
                    c.m_TriggerType = AddConditionTrigger.TriggerType.OnConditionAdded;
                    c.Conditions = new UnitCondition[] {
                        UnitCondition.Frightened,
                        UnitCondition.Shaken
                    };
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = NobleScionOfPrideSuppresion.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            AsChild = true
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;

            });
            var NobleScionOfPrideFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfPrideFeature", bp => {
                bp.SetName("Noble Scion of Pride");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks, and this skill is always considered a class skill for you. " +
                    "\r\nOnce per day when affected by a fear effect, you suppress any fear effects affecting you for a number of rounds equal to half your character level " +
                    "(minimum 1) even if you could not normally take actions due to your fear.");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddRestTrigger>(c => {
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = NobleScionOfPrideBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            },
                            AsChild = true
                        }
                    );
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionOfWarFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfWarFeature", bp => {
                bp.SetName("Noble Scion of War");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks, and this skill is always considered a class skill for you. " +
                    "\r\nYou use your Charisma modifier to adjust Initiative checks instead of your Dexterity modifier.");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.Initiative;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.Initiative;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.DamageBonus
                    };
                    c.Multiplier = -1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = false;
                    c.m_Min = 0;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Dexterity;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = false;
                    c.m_Min = 0;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Dexterity;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("NobleScionFeatureSelection", bp => {
                bp.SetName("Noble Scion");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks, and this skill is always considered a class skill for you. " +
                    "\nWhen you select this feat, choose one of the benefits that matches the flavor of your noble family.");
                bp.AddComponent<PrerequisiteCharacterIsFirstLevel>(c => { c.HideInUI = true; });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Stat = StatType.Charisma;
                    c.Value = 13;                    
                });                
                bp.m_Features = new BlueprintFeatureReference[] {
                    NobleScionOfTheArtsFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfLoreFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfMagicFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfPrideFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfWarFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    NobleScionOfTheArtsFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfLoreFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfMagicFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfPrideFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfWarFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Feat,
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });

            if (ModSettings.AddedContent.Feats.IsDisabled("Noble Scion")) { return; }
            FeatTools.AddAsFeat(NobleScionFeatureSelection);
        }
    }
}
