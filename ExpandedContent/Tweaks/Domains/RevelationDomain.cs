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
using Kingmaker.Enums;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class RevelationDomain {

        public static void AddRevelationDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var SunDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("3e301c9d0e735b649955139ee0f5f165");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            

            

            //Spelllist
            var FaerieFireSpell = Resources.GetBlueprint<BlueprintAbility>("4d9bf81b7939b304185d58a09960f589");
            var SeeInvisibilitySpell = Resources.GetBlueprint<BlueprintAbility>("30e5dc243f937fc4b95d2f8f4e1b7ff3");
            var DispelMagicSpell = Resources.GetBlueprint<BlueprintAbility>("92681f181b507b34ea87018e8f7a528a");
            var ShieldOfDawnSpell = Resources.GetBlueprint<BlueprintAbility>("62888999171921e4dafb46de83f4d67d");
            var TrueSeeingSpell = Resources.GetBlueprint<BlueprintAbility>("b3da3fbee6a751d4197e446c7e852bcb");
            var ChainsOfLightSpell = Resources.GetBlueprint<BlueprintAbility>("f8cea58227f59c64399044a82c9735c4");
            var SunbeamSpell = Resources.GetBlueprint<BlueprintAbility>("1fca0ba2fdfe2994a8c8bc1f0f2fc5b1");
            var SunburstSpell = Resources.GetBlueprint<BlueprintAbility>("e96424f70ff884947b06f41a765b7658");
            var ElementalSwarmFireSpell = Resources.GetBlueprint<BlueprintAbility>("1c509c6f186528b49a291ab77f7f997d");
            var RevelationDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("RevelationDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FaerieFireSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SeeInvisibilitySpell.ToReference<BlueprintAbilityReference>()
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
                            ShieldOfDawnSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TrueSeeingSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ChainsOfLightSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SunbeamSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SunburstSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalSwarmFireSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var RevelationDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("RevelationDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RevelationDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var RevelationDomainGuidedEyes = Helpers.CreateBlueprint<BlueprintFeature>("RevelationDomainGuidedEyes", bp => {
                bp.SetName("Guided Eyes");
                bp.SetDescription("Perception is always a class skill for you. In addition, whenever you make a skill check to see through a disguise or find something " +
                    "hat is hidden or concealed, you gain a +4 sacred bonus on the check.");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Sacred;
                    c.Stat = StatType.SkillPerception;
                    c.Value = 4;
                });
                bp.IsClassFeature = true;
            });

            var RevelationDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("RevelationDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { RevelationDomainGuidedEyes.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = RevelationDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revelation Subdomain");
                bp.SetDescription("\nYour deity grants you the power to see all as divine revelation, and punish those who do harm with their light.\nGuided Eyes: Perception " +
                    "is always a class skill for you. In addition, whenever you make a skill check to see through a disguise or find something that is hidden or concealed, " +
                    "you gain a +4 sacred bonus on the check.\nNimbus of Light: At 8th level, you can emit a 30-foot nimbus of light for a number of " +
                    "{g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. Any hostile creature within this " +
                    "radius must succeed at a Fortitude save to resist the effects of this aura. If the creature fails, it is blinded until it leaves the area of the " +
                    "{g|Encyclopedia:Spell}spell{/g}. A creature that has resisted the effect cannot be affected again by this particular aura. In addition, undead within " +
                    "this radius take an amount of damage equal to your level in the class that gave you access to this domain each round that they remain inside the nimbus. " +
                    "These rounds do not need to be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var RevelationDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("RevelationDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = RevelationDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var RevelationDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("RevelationDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevelationDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RevelationDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RevelationDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revelation Subdomain");
                bp.SetDescription("\nYour deity grants you the power to see all as divine revelation, and punish those who do harm with their light.\nGuided Eyes: Perception " +
                    "is always a class skill for you. In addition, whenever you make a skill check to see through a disguise or find something that is hidden or concealed, " +
                    "you gain a +4 sacred bonus on the check.\nNimbus of Light: At 8th level, you can emit a 30-foot nimbus of light for a number of " +
                    "{g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. Any hostile creature within this " +
                    "radius must succeed at a Fortitude save to resist the effects of this aura. If the creature fails, it is blinded until it leaves the area of the " +
                    "{g|Encyclopedia:Spell}spell{/g}. A creature that has resisted the effect cannot be affected again by this particular aura. In addition, undead within " +
                    "this radius take an amount of damage equal to your level in the class that gave you access to this domain each round that they remain inside the nimbus. " +
                    "These rounds do not need to be consecutive.\nDomain Spells: faerie fire, see invisibility, dispel magic, shield of dawn, true seeing, chains of light, " +
                    "sunbeam, sunburst, elemental swarm (fire).");
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
                    Helpers.LevelEntry(1, RevelationDomainBaseFeature),
                    Helpers.LevelEntry(8, SunDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RevelationDomainBaseFeature, SunDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var RevelationDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("RevelationDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevelationDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevelationDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revelation Subdomain");
                bp.SetDescription("\nYour deity grants you the power to see all as divine revelation, and punish those who do harm with their light.\nGuided Eyes: Perception " +
                    "is always a class skill for you. In addition, whenever you make a skill check to see through a disguise or find something that is hidden or concealed, " +
                    "you gain a +4 sacred bonus on the check.\nNimbus of Light: At 8th level, you can emit a 30-foot nimbus of light for a number of " +
                    "{g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. Any hostile creature within this " +
                    "radius must succeed at a Fortitude save to resist the effects of this aura. If the creature fails, it is blinded until it leaves the area of the " +
                    "{g|Encyclopedia:Spell}spell{/g}. A creature that has resisted the effect cannot be affected again by this particular aura. In addition, undead within " +
                    "this radius take an amount of damage equal to your level in the class that gave you access to this domain each round that they remain inside the nimbus. " +
                    "These rounds do not need to be consecutive.\nDomain Spells: faerie fire, see invisibility, dispel magic, shield of dawn, true seeing, chains of light, " +
                    "sunbeam, sunburst, elemental swarm (fire).");
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
                    Helpers.LevelEntry(1, RevelationDomainBaseFeature),
                    Helpers.LevelEntry(8, SunDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RevelationDomainBaseFeature, SunDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            
            RevelationDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                RevelationDomainProgression.ToReference<BlueprintFeatureReference>(),
                RevelationDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            RevelationDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevelationDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            RevelationDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevelationDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Revelation Subdomain")) { return; }
            DomainTools.RegisterDomain(RevelationDomainProgression);
            DomainTools.RegisterSecondaryDomain(RevelationDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(RevelationDomainProgression);
            DomainTools.RegisterTempleDomain(RevelationDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(RevelationDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(RevelationDomainProgression, RevelationDomainProgressionSecondary);
        }
    }
}
