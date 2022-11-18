using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.RuleSystem.Rules.Damage;
using ExpandedContent.Config;
using Kingmaker.Craft;
using Kingmaker.ResourceLinks;

namespace ExpandedContent.Tweaks.Domains {
    internal class WhimsyDomain {

        public static void AddWhimsyDomain() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ChaosDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("ca1a4cd28737ae544a0a7e5415c79d9b");
            var ChaosDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("3016a65f655061b44b0b5624da43983d");
            var HideousLaughterSpell = Resources.GetBlueprint<BlueprintAbility>("fd4d9fd7f87575d47aafe2a64a6e2d8d");

            //WhimsyDomainGreaterResource
            var WhimsyDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("WhimsyDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 8,
                    LevelStep = 4,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });


            var WhimsyDomainGreaterBuff = Helpers.CreateBuff("WhimsyDomainGreaterBuff", bp => {
                bp.SetName("Unexpected Whimsy");
                bp.SetDescription("All enemies within 30 feet of you that can see and hear you must succeed a Will saving throw or they collapse into gales of manic laughter, " +
                    "falling prone. Those who fail their saving throws can take no actions other than laughing for 1 round, but are not considered helpless. You can use this " +
                    "ability once per day at 8th level and one additional time for every 4 levels you possess beyond 8th.");
                bp.m_Icon = HideousLaughterSpell.m_Icon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.CantAct;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Prone;
                });
                bp.FxOnStart = new PrefabLink() { AssetId = "ff26ee73fa464a44ca8bf20e858dc3bc" };
            });

            //WhimsyDomainGreaterAbility
            var WhimsyDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("WhimsyDomainGreaterAbility", bp => {
                bp.SetName("Unexpected Whimsy");
                bp.SetDescription("All enemies within 30 feet of you that can see and hear you must succeed a Will saving throw or they collapse into gales of manic laughter, " +
                    "falling prone. Those who fail their saving throws can take no actions other than laughing for 1 round, but are not considered helpless. You can use this " +
                    "ability once per day at 8th level and one additional time for every 4 levels you possess beyond 8th.");
                bp.m_Icon = HideousLaughterSpell.m_Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = WhimsyDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = WhimsyDomainGreaterBuff.ToReference<BlueprintBuffReference>(),
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
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    ToCaster = false,
                                }
                                )
                        });
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Enemy;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });                              
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = Helpers.CreateString("WhimsyDomainGreaterAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //WhimsyDomainGreaterFeature
            var WhimsyDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("WhimsyDomainGreaterFeature", bp => {
                bp.SetName("Unexpected Whimsy");
                bp.SetDescription("All enemies within 30 feet of you that can see and hear you must succeed a Will saving throw or they collapse into gales of manic laughter, " +
                    "falling prone. Those who fail their saving throws can take no actions other than laughing for 1 round, but are not considered helpless. You can use this " +
                    "ability once per day at 8th level and one additional time for every 4 levels you possess beyond 8th.");
                bp.m_Icon = HideousLaughterSpell.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WhimsyDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WhimsyDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var ProtectionFromLawCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("8b8ccc9763e3cc74bbf5acc9c98557b9");
            var DispelMagicSpell = Resources.GetBlueprint<BlueprintAbility>("92681f181b507b34ea87018e8f7a528a");
            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var AcidicSpraySpell = Resources.GetBlueprint<BlueprintAbility>("c543eef6d725b184ea8669dd09b3894c");
            var CloakOfDreamsSpell = Resources.GetBlueprint<BlueprintAbility>("7f71a70d822af94458dc1a235507e972");
            var WordOfChaosSpell = Resources.GetBlueprint<BlueprintAbility>("69f2e7aff2d1cd148b8075ee476515b1");
            var CloakOfChaosSpell = Resources.GetBlueprint<BlueprintAbility>("9155dbc8268da1c49a7fc4834fa1a4b1");
            var SummonMonsterIXBaseSpell = Resources.GetBlueprint<BlueprintAbility>("52b5df2a97df18242aec67610616ded0");
            var WhimsyDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("WhimsyDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HideousLaughterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromLawCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DispelMagicSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ConfusionSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AcidicSpraySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CloakOfDreamsSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            WordOfChaosSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CloakOfChaosSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonMonsterIXBaseSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var WhimsyDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("WhimsyDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WhimsyDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var WhimsyDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("WhimsyDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ChaosDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = ChaosDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { ChaosDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = WhimsyDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Whimsy Subdomain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} infuses life and weapons with chaos, and you carry a divine whimsy wherever you go.\nTouch of Chaos: " +
                    "You can imbue a target with chaos as a melee touch {g|Encyclopedia:Attack}attack{/g}. For the next {g|Encyclopedia:Combat_Round}round{/g}, anytime the target " +
                    "{g|Encyclopedia:Dice}rolls{/g} a d20, he must roll twice and take the less favorable result. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nUnexpected Whimsy: All enemies within 30 feet of you that can see and hear you must succeed a Will saving throw or they " +
                    "collapse into gales of manic laughter, falling prone. Those who fail their saving throws can take no actions other than laughing for 1 round, but are not considered " +
                    "helpless. You can use this ability once per day at 8th level and one additional time for every 4 levels you possess beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var WhimsyDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("WhimsyDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = WhimsyDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var WhimsyDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("WhimsyDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WhimsyDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WhimsyDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WhimsyDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Whimsy Subdomain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} infuses life and weapons with chaos, and you carry a divine whimsy wherever you go.\nTouch of Chaos: " +
                    "You can imbue a target with chaos as a melee touch {g|Encyclopedia:Attack}attack{/g}. For the next {g|Encyclopedia:Combat_Round}round{/g}, anytime the target " +
                    "{g|Encyclopedia:Dice}rolls{/g} a d20, he must roll twice and take the less favorable result. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nUnexpected Whimsy: All enemies within 30 feet of you that can see and hear you must succeed a Will saving throw or they " +
                    "collapse into gales of manic laughter, falling prone. Those who fail their saving throws can take no actions other than laughing for 1 round, but are not considered " +
                    "helpless. You can use this ability once per day at 8th level and one additional time for every 4 levels you possess beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: " +
                    "hideous laughter, communal protection from law, dispel magic, confusion, acidic spray, cloak of dreams, word of chaos, cloak of chaos, summon monster IX.");
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
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = -2
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };                
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, WhimsyDomainBaseFeature),
                    Helpers.LevelEntry(6, WhimsyDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(WhimsyDomainBaseFeature, WhimsyDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var WhimsyDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("WhimsyDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WhimsyDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WhimsyDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Whimsy Subdomain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} infuses life and weapons with chaos, and you carry a divine whimsy wherever you go.\nTouch of Chaos: " +
                    "You can imbue a target with chaos as a melee touch {g|Encyclopedia:Attack}attack{/g}. For the next {g|Encyclopedia:Combat_Round}round{/g}, anytime the target " +
                    "{g|Encyclopedia:Dice}rolls{/g} a d20, he must roll twice and take the less favorable result. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nUnexpected Whimsy: All enemies within 30 feet of you that can see and hear you must succeed a Will saving throw or they " +
                    "collapse into gales of manic laughter, falling prone. Those who fail their saving throws can take no actions other than laughing for 1 round, but are not considered " +
                    "helpless. You can use this ability once per day at 8th level and one additional time for every 4 levels you possess beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: " +
                    "hideous laughter, communal protection from law, dispel magic, confusion, acidic spray, cloak of dreams, word of chaos, cloak of chaos, summon monster IX.");
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
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, WhimsyDomainBaseFeature),
                    Helpers.LevelEntry(6, WhimsyDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(WhimsyDomainBaseFeature, WhimsyDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            WhimsyDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                WhimsyDomainProgression.ToReference<BlueprintFeatureReference>(),
                WhimsyDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            WhimsyDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WhimsyDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            WhimsyDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WhimsyDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(WhimsyDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Whimsy Subdomain")) { return; }
            DomainTools.RegisterDomain(WhimsyDomainProgression);
            DomainTools.RegisterSecondaryDomain(WhimsyDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(WhimsyDomainProgression);
            DomainTools.RegisterTempleDomain(WhimsyDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(WhimsyDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(WhimsyDomainProgression, WhimsyDomainProgressionSecondary);
        }
    }
}
