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
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.UnitLogic.Abilities;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Domains {
    internal class ScalykindDomain {

        public static void AddScalykindDomain() {

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
            var SacredHuntsmasterArchetype = Resources.GetBlueprint<BlueprintArchetype>("46eb929c8b6d7164188eb4d9bcd0a012");
            var Hypnotism = Resources.GetBlueprint<BlueprintAbility>("88367310478c10b47903463c5d0152b0");
            var FascinateEffectBuff = Resources.GetBlueprint<BlueprintBuff>("2d4bd347dec7d8648afd502ee40ae661");

            //Spelllist
            var MagicFangSpell = Resources.GetBlueprint<BlueprintAbility>("403cf599412299a4f9d5d925c7b9fb33");
            var PerniciousPoisonSpell = Resources.GetBlueprint<BlueprintAbility>("dee3074b2fbfb064b80b973f9b56319e");
            var MagicFangGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("f1100650705a69c4384d3edd88ba0f52");
            var PoisonSpell = Resources.GetBlueprint<BlueprintAbility>("d797007a142a6c0409a74b064065a15e");
            var AnimalGrowthSpell = Resources.GetBlueprint<BlueprintAbility>("56923211d2ac95e43b8ac5031bab74d8");
            var EyebiteSpell = Resources.GetBlueprint<BlueprintAbility>("3167d30dd3c622c46b0c0cb242061642");
            var CreepingDoomSpell = Resources.GetBlueprint<BlueprintAbility>("b974af13e45639a41a04843ce1c9aa12");
            var AnimalShapesSpell = Resources.GetBlueprint<BlueprintAbility>("cf689244b2c7e904eb85f26fd6e81552");
            var ShapechangeSpell = Resources.GetBlueprint<BlueprintAbility>("22b9044aa229815429d57d0a30e4b739");
            var ScalykindDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("ScalykindDomainSpellList", bp => {
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
                            PerniciousPoisonSpell.ToReference<BlueprintAbilityReference>()
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
                            PoisonSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AnimalGrowthSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EyebiteSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CreepingDoomSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AnimalShapesSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShapechangeSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var ScalykindDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("ScalykindDomainSpellListFeature", bp => {
                bp.m_Icon = ShapechangeSpell.Icon;
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ScalykindDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            //ScalykindCompanionSelectionDomain
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var DomainAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("125af359f8bc9a145968b5d8fd8159b8");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("65af7290b4efd5f418132141aaa36c1b");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");

            var AnimalCompanionFeatureCentipede = Resources.GetBlueprint<BlueprintFeature>("f9ef7717531f5914a9b6ecacfad63f46");
            var AnimalCompanionFeatureMonitor = Resources.GetBlueprint<BlueprintFeature>("ece6bde3dfc76ba4791376428e70621a");
            var AnimalCompanionFeatureTriceratops = Resources.GetBlueprint<BlueprintFeature>("2d3f409bb0956d44187e9ec8340163f8");
            var AnimalCompanionFeatureVelociaptor = Resources.GetBlueprint<BlueprintFeature>("89420de28b6bb9443b62ce489ae5423b");
            var AnimalCompanionFeatureTriceratops_PreorderBonus = Resources.GetBlueprint<BlueprintFeature>("52c854f77105445a9457572ab5826c00");
            var ScalykindCompanionSelectionDomain = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ScalykindCompanionSelectionDomain", bp => {
                bp.SetName("Scalykind Animal Companion");
                bp.SetDescription("At 4th level, you gain the service of an scaled animal companion. Your effective druid level for this animal companion is equal to your cleric level –2");
                bp.m_Icon = MagicFangGreaterSpell.Icon;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.IgnorePrerequisites = true;
                bp.AddFeatures(AnimalCompanionFeatureCentipede, AnimalCompanionFeatureMonitor, AnimalCompanionFeatureTriceratops, AnimalCompanionFeatureVelociaptor, AnimalCompanionFeatureTriceratops_PreorderBonus);
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
            });
            // ScalykindDomainBaseResource
            var ScalykindDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ScalykindDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });
            
            // ScalykindDomainBaseAbility
            var ScalykindDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ScalykindBaseAbility", bp => {
                bp.SetName("Venomous Stare");
                bp.SetDescription("This gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Untyped
                                    },
                                    AbilityType = StatType.Unknown,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero
                                    },
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank
                                        }
                                    }
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = FascinateEffectBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = true,
                                    DurationSeconds = 6
                                }
                            )
                        }
                    );
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
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScalykindDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ScalykindDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ScalykindDomainBaseFeature", bp => {
                bp.SetName("Scalykind Domain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nScaled Companion: At 4th level, you gain the service of an animal companion. Your effective druid level for this animal " +
                    "companion is equal to your cleric level –2.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ScalykindDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = ScalykindDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { ScalykindDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ScalykindDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                
                bp.IsClassFeature = true;
            });
            //Deity plug
            var ScalykindDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("ScalykindDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = ScalykindDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var ScalykindDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("ScalykindDomainProgression", bp => {
                bp.SetName("Scalykind Domain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nScaled Companion: At 4th level, you gain the service of an animal companion. Your effective druid level for this animal " +
                    "companion is equal to your cleric level –2.\nDomain Spells: magic fang, pernicious poison, greater magic fang, poison, animal growth, eyebite, " +
                    "creeping doom, animal shapes, shapechange.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SacredHuntsmasterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ScalykindDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ScalykindDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;                
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
                    Helpers.LevelEntry(1, ScalykindDomainBaseFeature),
                    Helpers.LevelEntry(4, ScalykindCompanionSelectionDomain)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ScalykindDomainBaseFeature, ScalykindCompanionSelectionDomain)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var ScalykindDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("ScalykindDomainProgressionSecondary", bp => {
                bp.SetName("Scalykind Domain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nScaled Companion: At 4th level, you gain the service of an animal companion. Your effective druid level for this animal " +
                    "companion is equal to your cleric level –2.\nDomain Spells: magic fang, pernicious poison, greater magic fang, poison, animal growth, eyebite, " +
                    "creeping doom, animal shapes, shapechange.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SacredHuntsmasterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
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
                    Helpers.LevelEntry(1, ScalykindDomainBaseFeature),
                    Helpers.LevelEntry(4, ScalykindCompanionSelectionDomain)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ScalykindDomainBaseFeature, ScalykindCompanionSelectionDomain)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var DomainAnimalCompanionProgressionSeparatist = Resources.GetBlueprint<BlueprintProgression>("c7a3ed56f239433fb50dfed4c07e8845");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var ScalykindDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ScalykindDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var ScalykindCompanionSelectionDomainSeparatist = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ScalykindCompanionSelectionDomainSeparatist", bp => {
                bp.SetName("Scalykind Animal Companion");
                bp.SetDescription("At 4th level, you gain the service of an scaled animal companion. Your effective druid level for this animal companion is equal to your cleric level –2");
                bp.m_Icon = MagicFangGreaterSpell.Icon;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.IgnorePrerequisites = true;
                bp.AddFeatures(AnimalCompanionFeatureCentipede, AnimalCompanionFeatureMonitor, AnimalCompanionFeatureTriceratops, AnimalCompanionFeatureVelociaptor, AnimalCompanionFeatureTriceratops_PreorderBonus);
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
            });
            var ScalykindDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("ScalykindDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var ScalykindDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("ScalykindBaseAbilitySeparatist", bp => {
                bp.SetName("Venomous Stare");
                bp.SetDescription("This gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Untyped
                                    },
                                    AbilityType = StatType.Unknown,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero
                                    },
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = FascinateEffectBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = true,
                                    DurationSeconds = 6
                                }
                            )
                        }
                    );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 4;
                    c.m_StepLevel = 2;
                    c.m_CustomProperty = SeparatistAsIsProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScalykindDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ScalykindDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ScalykindDomainBaseFeatureSeparatist", bp => {
                bp.SetName("Scalykind Domain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nScaled Companion: At 4th level, you gain the service of an animal companion. Your effective druid level for this animal " +
                    "companion is equal to your cleric level –2.");
                bp.m_Icon = Hypnotism.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ScalykindDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ScalykindDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { ScalykindDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ScalykindDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;

                bp.IsClassFeature = true;
            });

            var ScalykindDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("ScalykindDomainProgressionSeparatist", bp => {
                bp.SetName("Scalykind Domain");
                bp.SetDescription("\nYou are a true lord of reptiles, and your gaze can drive weak creatures into unconsciousness. \nVenomous Stare: This " +
                    "gaze attack can target a single creature within meduim range. The target must make a Will save (DC = 10 + 1/2 your cleric level + your Wisdom " +
                    "modifier). Those who fail take 1d6 points of nonlethal damage + 1 point for every two cleric levels you possess and are fascinated until the " +
                    "beginning of your next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. This is a mind-affecting " +
                    "effect. \nScaled Companion: At 4th level, you gain the service of an animal companion. Your effective druid level for this animal " +
                    "companion is equal to your cleric level –2.\nDomain Spells: magic fang, pernicious poison, greater magic fang, poison, animal growth, eyebite, " +
                    "creeping doom, animal shapes, shapechange.");
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SacredHuntsmasterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ScalykindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.ClericSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ScalykindDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(6, ScalykindCompanionSelectionDomainSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ScalykindDomainBaseFeatureSeparatist, ScalykindCompanionSelectionDomainSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            ScalykindDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ScalykindDomainProgression.ToReference<BlueprintFeatureReference>(),
                ScalykindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            ScalykindDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ScalykindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ScalykindDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ScalykindDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ScalykindDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ScalykindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ScalykindDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ScalykindDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ScalykindDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ScalykindDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(ScalykindDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(ScalykindDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.RetiredFeatures.IsDisabled("Old Scalykind Domain")) { return; }
            DomainTools.RegisterDomain(ScalykindDomainProgression);
            DomainTools.RegisterSecondaryDomain(ScalykindDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(ScalykindDomainProgression);
            DomainTools.RegisterTempleDomain(ScalykindDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(ScalykindDomainProgressionSecondary);
            DomainTools.RegisterImpossibleDomain(ScalykindDomainProgression, ScalykindDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(ScalykindDomainProgressionSeparatist);

        }

    }
}
