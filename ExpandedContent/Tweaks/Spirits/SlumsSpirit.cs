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
                    "However only you are transported and not your allies." +
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
                    "However only you are transported and not your allies." +
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
            #region Greater TEST 

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
                bp.m_Icon = GlitterdustBuff.Icon;
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
            #region Guiding Star Hex
            var GuidingStarIcon = AssetLoader.LoadInternal("Skills", "Icon_GuidingStar.png");
            var ShamanSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("44f16931dabdff643bfe2a48138e769f");
            var ShamanHexGuidingStarSkillBuff = Helpers.CreateBuff("ShamanHexGuidingStarSkillBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillUseMagicDevice;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillPersuasion;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Wisdom;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ShamanHexGuidingStarSkillFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexGuidingStarSkillFeature", bp => {
                bp.SetName("Guiding Star - Skill Bonus");
                bp.SetDescription("The stars themselves hold many answers, you may add your Wisdom modifier to your Charisma modifier on all Charisma-based skill checks.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShamanHexGuidingStarSkillBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ShamanHexGuidingStarResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHexGuidingStarResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            var ShamanHexGuidingStarMetamagicBuffEmpower = Helpers.CreateBuff("ShamanHexGuidingStarMetamagicBuffEmpower", bp => {
                bp.SetName("Guiding Star - Empower Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Empower Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Empower;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = false;
                    c.m_Spellbook = new BlueprintSpellbookReference();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = true;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var ShamanHexGuidingStarMetamagicBuffExtend = Helpers.CreateBuff("ShamanHexGuidingStarMetamagicBuffExtend", bp => {
                bp.SetName("Guiding Star - Extend Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Extend;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = false;
                    c.m_Spellbook = new BlueprintSpellbookReference();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = true;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var ShamanHexGuidingStarMetamagicAbilityBase = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexGuidingStarMetamagicAbilityBase", bp => {
                bp.SetName("Guiding Star");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Empower Spell or Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                // Ability Variants added later
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanHexGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHexGuidingStarMetamagicAbilityEmpower = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexGuidingStarMetamagicAbilityEmpower", bp => {
                bp.SetName("Guiding Star - Empower Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Empower Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexGuidingStarMetamagicBuffEmpower.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexGuidingStarMetamagicBuffEmpower.ToReference<BlueprintUnitFactReference>(),
                        ShamanHexGuidingStarMetamagicBuffExtend.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanHexGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = ShamanHexGuidingStarMetamagicAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHexGuidingStarMetamagicAbilityExtend = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexGuidingStarMetamagicAbilityExtend", bp => {
                bp.SetName("Guiding Star - Extend Spell");
                bp.SetDescription("Once per day you can cast one spell as if it were modified by the Extend Spell feat without increasing the spell’s casting time or level.");
                bp.m_Icon = GuidingStarIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexGuidingStarMetamagicBuffExtend.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexGuidingStarMetamagicBuffEmpower.ToReference<BlueprintUnitFactReference>(),
                        ShamanHexGuidingStarMetamagicBuffExtend.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanHexGuidingStarResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Parent = ShamanHexGuidingStarMetamagicAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            ShamanHexGuidingStarMetamagicAbilityBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    ShamanHexGuidingStarMetamagicAbilityEmpower.ToReference<BlueprintAbilityReference>(),
                    ShamanHexGuidingStarMetamagicAbilityExtend.ToReference<BlueprintAbilityReference>()
                };
            });
            var ShamanHexAccidentFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexAccidentFeature", bp => {
                bp.SetName("Accident");
                bp.SetDescription("The shaman causes a target within 30 feet to stumble and fall. The shaman attempts a trip {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} " +
                    "using her {g|Encyclopedia:Caster_Level}caster level{/g} as its {g|Encyclopedia:BAB}base attack bonus{/g} against the target’s CMD. On a successful check, " +
                    "the target falls prone and takes 1d6 points of damage.");
                bp.m_Icon = GuidingStarIcon;
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
            #region Lure of the Slums
            var LureOfTheSlumsIcon = AssetLoader.LoadInternal("Skills", "Icon_LureOfTheSlums.png");
            var ShamanHexLureOfTheSlumsResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHexLureOfTheSlumsResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    LevelIncrease = 1,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 1,
                    StartingIncrease = 1,
                    LevelStep = 0,
                    PerStepIncrease = 1,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    }
                };
                bp.m_UseMax = true;
                bp.m_Max = 20;
            });
            var ShamanHexLureOfTheSlumsHoverFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexLureOfTheSlumsHoverFeature", bp => {
                bp.SetName("Lure of the Slums");
                bp.SetDescription("");
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Ground;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ShamanHexLureOfTheSlumsFlyBuff = Helpers.CreateBuff("ShamanHexLureOfTheSlumsFlyBuff", bp => {
                bp.SetName("Lure of the Slums");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your shaman level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = LureOfTheSlumsIcon;
                bp.AddComponent<ACBonusAgainstAttacks>(c => {
                    c.AgainstMeleeOnly = true;
                    c.AgainstRangedOnly = false;
                    c.OnlySneakAttack = false;
                    c.NotTouch = false;
                    c.IsTouch = false;
                    c.OnlyAttacksOfOpportunity = false;
                    c.Value = new ContextValue();
                    c.ArmorClassBonus = 3;
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.CheckArmorCategory = false;
                    c.NoShield = false;
                });
                bp.AddComponent<FormationACBonus>(c => {
                    c.UnitProperty = false;
                    c.Bonus = 3;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ShamanHexLureOfTheSlumsFlyAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("ShamanHexLureOfTheSlumsFlyAbility", bp => {
                bp.SetName("Lure of the Slums");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your shaman level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.m_Icon = LureOfTheSlumsIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = ShamanHexLureOfTheSlumsResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = ShamanHexLureOfTheSlumsFlyBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var ShamanHexLureOfTheSlumsFlyFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexLureOfTheSlumsFlyFeature", bp => {
                bp.SetName("Lure of the Slums");
                bp.SetDescription("Your connection to the skies above is so strong that your feet barely touch the ground. At 1st level, you no longer leave tracks gaining a +1 to stealth. At 5th level, " +
                    "you can hover up to 6 inches above the ground, avoiding difficult terrain. At 10th level you gain the ability to fly along the ground, gaining a +3 dodge bonus to AC against melee attacks, " +
                    "for a number of minutes per day equal to your shaman level. This duration does not need to be consecutive, but it must be spent in 1-minute increments.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShamanHexLureOfTheSlumsFlyAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ShamanHexBadPennyFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexBadPennyFeature", bp => {
                bp.SetName("Bad Penny");
                bp.SetDescription("As a standard action, the shaman can curse a coin, and magically place it in the pockets of a target within 30 feet. " +
                    "The bearer of the cursed coin takes a –2 penalty on all saving throws and skill checks as long he has the coin on his person. " +
                    "Once the coin leaves his person, the curse ends and the coin becomes a mundane piece of tender again. " +
                    "At 8th level, the penalty becomes –4. If the shaman curses a new coin, the previous curse ends. This is a curse effect.");
                bp.m_Icon = LureOfTheSlumsIcon;
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
            #region Starburn
            var MageLightBuff = Resources.GetBlueprint<BlueprintBuff>("571baa4cf65bbcb4996fe429ca77d1a5");
            var ShamanHexStarburnResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHexStarburnResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                };
                bp.m_Min = 1;
            });
            var ShamanHexStarburnBuff = Helpers.CreateBuff("ShamanHexStarburnBuff", bp => {
                bp.SetName("Starburn");
                bp.SetDescription("As a standard action, the shaman causes one creature within 30 feet to burn like a star. The creature takes 1d6 points of fire damage for every " +
                    "2 levels the shaman possesses and emits bright light for 1 round. A successful Fortitude saving throw halves the damage and negates the emission of bright light. " +
                    "The shaman can use this hex a number of times per day equal to her Charisma modifier (minimum 1), but must wait 1d4 rounds between uses.");
                bp.m_Icon = MageLightBuff.Icon;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.GreaterInvisibility;
                });
                bp.FxOnStart = new PrefabLink() { AssetId = "72938ec0a6e4a10459ea374d65aecfa5" };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanHexStarburnAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexStarburnAbility", bp => {
                bp.SetName("Starburn");
                bp.SetDescription("As a standard action, the shaman causes one creature within 30 feet to burn like a star. The creature takes 1d6 points of fire damage for every " +
                    "2 levels the shaman possesses and emits bright light for 1 round. A successful Fortitude saving throw halves the damage and negates the emission of bright light. " +
                    "The shaman can use this hex a number of times per day equal to her Charisma modifier (minimum 1), but must wait 1d4 rounds between uses.");
                bp.m_Icon = MageLightBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
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
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = true,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ShamanHexStarburnBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanHexStarburnResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Wisdom;
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("ShamanHexStarburnAbility.SavingThrow", "Fortitude partial");
            });
            var ShamanHexCitySpiritFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexCitySpiritFeature", bp => {
                bp.SetName("City Spirit");
                bp.SetDescription("As a swift action, the shaman channels the city’s spirit through herself, gaining a +4 bonus on all Dexterity– and Wisdom-based skill checks. " +
                    "She can use this ability for a number of minutes per day equal to 3 + her Charisma modifier. These minutes need not be consecutive.");
                bp.m_Icon = MageLightBuff.m_Icon;
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
            #region Ward of the City

            var ShamanHexWardOfTheCityFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexWardOfTheCityFeature", bp => {
                bp.SetName("Ward of the City");
                bp.SetDescription("The spirit of the city shrouds one creature the shaman touches from the hazards of the slums. " +
                    "The warded creature gains a +5 bonus on saves against disease and poison, and a +25% bonus on percentage chances to negate critical hits and sneak attacks. " +
                    "(This stacks with effects such as fortification, or abilities that grant a creature with no chance to negate critical hits a flat 25% chance.) " +
                    "Each time the ward is used (whether the roll is successful or not), the bonuses are reduced by 1 and 5%, respectively. " +
                    "The ward ends when the bonuses are reduced to 0, when the shaman wards a new creature, or after 24 hours, whichever comes first. " +
                    "At 8th level and 16th level, the ward’s starting bonuses increase by 2 and 10%, respectively. " +
                    "A creature affected by this hex cannot be affected by it again for 24 hours.");
                bp.m_Icon = MageLightBuff.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexWardOfTheCityAbility.ToReference<BlueprintUnitFactReference>()
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
            SpiritTools.RegisterShamanHex(ShamanHexWardOfTheCityFeature);
            #endregion


            ShamanSlumsSpiritProgression.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                ShamanHexAccidentFeature.ToReference<BlueprintFeatureReference>(),
                ShamanHexBadPennyFeature.ToReference<BlueprintFeatureReference>(),
                ShamanHexCitySpiritFeature.ToReference<BlueprintFeatureReference>(),
                ShamanHexWardOfTheCityFeature.ToReference<BlueprintFeatureReference>()
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
