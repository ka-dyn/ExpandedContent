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
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Items.Weapons;

namespace ExpandedContent.Tweaks.Spirits {
    internal class MammothSpirit {
        public static void AddMammothSprit() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var UnswornShamanArchetype = Resources.GetBlueprint<BlueprintArchetype>("556590a43467a27459ac1a80324c9f9f");
            var ShamanHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4223fe18c75d4d14787af196a04e14e7");
            var PossessedShamanSharedSkillSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("9d0477ebd71d43041b419c216b5d6cff");



            #region Spelllist
            var EnlargePersonSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c60969e7f264e6d4b84a1499fdcf9039");
            var BullsStrengthSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4c3d08935262b6544ae97599b3a9556d");
            var RageSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("97b991256e43bb140b263c326f690ce2");
            var StoneskinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c66e86905f7606c4eaa5c774f0357b2b");
            var BeastShapeIIISpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9b93040dad242eb43ac7de6bb6547030");
            var TarPoolSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7d700cdf260d36e48bb7af3a8ca5031f");
            var SummonNaturesAllyVIISpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("051b979e7d7f8ec41b9fa35d04746b33");
            var FrightfulAspectSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e788b02f8d21014488067bdd3ba7b325");
            var PolarMidnightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ba48abb52b142164eba309fd09898856");

