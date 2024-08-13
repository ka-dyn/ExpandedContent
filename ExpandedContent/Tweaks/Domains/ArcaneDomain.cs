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

namespace ExpandedContent.Tweaks.Domains {
    internal class ArcaneDomain {

        public static void AddArcaneDomain() {

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
            var MagicDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("cf47e96abd88c9f418f8e67f5a14381f");
            var MagicDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("724216a6124d486fa55d7476db26bf1a");
            var AirBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");
            var Kinetic_AirBlastLine00 = Resources.GetBlueprint<BlueprintProjectile>("03689858955c6bf409be06f35f09946a");


            var ArcaneDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ArcaneDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });


            var ArcaneDomainBaseDCSelfBuff = Helpers.CreateBuff("ArcaneDomainBaseDCSelfBuff", bp => {
                bp.SetName("Arcane Beacon - DC");
                bp.SetDescription("An aura emanates 15 feet from you. All arcane spells cast within the aura increase their saving throw DC by +1.");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = ArcaneDomainBaseDCArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ArcaneDomainBaseCLSelfBuff = Helpers.CreateBuff("ArcaneDomainBaseCLSelfBuff", bp => {
                bp.SetName("Arcane Beacon - Caster Level");
                bp.SetDescription("An aura emanates 15 feet from you. All arcane spells cast within the aura gain a +1 bonus to their caster level.");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = ArcaneDomainBaseCLArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });


            var ArcaneDomainBaseDCAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArcaneDomainBaseDCAbility", bp => {
                bp.SetName("Arcane Beacon - DC");
                bp.SetDescription("As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. " +
                    "The aura emanates 15 feet from you. All arcane spells cast within the aura increase their saving throw DC by +1. " +
                    "You can use arcane beacon a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcaneDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArcaneDomainBaseDCSelfBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 2,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = TorrentAirBlastAbility.m_Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArcaneDomainBaseCLAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArcaneDomainBaseCLAbility", bp => {
                bp.SetName("Arcane Beacon - Caster Level");
                bp.SetDescription("As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. " +
                    "The aura emanates 15 feet from you. All arcane spells cast within the aura gain a +1 bonus to their caster level. " +
                    "You can use arcane beacon a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcaneDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArcaneDomainBaseCLSelfBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 2,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = TorrentAirBlastAbility.m_Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            //Spelllist
            var MagicMissileSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4ac47ddb9fa1eaf43a1b6809980cfbd2");
            var ResistEnergySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("21ffef7791ce73f468b6fca4d9371e8b");
            var DispelMagicSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("92681f181b507b34ea87018e8f7a528a");
            var ArcaneConcordanceAbility = Resources.GetModBlueprint<BlueprintAbility>("ArcaneConcordanceAbility");
            var SpellResistanceSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("0a5ddfbcfb3989543ac7c936fc256889");
            var GreaterDispelMagicSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0f761b808dc4b149b08eaf44b99f633");
            var PowerWordBlindSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("261e1788bfc5ac1419eec68b1d485dbc");
            var ProtectionFromSpellsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("42aa71adc7343714fa92e471baa98d42");
            var ClashingRocksSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("01300baad090d634cb1a1b2defe068d6");
            var ArcaneDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("ArcaneDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicMissileSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ResistEnergySpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DispelMagicSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ArcaneConcordanceAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SpellResistanceSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            GreaterDispelMagicSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PowerWordBlindSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromSpellsSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ClashingRocksSpell//Swapped for Mages Disjunction if TTT installed
                        }
                    },
                };
            });     
            var ArcaneDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArcaneDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ArcaneDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var ArcaneDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArcaneDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArcaneDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = ArcaneDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { ArcaneDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ArcaneDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Arcane Subdomain");
                bp.SetDescription("\nYou are a true scholar of the mystic arts, harnessing the fusion of arcane and divine. " +
                    "\nArcane Beacon: As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. The aura emanates 15 feet from you. " +
                    "All arcane spells cast within the aura either gain a +1 bonus to their caster level or increase their saving throw DC by +1. " +
                    "The caster chooses the benefit when she casts the spell. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var ArcaneDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("ArcaneDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = ArcaneDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var ArcaneDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("ArcaneDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ArcaneDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ArcaneDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Arcane Subdomain");
                bp.SetDescription("\nYou are a true scholar of the mystic arts, harnessing the fusion of arcane and divine. " +
                    "\nArcane Beacon: As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. The aura emanates 15 feet from you. " +
                    "All arcane spells cast within the aura either gain a +1 bonus to their caster level or increase their saving throw DC by +1. " +
                    "The caster chooses the benefit when she casts the spell. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic missile, {g|SpellsResistEnergy}resist energy{/g}, {g|SpellsDispelMagic}dispel magic{/g}, arcane concordance, " +
                    "{g|SpellsSpellResistance}spell resistance{/g}, {g|SpellsGreaterDispelMagic}dispel magic, greater{/g}, {g|SpellsPowerWordBlind}power word blind{/g}, " +
                    "{g|SpellsProtectionFromSpells}protection from spells{/g}, {g|SpellsClashingRocks}clashing rocks{/g}.");
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
                    Helpers.LevelEntry(1, ArcaneDomainBaseFeature),
                    Helpers.LevelEntry(8, MagicDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ArcaneDomainBaseFeature, MagicDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var ArcaneDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("ArcaneDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Arcane Subdomain");
                bp.SetDescription("\nYou are a true scholar of the mystic arts, harnessing the fusion of arcane and divine. " +
                    "\nArcane Beacon: As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. The aura emanates 15 feet from you. " +
                    "All arcane spells cast within the aura either gain a +1 bonus to their caster level or increase their saving throw DC by +1. " +
                    "The caster chooses the benefit when she casts the spell. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic missile, {g|SpellsResistEnergy}resist energy{/g}, {g|SpellsDispelMagic}dispel magic{/g}, arcane concordance, " +
                    "{g|SpellsSpellResistance}spell resistance{/g}, {g|SpellsGreaterDispelMagic}dispel magic, greater{/g}, {g|SpellsPowerWordBlind}power word blind{/g}, " +
                    "{g|SpellsProtectionFromSpells}protection from spells{/g}, {g|SpellsClashingRocks}clashing rocks{/g}.");
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
                    Helpers.LevelEntry(1, ArcaneDomainBaseFeature),
                    Helpers.LevelEntry(8, MagicDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ArcaneDomainBaseFeature, MagicDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            

            //Separatist versions
            var ArcaneDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ArcaneDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var ArcaneDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("ArcaneDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });




            var ArcaneDomainBaseDCAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("ArcaneDomainBaseDCAbilitySeparatist", bp => {
                bp.SetName("Arcane Beacon - DC");
                bp.SetDescription("As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. " +
                    "The aura emanates 15 feet from you. All arcane spells cast within the aura increase their saving throw DC by +1. " +
                    "You can use arcane beacon a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcaneDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArcaneDomainBaseDCSelfBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 2,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = TorrentAirBlastAbility.m_Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArcaneDomainBaseCLAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("ArcaneDomainBaseCLAbilitySeparatist", bp => {
                bp.SetName("Arcane Beacon - Caster Level");
                bp.SetDescription("As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. " +
                    "The aura emanates 15 feet from you. All arcane spells cast within the aura gain a +1 bonus to their caster level. " +
                    "You can use arcane beacon a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcaneDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArcaneDomainBaseCLSelfBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 2,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = TorrentAirBlastAbility.m_Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            var ArcaneDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ArcaneDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArcaneDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ArcaneDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { ArcaneDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ArcaneDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Arcane Subdomain");
                bp.SetDescription("\nYou are a true scholar of the mystic arts, harnessing the fusion of arcane and divine. " +
                    "\nArcane Beacon: As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. The aura emanates 15 feet from you. " +
                    "All arcane spells cast within the aura either gain a +1 bonus to their caster level or increase their saving throw DC by +1. " +
                    "The caster chooses the benefit when she casts the spell. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th.");
                bp.IsClassFeature = true;
            });

            var ArcaneDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("ArcaneDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArcaneDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Arcane Subdomain");
                bp.SetDescription("\nYou are a true scholar of the mystic arts, harnessing the fusion of arcane and divine. " +
                    "\nArcane Beacon: As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. The aura emanates 15 feet from you. " +
                    "All arcane spells cast within the aura either gain a +1 bonus to their caster level or increase their saving throw DC by +1. " +
                    "The caster chooses the benefit when she casts the spell. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic missile, {g|SpellsResistEnergy}resist energy{/g}, {g|SpellsDispelMagic}dispel magic{/g}, arcane concordance, " +
                    "{g|SpellsSpellResistance}spell resistance{/g}, {g|SpellsGreaterDispelMagic}dispel magic, greater{/g}, {g|SpellsPowerWordBlind}power word blind{/g}, " +
                    "{g|SpellsProtectionFromSpells}protection from spells{/g}, {g|SpellsClashingRocks}clashing rocks{/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ArcaneDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, MagicDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ArcaneDomainBaseFeatureSeparatist, MagicDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            ArcaneDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ArcaneDomainProgression.ToReference<BlueprintFeatureReference>(),
                ArcaneDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            ArcaneDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ArcaneDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ArcaneDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ArcaneDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ArcaneDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ArcaneDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ArcaneDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ArcaneDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ArcaneDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ArcaneDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(ArcaneDomainBaseDCAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(ArcaneDomainBaseCLAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(ArcaneDomainBaseDCAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(ArcaneDomainBaseCLAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Arcane Subdomain")) { return; }
            DomainTools.RegisterDomain(ArcaneDomainProgression);
            DomainTools.RegisterSecondaryDomain(ArcaneDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(ArcaneDomainProgression);
            DomainTools.RegisterTempleDomain(ArcaneDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(ArcaneDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(ArcaneDomainProgression, ArcaneDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(ArcaneDomainProgressionSeparatist);

        }
    }
}
