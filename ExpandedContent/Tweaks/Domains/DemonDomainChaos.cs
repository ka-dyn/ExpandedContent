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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class DemonDomainChaos {

        public static void AddDemonDomainChaos() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ChaosDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("41b647ee4591dc1448a665a62b7a7b5f");
            var BloodlineAbyssalProgression = Resources.GetBlueprint<BlueprintProgression>("d3a4cb7be97a6694290f0dcfbd147113");
            var StandartRageBuff = Resources.GetBlueprint<BlueprintBuff>("da8ce41ac3cd74742b80984ccc3c9613");

            var DemonDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("DemonDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });

            var DemonDomainBaseBuff = Helpers.CreateBuff("DemonDomainBaseBuff", bp => {
                bp.SetName("Fury of the Abyss");
                bp.SetDescription("As a swift action, you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, " +
                    "and combat maneuver checks. This bonus lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per " +
                    "day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = BloodlineAbyssalProgression.m_Icon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.AC;
                    c.Value = -2;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.AdditionalDamage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.AdditionalCMB;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.AdditionalCMD;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.MaxClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.FxOnStart = StandartRageBuff.FxOnStart;
            });

            var DemonDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("DemonDomainBaseAbility", bp => {
                bp.SetName("Fury of the Abyss");
                bp.SetDescription("As a swift action, you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, " +
                    "and combat maneuver checks. This bonus lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per " +
                    "day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = BloodlineAbyssalProgression.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DemonDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DemonDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var DoomSpell = Resources.GetBlueprint<BlueprintAbility>("fbdd8c455ac4cde4a9a3e18c84af9485");
            var ProtectionFromLawCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("8b8ccc9763e3cc74bbf5acc9c98557b9");
            var RageSpell = Resources.GetBlueprint<BlueprintAbility>("97b991256e43bb140b263c326f690ce2");
            var FreedomOfMovementSpell = Resources.GetBlueprint<BlueprintAbility>("4c349361d720e844e846ad8c19959b1e");
            var AcidicSpraySpell = Resources.GetBlueprint<BlueprintAbility>("c543eef6d725b184ea8669dd09b3894c");
            var SummonMonsterVIBaseSpell = Resources.GetBlueprint<BlueprintAbility>("e740afbab0147944dab35d83faa0ae1c");
            var WordOfChaosSpell = Resources.GetBlueprint<BlueprintAbility>("69f2e7aff2d1cd148b8075ee476515b1");
            var CloakOfChaosSpell = Resources.GetBlueprint<BlueprintAbility>("9155dbc8268da1c49a7fc4834fa1a4b1");
            var SummonMonsterIXBaseSpell = Resources.GetBlueprint<BlueprintAbility>("52b5df2a97df18242aec67610616ded0");
            var DemonDomainChaosSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("DemonDomainChaosSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DoomSpell.ToReference<BlueprintAbilityReference>()
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
                            RageSpell.ToReference<BlueprintAbilityReference>()
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
                            AcidicSpraySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonMonsterVIBaseSpell.ToReference<BlueprintAbilityReference>()
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
            var DemonDomainChaosSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("DemonDomainChaosSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DemonDomainChaosSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var DemonDomainChaosBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("DemonDomainChaosBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DemonDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = DemonDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DemonDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DemonDomainChaosSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Demon Subdomain - Chaos");
                bp.SetDescription("\nYour soul embodies the anarchic and evil nature of demonkind, your master grants you the power of the Abyss.\nFury of the Abyss: As a swift action, " +
                    "you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, and combat maneuver checks. This bonus " +
                    "lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nChaos " +
                    "Blade: At 8th level, you can give a weapon you touch the anarchic special weapon quality for a number of rounds equal to 1/2 your level in the class that gave you " +
                    "access to this domain. You can use this ability once per day at 8th level, and an additional time per day for every four levels beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var DemonDomainChaosAllowed = Helpers.CreateBlueprint<BlueprintFeature>("DemonDomainChaosAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = DemonDomainChaosSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var DemonDomainChaosProgression = Helpers.CreateBlueprint<BlueprintProgression>("DemonDomainChaosProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainChaosAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DemonDomainChaosSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DemonDomainChaosSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Demon Subdomain - Chaos");
                bp.SetDescription("\nYour soul embodies the anarchic and evil nature of demonkind, your master grants you the power of the Abyss.\nFury of the Abyss: As a swift action, " +
                    "you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, and combat maneuver checks. This bonus " +
                    "lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nChaos " +
                    "Blade: At 8th level, you can give a weapon you touch the anarchic special weapon quality for a number of rounds equal to 1/2 your level in the class that gave you " +
                    "access to this domain. You can use this ability once per day at 8th level, and an additional time per day for every four levels beyond 8th.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: doom, communal protection from law, rage, freedom of movement, acidic spray, summon monster VI, word of chaos, " +
                    "cloak of chaos, summon monster IX.");
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
                    Helpers.LevelEntry(1, DemonDomainChaosBaseFeature),
                    Helpers.LevelEntry(8, ChaosDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DemonDomainChaosBaseFeature, ChaosDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var DemonDomainChaosProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("DemonDomainChaosProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainChaosAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainChaosProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Demon Subdomain - Chaos");
                bp.SetDescription("\nYour soul embodies the anarchic and evil nature of demonkind, your master grants you the power of the Abyss.\nFury of the Abyss: As a swift action, " +
                    "you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, and combat maneuver checks. This bonus " +
                    "lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nChaos " +
                    "Blade: At 8th level, you can give a weapon you touch the anarchic special weapon quality for a number of rounds equal to 1/2 your level in the class that gave you " +
                    "access to this domain. You can use this ability once per day at 8th level, and an additional time per day for every four levels beyond 8th.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: doom, communal protection from law, rage, freedom of movement, acidic spray, summon monster VI, word of chaos, " +
                    "cloak of chaos, summon monster IX.");
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
                    Helpers.LevelEntry(1, DemonDomainChaosBaseFeature),
                    Helpers.LevelEntry(8, ChaosDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DemonDomainChaosBaseFeature, ChaosDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            
            DemonDomainChaosAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                DemonDomainChaosProgression.ToReference<BlueprintFeatureReference>(),
                DemonDomainChaosProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            DemonDomainChaosProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainChaosProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            DemonDomainChaosProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainChaosProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Demon Subdomain")) { return; }
            DomainTools.RegisterDomain(DemonDomainChaosProgression);
            DomainTools.RegisterSecondaryDomain(DemonDomainChaosProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(DemonDomainChaosProgression);
            DomainTools.RegisterTempleDomain(DemonDomainChaosProgression);
            DomainTools.RegisterSecondaryTempleDomain(DemonDomainChaosProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(DemonDomainChaosProgression, DemonDomainChaosProgressionSecondary);

        }
    }
}
