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
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class FistDomain {

        public static void AddFistDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var StrengthDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("3298fd30e221ef74189a06acbf376d29");
            var StrengthBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("44f9162736a5c2040ae8ede853bc6639");
            var WoodenFistIcon = AssetLoader.LoadInternal("Skills", "Icon_WoodenFist.jpg");


            var FistDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("FistDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });
            var PlantDomainNewBaseBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantDomainNewBaseBuff");
            var FistDomainBaseAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FistDomainBaseAbility", bp => {
                bp.SetName("Wooden Fist");
                bp.SetDescription("As a free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke " +
                    "attacks of opportunity, and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day " +
                    "equal to 3 + your Wisdom modifier. These rounds do not need to be consecutive.");
                bp.m_Icon = WoodenFistIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = FistDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = PlantDomainNewBaseBuff.ToReference<BlueprintBuffReference>();
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.DeactivateIfCombatEnded = false;
            });

            //Spelllist
            var TrueStrikeSpell = Resources.GetBlueprint<BlueprintAbility>("2c38da66e5a599347ac95b3294acbe00");
            var BullsStrengthSpell = Resources.GetBlueprint<BlueprintAbility>("4c3d08935262b6544ae97599b3a9556d");
            var MagicFangGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("f1100650705a69c4384d3edd88ba0f52");
            var ForcePunchSpell = Resources.GetBlueprint<BlueprintAbility>("fc58ddcff6ab1394eb6c18e9126bb990");
            var RighteousMightSpell = Resources.GetBlueprint<BlueprintAbility>("90810e5cf53bf854293cbd5ea1066252");
            var StoneSkinSpell = Resources.GetBlueprint<BlueprintAbility>("c66e86905f7606c4eaa5c774f0357b2b");
            var LegendaryProportionsSpell = Resources.GetBlueprint<BlueprintAbility>("da1b292d91ba37948893cdbe9ea89e28");
            var FrightfulAspectSpell = Resources.GetBlueprint<BlueprintAbility>("e788b02f8d21014488067bdd3ba7b325");
            var TransformationSpell = Resources.GetBlueprint<BlueprintAbility>("27203d62eb3d4184c9aced94f22e1806");
            var FistDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("FistDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TrueStrikeSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BullsStrengthSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicFangGreaterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ForcePunchSpell.ToReference<BlueprintAbilityReference>()
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
                            StoneSkinSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            LegendaryProportionsSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FrightfulAspectSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TransformationSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var FistDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("FistDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FistDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var FistDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("FistDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FistDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = FistDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { FistDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = FistDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fist Subdomain");
                bp.SetDescription("\nTrue strength frees believers for the need of weaponry, as with training the faithful become weapons themselves. \nWooden Fist: As a " +
                    "free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke attacks of opportunity, " +
                    "and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day equal to 3 + your Wisdom " +
                    "modifier. These rounds do not need to be consecutive.\nMight of the Gods: At 8th level, you add 1/2 of your level in the class that gave you access to this domain " +
                    "as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var FistDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("FistDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = FistDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var FistDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("FistDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FistDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FistDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FistDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fist Subdomain");
                bp.SetDescription("\nTrue strength frees believers for the need of weaponry, as with training the faithful become weapons themselves. \nWooden Fist: As a " +
                    "free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke attacks of opportunity, " +
                    "and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day equal to 3 + your Wisdom " +
                    "modifier. These rounds do not need to be consecutive.\nMight of the Gods: At 8th level, you add 1/2 of your level in the class that gave you access to this domain " +
                    "as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.\nDomain {g|Encyclopedia:Spell}Spells{/g}: true strike, bull's strength, greater magic fang, " +
                    "force punch, righteous might, stoneskin, legendary proportions, frightful aspect, transformation.");
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
                    Helpers.LevelEntry(1, FistDomainBaseFeature),
                    Helpers.LevelEntry(8, StrengthDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FistDomainBaseFeature, StrengthDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var FistDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("FistDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FistDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FistDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fist Subdomain");
                bp.SetDescription("\nTrue strength frees believers for the need of weaponry, as with training the faithful become weapons themselves. \nWooden Fist: As a " +
                    "free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke attacks of opportunity, " +
                    "and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day equal to 3 + your Wisdom " +
                    "modifier. These rounds do not need to be consecutive.\nMight of the Gods: At 8th level, you add 1/2 of your level in the class that gave you access to this domain " +
                    "as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.\nDomain {g|Encyclopedia:Spell}Spells{/g}: true strike, bull's strength, greater magic fang, " +
                    "force punch, righteous might, stoneskin, legendary proportions, frightful aspect, transformation.");
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
                    Helpers.LevelEntry(1, FistDomainBaseFeature),
                    Helpers.LevelEntry(8, StrengthDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FistDomainBaseFeature, StrengthDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            FistDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                StrengthBlessingFeature.ToReference<BlueprintFeatureReference>(),
                FistDomainProgression.ToReference<BlueprintFeatureReference>(),
                FistDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            FistDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FistDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            FistDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FistDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });

            if (ModSettings.AddedContent.Domains.IsDisabled("Fist Subdomain")) { return; }
            DomainTools.RegisterDomain(FistDomainProgression);
            DomainTools.RegisterSecondaryDomain(FistDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(FistDomainProgression);
            DomainTools.RegisterTempleDomain(FistDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(FistDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(FistDomainProgression, FistDomainProgressionSecondary);
        }
    }
}
