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
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.ResourceLinks;
using UnityEngine;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Commands.Base;

namespace ExpandedContent.Tweaks.Domains {
    internal class InsanityDomain {

        public static void AddInsanityDomain() {

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
            var MadnessDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("9acc8ab2f313d0e49bb01e030c868e3f");
            var MadnessDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("249e48a1be504850808042c3ed053cda");
            var Confusionbuff = Resources.GetBlueprintReference<BlueprintBuffReference>("886c7407dc629dc499b9f1465ff382df");
            var FrightfulAspectIcon = Resources.GetBlueprint<BlueprintAbility>("e788b02f8d21014488067bdd3ba7b325").Icon;

            var InsanityDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("InsanityDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });


            var InsanityDomainBaseBuff = Helpers.CreateBuff("InsanityDomainBaseBuff", bp => {
                bp.SetName("Insanity Beacon - DC");
                bp.SetDescription("You have a +4 bonus on all saving throws made against mind-affecting effects and immunity to confusion. " +
                    "This bonus lasts for 1 minute. If you fail a saving throw against a mind-affecting effect during this period, you lose " +
                    "your immunity to confusion and are immediately confused for one round.");
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Confusion;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                    c.Bonus = new ContextValue();
                });
                bp.AddComponent<AddInitiatorSavingThrowTriggerExpanded>(c => {
                    c.OnlyPass = false;
                    c.OnlyFail = true;
                    c.SpecificSave = false;
                    c.ChooseSave = SavingThrowType.Fortitude;
                    c.SpecificDescriptor = true;

                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = Confusionbuff,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_Icon = FrightfulAspectIcon;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            
            var InsanityDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("InsanityDomainBaseAbility", bp => {
                bp.SetName("Insane Focus");
                bp.SetDescription("You can touch a willing creature as a standard action, granting it a +4 bonus on all saving throws made against " +
                    "mind-affecting effects and immunity to confusion. This bonus lasts for 1 minute. If the creature fails a saving throw against a " +
                    "mind-affecting effect during this period, it loses its immunity to confusion and is immediately confused for one round. You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = InsanityDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = InsanityDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = FrightfulAspectIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("InsanityDomainBaseAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var CauseFearSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("bd81a3931aa285a4f9844585b5d97e51");
            var CacophonousCallSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e6048d85fc3294f4c92b21c8d7526b1f");
            var RageSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("97b991256e43bb140b263c326f690ce2");
            var MoonstruckAbility = Resources.GetModBlueprint<BlueprintAbility>("MoonstruckAbility");
            var FeeblemindSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("444eed6e26f773a40ab6e4d160c67faa");
            var PhantasmalWebSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("12fb4a4c22549c74d949e2916a2f0b6a");
            var InsanitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2b044152b3620c841badb090e01ed9de");
            var ScintillatingPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4dc60d08c6c4d3c47b413904e4de5ff0");
            var WeirdSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("870af83be6572594d84d276d7fc583e0");
            var InsanityDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("InsanityDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CauseFearSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CacophonousCallSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RageSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MoonstruckAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FeeblemindSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PhantasmalWebSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            InsanitySpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ScintillatingPatternSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            WeirdSpell
                        }
                    },
                };
            });
            var InsanityDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("InsanityDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = InsanityDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var InsanityDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("InsanityDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        InsanityDomainBaseAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = InsanityDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = InsanityDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insanity Subdomain");
                bp.SetDescription("\nYou see it, the uncertain uncertainty; you can help them understand, make them understand. " +
                    "\nInsane Focus: You can touch a willing creature as a standard action, granting it a +4 bonus on all saving throws made against " +
                    "mind-affecting effects and immunity to confusion. This bonus lasts for 1 minute. If the creature fails a saving throw against a " +
                    "mind-affecting effect during this period, it loses its immunity to confusion and is immediately confused for one round. You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nAura of Madness: At 8th level, you can emit a 30-foot aura of madness for a number of rounds per day equal to your level in the " +
                    "class that gave you access to this domain. Enemies within this aura are affected by {g|ConditionConfusion}confusion{/g} unless " +
                    "they make a Will save with a {g|Encyclopedia:DC}DC{/g} equal to 10 + 1/2 your level in the class that gave you access to this " +
                    "domain + your Wisdom modifier. The confusion effect ends immediately when the creature leaves the area or the aura expires. " +
                    "Creatures that succeed on their saving throw are immune to this aura for 24 hours.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var InsanityDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("InsanityDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = InsanityDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // Main Blueprint
            var InsanityDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("InsanityDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = InsanityDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = InsanityDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insanity Subdomain");
                bp.SetDescription("\nYou see it, the uncertain uncertainty; you can help them understand, make them understand. " +
                    "\nInsane Focus: You can touch a willing creature as a standard action, granting it a +4 bonus on all saving throws made against " +
                    "mind-affecting effects and immunity to confusion. This bonus lasts for 1 minute. If the creature fails a saving throw against a " +
                    "mind-affecting effect during this period, it loses its immunity to confusion and is immediately confused for one round. You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nAura of Madness: At 8th level, you can emit a 30-foot aura of madness for a number of rounds per day equal to your level in the " +
                    "class that gave you access to this domain. Enemies within this aura are affected by {g|ConditionConfusion}confusion{/g} unless " +
                    "they make a Will save with a {g|Encyclopedia:DC}DC{/g} equal to 10 + 1/2 your level in the class that gave you access to this " +
                    "domain + your Wisdom modifier. The confusion effect ends immediately when the creature leaves the area or the aura expires. " +
                    "Creatures that succeed on their saving throw are immune to this aura for 24 hours. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsCauseFear}cause fear{/g}, {g|SpellsCacophonousCall}cacophonous call{/g}, {g|SpellsRage}rage{/g}, " +
                    "moonstruck, {g|SpellsFeeblemind}feeblemind{/g}, {g|SpellsPhantasmalWeb}phantasmal web{/g}, {g|SpellsInsanity}insanity{/g}, " +
                    "{g|SpellsFrightfulAspect}frightful aspect{/g}, {g|SpellsWeird}weird{/g}.");
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
                    Helpers.LevelEntry(1, InsanityDomainBaseFeature),
                    Helpers.LevelEntry(8, MadnessDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(InsanityDomainBaseFeature, MadnessDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var InsanityDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("InsanityDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insanity Subdomain");
                bp.SetDescription("\nYou see it, the uncertain uncertainty; you can help them understand, make them understand. " +
                    "\nInsane Focus: You can touch a willing creature as a standard action, granting it a +4 bonus on all saving throws made against " +
                    "mind-affecting effects and immunity to confusion. This bonus lasts for 1 minute. If the creature fails a saving throw against a " +
                    "mind-affecting effect during this period, it loses its immunity to confusion and is immediately confused for one round. You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nAura of Madness: At 8th level, you can emit a 30-foot aura of madness for a number of rounds per day equal to your level in the " +
                    "class that gave you access to this domain. Enemies within this aura are affected by {g|ConditionConfusion}confusion{/g} unless " +
                    "they make a Will save with a {g|Encyclopedia:DC}DC{/g} equal to 10 + 1/2 your level in the class that gave you access to this " +
                    "domain + your Wisdom modifier. The confusion effect ends immediately when the creature leaves the area or the aura expires. " +
                    "Creatures that succeed on their saving throw are immune to this aura for 24 hours. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsCauseFear}cause fear{/g}, {g|SpellsCacophonousCall}cacophonous call{/g}, {g|SpellsRage}rage{/g}, " +
                    "moonstruck, {g|SpellsFeeblemind}feeblemind{/g}, {g|SpellsPhantasmalWeb}phantasmal web{/g}, {g|SpellsInsanity}insanity{/g}, " +
                    "{g|SpellsFrightfulAspect}frightful aspect{/g}, {g|SpellsWeird}weird{/g}.");
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
                    Helpers.LevelEntry(1, InsanityDomainBaseFeature),
                    Helpers.LevelEntry(8, MadnessDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(InsanityDomainBaseFeature, MadnessDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });


            //Separatist versions
            var InsanityDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("InsanityDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var InsanityDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("InsanityDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var InsanityDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("InsanityDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Insane Focus");
                bp.SetDescription("You can touch a willing creature as a standard action, granting it a +4 bonus on all saving throws made against " +
                    "mind-affecting effects and immunity to confusion. This bonus lasts for 1 minute. If the creature fails a saving throw against a " +
                    "mind-affecting effect during this period, it loses its immunity to confusion and is immediately confused for one round. You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = InsanityDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = InsanityDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = FrightfulAspectIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("InsanityDomainBaseAbilitySeparatist.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            

            var InsanityDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("InsanityDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        InsanityDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = InsanityDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = InsanityDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insanity Subdomain");
                bp.SetDescription("\nYou see it, the uncertain uncertainty; you can help them understand, make them understand. " +
                    "\nInsane Focus: You can touch a willing creature as a standard action, granting it a +4 bonus on all saving throws made against " +
                    "mind-affecting effects and immunity to confusion. This bonus lasts for 1 minute. If the creature fails a saving throw against a " +
                    "mind-affecting effect during this period, it loses its immunity to confusion and is immediately confused for one round. You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nAura of Madness: At 8th level, you can emit a 30-foot aura of madness for a number of rounds per day equal to your level in the " +
                    "class that gave you access to this domain. Enemies within this aura are affected by {g|ConditionConfusion}confusion{/g} unless " +
                    "they make a Will save with a {g|Encyclopedia:DC}DC{/g} equal to 10 + 1/2 your level in the class that gave you access to this " +
                    "domain + your Wisdom modifier. The confusion effect ends immediately when the creature leaves the area or the aura expires. " +
                    "Creatures that succeed on their saving throw are immune to this aura for 24 hours.");
                bp.IsClassFeature = true;
            });

            var InsanityDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("InsanityDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsanityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insanity Subdomain");
                bp.SetDescription("\nYou see it, the uncertain uncertainty; you can help them understand, make them understand. " +
                    "\nInsane Focus: You can touch a willing creature as a standard action, granting it a +4 bonus on all saving throws made against " +
                    "mind-affecting effects and immunity to confusion. This bonus lasts for 1 minute. If the creature fails a saving throw against a " +
                    "mind-affecting effect during this period, it loses its immunity to confusion and is immediately confused for one round. You can " +
                    "use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nAura of Madness: At 8th level, you can emit a 30-foot aura of madness for a number of rounds per day equal to your level in the " +
                    "class that gave you access to this domain. Enemies within this aura are affected by {g|ConditionConfusion}confusion{/g} unless " +
                    "they make a Will save with a {g|Encyclopedia:DC}DC{/g} equal to 10 + 1/2 your level in the class that gave you access to this " +
                    "domain + your Wisdom modifier. The confusion effect ends immediately when the creature leaves the area or the aura expires. " +
                    "Creatures that succeed on their saving throw are immune to this aura for 24 hours. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsCauseFear}cause fear{/g}, {g|SpellsCacophonousCall}cacophonous call{/g}, {g|SpellsRage}rage{/g}, " +
                    "moonstruck, {g|SpellsFeeblemind}feeblemind{/g}, {g|SpellsPhantasmalWeb}phantasmal web{/g}, {g|SpellsInsanity}insanity{/g}, " +
                    "{g|SpellsFrightfulAspect}frightful aspect{/g}, {g|SpellsWeird}weird{/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, InsanityDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, MadnessDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(InsanityDomainBaseFeatureSeparatist, MadnessDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            InsanityDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                InsanityDomainProgression.ToReference<BlueprintFeatureReference>(),
                InsanityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            InsanityDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsanityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            InsanityDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsanityDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            InsanityDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsanityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            InsanityDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsanityDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            InsanityDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsanityDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(InsanityDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(InsanityDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Insanity Subdomain")) { return; }
            DomainTools.RegisterDomain(InsanityDomainProgression);
            DomainTools.RegisterSecondaryDomain(InsanityDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(InsanityDomainProgression);
            DomainTools.RegisterTempleDomain(InsanityDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(InsanityDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(InsanityDomainProgression, InsanityDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(InsanityDomainProgressionSeparatist);

        }
    }
}
