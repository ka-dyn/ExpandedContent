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
    internal class GrowthDomain {

        public static void AddGrowthDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var PlantDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("24ec8901c8092264f864c7626ec3677e");
            //var PlantBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");

            var PlantDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("8942e816a533a4a40b04745c516d085a");
            var PlantDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("82c76ed1de2e1114f8c08862cf2e6ee6");

            //Spelllist
            var EnlargePersonSpell = Resources.GetBlueprint<BlueprintAbility>("c60969e7f264e6d4b84a1499fdcf9039");
            var BarkskinSpell = Resources.GetBlueprint<BlueprintAbility>("5b77d7cc65b8ab74688e74a37fc2f553");
            var ContagionSpell = Resources.GetBlueprint<BlueprintAbility>("48e2744846ed04b4580be1a3343a5d3d");
            var ThornBodySpell = Resources.GetBlueprint<BlueprintAbility>("2daf9c5112f16d54ab3cd6904c705c59");
            var RighteousMightSpell = Resources.GetBlueprint<BlueprintAbility>("90810e5cf53bf854293cbd5ea1066252");
            var PlantShapeIIShamblingMoundSpell = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIAbility");
            var ChangestaffSpell = Resources.GetBlueprint<BlueprintAbility>("26be70c4664d07446bdfe83504c1d757");
            var MindBlankSpell = Resources.GetBlueprint<BlueprintAbility>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var ShamblerSpell = Resources.GetModBlueprint<BlueprintAbility>("ShamblerAbility");
            var GrowthDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("GrowthDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EnlargePersonSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BarkskinSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ContagionSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ThornBodySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RighteousMightSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PlantShapeIIShamblingMoundSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ChangestaffSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MindBlankSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShamblerSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var GrowthDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("GrowthDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = GrowthDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var GrowthDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("GrowthDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = PlantDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { PlantDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = GrowthDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Growth Subdomain");
                bp.SetDescription("\nYou are gifted the power to expand life, can gain defensive thorns, and can communicate with plants. \nEnlarge: As a {g|Encyclopedia:Swift_Action}swift action{/g} " +
                    "you can enlarge yourself for 1 {g|Encyclopedia:Combat_Round}round{/g}, as if you were the target of the enlarge person {g|Encyclopedia:Spell}spell{/g}. You can use this power a " +
                    "number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nBramble Armor: At 6th level, you can cause a host of wooden thorns to burst from your skin " +
                    "as a {g|Encyclopedia:Free_Action}free action{/g}. While bramble armor is in effect, any foe striking you with a melee weapon without {g|Encyclopedia:Reach}reach{/g} takes " +
                    "{g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage_Type}piercing damage{/g} + 1 point per two levels you possess in the class that gave you access to this domain. " +
                    "You can use this ability for a number of rounds per day equal to your level in the class that gave you access to this domain. These rounds do not need to be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var GrowthDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("GrowthDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = GrowthDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var GrowthDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("GrowthDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = GrowthDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = GrowthDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = GrowthDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Growth Subdomain");
                bp.SetDescription("\nYou are gifted the power to expand life, can gain defensive thorns, and can communicate with plants. \nEnlarge: As a {g|Encyclopedia:Swift_Action}swift action{/g} " +
                    "you can enlarge yourself for 1 {g|Encyclopedia:Combat_Round}round{/g}, as if you were the target of the enlarge person {g|Encyclopedia:Spell}spell{/g}. You can use this power a " +
                    "number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nBramble Armor: At 6th level, you can cause a host of wooden thorns to burst from your skin " +
                    "as a {g|Encyclopedia:Free_Action}free action{/g}. While bramble armor is in effect, any foe striking you with a melee weapon without {g|Encyclopedia:Reach}reach{/g} takes " +
                    "{g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage_Type}piercing damage{/g} + 1 point per two levels you possess in the class that gave you access to this domain. " +
                    "You can use this ability for a number of rounds per day equal to your level in the class that gave you access to this domain. These rounds do not need to be consecutive. \nDomain " +
                    "Spells: enlarge person, barkskin, contagion, thorn body, righteous might, plant shape II (shambling mound), changestaff, mind blank, shambler.");
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
                    Helpers.LevelEntry(1, GrowthDomainBaseFeature),
                    Helpers.LevelEntry(6, PlantDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(GrowthDomainBaseFeature, PlantDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var GrowthDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("GrowthDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = GrowthDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = GrowthDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Growth Subdomain");
                bp.SetDescription("\nYou are gifted the power to expand life, can gain defensive thorns, and can communicate with plants. \nEnlarge: As a {g|Encyclopedia:Swift_Action}swift action{/g} " +
                    "you can enlarge yourself for 1 {g|Encyclopedia:Combat_Round}round{/g}, as if you were the target of the enlarge person {g|Encyclopedia:Spell}spell{/g}. You can use this power a " +
                    "number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nBramble Armor: At 6th level, you can cause a host of wooden thorns to burst from your skin " +
                    "as a {g|Encyclopedia:Free_Action}free action{/g}. While bramble armor is in effect, any foe striking you with a melee weapon without {g|Encyclopedia:Reach}reach{/g} takes " +
                    "{g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage_Type}piercing damage{/g} + 1 point per two levels you possess in the class that gave you access to this domain. " +
                    "You can use this ability for a number of rounds per day equal to your level in the class that gave you access to this domain. These rounds do not need to be consecutive. \nDomain " +
                    "Spells: enlarge person, barkskin, contagion, thorn body, righteous might, plant shape II (shambling mound), changestaff, mind blank, shambler.");
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
                    Helpers.LevelEntry(1, GrowthDomainBaseFeature),
                    Helpers.LevelEntry(6, PlantDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(GrowthDomainBaseFeature, PlantDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // GrowthDomainSpellListFeatureDruid
            var GrowthDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("GrowthDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = GrowthDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // GrowthDomainProgressionDruid
            var GrowthDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("GrowthDomainProgressionDruid", bp => {
                bp.SetName("Growth Subdomain");
                bp.SetDescription("\nYou are gifted the power to expand life, can gain defensive thorns, and can communicate with plants. \nEnlarge: As a {g|Encyclopedia:Swift_Action}swift action{/g} " +
                    "you can enlarge yourself for 1 {g|Encyclopedia:Combat_Round}round{/g}, as if you were the target of the enlarge person {g|Encyclopedia:Spell}spell{/g}. You can use this power a " +
                    "number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nBramble Armor: At 6th level, you can cause a host of wooden thorns to burst from your skin " +
                    "as a {g|Encyclopedia:Free_Action}free action{/g}. While bramble armor is in effect, any foe striking you with a melee weapon without {g|Encyclopedia:Reach}reach{/g} takes " +
                    "{g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage_Type}piercing damage{/g} + 1 point per two levels you possess in the class that gave you access to this domain. " +
                    "You can use this ability for a number of rounds per day equal to your level in the class that gave you access to this domain. These rounds do not need to be consecutive. \nDomain " +
                    "Spells: enlarge person, barkskin, contagion, thorn body, righteous might, plant shape II (shambling mound), changestaff, mind blank, shambler.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain, FeatureGroup.BlightDruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, GrowthDomainBaseFeature,GrowthDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(6, PlantDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(GrowthDomainBaseFeature, PlantDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            GrowthDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                //PlantBlessingFeature.ToReference<BlueprintFeatureReference>(),
                GrowthDomainProgression.ToReference<BlueprintFeatureReference>(),
                GrowthDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            GrowthDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = GrowthDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            GrowthDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = GrowthDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });

            if (ModSettings.AddedContent.Domains.IsDisabled("Growth Subdomain")) { return; }
            DomainTools.RegisterDomain(GrowthDomainProgression);
            DomainTools.RegisterSecondaryDomain(GrowthDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(GrowthDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(GrowthDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(GrowthDomainProgression);
            DomainTools.RegisterTempleDomain(GrowthDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(GrowthDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(GrowthDomainProgression, GrowthDomainProgressionSecondary);
        }
    }
}
