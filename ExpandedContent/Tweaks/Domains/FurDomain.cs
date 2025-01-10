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
    internal class FurDomain {

        public static void AddFurDomain() {

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



            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var DomainAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("125af359f8bc9a145968b5d8fd8159b8");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("65af7290b4efd5f418132141aaa36c1b");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");



            var AnimalCompanionEmpty = Resources.GetBlueprintReference<BlueprintFeatureReference>("472091361cf118049a2b4339c4ea836a");
            var AnimalCompanionFeatureBear= Resources.GetBlueprintReference<BlueprintFeatureReference>("f6f1cdcc404f10c4493dc1e51208fd6f");
            var AnimalCompanionFeatureBoar= Resources.GetBlueprintReference<BlueprintFeatureReference>("afb817d80b843cc4fa7b12289e6ebe3d");
            var AnimalCompanionFeatureDog= Resources.GetBlueprintReference<BlueprintFeatureReference>("f894e003d31461f48a02f5caec4e3359");
            var AnimalCompanionFeatureElk= Resources.GetBlueprintReference<BlueprintFeatureReference>("aa92fea676be33d4dafd176d699d7996");
            var AnimalCompanionFeatureHorse= Resources.GetBlueprintReference<BlueprintFeatureReference>("9dc58b5901677c942854019d1dd98374");
            var AnimalCompanionFeatureLeopard= Resources.GetBlueprintReference<BlueprintFeatureReference>("2ee2ba60850dd064e8b98bf5c2c946ba");
            var AnimalCompanionFeatureMammoth= Resources.GetBlueprintReference<BlueprintFeatureReference>("6adc3aab7cde56b40aa189a797254271");
            var AnimalCompanionFeatureSmilodon= Resources.GetBlueprintReference<BlueprintFeatureReference>("126712ef923ab204983d6f107629c895");
            var AnimalCompanionFeatureWolf= Resources.GetBlueprintReference<BlueprintFeatureReference>("67a9dc42b15d0954ca4689b13e8dedea");
            var AnimalCompanionFeaturePreorderHorse= Resources.GetBlueprintReference<BlueprintFeatureReference>("bfeb9be0a3c9420b8b2beecc8171029c");
            var AnimalCompanionFeaturePreorderSmilodon= Resources.GetBlueprintReference<BlueprintFeatureReference>("44f4d77689434e07a5a44dcb65b25f71");
            var CompanionWolverineFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionWolverineFeature");


            var FurDomainAnimalCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("FurDomainAnimalCompanionSelection", bp => {
                bp.SetName("Fur Subdomain Animal Companion");
                bp.SetDescription("At 4th level, you gain the service of an mammal animal companion. Your effective druid level for this animal companion is equal to your level in the class " +
                    "that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use their druid level – 3 to determine the abilities of " +
                    "their animal companions.)");
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
                bp.AddFeatures(AnimalCompanionFeatureBear, AnimalCompanionFeatureBoar, AnimalCompanionFeatureDog, AnimalCompanionFeatureElk, AnimalCompanionFeatureHorse, 
                    AnimalCompanionFeatureLeopard, AnimalCompanionFeatureMammoth, AnimalCompanionFeatureSmilodon, AnimalCompanionFeatureWolf, AnimalCompanionFeaturePreorderHorse, 
                    AnimalCompanionFeaturePreorderSmilodon,CompanionWolverineFeature, AnimalCompanionEmpty);
            });







            var CheetahSprintIcon = AssetLoader.LoadInternal("Skills", "Icon_CheetahSprint.jpg");

            var FurDomainBaseBuff = Helpers.CreateBuff("FurDomainBaseBuff", bp => {
                bp.SetName("Predator’s Grace");
                bp.SetDescription("You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric " +
                    "levels you possess. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = CheetahSprintIcon;
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 0;
                    c.ContextBonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });                
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 4, ProgressionValue = 10 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 9, ProgressionValue = 15 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 20 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 19, ProgressionValue = 25 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 24, ProgressionValue = 30 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 29, ProgressionValue = 35 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 34, ProgressionValue = 40 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 39, ProgressionValue = 45 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 50 }
                    };
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
                bp.FxOnStart = new PrefabLink() { AssetId = "91ef30ab58fa0d3449d4d2ccc20cb0f8" }; //Haste FX
            });

            var FurDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("FurDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });


            var FurDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("FurDomainBaseAbility", bp => {
                bp.SetName("Predator’s Grace");
                bp.SetDescription("You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric " +
                    "levels you possess. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = FurDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FurDomainBaseBuff.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = WildShapeWolfBuff.m_Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("FurDomainBaseAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var MagicFangSpell = Resources.GetBlueprint<BlueprintAbility>("403cf599412299a4f9d5d925c7b9fb33");
            var HoldAnimalSpell = Resources.GetBlueprint<BlueprintAbility>("41bab342089c0254ca222eb918e98cd4");
            var BeastShapeISpell = Resources.GetBlueprint<BlueprintAbility>("61a7ed778dd93f344a5dacdbad324cc9");
            var SummonNaturesAllyIVSpell = Resources.GetBlueprint<BlueprintAbility>("c83db50513abdf74ca103651931fac4b");
            var BeastShapeIIISpell = Resources.GetBlueprint<BlueprintAbility>("9b93040dad242eb43ac7de6bb6547030");
            var SummonNaturesAllyVISpell = Resources.GetBlueprint<BlueprintAbility>("55bbce9b3e76d4a4a8c8e0698d29002c");
            var AnimalShapesSpell = Resources.GetBlueprint<BlueprintAbility>("cf689244b2c7e904eb85f26fd6e81552");
            var SummonNaturesAllyVIIISpell = Resources.GetBlueprint<BlueprintAbility>("ea78c04f0bd13d049a1cce5daf8d83e0");
            var SummonNaturesAllyIXSpell = Resources.GetBlueprint<BlueprintAbility>("a7469ef84ba50ac4cbf3d145e3173f8e");
            var FurDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("FurDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicFangSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HoldAnimalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BeastShapeISpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonNaturesAllyIVSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BeastShapeIIISpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonNaturesAllyVISpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AnimalShapesSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonNaturesAllyVIIISpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonNaturesAllyIXSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var FurDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("FurDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FurDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var FurDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("FurDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FurDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = FurDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { FurDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = FurDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fur Subdomain");
                bp.SetDescription("You share a bond with your furred allies. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}.\nPredator’s Grace: " +
                    "You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric levels you possess. You can use this " +
                    "ability a number of times per day equal to 3 + your Wisdom modifier.\nAnimal Companion: At 4th level, you gain the service of an mammal animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var FurDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("FurDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = FurDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var FurDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("FurDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FurDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FurDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fur Subdomain");
                bp.SetDescription("You share a bond with your furred allies. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}.\nPredator’s Grace: " +
                    "You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric levels you possess. You can use this " +
                    "ability a number of times per day equal to 3 + your Wisdom modifier.\nAnimal Companion: At 4th level, you gain the service of an mammal animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, beast shape I, summon nature's " +
                    "ally IV, beast shape III, summon nature's ally VI, summon nature's ally VII, summon nature's ally VIII, summon nature's ally IX.");
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
                    Helpers.LevelEntry(1, FurDomainBaseFeature),
                    Helpers.LevelEntry(4, FurDomainAnimalCompanionSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FurDomainBaseFeature, FurDomainAnimalCompanionSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var FurDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("FurDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fur Subdomain");
                bp.SetDescription("You share a bond with your furred allies. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}.\nPredator’s Grace: " +
                    "You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric levels you possess. You can use this " +
                    "ability a number of times per day equal to 3 + your Wisdom modifier.\nAnimal Companion: At 4th level, you gain the service of an mammal animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, beast shape I, summon nature's " +
                    "ally IV, beast shape III, summon nature's ally VI, summon nature's ally VII, summon nature's ally VIII, summon nature's ally IX.");
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
                    Helpers.LevelEntry(1, FurDomainBaseFeature),
                    Helpers.LevelEntry(4, FurDomainAnimalCompanionSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FurDomainBaseFeature, FurDomainAnimalCompanionSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // FurDomainSpellListFeatureDruid
            var FurDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("FurDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FurDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // FurDomainProgressionDruid
            var FurDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("FurDomainProgressionDruid", bp => {
                bp.SetName("Fur Subdomain");
                bp.SetDescription("You share a bond with your furred allies. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}.\nPredator’s Grace: " +
                    "You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric levels you possess. You can use this " +
                    "ability a number of times per day equal to 3 + your Wisdom modifier.\nAnimal Companion: At 4th level, you gain the service of an mammal animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, beast shape I, summon nature's " +
                    "ally IV, beast shape III, summon nature's ally VI, summon nature's ally VII, summon nature's ally VIII, summon nature's ally IX.");
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
                    Helpers.LevelEntry(1, FurDomainBaseFeature,FurDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(4, FurDomainAnimalCompanionSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FurDomainBaseFeature, FurDomainAnimalCompanionSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var ProtectionDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("7eb39ba8115a422bb69c702cc20ca58a");

            var FurDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("FurDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var FurDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("FurDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var SeparatistWithDruidAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistWithDruidAsIsProperty");

            var FurDomainBaseBuffSeparatist = Helpers.CreateBuff("FurDomainBaseBuffSeparatist", bp => {
                bp.SetName("Predator’s Grace");
                bp.SetDescription("You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric " +
                    "levels you possess. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = CheetahSprintIcon;
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 0;
                    c.ContextBonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 4, ProgressionValue = 10 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 9, ProgressionValue = 15 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 20 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 19, ProgressionValue = 25 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 24, ProgressionValue = 30 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 29, ProgressionValue = 35 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 34, ProgressionValue = 40 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 39, ProgressionValue = 45 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 50 }
                    };
                    c.m_CustomProperty = SeparatistWithDruidAsIsProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.FxOnStart = new PrefabLink() { AssetId = "91ef30ab58fa0d3449d4d2ccc20cb0f8" }; //Haste FX
            });

            var FurDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("FurDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Predator’s Grace");
                bp.SetDescription("You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric " +
                    "levels you possess. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = FurDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FurDomainBaseBuffSeparatist.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = WildShapeWolfBuff.m_Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("FurDomainBaseAbilitySeparatist.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var FurDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("FurDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FurDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = FurDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { FurDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = FurDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fur Subdomain");
                bp.SetDescription("You share a bond with your furred allies. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}.\nPredator’s Grace: " +
                    "You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric levels you possess. You can use this " +
                    "ability a number of times per day equal to 3 + your Wisdom modifier.\nAnimal Companion: At 4th level, you gain the service of an mammal animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)");
                bp.IsClassFeature = true;
            });

            var DomainAnimalCompanionProgressionSeparatist = Resources.GetBlueprint<BlueprintProgression>("c7a3ed56f239433fb50dfed4c07e8845");
            var FurDomainAnimalCompanionSelectionSeparatist = Helpers.CreateBlueprint<BlueprintFeatureSelection>("FurDomainAnimalCompanionSelectionSeparatist", bp => {
                bp.SetName("Fur Subdomain Animal Companion");
                bp.SetDescription("At 4th level, you gain the service of an mammal animal companion. Your effective druid level for this animal companion is equal to your level in the class " +
                    "that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use their druid level – 3 to determine the abilities of " +
                    "their animal companions.)");
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
                bp.AddFeatures(AnimalCompanionFeatureBear, AnimalCompanionFeatureBoar, AnimalCompanionFeatureDog, AnimalCompanionFeatureElk, AnimalCompanionFeatureHorse,
                    AnimalCompanionFeatureLeopard, AnimalCompanionFeatureMammoth, AnimalCompanionFeatureSmilodon, AnimalCompanionFeatureWolf, AnimalCompanionFeaturePreorderHorse,
                    AnimalCompanionFeaturePreorderSmilodon, CompanionWolverineFeature, AnimalCompanionEmpty);
            });

            var FurDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("FurDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FurDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Fur Subdomain");
                bp.SetDescription("You share a bond with your furred allies. You treat {g|Encyclopedia:Lore_Nature}Lore (nature){/g} as a class {g|Encyclopedia:Skills}skill{/g}.\nPredator’s Grace: " +
                    "You can, as a swift action, grant yourself a +10-foot bonus to your base speed for 1 round. This bonus increases by 5 feet for every 5 cleric levels you possess. You can use this " +
                    "ability a number of times per day equal to 3 + your Wisdom modifier.\nAnimal Companion: At 4th level, you gain the service of an mammal animal companion. Your effective druid level " +
                    "for this animal companion is equal to your level in the class that gave you access to this domain – 3. (Druids who take this ability through their nature bond class feature use " +
                    "their druid level – 3 to determine the abilities of their animal companions.)\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic fang, hold animal, beast shape I, summon nature's " +
                    "ally IV, beast shape III, summon nature's ally VI, summon nature's ally VII, summon nature's ally VIII, summon nature's ally IX.");
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
                    Helpers.LevelEntry(1, FurDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(6, FurDomainAnimalCompanionSelectionSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FurDomainBaseFeatureSeparatist, FurDomainAnimalCompanionSelectionSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            FurDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                FurDomainProgression.ToReference<BlueprintFeatureReference>(),
                FurDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            FurDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FurDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            FurDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FurDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            FurDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FurDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            FurDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FurDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            FurDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FurDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            Main.Log("Stop looking at my logs.");
            if (ModSettings.AddedContent.Domains.IsDisabled("Fur Subdomain")) { return; }
            DomainTools.RegisterDomain(FurDomainProgression);
            DomainTools.RegisterSecondaryDomain(FurDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(FurDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(FurDomainProgression);
            DomainTools.RegisterTempleDomain(FurDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(FurDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(FurDomainProgression, FurDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(FurDomainProgressionSeparatist);

        }
    }
}
