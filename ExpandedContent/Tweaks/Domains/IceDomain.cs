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
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Enums.Damage;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class IceDomain {

        public static void AddIceDomain() {

            //This Domain will not be updated due to Owlcat adding their own version

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var WaterDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("5e1db2ef80ff361448549beeb7785791");
            var WaterDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("51e28e9264e50df4cb762428cea96119");
            var WaterBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("0f457943bb99f9b48b709c90bfc0467e");
            var IceBodySpell = Resources.GetBlueprint<BlueprintAbility>("89778dc261fe6094bb2445cb389842d2");
            var IceBodyBuff = Resources.GetBlueprint<BlueprintBuff>("a6da7d6a5c9377047a7bd2680912860f");

            var IceDomainGreaterBuff = Helpers.CreateBuff("IceDomainGreaterBuff", bp => {
                bp.SetName("Body of Ice");
                bp.SetDescription("Your body is transmuted to a ice form, you are immune to cold and have DR 5/—, but you take twice the normal amount of damage from fire." +
                    "You can end the transmutation with a free action on your turn. You can take on the form of ice for a number of rounds per day equal to your cleric " +
                    "level. The rounds need not be consecutive");
                bp.m_Icon = IceBodySpell.Icon;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Cold;
                });
                bp.AddComponent<AddEnergyVulnerability>(c => {
                    c.Type = DamageEnergyType.Fire;
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.FxOnStart = IceBodyBuff.FxOnStart;
                bp.FxOnRemove = IceBodyBuff.FxOnRemove;
                
            });

            //IceDomainGreaterResource
            var IceDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("IceDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 8,
                    StartingIncrease = 1,
                };
            });

            //IceDomainGreaterAbility
            var IceDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("IceDomainGreaterAbility", bp => {
                bp.SetName("Body of Ice");
                bp.SetDescription("At 8th level, you can transmute your body and equipment to ice for a period of time. It takes a standard action to take on " +
                    "the form of ice, and you can end the transmutation with a free action on your turn. When you take on the form of ice, you are immune to " +
                    "cold and have DR 5/—, but you take twice the normal amount of damage from fire. You can take on the form of ice for a number of rounds per " +
                    "day equal to your cleric level. The rounds need not be consecutive.");
                bp.m_Icon = IceBodySpell.Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = IceDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = IceDomainGreaterBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;                
            });

            //IceDomainGreaterFeature
            var IceDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("IceDomainGreaterFeature", bp => {
                bp.SetName("Body of Ice");
                bp.SetDescription("At 8th level, you can transmute your body and equipment to ice for a period of time. It takes a standard action to take on " +
                    "the form of ice, and you can end the transmutation with a free action on your turn. When you take on the form of ice, you are immune to " +
                    "cold and have DR 5/—, but you take twice the normal amount of damage from fire. You can take on the form of ice for a number of rounds per " +
                    "day equal to your cleric level. The rounds need not be consecutive.");
                bp.m_Icon = IceBodySpell.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = IceDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { IceDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;


            });

            //Spelllist
            var RayOfSickeningSpell = Resources.GetBlueprint<BlueprintAbility>("fa3078b9976a5b24caf92e20ee9c0f54");
            var AcidArrowSpell = Resources.GetBlueprint<BlueprintAbility>("9a46dfd390f943647ab4395fc997936d");
            var StinkingCloudSpell = Resources.GetBlueprint<BlueprintAbility>("68a9e6d7256f1354289a39003a46d826");
            var FreedomOfMovementSpell = Resources.GetBlueprint<BlueprintAbility>("0087fc2d64b6095478bc7b8d7d512caf");
            var IceStormSpell = Resources.GetBlueprint<BlueprintAbility>("fcb028205a71ee64d98175ff39a0abf9");
            var ConeOfColdSpell = Resources.GetBlueprint<BlueprintAbility>("e7c530f8137630f4d9d7ee1aa7b1edc0");
            var HorridWiltingSpell = Resources.GetBlueprint<BlueprintAbility>("08323922485f7e246acb3d2276515526");
            var PolarRaySpell = Resources.GetBlueprint<BlueprintAbility>("17696c144a0194c478cbe402b496cb23");
            var IceDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("IceDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RayOfSickeningSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AcidArrowSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            StinkingCloudSpell.ToReference<BlueprintAbilityReference>()
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
                            IceStormSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ConeOfColdSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            IceBodySpell.ToReference<BlueprintAbilityReference>()
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
                            PolarRaySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var IceDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("IceDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = IceDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var IceDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("IceDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WaterDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = WaterDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { WaterDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = IceDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ice Subdomain");
                bp.SetDescription("\nYou can manipulate ice and water and mist, conjure hails of ice, and transmute yourself into a frozen form.\nIcicle: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can fire an icicle from your finger, targeting any foe within 30 feet as a " +
                    "ranged {g|Encyclopedia:TouchAttack}touch attack{/g}. The icicle deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 " +
                    "+ your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nBody of Ice: At 8th level, you can transmute your body and equipment to ice for a period of time. It takes " +
                    "a standard action to take on the form of ice, and you can end the transmutation with a free action on your turn. When you take on the form of ice, you are " +
                    "immune to cold and have DR 5/—, but you take twice the normal amount of damage from fire. You can take on the form of ice for a number of rounds per day equal " +
                    "to your cleric level. The rounds need not be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var IceDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("IceDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = IceDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var IceDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("IceDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = IceDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = IceDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = IceDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ice Subdomain");
                bp.SetDescription("\nYou can manipulate ice and water and mist, conjure hails of ice, and transmute yourself into a frozen form.\nIcicle: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can fire an icicle from your finger, targeting any foe within 30 feet as a " +
                    "ranged {g|Encyclopedia:TouchAttack}touch attack{/g}. The icicle deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 " +
                    "+ your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nBody of Ice: At 8th level, you can transmute your body and equipment to ice for a period of time. It takes " +
                    "a standard action to take on the form of ice, and you can end the transmutation with a free action on your turn. When you take on the form of ice, you are " +
                    "immune to cold and have DR 5/—, but you take twice the normal amount of damage from fire. You can take on the form of ice for a number of rounds per day equal " +
                    "to your cleric level. The rounds need not be consecutive." +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: ray of sickening, acid arrow, stinking cloud, freedom of movement, ice storm, cone of cold, ice body, horrid wilting, polar ray.");
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
                    Helpers.LevelEntry(1, IceDomainBaseFeature),
                    Helpers.LevelEntry(8, IceDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(IceDomainBaseFeature, IceDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var IceDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("IceDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = IceDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = IceDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ice Subdomain");
                bp.SetDescription("\nYou can manipulate ice and water and mist, conjure hails of ice, and transmute yourself into a frozen form.\nIcicle: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can fire an icicle from your finger, targeting any foe within 30 feet as a " +
                    "ranged {g|Encyclopedia:TouchAttack}touch attack{/g}. The icicle deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 " +
                    "+ your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nBody of Ice: At 8th level, you can transmute your body and equipment to ice for a period of time. It takes " +
                    "a standard action to take on the form of ice, and you can end the transmutation with a free action on your turn. When you take on the form of ice, you are " +
                    "immune to cold and have DR 5/—, but you take twice the normal amount of damage from fire. You can take on the form of ice for a number of rounds per day equal " +
                    "to your cleric level. The rounds need not be consecutive." +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: ray of sickening, acid arrow, stinking cloud, freedom of movement, ice storm, cone of cold, ice body, horrid wilting, polar ray.");
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
                    Helpers.LevelEntry(1, IceDomainBaseFeature),
                    Helpers.LevelEntry(8, IceDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(IceDomainBaseFeature, IceDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // IceDomainSpellListFeatureDruid
            var IceDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("IceDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = IceDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // IceDomainProgressionDruid
            var IceDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("IceDomainProgressionDruid", bp => {
                bp.SetName("Ice Subdomain");
                bp.SetDescription("\nYou can manipulate ice and water and mist, conjure hails of ice, and transmute yourself into a frozen form.\nIcicle: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can fire an icicle from your finger, targeting any foe within 30 feet as a " +
                    "ranged {g|Encyclopedia:TouchAttack}touch attack{/g}. The icicle deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 " +
                    "+ your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nBody of Ice: At 8th level, you can transmute your body and equipment to ice for a period of time. It takes " +
                    "a standard action to take on the form of ice, and you can end the transmutation with a free action on your turn. When you take on the form of ice, you are " +
                    "immune to cold and have DR 5/—, but you take twice the normal amount of damage from fire. You can take on the form of ice for a number of rounds per day equal " +
                    "to your cleric level. The rounds need not be consecutive." +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: ray of sickening, acid arrow, stinking cloud, freedom of movement, ice storm, cone of cold, ice body, horrid wilting, polar ray.");
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
                    Helpers.LevelEntry(1, IceDomainBaseFeature, IceDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, IceDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            IceDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                WaterBlessingFeature.ToReference<BlueprintFeatureReference>(),
                IceDomainProgression.ToReference<BlueprintFeatureReference>(),
                IceDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            IceDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = IceDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            IceDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = IceDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(IceDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.RetiredFeatures.IsDisabled("Old Ice Subdomain")) { return; }
            DomainTools.RegisterDomain(IceDomainProgression);
            DomainTools.RegisterSecondaryDomain(IceDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(IceDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(IceDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(IceDomainProgression);
            DomainTools.RegisterTempleDomain(IceDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(IceDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(IceDomainProgression, IceDomainProgressionSecondary);
        }
    }
}
