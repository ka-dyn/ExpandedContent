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
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Utilities;
using Kingmaker.Craft;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using System.Collections.Generic;
using Kingmaker.Formations.Facts;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Blueprints.Items;

namespace ExpandedContent.Tweaks.Spirits {
    internal class SlumsSpirit {
        public static void AddSlumsSprit() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var UnswornShamanArchetype = Resources.GetBlueprint<BlueprintArchetype>("556590a43467a27459ac1a80324c9f9f");
            var ShamanHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4223fe18c75d4d14787af196a04e14e7");
            var PossessedShamanSharedSkillSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("9d0477ebd71d43041b419c216b5d6cff");



            #region Spelllist
            var HazeOfDreamsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("40ec382849b60504d88946df46a10f2d");
            var SummonMonsterIIBaseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1724061e89c667045a6891179ee2e8e7");
            var HoldPersonSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c7104f7526c4c524f91474614054547e");
            var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
            var EcholocationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("20b548bf09bb3ea4bafea78dcb4f3db6");
            var CloakOfDreamsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7f71a70d822af94458dc1a235507e972");
            var HoldPersonMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("defbbeaef79eda64abc645036228a31b");
            var RiftOfRuinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("dd3dacafcf40a0145a5824c838e2698d");
            var HoldMonsterMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7f4b66a2b1fdab142904a263c7866d46");

