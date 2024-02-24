using System.Collections.Generic;
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
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Domains {
    internal class LustDomain {

        public static void AddLustDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var CharmDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("84cd24a110af59140b066bc2c69619bd");
            var CharmDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("e3ec8bc31cab642488396d259a8ab0d9");

            var DominateMonsterBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("c0f4e1c24c9cd334ca988ed1bd9d201f");
            var ProneBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("24cf3deb078d3df4d92ba24b176bda97");
            var StunnedBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("09d39b38bb7c6014394b6daced9bacd3");
            var Castigate = Resources.GetBlueprint<BlueprintAbility>("ce4c4e52c53473549ae033e2bb44b51a");


            
            
            //LustDomainGreaterResource
            var LustDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("LustDomainGreaterResource", bp => {
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
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
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
            //LustDomainGreaterAbility
            var LustDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("LustDomainGreaterAbility", bp => {
                bp.SetName("Anything to Please");
                bp.SetDescription("At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.");
                bp.m_Icon = Castigate.Icon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LustDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionRandomize() {
                                    m_Actions = new ContextActionRandomize.ActionWrapper[3] {
                                        new ContextActionRandomize.ActionWrapper{ 
                                            Weight = 0,
                                            Action = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = DominateMonsterBuff,
                                                    Permanent = false,
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
                                                        }
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                )
                                        },
                                        new ContextActionRandomize.ActionWrapper{
                                            Weight = 0,
                                            Action = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = ProneBuff,
                                                    Permanent = false,
                                                    UseDurationSeconds = false,
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.D4,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 1,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage
                                                        },
                                                        BonusValue = 0
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                )
                                        },
                                        new ContextActionRandomize.ActionWrapper{
                                            Weight = 0,
                                            Action = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = StunnedBuff,
                                                    Permanent = false,
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
                                                        }
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                )
                                        }
                                    }
                                }
                                )
                        });
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //LustDomainGreaterFeature
            var LustDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("LustDomainGreaterFeature", bp => {
                bp.SetName("Anything to Please");
                bp.SetDescription("At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.");
                bp.m_Icon = Castigate.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = LustDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LustDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { LustDomainGreaterAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var HypnotismSpell = Resources.GetBlueprint<BlueprintAbility>("88367310478c10b47903463c5d0152b0");
            var HideousLaughterSpell = Resources.GetBlueprint<BlueprintAbility>("fd4d9fd7f87575d47aafe2a64a6e2d8d");
            var DeepSlumberSpell = Resources.GetBlueprint<BlueprintAbility>("7658b74f626c56a49939d9c20580885e");
            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var DominatePersonSpell = Resources.GetBlueprint<BlueprintAbility>("d7cbd2004ce66a042aeab2e95a3c5c61");
            var EaglesSplendorSpell = Resources.GetBlueprint<BlueprintAbility>("2caa607eadda4ab44934c5c9875e01bc");
            var InsanitySpell = Resources.GetBlueprint<BlueprintAbility>("2b044152b3620c841badb090e01ed9de");
            var EuphoricTranquilitySpell = Resources.GetBlueprint<BlueprintAbility>("740d943e42b60f64a8de74926ba6ddf7");
            var DominateMonsterSpell = Resources.GetBlueprint<BlueprintAbility>("3c17035ec4717674cae2e841a190e757");
            var LustDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("LustDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HypnotismSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HideousLaughterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DeepSlumberSpell.ToReference<BlueprintAbilityReference>()
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
                            DominatePersonSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EaglesSplendorSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            InsanitySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EuphoricTranquilitySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DominateMonsterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var LustDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("LustDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LustDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var LustDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("LustDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CharmDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = CharmDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { CharmDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = LustDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lust Subdomain");
                bp.SetDescription("\nYou can entrance friend and foe alike, able to inspire people or reduce them to a stunned stupor.\nDazing Touch: You can cause a living creature to become dazed " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more {g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the " +
                    "class that gave you access to this domain are unaffected. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAnything " +
                    "to Please: At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var LustDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("LustDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = LustDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var LustDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("LustDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LustDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LustDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lust Subdomain");
                bp.SetDescription("\nYou can entrance friend and foe alike, able to inspire people or reduce them to a stunned stupor.\nDazing Touch: You can cause a living creature to become dazed " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more {g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the " +
                    "class that gave you access to this domain are unaffected. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAnything " +
                    "to Please: At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.\nDomain {g|Encyclopedia:Spell}Spells{/g}: hypnotism, hideous laughter, deep slumber, confusion, " +
                    "dominate person, mass eagle's splendor, insanity, euphoric tranquility, dominate monster.");
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
                    Helpers.LevelEntry(1, LustDomainBaseFeature),
                    Helpers.LevelEntry(8, LustDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LustDomainBaseFeature, LustDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var LustDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("LustDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lust Subdomain");
                bp.SetDescription("\nYou can entrance friend and foe alike, able to inspire people or reduce them to a stunned stupor.\nDazing Touch: You can cause a living creature to become dazed " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more {g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the " +
                    "class that gave you access to this domain are unaffected. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAnything " +
                    "to Please: At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.\nDomain {g|Encyclopedia:Spell}Spells{/g}: hypnotism, hideous laughter, deep slumber, confusion, " +
                    "dominate person, mass eagle's splendor, insanity, euphoric tranquility, dominate monster.");
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
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LustDomainBaseFeature),
                    Helpers.LevelEntry(8, LustDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LustDomainBaseFeature, LustDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var CharmDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("6e62c014399e438aaaf6f3d675fa0fac");
            var CharmDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("19196d84c81b4a979f361dda1ddf1a58");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var LustDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LustDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var LustDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("LustDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    m_Class = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    LevelIncrease = 1,
                    StartingLevel = 3,
                    StartingIncrease = 1,
                    LevelStep = 1,
                    PerStepIncrease = 1,
                };
                bp.m_Min = 1;
            });

            var LustDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("LustDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Anything to Please");
                bp.SetDescription("At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.");
                bp.m_Icon = Castigate.Icon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LustDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionRandomize() {
                                    m_Actions = new ContextActionRandomize.ActionWrapper[3] {
                                        new ContextActionRandomize.ActionWrapper{
                                            Weight = 0,
                                            Action = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = DominateMonsterBuff,
                                                    Permanent = false,
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
                                                        }
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                )
                                        },
                                        new ContextActionRandomize.ActionWrapper{
                                            Weight = 0,
                                            Action = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = ProneBuff,
                                                    Permanent = false,
                                                    UseDurationSeconds = false,
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.D4,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 1,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage
                                                        },
                                                        BonusValue = 0
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                )
                                        },
                                        new ContextActionRandomize.ActionWrapper{
                                            Weight = 0,
                                            Action = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = StunnedBuff,
                                                    Permanent = false,
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
                                                        }
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                )
                                        }
                                    }
                                }
                                )
                        });
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var LustDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LustDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Anything to Please");
                bp.SetDescription("At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.");
                bp.m_Icon = Castigate.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = LustDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LustDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { LustDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var LustDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LustDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CharmDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CharmDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { CharmDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = LustDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lust Subdomain");
                bp.SetDescription("\nYou can entrance friend and foe alike, able to inspire people or reduce them to a stunned stupor.\nDazing Touch: You can cause a living creature to become dazed " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more {g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the " +
                    "class that gave you access to this domain are unaffected. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAnything " +
                    "to Please: At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.");
                bp.IsClassFeature = true;
            });

            var LustDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("LustDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LustDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lust Subdomain");
                bp.SetDescription("\nYou can entrance friend and foe alike, able to inspire people or reduce them to a stunned stupor.\nDazing Touch: You can cause a living creature to become dazed " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more {g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the " +
                    "class that gave you access to this domain are unaffected. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAnything " +
                    "to Please: At 8th level, you can compel a creature within close range to attempt to please you as a standard action. The creature receives a Will save to negate this affect. If the save fails, " +
                    "the creature attacks your enemies for 1 round, stands stunned by you for 1 round, or drops prone and grovels for 1d4 rounds, chosen at random. You can use this ability once per day at 8th level " +
                    "and one additional time per day for every four levels beyond 8th. This is a mind-affecting effect.\nDomain {g|Encyclopedia:Spell}Spells{/g}: hypnotism, hideous laughter, deep slumber, confusion, " +
                    "dominate person, mass eagle's splendor, insanity, euphoric tranquility, dominate monster.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LustDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, LustDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LustDomainBaseFeatureSeparatist, LustDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            LustDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                LustDomainProgression.ToReference<BlueprintFeatureReference>(),
                LustDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            LustDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LustDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            LustDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LustDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            LustDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LustDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            LustDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LustDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            LustDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LustDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(LustDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(LustDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Lust Subdomain")) { return; }
            DomainTools.RegisterDomain(LustDomainProgression);
            DomainTools.RegisterSecondaryDomain(LustDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(LustDomainProgression);
            DomainTools.RegisterTempleDomain(LustDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(LustDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(LustDomainProgression, LustDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(LustDomainProgressionSeparatist);

        }

    }
}
