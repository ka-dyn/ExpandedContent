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
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Alignments;

namespace ExpandedContent.Tweaks.Domains {
    internal class LoyaltyDomain {

        public static void AddLoyaltyDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var LawDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("3dc5e2b315ff07f438582a2468beb1fb");
            var RemoveFear = Resources.GetBlueprint<BlueprintAbility>("55a037e514c0ee14a8e3ed14b47061de");

            var LoyaltyDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("LoyaltyDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });

            var LoyaltyDomainBaseBuff = Helpers.CreateBuff("LoyaltyDomainBaseBuff", bp => {
                bp.SetName("Touch of Loyalty");
                bp.SetDescription("You have been granted a +4 sacred bonus on saving throws to resist charm, compulsion, and fear effects.");
                bp.m_Icon = RemoveFear.m_Icon;
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.Value = 4;
                    c.ModifierDescriptor = ModifierDescriptor.Sacred;
                    c.SpellDescriptor = SpellDescriptor.Fear | SpellDescriptor.Charm | SpellDescriptor.Compulsion;
                });
                bp.IsClassFeature = true;
            });

            var LoyaltyDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("LoyaltyDomainBaseAbility", bp => {
                bp.SetName("Touch of Loyalty");
                bp.SetDescription("As a standard action, you can touch a willing creature, granting it a +4 sacred bonus on saving throws to resist charm, compulsion, and fear " +
                    "effects for 1 hour. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = RemoveFear.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LoyaltyDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "c4d861e816edd6f4eab73c55a18fdadd" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LoyaltyDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("LoyaltyDomainBaseAbility.Duration", "1 hour");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var RemoveFearSpell = Resources.GetBlueprint<BlueprintAbility>("55a037e514c0ee14a8e3ed14b47061de");
            var ProtectionFromChaosCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("0ec75ec95d9e39d47a23610123ba1bad");
            var PrayerSpell = Resources.GetBlueprint<BlueprintAbility>("faabd2cc67efa4646ac58c7bb3e40fcc");
            var ProtectionFromEnergyCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("76a629d019275b94184a1a8733cac45e");
            var CommandGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("cb15cc8d7a5480648855a23b3ba3f93d");
            var BladeBarrierSpell = Resources.GetBlueprint<BlueprintAbility>("36c8971e91f1745418cc3ffdfac17b74");
            var DictumSpell = Resources.GetBlueprint<BlueprintAbility>("302ab5e241931a94881d323a7844ae8f");
            var ShieldOfLawSpell = Resources.GetBlueprint<BlueprintAbility>("73e7728808865094b8892613ddfaf7f5");
            var DominateMonsterSpell = Resources.GetBlueprint<BlueprintAbility>("3c17035ec4717674cae2e841a190e757");
            var LoyaltyDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("LoyaltyDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RemoveFearSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromChaosCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PrayerSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromEnergyCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CommandGreaterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BladeBarrierSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DictumSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShieldOfLawSpell.ToReference<BlueprintAbilityReference>()
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
            var LoyaltyDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("LoyaltyDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LoyaltyDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });            
            var LoyaltyDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("LoyaltyDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LoyaltyDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = LoyaltyDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { LoyaltyDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = LoyaltyDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Loyalty Subdomain");
                bp.SetDescription("\nYou value loyalty above other virtues, both between allies and towards ones holy patron. \nTouch of Loyalty: As a standard action, you can " +
                    "touch a willing creature, granting it a +4 sacred bonus on saving throws to resist charm, compulsion, and fear effects for 1 hour. You can use this ability " +
                    "a number of times per day equal to 3 + your Wisdom modifier.\nStaff of Order: At 8th level, you can give a weapon you touch the axiomatic special weapon " +
                    "quality for a number of rounds equal to 1/2 your level in the class that gave you access to this domain. You can use this ability once per day at 8th level, " +
                    "and an additional time per day for every four levels beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var LoyaltyDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("LoyaltyDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = LoyaltyDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var LoyaltyDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoyaltyDomainProgression", bp => {
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.Alignment = AlignmentMaskType.Lawful;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LoyaltyDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LoyaltyDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Loyalty Subdomain");
                bp.SetDescription("\nYou value loyalty above other virtues, both between allies and towards ones holy patron. \nTouch of Loyalty: As a standard action, you can " +
                    "touch a willing creature, granting it a +4 sacred bonus on saving throws to resist charm, compulsion, and fear effects for 1 hour. You can use this ability " +
                    "a number of times per day equal to 3 + your Wisdom modifier.\nStaff of Order: At 8th level, you can give a weapon you touch the axiomatic special weapon " +
                    "quality for a number of rounds equal to 1/2 your level in the class that gave you access to this domain. You can use this ability once per day at 8th level, " +
                    "and an additional time per day for every four levels beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: remove fear, communal protection from chaos, " +
                    "prayer, communal protection from energy, greater command, blade barrier, dictum, shield of law, dominate monster.");
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
                    }
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
                    Helpers.LevelEntry(1, LoyaltyDomainBaseFeature),
                    Helpers.LevelEntry(8, LawDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LoyaltyDomainBaseFeature, LawDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var LoyaltyDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("LoyaltyDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.Alignment = AlignmentMaskType.Lawful;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Loyalty Subdomain");
                bp.SetDescription("\nYou value loyalty above other virtues, both between allies and towards ones holy patron. \nTouch of Loyalty: As a standard action, you can " +
                    "touch a willing creature, granting it a +4 sacred bonus on saving throws to resist charm, compulsion, and fear effects for 1 hour. You can use this ability " +
                    "a number of times per day equal to 3 + your Wisdom modifier.\nStaff of Order: At 8th level, you can give a weapon you touch the axiomatic special weapon " +
                    "quality for a number of rounds equal to 1/2 your level in the class that gave you access to this domain. You can use this ability once per day at 8th level, " +
                    "and an additional time per day for every four levels beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: remove fear, communal protection from chaos, " +
                    "prayer, communal protection from energy, greater command, blade barrier, dictum, shield of law, dominate monster.");
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
                    }
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
                    Helpers.LevelEntry(1, LoyaltyDomainBaseFeature),
                    Helpers.LevelEntry(8, LawDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LoyaltyDomainBaseFeature, LawDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var LawDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("e1ac3d1b6f8a4a18b18f61f49a40722e");


            var LoyaltyDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LoyaltyDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });


            var LoyaltyDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("LoyaltyDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });

            var LoyaltyDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("LoyaltyDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Touch of Loyalty");
                bp.SetDescription("As a standard action, you can touch a willing creature, granting it a +4 sacred bonus on saving throws to resist charm, compulsion, and fear " +
                    "effects for 1 hour. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = RemoveFear.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LoyaltyDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "c4d861e816edd6f4eab73c55a18fdadd" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LoyaltyDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("LoyaltyDomainBaseAbilitySeparatist.Duration", "1 hour");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var LoyaltyDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LoyaltyDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LoyaltyDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = LoyaltyDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { LoyaltyDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = LoyaltyDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Loyalty Subdomain");
                bp.SetDescription("\nYou value loyalty above other virtues, both between allies and towards ones holy patron. \nTouch of Loyalty: As a standard action, you can " +
                    "touch a willing creature, granting it a +4 sacred bonus on saving throws to resist charm, compulsion, and fear effects for 1 hour. You can use this ability " +
                    "a number of times per day equal to 3 + your Wisdom modifier.\nStaff of Order: At 8th level, you can give a weapon you touch the axiomatic special weapon " +
                    "quality for a number of rounds equal to 1/2 your level in the class that gave you access to this domain. You can use this ability once per day at 8th level, " +
                    "and an additional time per day for every four levels beyond 8th.");
                bp.IsClassFeature = true;
            });

            var LoyaltyDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("LoyaltyDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.Alignment = AlignmentMaskType.Lawful;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LoyaltyDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Loyalty Subdomain");
                bp.SetDescription("\nYou value loyalty above other virtues, both between allies and towards ones holy patron. \nTouch of Loyalty: As a standard action, you can " +
                    "touch a willing creature, granting it a +4 sacred bonus on saving throws to resist charm, compulsion, and fear effects for 1 hour. You can use this ability " +
                    "a number of times per day equal to 3 + your Wisdom modifier.\nStaff of Order: At 8th level, you can give a weapon you touch the axiomatic special weapon " +
                    "quality for a number of rounds equal to 1/2 your level in the class that gave you access to this domain. You can use this ability once per day at 8th level, " +
                    "and an additional time per day for every four levels beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: remove fear, communal protection from chaos, " +
                    "prayer, communal protection from energy, greater command, blade barrier, dictum, shield of law, dominate monster.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LoyaltyDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, LawDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LoyaltyDomainBaseFeatureSeparatist, LawDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });


            LoyaltyDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                LoyaltyDomainProgression.ToReference<BlueprintFeatureReference>(),
                LoyaltyDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            LoyaltyDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LoyaltyDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            LoyaltyDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LoyaltyDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            LoyaltyDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LoyaltyDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            LoyaltyDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LoyaltyDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            LoyaltyDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LoyaltyDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(LoyaltyDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(LoyaltyDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Loyalty Subdomain")) { return; }
            DomainTools.RegisterDomain(LoyaltyDomainProgression);
            DomainTools.RegisterSecondaryDomain(LoyaltyDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(LoyaltyDomainProgression);
            DomainTools.RegisterTempleDomain(LoyaltyDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(LoyaltyDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(LoyaltyDomainProgression, LoyaltyDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(LoyaltyDomainProgressionSeparatist);

        }
    }
}