            var SlumsSpiritSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("SlumsSpiritSpellList", bp => {
                bp.IsMythic = false;
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HazeOfDreamsSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonMonsterIIBaseSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HoldPersonSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ConfusionSpell
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EcholocationSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CloakOfDreamsSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HoldPersonMassSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RiftOfRuinSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HoldMonsterMassSpell
                        }
                    },
                };
            });
            var SlumsSpiritSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("SlumsSpiritSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SlumsSpiritSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = true;
                    c.HideInUI = false;
                    c.m_Feature = PossessedShamanSharedSkillSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Spirit Features
            #region Base TEST NUMBER OF USES
            var DimensionDorrCasterOnly = Resources.GetBlueprint<BlueprintAbility>("a9b8be9b87865744382f7c64e599aeb2");
            var ShamanSlumsSpiritBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanSlumsSpiritBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[] { },
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] { },
                    LevelIncrease = 1,
                    StartingLevel = 12,
                    LevelStep = 8,
                    StartingIncrease = 1,
                    PerStepIncrease = 1
                };
            });

            var ShamanSlumsSpiritBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanSlumsSpiritBaseAbility", bp => {
                bp.SetName("Doors to Everywhere");
                bp.SetDescription("You instantly transfer yourself from your current location to any other spot within range, as if using the dimention door spell. " +
                    "However only you are transported and not your allies. " +
                    "You can use this ability three times per day, plus one additional time per day at 12th level and at 20th level.");
                bp.AddComponent(Helpers.CreateCopy(DimensionDorrCasterOnly.GetComponent<AbilityCustomDimensionDoor>()));
                bp.AddComponent<LineOfSightIgnorance>();
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanSlumsSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = DimensionDorrCasterOnly.Icon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanSlumsSpiritBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanSlumsSpiritBaseFeature", bp => {
                bp.SetName("Doors to Everywhere");
                bp.SetDescription("You instantly transfer yourself from your current location to any other spot within range, as if using the dimention door spell. " +
                    "However only you are transported and not your allies. " +
                    "You can use this ability three times per day, plus one additional time per day at 12th level and at 20th level.");
                bp.m_Icon = DimensionDorrCasterOnly.Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShamanSlumsSpiritBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanSlumsSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Greater  
            var FeatherStepIcon = Resources.GetBlueprint<BlueprintAbility>("f3c0b267dd17a2a45a40805e31fe3cd1").Icon;
            var ShamanSlumsSpiritGreaterBuff = Helpers.CreateBuff("ShamanSlumsSpiritGreaterBuff", bp => {
                bp.SetName("City’s Shroud");
                bp.SetDescription("When in an urban environment, the shaman blends into the streets around her, making her difficult to pin down. " +
                    "She gains the effects of the evasion and improved uncanny dodge class features." +
                    "\nEvasion" +
                    "\nA character can avoid even magical and unusual attacks with great agility. If a character makes a successful " +
                    "{g|Encyclopedia:Saving_Throw}Reflex saving throw{/g} against an attack that normally deals half {g|Encyclopedia:Damage}damage{/g} " +
                    "on a successful save, he instead takes no damage. A {g|Encyclopedia:Helpless}helpless{/g} character does not gain the benefit of evasion." +
                    "\nImproved Uncanny Dodge" +
                    "\nThe character can no longer be {g|Encyclopedia:Flanking}flanked{/g}.\r\nThis defense denies another rogue the ability to sneak attack " +
                    "the character by flanking her, unless the attacker has at least four more rogue levels than the target does.");
                bp.m_Icon = FeatherStepIcon;
                bp.AddComponent<Evasion>(c => {
                    c.SavingThrow = SavingThrowType.Reflex;
                });
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.CannotBeFlanked;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanSlumsSpiritGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanSlumsSpiritGreaterFeature", bp => {
                bp.SetName("City’s Shroud");
                bp.SetDescription("When in an urban environment, the shaman blends into the streets around her, making her difficult to pin down. " +
                    "She gains the effects of the evasion and improved uncanny dodge class features." +
                    "\nEvasion" +
                    "\nA character can avoid even magical and unusual attacks with great agility. If a character makes a successful " +
                    "{g|Encyclopedia:Saving_Throw}Reflex saving throw{/g} against an attack that normally deals half {g|Encyclopedia:Damage}damage{/g} " +
                    "on a successful save, he instead takes no damage. A {g|Encyclopedia:Helpless}helpless{/g} character does not gain the benefit of evasion." +
                    "\nImproved Uncanny Dodge" +
                    "\nThe character can no longer be {g|Encyclopedia:Flanking}flanked{/g}.\r\nThis defense denies another rogue the ability to sneak attack " +
                    "the character by flanking her, unless the attacker has at least four more rogue levels than the target does.");
                bp.AddComponent<AddBuffInTerrain>(c => {
                    c.Terrain = AreaSetting.Urban;
                    c.m_Buff = ShamanSlumsSpiritGreaterBuff.ToReference<BlueprintBuffReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region True
            var SneakAttackIcon = Resources.GetBlueprint<BlueprintFeature>("9b9eac6709e1c084cb18c3a366e0ec87").Icon;
            var ShamanSlumsSpiritTrueResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanSlumsSpiritTrueResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                };
            });
            var ShamanSlumsSpiritTrueBuff = Helpers.CreateBuff("ShamanSlumsSpiritTrueBuff", bp => {
                bp.SetName("Paragon of the City");
                bp.SetDescription("As a standard action, the shaman assumes a spirit-infused paragon form that makes her a lethal stalker of the alleys and shadows. " +
                    "She gains the ability to make sneak attacks as a rogue of her shaman level for 1 minute. (If she already has sneak attack dice, these stack.) " +
                    "She can use this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.m_Icon = SneakAttackIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SneakAttack;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanSlumsSpiritTrueAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanSlumsSpiritTrueAbility", bp => {
                bp.SetName("Paragon of the City");
                bp.SetDescription("As a standard action, the shaman assumes a spirit-infused paragon form that makes her a lethal stalker of the alleys and shadows. " +
                    "She gains the ability to make sneak attacks as a rogue of her shaman level for 1 minute. (If she already has sneak attack dice, these stack.) " +
                    "She can use this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.m_Icon = SneakAttackIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanSlumsSpiritTrueBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0,
                            IsNotDispelable = true
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Reach | Metamagic.Selective;
                bp.LocalizedDuration = Helpers.CreateString("ShamanSlumsSpiritTrueAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanSlumsSpiritTrueFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanSlumsSpiritTrueFeature", bp => {
                bp.SetName("Paragon of the City");
                bp.SetDescription("As a standard action, the shaman assumes a spirit-infused paragon form that makes her a lethal stalker of the alleys and shadows. " +
                    "She gains the ability to make sneak attacks as a rogue of her shaman level for 1 minute. (If she already has sneak attack dice, these stack.) " +
                    "She can use this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.m_Icon = SneakAttackIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanSlumsSpiritTrueAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanSlumsSpiritTrueResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Manifestation 
            var Corrupter = Resources.GetBlueprint<BlueprintFeature>("55c364c3f02e4fdc8a63125b5a4c256c");
            var ShamanSlumsSpiritManifestationFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanSlumsSpiritManifestationFeature", bp => {
                bp.SetName("Manifestation");
                bp.SetDescription("Upon reaching 20th level, the shaman becomes a spirit of the slums. She is immune to all diseases and poisons. " +
                    "When in an urban environment, she gains a +4 insight bonus to her AC and on Reflex saves.");
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Poison | SpellDescriptor.Disease;
                    c.m_CasterIgnoreImmunityFact = Corrupter.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Poison | SpellDescriptor.Disease;
                    c.m_IgnoreFeature = Corrupter.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Urban;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.AC;
                    c.Value = 4;
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Urban;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 4;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #endregion
            #region Progression
            var ShamanSlumsSpiritProgression = Helpers.CreateBlueprint<BlueprintProgression>("ShamanSlumsSpiritProgression", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFeaturesFromSelectionToDescription>(c => {
                    c.SetIntroduction("Additional Hexes:");
                    c.m_FeatureSelection = ShamanHexSelection.ToReference<BlueprintFeatureSelectionReference>();
                    c.OnlyIfRequiresThisFeature = true;
                });
                bp.AddComponent<AddSpellsToDescription>(c => {
                    c.SetIntroduction("Bonus Spells:");
                    c.m_SpellLists = new BlueprintSpellListReference[] { SlumsSpiritSpellList.ToReference<BlueprintSpellListReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanSpirit };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ShamanClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] { };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ShamanSlumsSpiritBaseFeature, SlumsSpiritSpellListFeature),
                    Helpers.LevelEntry(8, ShamanSlumsSpiritGreaterFeature),
                    Helpers.LevelEntry(16, ShamanSlumsSpiritTrueFeature),
                    Helpers.LevelEntry(20, ShamanSlumsSpiritManifestationFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            #endregion



            #region Wandering Spirit
            var ShamanSlumsSpiritWanderingTrueBuff = Helpers.CreateBuff("ShamanSlumsSpiritWanderingTrueBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanSlumsSpiritTrueFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanSlumsSpiritWanderingGreaterBuff = Helpers.CreateBuff("ShamanSlumsSpiritWanderingGreaterBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanSlumsSpiritGreaterFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanSlumsSpiritWanderingBaseBuff = Helpers.CreateBuff("ShamanSlumsSpiritWanderingBaseBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanSlumsSpiritBaseFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanSlumsSpiritWanderingFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanSlumsSpiritWanderingFeature", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 20,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanSlumsSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 12,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanSlumsSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SlumsSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Unsworn Wandering Spirit
            var UnswornShamanSlumsSpiritWanderingFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("UnswornShamanSlumsSpiritWanderingFeature1", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 18,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanSlumsSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 10,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanSlumsSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SlumsSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var UnswornShamanSlumsSpiritWanderingFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("UnswornShamanSlumsSpiritWanderingFeature2", bp => {
                bp.SetName("Slums");
                bp.SetDescription("A shaman who selects the slums spirit gains the city’s alleys and avenues as steadfast allies. The rats in the gutter, " +
                    "the torches along the walls, the coins that flow through the market are all a part of her and serve her whim.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 20,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanSlumsSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 14,
                                        Inverted = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanSlumsSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0,
                                        m_IsExtendable = true
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            }
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanSlumsSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SlumsSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion

            #region Hexes
            #region Accident 
            var TouchOfGracelessnessIcon = Resources.GetBlueprint<BlueprintAbility>("5d38c80a819e8084ba19b29a865312c2").Icon;
            var ShamanHexAccidentAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexAccidentAbility", bp => {
                bp.SetName("Accident");
                bp.SetDescription("The shaman causes a target within 30 feet to stumble and fall. The shaman attempts a trip {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} " +
                    "using her {g|Encyclopedia:Caster_Level}caster level{/g} as its {g|Encyclopedia:BAB}base attack bonus{/g} against the target’s CMD. On a successful trip attempt, " +
                    "the target falls prone and takes 1d6 points of damage.");
                bp.m_Icon = TouchOfGracelessnessIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Trip,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Bludgeoning,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = Kingmaker.Enums.Damage.DamageEnergyType.Acid,
                                        Type = DamageType.Physical
                                    },
                                    Drain = false,
                                    AbilityType = StatType.Unknown,
                                    Duration = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = true,
                                    },
                                    PreRolledSharedValue = AbilitySharedValue.Damage,
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D6,
                                        DiceCountValue = 1,
                                        BonusValue = 0,
                                    },
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage,
                                    Half = false
                                }
                                ),
                            ReplaceStat = true,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = true,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                }); 
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Wisdom;
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                });
                //bp.AddComponent<ContextCalculateAbilityParams>(c => {
                //    c.UseKineticistMainStat = false;
                //    c.StatType = StatType.Wisdom;
                //    c.StatTypeFromCustomProperty = false;
                //    c.m_CustomProperty = null;
                //    c.ReplaceCasterLevel = true;
                //    c.CasterLevel = new ContextValue() {
                //        ValueType = ContextValueType.Rank,
                //        ValueRank = AbilityRankType.Default
                //    };
                //    c.ReplaceSpellLevel = false;
                //    c.SpellLevel = new ContextValue();
                //});
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHexAccidentFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexAccidentFeature", bp => {
                bp.SetName("Accident");
                bp.SetDescription("The shaman causes a target within 30 feet to stumble and fall. The shaman attempts a trip {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} " +
                    "using her {g|Encyclopedia:Caster_Level}caster level{/g} as its {g|Encyclopedia:BAB}base attack bonus{/g} against the target’s CMD. On a successful trip attempt, " +
                    "the target falls prone and takes 1d6 points of damage.");
                bp.m_Icon = TouchOfGracelessnessIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexAccidentAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexAccidentFeature);
            #endregion
            #region Bad Penny - Need to code getting the coin back in the future
            var BadPennyIcon = AssetLoader.LoadInternal("Skills", "Icon_BadPenny.png");
            var GoldCoins = Resources.GetBlueprint<BlueprintItem>("f2bc0997c24e573448c6c91d2be88afa");
            var TouchItem = Resources.GetBlueprintReference<BlueprintItemWeaponReference>("bb337517547de1a4189518d404ec49d4");
            var ShamanHexBadPennyBuff = Helpers.CreateBuff("ShamanHexBadPennyBuff", bp => {
                bp.SetName("Bad Penny");
                bp.SetDescription("As a standard action, the shaman can curse a coin, and place it in the pockets of a target, attempting to plant the coin is a melee touch attack. " +
                    "The bearer of the cursed coin takes a –2 penalty on all saving throws and skill checks as long he has the coin on his person. " +
                    "Once the coin leaves his person, the curse ends and the coin becomes a mundane piece of tender again. " +
                    "At 8th level, the penalty becomes –4. If the shaman curses a new coin, the previous curse ends. This is a curse effect.");
                bp.m_Icon = BadPennyIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveReflex;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveWill;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = -2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = -4 }
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Curse;
                });
                bp.AddComponent<UniqueBuff>();
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ShamanHexBadPennyAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexBadPennyAbility", bp => {
                bp.SetName("Bad Penny");
                bp.SetDescription("As a standard action, the shaman can curse a coin, and place it in the pockets of a target, attempting to plant the coin is a melee touch attack. " +
                    "The bearer of the cursed coin takes a –2 penalty on all saving throws and skill checks as long he has the coin on his person. " +
                    "Once the coin leaves his person, the curse ends and the coin becomes a mundane piece of tender again. " +
                    "At 8th level, the penalty becomes –4. If the shaman curses a new coin, the previous curse ends. This is a curse effect.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexBadPennyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Curse | SpellDescriptor.Hex;
                });
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 1,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.m_Icon = BadPennyIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHexBadPennyFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexBadPennyFeature", bp => {
                bp.SetName("Bad Penny");
                bp.SetDescription("As a standard action, the shaman can curse a coin, and place it in the pockets of a target, attempting to plant the coin is a melee touch attack. " +
                    "The bearer of the cursed coin takes a –2 penalty on all saving throws and skill checks as long he has the coin on his person. " +
                    "Once the coin leaves his person, the curse ends and the coin becomes a mundane piece of tender again. " +
                    "At 8th level, the penalty becomes –4. If the shaman curses a new coin, the previous curse ends. This is a curse effect.");
                bp.m_Icon = BadPennyIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexBadPennyAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexBadPennyFeature);
            #endregion
            #region City Spirit
            var SenseiMythicWisdomIcon = Resources.GetBlueprint<BlueprintFeature>("4356b5d6d34489747bba68d43924a857").Icon;
            var ShamanHexCitySpiritResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHexCitySpiritResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                };
            });
            var ShamanHexCitySpiritBuff = Helpers.CreateBuff("ShamanHexCitySpiritBuff", bp => {
                bp.SetName("City Spirit");
                bp.SetDescription("As a swift action, the shaman channels the city’s spirit through herself, gaining a +4 bonus on all Dexterity– and Wisdom-based skill checks. " +
                    "She can use this ability for a number of rounds per day equal to 3 + her Charisma modifier. These rounds need not be consecutive.");
                bp.m_Icon = SenseiMythicWisdomIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillMobility;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillThievery;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillLoreNature;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillLoreReligion;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanHexCitySpiritAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("ShamanHexCitySpiritAbility", bp => {
                bp.SetName("City Spirit");
                bp.SetDescription("As a swift action, the shaman channels the city’s spirit through herself, gaining a +4 bonus on all Dexterity– and Wisdom-based skill checks. " +
                    "She can use this ability for a number of rounds per day equal to 3 + her Charisma modifier. These rounds need not be consecutive.");
                bp.m_Icon = SenseiMythicWisdomIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = ShamanHexCitySpiritResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = ShamanHexCitySpiritBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
                bp.DeactivateIfCombatEnded = false;
            });
            var ShamanHexCitySpiritFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexCitySpiritFeature", bp => {
                bp.SetName("City Spirit");
                bp.SetDescription("As a swift action, the shaman channels the city’s spirit through herself, gaining a +4 bonus on all Dexterity– and Wisdom-based skill checks. " +
                    "She can use this ability for a number of rounds per day equal to 3 + her Charisma modifier. These rounds need not be consecutive.");
                bp.m_Icon = SenseiMythicWisdomIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexCitySpiritAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanHexCitySpiritResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanSlumsSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexCitySpiritFeature);
            #endregion


            ShamanSlumsSpiritProgression.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ShamanHexAccidentFeature.ToReference<BlueprintFeatureReference>(),
                ShamanHexBadPennyFeature.ToReference<BlueprintFeatureReference>(),
                ShamanHexCitySpiritFeature.ToReference<BlueprintFeatureReference>()
            };
            #endregion


            SpiritTools.RegisterSpirit(ShamanSlumsSpiritProgression);
            SpiritTools.RegisterSecondSpirit(ShamanSlumsSpiritProgression);
            SpiritTools.RegisterWanderingSpirit(ShamanSlumsSpiritWanderingFeature);
            SpiritTools.RegisterUnswornSpirit1(UnswornShamanSlumsSpiritWanderingFeature1);
            SpiritTools.RegisterUnswornSpirit2(UnswornShamanSlumsSpiritWanderingFeature2);


        }
    }
}
