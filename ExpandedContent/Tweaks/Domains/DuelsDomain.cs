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
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using ExpandedContent.Tweaks.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using static Kingmaker.EntitySystem.EntityDataBase;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.Tutorial.Triggers;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace ExpandedContent.Tweaks.Domains {
    internal class DuelsDomain {

        public static void AddDuelsDomain() {

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
            var WarDomainGreaterFeatureSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("79c6421dbdb028c4fa0c31b8eea95f16");
            var FeintAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("1bb6f0b196aa457ba80bdb312dc64952");
            var RangedFeintAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("74134224db554e63bd6183af247340e0");
            var CommandApproachAbilityIcon = Resources.GetBlueprint<BlueprintAbility>("f049fe38f5bb5ae48b252852727ab86a").Icon;

            var DuelsDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("DuelsDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var DuelsDomainBaseBuff = Helpers.CreateBuff("DuelsDomainBaseBuff", bp => {
                bp.SetName("Divine Challenge");
                bp.SetDescription("This creature is the target of a divine challenge. " +
                    "\nAs a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level.");
                bp.m_Icon = CommandApproachAbilityIcon;
                bp.AddComponent<UniqueBuff>();
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<ACBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.Sacred;
                    c.Value = 1;
                    c.CheckCaster = true;
                });
            });

            var DuelsDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("DuelsDomainBaseAbility", bp => {
                bp.SetName("Divine Challenge");
                bp.SetDescription("As a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = CommandApproachAbilityIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DuelsDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
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
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
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
                    c.m_RequiredResource = DuelsDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("DuelsDomainBaseAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var MagicWeaponSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d7fdd79f0f6b6a2418298e936bb68e40");
            var GraceSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("464a7193519429f48b4d190acb753cf0");
            var MagicalVestmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2d4263d80f5136b4296d6eb43a221d7d");
            var DivinePowerSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ef16771cb05d1344989519e87f25b3c5");
            var DanceOfAHundredCutsAbility = Resources.GetModBlueprint<BlueprintAbility>("DanceOfAHundredCutsAbility");
            var BladeBarrierSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("36c8971e91f1745418cc3ffdfac17b74");
            var PowerWordBlindSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("261e1788bfc5ac1419eec68b1d485dbc");
            var DanceOfAThousandCutsAbility = Resources.GetModBlueprint<BlueprintAbility>("DanceOfAThousandCutsAbility");
            var PowerWordKillSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2f8a67c483dfa0f439b293e094ca9e3c");
            var DuelsDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("DuelsDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicWeaponSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            GraceSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicalVestmentSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DivinePowerSpell
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DanceOfAHundredCutsAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BladeBarrierSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PowerWordBlindSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DanceOfAThousandCutsAbility.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PowerWordKillSpell
                        }
                    },
                };
            });
            var DuelsDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("DuelsDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DuelsDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DuelsDomainFeintBluffBuff = Helpers.CreateBuff("DuelsDomainFeintBluffBuff", bp => {
                bp.SetName("DuelsDomainFeintBluffBuff");
                bp.SetDescription("");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Stat = StatType.CheckBluff;
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
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
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
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });

            var DuelsDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("DuelsDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DuelsDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DuelsDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DuelsDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DuelsDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        FeintAbility, RangedFeintAbility
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = DuelsDomainBaseBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DuelsDomainFeintBluffBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0,
                                    ToCaster = true,
                                    AsChild = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }                        
                        );
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        FeintAbility, RangedFeintAbility
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = DuelsDomainFeintBluffBuff.ToReference<BlueprintBuffReference>(),
                            ToCaster = true
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Duels Subdomain");
                bp.SetDescription("\nYou seek to honour your deity with duels against worthy opponents." +
                    "\nDivine Challenge: As a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var DuelsDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("DuelsDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = DuelsDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // Main Blueprint
            var DuelsDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("DuelsDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DuelsDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DuelsDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Duels Subdomain");
                bp.SetDescription("\nYou seek to honour your deity with duels against worthy opponents." +
                    "\nDivine Challenge: As a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level. You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsMagicWeapon}magic weapon{/g}, {g|SpellsGrace}grace{/g}, {g|SpellsMagicalVestment}magical vestment{/g}, " +
                    "{g|SpellsDivinePower}divine power{/g}, dance of a hundred cuts, {g|SpellsBladeBarrier}blade barrier{/g}, " +
                    "{g|SpellsPowerWordBlind}power word blind{/g}, dance of a thousand cuts, {g|SpellsPowerWordKill}power word kill{/g}.");
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
                    Helpers.LevelEntry(1, DuelsDomainBaseFeature),
                    Helpers.LevelEntry(8, WarDomainGreaterFeatureSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DuelsDomainBaseFeature, WarDomainGreaterFeatureSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var DuelsDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("DuelsDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any; //to work with divine scourge
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Duels Subdomain");
                bp.SetDescription("\nYou seek to honour your deity with duels against worthy opponents." +
                    "\nDivine Challenge: As a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level. You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsMagicWeapon}magic weapon{/g}, {g|SpellsGrace}grace{/g}, {g|SpellsMagicalVestment}magical vestment{/g}, " +
                    "{g|SpellsDivinePower}divine power{/g}, dance of a hundred cuts, {g|SpellsBladeBarrier}blade barrier{/g}, " +
                    "{g|SpellsPowerWordBlind}power word blind{/g}, dance of a thousand cuts, {g|SpellsPowerWordKill}power word kill{/g}.");
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
                    Helpers.LevelEntry(1, DuelsDomainBaseFeature),
                    Helpers.LevelEntry(8, WarDomainGreaterFeatureSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DuelsDomainBaseFeature, WarDomainGreaterFeatureSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions

            var DuelsDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DuelsDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DuelsDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("DuelsDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });


            var DuelsDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("DuelsDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Divine Challenge");
                bp.SetDescription("As a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = CommandApproachAbilityIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DuelsDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
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
                    c.m_RequiredResource = DuelsDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("DuelsDomainBaseAbilitySeparatist.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var DuelsDomainFeintBluffBuffSeparatist = Helpers.CreateBuff("DuelsDomainFeintBluffBuffSeparatist", bp => {
                bp.SetName("DuelsDomainFeintBluffBuffSeparatist");
                bp.SetDescription("");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Stat = StatType.CheckBluff;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
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
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });

            var DuelsDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DuelsDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DuelsDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DuelsDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DuelsDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DuelsDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        FeintAbility, RangedFeintAbility
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = DuelsDomainBaseBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DuelsDomainFeintBluffBuffSeparatist.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0,
                                    ToCaster = true,
                                    AsChild = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }
                        );
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        FeintAbility, RangedFeintAbility
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = DuelsDomainFeintBluffBuffSeparatist.ToReference<BlueprintBuffReference>(),
                            ToCaster = true
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Duels Subdomain");
                bp.SetDescription("\nYou seek to honour your deity with duels against worthy opponents." +
                    "\nDivine Challenge: As a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.IsClassFeature = true;
            });

            var DuelsDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("DuelsDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DuelsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Duels Subdomain");
                bp.SetDescription("\nYou seek to honour your deity with duels against worthy opponents." +
                    "\nDivine Challenge: As a swift action, you can challenge a visible foe within 30 feet, gaining a +1 sacred bonus to your " +
                    "AC against that creature’s attacks and a bonus equal to 1/2 your cleric level on Bluff skill checks to feint against it. " +
                    "These bonuses last for a number of rounds equal to 1/2 your cleric level. You can use this ability a number of times per day equal to 3 + your Wisdom modifier." +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsMagicWeapon}magic weapon{/g}, {g|SpellsGrace}grace{/g}, {g|SpellsMagicalVestment}magical vestment{/g}, " +
                    "{g|SpellsDivinePower}divine power{/g}, dance of a hundred cuts, {g|SpellsBladeBarrier}blade barrier{/g}, " +
                    "{g|SpellsPowerWordBlind}power word blind{/g}, dance of a thousand cuts, {g|SpellsPowerWordKill}power word kill{/g}.");
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
                    Helpers.LevelEntry(1, DuelsDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, WarDomainGreaterFeatureSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DuelsDomainBaseFeatureSeparatist, WarDomainGreaterFeatureSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            DuelsDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                DuelsDomainProgression.ToReference<BlueprintFeatureReference>(),
                DuelsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            DuelsDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DuelsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            DuelsDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DuelsDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            DuelsDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DuelsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            DuelsDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DuelsDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            DuelsDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DuelsDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Duels Subdomain")) { return; }
            DomainTools.RegisterDomain(DuelsDomainProgression);
            DomainTools.RegisterSecondaryDomain(DuelsDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(DuelsDomainProgression);
            DomainTools.RegisterTempleDomain(DuelsDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(DuelsDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(DuelsDomainProgression, DuelsDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(DuelsDomainProgressionSeparatist);

        }
    }
}
