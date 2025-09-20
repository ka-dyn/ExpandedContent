using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using static ExpandedContent.Utilities.DeityTools;

namespace ExpandedContent.Tweaks.Domains {
    internal class FerocityDomain {

        public static void AddFerocityDomain() {

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
            var StrengthDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("3298fd30e221ef74189a06acbf376d29");
            var LeadBlades = Resources.GetBlueprint<BlueprintAbility>("779179912e6c6fe458fa4cfb90d96e10");

            var FerocityDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("FerocityDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var FerocityDomainBaseBuff = Helpers.CreateBuff("FerocityDomainBaseBuff", bp => {
                bp.SetName("Ferocious Strikes");
                bp.SetDescription("As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = LeadBlades.m_Icon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.AdditionalDamage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
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
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
            });

            var FerocityDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("FerocityDomainBaseAbility", bp => {
                bp.SetName("Ferocious Strikes");
                bp.SetDescription("As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = LeadBlades.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FerocityDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = FerocityDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var EnlargePersonSpell = Resources.GetBlueprint<BlueprintAbility>("c60969e7f264e6d4b84a1499fdcf9039");
            var BullsStrengthSpell = Resources.GetBlueprint<BlueprintAbility>("4c3d08935262b6544ae97599b3a9556d");
            var RageSpell = Resources.GetBlueprint<BlueprintAbility>("97b991256e43bb140b263c326f690ce2");
            var EnlargePersonMassSpell = Resources.GetBlueprint<BlueprintAbility>("66dc49bf154863148bd217287079245e");
            var RighteousMightSpell = Resources.GetBlueprint<BlueprintAbility>("90810e5cf53bf854293cbd5ea1066252");
            var BullsStrengthMassSpell = Resources.GetBlueprint<BlueprintAbility>("6a234c6dcde7ae94e94e9c36fd1163a7");
            var LegendaryProportionsSpell = Resources.GetBlueprint<BlueprintAbility>("da1b292d91ba37948893cdbe9ea89e28");
            var FrightfulAspectSpell = Resources.GetBlueprint<BlueprintAbility>("e788b02f8d21014488067bdd3ba7b325");
            var TransformationSpell = Resources.GetBlueprint<BlueprintAbility>("27203d62eb3d4184c9aced94f22e1806");
            var FerocityDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("FerocityDomainSpellList", bp => {
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
                            BullsStrengthSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RageSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EnlargePersonMassSpell.ToReference<BlueprintAbilityReference>()
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
                            BullsStrengthMassSpell.ToReference<BlueprintAbilityReference>()
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
            var FerocityDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("FerocityDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FerocityDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });            
            var FerocityDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("FerocityDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FerocityDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = FerocityDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { FerocityDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = FerocityDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ferocity Subdomain");
                bp.SetDescription("\nThe will of your deitiy must be delivered with conviction and ferocity — your faith gives you incredible might and fury.\nFerocious Strikes: " +
                    "As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nMight of the Gods: At 8th level, you add 1/2 of " +
                    "your level in the class that gave you access to this domain as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var FerocityDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("FerocityDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = FerocityDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var FerocityDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("FerocityDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FerocityDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = FerocityDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ferocity Subdomain");
                bp.SetDescription("\nThe will of your deitiy must be delivered with conviction and ferocity — your faith gives you incredible might and fury.\nFerocious Strikes: " +
                    "As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nMight of the Gods: At 8th level, you add 1/2 of " +
                    "your level in the class that gave you access to this domain as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: enlarge person, bull's strength, rage, mass enlarge person, righteous might, mass bull's strength, legendary proportions, " +
                    "frightful aspect, transformation.");
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
                    Helpers.LevelEntry(1, FerocityDomainBaseFeature),
                    Helpers.LevelEntry(8, StrengthDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FerocityDomainBaseFeature, StrengthDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var FerocityDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("FerocityDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ferocity Subdomain");
                bp.SetDescription("\nThe will of your deitiy must be delivered with conviction and ferocity — your faith gives you incredible might and fury.\nFerocious Strikes: " +
                    "As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nMight of the Gods: At 8th level, you add 1/2 of " +
                    "your level in the class that gave you access to this domain as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: enlarge person, bull's strength, rage, mass enlarge person, righteous might, mass bull's strength, legendary proportions, " +
                    "frightful aspect, transformation.");
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
                    Helpers.LevelEntry(1, FerocityDomainBaseFeature),
                    Helpers.LevelEntry(8, StrengthDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FerocityDomainBaseFeature, StrengthDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var StrengthDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("fb3d1b1ff65441fa9413cda1d107c1ea");

            var FerocityDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("FerocityDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var FerocityDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("FerocityDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var FerocityDomainBaseBuffSeparatist = Helpers.CreateBuff("FerocityDomainBaseBuffSeparatist", bp => {
                bp.SetName("Ferocious Strikes");
                bp.SetDescription("As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = LeadBlades.m_Icon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.AdditionalDamage;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
            });
            var FerocityDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("FerocityDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Ferocious Strikes");
                bp.SetDescription("As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = LeadBlades.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FerocityDomainBaseBuffSeparatist.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = FerocityDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var FerocityDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("FerocityDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FerocityDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = FerocityDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { FerocityDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = FerocityDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ferocity Subdomain");
                bp.SetDescription("\nThe will of your deitiy must be delivered with conviction and ferocity — your faith gives you incredible might and fury.\nFerocious Strikes: " +
                    "As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nMight of the Gods: At 8th level, you add 1/2 of " +
                    "your level in the class that gave you access to this domain as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.");
                bp.IsClassFeature = true;
            });

            var FerocityDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("FerocityDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = FerocityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ferocity Subdomain");
                bp.SetDescription("\nThe will of your deitiy must be delivered with conviction and ferocity — your faith gives you incredible might and fury.\nFerocious Strikes: " +
                    "As a swift action, you can imbue attacks you make this round to be ferocious strikes. If these attacks hit, they deal additional damage equal to 1/2 your cleric " +
                    "level (minimum +1). You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nMight of the Gods: At 8th level, you add 1/2 of " +
                    "your level in the class that gave you access to this domain as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: enlarge person, bull's strength, rage, mass enlarge person, righteous might, mass bull's strength, legendary proportions, " +
                    "frightful aspect, transformation.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {  };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, FerocityDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, StrengthDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(FerocityDomainBaseFeatureSeparatist, StrengthDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            FerocityDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                FerocityDomainProgression.ToReference<BlueprintFeatureReference>(),
                FerocityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            FerocityDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FerocityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            FerocityDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FerocityDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            FerocityDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FerocityDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            FerocityDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FerocityDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            FerocityDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = FerocityDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Ferocity Subdomain")) { return; }
            DomainTools.RegisterDomain(FerocityDomainProgression);
            DomainTools.RegisterSecondaryDomain(FerocityDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(FerocityDomainProgression);
            DomainTools.RegisterTempleDomain(FerocityDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(FerocityDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(FerocityDomainProgression, FerocityDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(FerocityDomainProgressionSeparatist);
            DomainTools.AllowFakeDivineSpark(FerocityDomainAllowed);
        }
    }
}
