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
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Blueprints.Items.Ecnchantments;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Domains {
    internal class PsychopompDomainDeath {

        public static void AddPsychopompDomainDeath() {

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
            var DeathDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("979f63920af22344d81da5099c9ec32e");
            var DeathDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("c28053c4878c0654cbfeaf96c2814955");
            var ShamanWeaponGhostTouchChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("7c96a5203b397744b9429ea3fd010728");
            var GhostTouch = Resources.GetBlueprint<BlueprintWeaponEnchantment>("47857e1a5a3ec1a46adf6491b1423b4f");

            //PsychopompDomainGreaterResource
            var PsychopompDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("PsychopompDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 6,
                    StartingIncrease = 1,
                };
            });


            var PsychopompDomainGreaterBuff = Helpers.CreateBuff("PsychopompDomainGreaterBuff", bp => {
                bp.SetName("Spirit Touch");
                bp.SetDescription("At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. " +
                    "You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.\nA ghost touch weapon deals " +
                    "{g|Encyclopedia:Damage}damage{/g} normally against {g|Encyclopedia:Incorporeal_Touch_Attack}incorporeal{/g} creatures, regardless of its " +
                    "{g|Encyclopedia:Bonus}bonus{/g}. An incorporeal creature's 50% reduction in damage from corporeal sources does not apply to " +
                    "{g|Encyclopedia:Attack}attacks{/g} made against it with ghost touch weapons.");
                bp.m_Icon = ShamanWeaponGhostTouchChoice.m_Icon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = GhostTouch.ToReference<BlueprintItemEnchantmentReference>();
                    c.AllWeapons = true;
                });                
            });

            //PsychopompDomainGreaterAbility
            var PsychopompDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PsychopompDomainGreaterAbility", bp => {
                bp.SetName("Spirit Touch");
                bp.SetDescription("At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. " +
                    "You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.\nA ghost touch weapon deals " +
                    "{g|Encyclopedia:Damage}damage{/g} normally against {g|Encyclopedia:Incorporeal_Touch_Attack}incorporeal{/g} creatures, regardless of its " +
                    "{g|Encyclopedia:Bonus}bonus{/g}. An incorporeal creature's 50% reduction in damage from corporeal sources does not apply to " +
                    "{g|Encyclopedia:Attack}attacks{/g} made against it with ghost touch weapons.");
                bp.m_Icon = ShamanWeaponGhostTouchChoice.m_Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = PsychopompDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = PsychopompDomainGreaterBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
                bp.DeactivateIfCombatEnded = false;

            });
            //PsychopompDomainGreaterFeature
            var PsychopompDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainGreaterFeature", bp => {
                bp.SetName("Spirit Touch");
                bp.SetDescription("At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. " +
                    "You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.\nA ghost touch weapon deals " +
                    "{g|Encyclopedia:Damage}damage{/g} normally against {g|Encyclopedia:Incorporeal_Touch_Attack}incorporeal{/g} creatures, regardless of its " +
                    "{g|Encyclopedia:Bonus}bonus{/g}. An incorporeal creature's 50% reduction in damage from corporeal sources does not apply to " +
                    "{g|Encyclopedia:Attack}attacks{/g} made against it with ghost touch weapons.");
                bp.m_Icon = ShamanWeaponGhostTouchChoice.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = PsychopompDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PsychopompDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var CauseFearSpell = Resources.GetBlueprint<BlueprintAbility>("bd81a3931aa285a4f9844585b5d97e51");
            var BoneshakerSpell = Resources.GetBlueprint<BlueprintAbility>("b7731c2b4fa1c9844a092329177be4c3");
            var RayOfExhaustionSpell = Resources.GetBlueprint<BlueprintAbility>("8eead52509987034ea9025d60cc05985");
            var EnervationSpell = Resources.GetBlueprint<BlueprintAbility>("f34fb78eaaec141469079af124bcfa0f");
            var SlayLivingSpell = Resources.GetBlueprint<BlueprintAbility>("4fbd47525382517419c66fb548fe9a67");
            var SummonMonsterVIBaseSpell = Resources.GetBlueprint<BlueprintAbility>("e740afbab0147944dab35d83faa0ae1c");
            var DestructionSpell = Resources.GetBlueprint<BlueprintAbility>("3b646e1db3403b940bf620e01d2ce0c7");
            var HorridWiltingSpell = Resources.GetBlueprint<BlueprintAbility>("08323922485f7e246acb3d2276515526");
            var WailOfBansheeSpell = Resources.GetBlueprint<BlueprintAbility>("b24583190f36a8442b212e45226c54fc");
            var PsychopompDomainDeathSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("PsychopompDomainDeathSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CauseFearSpell.ToReference<BlueprintAbilityReference>()
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
                            RayOfExhaustionSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EnervationSpell.ToReference<BlueprintAbilityReference>()
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
            var PsychopompDomainDeathSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainDeathSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = PsychopompDomainDeathSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var PsychopompDomainDeathBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainDeathBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DeathDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = DeathDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DeathDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = PsychopompDomainDeathSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Death");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nBleeding Touch: " +
                    "As a melee touch {g|Encyclopedia:Attack}attack{/g}, you can cause a living creature to take {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} " +
                    "per {g|Encyclopedia:Combat_Round}round{/g}. This effect persists for a number of rounds equal to 1/2 your level in the class that gave you access to this domain " +
                    "(minimum 1). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nSpirit Touch: At 6th level, as a " +
                    "swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. You can use this power a number of rounds per " +
                    "day equal to your cleric level. These rounds need not be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var PsychopompDomainDeathAllowed = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainDeathAllowed", bp => {
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
            var PsychopompDomainDeathProgression = Helpers.CreateBlueprint<BlueprintProgression>("PsychopompDomainDeathProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = PsychopompDomainDeathSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = PsychopompDomainDeathSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Death");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nBleeding Touch: " +
                    "As a melee touch {g|Encyclopedia:Attack}attack{/g}, you can cause a living creature to take {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} " +
                    "per {g|Encyclopedia:Combat_Round}round{/g}. This effect persists for a number of rounds equal to 1/2 your level in the class that gave you access to this domain " +
                    "(minimum 1). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nSpirit Touch: At 6th level, as a " +
                    "swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. You can use this power a number of rounds per " +
                    "day equal to your cleric level. These rounds need not be consecutive.\nDomain {g|Encyclopedia:Spell}Spells{/g}: cause fear, boneshaker, ray of exhaustion, " +
                    "enervation, slay living, summon monster VI, destruction, horrid wilting, wail of the banshee.");
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
                    Helpers.LevelEntry(1, PsychopompDomainDeathBaseFeature),
                    Helpers.LevelEntry(6, PsychopompDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(PsychopompDomainDeathBaseFeature, PsychopompDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var PsychopompDomainDeathProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("PsychopompDomainDeathProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Death");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nBleeding Touch: " +
                    "As a melee touch {g|Encyclopedia:Attack}attack{/g}, you can cause a living creature to take {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} " +
                    "per {g|Encyclopedia:Combat_Round}round{/g}. This effect persists for a number of rounds equal to 1/2 your level in the class that gave you access to this domain " +
                    "(minimum 1). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nSpirit Touch: At 6th level, as a " +
                    "swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. You can use this power a number of rounds per " +
                    "day equal to your cleric level. These rounds need not be consecutive.\nDomain {g|Encyclopedia:Spell}Spells{/g}: cause fear, boneshaker, ray of exhaustion, " +
                    "enervation, slay living, summon monster VI, destruction, horrid wilting, wail of the banshee.");
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
                    Helpers.LevelEntry(1, PsychopompDomainDeathBaseFeature),
                    Helpers.LevelEntry(6, PsychopompDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(PsychopompDomainDeathBaseFeature, PsychopompDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var DeathDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("3c32538833e94314b5a6efb43fa83924");
            var DeathDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("eebd3c80b7494aa3bae76148984a0269");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var PsychopompDomainDeathAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainDeathAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var PsychopompDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("PsychopompDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    m_Class = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    LevelIncrease = 1,
                    StartingLevel = 6,
                    StartingIncrease = 1,
                    LevelStep = 1,
                    PerStepIncrease = 1,
                };
                bp.m_Min = 1;
            });

            var PsychopompDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PsychopompDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Spirit Touch");
                bp.SetDescription("At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. " +
                    "You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.\nA ghost touch weapon deals " +
                    "{g|Encyclopedia:Damage}damage{/g} normally against {g|Encyclopedia:Incorporeal_Touch_Attack}incorporeal{/g} creatures, regardless of its " +
                    "{g|Encyclopedia:Bonus}bonus{/g}. An incorporeal creature's 50% reduction in damage from corporeal sources does not apply to " +
                    "{g|Encyclopedia:Attack}attacks{/g} made against it with ghost touch weapons.");
                bp.m_Icon = ShamanWeaponGhostTouchChoice.m_Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = PsychopompDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = PsychopompDomainGreaterBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
                bp.DeactivateIfCombatEnded = false;

            });

            var PsychopompDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Spirit Touch");
                bp.SetDescription("At 6th level, as a swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. " +
                    "You can use this power a number of rounds per day equal to your cleric level. These rounds need not be consecutive.\nA ghost touch weapon deals " +
                    "{g|Encyclopedia:Damage}damage{/g} normally against {g|Encyclopedia:Incorporeal_Touch_Attack}incorporeal{/g} creatures, regardless of its " +
                    "{g|Encyclopedia:Bonus}bonus{/g}. An incorporeal creature's 50% reduction in damage from corporeal sources does not apply to " +
                    "{g|Encyclopedia:Attack}attacks{/g} made against it with ghost touch weapons.");
                bp.m_Icon = ShamanWeaponGhostTouchChoice.m_Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = PsychopompDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PsychopompDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var PsychopompDomainDeathBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("PsychopompDomainDeathBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DeathDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DeathDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DeathDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = PsychopompDomainDeathSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Death");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nBleeding Touch: " +
                    "As a melee touch {g|Encyclopedia:Attack}attack{/g}, you can cause a living creature to take {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} " +
                    "per {g|Encyclopedia:Combat_Round}round{/g}. This effect persists for a number of rounds equal to 1/2 your level in the class that gave you access to this domain " +
                    "(minimum 1). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nSpirit Touch: At 6th level, as a " +
                    "swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. You can use this power a number of rounds per " +
                    "day equal to your cleric level. These rounds need not be consecutive.");
                bp.IsClassFeature = true;
            });

            var PsychopompDomainDeathProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("PsychopompDomainDeathProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = PsychopompDomainDeathProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Psychopomp Subdomain - Death");
                bp.SetDescription("\nYou channel the power of the Boneyard, the place of judgement for all souls, your touch can move foes to an early rest.\nBleeding Touch: " +
                    "As a melee touch {g|Encyclopedia:Attack}attack{/g}, you can cause a living creature to take {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} " +
                    "per {g|Encyclopedia:Combat_Round}round{/g}. This effect persists for a number of rounds equal to 1/2 your level in the class that gave you access to this domain " +
                    "(minimum 1). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nSpirit Touch: At 6th level, as a " +
                    "swift action, you can give your natural weapons or any weapons you wield the ghost touch weapon special ability. You can use this power a number of rounds per " +
                    "day equal to your cleric level. These rounds need not be consecutive.\nDomain {g|Encyclopedia:Spell}Spells{/g}: cause fear, boneshaker, ray of exhaustion, " +
                    "enervation, slay living, summon monster VI, destruction, horrid wilting, wail of the banshee.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, PsychopompDomainDeathBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, PsychopompDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(PsychopompDomainDeathBaseFeatureSeparatist, PsychopompDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            PsychopompDomainDeathAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                PsychopompDomainDeathProgression.ToReference<BlueprintFeatureReference>(),
                PsychopompDomainDeathProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            PsychopompDomainDeathProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = PsychopompDomainDeathProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            PsychopompDomainDeathProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = PsychopompDomainDeathProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            PsychopompDomainDeathProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = PsychopompDomainDeathProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            PsychopompDomainDeathProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = PsychopompDomainDeathProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            PsychopompDomainDeathProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = PsychopompDomainDeathProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Psychopomp Subdomain")) { return; }
            DomainTools.RegisterDomain(PsychopompDomainDeathProgression);
            DomainTools.RegisterSecondaryDomain(PsychopompDomainDeathProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(PsychopompDomainDeathProgression);
            DomainTools.RegisterTempleDomain(PsychopompDomainDeathProgression);
            DomainTools.RegisterSecondaryTempleDomain(PsychopompDomainDeathProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(PsychopompDomainDeathProgression, PsychopompDomainDeathProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(PsychopompDomainDeathProgressionSeparatist);
        }
    }
}
