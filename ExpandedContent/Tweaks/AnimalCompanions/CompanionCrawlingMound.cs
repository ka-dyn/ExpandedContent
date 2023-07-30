using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.AnimalCompanions {
    internal class CompanionCrawlingMound {
        public static void AddCompanionCrawlingMound() {

            var AnimalCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("26b10d4340839004f960f9816f6109fe");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var ShamblingMoundBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("2b16730449d17104fa90b38ac310a547");
            var CR6ShamblingMoundUnit = Resources.GetBlueprint<BlueprintUnit>("3ba7ed5832a44bae8a549c00455d8bde");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var Neutrals = Resources.GetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
            var WeaponEmptyHand = Resources.GetBlueprint<BlueprintItemWeapon>("20375b5a0c9243d45966bd72c690ab74");
            var Slam1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("7445b0b255796d34495a8bca81b2e2d4");
            var NaturalArmor6 = Resources.GetBlueprint<BlueprintUnitFact>("987ba44303e88054c9504cb3083ba0c9");
            var NaturalArmor7 = Resources.GetBlueprint<BlueprintUnitFact>("e73864391ccf0894997928443a29d755");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var TripDefetrrghergeenceFourLegs = Resources.GetBlueprint<BlueprintFeature>("136fa0343d5b4b348bdaa05d83408db3");
            var AnimalCompanionSlotFeature = Resources.GetBlueprint<BlueprintFeature>("75bb2b3c41c99e041b4743fdb16a4289");
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var EntangleSpell = Resources.GetBlueprint<BlueprintAbility>("0fd00984a2c0e0a429cf1a911b4ec5ca");
            var ShamblingMoundReleaseGrappleAbility = Resources.GetBlueprint<BlueprintAbility>("7b699fbf988944b09371b1e08cda6885");
            var ShamblingMoundGrapActivatableAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("d170e5e6e98a4229a4f394dd2248151a");
            var ShamblingMoundGrappledInitiatorBuff = Resources.GetBlueprint<BlueprintBuff>("e6bf1a4533604de698b634992d4894c8");
            var ShamblingMoundGrappledTargetBuff = Resources.GetBlueprint<BlueprintBuff>("2b5743ae1c3e478ab99defebcc881019");
            var UnmountableFeature = Resources.GetModBlueprint<BlueprintFeature>("UnmountableFeature");
            var SlamType = Resources.GetBlueprint<BlueprintWeaponType>("f18cbcb39a1b35643a8d129b1ec4e716");

            var CompanionCrawlingMoundConstrictFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionCrawlingMoundConstrictFeature", bp => {
                bp.SetName("Constrict");
                bp.SetDescription("Whenever the crawling mound's vines coil successfully grapples a creature, they take {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g}. " +
                    "Everyround the grappled creature fails to escape the grapple they take {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g}.");                
                bp.IsClassFeature = true;
            });

            var CompanionCrawlingMoundGrappleTargetBuff = Helpers.CreateBuff("CompanionCrawlingMoundGrappleTargetBuff", bp => {
                bp.SetName("Constricting Vines");
                bp.SetDescription("A crawling mound's vines coil around any creature it hits with a slam {g|Encyclopedia:Attack}attack{/g}. The crawling mound attempts a grapple " +
                    "{g|Encyclopedia:Combat_Maneuvers}maneuver{/g} {g|Encyclopedia:Check}check{/g} against its target, on a successful check the foe is grappled. \nGrappled characters " +
                    "cannot move, and take a -2 {g|Encyclopedia:Penalty}penalty{/g} on all attack rolls and a -4 penalty to {g|Encyclopedia:Dexterity}Dexterity{/g}. Grappled characters " +
                    "attempt to escape every {g|Encyclopedia:Combat_Round}round{/g} by making a successful combat maneuver, {g|Encyclopedia:Strength}Strength{/g}, {g|Encyclopedia:Athletics}Athletics{/g}, " +
                    "or {g|Encyclopedia:Mobility}Mobility check{/g}. The {g|Encyclopedia:DC}DC{/g} of this check is the crawling mound's {g|Encyclopedia:CMD}CMD{/g}.");
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<ShifterGrabTargetBuff>(c => {
                    c.m_AttackRollBonus = -2;
                    c.m_DexterityBonus = -4;
                    c.m_PinnedACBonus = -4;
                });
                bp.m_Icon = ShamblingMoundGrappledTargetBuff.Icon;
                bp.IsClassFeature = false;
                bp.FxOnStart = new PrefabLink() { AssetId = "063ff6e114b9ff94c9b32ea0e5567c6a" };
            });
            var CompanionCrawlingMoundGrappleTargetBuffUpgraded = Helpers.CreateBuff("CompanionCrawlingMoundGrappleTargetBuffUpgraded", bp => {
                bp.SetName("Constricting Vines");
                bp.SetDescription("A crawling mound's vines coil around any creature it hits with a slam {g|Encyclopedia:Attack}attack{/g}. The crawling mound attempts a grapple " +
                    "{g|Encyclopedia:Combat_Maneuvers}maneuver{/g} {g|Encyclopedia:Check}check{/g} against its target, on a successful check the foe is grappled. \nGrappled characters " +
                    "cannot move, and take a -2 {g|Encyclopedia:Penalty}penalty{/g} on all attack rolls and a -4 penalty to {g|Encyclopedia:Dexterity}Dexterity{/g}. Grappled characters " +
                    "attempt to escape every {g|Encyclopedia:Combat_Round}round{/g} by making a successful combat maneuver, {g|Encyclopedia:Strength}Strength{/g}, {g|Encyclopedia:Athletics}Athletics{/g}, " +
                    "or {g|Encyclopedia:Mobility}Mobility check{/g}. The {g|Encyclopedia:DC}DC{/g} of this check is the crawling mound's {g|Encyclopedia:CMD}CMD{/g}. \nWhen a creature is successfully " +
                    "grappled or spends a whole round failing to escape the grapple, they are dealt {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g}.");
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<ShifterGrabTargetBuff>(c => {
                    c.m_AttackRollBonus = -2;
                    c.m_DexterityBonus = -4;
                    c.m_PinnedACBonus = -4;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Physical,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Bludgeoning | PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
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
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.DamageDice,
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
                            HalfIfSaved = false,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Physical,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Bludgeoning | PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
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
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.DamageDice,
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
                            HalfIfSaved = false,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.m_Icon = ShamblingMoundGrappledTargetBuff.Icon;
                bp.IsClassFeature = false;
                bp.FxOnStart = new PrefabLink() { AssetId = "063ff6e114b9ff94c9b32ea0e5567c6a" };
            });




            var CompanionCrawlingMoundGrappleBuff = Helpers.CreateBuff("CompanionCrawlingMoundGrappleBuff", bp => {
                bp.SetName("Constricting Vines");
                bp.SetDescription("A crawling mound's vines coil around any creature it hits with a slam {g|Encyclopedia:Attack}attack{/g}. The crawling mound attempts a grapple " +
                    "{g|Encyclopedia:Combat_Maneuvers}maneuver{/g} {g|Encyclopedia:Check}check{/g} against its target, on a successful check the foe is grappled. \nGrappled characters " +
                    "cannot move, and take a -2 {g|Encyclopedia:Penalty}penalty{/g} on all attack rolls and a -4 penalty to {g|Encyclopedia:Dexterity}Dexterity{/g}. Grappled characters " +
                    "attempt to escape every {g|Encyclopedia:Combat_Round}round{/g} by making a successful combat maneuver, {g|Encyclopedia:Strength}Strength{/g}, {g|Encyclopedia:Athletics}Athletics{/g}, " +
                    "or {g|Encyclopedia:Mobility}Mobility check{/g}. The {g|Encyclopedia:DC}DC{/g} of this check is the crawling mound's {g|Encyclopedia:CMD}CMD{/g}.");
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.OnlyHit = true;
                    c.m_WeaponType = SlamType.ToReference<BlueprintWeaponTypeReference>();
                    c.Action = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = CompanionCrawlingMoundConstrictFeature.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                               new ContextActionCombatManeuverCustom() {
                                   Type = CombatManeuver.Grapple,
                                   Success = Helpers.CreateActionList(
                                       new ContextActionGrapple() {
                                           m_CasterBuff = ShamblingMoundGrappledInitiatorBuff.ToReference<BlueprintBuffReference>(),
                                           m_TargetBuff = CompanionCrawlingMoundGrappleTargetBuffUpgraded.ToReference<BlueprintBuffReference>()
                                       }
                                       ),
                                   Failure = Helpers.CreateActionList()
                               }
                               ),
                            IfFalse = Helpers.CreateActionList(
                               new ContextActionCombatManeuverCustom() {
                                   Type = CombatManeuver.Grapple,
                                   Success = Helpers.CreateActionList(
                                       new ContextActionGrapple() {
                                           m_CasterBuff = ShamblingMoundGrappledInitiatorBuff.ToReference<BlueprintBuffReference>(),
                                           m_TargetBuff = CompanionCrawlingMoundGrappleTargetBuff.ToReference<BlueprintBuffReference>()
                                       }
                                       ),
                                   Failure = Helpers.CreateActionList()
                               }
                               )
                        });
                });

                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
            });

            var CompanionCrawlingMoundGrabActivatableAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("CompanionCrawlingMoundGrabActivatableAbility", bp => {
                bp.SetName("Constricting Vines");
                bp.SetDescription("A crawling mound's vines coil around any creature it hits with a slam {g|Encyclopedia:Attack}attack{/g}. The crawling mound attempts a grapple " +
                    "{g|Encyclopedia:Combat_Maneuvers}maneuver{/g} {g|Encyclopedia:Check}check{/g} against its target, on a successful check the foe is grappled. \nGrappled characters " +
                    "cannot move, and take a -2 {g|Encyclopedia:Penalty}penalty{/g} on all attack rolls and a -4 penalty to {g|Encyclopedia:Dexterity}Dexterity{/g}. Grappled characters " +
                    "attempt to escape every {g|Encyclopedia:Combat_Round}round{/g} by making a successful combat maneuver, {g|Encyclopedia:Strength}Strength{/g}, {g|Encyclopedia:Athletics}Athletics{/g}, " +
                    "or {g|Encyclopedia:Mobility}Mobility check{/g}. The {g|Encyclopedia:DC}DC{/g} of this check is the crawling mound's {g|Encyclopedia:CMD}CMD{/g}.");
                bp.m_Icon = ShamblingMoundGrapActivatableAbility.Icon;
                bp.m_Buff = CompanionCrawlingMoundGrappleBuff.ToReference<BlueprintBuffReference>();
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var CompanionCrawlingMoundGrabFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionCrawlingMoundGrabFeature", bp => {
                bp.SetName("Constricting Vines");
                bp.SetDescription("A crawling mound's vines coil around any creature it hits with a slam {g|Encyclopedia:Attack}attack{/g}. The crawling mound attempts a grapple " +
                    "{g|Encyclopedia:Combat_Maneuvers}maneuver{/g} {g|Encyclopedia:Check}check{/g} against its target, on a successful check the foe is grappled. \nGrappled characters " +
                    "cannot move, and take a -2 {g|Encyclopedia:Penalty}penalty{/g} on all attack rolls and a -4 penalty to {g|Encyclopedia:Dexterity}Dexterity{/g}. Grappled characters " +
                    "attempt to escape every {g|Encyclopedia:Combat_Round}round{/g} by making a successful combat maneuver, {g|Encyclopedia:Strength}Strength{/g}, {g|Encyclopedia:Athletics}Athletics{/g}, " +
                    "or {g|Encyclopedia:Mobility}Mobility check{/g}. The {g|Encyclopedia:DC}DC{/g} of this check is the crawling mound's {g|Encyclopedia:CMD}CMD{/g}.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamblingMoundReleaseGrappleAbility.ToReference<BlueprintUnitFactReference>(),
                        CompanionCrawlingMoundGrabActivatableAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = true;
            });

            var CompanionNotUpgradedCrawlingMound = Helpers.CreateBlueprint<BlueprintFeature>("CompanionNotUpgradedCrawlingMound", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -2;
                    c.Size = Size.Fine;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NaturalArmor6.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpgradeCrawlingMound = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpgradeCrawlingMound", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 8;
                    c.Stat = StatType.Strength;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = -2;
                    c.Stat = StatType.Dexterity;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 4;
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NaturalArmor7.ToReference<BlueprintUnitFactReference>(),
                        CompanionCrawlingMoundConstrictFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.IterativeNaturalAttacks;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpdateCrawlingMoundFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpdateCrawlingMoundFeature", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -1;
                    c.Size = Size.Fine;
                });
                bp.AddComponent<ChangeImpatience>(c => {
                    c.Delta = -1;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 4;
                    c.m_Feature = CompanionNotUpgradedCrawlingMound.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 4;
                    c.m_Feature = CompanionUpgradeCrawlingMound.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });

            var CompanionCrawlingMoundPortrait = Helpers.CreateBlueprint<BlueprintPortrait>("CompanionCrawlingMoundPortrait", bp => {
                bp.Data = PortraitLoader.LoadPortraitData("CrawlingMound");
            });
            var CompanionCrawlingMoundUnit = Helpers.CreateBlueprint<BlueprintUnit>("CompanionCrawlingMoundUnit", bp => {
                bp.SetLocalisedName("Crawling Mound");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Strength;
                    c.Skills = new StatType[] { };
                    // Do I need Selections???
                    c.DoNotApplyAutomatically = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AllowDyingCondition>();
                bp.AddComponent<AddResurrectOnRest>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Huge;
                bp.Color = CR6ShamblingMoundUnit.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = CompanionCrawlingMoundPortrait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "8c6566ffd28ca3c4ea932a0a83398a71" };
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Vegetable,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = ShamblingMoundBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Wood,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootLarge,
                    FootSoundType = FootSoundType.Foot,
                    FootSoundSize = Size.Large,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Large,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Neutrals.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.m_Brain = CharacterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = false,
                    m_EmptyHandWeapon = WeaponEmptyHand.ToReference<BlueprintItemWeaponReference>(),
                    m_PrimaryHand = Slam1d4.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = Slam1d4.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = Slam1d4.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = Slam1d4.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[0] { }
                };
                bp.Strength = 13;
                bp.Dexterity = 17;
                bp.Constitution = 13;
                bp.Wisdom = 12;
                bp.Intelligence = 1;
                bp.Charisma = 2;
                bp.Speed = new Feet(20);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    CompanionUpdateCrawlingMoundFeature.ToReference<BlueprintUnitFactReference>(),
                    PlantType.ToReference<BlueprintUnitFactReference>(),
                    UnmountableFeature.ToReference<BlueprintUnitFactReference>(),
                    CompanionCrawlingMoundGrabFeature.ToReference<BlueprintUnitFactReference>(),
                    AnimalCompanionSlotFeature.ToReference<BlueprintUnitFactReference>()
                };
            });

            EyePortraitInjecotr.Replacements[CompanionCrawlingMoundUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "CrawlingMoundPetEye.png", new Vector2Int(176, 24), TextureFormat.RGBA32);

            var CompanionCrawlingMoundFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionCrawlingMoundFeature", bp => {
                bp.SetName("Animal Companion — Crawling Mound");
                bp.SetDescription("{g|Encyclopedia:Size}Size{/g}: Medium" +
                    "\n{g|Encyclopedia:Speed}Speed{/g}: 20 ft." +
                    "\n{g|Encyclopedia:Armor_Class}AC{/g}: +6 natural armor" +
                    "\n{g|Encyclopedia:Attack}Attack{/g}: slam ({g|Encyclopedia:Dice}1d4{/g}) + grab" +
                    "\n{g|Encyclopedia:Ability_Scores}Ability scores{/g}: {g|Encyclopedia:Strength}Str{/g} 13, {g|Encyclopedia:Dexterity}Dex{/g} 17, {g|Encyclopedia:Constitution}Con{/g} 13, {g|Encyclopedia:Intelligence}Int{/g} 1, {g|Encyclopedia:Wisdom}Wis{/g} 12, {g|Encyclopedia:Charisma}Cha{/g} 2" +
                    "\nAt 4th level size becomes Large, Str +8, Dex -2, Con +4, +1 natural armor, iterative attacks, and grab now deals 1d6 damage on successful grapple and each round." +
                    "\nThis animal companion can't be used as a mount.");
                bp.m_Icon = EntangleSpell.m_Icon;
                bp.AddComponent<AddPet>(c => {
                    c.Type = PetType.AnimalCompanion;
                    c.ProgressionType = PetProgressionType.AnimalCompanion;
                    c.m_Pet = CompanionCrawlingMoundUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => {
                    c.NoCompanion = true;
                    c.Type = PetType.AnimalCompanion;
                    c.HideInUI = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });

            //Does not need to be added to pet selections

        }
    }
}
