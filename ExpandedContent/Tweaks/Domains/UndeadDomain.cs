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
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class UndeadDomain {

        public static void AddUndeadDomain() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var DeathDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("b0acce833384b9b428f32517163c9117");
            var SiphonLife = Resources.GetBlueprint<BlueprintAbility>("7bd52a86498c7854ebe99bc3cfb85bfe");
            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");

            var UndeadDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("UndeadDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });

            var UndeadDomainBaseAbilityBuff = Helpers.CreateBuff("UndeadDomainBaseAbilityBuff", bp => {
                bp.SetName("Death’s Kiss");
                bp.SetDescription("You are treated as undead for the purposes of effects that heal or cause damage based on positive and negative energy");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var UndeadDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("UndeadDomainBaseAbility", bp => {
                bp.SetName("Death’s Kiss");
                bp.SetDescription("You can cause a creature to take on some of the traits of the undead with a melee touch attack. Touched creatures are treated as undead for " +
                    "the purposes of effects that heal or cause damage based on positive and negative energy. This effect lasts for a number of rounds equal to 1/2 your cleric " +
                    "level (minimum 1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = UndeadDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });                
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = UndeadDomainBaseAbilityBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                m_IsExtendable = true,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    Property = UnitProperty.None
                                }
                            }
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.m_Icon = SiphonLife.m_Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.AvailableMetamagic = Metamagic.Reach | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var CauseFearSpell = Resources.GetBlueprint<BlueprintAbility>("bd81a3931aa285a4f9844585b5d97e51");
            var FrigidTouchSpell = Resources.GetBlueprint<BlueprintAbility>("c83447189aabc72489164dfc246f3a36");
            var BestowCurseSpell = Resources.GetBlueprint<BlueprintAbility>("989ab5c44240907489aba0a8568d0603");
            var EnervationSpell = Resources.GetBlueprint<BlueprintAbility>("f34fb78eaaec141469079af124bcfa0f");
            var SlayLivingSpell = Resources.GetBlueprint<BlueprintAbility>("4fbd47525382517419c66fb548fe9a67");
            var CircleOfDeathSpell = Resources.GetBlueprint<BlueprintAbility>("a89dcbbab8f40e44e920cc60636097cf");
            var DestructionSpell = Resources.GetBlueprint<BlueprintAbility>("3b646e1db3403b940bf620e01d2ce0c7");
            var HorridWiltingSpell = Resources.GetBlueprint<BlueprintAbility>("08323922485f7e246acb3d2276515526");
            var EnergyDrainSpell = Resources.GetBlueprint<BlueprintAbility>("37302f72b06ced1408bf5bb965766d46");
            var UndeadDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("UndeadDomainSpellList", bp => {
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
                            FrigidTouchSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BestowCurseSpell.ToReference<BlueprintAbilityReference>()
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
                            CircleOfDeathSpell.ToReference<BlueprintAbilityReference>()
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
                            EnergyDrainSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var UndeadDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("UndeadDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = UndeadDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var UndeadDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("UndeadDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { UndeadDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = UndeadDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { UndeadDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = UndeadDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Undead Subdomain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} can allow others to embrace undeath, willfully or not.\nDeath’s Kiss: You can cause a creature to take on some " +
                    "of the traits of the undead with a melee touch attack. Touched creatures are treated as undead for the purposes of effects that heal or cause damage based on positive " +
                    "and negative energy. This effect lasts for a number of rounds equal to 1/2 your cleric level (minimum 1). You can use this ability a number of times per day equal to 3 + " +
                    "your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\n{g|Encyclopedia:Injury_Death}Death{/g}'s Embrace: At 8th level, you {g|Encyclopedia:Healing}heal{/g} damage instead " +
                    "of taking damage from channeled negative energy. If the channeled negative energy targets undead, you heal {g|Encyclopedia:HP}hit points{/g} just like undead in the area." +
                    "\nIf you are undead, then you instead do not take {g|Encyclopedia:Energy_Damage}damage from positive energy{/g}.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var UndeadDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("UndeadDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = UndeadDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var UndeadDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("UndeadDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = UndeadDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = UndeadDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = UndeadDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Undead Subdomain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} can allow others to embrace undeath, willfully or not.\nDeath’s Kiss: You can cause a creature to take on some " +
                    "of the traits of the undead with a melee touch attack. Touched creatures are treated as undead for the purposes of effects that heal or cause damage based on positive " +
                    "and negative energy. This effect lasts for a number of rounds equal to 1/2 your cleric level (minimum 1). You can use this ability a number of times per day equal to 3 + " +
                    "your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\n{g|Encyclopedia:Injury_Death}Death{/g}'s Embrace: At 8th level, you {g|Encyclopedia:Healing}heal{/g} damage instead " +
                    "of taking damage from channeled negative energy. If the channeled negative energy targets undead, you heal {g|Encyclopedia:HP}hit points{/g} just like undead in the area." +
                    "\nIf you are undead, then you instead do not take {g|Encyclopedia:Energy_Damage}damage from positive energy{/g}.\nDomain {g|Encyclopedia:Spell}Spells{/g}: cause fear, " +
                    "frigid touch, bestow curse, enervation, slay living, circle of death, destruction, horrid wilting, energy drain.");
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
                    Helpers.LevelEntry(1, UndeadDomainBaseFeature),
                    Helpers.LevelEntry(8, DeathDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(UndeadDomainBaseFeature, DeathDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var UndeadDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("UndeadDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = UndeadDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = UndeadDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Undead Subdomain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} can allow others to embrace undeath, willfully or not.\nDeath’s Kiss: You can cause a creature to take on some " +
                    "of the traits of the undead with a melee touch attack. Touched creatures are treated as undead for the purposes of effects that heal or cause damage based on positive " +
                    "and negative energy. This effect lasts for a number of rounds equal to 1/2 your cleric level (minimum 1). You can use this ability a number of times per day equal to 3 + " +
                    "your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\n{g|Encyclopedia:Injury_Death}Death{/g}'s Embrace: At 8th level, you {g|Encyclopedia:Healing}heal{/g} damage instead " +
                    "of taking damage from channeled negative energy. If the channeled negative energy targets undead, you heal {g|Encyclopedia:HP}hit points{/g} just like undead in the area." +
                    "\nIf you are undead, then you instead do not take {g|Encyclopedia:Energy_Damage}damage from positive energy{/g}.\nDomain {g|Encyclopedia:Spell}Spells{/g}: cause fear, " +
                    "frigid touch, bestow curse, enervation, slay living, circle of death, destruction, horrid wilting, energy drain.");
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
                    Helpers.LevelEntry(1, UndeadDomainBaseFeature),
                    Helpers.LevelEntry(8, DeathDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(UndeadDomainBaseFeature, DeathDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // UndeadDomainSpellListFeatureDruid
            var UndeadDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("UndeadDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = UndeadDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // UndeadDomainProgressionDruid
            var UndeadDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("UndeadDomainProgressionDruid", bp => {
                bp.SetName("Undead Subdomain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} can allow others to embrace undeath, willfully or not.\nDeath’s Kiss: You can cause a creature to take on some " +
                    "of the traits of the undead with a melee touch attack. Touched creatures are treated as undead for the purposes of effects that heal or cause damage based on positive " +
                    "and negative energy. This effect lasts for a number of rounds equal to 1/2 your cleric level (minimum 1). You can use this ability a number of times per day equal to 3 + " +
                    "your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\n{g|Encyclopedia:Injury_Death}Death{/g}'s Embrace: At 8th level, you {g|Encyclopedia:Healing}heal{/g} damage instead " +
                    "of taking damage from channeled negative energy. If the channeled negative energy targets undead, you heal {g|Encyclopedia:HP}hit points{/g} just like undead in the area." +
                    "\nIf you are undead, then you instead do not take {g|Encyclopedia:Energy_Damage}damage from positive energy{/g}.\nDomain {g|Encyclopedia:Spell}Spells{/g}: cause fear, " +
                    "frigid touch, bestow curse, enervation, slay living, circle of death, destruction, horrid wilting, energy drain.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.BlightDruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, UndeadDomainBaseFeature, UndeadDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, DeathDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            UndeadDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                UndeadDomainProgression.ToReference<BlueprintFeatureReference>(),
                UndeadDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            UndeadDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = UndeadDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            UndeadDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = UndeadDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(UndeadDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Undead Subdomain")) { return; }
            DomainTools.RegisterDomain(UndeadDomainProgression);
            DomainTools.RegisterSecondaryDomain(UndeadDomainProgressionSecondary);
            DomainTools.RegisterBlightDruidDomain(UndeadDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(UndeadDomainProgression);
            DomainTools.RegisterTempleDomain(UndeadDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(UndeadDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(UndeadDomainProgression, UndeadDomainProgressionSecondary);
        }
    }
}
