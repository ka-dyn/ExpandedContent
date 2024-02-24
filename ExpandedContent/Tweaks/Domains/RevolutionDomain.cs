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
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Domains {
    internal class RevolutionDomain {

        public static void AddRevolutionDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var LiberationDomainBaseAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("edaac27ed85814b438ea7908b5226684");
            var LiberationDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("8ddc7f532cf2b3b4c877497856cc5b97");
            var SkillFocusDiplomacy = Resources.GetBlueprint<BlueprintFeature>("1621be43793c5bb43be55493e9c45924");

            //RevolutionDomainGreaterBuff
            var RevolutionDomainGreaterBuff = Helpers.CreateBuff("RevolutionDomainGreaterBuff", bp => {
                bp.SetName("Powerful Persuader");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusDiplomacy.m_Icon;
                bp.AddComponent<ModifyD20>(c => {
                    c.Rule = RuleType.SkillCheck;
                    c.DispellMagicCheckType = RuleDispelMagic.CheckType.None;
                    c.RollsAmount = 1;
                    c.TakeBest = true;
                    c.RollResult = new ContextValue();
                    c.AddBonus = false;
                    c.Bonus = new ContextValue();
                    c.BonusDescriptor = ModifierDescriptor.None;
                    c.WithChance = false;
                    c.Chance = new ContextValue();
                    c.RerollOnlyIfFailed = false;
                    c.RerollOnlyIfSuccess = false;
                    c.RollCondition = ModifyD20.RollConditionType.None;
                    c.ValueToCompareRoll = new ContextValue();
                    c.DispellOnRerollFinished = true;
                    c.DispellOn20 = false;
                    c.AgainstAlignment = false;
                    c.Alignment = AlignmentComponent.None;
                    c.TargetAlignment = false;
                    c.SpecificSkill = true;
                    c.Skill = new StatType[] { StatType.SkillPersuasion };
                    c.Value = new ContextValue();
                });
                bp.m_AllowNonContextActions = false;
            });
            //RevolutionDomainGreaterResource
            var RevolutionDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("RevolutionDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 8,
                    LevelStep = 2,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });
            //RevolutionDomainGreaterAbility
            var RevolutionDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("RevolutionDomainGreaterAbility", bp => {
                bp.SetName("Powerful Persuader");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusDiplomacy.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.m_Flags = 0;
                    c.School = SpellSchool.None;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = RevolutionDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { RevolutionDomainGreaterBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            Permanent = true,
                            m_Buff = RevolutionDomainGreaterBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue(),
                            DurationSeconds = 0
                        });
                });                
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                
            });
            //RevolutionDomainGreaterFeature
            var RevolutionDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("RevolutionDomainGreaterFeature", bp => {
                bp.SetName("Powerful Persuader");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusDiplomacy.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = RevolutionDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { RevolutionDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var RemoveFearSpell = Resources.GetBlueprint<BlueprintAbility>("55a037e514c0ee14a8e3ed14b47061de");
            var ScareSpell = Resources.GetBlueprint<BlueprintAbility>("08cb5f4c3b2695e44971bf5c45205df0");
            var RemoveCurseSpell = Resources.GetBlueprint<BlueprintAbility>("b48674cef2bff5e478a007cf57d8345b");
            var FreedomOfMovementSpell = Resources.GetBlueprint<BlueprintAbility>("0087fc2d64b6095478bc7b8d7d512caf");
            var BreakEnchantmentSpell = Resources.GetBlueprint<BlueprintAbility>("7792da00c85b9e042a0fdfc2b66ec9a8");
            var HeroismGreater = Resources.GetBlueprint<BlueprintAbility>("e15e5e7045fda2244b98c8f010adfe31");
            var ElementalBodyIVBaseSpell = Resources.GetBlueprint<BlueprintAbility>("376db0590f3ca4945a8b6dc16ed14975");
            var MindBlankSpell = Resources.GetBlueprint<BlueprintAbility>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var MindBlankCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("87a29febd010993419f2a4a9bee11cfc");
            var RevolutionDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("RevolutionDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RemoveFearSpell.ToReference<BlueprintAbilityReference>()
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
                            RemoveCurseSpell.ToReference<BlueprintAbilityReference>()
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
                            BreakEnchantmentSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HeroismGreater.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalBodyIVBaseSpell.ToReference<BlueprintAbilityReference>()
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
                            MindBlankCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var RevolutionDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("RevolutionDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RevolutionDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var RevolutionDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("RevolutionDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LiberationDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = LiberationDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { LiberationDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = RevolutionDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revolution Subdomain");
                bp.SetName("Revolution Subdomain");
                bp.SetDescription("\nYou are a beacon of revolution inspiring your allies to fight well and all foes to relent.\nYou have the ability to ignore impediments to your {g|Encyclopedia:Mobility}mobility{/g}. " +
                    "For a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain, you can move normally regardless of magical effects that " +
                    "impede movement, as if you were affected by freedom of movement. This effect occurs automatically as soon as it applies. These rounds do not need to be consecutive.\nPowerful Persuader: " +
                    "At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var RevolutionDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("RevolutionDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = RevolutionDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var RevolutionDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("RevolutionDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RevolutionDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RevolutionDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revolution Subdomain");
                bp.SetDescription("\nYou are a beacon of revolution inspiring your allies to fight well and all foes to relent.\nYou have the ability to ignore impediments to your {g|Encyclopedia:Mobility}mobility{/g}. " +
                    "For a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain, you can move normally regardless of magical effects that " +
                    "impede movement, as if you were affected by freedom of movement. This effect occurs automatically as soon as it applies. These rounds do not need to be consecutive.\nPowerful Persuader: " +
                    "At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: remove fear, scare, " +
                    "remove curse, freedom of movement, break enchantment, greater heroism, elemental body IV, mind blank, communal mind blank.");
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
                    Helpers.LevelEntry(1, RevolutionDomainBaseFeature),
                    Helpers.LevelEntry(8, RevolutionDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RevolutionDomainBaseFeature, RevolutionDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var RevolutionDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("RevolutionDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revolution Subdomain");
                bp.SetDescription("\nYou are a beacon of revolution inspiring your allies to fight well and all foes to relent.\nYou have the ability to ignore impediments to your {g|Encyclopedia:Mobility}mobility{/g}. " +
                    "For a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain, you can move normally regardless of magical effects that " +
                    "impede movement, as if you were affected by freedom of movement. This effect occurs automatically as soon as it applies. These rounds do not need to be consecutive.\nPowerful Persuader: " +
                    "At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: remove fear, scare, " +
                    "remove curse, freedom of movement, break enchantment, greater heroism, elemental body IV, mind blank, communal mind blank.");
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
                    Helpers.LevelEntry(1, RevolutionDomainBaseFeature),
                    Helpers.LevelEntry(8, RevolutionDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RevolutionDomainBaseFeature, RevolutionDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var LiberationDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintActivatableAbility>("a978ae4de4aa4387bf1c2901e44588aa");
            var LiberationDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("e3002cf70dd64efd9eb8a29ac954ec0e");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var RevolutionDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("RevolutionDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var RevolutionDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("RevolutionDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 10,
                    LevelStep = 2,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });

            var RevolutionDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("RevolutionDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Powerful Persuader");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusDiplomacy.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.m_Flags = 0;
                    c.School = SpellSchool.None;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = RevolutionDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { RevolutionDomainGreaterBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            Permanent = true,
                            m_Buff = RevolutionDomainGreaterBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue(),
                            DurationSeconds = 0
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();

            });

            var RevolutionDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("RevolutionDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Powerful Persuader");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusDiplomacy.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = RevolutionDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { RevolutionDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var RevolutionDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("RevolutionDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LiberationDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = LiberationDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { LiberationDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = RevolutionDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revolution Subdomain");
                bp.SetName("Revolution Subdomain");
                bp.SetDescription("\nYou are a beacon of revolution inspiring your allies to fight well and all foes to relent.\nYou have the ability to ignore impediments to your {g|Encyclopedia:Mobility}mobility{/g}. " +
                    "For a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain, you can move normally regardless of magical effects that " +
                    "impede movement, as if you were affected by freedom of movement. This effect occurs automatically as soon as it applies. These rounds do not need to be consecutive.\nPowerful Persuader: " +
                    "At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.IsClassFeature = true;
            });

            var RevolutionDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("RevolutionDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RevolutionDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Revolution Subdomain");
                bp.SetDescription("\nYou are a beacon of revolution inspiring your allies to fight well and all foes to relent.\nYou have the ability to ignore impediments to your {g|Encyclopedia:Mobility}mobility{/g}. " +
                    "For a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain, you can move normally regardless of magical effects that " +
                    "impede movement, as if you were affected by freedom of movement. This effect occurs automatically as soon as it applies. These rounds do not need to be consecutive.\nPowerful Persuader: " +
                    "At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Persuasion}persuasion{/g} or intimidate  skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.\nDomain {g|Encyclopedia:Spell}Spells{/g}: remove fear, scare, " +
                    "remove curse, freedom of movement, break enchantment, greater heroism, elemental body IV, mind blank, communal mind blank.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RevolutionDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, RevolutionDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RevolutionDomainBaseFeatureSeparatist, RevolutionDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            RevolutionDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                RevolutionDomainProgression.ToReference<BlueprintFeatureReference>(),
                RevolutionDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            RevolutionDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RevolutionDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            RevolutionDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RevolutionDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            RevolutionDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RevolutionDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            RevolutionDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RevolutionDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            RevolutionDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = RevolutionDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });

            if (ModSettings.AddedContent.Domains.IsDisabled("Revolution Subdomain")) { return; }
            DomainTools.RegisterDomain(RevolutionDomainProgression);
            DomainTools.RegisterSecondaryDomain(RevolutionDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(RevolutionDomainProgression);
            DomainTools.RegisterTempleDomain(RevolutionDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(RevolutionDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(RevolutionDomainProgression, RevolutionDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(RevolutionDomainProgressionSeparatist);

        }
    }
}
