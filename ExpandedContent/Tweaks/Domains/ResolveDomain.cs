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
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Domains {
    internal class ResolveDomain {

        public static void AddResolveDomain() {

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
            var StrengthDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("1d6364123e1f6a04c88313d83d3b70ee");
            var StrengthDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("95525809d6e672a4880ea629ca5b58ab");

            var ResiliencyJudgmentAbilityGood = Resources.GetBlueprint<BlueprintActivatableAbility>("d47ecfecae485ac41b54fe4e8027797c");

            var ResolveDomainGreaterBuff = Helpers.CreateBuff("ResolveDomainGreaterBuff", bp => {
                bp.SetName("Bestow Resolve");
                bp.SetDescription("At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.m_Icon = ResiliencyJudgmentAbilityGood.Icon;
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
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
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.StatBonus,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Replace;
            });

            //ResolveDomainGreaterResource
            var ResolveDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ResolveDomainGreaterResource", bp => {
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
                    LevelStep = 4,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });
            //ResolveDomainGreaterAbility
            var ResolveDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("ResolveDomainGreaterAbility", bp => {
                bp.SetName("Bestow Resolve");
                bp.SetDescription("At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.m_Icon = ResiliencyJudgmentAbilityGood.Icon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ResolveDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;

                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ResolveDomainGreaterBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            AsChild = true
                        });                        
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 20.Feet();
                    c.m_TargetType = TargetType.Ally;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" }; //Enlarge Person
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic |= Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("ResolveDomainGreaterAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                
            });
            //ResolveDomainGreaterFeature
            var ResolveDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("ResolveDomainGreaterFeature", bp => {
                bp.SetName("Bestow Resolve");
                bp.SetDescription("At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.m_Icon = ResiliencyJudgmentAbilityGood.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ResolveDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ResolveDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var BlessSpell = Resources.GetBlueprint<BlueprintAbility>("90e59f4a4ada87243b7b3535a06d0638");
            var BullsStrengthSpell = Resources.GetBlueprint<BlueprintAbility>("4c3d08935262b6544ae97599b3a9556d");
            var MagicalVestmentSpell = Resources.GetBlueprint<BlueprintAbility>("2d4263d80f5136b4296d6eb43a221d7d");
            var EnlargePersonMassSpell = Resources.GetBlueprint<BlueprintAbility>("66dc49bf154863148bd217287079245e");
            var RighteousMightSpell = Resources.GetBlueprint<BlueprintAbility>("90810e5cf53bf854293cbd5ea1066252");
            var StoneSkinSpell = Resources.GetBlueprint<BlueprintAbility>("c66e86905f7606c4eaa5c774f0357b2b");
            var LegendaryProportionsSpell = Resources.GetBlueprint<BlueprintAbility>("da1b292d91ba37948893cdbe9ea89e28");
            var FrightfulAspectSpell = Resources.GetBlueprint<BlueprintAbility>("e788b02f8d21014488067bdd3ba7b325");
            var TransformationSpell = Resources.GetBlueprint<BlueprintAbility>("27203d62eb3d4184c9aced94f22e1806");
            var ResolveDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("ResolveDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BlessSpell.ToReference<BlueprintAbilityReference>()
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
                            MagicalVestmentSpell.ToReference<BlueprintAbilityReference>()
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
            var ResolveDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("ResolveDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ResolveDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var ResolveDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ResolveDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StrengthDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = StrengthDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { StrengthDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ResolveDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Resolve Subdomain");
                bp.SetDescription("\nFaith grants resolve — resolve grants strength and fortitude.\nStrength Surge: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} a creature to give it great strength. For 1 {g|Encyclopedia:Combat_Round}round{/g}, the target gains an enhancement " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum +1) to {g|Encyclopedia:MeleeAttack}melee attacks{/g} " +
                    "and {g|Encyclopedia:Athletics}Athletics checks{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nBestow Resolve: At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var ResolveDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("ResolveDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = ResolveDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var ResolveDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("ResolveDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ResolveDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = ResolveDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Resolve Subdomain");
                bp.SetDescription("\nFaith grants resolve — resolve grants strength and fortitude.\nStrength Surge: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} a creature to give it great strength. For 1 {g|Encyclopedia:Combat_Round}round{/g}, the target gains an enhancement " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum +1) to {g|Encyclopedia:MeleeAttack}melee attacks{/g} " +
                    "and {g|Encyclopedia:Athletics}Athletics checks{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nBestow Resolve: At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th." +
                    "\nDomain Spells: bless, bull's strength, magical vestment, mass enlarge person, righteous might, stoneskin, legendary proportions, frightful aspect, transformation.");
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
                    Helpers.LevelEntry(1, ResolveDomainBaseFeature),
                    Helpers.LevelEntry(8, ResolveDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ResolveDomainBaseFeature, ResolveDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var ResolveDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("ResolveDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Resolve Subdomain");
                bp.SetDescription("\nFaith grants resolve — resolve grants strength and fortitude.\nStrength Surge: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} a creature to give it great strength. For 1 {g|Encyclopedia:Combat_Round}round{/g}, the target gains an enhancement " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum +1) to {g|Encyclopedia:MeleeAttack}melee attacks{/g} " +
                    "and {g|Encyclopedia:Athletics}Athletics checks{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nBestow Resolve: At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th." +
                    "\nDomain Spells: bless, bull's strength, magical vestment, mass enlarge person, righteous might, stoneskin, legendary proportions, frightful aspect, transformation.");
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
                    Helpers.LevelEntry(1, ResolveDomainBaseFeature),
                    Helpers.LevelEntry(8, ResolveDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ResolveDomainBaseFeature, ResolveDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var StrengthDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("de9beaa7a8e34e7189c9d23ff0c565ea");
            var StrengthDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("edc48ef3a5364f8ba6998724df15dce3");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var ResolveDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ResolveDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var ResolveDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("ResolveDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,                    
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 10,
                    StartingIncrease = 1,
                    LevelStep = 4,
                    PerStepIncrease = 1,
                };
                bp.m_Min = 1;
            });

            var ResolveDomainGreaterBuffSeparatist = Helpers.CreateBuff("ResolveDomainGreaterBuffSeparatist", bp => {
                bp.SetName("Bestow Resolve");
                bp.SetDescription("At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.m_Icon = ResiliencyJudgmentAbilityGood.Icon;
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_CustomProperty = SeparatistAsIsProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.StatBonus,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Replace;
            });

            var ResolveDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("ResolveDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Bestow Resolve");
                bp.SetDescription("At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.m_Icon = ResiliencyJudgmentAbilityGood.Icon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ResolveDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;

                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ResolveDomainGreaterBuffSeparatist.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            AsChild = true
                        });
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 20.Feet();
                    c.m_TargetType = TargetType.Ally;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" }; //Enlarge Person
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic |= Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("ResolveDomainGreaterAbilitySeparatist.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();

            });

            var ResolveDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ResolveDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Bestow Resolve");
                bp.SetDescription("At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.m_Icon = ResiliencyJudgmentAbilityGood.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ResolveDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ResolveDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var ResolveDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("ResolveDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StrengthDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = StrengthDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { StrengthDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = ResolveDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Resolve Subdomain");
                bp.SetDescription("\nFaith grants resolve — resolve grants strength and fortitude.\nStrength Surge: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} a creature to give it great strength. For 1 {g|Encyclopedia:Combat_Round}round{/g}, the target gains an enhancement " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum +1) to {g|Encyclopedia:MeleeAttack}melee attacks{/g} " +
                    "and {g|Encyclopedia:Athletics}Athletics checks{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nBestow Resolve: At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th.");
                bp.IsClassFeature = true;
            });

            var ResolveDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("ResolveDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = ResolveDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Resolve Subdomain");
                bp.SetDescription("\nFaith grants resolve — resolve grants strength and fortitude.\nStrength Surge: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can " +
                    "{g|Encyclopedia:TouchAttack}touch{/g} a creature to give it great strength. For 1 {g|Encyclopedia:Combat_Round}round{/g}, the target gains an enhancement " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum +1) to {g|Encyclopedia:MeleeAttack}melee attacks{/g} " +
                    "and {g|Encyclopedia:Athletics}Athletics checks{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nBestow Resolve: At 8th level, you can bless creatures with the boldness of your deity. You can bestow a number of temporary hit points equal to your level + your Wisdom " +
                    "modifier to all allies within 20 feet. The temporary hit points remain for 1 minute. You can use this ability once per day at 8th level, plus one additional time per day " +
                    "for every 4 levels you possess beyond 8th." +
                    "\nDomain Spells: bless, bull's strength, magical vestment, mass enlarge person, righteous might, stoneskin, legendary proportions, frightful aspect, transformation.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ResolveDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, ResolveDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ResolveDomainBaseFeatureSeparatist, ResolveDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });


            ResolveDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ResolveDomainProgression.ToReference<BlueprintFeatureReference>(),
                ResolveDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            ResolveDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ResolveDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ResolveDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ResolveDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ResolveDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ResolveDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            ResolveDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ResolveDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            ResolveDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = ResolveDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(ResolveDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(ResolveDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Resolve Subdomain")) { return; }
            DomainTools.RegisterDomain(ResolveDomainProgression);
            DomainTools.RegisterSecondaryDomain(ResolveDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(ResolveDomainProgression);
            DomainTools.RegisterTempleDomain(ResolveDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(ResolveDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(ResolveDomainProgression, ResolveDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(ResolveDomainProgressionSeparatist);

        }

    }
}
