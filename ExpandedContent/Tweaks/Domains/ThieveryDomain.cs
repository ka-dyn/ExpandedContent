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
    internal class ThieveryDomain {

        public static void AddThieveryDomain() {

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
            var TrickeryDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("ee7eb5b9c644a0347b36eec653d3dfcb");
            var TrickeryDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("148c9ad7e47f4284b9c3686bb440c08c");
            var SkillFocusThievery = Resources.GetBlueprint<BlueprintFeature>("7feda1b98f0c169418aa9af78a85953b");

            //ThieveryDomainGreaterBuff
            var ThieveryDomainGreaterBuff = Helpers.CreateBuff("ThieveryDomainGreaterBuff", bp => {
                bp.SetName("Thief of the Gods");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusThievery.m_Icon;
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
                    c.Skill = new StatType[] { StatType.SkillThievery };
                    c.Value = new ContextValue();
                });
                bp.m_AllowNonContextActions = false;
            });
            //ThieveryDomainGreaterResource
            var ThieveryDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ThieveryDomainGreaterResource", bp => {
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
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 8,
                    LevelStep = 2,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });
            //ThieveryDomainGreaterAbility
            var ThieveryDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("ThieveryDomainGreaterAbility", bp => {
                bp.SetName("Thief of the Gods");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusThievery.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.m_Flags = 0;
                    c.School = SpellSchool.None;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ThieveryDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { ThieveryDomainGreaterBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            Permanent = true,
                            m_Buff = ThieveryDomainGreaterBuff.ToReference<BlueprintBuffReference>(),
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
            //ThieveryDomainGreaterFeature
            var ThieveryDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("ThieveryDomainGreaterFeature", bp => {
                bp.SetName("Thief of the Gods");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusThievery.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ThieveryDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ThieveryDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var SleepSpell = Resources.GetBlueprint<BlueprintAbility>("bb7ecad2d3d2c8247a38f44855c99061");
            var InvisibilitySpell = Resources.GetBlueprint<BlueprintAbility>("89940cde01689fb46946b2f8cd7b66b7");
            var SeeInvisibilityCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("1a045f845778dc54db1c2be33a8c3c0a");
            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var MindFogSpell = Resources.GetBlueprint<BlueprintAbility>("eabf94e4edc6e714cabd96aa69f8b207");
            var CloakofDreamsSpell = Resources.GetBlueprint<BlueprintAbility>("7f71a70d822af94458dc1a235507e972");
            var WalkThroughSpaceSpell = Resources.GetBlueprint<BlueprintAbility>("368d7cf2fb69d8a46be5a650f5a5a173");
            var InvisibilityMassSpell = Resources.GetBlueprint<BlueprintAbility>("98310a099009bbd4dbdf66bcef58b4cd");
            var WeirdSpell = Resources.GetBlueprint<BlueprintAbility>("870af83be6572594d84d276d7fc583e0");
            var ThieveryDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("ThieveryDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SleepSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            InvisibilitySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SeeInvisibilityCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ConfusionSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MindFogSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CloakofDreamsSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            WalkThroughSpaceSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            InvisibilityMassSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            WeirdSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var ThieveryDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("ThieveryDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ThieveryDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var ThieveryDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ThieveryDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TrickeryDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = TrickeryDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { TrickeryDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ThieveryDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillThievery;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Thievery Subdomain");
                bp.SetDescription("\nYou are a master of thievery, both in the name of your god and yourself. {g|Encyclopedia:Trickery}Trickery{/g} and {g|Encyclopedia:Stealth}Stealth{/g} are class " +
                    "{g|Encyclopedia:Skills}skills{/g}.\nCopycat: You can create an illusory double of yourself as a {g|Encyclopedia:Move_Action}move action{/g}. This double functions as a single mirror " +
                    "image, and lasts for a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain, or until the illusory duplicate is " +
                    "dispelled or destroyed. You can have no more than one copycat at a time. This ability does not stack with the mirror image {g|Encyclopedia:Spell}spell{/g}. You can use this ability " +
                    "a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nThief of the Gods: At 8th level, you can make grant yourself the ability to roll twice on any " +
                    "{g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used then dispels itself. You can use this ability once per day at 8th level, plus one additional " +
                    "time per day for every 2 levels beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var ThieveryDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("ThieveryDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = ThieveryDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var ThieveryDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("ThieveryDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ThieveryDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ThieveryDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Thievery Subdomain");
                bp.SetDescription("\nYou are a master of thievery, both in the name of your god and yourself. {g|Encyclopedia:Trickery}Trickery{/g} and {g|Encyclopedia:Stealth}Stealth{/g} are class " +
                    "{g|Encyclopedia:Skills}skills{/g}.\nCopycat: You can create an illusory double of yourself as a {g|Encyclopedia:Move_Action}move action{/g}. This double functions as a single mirror " +
                    "image, and lasts for a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain, or until the illusory duplicate is " +
                    "dispelled or destroyed. You can have no more than one copycat at a time. This ability does not stack with the mirror image {g|Encyclopedia:Spell}spell{/g}. You can use this ability " +
                    "a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nThief of the Gods: At 8th level, you can make grant yourself the ability to roll twice on any " +
                    "{g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used then dispels itself. You can use this ability once per day at 8th level, plus one additional " +
                    "time per day for every 2 levels beyond 8th.\nDomain Spells: sleep, invisibility, see invisibility, confusion, mind fog, cloak of dreams, walk through space, mass invisibility, weird.");
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
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };                
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ThieveryDomainBaseFeature),
                    Helpers.LevelEntry(8, ThieveryDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ThieveryDomainBaseFeature, ThieveryDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var ThieveryDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("ThieveryDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Thievery Subdomain");
                bp.SetDescription("\nYou are a master of thievery, both in the name of your god and yourself. {g|Encyclopedia:Trickery}Trickery{/g} and {g|Encyclopedia:Stealth}Stealth{/g} are class " +
                    "{g|Encyclopedia:Skills}skills{/g}.\nCopycat: You can create an illusory double of yourself as a {g|Encyclopedia:Move_Action}move action{/g}. This double functions as a single mirror " +
                    "image, and lasts for a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain, or until the illusory duplicate is " +
                    "dispelled or destroyed. You can have no more than one copycat at a time. This ability does not stack with the mirror image {g|Encyclopedia:Spell}spell{/g}. You can use this ability " +
                    "a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nThief of the Gods: At 8th level, you can make grant yourself the ability to roll twice on any " +
                    "{g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used then dispels itself. You can use this ability once per day at 8th level, plus one additional " +
                    "time per day for every 2 levels beyond 8th.\nDomain Spells: sleep, invisibility, see invisibility, confusion, mind fog, cloak of dreams, walk through space, mass invisibility, weird.");
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
                    },
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
                    Helpers.LevelEntry(1, ThieveryDomainBaseFeature),
                    Helpers.LevelEntry(8, ThieveryDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ThieveryDomainBaseFeature, ThieveryDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var TrickeryDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("74eae22022cd4f619e889abae9f5e8de");
            var TrickeryDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("0274f198633c4891b5007f294bf1de1b");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var ThieveryDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ThieveryDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var ThieveryDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("ThieveryDomainGreaterResourceSeparatist", bp => {
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
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 10,
                    LevelStep = 2,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });

            var ThieveryDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("ThieveryDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Thief of the Gods");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusThievery.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.m_Flags = 0;
                    c.School = SpellSchool.None;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ThieveryDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { ThieveryDomainGreaterBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            Permanent = true,
                            m_Buff = ThieveryDomainGreaterBuff.ToReference<BlueprintBuffReference>(),
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

            var ThieveryDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ThieveryDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Thief of the Gods");
                bp.SetDescription("At 8th level, you can make grant yourself the ability to roll twice on any {g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used " +
                    "then dispels itself. You can use this ability once per day at 8th level, plus one additional time per day for every 2 levels beyond 8th.");
                bp.m_Icon = SkillFocusThievery.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ThieveryDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ThieveryDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var ThieveryDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ThieveryDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TrickeryDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = TrickeryDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { TrickeryDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ThieveryDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillThievery;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Thievery Subdomain");
                bp.SetDescription("\nYou are a master of thievery, both in the name of your god and yourself. {g|Encyclopedia:Trickery}Trickery{/g} and {g|Encyclopedia:Stealth}Stealth{/g} are class " +
                    "{g|Encyclopedia:Skills}skills{/g}.\nCopycat: You can create an illusory double of yourself as a {g|Encyclopedia:Move_Action}move action{/g}. This double functions as a single mirror " +
                    "image, and lasts for a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain, or until the illusory duplicate is " +
                    "dispelled or destroyed. You can have no more than one copycat at a time. This ability does not stack with the mirror image {g|Encyclopedia:Spell}spell{/g}. You can use this ability " +
                    "a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nThief of the Gods: At 8th level, you can make grant yourself the ability to roll twice on any " +
                    "{g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used then dispels itself. You can use this ability once per day at 8th level, plus one additional " +
                    "time per day for every 2 levels beyond 8th.");
                bp.IsClassFeature = true;
            });

            var ThieveryDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("ThieveryDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ThieveryDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Thievery Subdomain");
                bp.SetDescription("\nYou are a master of thievery, both in the name of your god and yourself. {g|Encyclopedia:Trickery}Trickery{/g} and {g|Encyclopedia:Stealth}Stealth{/g} are class " +
                    "{g|Encyclopedia:Skills}skills{/g}.\nCopycat: You can create an illusory double of yourself as a {g|Encyclopedia:Move_Action}move action{/g}. This double functions as a single mirror " +
                    "image, and lasts for a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain, or until the illusory duplicate is " +
                    "dispelled or destroyed. You can have no more than one copycat at a time. This ability does not stack with the mirror image {g|Encyclopedia:Spell}spell{/g}. You can use this ability " +
                    "a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nThief of the Gods: At 8th level, you can make grant yourself the ability to roll twice on any " +
                    "{g|Encyclopedia:Trickery}trickery{/g} skill check, this abilities effect lasts until used then dispels itself. You can use this ability once per day at 8th level, plus one additional " +
                    "time per day for every 2 levels beyond 8th.\nDomain Spells: sleep, invisibility, see invisibility, confusion, mind fog, cloak of dreams, walk through space, mass invisibility, weird.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ThieveryDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, ThieveryDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ThieveryDomainBaseFeatureSeparatist, ThieveryDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            ThieveryDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ThieveryDomainProgression.ToReference<BlueprintFeatureReference>(),
                ThieveryDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            ThieveryDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ThieveryDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ThieveryDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ThieveryDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ThieveryDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ThieveryDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ThieveryDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ThieveryDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ThieveryDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ThieveryDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });

            if (ModSettings.AddedContent.Domains.IsDisabled("Thievery Subdomain")) { return; }
            DomainTools.RegisterDomain(ThieveryDomainProgression);
            DomainTools.RegisterSecondaryDomain(ThieveryDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(ThieveryDomainProgression);
            DomainTools.RegisterTempleDomain(ThieveryDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(ThieveryDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(ThieveryDomainProgression, ThieveryDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(ThieveryDomainProgressionSeparatist);

        }
    }
}
