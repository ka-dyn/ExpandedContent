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

namespace ExpandedContent.Tweaks.Domains {
    internal class PsychopompDomainRepose {

        public static void AddPsychopompDomainRepose() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ReposeDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("30dfb2e83f9de7246ad6cb44e36f2b4d");
            var ReposeDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("2b3b95e5ad1dbbf46be85b7f48a60f6a");
            var ShamanWeaponGhostTouchChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("7c96a5203b397744b9429ea3fd010728");
            var GhostTouch = Resources.GetBlueprint<BlueprintWeaponEnchantment>("47857e1a5a3ec1a46adf6491b1423b4f");

            //PsychopompDomainGreaterResource
            var PsychopompDomainGreaterResource = Resources.GetModBlueprint<BlueprintAbilityResource>("PsychopompDomainGreaterResource");
            //PsychopompDomainGreaterAbility
            var PsychopompDomainGreaterBuff = Resources.GetModBlueprint<BlueprintBuff>("PsychopompDomainGreaterBuff");
            var PsychopompDomainGreaterAbility = Resources.GetModBlueprint<BlueprintActivatableAbility>("PsychopompDomainGreaterAbility");
            //PsychopompDomainGreaterFeature
            var PsychopompDomainGreaterFeature = Resources.GetModBlueprint<BlueprintFeature>("PsychopompDomainGreaterFeature");


            //Spelllist
            var DoomSpell = Resources.GetBlueprint<BlueprintAbility>("fbdd8c455ac4cde4a9a3e18c84af9485");
            var ScareSpell = Resources.GetBlueprint<BlueprintAbility>("08cb5f4c3b2695e44971bf5c45205df0");
            var RayOfExhaustionSpell = Resources.GetBlueprint<BlueprintAbility>("8eead52509987034ea9025d60cc05985");
            var DeathWardSpell = Resources.GetBlueprint<BlueprintAbility>("e9cc9378fd6841f48ad59384e79e9953");
            var SlayLivingSpell = Resources.GetBlueprint<BlueprintAbility>("4fbd47525382517419c66fb548fe9a67");
            var SummonMonsterVIBaseSpell = Resources.GetBlueprint<BlueprintAbility>("e740afbab0147944dab35d83faa0ae1c");
            var DestructionSpell = Resources.GetBlueprint<BlueprintAbility>("3b646e1db3403b940bf620e01d2ce0c7");
            var HorridWiltingSpell = Resources.GetBlueprint<BlueprintAbility>("08323922485f7e246acb3d2276515526");
            var WailOfBansheeSpell = Resources.GetBlueprint<BlueprintAbility>("b24583190f36a8442b212e45226c54fc");
            var PsychopompDomainReposeSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("PsychopompDomainReposeSpellList", bp => {
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
                            ScareSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RayOfExhaustionSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DeathWardSpell.ToReference<BlueprintAbilityReference>()
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
                            DestructionSpell.ToReference<BlueprintAbilityReference>()
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
                            WailOfBansheeSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var PsychopompDomainReposeSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainReposeSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = PsychopompDomainReposeSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var PsychopompDomainReposeBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainReposeBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ReposeDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = ReposeDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { ReposeDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = PsychopompDomainReposeSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Repose");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nGentle Rest: Your " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} can fill a creature with lethargy, causing a living creature to become staggered for 1 {g|Encyclopedia:Combat_Round}round{/g} " +
                    "as a melee touch {g|Encyclopedia:Attack}attack{/g}. If you touch a staggered living creature, that creature falls asleep for 1 round instead. Undead creatures " +
                    "touched are staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. You can use this ability a number of times per day equal " +
                    "to 3 + your Wisdom modifier.\nSpirit Touch: At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon " +
                    "special ability. You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var PsychopompDomainReposeAllowed = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainReposeAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = PsychopompDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var PsychopompDomainReposeProgression = Helpers.CreateBlueprint<BlueprintProgression>("PsychopompDomainReposeProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainReposeAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = PsychopompDomainReposeSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = PsychopompDomainReposeSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Repose");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nGentle Rest: Your " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} can fill a creature with lethargy, causing a living creature to become staggered for 1 {g|Encyclopedia:Combat_Round}round{/g} " +
                    "as a melee touch {g|Encyclopedia:Attack}attack{/g}. If you touch a staggered living creature, that creature falls asleep for 1 round instead. Undead creatures " +
                    "touched are staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. You can use this ability a number of times per day equal " +
                    "to 3 + your Wisdom modifier.\nSpirit Touch: At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon " +
                    "special ability. You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: doom, scare, ray of exhaustion, death ward, slay living, summon monster VI, destruction, horrid wilting, wail of the banshee.");
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
                    Helpers.LevelEntry(1, PsychopompDomainReposeBaseFeature),
                    Helpers.LevelEntry(6, PsychopompDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(PsychopompDomainReposeBaseFeature, PsychopompDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var PsychopompDomainReposeProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("PsychopompDomainReposeProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainReposeAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainReposeProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Repose");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nGentle Rest: Your " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} can fill a creature with lethargy, causing a living creature to become staggered for 1 {g|Encyclopedia:Combat_Round}round{/g} " +
                    "as a melee touch {g|Encyclopedia:Attack}attack{/g}. If you touch a staggered living creature, that creature falls asleep for 1 round instead. Undead creatures " +
                    "touched are staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. You can use this ability a number of times per day equal " +
                    "to 3 + your Wisdom modifier.\nSpirit Touch: At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon " +
                    "special ability. You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: doom, scare, ray of exhaustion, death ward, slay living, summon monster VI, destruction, horrid wilting, wail of the banshee.");
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
                    Helpers.LevelEntry(1, PsychopompDomainReposeBaseFeature),
                    Helpers.LevelEntry(6, PsychopompDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(PsychopompDomainReposeBaseFeature, PsychopompDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            PsychopompDomainReposeAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                PsychopompDomainReposeProgression.ToReference<BlueprintFeatureReference>(),
                PsychopompDomainReposeProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            PsychopompDomainReposeProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainReposeProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            PsychopompDomainReposeProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainReposeProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });            
            if (ModSettings.AddedContent.Domains.IsDisabled("Psychopomp Subdomain")) { return; }
            DomainTools.RegisterDomain(PsychopompDomainReposeProgression);
            DomainTools.RegisterSecondaryDomain(PsychopompDomainReposeProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(PsychopompDomainReposeProgression);
            DomainTools.RegisterTempleDomain(PsychopompDomainReposeProgression);
            DomainTools.RegisterSecondaryTempleDomain(PsychopompDomainReposeProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(PsychopompDomainReposeProgression, PsychopompDomainReposeProgressionSecondary);
        }
    }
}
