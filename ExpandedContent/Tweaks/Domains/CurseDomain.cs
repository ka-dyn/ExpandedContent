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
using ExpandedContent.Tweaks.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;

namespace ExpandedContent.Tweaks.Domains {
    internal class CurseDomain {

        public static void AddCurseDomain() {

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
            var LuckDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("dd58b458af054e642bf845c3f01307e5");
            var WitchHexEvilEyeSavesAbility = Resources.GetBlueprint<BlueprintAbility>("ba52aed3017521a4abafcbae4ee06d10");

            var CurseDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("CurseDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var CurseDomainBaseBuff = Helpers.CreateBuff("CurseDomainBaseBuff", bp => {
                bp.SetName("Malign Eye");
                bp.SetDescription("As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = WitchHexEvilEyeSavesAbility.m_Icon;
                bp.AddComponent<SavingThrowBonusAgainstCaster>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Reflex = true;
                    c.Fortitude = true;
                    c.Will = true;
                    c.Value = -2;
                });                
            });

            var CurseDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("CurseDomainBaseAbility", bp => {
                bp.SetName("Malign Eye");
                bp.SetDescription("As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = WitchHexEvilEyeSavesAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CurseDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "cbfe312cb8e63e240a859efaad8e467c" };
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
                    c.m_RequiredResource = CurseDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var BaneSpell = Resources.GetBlueprint<BlueprintAbility>("8bc64d869456b004b9db255cdd1ea734");
            var AidSpell = Resources.GetBlueprint<BlueprintAbility>("03a9630394d10164a9410882d31572f0");
            var BestowCurseSpell = Resources.GetBlueprint<BlueprintAbility>("989ab5c44240907489aba0a8568d0603");
            var ProtectionFromEnergyCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("76a629d019275b94184a1a8733cac45e");
            var BreakEnchantmentSpell = Resources.GetBlueprint<BlueprintAbility>("7792da00c85b9e042a0fdfc2b66ec9a8");
            var EyebiteSpell = Resources.GetBlueprint<BlueprintAbility>("3167d30dd3c622c46b0c0cb242061642");
            var RestorationGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("fafd77c6bfa85c04ba31fdc1c962c914");
            var MomentOfPrescienceAbility = Resources.GetModBlueprint<BlueprintAbility>("MomentOfPrescienceAbility");
            var HealMassSpell = Resources.GetBlueprint<BlueprintAbility>("867524328b54f25488d371214eea0d90");
            var CurseDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("CurseDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BaneSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AidSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BestowCurseSpell.ToReference<BlueprintAbilityReference>()
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
                            BreakEnchantmentSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EyebiteSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RestorationGreaterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MomentOfPrescienceAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HealMassSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var CurseDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("CurseDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = CurseDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });            
            var CurseDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("CurseDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CurseDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = CurseDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { CurseDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = CurseDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Curse Subdomain");
                bp.SetDescription("\nYou are infused with fate, spreading ill omens to foes while granting fortune to yourself.\nMalign Eye: " +
                    "As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nDivine Fortune: At 6th level, as a standard " +
                    "{g|Encyclopedia:CA_Types}action{/g}, you can bless yourself with divine luck. For the next half your level in the class that gave you access to this domain rounds you " +
                    "roll two times on every d20 roll and take the best result. You can use this ability once per day at 6th level, and one additional time per day for every 6 levels in the " +
                    "class that gave you access to this domain beyond 6th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var CurseDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("CurseDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = CurseDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var CurseDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("CurseDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = CurseDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = CurseDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Curse Subdomain");
                bp.SetDescription("\nYou are infused with fate, spreading ill omens to foes while granting fortune to yourself.\nMalign Eye: " +
                    "As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nDivine Fortune: At 6th level, as a standard " +
                    "{g|Encyclopedia:CA_Types}action{/g}, you can bless yourself with divine luck. For the next half your level in the class that gave you access to this domain rounds you " +
                    "roll two times on every d20 roll and take the best result. You can use this ability once per day at 6th level, and one additional time per day for every 6 levels in the " +
                    "class that gave you access to this domain beyond 6th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: bane, aid, bestow curse, communal protection from energy, break enchantment, " +
                    "eyebite, restoration greater, moment of prescience, mass heal.");
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
                    Helpers.LevelEntry(1, CurseDomainBaseFeature),
                    Helpers.LevelEntry(6, LuckDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CurseDomainBaseFeature, LuckDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var CurseDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("CurseDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any; //to work with divine scourge
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Curse Subdomain");
                bp.SetDescription("\nYou are infused with fate, spreading ill omens to foes while granting fortune to yourself.\nMalign Eye: " +
                    "As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nDivine Fortune: At 6th level, as a standard " +
                    "{g|Encyclopedia:CA_Types}action{/g}, you can bless yourself with divine luck. For the next half your level in the class that gave you access to this domain rounds you " +
                    "roll two times on every d20 roll and take the best result. You can use this ability once per day at 6th level, and one additional time per day for every 6 levels in the " +
                    "class that gave you access to this domain beyond 6th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: bane, aid, bestow curse, communal protection from energy, break enchantment, " +
                    "eyebite, restoration greater, moment of prescience, mass heal.");
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
                    Helpers.LevelEntry(1, CurseDomainBaseFeature),
                    Helpers.LevelEntry(6, LuckDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CurseDomainBaseFeature, LuckDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var LuckDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("58db10a0b5a143e2a4867793ca869225");

            var CurseDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("CurseDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var CurseDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("CurseDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });


            var CurseDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("CurseDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Malign Eye");
                bp.SetDescription("As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = WitchHexEvilEyeSavesAbility.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CurseDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "cbfe312cb8e63e240a859efaad8e467c" };
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
                    c.m_RequiredResource = CurseDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var CurseDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("CurseDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CurseDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CurseDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { CurseDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = CurseDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Curse Subdomain");
                bp.SetDescription("\nYou are infused with fate, spreading ill omens to foes while granting fortune to yourself.\nMalign Eye: " +
                    "As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nDivine Fortune: At 6th level, as a standard " +
                    "{g|Encyclopedia:CA_Types}action{/g}, you can bless yourself with divine luck. For the next half your level in the class that gave you access to this domain rounds you " +
                    "roll two times on every d20 roll and take the best result. You can use this ability once per day at 6th level, and one additional time per day for every 6 levels in the " +
                    "class that gave you access to this domain beyond 6th.");
                bp.IsClassFeature = true;
            });

            var CurseDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("CurseDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Curse Subdomain");
                bp.SetDescription("\nYou are infused with fate, spreading ill omens to foes while granting fortune to yourself.\nMalign Eye: " +
                    "As a standard action, you can afflict one target within 30 feet with your malign eye, causing it to take a –2 penalty on all saving throws against your spells. " +
                    "The effect lasts for 1 minute. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nDivine Fortune: At 6th level, as a standard " +
                    "{g|Encyclopedia:CA_Types}action{/g}, you can bless yourself with divine luck. For the next half your level in the class that gave you access to this domain rounds you " +
                    "roll two times on every d20 roll and take the best result. You can use this ability once per day at 6th level, and one additional time per day for every 6 levels in the " +
                    "class that gave you access to this domain beyond 6th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: bane, aid, bestow curse, communal protection from energy, break enchantment, " +
                    "eyebite, restoration greater, moment of prescience, mass heal.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {  };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, CurseDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, LuckDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CurseDomainBaseFeatureSeparatist, LuckDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            CurseDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                CurseDomainProgression.ToReference<BlueprintFeatureReference>(),
                CurseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            CurseDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CurseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            CurseDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CurseDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            CurseDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CurseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            CurseDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CurseDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            CurseDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CurseDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(CurseDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(CurseDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Curse Subdomain")) { return; }
            DomainTools.RegisterDomain(CurseDomainProgression);
            DomainTools.RegisterSecondaryDomain(CurseDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(CurseDomainProgression);
            DomainTools.RegisterTempleDomain(CurseDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(CurseDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(CurseDomainProgression, CurseDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(CurseDomainProgressionSeparatist);

        }
    }
}
