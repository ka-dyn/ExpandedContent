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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Domains {
    internal class InsectDomain {

        public static void AddInsectDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var AnimalBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("9d991f8374c3def4cb4a6287f370814d");
            var WildShapeWolfBuff = Resources.GetBlueprint<BlueprintBuff>("470fb1a22e7eb5849999f1101eacc5dc"); //Animal
            var OracleRevelationWoodArmorIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleRevelationWoodArmor.jpg"); //Might change this



            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var DomainAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("125af359f8bc9a145968b5d8fd8159b8");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("65af7290b4efd5f418132141aaa36c1b");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");



            var AnimalCompanionEmpty = Resources.GetBlueprintReference<BlueprintFeatureReference>("472091361cf118049a2b4339c4ea836a");
            var AnimalCompanionFeatureCentipede = Resources.GetBlueprintReference<BlueprintFeatureReference>("f9ef7717531f5914a9b6ecacfad63f46");
            var CompanionGiantFlyFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionGiantFlyFeature");
            var CompanionWebSpiderFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionWebSpiderFeature");


            var InsectDomainAnimalCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("InsectDomainAnimalCompanionSelection", bp => {
                bp.SetName("Insect Subdomain Vermin Companion");
                bp.SetDescription("At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)");
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DomainAnimalCompanionProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AnimalCompanionFeatureCentipede, CompanionGiantFlyFeature, CompanionWebSpiderFeature, AnimalCompanionEmpty);
            });

            var CheetahSprintIcon = AssetLoader.LoadInternal("Skills", "Icon_CheetahSprint.jpg");

            var InsectDomainBaseBuff = Helpers.CreateBuff("InsectDomainBaseBuff", bp => {
                bp.SetName("Exoskeleton");
                bp.SetDescription("As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 5;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 1,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
            });

            var InsectDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("InsectDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var InsectDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("InsectDomainBaseAbility", bp => {
                bp.SetName("Exoskeleton");
                bp.SetDescription("As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = InsectDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = InsectDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Extend;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("InsectDomainBaseAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var MagicFangSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("403cf599412299a4f9d5d925c7b9fb33");
            var HoldAnimalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("41bab342089c0254ca222eb918e98cd4");
            var VerminShapeIParentAbility = Resources.GetModBlueprint<BlueprintAbility>("VerminShapeIParentAbility");
            var VerminShapeIIAbility = Resources.GetModBlueprint<BlueprintAbility>("VerminShapeIIAbility");
            var CapeOfWaspsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e418c20c8ce362943a8025d82c865c1c");
            var PlagueStormSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("82a5b848c05e3f342b893dedb1f9b446");
            var SummonNaturesAllyVIISpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("051b979e7d7f8ec41b9fa35d04746b33");
            var SummonNaturesAllyVIIISpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ea78c04f0bd13d049a1cce5daf8d83e0");
            var ShapechangeSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("22b9044aa229815429d57d0a30e4b739");
            var InsectDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("InsectDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicFangSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HoldAnimalSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            VerminShapeIParentAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            VerminShapeIIAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CapeOfWaspsSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PlagueStormSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonNaturesAllyVIISpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonNaturesAllyVIIISpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShapechangeSpell
                        }
                    },
                };
            });
            var InsectDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("InsectDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = InsectDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var InsectDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("InsectDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { InsectDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = InsectDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { InsectDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = InsectDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insect Subdomain");
                bp.SetDescription("You command the minds of vermin. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}." +
                    "\nExoskeleton: As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nVermin Companion: At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var InsectDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("InsectDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = InsectDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // Main Blueprint
            var InsectDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("InsectDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = InsectDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = InsectDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insect Subdomain");
                bp.SetDescription("You command the minds of vermin. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}." +
                    "\nExoskeleton: As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nVermin Companion: At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)" +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, vermin shape I, vermin shape II, cape of wasps, plague storm, summon nature's ally VII, " +
                    "summon nature's ally VIII, shapechange.");
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
                    Helpers.LevelEntry(1, InsectDomainBaseFeature),
                    Helpers.LevelEntry(4, InsectDomainAnimalCompanionSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(InsectDomainBaseFeature, InsectDomainAnimalCompanionSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var InsectDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("InsectDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insect Subdomain");
                bp.SetDescription("You command the minds of vermin. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}." +
                    "\nExoskeleton: As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nVermin Companion: At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)" +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, vermin shape I, vermin shape II, cape of wasps, plague storm, summon nature's ally VII, " +
                    "summon nature's ally VIII, shapechange.");
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
                    Helpers.LevelEntry(1, InsectDomainBaseFeature),
                    Helpers.LevelEntry(4, InsectDomainAnimalCompanionSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(InsectDomainBaseFeature, InsectDomainAnimalCompanionSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // InsectDomainSpellListFeatureDruid
            var InsectDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("InsectDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = InsectDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // InsectDomainProgressionDruid
            var InsectDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("InsectDomainProgressionDruid", bp => {
                bp.SetName("Insect Subdomain");
                bp.SetDescription("You command the minds of vermin. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}." +
                    "\nExoskeleton: As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nVermin Companion: At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)" +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, vermin shape I, vermin shape II, cape of wasps, plague storm, summon nature's ally VII, " +
                    "summon nature's ally VIII, shapechange.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 5;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 6;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 7;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 8;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 9;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 10;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 11;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 12;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 13;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 14;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 15;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 16;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 17;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 18;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 19;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                    c.Level = 20;
                });

                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, InsectDomainBaseFeature,InsectDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(4, InsectDomainAnimalCompanionSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(InsectDomainBaseFeature, InsectDomainAnimalCompanionSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var ProtectionDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("7eb39ba8115a422bb69c702cc20ca58a");

            var InsectDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("InsectDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var InsectDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("InsectDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var SeparatistWithDruidAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistWithDruidAsIsProperty");

            var InsectDomainBaseBuffSeparatist = Helpers.CreateBuff("InsectDomainBaseBuffSeparatist", bp => {
                bp.SetName("Exoskeleton");
                bp.SetDescription("As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_StartLevel = -2;
                    c.m_StepLevel = 5;
                    c.m_CustomProperty = SeparatistWithDruidAsIsProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_CustomProperty = SeparatistWithDruidAsIsProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 1,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
            });

            var InsectDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("InsectDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Exoskeleton");
                bp.SetDescription("As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = InsectDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = InsectDomainBaseBuffSeparatist.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Extend;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("InsectDomainBaseAbilitySeparatist.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var InsectDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("InsectDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { InsectDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = InsectDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { InsectDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = InsectDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insect Subdomain");
                bp.SetDescription("You command the minds of vermin. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}." +
                    "\nExoskeleton: As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nVermin Companion: At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)");
                bp.IsClassFeature = true;
            });

            var DomainAnimalCompanionProgressionSeparatist = Resources.GetBlueprint<BlueprintProgression>("c7a3ed56f239433fb50dfed4c07e8845");
            var InsectDomainAnimalCompanionSelectionSeparatist = Helpers.CreateBlueprint<BlueprintFeatureSelection>("InsectDomainAnimalCompanionSelectionSeparatist", bp => {
                bp.SetName("Insect Subdomain Vermin Companion");
                bp.SetDescription("At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)");
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DomainAnimalCompanionProgressionSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AnimalCompanionFeatureCentipede, CompanionGiantFlyFeature, CompanionWebSpiderFeature, AnimalCompanionEmpty);
            });

            var InsectDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("InsectDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = InsectDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Insect Subdomain");
                bp.SetDescription("You command the minds of vermin. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}." +
                    "\nExoskeleton: As a swift action, you can grow an exoskeleton that grants you a +1 enhancement bonus to your natural armor and 1d4 temporary hit points + 1 " +
                    "for every 2 cleric levels you have. The natural armor bonus increases by 1 for every 5 cleric levels you have. The exoskeleton retracts after 1 round, ending its benefits. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nVermin Companion: At 4th level, you gain the service of an vermin animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)" +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, vermin shape I, vermin shape II, cape of wasps, plague storm, summon nature's ally VII, " +
                    "summon nature's ally VIII, shapechange.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] { };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, InsectDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(6, InsectDomainAnimalCompanionSelectionSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(InsectDomainBaseFeatureSeparatist, InsectDomainAnimalCompanionSelectionSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            InsectDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                InsectDomainProgression.ToReference<BlueprintFeatureReference>(),
                InsectDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            InsectDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsectDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            InsectDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsectDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            InsectDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsectDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            InsectDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsectDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            InsectDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = InsectDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Insect Subdomain")) { return; }
            DomainTools.RegisterDomain(InsectDomainProgression);
            DomainTools.RegisterSecondaryDomain(InsectDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(InsectDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(InsectDomainProgression);
            DomainTools.RegisterTempleDomain(InsectDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(InsectDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(InsectDomainProgression, InsectDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(InsectDomainProgressionSeparatist);

        }
    }
}
