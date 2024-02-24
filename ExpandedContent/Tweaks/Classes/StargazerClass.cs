using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using Kingmaker.Formations.Facts;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Craft;

namespace ExpandedContent.Tweaks.Classes {
    internal class StargazerClass {
        public static void AddStargazerClass() {


            var BABMedium = Resources.GetBlueprint<BlueprintStatProgression>("4c936de4249b61e419a3fb775b9f2581");
            var SavesPrestigeHigh = Resources.GetBlueprint<BlueprintStatProgression>("1f309006cd2855e4e91a6c3707f3f700");
            var SavesPrestigeLow= Resources.GetBlueprint<BlueprintStatProgression>("dc5257e1100ad0d48b8f3b9798421c72");
            var PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");

            var StargazerProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerProgression", bp => {
                bp.SetName("Stargazer");
                bp.SetDescription("The empyreal lord Pulura, the Shimmering Maiden, is said to dance among the lights " +
                    "of the aurora. Some of her worshipers seek her guidance in the starry skies, attuning themselves to the constellations of the Cosmic Caravan. Beyond the " +
                    "stargazers’ interests in the heavens, though, they also train for the inevitable conflict with demons. Pulura’s bastion of worship in the Inner Sea region " +
                    "has traditionally been in the land of Sarkoris, which was consumed from within by the Worldwound at the advent of the Age of Lost Omens. Many of Pulura’s " +
                    "worshipers perished during that disastrous invasion from the Abyss, but those who survived remained behind and, over the past several decades, have grown in power.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var StargazerClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("StargazerClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"StargazerClass.Name", "Stargazer");
                bp.LocalizedDescription = Helpers.CreateString($"StargazerClass.Description", "The empyreal lord Pulura, the Shimmering Maiden, is said to dance among the lights " +
                    "of the aurora. Some of her worshipers seek her guidance in the starry skies, attuning themselves to the constellations of the Cosmic Caravan. Beyond the " +
                    "stargazers’ interests in the heavens, though, they also train for the inevitable conflict with demons. Pulura’s bastion of worship in the Inner Sea region " +
                    "has traditionally been in the land of Sarkoris, which was consumed from within by the Worldwound at the advent of the Age of Lost Omens. Many of Pulura’s " +
                    "worshipers perished during that disastrous invasion from the Abyss, but those who survived remained behind and, over the past several decades, have grown in power.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"StargazerClass.Description", "The empyreal lord Pulura, the Shimmering Maiden, is said to dance among the lights " +
                    "of the aurora. Some of her worshipers seek her guidance in the starry skies, attuning themselves to the constellations of the Cosmic Caravan. Beyond the " +
                    "stargazers’ interests in the heavens, though, they also train for the inevitable conflict with demons. Pulura’s bastion of worship in the Inner Sea region " +
                    "has traditionally been in the land of Sarkoris, which was consumed from within by the Worldwound at the advent of the Age of Lost Omens. Many of Pulura’s " +
                    "worshipers perished during that disastrous invasion from the Abyss, but those who survived remained behind and, over the past several decades, have grown in power.");
                bp.SkillPoints = 4;
                bp.HitDie = Kingmaker.RuleSystem.DiceType.D6;
                bp.HideIfRestricted = false;
                bp.PrestigeClass = true;
                bp.IsMythic = false;                
                bp.m_BaseAttackBonus = BABMedium.ToReference<BlueprintStatProgressionReference>();
                bp.m_FortitudeSave = SavesPrestigeLow.ToReference<BlueprintStatProgressionReference>();                
                bp.m_ReflexSave = SavesPrestigeHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_WillSave = SavesPrestigeHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_Progression = StargazerProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Spellbook = null;
                bp.ClassSkills = new StatType[] {
                    StatType.SkillMobility,
                    StatType.SkillAthletics,
                    StatType.SkillPersuasion,
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillPerception,
                    StatType.SkillLoreReligion,
                    StatType.SkillLoreNature,
                };
                bp.IsDivineCaster = false;
                bp.IsArcaneCaster = false;
                bp.StartingGold = 0;
                bp.m_StartingItems = new BlueprintItemReference[] {};
                bp.PrimaryColor = 0;
                bp.SecondaryColor = 0;
                bp.m_EquipmentEntities = new KingmakerEquipmentEntityReference[] {};
                bp.MaleEquipmentEntities = new Kingmaker.ResourceLinks.EquipmentEntityLink[] {};
                bp.FemaleEquipmentEntities = new Kingmaker.ResourceLinks.EquipmentEntityLink[] {};
                bp.m_Difficulty = 1;
                bp.RecommendedAttributes = new StatType[] {};
                bp.NotRecommendedAttributes = new StatType[] {};
                bp.m_DefaultBuild = null;
                bp.m_Archetypes = new BlueprintArchetypeReference[] { };
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = PuluraFeature.ToReference<BlueprintFeatureReference>();
                    c.Group = Prerequisite.GroupType.All;
                });
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 5;
                });
                bp.AddComponent<PrerequisiteCasterTypeSpellLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.IsArcane = false;
                    c.OnlySpontaneous = false;
                    c.RequiredSpellLevel = 3;
                });
                bp.AddComponent<PrerequisiteCasterTypeSpellLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.IsArcane = true;
                    c.OnlySpontaneous = false;
                    c.RequiredSpellLevel = 3;
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.Not = true;
                    c.HideInUI = true;
                });
            });
            StargazerProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                new BlueprintProgression.ClassWithLevel {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                }
            };
            //Guiding Light
            var WitchFamiliarSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("29a333b7ccad3214ea3a51943fa0d8e9");
            var StargazerGuidingLightFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StargazerGuidingLightFeatureSelection", bp => {
                bp.SetName("Guiding Light");
                bp.SetDescription("At 1st level the stargazer gains familiar. A familiar is a magic pet that enhances the stargazer's {g|Encyclopedia:Skills}skills{/g} and senses.");
                bp.m_Icon = WitchFamiliarSelection.m_Icon;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.Familiar;
                bp.m_AllFeatures = WitchFamiliarSelection.m_AllFeatures;
            });
            #region Hex Stuff
            //Mystery Magic - Hex
            var WitchHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("9846043cf51251a4897728ed6e24e76f");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var MagusClass = Resources.GetBlueprint<BlueprintCharacterClass>("45a4607686d96a1498891b3286121780");
            var RogueClass = Resources.GetBlueprint<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");
            var WinterWitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("eb24ca44debf6714aabe1af1fd905a07");
            var HexCrafterArchetype = Resources.GetBlueprint<BlueprintArchetype>("79ccf7a306a5d5547bebd97299f6fc89");
            var SylvanTricksterArchetype = Resources.GetBlueprint<BlueprintArchetype>("490394869f666c141bf8647b1a365220");
            var WitchHexDCProperty = Resources.GetBlueprint<BlueprintUnitProperty>("bdc230ce338f427ba74de65597b0d57a");
            var WitchHexCasterLevelProperty = Resources.GetBlueprint<BlueprintUnitProperty>("2d2243f4f3654512bdda92e80ef65b6d");
            var WitchHexSpellLevelProperty = Resources.GetBlueprint<BlueprintUnitProperty>("75efe8b64a3a4cd09dda28cef156cfb5");
            //Guiding Star
            var GuidingStarIcon = AssetLoader.LoadInternal("Skills", "Icon_GuidingStar.png");
            var WitchHexGuidingStarSkillBuff = Helpers.CreateBuff("WitchHexGuidingStarSkillBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillLoreNature;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillLoreReligion;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillPerception;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var WitchHexGuidingStarSkillFeature = Helpers.CreateBlueprint<BlueprintFeature>("WitchHexGuidingStarSkillFeature", bp => {
                bp.SetName("Guiding Star - Skill Bonus");
                bp.SetDescription("The stars themselves hold many answers, you may add your Charisma modifier to your Wisdom modifier on all Wisdom-based checks.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WitchHexGuidingStarSkillBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });
            var WitchHexGuidingStarResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("WitchHexGuidingStarResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            var WitchHexGuidingStarMetamagicBuffEmpower = Helpers.CreateBuff("WitchHexGuidingStarMetamagicBuffEmpower", bp => {
                bp.SetName("Guiding Star - Empower Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Empower Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Empower;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = false;
                    c.m_Spellbook = new BlueprintSpellbookReference();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = true;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var WitchHexGuidingStarMetamagicBuffExtend = Helpers.CreateBuff("WitchHexGuidingStarMetamagicBuffExtend", bp => {
                bp.SetName("Guiding Star - Extend Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Extend;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = false;
                    c.m_Spellbook = new BlueprintSpellbookReference();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = true;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var WitchHexGuidingStarMetamagicAbilityBase = Helpers.CreateBlueprint<BlueprintAbility>("WitchHexGuidingStarMetamagicAbilityBase", bp => {
                bp.SetName("Guiding Star");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Empower Spell or Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WitchHexGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WitchHexGuidingStarMetamagicAbilityEmpower = Helpers.CreateBlueprint<BlueprintAbility>("WitchHexGuidingStarMetamagicAbilityEmpower", bp => {
                bp.SetName("Guiding Star - Empower Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Empower Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WitchHexGuidingStarMetamagicBuffEmpower.ToReference<BlueprintBuffReference>(),
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
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WitchHexGuidingStarMetamagicBuffEmpower.ToReference<BlueprintUnitFactReference>(),
                        WitchHexGuidingStarMetamagicBuffExtend.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WitchHexGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WitchHexGuidingStarMetamagicAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WitchHexGuidingStarMetamagicAbilityExtend = Helpers.CreateBlueprint<BlueprintAbility>("WitchHexGuidingStarMetamagicAbilityExtend", bp => {
                bp.SetName("Guiding Star - Extend Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WitchHexGuidingStarMetamagicBuffExtend.ToReference<BlueprintBuffReference>(),
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
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WitchHexGuidingStarMetamagicBuffEmpower.ToReference<BlueprintUnitFactReference>(),
                        WitchHexGuidingStarMetamagicBuffExtend.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WitchHexGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = WitchHexGuidingStarMetamagicAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            WitchHexGuidingStarMetamagicAbilityBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WitchHexGuidingStarMetamagicAbilityEmpower.ToReference<BlueprintAbilityReference>(),
                    WitchHexGuidingStarMetamagicAbilityExtend.ToReference<BlueprintAbilityReference>()
                };
            });
            var WitchHexGuidingStarFeature = Helpers.CreateBlueprint<BlueprintFeature>("WitchHexGuidingStarFeature", bp => {
                bp.SetName("Guiding Star");
                bp.SetDescription("The stars themselves hold many answers, you may add your Charisma modifier to your Wisdom modifier on all Wisdom-based checks. In addition, once per day you can " +
                    "cast one spell as if it were modified by the Empower Spell or Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WitchHexGuidingStarSkillFeature.ToReference<BlueprintUnitFactReference>(),
                        WitchHexGuidingStarMetamagicAbilityBase.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WitchHexGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WitchHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideNotAvailibleInUI = true;
            });
            WitchTools.RegisterWitchHex(WitchHexGuidingStarFeature);
            //Lure of the Heavens
            var LureOfTheHeavensIcon = AssetLoader.LoadInternal("Skills", "Icon_LureOfTheHeavens.png");
            var WitchHexLureOfTheHeavensResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("WitchHexLureOfTheHeavensResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    LevelIncrease = 1,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 1,
                    StartingIncrease = 1,
                    LevelStep = 0,
                    PerStepIncrease = 1,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                    m_Class = new BlueprintCharacterClassReference[] {
                        WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        MagusClass.ToReference<BlueprintCharacterClassReference>(),
                        RogueClass.ToReference<BlueprintCharacterClassReference>(),
                        WinterWitchClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        HexCrafterArchetype.ToReference<BlueprintArchetypeReference>(),
                        SylvanTricksterArchetype.ToReference<BlueprintArchetypeReference>()
                    }
                };
                bp.m_UseMax = true;
                bp.m_Max = 20;
            });
            var WitchHexLureOfTheHeavensHoverFeature = Helpers.CreateBlueprint<BlueprintFeature>("WitchHexLureOfTheHeavensHoverFeature", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("");
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Ground;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var WitchHexLureOfTheHeavensFlyBuff = Helpers.CreateBuff("WitchHexLureOfTheHeavensFlyBuff", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your shaman level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = LureOfTheHeavensIcon;
                bp.AddComponent<ACBonusAgainstAttacks>(c => {
                    c.AgainstMeleeOnly = true;
                    c.AgainstRangedOnly = false;
                    c.OnlySneakAttack = false;
                    c.NotTouch = false;
                    c.IsTouch = false;
                    c.OnlyAttacksOfOpportunity = false;
                    c.Value = new ContextValue();
                    c.ArmorClassBonus = 3;
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.CheckArmorCategory = false;
                    c.NoShield = false;
                });
                bp.AddComponent<FormationACBonus>(c => {
                    c.UnitProperty = false;
                    c.Bonus = 3;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var WitchHexLureOfTheHeavensFlyAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("WitchHexLureOfTheHeavensFlyAbility", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your shaman level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = LureOfTheHeavensIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = WitchHexLureOfTheHeavensResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = WitchHexLureOfTheHeavensFlyBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var WitchHexLureOfTheHeavensFlyFeature = Helpers.CreateBlueprint<BlueprintFeature>("WitchHexLureOfTheHeavensFlyFeature", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your shaman level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WitchHexLureOfTheHeavensFlyAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var WitchHexLureOfTheHeavensProgression = Helpers.CreateBlueprint<BlueprintProgression>("WitchHexLureOfTheHeavensProgression", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = MagusClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = RogueClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WinterWitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = HexCrafterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = SylvanTricksterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, WitchHexLureOfTheHeavensHoverFeature),
                    Helpers.LevelEntry(10, WitchHexLureOfTheHeavensFlyFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var WitchHexLureOfTheHeavensFeature = Helpers.CreateBlueprint<BlueprintFeature>("WitchHexLureOfTheHeavensFeature", bp => {
                bp.SetName("Lure of the Heavens");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your shaman level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = LureOfTheHeavensIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillStealth;
                    c.Value = 1;
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = WitchHexLureOfTheHeavensProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WitchHexLureOfTheHeavensResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WitchHex };
                bp.HideNotAvailibleInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            WitchTools.RegisterWitchHex(WitchHexLureOfTheHeavensFeature);
            //Starburn
            var MageLightBuff = Resources.GetBlueprint<BlueprintBuff>("571baa4cf65bbcb4996fe429ca77d1a5");
            var WitchHexStarburnResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("WitchHexStarburnResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                };
                bp.m_Min = 1;
            });
            var WitchHexStarburnBuff = Helpers.CreateBuff("WitchHexStarburnBuff", bp => {
                bp.SetName("Starburn");
                bp.SetDescription("As a standard action, the stargazer causes one creature within 30 feet to burn like a star. The creature takes 1d6 points of fire damage for every " +
                    "2 levels the stargazer possesses and emits bright light for 1 round. A successful Fortitude saving throw halves the damage and negates the emission of bright light. " +
                    "The Witch can use this hex a number of times per day equal to her Charisma modifier (minimum 1), but must wait 1d4 rounds between uses.");
                bp.m_Icon = MageLightBuff.Icon;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.GreaterInvisibility;
                });
                bp.FxOnStart = new PrefabLink() { AssetId = "72938ec0a6e4a10459ea374d65aecfa5" };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var WitchHexStarburnAbility = Helpers.CreateBlueprint<BlueprintAbility>("WitchHexStarburnAbility", bp => {
                bp.SetName("Starburn");
                bp.SetDescription("As a standard action, the stargazer causes one creature within 30 feet to burn like a star. The creature takes 1d6 points of fire damage for every " +
                    "2 levels the stargazer possesses and emits bright light for 1 round. A successful Fortitude saving throw halves the damage and negates the emission of bright light. " +
                    "The stargazer can use this hex a number of times per day equal to her Charisma modifier (minimum 1), but must wait 1d4 rounds between uses.");
                bp.m_Icon = MageLightBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
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
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = true,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WitchHexStarburnBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WitchHexStarburnResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        MagusClass.ToReference<BlueprintCharacterClassReference>(),
                        RogueClass.ToReference<BlueprintCharacterClassReference>(),
                        WinterWitchClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = SylvanTricksterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { HexCrafterArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.AddComponent<ContextSetAbilityParams>(c => {
                    c.Add10ToDC = false;
                    c.DC = new ContextValue() {
                        ValueType = ContextValueType.CasterCustomProperty,
                        m_CustomProperty = WitchHexDCProperty.ToReference<BlueprintUnitPropertyReference>()
                    };
                    c.CasterLevel = new ContextValue() {
                        ValueType = ContextValueType.CasterCustomProperty,
                        m_CustomProperty = WitchHexCasterLevelProperty.ToReference<BlueprintUnitPropertyReference>()
                    };
                    c.Concentration = 0;
                    c.SpellLevel = new ContextValue() {
                        ValueType = ContextValueType.CasterCustomProperty,
                        m_CustomProperty = WitchHexSpellLevelProperty.ToReference<BlueprintUnitPropertyReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("WitchHexStarburnAbility.SavingThrow", "Fortitude partial");
            });
            var WitchHexStarburnFeature = Helpers.CreateBlueprint<BlueprintFeature>("WitchHexStarburnFeature", bp => {
                bp.SetName("Starburn");
                bp.SetDescription("As a standard action, the stargazer causes one creature within 30 feet to burn like a star. The creature takes 1d6 points of fire damage for every " +
                    "2 levels the stargazer possesses and emits bright light for 1 round. A successful Fortitude saving throw halves the damage and negates the emission of bright light. " +
                    "The stargazer can use this hex a number of times per day equal to her Charisma modifier (minimum 1), but must wait 1d4 rounds between uses.");
                bp.m_Icon = MageLightBuff.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WitchHexStarburnAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WitchHexStarburnResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WitchHex };
                bp.HideNotAvailibleInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            WitchTools.RegisterWitchHex(WitchHexStarburnFeature);
            var StargazerMysteryMagicHexFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StargazerMysteryMagicHexFeatureSelection", bp => {
                bp.SetName("Mystery Magic - Hex");
                bp.SetDescription("At 1st, 7th and 9th levels, the stargazer gains a hex from the witch’s list of hexes. His stargazer levels count as (and stack with) witch levels when " +
                    "determining the effects of hexes. In addition, the stargazer adds all hexes available to a shaman with the heavens spirit to the witch list.");
                bp.m_Icon = WitchHexSelection.m_Icon;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.WitchHex;
                bp.m_AllFeatures = WitchHexSelection.m_AllFeatures;
            });
            //Hooking Stargazer into the shared witchhex params
            var WitchHexDCPropertyLevel = Resources.GetBlueprint<BlueprintUnitProperty>("bdc230ce338f427ba74de65597b0d57a").GetComponent<SummClassLevelGetter>();
            WitchHexDCPropertyLevel.m_Class = WitchHexDCPropertyLevel.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var WitchHexCasterLevelPropertyLevel = Resources.GetBlueprint<BlueprintUnitProperty>("2d2243f4f3654512bdda92e80ef65b6d").GetComponent<SummClassLevelGetter>();
            WitchHexCasterLevelPropertyLevel.m_Class = WitchHexCasterLevelPropertyLevel.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var WitchHexSpellLevelPropertyLevel = Resources.GetBlueprint<BlueprintUnitProperty>("75efe8b64a3a4cd09dda28cef156cfb5").GetComponent<SummClassLevelGetter>();
            WitchHexSpellLevelPropertyLevel.m_Class = WitchHexSpellLevelPropertyLevel.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Hooking Stargazer into the ContextRankConfigs of the witchhexs
            WitchTools.AddClassToHexConfigs(StargazerClass);
            #endregion
            #region Domain Stuff
            //Hooking Stargazer into the Domains
            //Domains
            var AirDomainProgression = Resources.GetBlueprint<BlueprintProgression>("750bfcd133cd52f42acbd4f7bc9cc365");
            var AirDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("d7169e8978d9e9d418398eab946c49e5");
            var AnimalDomainProgression = Resources.GetBlueprint<BlueprintProgression>("23d2f87aa54c89f418e68e790dba11e0");
            var AnimalDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("f13eb6be93dd5234c8126e5384040009");
            var ArtificeDomainProgression = Resources.GetBlueprint<BlueprintProgression>("6454b37f50e10ae41bca83aaaa81ffc2");
            var ArtificeDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("988f3e10cc2fd8f48915acdc640a65b3");
            var ChaosDomainProgression = Resources.GetBlueprint<BlueprintProgression>("5a5d19c246961484a97e1e5dded98ab2");
            var ChaosDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("85e8db7e938d4f947a084a21d3535adf");
            var CharmDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b5c056787d1bf544588ec3a150ed0b3b");
            var CharmDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("242eba70a5e2317479de39de3c1e64ad");
            var CommunityDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b8bbe42616d61ac419971b7910d79fc8");
            var CommunityDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("3a397e27682edfd409cb73ff12de7c51");
            var DarknessDomainProgression = Resources.GetBlueprint<BlueprintProgression>("1e1b4128290b11a41ba55280ede90d7d");
            var DarknessDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("bfdc224a1362f2b4688b57f70adcc26f");
            var DeathDomainProgression = Resources.GetBlueprint<BlueprintProgression>("710d8c959e7036448b473ffa613cdeba");
            var DeathDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("023794a8386506c49aad142846700594");
            var DestructionDomainProgression = Resources.GetBlueprint<BlueprintProgression>("269ff0bf4596f5248864bc2653a2f0e0");
            var DestructionDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("8edced7121849414f8b1dc77a119b4a2");
            var EarthDomainProgression = Resources.GetBlueprint<BlueprintProgression>("08bbcbfe5eb84604481f384c517ac800");
            var EarthDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("4132a011b835a36479d6bc19a1b962e6");
            var EvilDomainProgression = Resources.GetBlueprint<BlueprintProgression>("a8936d29b6051a1418682da1878b644e");
            var EvilDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("82b654d68ea6ce143be5f7df646d6385");
            var FireDomainProgression = Resources.GetBlueprint<BlueprintProgression>("881b2137a1779294c8956fe5b497cc35");
            var FireDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("2bd6aa3c4979fd045bbbda8da586d3fb");
            var GloryDomainProgression = Resources.GetBlueprint<BlueprintProgression>("f0a61a043bcdf0f4c8efc59962afafb8");
            var GloryDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("434531fa0827f4c4a97482ffc71e7234");
            var GoodDomainProgression = Resources.GetBlueprint<BlueprintProgression>("243ab3e7a86d30243bdfe79c83e6adb4");
            var GoodDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("efc4219c7894afc438180737adc0b7ac");
            var HealingDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b0a26ee984b6b6945b884467aa2f1baa");
            var HealingDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("599fb0d60358c354d8c5c4304a73e19a");
            var KnowledgeDomainProgression = Resources.GetBlueprint<BlueprintProgression>("28edbdbefca579b4ab4992e98af71981");
            var KnowledgeDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("72c0d2f6379114947a6072278baedb90");
            var LawDomainProgression = Resources.GetBlueprint<BlueprintProgression>("a723d11a5ae5df0488775e31fac9117d");
            var LawDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("0d9749df9d68ded438ecdf8527085963");
            var LiberationDomainProgression = Resources.GetBlueprint<BlueprintProgression>("df2f14ced8710664ba7db914880c4a02");
            var LiberationDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("34b0e4bb90e3a4f4183b095f0d44ca5d");
            var LuckDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8bd8cfad69085654b9118534e4aa215e");
            var LuckDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("1ba7fc652568a524db218ccff2f9ed90");
            var MadnessDomainProgression = Resources.GetBlueprint<BlueprintProgression>("9ebe166b9b901c746b1858029f13a2c5");
            var MadnessDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("ae9c936c86d248848b2fb90b32b3b41d");
            var MagicDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8f90e7129b0f3b742921c2c9c9bd64fc");
            var MagicDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("9c5053b1ad83a9742839b3ab824abbd2");
            var NobilityDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8480f2d1ca764774895ee6fd610a568e");
            var NobilityDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("6bddbc86f5ee80146ac0fc51923ef4fd");
            var PlantDomainProgression = Resources.GetBlueprint<BlueprintProgression>("467d2a1d2107da64395b591393baad17");
            var PlantDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("22f3a592849c06e4c8869e2132e11597");
            var ProtectionDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b750650400d9d554b880dbf4c8347b24");
            var ProtectionDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("6be37e1316160d949aa2e5a0be99404a");
            var ReposeDomainProgression = Resources.GetBlueprint<BlueprintProgression>("a2ab5a696d0dd134d94b2631151a15ee");
            var ReposeDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("a12fa2f458841ca4bb4ef666e1fbceef");
            var RuneDomainProgression = Resources.GetBlueprint<BlueprintProgression>("6d4dac497c182754d8b1f49071cca3fd");
            var RuneDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("8d176f8fe5616a64ca37835be7c2ccfe");
            var StrenthDomainProgression = Resources.GetBlueprint<BlueprintProgression>("07854f99c8d029b4cbfdf6ae6c7bc452");
            var StrenthDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("2ed973db1af2c8e428ce404fb1e9a20d");
            var SunDomainProgression = Resources.GetBlueprint<BlueprintProgression>("c85c8791ee13d4c4ea10d93c97a19afc");
            var SunDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("0877f33e01ba9884daed94cc7633e09c");
            var TravelDomainProgression = Resources.GetBlueprint<BlueprintProgression>("d169dd2de3630b749a2363c028bb6e7b");
            var TravelDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("9dc676c4ab1d0c643bb2293696375fcf");
            var TrickeryDomainProgression = Resources.GetBlueprint<BlueprintProgression>("cc2d330bb0200e840aeb79140e770198");
            var TrickeryDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("720cb8ed96386d24d8dfe38cad153cbd");
            var WarDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8d454cbb7f25070419a1c8eaf89b5be5");
            var WarDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("2d2b61d86f93ce84f8375f9e59489072");
            var WaterDomainProgression = Resources.GetBlueprint<BlueprintProgression>("e63d9133cebf2cf4788e61432a939084");
            var WaterDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("f05fb4f417465f94fb1b4d6c48ea42cf");
            var WeatherDomainProgression = Resources.GetBlueprint<BlueprintProgression>("c18a821ee662db0439fb873165da25be");
            var WeatherDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("d124d29c7c96fc345943dd17e24990e8");
            //Allowing Progression on normal Domains
            AirDomainProgression.m_Classes = AirDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });            
            AnimalDomainProgression.m_Classes = AnimalDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            ArtificeDomainProgression.m_Classes = ArtificeDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            CharmDomainProgression.m_Classes = CharmDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            CommunityDomainProgression.m_Classes = CommunityDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            DarknessDomainProgression.m_Classes = DarknessDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            DeathDomainProgression.m_Classes = DeathDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            DestructionDomainProgression.m_Classes = DestructionDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            EarthDomainProgression.m_Classes = EarthDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            FireDomainProgression.m_Classes = FireDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            GloryDomainProgression.m_Classes = GloryDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            GoodDomainProgression.m_Classes = GoodDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            HealingDomainProgression.m_Classes = HealingDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            KnowledgeDomainProgression.m_Classes = KnowledgeDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LawDomainProgression.m_Classes = LawDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LiberationDomainProgression.m_Classes = LiberationDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LuckDomainProgression.m_Classes = LuckDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MadnessDomainProgression.m_Classes = MadnessDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MagicDomainProgression.m_Classes = MagicDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            NobilityDomainProgression.m_Classes = NobilityDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            PlantDomainProgression.m_Classes = PlantDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ProtectionDomainProgression.m_Classes = ProtectionDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ReposeDomainProgression.m_Classes = ReposeDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            RuneDomainProgression.m_Classes = RuneDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            StrenthDomainProgression.m_Classes = StrenthDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            SunDomainProgression.m_Classes = SunDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TravelDomainProgression.m_Classes = TravelDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TrickeryDomainProgression.m_Classes = TrickeryDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WarDomainProgression.m_Classes = WarDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WaterDomainProgression.m_Classes = WaterDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WeatherDomainProgression.m_Classes = WeatherDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });

            //Allowing Prgression on Secondary Domains
            AirDomainProgressionSecondary.m_Classes = AirDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            AnimalDomainProgressionSecondary.m_Classes = AnimalDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ArtificeDomainProgressionSecondary.m_Classes = ArtificeDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            CharmDomainProgressionSecondary.m_Classes = CharmDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            CommunityDomainProgressionSecondary.m_Classes = CommunityDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            DarknessDomainProgressionSecondary.m_Classes = DarknessDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            DeathDomainProgressionSecondary.m_Classes = DeathDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            DestructionDomainProgressionSecondary.m_Classes = DestructionDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            EarthDomainProgressionSecondary.m_Classes = EarthDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            FireDomainProgressionSecondary.m_Classes = FireDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            GloryDomainProgressionSecondary.m_Classes = GloryDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            GoodDomainProgressionSecondary.m_Classes = GoodDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            HealingDomainProgressionSecondary.m_Classes = HealingDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            KnowledgeDomainProgressionSecondary.m_Classes = KnowledgeDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LawDomainProgressionSecondary.m_Classes = LawDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LiberationDomainProgressionSecondary.m_Classes = LiberationDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LuckDomainProgressionSecondary.m_Classes = LuckDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MadnessDomainProgressionSecondary.m_Classes = MadnessDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MagicDomainProgressionSecondary.m_Classes = MagicDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            NobilityDomainProgressionSecondary.m_Classes = NobilityDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            PlantDomainProgressionSecondary.m_Classes = PlantDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ProtectionDomainProgressionSecondary.m_Classes = ProtectionDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ReposeDomainProgressionSecondary.m_Classes = ReposeDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            RuneDomainProgressionSecondary.m_Classes = RuneDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            StrenthDomainProgressionSecondary.m_Classes = StrenthDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            SunDomainProgressionSecondary.m_Classes = SunDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TravelDomainProgressionSecondary.m_Classes = TravelDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TrickeryDomainProgressionSecondary.m_Classes = TrickeryDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WarDomainProgressionSecondary.m_Classes = WarDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WaterDomainProgressionSecondary.m_Classes = WaterDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WeatherDomainProgressionSecondary.m_Classes = WeatherDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            //Air Domain Hooks
            var AirDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("39b0c7db785560041b436b558c9df2bb").GetComponent<AddFeatureOnClassLevel>();
            AirDomainBaseFeatureConfig.m_AdditionalClasses = AirDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var AirDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("b3494639791901e4db3eda6117ad878f").GetComponent<ContextRankConfig>();
            AirDomainBaseAbilityConfig.m_Class = AirDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Animal Domain Hooks
            var AnimalDomainBaseFeatureComp0 = Resources.GetBlueprint<BlueprintFeature>("d577aba79b5727a4ab74627c4c6ba23c").GetComponent<AddFeatureOnClassLevel>();
            AnimalDomainBaseFeatureComp0.m_AdditionalClasses = AnimalDomainBaseFeatureComp0.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var AnimalDomainBaseFeatureComp2 = Resources.GetBlueprint<BlueprintFeature>("d577aba79b5727a4ab74627c4c6ba23c").GetComponent<ContextRankConfig>();
            AnimalDomainBaseFeatureComp2.m_Class = AnimalDomainBaseFeatureComp2.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var DomainAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("125af359f8bc9a145968b5d8fd8159b8");
            DomainAnimalCompanionProgression.m_Classes = DomainAnimalCompanionProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = -3
                });
            //Artifice Domain Hooks
            var ArtificeDomainBaseAuraConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("f042f2d62e6785d4e8612a027de1f298").GetComponent<ContextRankConfig>();
            ArtificeDomainBaseAuraConfig.m_Class = ArtificeDomainBaseAuraConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ArtificeDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("01025d876ac28d349ac42d69ba462059").GetComponent<AddFeatureOnClassLevel>();
            ArtificeDomainBaseFeatureConfig.m_AdditionalClasses = ArtificeDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ArtificeDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("d2c3c7c7efbc71c438dc4e0c3f216407");
            ArtificeDomainBaseResource.m_MaxAmount.m_Class = ArtificeDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ArtificeDomainGreaterEffectConfig = Resources.GetBlueprint<BlueprintBuff>("9d4a139cb5605fa409b1be3ad6e87ba9").GetComponent<ContextRankConfig>();
            ArtificeDomainGreaterEffectConfig.m_Class = ArtificeDomainGreaterEffectConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Charm Domain Hooks
            var CharmDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("84cd24a110af59140b066bc2c69619bd").GetComponent<ContextRankConfig>();
            CharmDomainBaseAbilityConfig.m_Class = CharmDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var CharmDomainBaseFeatureComnfig = Resources.GetBlueprint<BlueprintFeature>("4847d450fbef9b444abcc3a82337b426").GetComponent<AddFeatureOnClassLevel>();
            CharmDomainBaseFeatureComnfig.m_AdditionalClasses = CharmDomainBaseFeatureComnfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var CharmDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("d49f0e3460fd52d4e9660a8ce52142a0");
            CharmDomainGreaterResource.m_MaxAmount.m_Class = CharmDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Community Domain Hooks
            var CommunityDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("b1b8efd70ba5dd84aa6985d46dc299d5").GetComponent<ContextRankConfig>();
            CommunityDomainBaseAbilityConfig.m_Class = CommunityDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var CommunityDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("102d61a114786894bb2b30568943ef1f").GetComponent<AddFeatureOnClassLevel>();
            CommunityDomainBaseFeatureConfig.m_AdditionalClasses = CommunityDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var CommunityDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("76291e62d2496ad41824044aba3077ea").GetComponent<ContextRankConfig>();
            CommunityDomainGreaterAbilityConfig.m_Class = CommunityDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Darkness Domain Hooks
            var DarknessDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("39ed9d4b1e033e042aac4f9eb9c7315f").GetComponent<ContextRankConfig>();
            DarknessDomainBaseAbilityConfig.m_Class = DarknessDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var DarknessDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9dc5863168155854fa8daf4a780f6663").GetComponent<AddFeatureOnClassLevel>();
            DarknessDomainBaseFeatureConfig.m_AdditionalClasses = DarknessDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //If multiple Components with same name use ".ForEach"
            var DarknessDomainGreaterAbility = Resources.GetBlueprint<BlueprintAbility>("31acd268039966940872c916782ae018");
            DarknessDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var DarknessDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("55efb511a2290b94bb218e2d56a51f1f");
            DarknessDomainGreaterResource.m_MaxAmount.m_ClassDiv = DarknessDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Death Domain Hooks
            var DeathDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("979f63920af22344d81da5099c9ec32e").GetComponent<ContextRankConfig>();
            DeathDomainBaseAbilityConfig.m_Class = DeathDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var DeathDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9809efa15e5f9ad478594479af575a5d").GetComponent<AddFeatureOnClassLevel>();
            DeathDomainBaseFeatureConfig.m_AdditionalClasses = DeathDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Destruction Domain Hooks
            var DestructionDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("0dfe08afb3cf3594987bab12d014e74b").GetComponent<ContextRankConfig>();
            DestructionDomainBaseBuffConfig.m_Class = DestructionDomainBaseBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var DestructionDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("2d3b9491bc05a114ab10e5b1b30dc86a").GetComponent<AddFeatureOnClassLevel>();
            DestructionDomainBaseFeatureConfig.m_AdditionalClasses = DestructionDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var DestructionDomainGreaterEffectConfig = Resources.GetBlueprint<BlueprintBuff>("f9de414e53a9c23419fa3cfc0daabde7").GetComponent<ContextRankConfig>();
            DestructionDomainGreaterEffectConfig.m_Class = DestructionDomainGreaterEffectConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var DestructionDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("2f9b00619b54bed4ba0c3b02298f9c34").GetComponent<ContextRankConfig>();
            DestructionDomainGreaterAbilityConfig.m_Class = DestructionDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var DestructionDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("98f07eabe9cb4f34cb1127de625f4bee");
            DestructionDomainGreaterResource.m_MaxAmount.m_Class = DestructionDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Earth Domain Hooks
            var EarthDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("828d82a0e8c5a944bbdb6b12f802ff02").GetComponent<AddFeatureOnClassLevel>();
            EarthDomainBaseFeatureConfig.m_AdditionalClasses = EarthDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var EarthDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("3ff40918d33219942929f0dbfe5d1dee").GetComponent<ContextRankConfig>();
            EarthDomainBaseAbilityConfig.m_Class = EarthDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Fire Domain Hooks
            var FireDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("42cc125d570c5334c89c6499b55fc0a3").GetComponent<AddFeatureOnClassLevel>();
            FireDomainBaseFeatureConfig.m_AdditionalClasses = FireDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var FireDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("4ecdf240d81533f47a5279f5075296b9").GetComponent<ContextRankConfig>();
            FireDomainBaseAbilityConfig.m_Class = FireDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Glory Domain Hooks
            var GloryDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("55edcfff497a1e04a963f72c485da5cb").GetComponent<ContextRankConfig>();
            GloryDomainBaseBuffConfig.m_Class = GloryDomainBaseBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var GloryDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("d018241b5a761414897ad6dc4df2db9f").GetComponent<ContextRankConfig>();
            GloryDomainBaseAbilityConfig.m_Class = GloryDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var GloryDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("17e891b3964492f43aae44f994b5d454").GetComponent<AddFeatureOnClassLevel>();
            GloryDomainBaseFeatureConfig.m_AdditionalClasses = GloryDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var GloryDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c89e92387e940e541b02c1969cd1fe2a").GetComponent<ContextRankConfig>();
            GloryDomainGreaterAbilityConfig.m_Class = GloryDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Good Domain Hooks
            var GoodDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("f185e4585bda72b479956772944ee665").GetComponent<ContextRankConfig>();
            GoodDomainBaseBuffConfig.m_Class = GoodDomainBaseBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var GoodDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("f27684b3b72c2f546abf3ef2fb611a05").GetComponent<AddFeatureOnClassLevel>();
            GoodDomainBaseFeatureConfig.m_AdditionalClasses = GoodDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var GoodDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("017afe6934e10c3489176e759a5f01b0").GetComponent<ContextRankConfig>();
            GoodDomainBaseAbilityConfig.m_Class = GoodDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var GoodDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("7fc3e743ba28fd64f977fb55b7536053").GetComponent<ContextRankConfig>();
            GoodDomainGreaterAbilityConfig.m_Class = GoodDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var GoodDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("8d45a527ce4d3ec47853faaa972c2362");
            GoodDomainGreaterResource.m_MaxAmount.m_ClassDiv = GoodDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Healing Domain Hooks
            var HealingDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("18f734e40dd7966438ab32086c3574e1").GetComponent<ContextRankConfig>();
            HealingDomainBaseAbilityConfig.m_Class = HealingDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var HealingdDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("303cf1c933f343c4d91212f8f4953e3c").GetComponent<AddFeatureOnClassLevel>();
            HealingdDomainBaseFeatureConfig.m_AdditionalClasses = HealingdDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Knowledge Domain Hooks
            var KnowledgeDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("35fa55fe2c60e4442b670a88a70c06c3").GetComponent<ContextRankConfig>();
            KnowledgeDomainBaseBuffConfig.m_Class = KnowledgeDomainBaseBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgeDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("02a79a205bce6f5419dcdf26b64f13c6").GetComponent<ContextRankConfig>();
            KnowledgeDomainBaseAbilityConfig.m_Class = KnowledgeDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgedDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("5335f015063776d429a0b5eab97eb060");
            KnowledgedDomainBaseFeatureConfig.GetComponents<AddFeatureOnClassLevel>().ForEach(c => {
                c.m_AdditionalClasses = c.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var KnowledgeDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("f88f616a4b6bd5f419025115c52cb329");
            KnowledgeDomainBaseResource.m_MaxAmount.m_Class = KnowledgeDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgeDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("ec582b195ccb2ef4ea8dcd96a5a6e009").GetComponent<ContextRankConfig>();
            KnowledgeDomainGreaterAbilityConfig.m_Class = KnowledgeDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgesDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("34f0a288ff5106645a88440b800686ca");
            KnowledgesDomainGreaterResource.m_MaxAmount.m_ClassDiv = KnowledgesDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Law Domain Hooks
            var LawDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9bd2d216e56a0db44be0df48ffc515af").GetComponent<AddFeatureOnClassLevel>();
            LawDomainBaseFeatureConfig.m_AdditionalClasses = LawDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var LawDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("0b1615ec2dabc6f4294a254b709188a4").GetComponent<ContextRankConfig>();
            LawDomainGreaterAbilityConfig.m_Class = LawDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var LawDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("de7945c4cc6a0a24790941d7e2b85838");
            LawDomainGreaterResource.m_MaxAmount.m_ClassDiv = LawDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Liberation Domain Hooks
            var LiberationDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("7cc934aa505172a40b4a10c14c7681c4").GetComponent<AddFeatureOnClassLevel>();
            LiberationDomainBaseFeatureConfig.m_AdditionalClasses = LiberationDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var LiberationDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("8ddc7f532cf2b3b4c877497856cc5b97");
            LiberationDomainBaseResource.m_MaxAmount.m_Class = LiberationDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var LiberationDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("d19e900012a69954c93f3b7533bc3911");
            LiberationDomainGreaterResource.m_MaxAmount.m_Class = LiberationDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Luck Domain Hooks
            var LuckDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("2b3818bf4656c1a41b93467755662c78").GetComponent<AddFeatureOnClassLevel>();
            LuckDomainBaseFeatureConfig.m_AdditionalClasses = LuckDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var LuckDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("0e0668a703fbfcf499d9aa9d918b71ea").GetComponent<ContextRankConfig>();
            LuckDomainGreaterAbilityConfig.m_Class = LuckDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var LuckDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("b209ca75fbea5144c9d73ecb29055a08");
            LuckDomainGreaterResource.m_MaxAmount.m_ClassDiv = LuckDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Madness Domain Hooks
            var MadnessDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("84bf46e8086dbdc438bac875ab0e5c2f").GetComponent<AddFeatureOnClassLevel>();
            MadnessDomainBaseFeatureConfig.m_AdditionalClasses = MadnessDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("a3d470a27ec5e4540aeaf9723e9b8ae7").GetComponent<ContextRankConfig>();
            MadnessDomainGreaterAbilityConfig.m_Class = MadnessDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("3289ee86c57f6134d81770865c315e8b");
            MadnessDomainGreaterResource.m_MaxAmount.m_ClassDiv = MadnessDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAttackRollsBuffConfig = Resources.GetBlueprint<BlueprintBuff>("6c69ec7a32190d44d99e746588de4a9c").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAttackRollsBuffConfig.m_Class = MadnessDomainBaseAttackRollsBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseSkillChecksBuffConfig = Resources.GetBlueprint<BlueprintBuff>("3e42877e5e481894880df63ad924e320").GetComponent<ContextRankConfig>();
            MadnessDomainBaseSkillChecksBuffConfig.m_Class = MadnessDomainBaseSkillChecksBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseSavingThrowsBuffConfig = Resources.GetBlueprint<BlueprintBuff>("53c721d7519ac3047b818516bb28b20f").GetComponent<ContextRankConfig>();
            MadnessDomainBaseSavingThrowsBuffConfig.m_Class = MadnessDomainBaseSavingThrowsBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAbilitySkillChecksConfig = Resources.GetBlueprint<BlueprintAbility>("d92b2eac4dbf31f439e5bc9d2d467ff1").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAbilitySkillChecksConfig.m_Class = MadnessDomainBaseAbilitySkillChecksConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAbilityAttackRollsConfig = Resources.GetBlueprint<BlueprintAbility>("c3e4ff89950f1d748be6f5958b1aa19c").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAbilityAttackRollsConfig.m_Class = MadnessDomainBaseAbilityAttackRollsConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAbilitySavingThrowsConfig = Resources.GetBlueprint<BlueprintAbility>("c09446b861bac7b4b83877db863150d9").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAbilitySavingThrowsConfig.m_Class = MadnessDomainBaseAbilitySavingThrowsConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Magic Domain Hooks
            var MagicDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("90f939eb611ac3743b5de3dd00135e22").GetComponent<AddFeatureOnClassLevel>();
            MagicDomainBaseFeatureConfig.m_AdditionalClasses = MagicDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MagicDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("da9e93791894b9d49a1f2bebd80e8085");
            MagicDomainBaseResource.m_MaxAmount.m_ClassDiv = MagicDomainBaseResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var MagicDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("3aecc0c5d17390443b30774309145854");
            MagicDomainGreaterResource.m_MaxAmount.m_ClassDiv = MagicDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Nobility Domain Hooks
            var NobilityDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("7a305ef528cb7884385867a2db410102").GetComponent<ContextRankConfig>();
            NobilityDomainBaseAbilityConfig.m_Class = NobilityDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a1a7f3dd904ed8e45b074232f48190d1").GetComponent<AddFeatureOnClassLevel>();
            NobilityDomainBaseFeatureConfig.m_AdditionalClasses = NobilityDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("3fc6e1f3acbcb0e4c83badf7709ce53d");
            NobilityDomainBaseResource.m_MaxAmount.m_Class = NobilityDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("2972215a5367ae44b8ddfe435a127a6e").GetComponent<ContextRankConfig>();
            NobilityDomainGreaterAbilityConfig.m_Class = NobilityDomainGreaterAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("cb3efe82596c908418c0dba4ef6f4210");
            NobilityDomainGreaterResource.m_MaxAmount.m_ClassDiv = NobilityDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Plant Domain Hooks
            var PlantDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("e433267d36089d049b34900fde38032b").GetComponent<AddFeatureOnClassLevel>();
            PlantDomainBaseFeatureConfig.m_AdditionalClasses = PlantDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var PlantDomainGreaterBuffConfig = Resources.GetBlueprint<BlueprintBuff>("58d86cc848805024abbbefd6abe2d433").GetComponent<ContextRankConfig>();
            PlantDomainGreaterBuffConfig.m_Class = PlantDomainGreaterBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var PlantDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("8942e816a533a4a40b04745c516d085a");
            PlantDomainBaseResource.m_MaxAmount.m_Class = PlantDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var PlantDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("881d696940ec99041aefafd5b2fda189");
            PlantDomainGreaterResource.m_MaxAmount.m_Class = PlantDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Protection Domain Hooks
            var ProtectionDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("2ddb4cfc3cfd04c46a66c6cd26df1c06").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseBuffConfig.m_Class = ProtectionDomainBaseBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c5815bd0bf87bdb4fa9c440c8088149b").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseAbilityConfig.m_Class = ProtectionDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a05a8959c594daa40a1c5add79566566").GetComponent<AddFeatureOnClassLevel>();
            ProtectionDomainBaseFeatureConfig.m_AdditionalClasses = ProtectionDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainBaseSelfBuffConfig = Resources.GetBlueprint<BlueprintBuff>("74a4fb45f23705d4db2784d16eb93138").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseSelfBuffConfig.m_Class = ProtectionDomainBaseSelfBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainGreaterEffect = Resources.GetBlueprint<BlueprintBuff>("fea7c44605c90f14fa40b2f2f5ae6339");
            ProtectionDomainGreaterEffect.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var ProtectionDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("f3d878f77d0ee854b864f5ea1c80e752");
            ProtectionDomainGreaterResource.m_MaxAmount.m_Class = ProtectionDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Repose Domain Hooks
            var ReposeDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("8526bc808c303034cb2b7832bccf1482").GetComponent<AddFeatureOnClassLevel>();
            ReposeDomainBaseFeatureConfig.m_AdditionalClasses = ReposeDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var ReposeDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("aefe627a3a2f8d94ea9d2b3961261282");
            ReposeDomainGreaterResource.m_MaxAmount.m_Class = ReposeDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Rune Domain Hooks
            var RuneDomainGreaterAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("e26de8b0164db23458eb64c21fac2846").GetComponent<ContextRankConfig>();
            RuneDomainGreaterAreaConfig.m_Class = RuneDomainGreaterAreaConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("b74c64a0152c7ee46b13ecdd72dda6f3").GetComponent<AddFeatureOnClassLevel>();
            RuneDomainBaseFeatureConfig.m_AdditionalClasses = RuneDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainGreaterAbility = Resources.GetBlueprint<BlueprintAbility>("9171a3ce8ea8cac44894b240709804ce");
            RuneDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("cb3efe82596c908418c0dba4ef6f4210");
            RuneDomainGreaterResource.m_MaxAmount.m_ClassDiv = RuneDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityFire = Resources.GetBlueprint<BlueprintAbility>("eddfe26a8a3892b47add3cb08db7069d");
            RuneDomainBaseAbilityFire.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityCold = Resources.GetBlueprint<BlueprintAbility>("2b81ff42fcbe9434eaf00fb0a873f579");
            RuneDomainBaseAbilityCold.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityAcid = Resources.GetBlueprint<BlueprintAbility>("92c821ecc8d73564bad15a8a07ed40f2");
            RuneDomainBaseAbilityAcid.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityFireAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("9b786945d2ec1884184235a488e5cb9e").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityFireAreaConfig.m_Class = RuneDomainBaseAbilityFireAreaConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityColdAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("8b8e98e8e0000f643ad97c744f3f850b").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityColdAreaConfig.m_Class = RuneDomainBaseAbilityColdAreaConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityAcidAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("98c3a36f2a3636c49a3f77c001a25f29").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityAcidAreaConfig.m_Class = RuneDomainBaseAbilityAcidAreaConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityElectricity = Resources.GetBlueprint<BlueprintAbility>("b67978e3d5a6c9247a393237bc660339");
            RuneDomainBaseAbilityElectricity.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityElectricityAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("db868c576c69d0e4a8462645267c6cdc").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityElectricityAreaConfig.m_Class = RuneDomainBaseAbilityElectricityAreaConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Strength Domain Hooks
            var StrengthDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("94dfcf5f3a72ce8478c8de5db69e752b").GetComponent<ContextRankConfig>();
            StrengthDomainBaseBuffConfig.m_Class = StrengthDomainBaseBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var StrengthDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("1d6364123e1f6a04c88313d83d3b70ee").GetComponent<ContextRankConfig>();
            StrengthDomainBaseAbilityConfig.m_Class = StrengthDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var StrengthDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("526f99784e9fe4346824e7f210d46112").GetComponent<AddFeatureOnClassLevel>();
            StrengthDomainBaseFeatureConfig.m_AdditionalClasses = StrengthDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var StrengthDomainGreaterFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3298fd30e221ef74189a06acbf376d29").GetComponent<ContextRankConfig>();
            StrengthDomainGreaterFeatureConfig.m_Class = StrengthDomainGreaterFeatureConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Sun Domain Hooks
            var SunDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3d8e38c9ed54931469281ab0cec506e9").GetComponent<AddFeatureOnClassLevel>();
            SunDomainBaseFeatureConfig.m_AdditionalClasses = SunDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var SunDomainBaseFeatureDamageConfig = Resources.GetBlueprint<BlueprintFeature>("3d8e38c9ed54931469281ab0cec506e9").GetComponent<IncreaseSpellDamageByClassLevel>();
            SunDomainBaseFeatureDamageConfig.m_AdditionalClasses = SunDomainBaseFeatureDamageConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var SunDomainGreaterAuraConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("cfe8c5683c759f047a56a4b5e77ac93f").GetComponent<ContextRankConfig>();
            SunDomainGreaterAuraConfig.m_Class = SunDomainGreaterAuraConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var SunDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("6bea29e2257fa6742923ba757435aba8");
            SunDomainGreaterResource.m_MaxAmount.m_Class = SunDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            SunDomainGreaterResource.m_MaxAmount.m_ClassDiv = SunDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Travel Domain Hooks
            var TravelDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3079cdfba971d614ab4f49220c6cd228").GetComponent<AddFeatureOnClassLevel>();
            TravelDomainBaseFeatureConfig.m_AdditionalClasses = TravelDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var TravelDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("52ee1ad8d1ac94d4b92a62acfa8931ad");
            TravelDomainBaseResource.m_MaxAmount.m_Class = TravelDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var TravelDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("657bfb21544642e4f8aef532c9f04ac2");
            TravelDomainGreaterResource.m_MaxAmount.m_Class = TravelDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            TravelDomainGreaterResource.m_MaxAmount.m_ClassDiv = TravelDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Trickery Domain Hooks
            var TrickeryDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("ee7eb5b9c644a0347b36eec653d3dfcb").GetComponent<ContextRankConfig>();
            TrickeryDomainBaseAbilityConfig.m_Class = TrickeryDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var TrickeryDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("cd1f4a784e0820647a34fe9bd5ffa770").GetComponent<AddFeatureOnClassLevel>();
            TrickeryDomainBaseFeatureConfig.m_AdditionalClasses = TrickeryDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var TrickeryDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("148c9ad7e47f4284b9c3686bb440c08c");
            TrickeryDomainBaseResource.m_MaxAmount.m_Class = TrickeryDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var TrickeryDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("520ad6381e09f8349a237ac4b247082e");
            TrickeryDomainGreaterResource.m_MaxAmount.m_Class = TrickeryDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            TrickeryDomainGreaterResource.m_MaxAmount.m_ClassDiv = TrickeryDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //War Domain Hooks
            var WarDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("aefec65136058694ab20cd71941eec81").GetComponent<ContextRankConfig>();
            WarDomainBaseBuffConfig.m_Class = WarDomainBaseBuffConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Removed in update
            //var WarDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("fbef6b2053ab6634a82df06f76c260e3").GetComponent<ContextRankConfig>();
            //WarDomainBaseAbilityConfig.m_Class = WarDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var WarDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("640c20da7d6fcbc43b0d30a0a762f122").GetComponent<AddFeatureOnClassLevel>();
            WarDomainBaseFeatureConfig.m_AdditionalClasses = WarDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Water Domain Hooks
            var WaterDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("4c21ad24f55f64d4fb722f40720d9ab0").GetComponent<AddFeatureOnClassLevel>();
            WaterDomainBaseFeatureConfig.m_AdditionalClasses = WaterDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var WaterDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("5e1db2ef80ff361448549beeb7785791").GetComponent<ContextRankConfig>();
            WaterDomainBaseAbilityConfig.m_Class = WaterDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            //Weather Domain Hooks
            var WeatherDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("1c37869ee06ca33459f16f23f4969e7d").GetComponent<AddFeatureOnClassLevel>();
            WeatherDomainBaseFeatureConfig.m_AdditionalClasses = WeatherDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var WeatherDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("f166325c271dd29449ba9f98d11542d9").GetComponent<ContextRankConfig>();
            WeatherDomainBaseAbilityConfig.m_Class = WeatherDomainBaseAbilityConfig.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var WeatherDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("53dd76c7053469541b99e01cb25711d6");
            WeatherDomainBaseResource.m_MaxAmount.m_Class = WeatherDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            var WeatherDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("5c88b557e79eaee41a4190712b178970");
            WeatherDomainGreaterResource.m_MaxAmount.m_Class = WeatherDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            WeatherDomainGreaterResource.m_MaxAmount.m_ClassDiv = WeatherDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(StargazerClass.ToReference<BlueprintCharacterClassReference>());
            #endregion
            var StargazerMysteryMagicStarsDomainBackupDomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StargazerMysteryMagicStarsDomainBackupDomainSelection", bp => {
                bp.SetName("Pulura's Domains");
                bp.SetDescription("If the stargazer already has the stars subdomain he may select another domain granted by Pulura. His stargazer levels count as (and stack with) cleric " +
                    "levels when determining which domain abilities he gains and their effects. ");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AirDomainProgressionSecondary.ToReference<BlueprintFeatureReference>(),
                    ChaosDomainProgressionSecondary.ToReference<BlueprintFeatureReference>(),
                    GoodDomainProgressionSecondary.ToReference<BlueprintFeatureReference>(),
                    WeatherDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
                };
            });
            var StargazerMysteryMagicStarsDomainFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StargazerMysteryMagicStarsDomainFeatureSelection", bp => {
                bp.SetName("Mystery Magic - Stars Subdomain");
                bp.SetDescription("At 3rd level, the stargazer gains the stars subdomain in addition to any domains he already has, if he already has the stars subdomain he may select another domain granted " +
                    "by Pulura. His stargazer levels count as (and stack with) cleric levels when determining which domain abilities he gains and their effects. He can use the stars are right ability regardless " +
                    "of his spellcasting class.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    StargazerMysteryMagicStarsDomainBackupDomainSelection.ToReference<BlueprintFeatureReference>() //Starsdomain added on it's own page
                };
            });
            //Mystery Magic - Coat of many Stars
            var EdictOfImpenetrableFortress = Resources.GetBlueprint<BlueprintAbility>("d7741c08ccf699e4a8a8f8ab2ed345f8");
            var StargazerMysteryMagicCOMSResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("StargazerMysteryMagicCOMSResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    LevelIncrease = 1,
                    StartingIncrease = 1,
                };
            });
            var StargazerMysteryMagicCOMSBuff = Helpers.CreateBuff("StargazerMysteryMagicCOMSBuff", bp => {
                bp.SetName("Mystery Magic - Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g} along with " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. At 7th  and 9th level, the armor " +
                    "bonus increases by +2. You can use this coat for 1 hour number of times per day equal to stargazer level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 2;
                    c.m_Class = new BlueprintCharacterClassReference[] { StargazerClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 4,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Heal,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Armor;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.UsePool = false;
                    c.Pool = new ContextValue();
                    c.Or = false;
                    c.BypassedByMaterial = false;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.BypassedByForm = true;
                    c.Form = PhysicalDamageForm.Slashing;
                    c.BypassedByMagic = false;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = false;
                    c.Alignment = DamageAlignment.Good;
                    c.BypassedByReality = false;
                    c.Reality = DamageRealityType.Ghost;
                    c.BypassedByWeaponType = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.BypassedByEpic = false;
                    c.m_CheckedFactMythic = new BlueprintUnitFactReference();
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
            });
            var StargazerMysteryMagicCOMSAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("StargazerMysteryMagicCOMSAbility", bp => {
                bp.SetName("Mystery Magic - Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g} along with " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. At 7th  and 9th level, the armor " +
                    "bonus increases by +2. You can use this coat for 1 hour number of times per day equal to stargazer level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = StargazerMysteryMagicCOMSResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = StargazerMysteryMagicCOMSBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var StargazerMysteryMagicCOMSFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerMysteryMagicCOMSFeature", bp => {
                bp.SetName("Mystery Magic - Coat of Many Stars");
                bp.SetDescription("You conjure a coat of starry radiance that grants you a +4 armor {g|Encyclopedia:Bonus}bonus{/g} along with " +
                    "{g|Encyclopedia:Damage_Reduction}DR{/g} 5/{g|Encyclopedia:Damage_Type}slashing{/g}. At 7th  and 9th level, the armor " +
                    "bonus increases by +2. You can use this coat for 1 hour number of times per day equal to stargazer level.");
                bp.m_Icon = EdictOfImpenetrableFortress.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerMysteryMagicCOMSAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = StargazerMysteryMagicCOMSResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #region Sidereal Arcana
            //Resoruces
            var SubtypeDemon = Resources.GetBlueprint<BlueprintFeature>("dc960a234d365cb4f905bdc5937e623a");
            var WitchHexHealingAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("ed4fbfcdb0f5dcb41b76d27ed00701af");
            var WitchHexMajorHealingAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("3408c351753aa9049af25af31ebef624");
            var ShamanHexHealingAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("3d4c6361e60fa664db01d5709baaa812");
            var MountedBuff = Resources.GetBlueprint<BlueprintBuff>("b2d13e8f3bb0f1d4c891d71b4d983cf7");
            var VanishBuff = Resources.GetBlueprint<BlueprintBuff>("e5b7ef8d49215314daaf0404349d42a6");
            var FreedomofMovementBuff = Resources.GetBlueprint<BlueprintBuff>("1533e782fca42b84ea370fc1dcbf4fc1");
            //Stuff for Abilities
            var StargazerSiderealTheDaughterAuraEffect = Helpers.CreateBuff("StargazerSiderealTheDaughterAuraEffect", bp => {
                bp.SetName("The Daughter Aura Effect");
                bp.SetDescription("The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Fear;
                    c.ModifierDescriptor = ModifierDescriptor.Morale;
                    c.Value = 4;
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });
            var StargazerSiderealTheDaughterAuraArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("StargazerSiderealTheDaughterAuraArea", bp => {
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.Condition = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionIsAlly() {
                                Not = false
                            }
                        }
                    };
                    c.m_Buff = StargazerSiderealTheDaughterAuraEffect.ToReference<BlueprintBuffReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Ally;
                bp.m_Tags = AreaEffectTags.None;
                bp.SpellResistance = false;
                bp.AffectEnemies = false;
                bp.AggroEnemies = false;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 13.Feet();
                bp.Fx = new PrefabLink();
            });
            var StargazerSiderealTheDaughterAura = Helpers.CreateBuff("StargazerSiderealTheDaughterAura", bp => {
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = StargazerSiderealTheDaughterAuraArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });
            var StargazerSiderealTheRiderPetBuff = Helpers.CreateBuff("StargazerSiderealTheRiderPetBuff", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.AddComponent<BuffAllSavesBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 1;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
                });
                bp.IsClassFeature = false;
            });
            var StargazerSiderealTheRiderPetFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheRiderPetFeature", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheRiderPetBuff.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
            });
            var StargazerSiderealTheRiderMountedEffect = Helpers.CreateBuff("StargazerSiderealTheRiderMountedEffect", bp => {
                bp.AddComponent<AddFeatureToPet>(c => {
                    c.m_PetType = PetType.AnimalCompanion;
                    c.m_Feature = StargazerSiderealTheRiderPetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });
            var StargazerSiderealTheStrangerAbilityResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("StargazerSiderealTheStrangerAbilityResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {                        
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0],
                    StartingLevel = 0,
                    LevelStep = 2,
                    StartingIncrease = 0,
                    PerStepIncrease = 1,
                };
            });
            var StargazerSiderealTheStrangerAbility = Helpers.CreateBlueprint<BlueprintAbility>("StargazerSiderealTheStrangerAbility", bp => {
                bp.SetName("Stargazer - Vanish");
                bp.SetDescription("The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day. \nIf a {g|Encyclopedia:Check}check{/g} is required, an invisible creature has a " +
                    "+20 {g|Encyclopedia:Bonus}bonus{/g} on its {g|Encyclopedia:Stealth}Stealth checks{/g}. The {g|Encyclopedia:Spell}spell{/g} ends if the subject " +
                    "{g|Encyclopedia:Attack}attacks{/g} any creature.[LONGSTART] For purposes of this spell, an attack includes any {g|Encyclopedia:Spell_Target}spell targeting{/g} " +
                    "a foe or whose area or effect includes a foe. Exactly who is a foe depends on the invisible character's {g|Encyclopedia:Perception}perceptions{/g}. " +
                    "{g|Encyclopedia:CA_Types}Actions{/g} directed at unattended objects do not break the spell. Causing harm indirectly is not an attack. Thus, an invisible " +
                    "being can open doors, talk, eat, climb stairs, summon monsters and have them attack, cut the ropes holding a rope bridge while enemies are on the bridge, " +
                    "remotely trigger traps, open a portcullis to release attack dogs, and so forth. If the subject attacks directly, however, it immediately becomes visible " +
                    "along with all its gear. Spells such as bless that specifically affect allies but not foes are not attacks for this purpose, even when they include foes " +
                    "in their area.[LONGEND]");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = VanishBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
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
                    c.m_UseMax = true;
                    c.m_Max = 5;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Illusion;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerSiderealTheStrangerAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.m_Icon = VanishBuff.Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("StargazerSiderealTheStrangerAbility.Duration", "1 round/level (up to 5 rounds)");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerSiderealTheWagonAbilityResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("StargazerSiderealTheWagonAbilityResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = false,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0],
                    StartingLevel = 0,
                    LevelStep = 2,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });
            var StargazerSiderealTheWagonAbility = Helpers.CreateBlueprint<BlueprintAbility>("StargazerSiderealTheWagonAbility", bp => {
                bp.SetName("Stargazer - Freedom of Movement");
                bp.SetDescription("The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round. \nThis {g|Encyclopedia:Spell}spell{/g} enables " +
                    "you to move and {g|Encyclopedia:Attack}attack{/g} normally for the duration of the spell, even under the influence of magic that usually impedes " +
                    "movement, such as paralysis, solid fog, slow, and web. All {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}checks{/g} " +
                    "made to grapple the target automatically fail.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FreedomofMovementBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = false
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Abjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerSiderealTheWagonAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.m_Icon = FreedomofMovementBuff.Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("StargazerSiderealTheWagonAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            #region Stars Dance
            var StarsDanceBridgeIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceBridge.jpg");
            var StarsDanceDaughterIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceDaughter.jpg");
            var StarsDanceFollowerIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceFollower.jpg");
            var StarsDanceLanternBearerIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceLanternBearer.jpg");
            var StarsDanceMotherIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceMother.jpg");
            var StarsDanceNewlywedsIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceNewlyweds.jpg");
            var StarsDancePackIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDancePack.jpg");
            var StarsDancePatriarchIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDancePatriarch.jpg");
            var StarsDanceRiderIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceRider.jpg");
            var StarsDanceStargazerIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceStargazer.jpg");
            var StarsDanceStrangerIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceStranger.jpg");
            var StarsDanceThrushIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceThrush.jpg");
            var StarsDanceWagonIcon = AssetLoader.LoadInternal("Skills", "Icon_StarsDanceWagon.jpg");
            var StargazerStarsDanceResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("StargazerStarsDanceResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            //Stars Dance Features
            var StargazerStarsDanceBridgeFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceBridgeFeature", bp => {
                bp.SetName("Sidereal Arcana - The Bridge");
                bp.SetDescription("The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Blindness;
                    c.CheckFact = true;
                    c.m_FactToCheck = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Cold;
                    c.Value = 5;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceDaughterFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceDaughterFeature", bp => {
                bp.SetName("Sidereal Arcana - The Daughter");
                bp.SetDescription("The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Fear;
                    c.CheckFact = true;
                    c.m_FactToCheck = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = StargazerSiderealTheDaughterAura.ToReference<BlueprintBuffReference>();
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceFollowerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceFollowerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Follower");
                bp.SetDescription("The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Death | SpellDescriptor.NegativeLevel | SpellDescriptor.ChannelNegativeHarm;
                    c.CheckFact = true;
                    c.m_FactToCheck = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Death;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceLanternBearerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceLanternBearerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Lantern Bearer");
                bp.SetDescription("The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.AddComponent<IncreaseSpellDescriptorCasterLevel>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.BonusCasterLevel = 2;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceMotherFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceMotherFeature", bp => {
                bp.SetName("Sidereal Arcana - The Mother");
                bp.SetDescription("The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = true;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        WitchHexHealingAbility,
                        WitchHexMajorHealingAbility,
                        ShamanHexHealingAbility
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = SpellDescriptor.Cure;
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionHealTarget() {
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Shared,
                                    ValueShared = AbilitySharedValue.Heal
                                }
                            }
                        }
                        );
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = true;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>() { };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = true;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = true;
                    c.SpellDescriptor = SpellDescriptor.Cure;
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionHealTarget() {
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Shared,
                                    ValueShared = AbilitySharedValue.Heal
                                }
                            }
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { StargazerClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceNewlywedsFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceNewlywedsFeature", bp => {
                bp.SetName("Sidereal Arcana - The Newlyweds");
                bp.SetDescription("The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.AddComponent<IncreaseSpellDescriptorCasterLevel>(c => {
                    c.Descriptor = SpellDescriptor.Compulsion;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.BonusCasterLevel = 1;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Compulsion;
                    c.ModifierDescriptor = ModifierDescriptor.Morale;
                    c.Value = 2;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDancePackFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDancePackFeature", bp => {
                bp.SetName("Sidereal Arcana - The Pack");
                bp.SetDescription("The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillLoreNature;
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDancePatriarchFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDancePatriarchFeature", bp => {
                bp.SetName("Sidereal Arcana - The Patriarch");
                bp.SetDescription("The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Value = 4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceRiderFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceRiderFeature", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.AddComponent<BuffExtraEffects>(c => {
                    c.m_CheckedBuff = MountedBuff.ToReference<BlueprintBuffReference>();
                    c.m_ExtraEffectBuff = StargazerSiderealTheRiderMountedEffect.ToReference<BlueprintBuffReference>();
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceStargazerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceStargazerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Stargazer");
                bp.SetDescription("The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.AddComponent<FlatFootedIgnore>(c => {
                    c.Type = FlatFootedIgnoreType.UncannyDodge;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.Initiative;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Value = 2;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceStrangerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceStrangerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Stranger");
                bp.SetDescription("The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = StargazerSiderealTheStrangerAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheStrangerAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });                
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceThrushFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceThrushFeature", bp => {
                bp.SetName("Sidereal Arcana - The Thrush");
                bp.SetDescription("The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        StargazerClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillPersuasion;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillUseMagicDevice;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            var StargazerStarsDanceWagonFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceWagonFeature", bp => {
                bp.SetName("Sidereal Arcana - The Wagon");
                bp.SetDescription("The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = StargazerSiderealTheWagonAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheWagonAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Value = 10;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.HideInUI = false;
                bp.IsClassFeature = false;
            });
            //Stars Dance Ability Parents - Variants added after buffs
            var StargazerStarsDanceBridgeSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Bridge. " +
                    "\nThe stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Daughter. " +
                    "\nThe Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale " +
                    "bonus on saving throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Follower. " +
                    "\nThe specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he " +
                    "is immune to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Lantern Bearer. " +
                    "\nThe stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Mother. " +
                    "\nThe stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Newlyweds. " +
                    "\nThe sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Pack. " +
                    "\nThe stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Patriarch. " +
                    "\nThe stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Rider. " +
                    "\nThe stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Stargazer. " +
                    "\nThe constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Stranger. " +
                    "\nThe stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Thrush. " +
                    "\nThe stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapParent = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapParent", bp => {
                bp.SetName("Stars’ Dance - The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. " +
                    "\nAll variants of this ability give up The Wagon. " +
                    "\nThe stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
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
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //Stars Dance Buffs
            var StargazerStarsDanceBridgeBuff = Helpers.CreateBuff("StargazerStarsDanceBridgeBuff", bp => {
                bp.SetName("Sidereal Arcana - The Bridge");
                bp.SetDescription("The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceDaughterBuff = Helpers.CreateBuff("StargazerStarsDanceDaughterBuff", bp => {
                bp.SetName("Sidereal Arcana - The Daughter");
                bp.SetDescription("The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceFollowerBuff = Helpers.CreateBuff("StargazerStarsDanceFollowerBuff", bp => {
                bp.SetName("Sidereal Arcana - The Follower");
                bp.SetDescription("The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceLanternBearerBuff = Helpers.CreateBuff("StargazerStarsDanceLanternBearerBuff", bp => {
                bp.SetName("Sidereal Arcana - The Lantern Bearer");
                bp.SetDescription("The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceMotherBuff = Helpers.CreateBuff("StargazerStarsDanceMotherBuff", bp => {
                bp.SetName("Sidereal Arcana - The Mother");
                bp.SetDescription("The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceNewlywedsBuff = Helpers.CreateBuff("StargazerStarsDanceNewlywedsBuff", bp => {
                bp.SetName("Sidereal Arcana - The Newlyweds");
                bp.SetDescription("The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDancePackBuff = Helpers.CreateBuff("StargazerStarsDancePackBuff", bp => {
                bp.SetName("Sidereal Arcana - The Pack");
                bp.SetDescription("The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDancePatriarchBuff = Helpers.CreateBuff("StargazerStarsDancePatriarchBuff", bp => {
                bp.SetName("Sidereal Arcana - The Patriarch");
                bp.SetDescription("The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceRiderBuff = Helpers.CreateBuff("StargazerStarsDanceRiderBuff", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceStargazerBuff = Helpers.CreateBuff("StargazerStarsDanceStargazerBuff", bp => {
                bp.SetName("Sidereal Arcana - The Stargazer");
                bp.SetDescription("The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceStrangerBuff = Helpers.CreateBuff("StargazerStarsDanceStrangerBuff", bp => {
                bp.SetName("Sidereal Arcana - The Stranger");
                bp.SetDescription("The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceThrushBuff = Helpers.CreateBuff("StargazerStarsDanceThrushBuff", bp => {
                bp.SetName("Sidereal Arcana - The Thrush");
                bp.SetDescription("The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var StargazerStarsDanceWagonBuff = Helpers.CreateBuff("StargazerStarsDanceWagonBuff", bp => {
                bp.SetName("Sidereal Arcana - The Wagon");
                bp.SetDescription("The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            #region Stars Dance Variants
            //Bridge
            var StargazerStarsDanceBridgeSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapDaughter", bp => {
                bp.SetName("The Bridge to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapFollower", bp => {
                bp.SetName("The Bridge to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapLanternBearer", bp => {
                bp.SetName("The Bridge to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapMother", bp => {
                bp.SetName("The Bridge to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapNewlyweds", bp => {
                bp.SetName("The Bridge to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapPack", bp => {
                bp.SetName("The Bridge to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapPatriarch", bp => {
                bp.SetName("The Bridge to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapRider", bp => {
                bp.SetName("The Bridge to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapStargazer", bp => {
                bp.SetName("The Bridge to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapStranger", bp => {
                bp.SetName("The Bridge to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapThrush", bp => {
                bp.SetName("The Bridge to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceBridgeSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceBridgeSwapWagon", bp => {
                bp.SetName("The Bridge to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Bridge and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceBridgeSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceBridgeSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceBridgeSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Daughter
            var StargazerStarsDanceDaughterSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapBridge", bp => {
                bp.SetName("The Daughter to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapFollower", bp => {
                bp.SetName("The Daughter to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapLanternBearer", bp => {
                bp.SetName("The Daughter to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapMother", bp => {
                bp.SetName("The Daughter to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapNewlyweds", bp => {
                bp.SetName("The Daughter to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapPack", bp => {
                bp.SetName("The Daughter to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapPatriarch", bp => {
                bp.SetName("The Daughter to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapRider", bp => {
                bp.SetName("The Daughter to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapStargazer", bp => {
                bp.SetName("The Daughter to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapStranger", bp => {
                bp.SetName("The Daughter to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapThrush", bp => {
                bp.SetName("The Daughter to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceDaughterSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceDaughterSwapWagon", bp => {
                bp.SetName("The Daughter to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Daughter and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceDaughterSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceDaughterSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceDaughterSwapWagon.ToReference<BlueprintAbilityReference>()

                };
            });
            //Follower
            var StargazerStarsDanceFollowerSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapBridge", bp => {
                bp.SetName("The Follower to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapDaughter", bp => {
                bp.SetName("The Follower to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapLanternBearer", bp => {
                bp.SetName("The Follower to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapMother", bp => {
                bp.SetName("The Follower to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapNewlyweds", bp => {
                bp.SetName("The Follower to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapPack", bp => {
                bp.SetName("The Follower to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapPatriarch", bp => {
                bp.SetName("The Follower to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapRider", bp => {
                bp.SetName("The Follower to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapStargazer", bp => {
                bp.SetName("The Follower to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapStranger", bp => {
                bp.SetName("The Follower to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapThrush", bp => {
                bp.SetName("The Follower to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceFollowerSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceFollowerSwapWagon", bp => {
                bp.SetName("The Follower to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Follower and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceFollowerSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceFollowerSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceFollowerSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Lantern Bearer
            var StargazerStarsDanceLanternBearerSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapBridge", bp => {
                bp.SetName("The Lantern Bearer to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapDaughter", bp => {
                bp.SetName("The Lantern Bearer to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapFollower", bp => {
                bp.SetName("The Lantern Bearer to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapMother", bp => {
                bp.SetName("The Lantern Bearer to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapNewlyweds", bp => {
                bp.SetName("The Lantern Bearer to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapPack", bp => {
                bp.SetName("The Lantern Bearer to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapPatriarch", bp => {
                bp.SetName("The Lantern Bearer to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapRider", bp => {
                bp.SetName("The Lantern Bearer to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapStargazer", bp => {
                bp.SetName("The Lantern Bearer to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapStranger", bp => {
                bp.SetName("The Lantern Bearer to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapThrush", bp => {
                bp.SetName("The Lantern Bearer to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceLanternBearerSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceLanternBearerSwapWagon", bp => {
                bp.SetName("The Lantern Bearer to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Lantern Bearer and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceLanternBearerSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceLanternBearerSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceLanternBearerSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Mother
            var StargazerStarsDanceMotherSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapBridge", bp => {
                bp.SetName("The Mother to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapDaughter", bp => {
                bp.SetName("The Mother to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapFollower", bp => {
                bp.SetName("The Mother to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapLanternBearer", bp => {
                bp.SetName("The Mother to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapNewlyweds", bp => {
                bp.SetName("The Mother to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapPack", bp => {
                bp.SetName("The Mother to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapPatriarch", bp => {
                bp.SetName("The Mother to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapRider", bp => {
                bp.SetName("The Mother to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapStargazer", bp => {
                bp.SetName("The Mother to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapStranger", bp => {
                bp.SetName("The Mother to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapThrush", bp => {
                bp.SetName("The Mother to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceMotherSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceMotherSwapWagon", bp => {
                bp.SetName("The Mother to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Mother and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceMotherSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceMotherSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceMotherSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceMotherSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Newlyweds
            var StargazerStarsDanceNewlywedsSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapBridge", bp => {
                bp.SetName("The Newlyweds to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapDaughter", bp => {
                bp.SetName("The Newlyweds to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapFollower", bp => {
                bp.SetName("The Newlyweds to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapLanternBearer", bp => {
                bp.SetName("The Newlyweds to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapMother", bp => {
                bp.SetName("The Newlyweds to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapPack", bp => {
                bp.SetName("The Newlyweds to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapPatriarch", bp => {
                bp.SetName("The Newlyweds to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapRider", bp => {
                bp.SetName("The Newlyweds to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapStargazer", bp => {
                bp.SetName("The Newlyweds to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapStranger", bp => {
                bp.SetName("The Newlyweds to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapThrush", bp => {
                bp.SetName("The Newlyweds to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceNewlywedsSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceNewlywedsSwapWagon", bp => {
                bp.SetName("The Newlyweds to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Newlyweds and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceNewlywedsSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceNewlywedsSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceNewlywedsSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Pack
            var StargazerStarsDancePackSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapBridge", bp => {
                bp.SetName("The Pack to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapDaughter", bp => {
                bp.SetName("The Pack to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapFollower", bp => {
                bp.SetName("The Pack to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapLanternBearer", bp => {
                bp.SetName("The Pack to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapMother", bp => {
                bp.SetName("The Pack to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapNewlyweds", bp => {
                bp.SetName("The Pack to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapPatriarch", bp => {
                bp.SetName("The Pack to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapRider", bp => {
                bp.SetName("The Pack to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapStargazer", bp => {
                bp.SetName("The Pack to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapStranger", bp => {
                bp.SetName("The Pack to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapThrush", bp => {
                bp.SetName("The Pack to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePackSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePackSwapWagon", bp => {
                bp.SetName("The Pack to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Pack and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePackSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDancePackSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDancePackSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePackSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Patriarch
            var StargazerStarsDancePatriarchSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapBridge", bp => {
                bp.SetName("The Patriarch to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapDaughter", bp => {
                bp.SetName("The Patriarch to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapFollower", bp => {
                bp.SetName("The Patriarch to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapLanternBearer", bp => {
                bp.SetName("The Patriarch to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapMother", bp => {
                bp.SetName("The Patriarch to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapNewlyweds", bp => {
                bp.SetName("The Patriarch to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapPack", bp => {
                bp.SetName("The Patriarch to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapRider", bp => {
                bp.SetName("The Patriarch to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapStargazer", bp => {
                bp.SetName("The Patriarch to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapStranger", bp => {
                bp.SetName("The Patriarch to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapThrush", bp => {
                bp.SetName("The Patriarch to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDancePatriarchSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDancePatriarchSwapWagon", bp => {
                bp.SetName("The Patriarch to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Patriarch and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDancePatriarchSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDancePatriarchSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDancePatriarchSwapWagon.ToReference<BlueprintAbilityReference>()

                };
            });
            //Rider
            var StargazerStarsDanceRiderSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapBridge", bp => {
                bp.SetName("The Rider to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapDaughter", bp => {
                bp.SetName("The Rider to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapFollower", bp => {
                bp.SetName("The Rider to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapLanternBearer", bp => {
                bp.SetName("The Rider to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapMother", bp => {
                bp.SetName("The Rider to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapNewlyweds", bp => {
                bp.SetName("The Rider to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapPack", bp => {
                bp.SetName("The Rider to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapPatriarch", bp => {
                bp.SetName("The Rider to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapStargazer", bp => {
                bp.SetName("The Rider to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapStranger", bp => {
                bp.SetName("The Rider to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapThrush", bp => {
                bp.SetName("The Rider to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceRiderSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceRiderSwapWagon", bp => {
                bp.SetName("The Rider to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Rider and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceRiderSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceRiderSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceRiderSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceRiderSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Stargazer
            var StargazerStarsDanceStargazerSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapBridge", bp => {
                bp.SetName("The Stargazer to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapDaughter", bp => {
                bp.SetName("The Stargazer to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapFollower", bp => {
                bp.SetName("The Stargazer to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapLanternBearer", bp => {
                bp.SetName("The Stargazer to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapMother", bp => {
                bp.SetName("The Stargazer to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapNewlyweds", bp => {
                bp.SetName("The Stargazer to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapPack", bp => {
                bp.SetName("The Stargazer to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapPatriarch", bp => {
                bp.SetName("The Stargazer to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapRider", bp => {
                bp.SetName("The Stargazer to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapStranger", bp => {
                bp.SetName("The Stargazer to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapThrush", bp => {
                bp.SetName("The Stargazer to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStargazerSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStargazerSwapWagon", bp => {
                bp.SetName("The Stargazer to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stargazer and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceStargazerSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceStargazerSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStargazerSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Stranger
            var StargazerStarsDanceStrangerSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapBridge", bp => {
                bp.SetName("The Stranger to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapDaughter", bp => {
                bp.SetName("The Stranger to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapFollower", bp => {
                bp.SetName("The Stranger to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapLanternBearer", bp => {
                bp.SetName("The Stranger to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapMother", bp => {
                bp.SetName("The Stranger to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapNewlyweds", bp => {
                bp.SetName("The Stranger to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapPack", bp => {
                bp.SetName("The Stranger to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapPatriarch", bp => {
                bp.SetName("The Stranger to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapRider", bp => {
                bp.SetName("The Stranger to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapStargazer", bp => {
                bp.SetName("The Stranger to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapThrush", bp => {
                bp.SetName("The Stranger to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceStrangerSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceStrangerSwapWagon", bp => {
                bp.SetName("The Stranger to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Stranger and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceStrangerSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceStrangerSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapThrush.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceStrangerSwapWagon.ToReference<BlueprintAbilityReference>()

                };
            });
            //Thrush
            var StargazerStarsDanceThrushSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapBridge", bp => {
                bp.SetName("The Thrush to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapDaughter", bp => {
                bp.SetName("The Thrush to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapFollower", bp => {
                bp.SetName("The Thrush to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapLanternBearer", bp => {
                bp.SetName("The Thrush to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapMother", bp => {
                bp.SetName("The Thrush to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapNewlyweds", bp => {
                bp.SetName("The Thrush to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapPack", bp => {
                bp.SetName("The Thrush to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapPatriarch", bp => {
                bp.SetName("The Thrush to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapRider", bp => {
                bp.SetName("The Thrush to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapStargazer", bp => {
                bp.SetName("The Thrush to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapStranger", bp => {
                bp.SetName("The Thrush to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceThrushSwapWagon = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceThrushSwapWagon", bp => {
                bp.SetName("The Thrush to The Wagon");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Thrush and gains The Wagon." +
                    "\nThe Wagon: The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.m_Icon = StarsDanceWagonIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceThrushSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceThrushSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceThrushSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceThrushSwapWagon.ToReference<BlueprintAbilityReference>()
                };
            });
            //Wagon
            var StargazerStarsDanceWagonSwapBridge = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapBridge", bp => {
                bp.SetName("The Wagon to The Bridge");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Bridge." +
                    "\nThe Bridge: The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.m_Icon = StarsDanceBridgeIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceBridgeBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapDaughter = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapDaughter", bp => {
                bp.SetName("The Wagon to The Daughter");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Daughter." +
                    "\nThe Daughter: The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.m_Icon = StarsDanceDaughterIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceDaughterBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapFollower = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapFollower", bp => {
                bp.SetName("The Wagon to The Follower");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Follower." +
                    "\nThe Follower: The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.m_Icon = StarsDanceFollowerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceFollowerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapLanternBearer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapLanternBearer", bp => {
                bp.SetName("The Wagon to The Lantern Bearer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Lantern Bearer." +
                    "\nThe LanternBearer: The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.m_Icon = StarsDanceLanternBearerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapMother = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapMother", bp => {
                bp.SetName("The Wagon to The Mother");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Mother." +
                    "\nThe Mother: The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.m_Icon = StarsDanceMotherIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceMotherBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapNewlyweds = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapNewlyweds", bp => {
                bp.SetName("The Wagon to The Newlyweds");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Newlyweds." +
                    "\nThe Newlyweds: The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.m_Icon = StarsDanceNewlywedsIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapPack = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapPack", bp => {
                bp.SetName("The Wagon to The Pack");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Pack." +
                    "\nThe Pack: The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.m_Icon = StarsDancePackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePackBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapPatriarch = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapPatriarch", bp => {
                bp.SetName("The Wagon to The Patriarch");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Patriarch." +
                    "\nThe Patriarch: The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.m_Icon = StarsDancePatriarchIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDancePatriarchBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapRider = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapRider", bp => {
                bp.SetName("The Wagon to The Rider");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Rider." +
                    "\nThe Rider: The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.m_Icon = StarsDanceRiderIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceRiderBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapStargazer = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapStargazer", bp => {
                bp.SetName("The Wagon to The Stargazer");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Stargazer." +
                    "\nThe Stargazer: The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.m_Icon = StarsDanceStargazerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStargazerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapStranger = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapStranger", bp => {
                bp.SetName("The Wagon to The Stranger");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Stranger." +
                    "\nThe Stranger: The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.m_Icon = StarsDanceStrangerIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceStrangerBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var StargazerStarsDanceWagonSwapThrush = Helpers.CreateBlueprint<BlueprintAbility>("StargazerStarsDanceWagonSwapThrush", bp => {
                bp.SetName("The Wagon to The Thrush");
                bp.SetDescription("Once per day, the stargazer can replace one of his sidereal arcana with any other one. This ability gives up The Wagon and gains The Thrush." +
                    "\nThe Thrush: The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.m_Icon = StarsDanceThrushIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceThrushBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StargazerStarsDanceWagonFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = StargazerStarsDanceWagonSwapParent.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            StargazerStarsDanceWagonSwapParent.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    StargazerStarsDanceWagonSwapBridge.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapDaughter.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapFollower.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapLanternBearer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapMother.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapNewlyweds.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapPack.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapPatriarch.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapRider.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapStargazer.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapStranger.ToReference<BlueprintAbilityReference>(),
                    StargazerStarsDanceWagonSwapThrush.ToReference<BlueprintAbilityReference>()
                };
            });
            #endregion
            #endregion
            //Pre level 10 features
            var StargazerSiderealTheBridgeEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheBridgeEffect", bp => {
                bp.SetName("Sidereal Arcana - The Bridge");
                bp.SetDescription("The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Blindness;
                    c.CheckFact = true;
                    c.m_FactToCheck = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Cold;
                    c.Value = 5;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheDaughterEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheDaughterEffect", bp => {
                bp.SetName("Sidereal Arcana - The Daughter");
                bp.SetDescription("The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Fear;
                    c.CheckFact = true;
                    c.m_FactToCheck = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = StargazerSiderealTheDaughterAura.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheFollowerEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheFollowerEffect", bp => {
                bp.SetName("Sidereal Arcana - The Follower");
                bp.SetDescription("The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Death | SpellDescriptor.NegativeLevel | SpellDescriptor.ChannelNegativeHarm;
                    c.CheckFact = true;
                    c.m_FactToCheck = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Death;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheLanternBearerEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheLanternBearerEffect", bp => {
                bp.SetName("Sidereal Arcana - The Lantern Bearer");
                bp.SetDescription("The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");                
                bp.AddComponent<IncreaseSpellDescriptorCasterLevel>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.BonusCasterLevel = 2;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheMotherEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheMotherEffect", bp => {
                bp.SetName("Sidereal Arcana - The Mother");
                bp.SetDescription("The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        WitchHexHealingAbility,
                        WitchHexMajorHealingAbility,
                        ShamanHexHealingAbility
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = true;
                    c.SpellDescriptor = SpellDescriptor.Cure;
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionHealTarget() {
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Shared,
                                    ValueShared = AbilitySharedValue.Heal
                                }
                            }
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { StargazerClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheNewlywedsEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheNewlywedsEffect", bp => {
                bp.SetName("Sidereal Arcana - The Newlyweds");
                bp.SetDescription("The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.AddComponent<IncreaseSpellDescriptorCasterLevel>(c => {
                    c.Descriptor = SpellDescriptor.Compulsion;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.BonusCasterLevel = 1;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Compulsion;
                    c.ModifierDescriptor = ModifierDescriptor.Morale;
                    c.Value = 2;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealThePackEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealThePackEffect", bp => {
                bp.SetName("Sidereal Arcana - The Pack");
                bp.SetDescription("The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillLoreNature;
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealThePatriarchEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealThePatriarchEffect", bp => {
                bp.SetName("Sidereal Arcana - The Patriarch");
                bp.SetDescription("The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Value = 4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheRiderEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheRiderEffect", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.AddComponent<BuffExtraEffects>(c => {
                    c.m_CheckedBuff = MountedBuff.ToReference<BlueprintBuffReference>();
                    c.m_ExtraEffectBuff = StargazerSiderealTheRiderMountedEffect.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheStargazerEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheStargazerEffect", bp => {
                bp.SetName("Sidereal Arcana - The Stargazer");
                bp.SetDescription("The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.AddComponent<FlatFootedIgnore>(c => {
                    c.Type = FlatFootedIgnoreType.UncannyDodge;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.Initiative;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Value = 2;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheStrangerEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheStrangerEffect", bp => {
                bp.SetName("Sidereal Arcana - The Stranger");
                bp.SetDescription("The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = StargazerSiderealTheStrangerAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheStrangerAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheThrushEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheThrushEffect", bp => {
                bp.SetName("Sidereal Arcana - The Thrush");
                bp.SetDescription("The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        StargazerClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillPersuasion;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillUseMagicDevice;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });
            var StargazerSiderealTheWagonEffect = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheWagonEffect", bp => {
                bp.SetName("Sidereal Arcana - The Wagon");
                bp.SetDescription("The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = StargazerSiderealTheWagonAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheWagonAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Value = 10;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = false;
            });

            //Holder to make UI look nice
            var StargazerSiderealTheBridgeHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheBridgeHolder", bp => {
                bp.SetName("Sidereal Arcana - The Bridge");
                bp.SetDescription("The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheBridgeEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheDaughterHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheDaughterHolder", bp => {
                bp.SetName("Sidereal Arcana - The Daughter");
                bp.SetDescription("The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheDaughterEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheFollowerHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheFollowerHolder", bp => {
                bp.SetName("Sidereal Arcana - The Follower");
                bp.SetDescription("The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheFollowerEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheLanternBearerHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheLanternBearerHolder", bp => {
                bp.SetName("Sidereal Arcana - The Lantern Bearer");
                bp.SetDescription("The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheLanternBearerEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheMotherHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheMotherHolder", bp => {
                bp.SetName("Sidereal Arcana - The Mother");
                bp.SetDescription("The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheMotherEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheNewlywedsHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheNewlywedsHolder", bp => {
                bp.SetName("Sidereal Arcana - The Newlyweds");
                bp.SetDescription("The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheNewlywedsEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealThePackHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealThePackHolder", bp => {
                bp.SetName("Sidereal Arcana - The Pack");
                bp.SetDescription("The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealThePackEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealThePatriarchHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealThePatriarchHolder", bp => {
                bp.SetName("Sidereal Arcana - The Patriarch");
                bp.SetDescription("The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealThePatriarchEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheRiderHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheRiderHolder", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheRiderEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheStargazerHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheStargazerHolder", bp => {
                bp.SetName("Sidereal Arcana - The Stargazer");
                bp.SetDescription("The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheStargazerEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheStrangerHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheStrangerHolder", bp => {
                bp.SetName("Sidereal Arcana - The Stranger");
                bp.SetDescription("The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheStrangerEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheThrushHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheThrushHolder", bp => {
                bp.SetName("Sidereal Arcana - The Thrush");
                bp.SetDescription("The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheThrushEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheWagonHolder = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheWagonHolder", bp => {
                bp.SetName("Sidereal Arcana - The Wagon");
                bp.SetDescription("The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 10;
                    c.m_Feature = StargazerSiderealTheWagonEffect.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            //Sidereal Arcana Options
            var StargazerSiderealTheBridgeFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheBridgeFeature", bp => {
                bp.SetName("Sidereal Arcana - The Bridge");
                bp.SetDescription("The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheBridgeHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheDaughterFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheDaughterFeature", bp => {
                bp.SetName("Sidereal Arcana - The Daughter");
                bp.SetDescription("The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheDaughterHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheFollowerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheFollowerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Follower");
                bp.SetDescription("The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheFollowerHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheLanternBearerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheLanternBearerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Lantern Bearer");
                bp.SetDescription("The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheLanternBearerHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheMotherFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheMotherFeature", bp => {
                bp.SetName("Sidereal Arcana - The Mother");
                bp.SetDescription("The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheMotherHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheNewlywedsFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheNewlywedsFeature", bp => {
                bp.SetName("Sidereal Arcana - The Newlyweds");
                bp.SetDescription("The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheNewlywedsHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealThePackFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealThePackFeature", bp => {
                bp.SetName("Sidereal Arcana - The Pack");
                bp.SetDescription("The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealThePackHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealThePatriarchFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealThePatriarchFeature", bp => {
                bp.SetName("Sidereal Arcana - The Patriarch");
                bp.SetDescription("The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealThePatriarchHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheRiderFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheRiderFeature", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheRiderHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheStargazerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheStargazerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Stargazer");
                bp.SetDescription("The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheStargazerHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheStrangerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheStrangerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Stranger");
                bp.SetDescription("The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheStrangerHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheThrushFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheThrushFeature", bp => {
                bp.SetName("Sidereal Arcana - The Thrush");
                bp.SetDescription("The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheThrushHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealTheWagonFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealTheWagonFeature", bp => {
                bp.SetName("Sidereal Arcana - The Wagon");
                bp.SetDescription("The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerSiderealTheWagonHolder.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            //Sidereal Arcana Options - Level 10
            var StargazerSiderealFinalTheBridgeFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheBridgeFeature", bp => {
                bp.SetName("Sidereal Arcana - The Bridge");
                bp.SetDescription("The stargazer is warded against winter’s darkness. He gains cold resistance 5 and an immunity to blindness effects caused by demons.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheBridgeFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceBridgeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheDaughterFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheDaughterFeature", bp => {
                bp.SetName("Sidereal Arcana - The Daughter");
                bp.SetDescription("The Daughter emboldens hearts with the promise of springtime and new life. The stargazer and allies within 10 feet gain a +4 morale bonus on saving " +
                    "throws against fear. The stargazer is immune to fear effects created by demons.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheDaughterFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceDaughterBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheFollowerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheFollowerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Follower");
                bp.SetDescription("The specter of death follows the stargazer, shielding him from doom. The stargazer gains a +4 bonus on saves against death effects. In addition, he is immune " +
                    "to all death effects, negative energy effects, and negative levels created by demons.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheFollowerFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceFollowerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheLanternBearerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheLanternBearerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Lantern Bearer");
                bp.SetDescription("The stargazer’s ability to conjure light increases. Any spell cast by the stargazer with the fire descriptor has its spell level increased by 2.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheLanternBearerFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceLanternBearerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheMotherFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheMotherFeature", bp => {
                bp.SetName("Sidereal Arcana - The Mother");
                bp.SetDescription("The stargazer channels the nurturing heart of the Caravan. Whenever the stargazer casts a cure spell, casts breath of life, or uses the healing hex, " +
                    "he adds twice his class level to the hit points restored.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheMotherFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceMotherBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheNewlywedsFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheNewlywedsFeature", bp => {
                bp.SetName("Sidereal Arcana - The Newlyweds");
                bp.SetDescription("The sign of intertwined lovers grants the stargazer a romantic mystique. Whenever he uses a spell with the compulsion descriptor the save DC increases by 1. " +
                    "In addition, stargazer gains a +2 morale bonus on saving throws against compulsion effects.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheNewlywedsFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceNewlywedsBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalThePackFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalThePackFeature", bp => {
                bp.SetName("Sidereal Arcana - The Pack");
                bp.SetDescription("The stargazer becomes attuned to the beasts that follow the Caravan. He gains a +2 bonus on Nature checks. In addition, whenever he casts a summoning " +
                    "spell that conjures multiple creatures of the animal type, he summons an additional animal of that type.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealThePackFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePackBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalThePatriarchFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalThePatriarchFeature", bp => {
                bp.SetName("Sidereal Arcana - The Patriarch");
                bp.SetDescription("The stargazer gains an innate sense of direction. He gains a +4 bonus on {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and " +
                    "Perception checks.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealThePatriarchFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDancePatriarchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheRiderFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheRiderFeature", bp => {
                bp.SetName("Sidereal Arcana - The Rider");
                bp.SetDescription("The stargazer and his mount ride as one. While he is mounted both rider and mount gain +1 on all saves and an immunity to difficult terrain.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheRiderFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceRiderBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheStargazerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheStargazerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Stargazer");
                bp.SetDescription("The constellation that shares his name warns the stargazer of danger. The stargazer gains a +2 insight bonus on " +
                    "initiative checks and is not considered flat-footed before he acts in combat, although this does not allow him to act if he " +
                    "could not otherwise do so.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheStargazerFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStargazerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheStrangerFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheStrangerFeature", bp => {
                bp.SetName("Sidereal Arcana - The Stranger");
                bp.SetDescription("The stargazer learns to blend seamlessly into others’ cultures. He can use vanish on himself as a swift action spell-like ability " +
                    "a number of times equal to half his stargazer level (min 1) per day.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheStrangerFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceStrangerBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheThrushFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheThrushFeature", bp => {
                bp.SetName("Sidereal Arcana - The Thrush");
                bp.SetDescription("The stargazer’s voice becomes harmonious. The stargazer gains a bonus equal to half his class level on charisma skill checks (minimum 1).");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheThrushFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceThrushBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StargazerSiderealFinalTheWagonFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSiderealFinalTheWagonFeature", bp => {
                bp.SetName("Sidereal Arcana - The Wagon");
                bp.SetDescription("The stargazer’s movement becomes swift and steady. He gains a +10-foot enhancement bonus to his movement speed. In addition, " +
                    "three times per day as a swift action, he can gain the effects of freedom of movement for 1 round.");
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.m_Feature = StargazerSiderealTheWagonFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StargazerStarsDanceWagonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });





            var StargazerSiderealArcanaSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StargazerSiderealArcanaSelection", bp => {
                bp.SetName("Sidereal Arcana");
                bp.SetDescription("As he studies the skies, the stargazer binds himself to constellations from the Cosmic Caravan, drawing on their unearthly power. At 2nd level and every 2 " +
                    "class levels thereafter, the stargazer gains one sidereal arcana of his choice.");
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllowNonContextActions = false;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    StargazerSiderealTheBridgeFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheDaughterFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheFollowerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheLanternBearerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheMotherFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheNewlywedsFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealThePackFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealThePatriarchFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheRiderFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheStargazerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheStrangerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheThrushFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealTheWagonFeature.ToReference<BlueprintFeatureReference>(),

                };
            });
            var StargazerSiderealArcanaFinalSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StargazerSiderealArcanaFinalSelection", bp => {
                bp.SetName("Sidereal Arcana");
                bp.SetDescription("As he studies the skies, the stargazer binds himself to constellations from the Cosmic Caravan, drawing on their unearthly power. At 2nd level and every 2 " +
                    "class levels thereafter, the stargazer gains one sidereal arcana of his choice.");
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllowNonContextActions = false;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    StargazerSiderealFinalTheBridgeFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheDaughterFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheFollowerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheLanternBearerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheMotherFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheNewlywedsFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalThePackFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalThePatriarchFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheRiderFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheStargazerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheStrangerFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheThrushFeature.ToReference<BlueprintFeatureReference>(),
                    StargazerSiderealFinalTheWagonFeature.ToReference<BlueprintFeatureReference>()
                };
            });

            var StargazerStarsDanceFeature = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDanceFeature", bp => {
                bp.SetName("Stars’ Dance");
                bp.SetDescription("At 10th level, the stargazer’s mastery of the Cosmic Caravan reaches its zenith. Once per day, the stargazer can replace one of his sidereal arcana with any other one.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = StargazerStarsDanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StargazerStarsDanceBridgeSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceDaughterSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceFollowerSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceLanternBearerSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceMotherSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceNewlywedsSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDancePackSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDancePatriarchSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceRiderSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceStargazerSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceStrangerSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceThrushSwapParent.ToReference<BlueprintUnitFactReference>(),
                        StargazerStarsDanceWagonSwapParent.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Spellbook Progression

            #region Resources
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var BardClass = Resources.GetBlueprint<BlueprintCharacterClass>("772c83a25e2268e448e841dcd548235f");
            var BloodragerClass = Resources.GetBlueprint<BlueprintCharacterClass>("d77e67a814d686842802c9cfd8ef8499");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var RangerClass = Resources.GetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var SkaldClass = Resources.GetBlueprint<BlueprintCharacterClass>("6afa347d804838b48bda16acb0573dc0");
            var SorcererClass = Resources.GetBlueprint<BlueprintCharacterClass>("b3a505fb61437dc4097f43c3f8f9a4cf");
            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WizardClass = Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");

            var UnletteredArcanistArchetype = Resources.GetBlueprint<BlueprintArchetype>("44f3ba33839a87f48a66b2b9b2f7c69b");
            var EldritchFontArchetype = Resources.GetBlueprint<BlueprintArchetype>("2f35608fc073c04448a9c162a20852ce");
            var NatureMageArchetype = Resources.GetBlueprint<BlueprintArchetype>("26185cfb81b34e778ad370407300de9a");
            var CrossbloodedArchetype = Resources.GetBlueprint<BlueprintArchetype>("e66a112ab57f69e418f1f11d5d2f3248");
            var EldritchScionArchetype = Resources.GetBlueprint<BlueprintArchetype>("d078b2ef073f2814c9e338a789d97b73");
            var EldritchScoundrelArchetype = Resources.GetBlueprint<BlueprintArchetype>("57f93dd8423c97c49989501281296c4a");
            var EmpyrealSorcererArchetype = Resources.GetBlueprint<BlueprintArchetype>("aa00d945f7cf6c34c909a29a25f2df38");
            var ExploiterWizardArchetype = Resources.GetBlueprint<BlueprintArchetype>("6d2027b4eec590c41adb5caeb7bb026d");
            var SwordSaintArchetype = Resources.GetBlueprint<BlueprintArchetype>("7d6678f2160018049814838af2ab4236");
            var SageSorcererArchetype = Resources.GetBlueprint<BlueprintArchetype>("00b990c8be2117e45ae6514ee4ef561c");
            var AccursedWitchArchetype = Resources.GetBlueprint<BlueprintArchetype>("c5f6e53e71059fb4d802ce81a277a12d");
            var LeyLineGuardianArchetype = Resources.GetBlueprint<BlueprintArchetype>("ee92a9813c04b1440a3af3c766b6b02d");
            var ThassilonianSpecialistArchetype = Resources.GetBlueprint<BlueprintArchetype>("55a8ce15e30d71547a44c69bb2e8a84f");
            var AngelfireApostleArchetype = Resources.GetBlueprint<BlueprintArchetype>("857bc9fadf70f294795a9cba974a48b8");
            var CrusaderArchetype = Resources.GetBlueprint<BlueprintArchetype>("6bfb7e74b530f3749b590286dd2b9b30");
            var FeyspeakerArchetype = Resources.GetBlueprint<BlueprintArchetype>("da69747aa3dd0044ebff5f3d701cdde3");
            var DivineGuardianArchetype = Resources.GetBlueprint<BlueprintArchetype>("5693945afac189a469ef970eac8f71d9");
            var WarriorOfTheHolyLightArchetype = Resources.GetBlueprint<BlueprintArchetype>("cc70a9fdcd8781a4ca2f0e594f066964");

            var LivingScriptureArchetype = Resources.GetModBlueprint<BlueprintArchetype>("LivingScriptureArchetype");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            var SkulkingHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SkulkingHunterArchetype");
            var SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            var ArcanistSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("33903fe5c4abeaa45bc249adb9d98848");
            var UnletteredArcanistSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("b242f991107095d41ae129f46ed5d97a");
            var EldritchFontSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("67b574abd845f3e42983ea6eb53a6f90");
            var NatureMageSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("3ed7e38dc8134af28e1a2b105f74fb7b");
            var BardSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("bc04fc157a8801d41b877ad0d9af03dd");
            var BloodragerSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("e19484252c2f80e4a9439b3681b20f00");
            var CrossbloodedSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("cb0be5988031ebe4c947086a1170eacc");
            var EldritchScionSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("e2763fbfdb91920458c4686c3e7ed085");
            var EldritchScoundrelSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4f96fb20f06b7494a8b2bd731a70af6c");
            var EmpyrealSorcererSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("cb9d75e5cb08572439a6b5cd562297a5");
            var ExploiterWizardSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("d09794fb6f93e4a40929a965b434070d");
            var SageSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("cc2052732997b654e93eac268a39a0a9");
            var SkaldSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("8f159d2f22ced074ea32995eb5a446a0");
            var SorcererSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("b3db3766a4b605040b366265e2af0e50");
            var SwordSaintSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("682545e11e5306c45b14ca78bcbe3e62");
            var ThassilonianAbjurationSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("58b15cc36ceda8942a7a29aafa755452");
            var ThassilonianConjurationSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("cbc30bcc7b8adec48a53a6540f5596ae");
            var ThassilonianEnchantmentSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("9e4b96d7b02f8c8498964aeee6eaef9b");
            var ThassilonianEvocationSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("05b105ddee654db4fb1547ba48ffa160");
            var ThassilonianIllusionSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("74b87962a97d56c4583979216631eb4c");
            var ThassilonianNecromancySpellbook = Resources.GetBlueprint<BlueprintSpellbook>("97cd3941ce333ce46ae09436287ed699");
            var ThassilonianTransmutationSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("5785f40e7b1bfc94ea078e7156aa9711");
            var WitchSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("dd04f9239f655ea438976742728e4909");
            var AccursedWitchSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("b897fe0947e4b804082b1a687c21e6e2");
            var LeyLineGuardianSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("fb4e216b322d3924e9c7041016b46999");
            var WizardSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("5a38c9ac8607890409fcb8f6342da6f4");
            var AngelfireApostleSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("d0313b3110357b14ebd3102c2d4cde20");
            var CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
            var DruidSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("fc78193f68150454483a7eea8b605b71");
            var FeyspeakerSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("c8c471f1f9889e1408347d3c7987b4f1");
            var InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
            var OracleSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("6c03364712b415941a98f74522a81273");
            var PaladinSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("bce4989b070ce924b986bf346f59e885");
            var RangerSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("762858a4a28eaaf43aa00f50441d7027");
            var ShamanSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("44f16931dabdff643bfe2a48138e769f");
            var HunterSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("885cd422aa357e2409146b38bb1fec51");
            var WarpriestSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("7d7d51be2948d2544b3c2e1596fd7603");
            var MagusSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("5d8d04e76dff6c5439de99af0d57be63");
            var ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");

            var ThassilonianAbjurationFeature = Resources.GetBlueprint<BlueprintFeatureReplaceSpellbook>("15c681d5a76c1a742abe2760376ddf6d");
            var ThassilonianConjurationFeature = Resources.GetBlueprint<BlueprintFeatureReplaceSpellbook>("1a258cd8e93461a4ab011c73a2c43dac");
            var ThassilonianEnchantmentFeature = Resources.GetBlueprint<BlueprintFeatureReplaceSpellbook>("e1ebc61a71c55054991863a5f6f6d2c2");
            var ThassilonianEvocationFeature = Resources.GetBlueprint<BlueprintFeatureReplaceSpellbook>("5e33543285d1c3d49b55282cf466bef3");
            var ThassilonianIllusionFeature = Resources.GetBlueprint<BlueprintFeatureReplaceSpellbook>("aa271e69902044b47a8e62c4e58a9dcb");
            var ThassilonianNecromancyFeature = Resources.GetBlueprint<BlueprintFeatureReplaceSpellbook>("fb343ede45ca1a84496c91c190a847ff");
            var ThassilonianTransmutationFeature = Resources.GetBlueprint<BlueprintFeatureReplaceSpellbook>("dd163630abbdace4e85284c55d269867");

            var MysticTheurgeOracleLevelSelection1 = Resources.GetBlueprint<BlueprintFeatureSelection>("3cb30f805e79a7944b0d0174c9a157b7");
            var MysticTheurgeOracleLevelSelection2 = Resources.GetBlueprint<BlueprintFeatureSelection>("5290b422c5c02c44f99291688e798d8f");
            var MysticTheurgeOracleLevelSelection3 = Resources.GetBlueprint<BlueprintFeatureSelection>("f8cf4644c7ff15d4799a59593b900091");
            var MysticTheurgeOracleLevelSelection4 = Resources.GetBlueprint<BlueprintFeatureSelection>("7f526cef9fcb7934aaaca1bf3d26fc0f");
            var MysticTheurgeOracleLevelSelection5 = Resources.GetBlueprint<BlueprintFeatureSelection>("2bf8b6fa7082d45409537d6112cb5647");
            var MysticTheurgeOracleLevelSelection6 = Resources.GetBlueprint<BlueprintFeatureSelection>("798bf6e210d9a0e42bf960390b5991ba");
            var MysticTheurgeOracleLevelSelection7 = Resources.GetBlueprint<BlueprintFeatureSelection>("6bbc04c6c95fc694788ddb265dc15ea2");
            var MysticTheurgeOracleLevelSelection8 = Resources.GetBlueprint<BlueprintFeatureSelection>("f6c08ecbd77a43b4dbc0252afc2cb578");
            var MysticTheurgeOracleLevelSelection9 = Resources.GetBlueprint<BlueprintFeatureSelection>("57dae97f6e587f54e9c0559c6fa6590f");
            var MysticTheurgeInquisitorLevelSelection1 = Resources.GetBlueprint<BlueprintFeatureSelection>("8ae18c62c0fbfeb4ea77f877883947fd");
            var MysticTheurgeInquisitorLevelSelection2 = Resources.GetBlueprint<BlueprintFeatureSelection>("f78f63d364bd9fe4ea2885d95432c068");
            var MysticTheurgeInquisitorLevelSelection3 = Resources.GetBlueprint<BlueprintFeatureSelection>("5f6c7b84edc68a146955be0600de4095");
            var MysticTheurgeInquisitorLevelSelection4 = Resources.GetBlueprint<BlueprintFeatureSelection>("b93df7bf0405a974cafcda21cbd070f1");
            var MysticTheurgeInquisitorLevelSelection5 = Resources.GetBlueprint<BlueprintFeatureSelection>("b7ed1fc44730bd1459c57763378a5a97");
            var MysticTheurgeInquisitorLevelSelection6 = Resources.GetBlueprint<BlueprintFeatureSelection>("200dec6712442e74c85803a1af72397a");
            #endregion
            #region New Spellbooks - Arcane
            var StargazerArcanist = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerArcanist", bp => {
                bp.SetName("Arcanist");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = UnletteredArcanistArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = EldritchFontArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = NatureMageArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ArcanistSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerArcanistEldritchFont = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerArcanistEldritchFont", bp => {
                bp.SetName("Arcanist");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = EldritchFontArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = EldritchFontSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerArcanistNatureMage = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerArcanistNatureMage", bp => {
                bp.SetName("Arcanist");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = NatureMageArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = NatureMageSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerArcanistUnletteredArcanist = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerArcanistUnletteredArcanist", bp => {
                bp.SetName("Arcanist");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = UnletteredArcanistArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = UnletteredArcanistSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerBard = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerBard", bp => {
                bp.SetName("Bard");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = BardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = BardSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerBloodrager = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerBloodrager", bp => {
                bp.SetName("Bloodrager");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = BloodragerClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = BloodragerSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerCrossblooded = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerCrossblooded", bp => {
                bp.SetName("Socerer");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = CrossbloodedArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = CrossbloodedSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerEldritchScion = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerEldritchScion", bp => {
                bp.SetName("Eldritch Scion");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = EldritchScionArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = EldritchScionSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerEldritchScoundrel = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerEldritchScoundrel", bp => {
                bp.SetName("Eldritch Scoundrel");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = EldritchScoundrelArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = EldritchScoundrelSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerEmpyrealSorcerer = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerEmpyrealSorcerer", bp => {
                bp.SetName("Empyreal Sorcerer");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = EmpyrealSorcererArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = EmpyrealSorcererSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerExploiterWizard = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerExploiterWizard", bp => {
                bp.SetName("Exploiter Wizard");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ExploiterWizardArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ExploiterWizardSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerMagus = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerMagus", bp => {
                bp.SetName("Magus");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = EldritchScionArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwordSaintArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = MagusSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerSageSorcerer = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerSageSorcerer", bp => {
                bp.SetName("Sage Socerer");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SageSorcererArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = SageSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerSkald = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerSkald", bp => {
                bp.SetName("Skald");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = SkaldClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = SkaldSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerSorcerer = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerSorcerer", bp => {
                bp.SetName("Sorcerer");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = EmpyrealSorcererArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = CrossbloodedArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwordSaintArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = SorcererSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerSwordSaint = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerSwordSaint", bp => {
                bp.SetName("Sword Saint");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwordSaintArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = SwordSaintSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerThassilonianAbjuration = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerThassilonianAbjuration", bp => {
                bp.SetName("Thassilonian Specialist — Envy");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ThassilonianAbjurationFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ThassilonianAbjurationSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerThassilonianConjuration = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerThassilonianConjuration", bp => {
                bp.SetName("Thassilonian Specialist — Sloth");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ThassilonianConjurationFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ThassilonianConjurationSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerThassilonianEnchantment = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerThassilonianEnchantment", bp => {
                bp.SetName("Thassilonian Specialist — Lust");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ThassilonianEnchantmentFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ThassilonianEnchantmentSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerThassilonianEvocation = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerThassilonianEvocation", bp => {
                bp.SetName("Thassilonian Specialist — Wrath");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ThassilonianEvocationFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ThassilonianEvocationSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerThassilonianIllusion = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerThassilonianIllusion", bp => {
                bp.SetName("Thassilonian Specialist — Pride");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ThassilonianIllusionFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ThassilonianIllusionSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerThassilonianNecromancy = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerThassilonianNecromancy", bp => {
                bp.SetName("Thassilonian Specialist — Gluttony");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ThassilonianNecromancyFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ThassilonianNecromancySpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerThassilonianTransmutation = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerThassilonianTransmutation", bp => {
                bp.SetName("Thassilonian Specialist — Greed");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = ThassilonianTransmutationFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = ThassilonianTransmutationSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerWitch = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerWitch", bp => {
                bp.SetName("Witch");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LeyLineGuardianArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = WitchSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerWitchAccursed = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerWitchAccursed", bp => {
                bp.SetName("Witch");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = AccursedWitchSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerWitchLeyLineGuardian = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerWitchLeyLineGuardian", bp => {
                bp.SetName("Witch");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LeyLineGuardianArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = LeyLineGuardianSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            var StargazerWizard = Helpers.CreateBlueprint<BlueprintFeatureReplaceSpellbook>("StargazerWizard", bp => {
                bp.SetName("Wizard");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ThassilonianSpecialistArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ExploiterWizardArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_Spellbook = WizardSpellbook.ToReference<BlueprintSpellbookReference>();
            });
            #endregion
            #region New Spellbooks - Divine
            var StargazerAngelfireApostleLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerAngelfireApostleLevelUp", bp => {
                bp.SetName("Angelfire Apostle");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = AngelfireApostleSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerAngelfireApostleProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerAngelfireApostleProgression", bp => {
                bp.SetName("Angelfire Apostle");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AngelfireApostleArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(2, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(3, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(4, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(5, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(6, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(7, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(8, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(9, StargazerAngelfireApostleLevelUp),
                    Helpers.LevelEntry(10, StargazerAngelfireApostleLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerClericLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerClericLevelUp", bp => {
                bp.SetName("Cleric");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = ClericSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerClericProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerClericProgression", bp => {
                bp.SetName("Cleric");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AngelfireApostleArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = CrusaderArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerClericLevelUp),
                    Helpers.LevelEntry(2, StargazerClericLevelUp),
                    Helpers.LevelEntry(3, StargazerClericLevelUp),
                    Helpers.LevelEntry(4, StargazerClericLevelUp),
                    Helpers.LevelEntry(5, StargazerClericLevelUp),
                    Helpers.LevelEntry(6, StargazerClericLevelUp),
                    Helpers.LevelEntry(7, StargazerClericLevelUp),
                    Helpers.LevelEntry(8, StargazerClericLevelUp),
                    Helpers.LevelEntry(9, StargazerClericLevelUp),
                    Helpers.LevelEntry(10, StargazerClericLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerCrusaderLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerCrusaderLevelUp", bp => {
                bp.SetName("Crusader");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = CrusaderSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerCrusaderProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerCrusaderProgression", bp => {
                bp.SetName("Crusader");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = CrusaderArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(2, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(3, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(4, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(5, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(6, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(7, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(8, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(9, StargazerCrusaderLevelUp),
                    Helpers.LevelEntry(10, StargazerCrusaderLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerDruidLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerDruidLevelUp", bp => {
                bp.SetName("Druid");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = DruidSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerDruidProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerDruidProgression", bp => {
                bp.SetName("Druid");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = FeyspeakerArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerDruidLevelUp),
                    Helpers.LevelEntry(2, StargazerDruidLevelUp),
                    Helpers.LevelEntry(3, StargazerDruidLevelUp),
                    Helpers.LevelEntry(4, StargazerDruidLevelUp),
                    Helpers.LevelEntry(5, StargazerDruidLevelUp),
                    Helpers.LevelEntry(6, StargazerDruidLevelUp),
                    Helpers.LevelEntry(7, StargazerDruidLevelUp),
                    Helpers.LevelEntry(8, StargazerDruidLevelUp),
                    Helpers.LevelEntry(9, StargazerDruidLevelUp),
                    Helpers.LevelEntry(10, StargazerDruidLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerFeyspeakerLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerFeyspeakerLevelUp", bp => {
                bp.SetName("Feyspeaker");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = FeyspeakerSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerFeyspeakerProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerFeyspeakerProgression", bp => {
                bp.SetName("Feyspeaker");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = FeyspeakerArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(2, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(3, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(4, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(5, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(6, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(7, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(8, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(9, StargazerFeyspeakerLevelUp),
                    Helpers.LevelEntry(10, StargazerFeyspeakerLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerInquisitorLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerInquisitorLevelUp", bp => {
                bp.SetName("Inquisitor");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = InquisitorSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerInquisitorProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerInquisitorProgression", bp => {
                bp.SetName("Inquisitor");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {                    
                    Helpers.LevelEntry(5, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(6, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(7, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(8, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(9, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(10, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(11, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(12, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(13, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection5, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(14, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(15, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(16, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(17, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(18, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(19, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(20, StargazerInquisitorLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = StargazerClass.ToReference<BlueprintCharacterClassReference>();
            });
            var StargazerOracleLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerOracleLevelUp", bp => {
                bp.SetName("Oracle");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = OracleSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerOracleProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerOracleProgression", bp => {
                bp.SetName("Oracle");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(2, StargazerOracleLevelUp),
                    Helpers.LevelEntry(3, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection1),
                    Helpers.LevelEntry(4, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection2),
                    Helpers.LevelEntry(5, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection1, MysticTheurgeOracleLevelSelection2),
                    Helpers.LevelEntry(6, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection3),
                    Helpers.LevelEntry(7, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection1, MysticTheurgeOracleLevelSelection2, MysticTheurgeOracleLevelSelection3),
                    Helpers.LevelEntry(8, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection4),
                    Helpers.LevelEntry(9, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection2, MysticTheurgeOracleLevelSelection3, MysticTheurgeOracleLevelSelection4),
                    Helpers.LevelEntry(10, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection5),
                    Helpers.LevelEntry(11, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection2, MysticTheurgeOracleLevelSelection3, MysticTheurgeOracleLevelSelection4, MysticTheurgeOracleLevelSelection5),
                    Helpers.LevelEntry(12, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection4),
                    Helpers.LevelEntry(13, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection4, MysticTheurgeOracleLevelSelection5, MysticTheurgeOracleLevelSelection6),
                    Helpers.LevelEntry(14, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection7),
                    Helpers.LevelEntry(15, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection6, MysticTheurgeOracleLevelSelection7),
                    Helpers.LevelEntry(16, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection8),
                    Helpers.LevelEntry(17, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection7, MysticTheurgeOracleLevelSelection8),
                    Helpers.LevelEntry(18, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection9),
                    Helpers.LevelEntry(19, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection8, MysticTheurgeOracleLevelSelection9),
                    Helpers.LevelEntry(20, StargazerOracleLevelUp, MysticTheurgeOracleLevelSelection9)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = StargazerClass.ToReference<BlueprintCharacterClassReference>();
            });
            var StargazerPaladinLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerPaladinLevelUp", bp => {
                bp.SetName("Paladin");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = PaladinSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerPaladinProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerPaladinProgression", bp => {
                bp.SetName("Paladin");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = DivineGuardianArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = WarriorOfTheHolyLightArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(2, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(3, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(4, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(5, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(6, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(7, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(8, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(9, StargazerPaladinLevelUp),
                    Helpers.LevelEntry(10, StargazerPaladinLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerRangerLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerRangerLevelUp", bp => {
                bp.SetName("Ranger");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = RangerSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerRangerProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerRangerProgression", bp => {
                bp.SetName("Ranger");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = RangerClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerRangerLevelUp),
                    Helpers.LevelEntry(2, StargazerRangerLevelUp),
                    Helpers.LevelEntry(3, StargazerRangerLevelUp),
                    Helpers.LevelEntry(4, StargazerRangerLevelUp),
                    Helpers.LevelEntry(5, StargazerRangerLevelUp),
                    Helpers.LevelEntry(6, StargazerRangerLevelUp),
                    Helpers.LevelEntry(7, StargazerRangerLevelUp),
                    Helpers.LevelEntry(8, StargazerRangerLevelUp),
                    Helpers.LevelEntry(9, StargazerRangerLevelUp),
                    Helpers.LevelEntry(10, StargazerRangerLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerShamanLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerShamanLevelUp", bp => {
                bp.SetName("Shaman");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = ShamanSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerShamanProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerShamanProgression", bp => {
                bp.SetName("Shaman");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerShamanLevelUp),
                    Helpers.LevelEntry(2, StargazerShamanLevelUp),
                    Helpers.LevelEntry(3, StargazerShamanLevelUp),
                    Helpers.LevelEntry(4, StargazerShamanLevelUp),
                    Helpers.LevelEntry(5, StargazerShamanLevelUp),
                    Helpers.LevelEntry(6, StargazerShamanLevelUp),
                    Helpers.LevelEntry(7, StargazerShamanLevelUp),
                    Helpers.LevelEntry(8, StargazerShamanLevelUp),
                    Helpers.LevelEntry(9, StargazerShamanLevelUp),
                    Helpers.LevelEntry(10, StargazerShamanLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerHunterLevelUp", bp => {
                bp.SetName("Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = HunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerHunterProgression", bp => {
                bp.SetName("Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerHunterLevelUp),
                    Helpers.LevelEntry(2, StargazerHunterLevelUp),
                    Helpers.LevelEntry(3, StargazerHunterLevelUp),
                    Helpers.LevelEntry(4, StargazerHunterLevelUp),
                    Helpers.LevelEntry(5, StargazerHunterLevelUp),
                    Helpers.LevelEntry(6, StargazerHunterLevelUp),
                    Helpers.LevelEntry(7, StargazerHunterLevelUp),
                    Helpers.LevelEntry(8, StargazerHunterLevelUp),
                    Helpers.LevelEntry(9, StargazerHunterLevelUp),
                    Helpers.LevelEntry(10, StargazerHunterLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            var StargazerWarpriestLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerWarpriestLevelUp", bp => {
                bp.SetName("Warpriest");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = WarpriestSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerWarpriestProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerWarpriestProgression", bp => {
                bp.SetName("Warpriest");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(2, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(3, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(4, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(5, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(6, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(7, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(8, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(9, StargazerWarpriestLevelUp),
                    Helpers.LevelEntry(10, StargazerWarpriestLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            #endregion
            #region New Spellbooks added by Expanded Content

            var LivingScriptureSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("LivingScriptureSpellbook");
            var StargazerLivingScriptureLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerLivingScriptureLevelUp", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = LivingScriptureSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerLivingScriptureProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerLivingScriptureProgression", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(2, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(3, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(4, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(5, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(6, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(7, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(8, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(9, StargazerLivingScriptureLevelUp),
                    Helpers.LevelEntry(10, StargazerLivingScriptureLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });

            var MysticTheurgeRavenerHunterLevelSelection1 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection1");
            var MysticTheurgeRavenerHunterLevelSelection2 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection2");
            var MysticTheurgeRavenerHunterLevelSelection3 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection3");
            var MysticTheurgeRavenerHunterLevelSelection4 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection4");
            var MysticTheurgeRavenerHunterLevelSelection5 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection5");
            var MysticTheurgeRavenerHunterLevelSelection6 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection6");
            var RavenerHunterSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("RavenerHunterSpellbook");
            var StargazerRavenerHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerRavenerHunterLevelUp", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerRavenerHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerRavenerHunterProgression", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(6, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(7, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(8, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(9, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(10, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(11, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(12, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(13, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection5, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(14, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(15, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(16, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(17, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(18, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(19, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(20, StargazerRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = StargazerClass.ToReference<BlueprintCharacterClassReference>();
            });

            var MysticTheurgeSkulkingHunterLevelSelection1 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection1");
            var MysticTheurgeSkulkingHunterLevelSelection2 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection2");
            var MysticTheurgeSkulkingHunterLevelSelection3 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection3");
            var MysticTheurgeSkulkingHunterLevelSelection4 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection4");
            var MysticTheurgeSkulkingHunterLevelSelection5 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection5");
            var MysticTheurgeSkulkingHunterLevelSelection6 = Resources.GetModBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection6");
            var SkulkingHunterSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("SkulkingHunterSpellbook");
            var StargazerSkulkingHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSkulkingHunterLevelUp", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SkulkingHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerSkulkingHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerSkulkingHunterProgression", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(6, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(7, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(8, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(9, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(10, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(11, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(12, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(13, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection5, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(14, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(15, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(16, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(17, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(18, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(19, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(20, StargazerSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = StargazerClass.ToReference<BlueprintCharacterClassReference>();
            });

            var SwornOfTheEldestSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("SwornOfTheEldestSpellbook");
            var StargazerSwornOfTheEldestLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("StargazerSwornOfTheEldestLevelUp", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SwornOfTheEldestSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var StargazerSwornOfTheEldestProgression = Helpers.CreateBlueprint<BlueprintProgression>("StargazerSwornOfTheEldestProgression", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(6, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(7, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(8, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(9, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(10, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(11, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(12, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(13, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection5, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(14, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(15, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(16, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(17, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(18, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(19, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(20, StargazerSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = StargazerClass.ToReference<BlueprintCharacterClassReference>();
            });

            #endregion


            //Spellbook Selection
            var StargazerSpellbook = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StargazerSpellbook", bp => {
                bp.SetName("Stargazer spellcasting");
                bp.SetDescription("At 1st level, and at every level thereafter, a Stargazer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a spellcasting class " +
                    "he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for additional spells per day, " +
                    "spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Stargazer, he must decide to which class " +
                    "he adds the new level for purposes of determining spells per day.");
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    StargazerArcanist.ToReference<BlueprintFeatureReference>(),
                    StargazerArcanistEldritchFont.ToReference<BlueprintFeatureReference>(),
                    StargazerArcanistNatureMage.ToReference<BlueprintFeatureReference>(),
                    StargazerArcanistUnletteredArcanist.ToReference<BlueprintFeatureReference>(),
                    StargazerBard.ToReference<BlueprintFeatureReference>(),
                    StargazerBloodrager.ToReference<BlueprintFeatureReference>(),
                    StargazerCrossblooded.ToReference<BlueprintFeatureReference>(),
                    StargazerEldritchScion.ToReference<BlueprintFeatureReference>(),
                    StargazerEldritchScoundrel.ToReference<BlueprintFeatureReference>(),
                    StargazerEmpyrealSorcerer.ToReference<BlueprintFeatureReference>(),
                    StargazerExploiterWizard.ToReference<BlueprintFeatureReference>(),
                    StargazerMagus.ToReference<BlueprintFeatureReference>(),
                    StargazerSageSorcerer.ToReference<BlueprintFeatureReference>(),
                    StargazerSkald.ToReference<BlueprintFeatureReference>(),
                    StargazerSorcerer.ToReference<BlueprintFeatureReference>(),
                    StargazerSwordSaint.ToReference<BlueprintFeatureReference>(),
                    StargazerThassilonianAbjuration.ToReference<BlueprintFeatureReference>(),
                    StargazerThassilonianConjuration.ToReference<BlueprintFeatureReference>(),
                    StargazerThassilonianEnchantment.ToReference<BlueprintFeatureReference>(),
                    StargazerThassilonianEvocation.ToReference<BlueprintFeatureReference>(),
                    StargazerThassilonianIllusion.ToReference<BlueprintFeatureReference>(),
                    StargazerThassilonianNecromancy.ToReference<BlueprintFeatureReference>(),
                    StargazerThassilonianTransmutation.ToReference<BlueprintFeatureReference>(),
                    StargazerWitch.ToReference<BlueprintFeatureReference>(),
                    StargazerWitchAccursed.ToReference<BlueprintFeatureReference>(),
                    StargazerWitchLeyLineGuardian.ToReference<BlueprintFeatureReference>(),
                    StargazerWizard.ToReference<BlueprintFeatureReference>(),
                    StargazerAngelfireApostleProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerClericProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerCrusaderProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerDruidProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerFeyspeakerProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerInquisitorProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerOracleProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerPaladinProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerRangerProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerShamanProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerHunterProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerWarpriestProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerLivingScriptureProgression.ToReference<BlueprintFeatureReference>(),
                    StargazerRavenerHunterProgression.ToReference<BlueprintFeatureReference>(),
                };
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.ReplaceSpellbook };
                bp.Mode = SelectionMode.Default;
                bp.ShowThisSelection = false;
                bp.Group = FeatureGroup.ReplaceSpellbook;
            });
            #endregion
            StargazerProgression.LevelEntries = new LevelEntry[] {
                Helpers.LevelEntry(1, StargazerGuidingLightFeatureSelection, StargazerMysteryMagicHexFeatureSelection, StargazerSpellbook),
                Helpers.LevelEntry(2, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(3, StargazerMysteryMagicStarsDomainFeatureSelection),
                Helpers.LevelEntry(4, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(5, StargazerMysteryMagicCOMSFeature),
                Helpers.LevelEntry(6, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(7, StargazerMysteryMagicHexFeatureSelection),
                Helpers.LevelEntry(8, StargazerSiderealArcanaSelection),
                Helpers.LevelEntry(9, StargazerMysteryMagicHexFeatureSelection),
                Helpers.LevelEntry(10, StargazerSiderealArcanaFinalSelection, StargazerStarsDanceFeature)
            };
            StargazerProgression.UIGroups = new UIGroup[] {
                Helpers.CreateUIGroup(StargazerSiderealArcanaSelection, StargazerSiderealArcanaFinalSelection),
            };
            if (ModSettings.AddedContent.Classes.IsDisabled("Stargazer")) { return; }
            Helpers.RegisterClass(StargazerClass);
        }
    }
}
