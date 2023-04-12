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
using Kingmaker.ResourceLinks;
using Kingmaker.Designers.Mechanics.Buffs;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class ArchonDomainLaw {

        public static void AddArchonDomainLaw() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var LawDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("a970537ea2da20e42ae709c0bb8f793f");
            var LawDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("c87873e6bc4bb884890e69f12e4e270e");
            var HolyAura = Resources.GetBlueprint<BlueprintAbility>("808ab74c12df8784ab4eeaf6a107dbea");

            //ArchonDomainLawDifficultTerrainBuff
            var ArchonDomainGreaterAreaBuff = Resources.GetModBlueprint<BlueprintBuff>("ArchonDomainGreaterAreaBuff");
            //ArchonDomainLawGreaterArea
            var ArchonDomainGreaterArea = Resources.GetModBlueprint<BlueprintAbilityAreaEffect>("ArchonDomainGreaterArea");
            //ArchonDomainLawGreaterBuff
            var ArchonDomainGreaterBuff = Resources.GetModBlueprint<BlueprintBuff>("ArchonDomainGreaterBuff");
            //ArchonDomainGreaterResource
            var ArchonDomainGreaterResource = Resources.GetModBlueprint<BlueprintAbilityResource>("ArchonDomainGreaterResource");
            //ArchonDomainLawGreaterAbility
            var ArchonDomainGreaterAbility = Resources.GetModBlueprint<BlueprintActivatableAbility>("ArchonDomainGreaterAbility");
            //ArchonDomainLawGreaterFeature
            var ArchonDomainGreaterFeature = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGreaterFeature");

            //Spelllist
            var DivineFavorSpell = Resources.GetBlueprint<BlueprintAbility>("9d5d2d3ffdd73c648af3eb3e585b1113");
            var ProtectionFromChaosCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("0ec75ec95d9e39d47a23610123ba1bad");
            var PrayerSpell = Resources.GetBlueprint<BlueprintAbility>("faabd2cc67efa4646ac58c7bb3e40fcc");
            var HolySmiteSpell = Resources.GetBlueprint<BlueprintAbility>("ad5ed5ea4ec52334a94e975a64dad336");
            var DominatePersonSpell = Resources.GetBlueprint<BlueprintAbility>("d7cbd2004ce66a042aeab2e95a3c5c61");
            var BladeBarrierSpell = Resources.GetBlueprint<BlueprintAbility>("36c8971e91f1745418cc3ffdfac17b74");
            var DictumSpell = Resources.GetBlueprint<BlueprintAbility>("302ab5e241931a94881d323a7844ae8f");
            var ShieldOfLawSpell = Resources.GetBlueprint<BlueprintAbility>("73e7728808865094b8892613ddfaf7f5");
            var DominateMonsterSpell = Resources.GetBlueprint<BlueprintAbility>("3c17035ec4717674cae2e841a190e757");
            var ArchonDomainLawSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("ArchonDomainLawSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DivineFavorSpell.ToReference<BlueprintAbilityReference>()
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
                            HolySmiteSpell.ToReference<BlueprintAbilityReference>()
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
            var ArchonDomainLawSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArchonDomainLawSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ArchonDomainLawSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var ArchonDomainLawBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArchonDomainLawBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LawDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = LawDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { LawDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ArchonDomainLawSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Archon Subdomain - Law");
                bp.SetDescription("\nYou follow the archons path of righteousness.\n{g|Encyclopedia:TouchAttack}Touch{/g} of Law: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a willing creature as a {g|Encyclopedia:Standard_Actions}standard action{/g}, infusing it with the power of divine order and allowing it to treat all " +
                    "{g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, and " +
                    "{g|Encyclopedia:Saving_Throw}saving throws{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g} as if the natural {g|Encyclopedia:Dice}d20{/g} " +
                    "roll resulted in an 11.[LONGSTART] You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.[LONGEND]\nAura of Menace: At 8th level, you can emit a 30-foot aura of menace as a standard action. Enemies in this aura take a –2 penalty to " +
                    "AC and on attacks and saves as long as they remain inside the aura. You can use this ability for a number of rounds per day equal " +
                    "to your cleric level. These rounds do not need to be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var ArchonDomainLawAllowed = Helpers.CreateBlueprint<BlueprintFeature>("ArchonDomainLawAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = ArchonDomainLawSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var ArchonDomainLawProgression = Helpers.CreateBlueprint<BlueprintProgression>("ArchonDomainLawProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArchonDomainLawAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ArchonDomainLawSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ArchonDomainLawSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Archon Subdomain - Law");
                bp.SetDescription("\nYou follow the archons path of righteousness.\n{g|Encyclopedia:TouchAttack}Touch{/g} of Law: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a willing creature as a {g|Encyclopedia:Standard_Actions}standard action{/g}, infusing it with the power of divine order and allowing it to treat all " +
                    "{g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, and " +
                    "{g|Encyclopedia:Saving_Throw}saving throws{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g} as if the natural {g|Encyclopedia:Dice}d20{/g} " +
                    "roll resulted in an 11.[LONGSTART] You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.[LONGEND]\nAura of Menace: At 8th level, you can emit a 30-foot aura of menace as a standard action. Enemies in this aura take a –2 penalty to " +
                    "AC and on attacks and saves as long as they remain inside the aura. You can use this ability for a number of rounds per day equal " +
                    "to your cleric level. These rounds do not need to be consecutive.\nDomain {g|Encyclopedia:Spell}Spells{/g}: divine favor, communal protection " +
                    "from chaos, prayer, holy smite, dominate person, blade barrier, dictum, sheild of law, dominate monster.");
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
                    Helpers.LevelEntry(1, ArchonDomainLawBaseFeature),
                    Helpers.LevelEntry(8, ArchonDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ArchonDomainLawBaseFeature, ArchonDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var ArchonDomainLawProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("ArchonDomainLawProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArchonDomainLawAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArchonDomainLawProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Archon Subdomain - Law");
                bp.SetDescription("\nYou follow the archons path of righteousness.\n{g|Encyclopedia:TouchAttack}Touch{/g} of Law: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a willing creature as a {g|Encyclopedia:Standard_Actions}standard action{/g}, infusing it with the power of divine order and allowing it to treat all " +
                    "{g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, and " +
                    "{g|Encyclopedia:Saving_Throw}saving throws{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g} as if the natural {g|Encyclopedia:Dice}d20{/g} " +
                    "roll resulted in an 11.[LONGSTART] You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.[LONGEND]\nAura of Menace: At 8th level, you can emit a 30-foot aura of menace as a standard action. Enemies in this aura take a –2 penalty to " +
                    "AC and on attacks and saves as long as they remain inside the aura. You can use this ability for a number of rounds per day equal " +
                    "to your cleric level. These rounds do not need to be consecutive.\nDomain {g|Encyclopedia:Spell}Spells{/g}: divine favor, communal protection " +
                    "from chaos, prayer, holy smite, dominate person, blade barrier, dictum, sheild of law, dominate monster.");
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
                    Helpers.LevelEntry(1, ArchonDomainLawBaseFeature),
                    Helpers.LevelEntry(8, ArchonDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ArchonDomainLawBaseFeature, ArchonDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            
            ArchonDomainLawAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                ArchonDomainLawProgression.ToReference<BlueprintFeatureReference>(),
                ArchonDomainLawProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            ArchonDomainLawProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArchonDomainLawProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            ArchonDomainLawProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ArchonDomainLawProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(ArchonDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Archon Subdomain")) { return; }
            DomainTools.RegisterDomain(ArchonDomainLawProgression);
            DomainTools.RegisterSecondaryDomain(ArchonDomainLawProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(ArchonDomainLawProgression);
            DomainTools.RegisterTempleDomain(ArchonDomainLawProgression);
            DomainTools.RegisterSecondaryTempleDomain(ArchonDomainLawProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(ArchonDomainLawProgression, ArchonDomainLawProgressionSecondary);

        }

    }
}
