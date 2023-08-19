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
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class DemonDomainEvil {

        public static void AddDemonDomainEvil() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var EvilDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("3784df3083cb6404fbce7a585be24bcf");
            var BloodlineAbyssalProgression = Resources.GetBlueprint<BlueprintProgression>("d3a4cb7be97a6694290f0dcfbd147113");

            var DemonDomainBaseResource = Resources.GetModBlueprint<BlueprintAbilityResource>("DemonDomainBaseResource");            
            var DemonDomainBaseAbility = Resources.GetModBlueprint<BlueprintAbility>("DemonDomainBaseAbility");

            //Spelllist
            var DoomSpell = Resources.GetBlueprint<BlueprintAbility>("fbdd8c455ac4cde4a9a3e18c84af9485");
            var BoneshakerSpell = Resources.GetBlueprint<BlueprintAbility>("b7731c2b4fa1c9844a092329177be4c3");
            var RageSpell = Resources.GetBlueprint<BlueprintAbility>("97b991256e43bb140b263c326f690ce2");
            var UnholyBlightSpell = Resources.GetBlueprint<BlueprintAbility>("a02cf51787df937489ef5d4cf5970335");
            var SlayLivingSpell = Resources.GetBlueprint<BlueprintAbility>("4fbd47525382517419c66fb548fe9a67");
            var SummonMonsterVIBaseSpell = Resources.GetBlueprint<BlueprintAbility>("e740afbab0147944dab35d83faa0ae1c");
            var BlasphemySpell = Resources.GetBlueprint<BlueprintAbility>("bd10c534a09f44f4ea632c8b8ae97145");
            var UnholyAuraSpell = Resources.GetBlueprint<BlueprintAbility>("47f9cb1c367a5e4489cfa32fce290f86");
            var SummonMonsterIXBaseSpell = Resources.GetBlueprint<BlueprintAbility>("52b5df2a97df18242aec67610616ded0");
            var DemonDomainEvilSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("DemonDomainEvilSpellList", bp => {
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
                            BoneshakerSpell.ToReference<BlueprintAbilityReference>()
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
                            UnholyBlightSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SlayLivingSpell.ToReference<BlueprintAbilityReference>()
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
                            BlasphemySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            UnholyAuraSpell.ToReference<BlueprintAbilityReference>()
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
            var DemonDomainEvilSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("DemonDomainEvilSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DemonDomainEvilSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var DemonDomainEvilBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("DemonDomainEvilBaseFeature", bp => {
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
                    c.m_Feature = DemonDomainEvilSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Demon Subdomain - Evil");
                bp.SetDescription("\nYour soul embodies the anarchic and evil nature of demonkind, your master grants you the power of the Abyss.\nFury of the Abyss: As a swift action, " +
                    "you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, and combat maneuver checks. This bonus " +
                    "lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nScythe " +
                    "of Evil: At 8th level, you can give a weapon touched the unholy special weapon quality for a number of rounds equal to 1/2 your level in the class that gave you access" +
                    "to this domain. You can use this ability once per day at 8th level, and an additional time per day for every four levels beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var DemonDomainEvilAllowed = Helpers.CreateBlueprint<BlueprintFeature>("DemonDomainEvilAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = DemonDomainEvilSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var DemonDomainEvilProgression = Helpers.CreateBlueprint<BlueprintProgression>("DemonDomainEvilProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainEvilAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DemonDomainEvilSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DemonDomainEvilSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Demon Subdomain - Evil");
                bp.SetDescription("\nYour soul embodies the anarchic and evil nature of demonkind, your master grants you the power of the Abyss.\nFury of the Abyss: As a swift action, " +
                    "you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, and combat maneuver checks. This bonus " +
                    "lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nScythe " +
                    "of Evil: At 8th level, you can give a weapon touched the unholy special weapon quality for a number of rounds equal to 1/2 your level in the class that gave you access" +
                    "to this domain. You can use this ability once per day at 8th level, and an additional time per day for every four levels beyond 8th.\nDomain " +
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
                    Helpers.LevelEntry(1, DemonDomainEvilBaseFeature),
                    Helpers.LevelEntry(8, EvilDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DemonDomainEvilBaseFeature, EvilDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var DemonDomainEvilProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("DemonDomainEvilProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainEvilAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainEvilProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Demon Subdomain - Evil");
                bp.SetDescription("\nYour soul embodies the anarchic and evil nature of demonkind, your master grants you the power of the Abyss.\nFury of the Abyss: As a swift action, " +
                    "you can give yourself an enhancement bonus equal to 1/2 your cleric level (minimum +1) on melee attacks, melee damage rolls, and combat maneuver checks. This bonus " +
                    "lasts for 1 round. During this round, you take a –2 penalty to AC. You can use this ability for a number of times per day equal to 3 + your Wisdom modifier.\nScythe " +
                    "of Evil: At 8th level, you can give a weapon touched the unholy special weapon quality for a number of rounds equal to 1/2 your level in the class that gave you access" +
                    "to this domain. You can use this ability once per day at 8th level, and an additional time per day for every four levels beyond 8th.\nDomain " +
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
                    Helpers.LevelEntry(1, DemonDomainEvilBaseFeature),
                    Helpers.LevelEntry(8, EvilDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DemonDomainEvilBaseFeature, EvilDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            
            DemonDomainEvilAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                DemonDomainEvilProgression.ToReference<BlueprintFeatureReference>(),
                DemonDomainEvilProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            DemonDomainEvilProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainEvilProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            DemonDomainEvilProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DemonDomainEvilProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Demon Subdomain")) { return; }
            DomainTools.RegisterDomain(DemonDomainEvilProgression);
            DomainTools.RegisterSecondaryDomain(DemonDomainEvilProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(DemonDomainEvilProgression);
            DomainTools.RegisterTempleDomain(DemonDomainEvilProgression);
            DomainTools.RegisterSecondaryTempleDomain(DemonDomainEvilProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(DemonDomainEvilProgression, DemonDomainEvilProgressionSecondary);

        }
    }
}
