using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UI.GenericSlot;
using Kingmaker.Blueprints.Items.Ecnchantments;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using ExpandedContent.Tweaks.Components;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Archer {
        public static void AddArcher() {

            var FighterClass = Resources.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var BraveryFeature = Resources.GetBlueprint<BlueprintFeature>("f6388946f9f472f4585591b80e9f2452");
            var ArmorTrainingFeature = Resources.GetBlueprint<BlueprintFeature>("3c380607706f209499d951b29d3c44f3");
            var WeaponTrainingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b8cecf4e5e464ad41b79d5b42b76b399");
            var WeaponTrainingRankUpSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5f3cc7b9a46b880448275763fe70c0b0");
            var ArmorMasteryFeature = Resources.GetBlueprint<BlueprintFeature>("ae177f17cfb45264291d4d7c2cb64671");
            var SenseVitals = Resources.GetBlueprint<BlueprintAbility>("82962a820ebc0e7408b8582fdc3f4c0c");
            var CrushingBlowFeature = Resources.GetBlueprint<BlueprintFeature>("4153755355a0b9b4e956c9ca232c22cf");
            var DisarmAction = Resources.GetBlueprint<BlueprintAbility>("45d94c6db453cfc4a9b99b72d6afe6f6");
            var ImprovedBullRushFeature = Resources.GetBlueprint<BlueprintFeature>("b3614622866fe7046b787a548bbd7f59");
            var PummelingStyleBuff = Resources.GetBlueprint<BlueprintBuff>("8cb3816915b1a8348b3872b964a2fa23");
            var AgileManeuversFeature = Resources.GetBlueprint<BlueprintFeature>("197306972c98bb843af738dc7529a7ac");
            var DeflectArrowsFeature = Resources.GetBlueprint<BlueprintFeature>("2c61fdbf242866f4e93c3e1477fb96b5");

            var ArcherVolleyIcon = AssetLoader.LoadInternal("Skills", "Icon_ArcherVolley.jpg");
            var TripIcon = AssetLoader.LoadInternal("Skills", "Icon_Trip.jpg");


            var ArcherArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("ArcherArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"ArcherArchetype.Name", "Archer");
                bp.LocalizedDescription = Helpers.CreateString($"ArcherArchetype.Description", "The archer is dedicated to the careful mastery of the bow, perfecting his skills with years of practice " +
                    "honed day after day on ranges and hunting for game, or else on the battlefield, raining destruction down on the enemy lines.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"ArcherArchetype.Description", "The archer is dedicated to the careful mastery of the bow, perfecting his skills with years of " +
                    "practice honed day after day on ranges and hunting for game, or else on the battlefield, raining destruction down on the enemy lines.");
                bp.OverrideAttributeRecommendations = true;
                bp.RecommendedAttributes = new StatType[] { StatType.Dexterity };
                bp.NotRecommendedAttributes = new StatType[] { StatType.Charisma };
                
            });
            var HawkeyeFeature = Helpers.CreateBlueprint<BlueprintFeature>("HawkeyeFeature", bp => {
                bp.SetName("Hawkeye");
                bp.SetDescription("At 2nd level, an archer gains a +1 bonus on Perception checks. This bonus increases by +1 for every 4 levels beyond 2nd.");
                bp.m_Icon = SenseVitals.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
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
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 2;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] { FighterClass.ToReference<BlueprintCharacterClassReference>() };
                });
            });

            var TrickShotDisarmBuff = Helpers.CreateBuff("TrickShotDisarmBuff", bp => {
                bp.SetName("Trick Shot - Disarm");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the disarm combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = DisarmAction.m_Icon;
                bp.AddComponent<ManeuverBonus>(c => {
                    c.Type = CombatManeuver.Disarm;
                    c.Mythic = false;
                    c.Bonus = -4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
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
                    c.Action = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Disarm,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = true;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var TrickShotSunderBuff = Helpers.CreateBuff("TrickShotSunderBuff", bp => {
                bp.SetName("Trick Shot - Sunder");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the sunder combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = CrushingBlowFeature.m_Icon;
                bp.AddComponent<ManeuverBonus>(c => {
                    c.Type = CombatManeuver.SunderArmor;
                    c.Mythic = false;
                    c.Bonus = -4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
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
                    c.Action = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.SunderArmor,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = true;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(                        
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var TrickShotBullRushBuff = Helpers.CreateBuff("TrickShotBullRushBuff", bp => {
                bp.SetName("Trick Shot - Bull Rush");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the bull rush combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = ImprovedBullRushFeature.m_Icon;
                bp.AddComponent<ManeuverBonus>(c => {
                    c.Type = CombatManeuver.BullRush;
                    c.Mythic = false;
                    c.Bonus = -4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
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
                    c.Action = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.BullRush,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = true;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });

            var BearGrappleTargetBuff = Resources.GetBlueprint<BlueprintBuff>("88be6cbfaf534e009c501e1d2ef3c1f6");
            var Haste = Resources.GetBlueprint<BlueprintBuff>("8d20b0a6129bd814eb0146041879f38a");

            var TrickShotGrappleCasterBuff = Helpers.CreateBuff("TrickShotGrappleCasterBuff", bp => { //Looks like I didn't need this, it's only still here so my own ongoing save does not break :)
                bp.m_AllowNonContextActions = false;
                bp.SetName("Test Stuff");
                bp.SetDescription("");
                bp.m_Icon = Haste.Icon;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var TrickShotGrappleTargetBuff = Helpers.CreateBuff("TrickShotGrappleTargetBuff", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Pinned by arrows");
                bp.SetDescription("A target grappled by an arrow can attempt to escape every {g|Encyclopedia:Combat_Round}round{/g} by making a successful combat " +
                    "maneuver, {g|Encyclopedia:Strength}Strength{/g}, {g|Encyclopedia:Athletics}Athletics{/g}, or {g|Encyclopedia:Mobility}Mobility check{/g} against the archers " +
                    "CMD. The pinned target gets a +4 bonus on this check.");
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.CantMove;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Entangled;
                });
                bp.AddComponent<ManeuverBonus>(c => {
                    c.Type = CombatManeuver.Grapple;
                    c.Mythic = false;
                    c.Bonus = 4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionBreakFree() {
                            UseCMB = true,
                            UseCMD = false,
                            Success = Helpers.CreateActionList(
                                new ContextActionRemoveSelf()
                                ),
                            Failure = Helpers.CreateActionList(),
                            //UseOverrideDC = true,
                            //OverridenDC = new ContextValue() { }
                        }
                        );
                });
                bp.m_Icon = BearGrappleTargetBuff.Icon;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });

            var TrickShotGrappleBuff = Helpers.CreateBuff("TrickShotGrappleBuff", bp => {
                bp.SetName("Trick Shot - Grapple");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the grapple combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = PummelingStyleBuff.m_Icon;
                bp.AddComponent<ManeuverBonus>(c => {
                    c.Type = CombatManeuver.Grapple;
                    c.Mythic = false;
                    c.Bonus = -4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
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
                    c.Action = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Grapple,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = TrickShotGrappleTargetBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                }
                                ),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = true;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var TrickShotTripBuff = Helpers.CreateBuff("TrickShotTripBuff", bp => {
                bp.SetName("Trick Shot - Trip");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the trip combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = TripIcon;
                bp.AddComponent<ManeuverBonus>(c => {
                    c.Type = CombatManeuver.Trip;
                    c.Mythic = false;
                    c.Bonus = -4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
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
                    c.Action = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Trip,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = true;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });

            var TrickShotDisarmAbility = Helpers.CreateBlueprint<BlueprintAbility>("TrickShotDisarmAbility", bp => {
                bp.SetName("Trick Shot - Disarm");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the disarm combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = DisarmAction.m_Icon;                
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TrickShotDisarmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotSunderBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotBullRushBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotGrappleBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotTripBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
            });
            var TrickShotSunderAbility = Helpers.CreateBlueprint<BlueprintAbility>("TrickShotSunderAbility", bp => {
                bp.SetName("Trick Shot - Sunder");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the sunder combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = CrushingBlowFeature.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TrickShotSunderBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotDisarmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotBullRushBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotGrappleBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotTripBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
            });
            var TrickShotBullRushAbility = Helpers.CreateBlueprint<BlueprintAbility>("TrickShotBullRushAbility", bp => {
                bp.SetName("Trick Shot - Bull Rush");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the bull rush combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = ImprovedBullRushFeature.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TrickShotBullRushBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotDisarmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotSunderBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotGrappleBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotTripBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
            });
            var TrickShotGrappleAbility = Helpers.CreateBlueprint<BlueprintAbility>("TrickShotGrappleAbility", bp => {
                bp.SetName("Trick Shot - Grapple");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the grapple combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = PummelingStyleBuff.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TrickShotGrappleBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotDisarmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotSunderBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotBullRushBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotTripBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
            });
            var TrickShotTripAbility = Helpers.CreateBlueprint<BlueprintAbility>("TrickShotTripAbility", bp => {
                bp.SetName("Trick Shot - Trip");
                bp.SetDescription("The next arrow the archer fires that hits this round will also perform the trip combat maneuver, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = TripIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TrickShotTripBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotDisarmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotSunderBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotBullRushBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = TrickShotGrappleBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
            });

            var TrickShotDisarmFeature = Helpers.CreateBlueprint<BlueprintFeature>("TrickShotDisarmFeature", bp => {
                bp.SetName("Trick Shot - Disarm");
                bp.SetDescription("The archer can perform a disarm combat maneuver along with a bow attack, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = DisarmAction.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TrickShotDisarmAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TrickShotSunderFeature = Helpers.CreateBlueprint<BlueprintFeature>("TrickShotSunderFeature", bp => {
                bp.SetName("Trick Shot - Sunder");
                bp.SetDescription("The archer can perform a sunder combat maneuver along with a bow attack, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = CrushingBlowFeature.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TrickShotSunderAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TrickShotBullRushFeature = Helpers.CreateBlueprint<BlueprintFeature>("TrickShotBullRushFeature", bp => {
                bp.SetName("Trick Shot - Bull Rush");
                bp.SetDescription("The archer can perform a bull rush combat maneuver along with a bow attack, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = ImprovedBullRushFeature.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TrickShotBullRushAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Level = 11;
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var TrickShotGrappleFeature = Helpers.CreateBlueprint<BlueprintFeature>("TrickShotGrappleFeature", bp => {
                bp.SetName("Trick Shot - Grapple");
                bp.SetDescription("The archer can perform a grapple combat maneuver along with a bow attack, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = PummelingStyleBuff.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TrickShotGrappleAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Level = 11;
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = TrickShotGrappleCasterBuff.ToReference<BlueprintBuffReference>();
                });
            });
            var TrickShotTripFeature = Helpers.CreateBlueprint<BlueprintFeature>("TrickShotTripFeature", bp => {
                bp.SetName("Trick Shot - Trip");
                bp.SetDescription("The archer can perform a trip combat maneuver along with a bow attack, the archer performs this maneuver with a –4 penalty to his CMB.");
                bp.m_Icon = TripIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TrickShotTripAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Level = 11;
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                });
            });

            var TrickShotSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("TrickShotSelection", bp => {
                bp.SetName("Trick Shot Selection");
                bp.SetDescription("At 3rd level, an archer can choose one of the following combat maneuvers or actions: disarm or sunder. He can perform this " +
                    "action with a bow against any target within 30 feet, with a –4 penalty to his CMB. Every four levels beyond 3rd, he may choose an additional trick shot to learn. " +
                    "\nAt 11th level, he may also choose from the following combat maneuvers: bull rush, grapple, trip.");
                bp.AddFeatures(TrickShotDisarmFeature, TrickShotSunderFeature, TrickShotBullRushFeature, TrickShotGrappleFeature, TrickShotTripFeature);
                bp.Mode = SelectionMode.OnlyNew;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            AgileManeuversFeature.AddComponent<PrerequisiteFeature>(c => { 
                c.m_Feature = TrickShotSelection.ToReference<BlueprintFeatureReference>();
                c.Group = Prerequisite.GroupType.Any;
                c.CheckInProgression = false;
                c.HideInUI = false;
            });

            var ExpertArcherFeature = Helpers.CreateBlueprint<BlueprintFeature>("ExpertArcherFeature", bp => {
                bp.SetName("Expert Archer");
                bp.SetDescription("At 5th level, an archer gains a +1 bonus on attack and damage rolls with bows. This bonus increases by +1 for every four levels beyond 5th.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<WeaponGroupAttackBonus>(c => {
                    c.WeaponGroup = WeaponFighterGroup.Bows;
                    c.AttackBonus = 1;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.multiplyByContext = true;
                    c.contextMultiplier = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<WeaponGroupDamageBonus>(c => {
                    c.WeaponGroup = WeaponFighterGroup.Bows;
                    c.DamageBonus = 0;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.AdditionalValue = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 5;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] { FighterClass.ToReference<BlueprintCharacterClassReference>() };
                });
            });

            var SafeShotFeature = Helpers.CreateBlueprint<BlueprintFeature>("SafeShotFeature", bp => {
                bp.SetName("Safe Shot");
                bp.SetDescription("At 9th level, an archer does not provoke attacks of opportunity when making ranged attacks with a bow.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<PointBlankMaster>(c => {
                    c.Category = WeaponCategory.Shortbow;
                });
                bp.AddComponent<PointBlankMaster>(c => {
                    c.Category = WeaponCategory.Longbow;
                });
                bp.AddComponent<PointBlankMaster>(c => {
                    c.Category = WeaponCategory.LightCrossbow;
                });
                bp.AddComponent<PointBlankMaster>(c => {
                    c.Category = WeaponCategory.HeavyCrossbow;
                });
            });

            var EvasiveArcherFeature = Helpers.CreateBlueprint<BlueprintFeature>("EvasiveArcherFeature", bp => {
                bp.SetName("Evasive Archer");
                bp.SetDescription("At 13th level, an archer gains a +2 dodge bonus to AC against ranged attacks. This bonus increases to +4 at 17th level.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<ACBonusAgainstAttacks>(c => {
                    c.AgainstMeleeOnly = false;
                    c.AgainstRangedOnly = true;
                    c.OnlySneakAttack = false;
                    c.NotTouch = false;
                    c.IsTouch = false;
                    c.OnlyAttacksOfOpportunity = false;
                    c.ArmorClassBonus = 0;
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { FighterClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 16, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 4 }
                    };
                });
            });

            var VolleyAbility = Helpers.CreateBlueprint<BlueprintAbility>("VolleyAbility", bp => {
                bp.SetName("Volley ");
                bp.SetDescription("At 17th level, as a full-round action, an archer can make a single bow attack at his highest base attack bonus against any number of " +
                    "creatures in a 15-foot radius burst, making separate attack and damage rolls for each creature.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRangedAttack() {
                            SelectNewTarget = false,
                            AutoHit = false,
                            IgnoreStatBonus = false,
                            AutoCritThreat = false,
                            AutoCritConfirmation = false
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 15.Feet();
                    c.m_TargetType = TargetType.Enemy;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 14.Feet();
                });
                bp.AddComponent<AbilityCasterHasWeaponWithRangeType>(c => {
                    c.RangeType = WeaponRangeType.Ranged;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = ArcherVolleyIcon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Weapon;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.m_IsFullRoundAction = true;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var VolleyFeature = Helpers.CreateBlueprint<BlueprintFeature>("VolleyFeature", bp => {
                bp.SetName("Volley");
                bp.SetDescription("At 17th level, as a full-round action, an archer can make a single bow attack at his highest base attack bonus against any number of " +
                    "creatures in a 15-foot radius burst, making separate attack and damage rolls for each creature.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { VolleyAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var RangedDefenceCooldownBuff = Helpers.CreateBuff("RangedDefenceCooldownBuff", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var RangedDefenceEffectBuff = Helpers.CreateBuff("RangedDefenceEffectBuff", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("");
                bp.SetDescription("An archer may catch an arrow fired at him once per round as if he had the deflect arrows feat, but only while holding a bow. \nWhen an arrow " +
                    "is successfully caught, he may fire it along with his next fired arrow as an extra attack. You may only fire one additional arrow at a time from this feature.");
                bp.m_Icon = DeflectArrowsFeature.m_Icon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.OnlyHit = false;
                    c.OnlyOnFirstAttack = true;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Bows;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRangedAttack() {
                            SelectNewTarget = false,
                            AutoHit = false,
                            IgnoreStatBonus = false,
                            AutoCritThreat = false,
                            AutoCritConfirmation = false,
                            ExtraAttack = true,
                            FullAttack = false,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var RangedDefenseFeature = Helpers.CreateBlueprint<BlueprintFeature>("RangedDefenseFeature", bp => {
                bp.SetName("Ranged Defense");
                bp.SetDescription("At 19th level, an archer gains DR 5/— against ranged attacks. In addition, an archer may catch an arrow fired at him once per round as if " +
                    "he had the deflect arrows feat, but only while holding a bow. \nWhen an arrow is successfully caught, he may fire it along with his next fired arrow as an " +
                    "extra attack. You may only fire one additional arrow at a time from this feature.");
                bp.m_Icon = DeflectArrowsFeature.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.BypassedByMeleeWeapon = true;
                });
                bp.AddComponent<DeflectArrowsExpanded>(c => {
                    c.m_Restriction = DeflectArrowsExpanded.RestrictionType.Bow;
                });
                bp.AddComponent<AddTargetAttackRollTrigger>(c => {
                    c.OnlyHit = true;
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = RangedDefenceCooldownBuff.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.One,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
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
                            }
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = RangedDefenceEffectBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.D4,
                                DiceCountValue = 0,
                                BonusValue = 0
                            }
                        }
                        );
                });
            });

            ArcherArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, BraveryFeature),
                    Helpers.LevelEntry(3, ArmorTrainingFeature),
                    Helpers.LevelEntry(5, WeaponTrainingSelection),
                    Helpers.LevelEntry(6, BraveryFeature),
                    Helpers.LevelEntry(7, ArmorTrainingFeature),
                    Helpers.LevelEntry(9, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(10, BraveryFeature),
                    Helpers.LevelEntry(11, ArmorTrainingFeature),
                    Helpers.LevelEntry(13, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(14, BraveryFeature),
                    Helpers.LevelEntry(15, ArmorTrainingFeature),
                    Helpers.LevelEntry(17, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(18, BraveryFeature),
                    Helpers.LevelEntry(19, ArmorMasteryFeature)
            };
            ArcherArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, HawkeyeFeature),
                    Helpers.LevelEntry(3, TrickShotSelection),
                    Helpers.LevelEntry(5, ExpertArcherFeature),
                    Helpers.LevelEntry(7, TrickShotSelection),
                    Helpers.LevelEntry(9, SafeShotFeature),
                    Helpers.LevelEntry(11, TrickShotSelection),
                    Helpers.LevelEntry(13, EvasiveArcherFeature),
                    Helpers.LevelEntry(15, TrickShotSelection),
                    Helpers.LevelEntry(17, VolleyFeature),
                    Helpers.LevelEntry(19, TrickShotSelection, RangedDefenseFeature)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Archer")) { return; }
            FighterClass.m_Archetypes = FighterClass.m_Archetypes.AppendToArray(ArcherArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