            var MammothSpiritSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("MammothSpiritSpellList", bp => {
                bp.IsMythic = false;
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EnlargePersonSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BullsStrengthSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RageSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            StoneskinSpell
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BeastShapeIIISpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TarPoolSpell
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
                            FrightfulAspectSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PolarMidnightSpell
                        }
                    },
                };
            });
            var MammothSpiritSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("MammothSpiritSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = MammothSpiritSpellList.ToReference<BlueprintSpellListReference>();
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
            #region Base
            var HammerblowToggleAbilityIcon = Resources.GetBlueprint<BlueprintActivatableAbility>("7dcd2824b30128a4e864a22ad8af8d46").Icon;
            var DazedBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("d2e35b870e4ac574d9873b36402487e5");
            var ImprovedUnarmedStrike = Resources.GetBlueprint<BlueprintFeature>("7812ad3672a4b9a4fb894ea402095167");

            var ShamanMammothSpiritBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanMammothSpiritBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                };
            });

            var TempImprovedUnarmedStrikeBuff = Helpers.CreateBuff("TempImprovedUnarmedStrikeBuff", bp => {
                bp.SetName("TempImprovedUnarmedStrikeBuff");
                bp.SetDescription("");
                bp.AddComponent(ImprovedUnarmedStrike.GetComponent<EmptyHandWeaponOverride>());
                bp.AddComponent(ImprovedUnarmedStrike.GetComponent<AddMechanicsFeature>());
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = false;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.CheckWeaponBlueprint = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList( new ContextActionRemoveSelf() );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var ShamanMammothSpiritBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanMammothSpiritBaseAbility", bp => {
                bp.SetName("Powerful Smash");
                bp.SetDescription("As a standard action, the shaman can attack an enemy with an unarmed strike as if she had the Improved Unarmed Strike feat. " +
                    "If the shaman hits a creature in this way, that creature must succeed at a Fortitude save " +
                    "(DC = 10 + half the shaman’s class level + her Charisma modifier) or be dazed for 1 round. " +
                    "The shaman can use this ability a number of times per day equal to 3 + her Charisma modifier. " +
                    "\nYou must be unarmed in your main hand to use this ability.");
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = ImprovedUnarmedStrike.ToReference<BlueprintUnitFactReference>(), Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = TempImprovedUnarmedStrikeBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    DurationValue = new ContextDurationValue() {
                                        m_IsExtendable = true,
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue(),
                                        BonusValue = 1
                                    },
                                    IsFromSpell = false,
                                    ToCaster = true
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DazedBuff,
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    DurationSeconds = 0
                                }
                            )
                        });
                });
                bp.AddComponent<AbilityCasterMainWeaponCheck>(c => { c.Category = new WeaponCategory[] { WeaponCategory.UnarmedStrike }; });
                bp.AddComponent<AbilityDeliverAttackWithWeapon>();
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                });

                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanMammothSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = HammerblowToggleAbilityIcon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Weapon;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanMammothSpiritBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanMammothSpiritBaseFeature", bp => {
                bp.SetName("Powerful Smash");
                bp.SetDescription("As a standard action, the shaman can attack an enemy with an unarmed strike as if she had the Improved Unarmed Strike feat. " +
                    "If the shaman hits a creature in this way, that creature must succeed at a Fortitude save " +
                    "(DC = 10 + half the shaman’s class level + her Charisma modifier) or be dazed for 1 round. " +
                    "The shaman can use this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.m_Icon = HammerblowToggleAbilityIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShamanMammothSpiritBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanMammothSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Greater              
            var ShamanMammothSpiritGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanMammothSpiritGreaterFeature", bp => {
                bp.SetName("Strength of the Beast");
                bp.SetDescription("The shaman gains a +2 enhancement bonus to her Strength score. This bonus increases by 2 every 6 shaman levels thereafter " +
                    "(at 14th and 20th levels for her spirit, and at 18th level for her wandering spirit).");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Strength;
                    c.Multiplier = 2;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 8;
                    c.m_StepLevel = 6;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ShamanMammothSpiritGreaterFeatureWandering = Helpers.CreateBlueprint<BlueprintFeature>("ShamanMammothSpiritGreaterFeatureWandering ", bp => {
                bp.SetName("Strength of the Beast");
                bp.SetDescription("The shaman gains a +2 enhancement bonus to her Strength score. This bonus increases by 2 every 6 shaman levels thereafter " +
                    "(at 14th and 20th levels for her spirit, and at 18th level for her wandering spirit).");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Strength;
                    c.Multiplier = 2;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 12;
                    c.m_StepLevel = 6;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region True
            //Just use Nature
            var ShamanNatureSpiritTrueSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("e7f4cfcd7488ac14bbc3e09426b59fd0");
            var ShamanAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("693879b0f26d7e04280510d4dcbf3de1");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var ShamanNatureSpiritWanderingTrueAbility = Resources.GetBlueprint<BlueprintAbility>("ebf81138bb5b42d4eb37ba8f2e1b0b8d");
            var ShamanNatureSpiritWanderingTrueResource = Resources.GetBlueprint<BlueprintAbilityResource>("5d851efd86a54ac44a47869b8986e187");
            #endregion
            #region Manifestation 
            var BeastShapeAbilityI = Resources.GetBlueprint<BlueprintAbility>("61a7ed778dd93f344a5dacdbad324cc9");
            var BeastShapeAbilityII = Resources.GetBlueprint<BlueprintAbility>("5d4028eb28a106d4691ed1b92bbb1915");
            var BeastShapeAbilityIII = Resources.GetBlueprint<BlueprintAbility>("9b93040dad242eb43ac7de6bb6547030");
            var BeastShapeAbilityIVS = Resources.GetBlueprint<BlueprintAbility>("502cd7fd8953ac74bb3a3df7e84818ae");
            var BeastShapeAbilityIVW = Resources.GetBlueprint<BlueprintAbility>("3fa892e5e3efa364fb3d2692738a7c15");
            var BeastShapeIBuff = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b");
            var BeastShapeIIBuff = Resources.GetBlueprint<BlueprintBuff>("200bd9b179ee660489fe88663115bcbc");
            var BeastShapeIIIBuff = Resources.GetBlueprint<BlueprintBuff>("0c0afabcfddeecc40a1545a282f2bec8");
            var BeastShapeIVSmilodonBuff = Resources.GetBlueprint<BlueprintBuff>("c38def68f6ce13b4b8f5e5e0c6e68d08");
            var BeastShapeIVWyvernBuff = Resources.GetBlueprint<BlueprintBuff>("dae2d173d9bd5b14dbeb4a1d9d9b0edc");
            var ShamanMammothSpiritManifestationAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanMammothSpiritManifestationAbility", bp => {
                bp.SetName("Beast Shape Manifestation");
                bp.SetDescription("At 20th level, the shaman can transform into any animal form granted by the beast shape spells, able to activate and dismiss " +
                    "the ability as often as she likes.");

                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BeastShapeIVSmilodonBuff.Icon;
                bp.Type = AbilityType.Spell;
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
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanMammothSpiritManifestationAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanMammothSpiritManifestationAbilityI = Helpers.CreateBlueprint<BlueprintAbility>("ShamanMammothSpiritManifestationAbilityI", bp => {
                bp.SetName("Beast Shape Manifestation (Wolf)");
                bp.m_Description = BeastShapeAbilityI.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 20
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BeastShapeIBuff.Icon;
                bp.m_Parent = ShamanMammothSpiritManifestationAbility.ToReference<BlueprintAbilityReference>();
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
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanMammothSpiritManifestationAbilityI.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanMammothSpiritManifestationAbilityII = Helpers.CreateBlueprint<BlueprintAbility>("ShamanMammothSpiritManifestationAbilityII", bp => {
                bp.SetName("Beast Shape Manifestation (Leopard)");
                bp.m_Description = BeastShapeAbilityII.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 20
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BeastShapeIIBuff.Icon;
                bp.m_Parent = ShamanMammothSpiritManifestationAbility.ToReference<BlueprintAbilityReference>();
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
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanMammothSpiritManifestationAbilityII.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanMammothSpiritManifestationAbilityIII = Helpers.CreateBlueprint<BlueprintAbility>("ShamanMammothSpiritManifestationAbilityIII", bp => {
                bp.SetName("Beast Shape Manifestation (Bear)");
                bp.m_Description = BeastShapeAbilityIII.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIIIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 20
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIIIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BeastShapeIIIBuff.Icon;
                bp.m_Parent = ShamanMammothSpiritManifestationAbility.ToReference<BlueprintAbilityReference>();
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
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanMammothSpiritManifestationAbilityIII.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanMammothSpiritManifestationAbilityIVS = Helpers.CreateBlueprint<BlueprintAbility>("ShamanMammothSpiritManifestationAbilityIVS", bp => {
                bp.SetName("Beast Shape Manifestation (Smilodon)");
                bp.m_Description = BeastShapeAbilityIVS.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIVSmilodonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 20
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIVSmilodonBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BeastShapeIVSmilodonBuff.Icon;
                bp.m_Parent = ShamanMammothSpiritManifestationAbility.ToReference<BlueprintAbilityReference>();
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
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanMammothSpiritManifestationAbilityIVS.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanMammothSpiritManifestationAbilityIVW = Helpers.CreateBlueprint<BlueprintAbility>("ShamanMammothSpiritManifestationAbilityIVW", bp => {
                bp.SetName("Beast Shape Manifestation (Wyvern)");
                bp.m_Description = BeastShapeAbilityIVW.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIVWyvernBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 20
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIVWyvernBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BeastShapeIVWyvernBuff.Icon;
                bp.m_Parent = ShamanMammothSpiritManifestationAbility.ToReference<BlueprintAbilityReference>();
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
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanMammothSpiritManifestationAbilityIVW.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            ShamanMammothSpiritManifestationAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                        ShamanMammothSpiritManifestationAbilityI.ToReference<BlueprintAbilityReference>(),
                        ShamanMammothSpiritManifestationAbilityII.ToReference<BlueprintAbilityReference>(),
                        ShamanMammothSpiritManifestationAbilityIII.ToReference<BlueprintAbilityReference>(),
                        ShamanMammothSpiritManifestationAbilityIVS.ToReference<BlueprintAbilityReference>(),
                        ShamanMammothSpiritManifestationAbilityIVW.ToReference<BlueprintAbilityReference>()
                    };
            });


            var ShamanMammothSpiritManifestationFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanMammothSpiritManifestationFeature", bp => {
                bp.SetName("Manifestation");
                bp.SetDescription("At 20th level, the shaman can transform into any animal form granted by the beast shape spells, able to activate and dismiss " +
                    "the ability as often as she likes.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanMammothSpiritManifestationAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #endregion
            #region Progression
            var ShamanMammothSpiritProgression = Helpers.CreateBlueprint<BlueprintProgression>("ShamanMammothSpiritProgression", bp => {
                bp.SetName("Mammoth");
                bp.SetDescription("A shaman who selects the mammoth spirit is abnormally tall and stocky, with thick shaggy hair. When she uses a " +
                    "special ability of this spirit, her muscles ripple and flex, and her stature seems even greater than before.");
                //bp.AddComponent<AddFeaturesFromSelectionToDescription>(c => {
                //    c.SetIntroduction("Additional Hexes:");
                //    c.m_FeatureSelection = ShamanHexSelection.ToReference<BlueprintFeatureSelectionReference>();
                //    c.OnlyIfRequiresThisFeature = true;
                //});
                bp.AddComponent<AddSpellsToDescription>(c => {
                    c.SetIntroduction("Bonus Spells:");
                    c.m_SpellLists = new BlueprintSpellListReference[] { MammothSpiritSpellList.ToReference<BlueprintSpellListReference>() };
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
                    Helpers.LevelEntry(1, ShamanMammothSpiritBaseFeature, MammothSpiritSpellListFeature),
                    Helpers.LevelEntry(8, ShamanMammothSpiritGreaterFeature),
                    Helpers.LevelEntry(16, ShamanNatureSpiritTrueSelection, ShamanAnimalCompanionProgression, AnimalCompanionRank),
                    Helpers.LevelEntry(20, ShamanMammothSpiritManifestationFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            #endregion
            #region Wandering Spirit
            var ShamanMammothSpiritWanderingTrueBuff = Helpers.CreateBuff("ShamanMammothSpiritWanderingTrueBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanAnimalCompanionProgression.ToReference<BlueprintUnitFactReference>(),
                        ShamanNatureSpiritWanderingTrueAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanNatureSpiritWanderingTrueResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = false;
                    c.RestoreOnLevelUp = false;
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanMammothSpiritWanderingGreaterBuff = Helpers.CreateBuff("ShamanMammothSpiritWanderingGreaterBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanMammothSpiritGreaterFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanMammothSpiritWanderingBaseBuff = Helpers.CreateBuff("ShamanMammothSpiritWanderingBaseBuff", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanMammothSpiritBaseFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanMammothSpiritWanderingFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanMammothSpiritWanderingFeature", bp => {
                bp.SetName("Mammoth");
                bp.SetDescription("A shaman who selects the mammoth spirit is abnormally tall and stocky, with thick shaggy hair. When she uses a " +
                    "special ability of this spirit, her muscles ripple and flex, and her stature seems even greater than before.");
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
                                    m_Buff = ShamanMammothSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
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
                                    m_Buff = ShamanMammothSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
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
                            m_Buff = ShamanMammothSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
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
                            m_Buff = ShamanMammothSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanMammothSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanMammothSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MammothSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanMammothSpiritProgression.ToReference<BlueprintFeatureReference>();
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
            var UnswornShamanMammothSpiritWanderingFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("UnswornShamanMammothSpiritWanderingFeature1", bp => {
                bp.SetName("Mammoth");
                bp.SetDescription("A shaman who selects the mammoth spirit is abnormally tall and stocky, with thick shaggy hair. When she uses a " +
                    "special ability of this spirit, her muscles ripple and flex, and her stature seems even greater than before.");
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
                                    m_Buff = ShamanMammothSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
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
                                    m_Buff = ShamanMammothSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
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
                            m_Buff = ShamanMammothSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
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
                            m_Buff = ShamanMammothSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanMammothSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanMammothSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MammothSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanMammothSpiritProgression.ToReference<BlueprintFeatureReference>();
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
            var UnswornShamanMammothSpiritWanderingFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("UnswornShamanMammothSpiritWanderingFeature2", bp => {
                bp.SetName("Mammoth");
                bp.SetDescription("A shaman who selects the mammoth spirit is abnormally tall and stocky, with thick shaggy hair. When she uses a " +
                    "special ability of this spirit, her muscles ripple and flex, and her stature seems even greater than before.");
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
                                    m_Buff = ShamanMammothSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>(),
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
                                    m_Buff = ShamanMammothSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>(),
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
                            m_Buff = ShamanMammothSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>(),
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
                            m_Buff = ShamanMammothSpiritWanderingTrueBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanMammothSpiritWanderingGreaterBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = ShamanMammothSpiritWanderingBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MammothSpiritSpellListFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.HideInUI = false;
                    c.m_Feature = ShamanMammothSpiritProgression.ToReference<BlueprintFeatureReference>();
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

            //#region Mammoth’s Hide
            //var SenseiMythicWisdomIcon = Resources.GetBlueprint<BlueprintFeature>("4356b5d6d34489747bba68d43924a857").Icon;
            //var ShamanHexCitySpiritResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHexCitySpiritResource", bp => {
            //    bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
            //        BaseValue = 3,
            //        IncreasedByLevel = false,
            //        IncreasedByStat = true,
            //        ResourceBonusStat = StatType.Charisma
            //    };
            //});
            //var ShamanHexCitySpiritBuff = Helpers.CreateBuff("ShamanHexCitySpiritBuff", bp => {
            //    bp.SetName("City Spirit");
            //    bp.SetDescription("As a swift action, the shaman channels the city’s spirit through herself, gaining a +4 bonus on all Dexterity– and Wisdom-based skill checks. " +
            //        "She can use this ability for a number of rounds per day equal to 3 + her Charisma modifier. These rounds need not be consecutive.");
            //    bp.m_Icon = SenseiMythicWisdomIcon;
            //    bp.AddComponent<AddStatBonus>(c => {
            //        c.Stat = StatType.SkillMobility;
            //        c.Descriptor = ModifierDescriptor.UntypedStackable;
            //        c.Value = 4;
            //    });
            //    bp.AddComponent<AddStatBonus>(c => {
            //        c.Stat = StatType.SkillThievery;
            //        c.Descriptor = ModifierDescriptor.UntypedStackable;
            //        c.Value = 4;
            //    });
            //    bp.AddComponent<AddStatBonus>(c => {
            //        c.Stat = StatType.SkillLoreNature;
            //        c.Descriptor = ModifierDescriptor.UntypedStackable;
            //        c.Value = 4;
            //    });
            //    bp.AddComponent<AddStatBonus>(c => {
            //        c.Stat = StatType.SkillLoreReligion;
            //        c.Descriptor = ModifierDescriptor.UntypedStackable;
            //        c.Value = 4;
            //    });
            //    bp.m_AllowNonContextActions = false;
            //    bp.IsClassFeature = false;
            //    bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
            //    bp.Stacking = StackingType.Replace;
            //});
            //var ShamanHexCitySpiritAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("ShamanHexCitySpiritAbility", bp => {
            //    bp.SetName("City Spirit");
            //    bp.SetDescription("As a swift action, the shaman channels the city’s spirit through herself, gaining a +4 bonus on all Dexterity– and Wisdom-based skill checks. " +
            //        "She can use this ability for a number of rounds per day equal to 3 + her Charisma modifier. These rounds need not be consecutive.");
            //    bp.m_Icon = SenseiMythicWisdomIcon;
            //    bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
            //        c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
            //        c.m_RequiredResource = ShamanHexCitySpiritResource.ToReference<BlueprintAbilityResourceReference>();
            //    });
            //    bp.m_Buff = ShamanHexCitySpiritBuff.ToReference<BlueprintBuffReference>();
            //    bp.AddComponent<SpellDescriptorComponent>(c => {
            //        c.Descriptor = SpellDescriptor.Hex;
            //    });
            //    bp.DeactivateIfOwnerDisabled = true;
            //    bp.ActivationType = AbilityActivationType.WithUnitCommand;
            //    bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
            //    bp.DeactivateIfCombatEnded = false;
            //});
            //var ShamanHexCitySpiritFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexCitySpiritFeature", bp => {
            //    bp.SetName("City Spirit");
            //    bp.SetDescription("As a swift action, the shaman channels the city’s spirit through herself, gaining a +4 bonus on all Dexterity– and Wisdom-based skill checks. " +
            //        "She can use this ability for a number of rounds per day equal to 3 + her Charisma modifier. These rounds need not be consecutive.");
            //    bp.m_Icon = SenseiMythicWisdomIcon;
            //    bp.AddComponent<AddFacts>(c => {
            //        c.m_Facts = new BlueprintUnitFactReference[] {
            //            ShamanHexCitySpiritAbility.ToReference<BlueprintUnitFactReference>()
            //        };
            //    });
            //    bp.AddComponent<AddAbilityResources>(c => {
            //        c.m_Resource = ShamanHexCitySpiritResource.ToReference<BlueprintAbilityResourceReference>();
            //        c.RestoreAmount = true;
            //    });
            //    bp.AddComponent<PrerequisiteFeature>(c => {
            //        c.Group = Prerequisite.GroupType.Any;
            //        c.CheckInProgression = false;
            //        c.HideInUI = false;
            //        c.m_Feature = ShamanMammothSpiritProgression.ToReference<BlueprintFeatureReference>();
            //    });
            //    bp.AddComponent<PrerequisiteFeature>(c => {
            //        c.Group = Prerequisite.GroupType.Any;
            //        c.CheckInProgression = false;
            //        c.HideInUI = false;
            //        c.m_Feature = ShamanMammothSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
            //    });
            //    bp.AddComponent<PrerequisiteFeature>(c => {
            //        c.Group = Prerequisite.GroupType.Any;
            //        c.CheckInProgression = false;
            //        c.HideInUI = false;
            //        c.m_Feature = ShamanMammothSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
            //    });
            //    bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
            //    bp.m_AllowNonContextActions = false;
            //    bp.IsClassFeature = true;
            //});
            //SpiritTools.RegisterShamanHex(ShamanHexCitySpiritFeature);
            //#endregion


            //ShamanMammothSpiritProgression.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
            //    QShamanHexAccidentFeature.ToReference<BlueprintFeatureReference>(),
            //    QShamanHexBadPennyFeature.ToReference<BlueprintFeatureReference>(),
            //    QShamanHexCitySpiritFeature.ToReference<BlueprintFeatureReference>()
            //};
            #endregion


            SpiritTools.RegisterSpirit(ShamanMammothSpiritProgression);
            SpiritTools.RegisterSecondSpirit(ShamanMammothSpiritProgression);
            SpiritTools.RegisterWanderingSpirit(ShamanMammothSpiritWanderingFeature);
            SpiritTools.RegisterUnswornSpirit1(UnswornShamanMammothSpiritWanderingFeature1);
            SpiritTools.RegisterUnswornSpirit2(UnswornShamanMammothSpiritWanderingFeature2);


        }
    }
}
