using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using static ExpandedContent.Utilities.DeityTools;

namespace ExpandedContent.Tweaks.Domains {
    internal class RiversDomain {

        public static void AddRiversDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var WaterDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("8f8d0892cbe15b54ebe10552603349b2");
            var WaterDomainCapstone = Resources.GetBlueprint<BlueprintFeature>("6ec8672a9dd06604b93d56c33904aee9");
            var AirBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");
            var Icon_RiversDomainBaseAbility = AssetLoader.LoadInternal("Skills", "Icon_RiversDomainBaseAbility.jpg");

            var RiversDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("RiversDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });
            var RiversDomainBaseBuff = Helpers.CreateBuff("RiversDomainBaseBuff", bp => {
                bp.SetName("Current Flow");
                bp.SetDescription("As a free action, you can increase your speed by 10 feet. Gain a bonus on Mobility checks equal to your 1/2 your cleric level " +
                    "(minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can use this ability a number of times " +
                    "per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = Icon_RiversDomainBaseAbility;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillMobility;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
            });
            var RiversDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("RiversDomainBaseAbility", bp => {
                bp.SetName("Current Flow");
                bp.SetDescription("As a free action, you can increase your speed by 10 feet. Gain a bonus on Mobility checks equal to your 1/2 your cleric level " +
                    "(minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can use this ability a number of times " +
                    "per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = RiversDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = RiversDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
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
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
                bp.m_Icon = Icon_RiversDomainBaseAbility;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var HydraulicPushAbility = Resources.GetModBlueprint<BlueprintAbility>("HydraulicPushAbility");
            var SlipstreamAbility = Resources.GetModBlueprint<BlueprintAbility>("SlipstreamAbility");
            var StinkingCloudSpell = Resources.GetBlueprint<BlueprintAbility>("68a9e6d7256f1354289a39003a46d826");
            var FreedomOfMovementSpell = Resources.GetBlueprint<BlueprintAbility>("0087fc2d64b6095478bc7b8d7d512caf");
            var ElementalBodyIIWaterSpell = Resources.GetBlueprint<BlueprintAbility>("935b63be93800394f8f7ae17060b041a");
            var ConeOfColdSpell = Resources.GetBlueprint<BlueprintAbility>("e7c530f8137630f4d9d7ee1aa7b1edc0");
            var ElementalBodyIVWaterSpell = Resources.GetBlueprint<BlueprintAbility>("96d2ab91f2d2329459a8dab496c5bede");
            var HorridWiltingSpell = Resources.GetBlueprint<BlueprintAbility>("08323922485f7e246acb3d2276515526");
            var TsunamiSpell = Resources.GetBlueprint<BlueprintAbility>("d8144161e352ca846a73cf90e85bf9ac");
            var RiversDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("RiversDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HydraulicPushAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SlipstreamAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            StinkingCloudSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FreedomOfMovementSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalBodyIIWaterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ConeOfColdSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalBodyIVWaterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HorridWiltingSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TsunamiSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var RiversDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("RiversDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RiversDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var RiversDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("RiversDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { RiversDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = RiversDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { RiversDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = RiversDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rivers Subdomain");
                bp.SetDescription("\nFreshwater rivers and streams are the lifeblood of both the land and your life.\nCurrent Flow: As a free action, you can increase your speed by 10 feet. " +
                    "Gain a bonus on Mobility checks equal to your 1/2 your cleric level (minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.\nCold Resistance: At 6th level, you gain resist cold 10. This resistance increases to 20 at 12th " +
                    "level. At 20th level, you gain immunity to cold.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var RiversDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("RiversDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = RiversDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var RiversDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("RiversDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RiversDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RiversDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rivers Subdomain");
                bp.SetDescription("\nFreshwater rivers and streams are the lifeblood of both the land and your life.\nCurrent Flow: As a free action, you can increase your speed by 10 feet. " +
                    "Gain a bonus on Mobility checks equal to your 1/2 your cleric level (minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.\nCold Resistance: At 6th level, you gain resist cold 10. This resistance increases to 20 at 12th " +
                    "level. At 20th level, you gain immunity to cold.\nDomain {g|Encyclopedia:Spell}Spells{/g}: hydraulic push, slipstream, stinking cloud, freedom of movement, elemental body IV " +
                    "(water), cone of cold, elemental body IV (water), horrid wilting, tsunami.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.Domain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = -2
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RiversDomainBaseFeature),
                    Helpers.LevelEntry(6, WaterDomainGreaterFeature),
                    Helpers.LevelEntry(12, WaterDomainGreaterFeature),
                    Helpers.LevelEntry(20, WaterDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RiversDomainBaseFeature, WaterDomainGreaterFeature, WaterDomainGreaterFeature, WaterDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var RiversDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("RiversDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rivers Subdomain");
                bp.SetDescription("\nFreshwater rivers and streams are the lifeblood of both the land and your life.\nCurrent Flow: As a free action, you can increase your speed by 10 feet. " +
                    "Gain a bonus on Mobility checks equal to your 1/2 your cleric level (minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.\nCold Resistance: At 6th level, you gain resist cold 10. This resistance increases to 20 at 12th " +
                    "level. At 20th level, you gain immunity to cold.\nDomain {g|Encyclopedia:Spell}Spells{/g}: hydraulic push, slipstream, stinking cloud, freedom of movement, elemental body IV " +
                    "(water), cone of cold, elemental body IV (water), horrid wilting, tsunami.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.ClericSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RiversDomainBaseFeature),
                    Helpers.LevelEntry(6, WaterDomainGreaterFeature),
                    Helpers.LevelEntry(12, WaterDomainGreaterFeature),
                    Helpers.LevelEntry(20, WaterDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RiversDomainBaseFeature, WaterDomainGreaterFeature, WaterDomainGreaterFeature, WaterDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // RiversDomainSpellListFeatureDruid
            var RiversDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("RiversDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RiversDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // RiversDomainProgressionDruid
            var RiversDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("RiversDomainProgressionDruid", bp => {
                bp.SetName("Rivers Subdomain");
                bp.SetDescription("\nFreshwater rivers and streams are the lifeblood of both the land and your life.\nCurrent Flow: As a free action, you can increase your speed by 10 feet. " +
                    "Gain a bonus on Mobility checks equal to your 1/2 your cleric level (minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.\nCold Resistance: At 6th level, you gain resist cold 10. This resistance increases to 20 at 12th " +
                    "level. At 20th level, you gain immunity to cold.\nDomain {g|Encyclopedia:Spell}Spells{/g}: hydraulic push, slipstream, stinking cloud, freedom of movement, elemental body IV " +
                    "(water), cone of cold, elemental body IV (water), horrid wilting, tsunami.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain, FeatureGroup.BlightDruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RiversDomainBaseFeature, RiversDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(6, WaterDomainGreaterFeature),
                    Helpers.LevelEntry(12, WaterDomainGreaterFeature),
                    Helpers.LevelEntry(20, WaterDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RiversDomainBaseFeature, WaterDomainGreaterFeature, WaterDomainGreaterFeature, WaterDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var ProtectionDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("7eb39ba8115a422bb69c702cc20ca58a");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var RiversDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("RiversDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var RiversDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("RiversDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });
            var RiversDomainBaseBuffSeparatist = Helpers.CreateBuff("RiversDomainBaseBuffSeparatist", bp => {
                bp.SetName("Current Flow");
                bp.SetDescription("As a free action, you can increase your speed by 10 feet. Gain a bonus on Mobility checks equal to your 1/2 your cleric level " +
                    "(minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can use this ability a number of times " +
                    "per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = Icon_RiversDomainBaseAbility;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillMobility;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_CustomProperty = SeparatistAsIsProperty.ToReference<BlueprintUnitPropertyReference>();
                });
            });

            var RiversDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("RiversDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Current Flow");
                bp.SetDescription("As a free action, you can increase your speed by 10 feet. Gain a bonus on Mobility checks equal to your 1/2 your cleric level " +
                    "(minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can use this ability a number of times " +
                    "per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = RiversDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = RiversDomainBaseBuffSeparatist.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
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
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 2;
                    c.m_StepLevel = 1;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
                bp.m_Icon = Icon_RiversDomainBaseAbility;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var RiversDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("RiversDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { RiversDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = RiversDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { RiversDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = RiversDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rivers Subdomain");
                bp.SetDescription("\nFreshwater rivers and streams are the lifeblood of both the land and your life.\nCurrent Flow: As a free action, you can increase your speed by 10 feet. " +
                    "Gain a bonus on Mobility checks equal to your 1/2 your cleric level (minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.\nCold Resistance: At 6th level, you gain resist cold 10. This resistance increases to 20 at 12th " +
                    "level. At 20th level, you gain immunity to cold.");
                bp.IsClassFeature = true;
            });

            var RiversDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("RiversDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RiversDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rivers Subdomain");
                bp.SetDescription("\nFreshwater rivers and streams are the lifeblood of both the land and your life.\nCurrent Flow: As a free action, you can increase your speed by 10 feet. " +
                    "Gain a bonus on Mobility checks equal to your 1/2 your cleric level (minimum 1). These effects last for a number of rounds equal to your Wisdom modifier (minimum 1). You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.\nCold Resistance: At 6th level, you gain resist cold 10. This resistance increases to 20 at 12th " +
                    "level. At 20th level, you gain immunity to cold.\nDomain {g|Encyclopedia:Spell}Spells{/g}: hydraulic push, slipstream, stinking cloud, freedom of movement, elemental body IV " +
                    "(water), cone of cold, elemental body IV (water), horrid wilting, tsunami.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RiversDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, WaterDomainGreaterFeature),
                    Helpers.LevelEntry(14, WaterDomainGreaterFeature),
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RiversDomainBaseFeatureSeparatist, WaterDomainGreaterFeature, WaterDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            RiversDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                RiversDomainProgression.ToReference<BlueprintFeatureReference>(),
                RiversDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            RiversDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RiversDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            RiversDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RiversDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            RiversDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RiversDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            RiversDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RiversDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            RiversDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RiversDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Rivers Subdomain")) { return; }
            DomainTools.RegisterDomain(RiversDomainProgression);
            DomainTools.RegisterSecondaryDomain(RiversDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(RiversDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(RiversDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(RiversDomainProgression);
            DomainTools.RegisterTempleDomain(RiversDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(RiversDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(RiversDomainProgression, RiversDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(RiversDomainProgressionSeparatist);
            DomainTools.AllowFakeDivineSpark(RiversDomainAllowed);

        }
    }
}
