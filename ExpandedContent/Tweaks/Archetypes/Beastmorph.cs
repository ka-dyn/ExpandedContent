using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.UnitSettings.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpandedContent.Tweaks.Miscellaneous.NewActivatableAbilityGroups.NewActivatableAbilityGroupAdder;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Utility;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.Settings;
using Kingmaker.Blueprints.Items.Weapons;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Beastmorph {
        public static void AddBeastmorph() {

            var AlchemistClass = Resources.GetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var PoisonResistanceFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("c9022272c87bd66429176ce5c597989c");
            var PoisonImmunityFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("202af59b918143a4ab7c33d72c8eb6d5");
            var PersistentMutagenFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("75ba281feb2b96547a3bfb12ecaff052");
            var BeastShapeIIISpell = Resources.GetBlueprint<BlueprintAbility>("9b93040dad242eb43ac7de6bb6547030");
            var WebArea = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("fd323c05f76390749a8555b13156813d");
            var WebSpell = Resources.GetBlueprint<BlueprintAbility>("134cb6d492269aa4f8662700ef57449f");



            var BeastmorphArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("BeastmorphArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"BeastmorphArchetype.Name", "Beastmorph");
                bp.LocalizedDescription = Helpers.CreateString($"BeastmorphArchetype.Description", "Beastmorphs study the anatomy of monsters, " +
                    "learning how they achieve their strange powers. They use their knowledge to duplicate these abilities, but at the cost of " +
                    "taking on inhuman shapes when they use mutagens.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"BeastmorphArchetype.Description", "Beastmorphs study the anatomy of monsters, " +
                    "learning how they achieve their strange powers. They use their knowledge to duplicate these abilities, but at the cost of " +
                    "taking on inhuman shapes when they use mutagens.");
            });

            #region Beastform Features
            var BeastmorphBeastformMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformMutagenFeature", bp => {
                bp.SetName("Beastform Mutagen");
                bp.SetDescription("At 3rd level, a beastmorph’s mutagen causes him to take on animalistic features, such as a furry muzzle and pointed ears like a " +
                    "werewolf, scaly skin like a lizardfolk or sahuagin, or compound eyes and mandibles like a giant insect. Before using his mutagen the beastmorph " +
                    "may select one of the following additional effects for the duration. " +
                    "\nScent - Detect unseen foes within 15 feet by sense of smell, as if you had {g|Encyclopedia:Blindsense}blindsense{/g}. " +
                    "\nScaly Skin - Gain a +2 natural armor bonus to AC. " +
                    "\nCompound Eyes - Gain a +4 bonus to {g|Encyclopedia:Perception}perception{/g}.");                
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });
            var BeastmorphBeastformImprovedMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformImprovedMutagenFeature", bp => {
                bp.SetName("Improved Beastform Mutagen");
                bp.SetDescription("At 6th level, a beastmorph’s mutagen grants him additional abilities and options. He may select two different abilities at once before " +
                    "using his mutagen, and the following effects are added to the list of beastform options. " +
                    "\nSpeed - Your movement speed is increased by 10 feet, increasing to 20 feet at 10th level, and 30 feet at 14th level. " +
                    "\nBuoyancy - You are immune to ground effects and being tripped. ");
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => {
                    c.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;                    
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });
            var BeastmorphBeastformGreaterMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformGreaterMutagenFeature", bp => {
                bp.SetName("Greater Beastform Mutagen");
                bp.SetDescription("At 10th level, a beastmorph’s mutagen grants him more additional abilities and options. He may select three different abilities at once before " +
                    "using his mutagen, and the following effects are added to the list of beastform options. " +
                    "\nPounce - You can perform a full attack at the end of your charge. " +
                    "\nTrip - Make a free trip attempt on your first melee attack that hits each round. ");
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => {
                    c.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });
            var BeastmorphBeastformGrandMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformGrandMutagenFeature", bp => {
                bp.SetName("Grand Beastform Mutagen");
                bp.SetDescription("At 14th level, a beastmorph’s mutagen grants him more exotic abilities and options. He may select four different abilities at once before " +
                    "using his mutagen, and the following effects are added to the list of beastform options. " +
                    "\nBlindsense - Detect unseen foes within 30 feet, this range does not stack with the range from the scent mutagen. " +
                    "\nFerocity - When your hit point total is below 0 but you are not killed, you can fight on for 1 more {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "At the end of your next turn, unless brought to above 0 {g|Encyclopedia:HP}hit points{/g}, you immediately fall {g|Encyclopedia:Injury_Death}unconscious{/g}. " +
                    "\nWeb - You gain the ability to fire webs as if a spider, you must wait one minute between uses and the DC of the web is 10 + half your beastmorph level + " +
                    "constitution modifier.");
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => {
                    c.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });
            #endregion
            #region Beastform Effect Buffs
            var BeastmorphBeastformMutagenScentEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenScentEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Scent");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nDetect unseen foes within 15 feet by sense of smell, as if you had {g|Encyclopedia:Blindsense}blindsense{/g}.");
                bp.AddComponent<Blindsense>(c => {
                    c.Range = 15.Feet();
                    c.Blindsight = false;
                });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenScalesEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenScalesEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Scaly Skin");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nGain a +2 natural armor bonus to AC.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Value = 2;
                    c.Stat = StatType.AC;
                });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenEyesEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenEyesEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Compound Eyes");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nGain a +4 bonus to {g|Encyclopedia:Perception}perception{/g}.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 4;
                    c.Stat = StatType.SkillPerception;
                });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenSpeedEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenSpeedEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Speed");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYour movement speed is increased by 10 feet, increasing to 20 feet at 10th level, and 30 feet at 14th level.");
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
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { AlchemistClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 9, ProgressionValue = 10 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 13, ProgressionValue = 20 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 30 }
                    };

                });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenBuoyancyEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenBuoyancyEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Buoyancy");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou are immune to ground effects and being tripped.");
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => { c.Descriptor = SpellDescriptor.Ground; });
                bp.AddComponent<BuffDescriptorImmunity>(c => { c.Descriptor = SpellDescriptor.Ground; });
                bp.AddComponent<AddMechanicsFeature>(c => { c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.Flying; });
                bp.AddComponent<AddConditionImmunity>(c => { c.Condition = Kingmaker.UnitLogic.UnitCondition.Prone; });
                bp.AddComponent<ManeuverImmunity>(c => { c.Type = CombatManeuver.Trip; });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenPounceEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenPounceEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Pounce");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou can perform a full attack at the end of your charge.");
                bp.AddComponent<AddMechanicsFeature>(c => { c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.Pounce; });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenTripEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenTripEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Trip");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nMake a free trip attempt on your first melee attack that hits each round.");
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = true;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = true;
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
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenBlindsenseEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenBlindsenseEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Blindsense");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nDetect unseen foes within 30 feet, this range does not stack with the range from the scent mutagen.");
                bp.AddComponent<Blindsense>(c => {
                    c.Range = 30.Feet();
                    c.Blindsight = false;
                });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenFerocityEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenFerocityEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Ferocity");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nWhen your hit point total is below 0 but you are not killed, you can fight on for 1 more {g|Encyclopedia:Combat_Round}round{/g}.");
                bp.AddComponent<AddMechanicsFeature>(c => { c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.Ferocity; });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var BeastformMutagenWebCooldown = Helpers.CreateBuff("BeastformMutagenWebCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Web Ability Cooldown");
                bp.SetDescription("After use you cannot use the spiders web ability for 1 minute.");
                bp.m_Icon = WebSpell.Icon;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var BeastformMutagenWebAbility = Helpers.CreateBlueprint<BlueprintAbility>("BeastformMutagenWebAbility", bp => {
                bp.SetName("Spiders Web");
                bp.SetDescription("Web creates a many-layered mass of {g|Encyclopedia:Strength}strong{/g}, sticky strands. These strands trap those caught in them. Creatures caught within a web become grappled by " +
                    "the sticky fibers.\nAnyone in the effect's area when spider's web is cast must make a {g|Encyclopedia:Saving_Throw}Reflex save{/g}. If this save succeeds, the creature is inside the web but is " +
                    "otherwise unaffected. If the save fails, the creature gains the grappled condition, but can break free by making a {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g}" +
                    ", {g|Encyclopedia:Athletics}Athletics check{/g}, or {g|Encyclopedia:Mobility}Mobility check{/g} as a {g|Encyclopedia:Standard_Actions}standard action{/g} against the {g|Encyclopedia:DC}DC{/g} of this " +
                    "ability. The entire area of the web is considered difficult terrain. Anyone moving through the webs must make a Reflex save each {g|Encyclopedia:Combat_Round}round{/g}. Creatures that fail lose their " +
                    "movement and become grappled in the first square of webbing that they enter. Spiders are immune to this ability. \nAfter use you cannot use this ability for 1 minute.");
                bp.m_Icon = WebSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Close;
                bp.CanTargetEnemies = false;
                bp.CanTargetPoint = true;
                bp.CanTargetFriends = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "1 minute");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Reflex");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = WebArea.ToReference<BlueprintAbilityAreaEffectReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            }
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = BeastformMutagenWebCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Minutes,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                    },
                                    IsNotDispelable = true
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextCalculateAbilityParams>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Constitution;
                    c.StatTypeFromCustomProperty = false;
                    c.ReplaceCasterLevel = false;
                    c.CasterLevel = 0;
                    c.ReplaceSpellLevel = false;
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { BeastformMutagenWebCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityAoERadius>(c => {
                    c.m_Radius = new Feet() { m_Value = 20 };
                    c.m_TargetType = TargetType.Any;
                    c.m_CanBeUsedInTacticalCombat = false;
                    c.m_DiameterInCells = 0;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Ground | SpellDescriptor.MovementImpairing;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
            });
            var BeastmorphBeastformMutagenWebEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenWebEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Web");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou gain the ability to fire webs as if a spider, you must wait one minute between uses and the DC of the web is 10 + " +
                    "half your level + constitution modifier.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BeastformMutagenWebAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            #endregion
            #region Beastform Toggle Buffs
            var BeastmorphBeastformMutagenScentBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenScentBuff", bp => {
                bp.SetName("Beastform Mutagen - Scent Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenScalesBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenScalesBuff", bp => {
                bp.SetName("Beastform Mutagen - Scales Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenEyesBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenEyesBuff", bp => {
                bp.SetName("Beastform Mutagen - Eyes Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenSpeedBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenSpeedBuff", bp => {
                bp.SetName("Beastform Mutagen - Speed Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenBuoyancyBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenBuoyancyBuff", bp => {
                bp.SetName("Beastform Mutagen - Buoyancy Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenPounceBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenPounceBuff", bp => {
                bp.SetName("Beastform Mutagen - Pounce Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenTripBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenTripBuff", bp => {
                bp.SetName("Beastform Mutagen - Trip Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenBlindsenseBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenBlindsenseBuff", bp => {
                bp.SetName("Beastform Mutagen - Blindsense Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            }); 
            var BeastmorphBeastformMutagenFerocityBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenFerocityBuff", bp => {
                bp.SetName("Beastform Mutagen - Ferocity Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenWebBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenWebBuff", bp => {
                bp.SetName("Beastform Mutagen - Web Toggle");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            #endregion
            #region Beastform Activatable Ability
            var BeastmorphBeastformMutagenScent = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenScent", bp => {
                bp.SetName("Beastform Mutagen - Scent");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nDetect unseen foes within 15 feet by sense of smell, as if you had {g|Encyclopedia:Blindsense}blindsense{/g}.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenScentBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenScales = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenScales", bp => {
                bp.SetName("Beastform Mutagen - Scaly Skin");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nGain a +2 natural armor bonus to AC.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenScalesBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenEyes = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenEyes", bp => {
                bp.SetName("Beastform Mutagen - Compound Eyes");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nGain a +4 bonus to {g|Encyclopedia:Perception}perception{/g}.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenEyesBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenSpeed = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenSpeed", bp => {
                bp.SetName("Beastform Mutagen - Speed");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYour movement speed is increased by 10 feet, increasing to 20 feet at 10th level, and 30 feet at 14th level.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenSpeedBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });

                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = BeastmorphBeastformImprovedMutagenFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });

                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenBuoyancy = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenBuoyancy", bp => {
                bp.SetName("Beastform Mutagen - Buoyancy");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou are immune to ground effects and being tripped.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenBuoyancyBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });

                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = BeastmorphBeastformImprovedMutagenFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });

                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenPounce = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenPounce", bp => {
                bp.SetName("Beastform Mutagen - Pounce");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou can perform a full attack at the end of your charge.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenPounceBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });

                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = BeastmorphBeastformGreaterMutagenFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });

                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenTrip = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenTrip", bp => {
                bp.SetName("Beastform Mutagen - Trip");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nMake a free trip attempt on your first melee attack that hits each round.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenTripBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });

                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = BeastmorphBeastformGreaterMutagenFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });

                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenBlindsense = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenBlindsense", bp => {
                bp.SetName("Beastform Mutagen - Blindsense");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nDetect unseen foes within 30 feet, this range does not stack with the range from the scent mutagen.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenBlindsenseBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });

                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = BeastmorphBeastformGrandMutagenFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });

                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenFerocity = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenFerocity", bp => {
                bp.SetName("Beastform Mutagen - Ferocity");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nWhen your hit point total is below 0 but you are not killed, you can fight on for 1 more {g|Encyclopedia:Combat_Round}round{/g}.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenFerocityBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });

                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = BeastmorphBeastformGrandMutagenFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });

                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenWeb = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenWeb", bp => {
                bp.SetName("Beastform Mutagen - Web");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou gain the ability to fire webs as if a spider, you must wait one minute between uses and the DC of the web is 10 + " +
                    "half your level + constitution modifier.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = BeastmorphBeastformMutagenWebBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });

                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = BeastmorphBeastformGrandMutagenFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });

                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.BeastformMutagen;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var BeastmorphBeastformMutagenBase = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BeastmorphBeastformMutagenBase", bp => {
                bp.SetName("Beastform Mutagen");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. Before using his mutagen the beastmorph may select one of the following " +
                    "additional effects to gain for the duration of his mutagens effect.");
                bp.AddComponent<ActivatableAbilityVariants>(c => {
                    c.m_Variants = new BlueprintActivatableAbilityReference[] {
                        BeastmorphBeastformMutagenScent.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenScales.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenEyes.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenSpeed.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenBuoyancy.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenPounce.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenTrip.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenBlindsense.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenFerocity.ToReference<BlueprintActivatableAbilityReference>(),
                        BeastmorphBeastformMutagenWeb.ToReference<BlueprintActivatableAbilityReference>()
                    };
                });
                bp.AddComponent<ActivationDisable>();
                bp.m_AllowNonContextActions = false;
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_Buff = new BlueprintBuffReference();
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            #endregion
            #region Beastform Feature Fact Imput
            BeastmorphBeastformMutagenFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] {
                    BeastmorphBeastformMutagenScent.ToReference<BlueprintUnitFactReference>(),
                    BeastmorphBeastformMutagenScales.ToReference<BlueprintUnitFactReference>(),
                    BeastmorphBeastformMutagenEyes.ToReference<BlueprintUnitFactReference>(),
                    BeastmorphBeastformMutagenBase.ToReference<BlueprintUnitFactReference>()
                };
            });
            BeastmorphBeastformImprovedMutagenFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] {
                    BeastmorphBeastformMutagenSpeed.ToReference<BlueprintUnitFactReference>(),
                    BeastmorphBeastformMutagenBuoyancy.ToReference<BlueprintUnitFactReference>()
                };
            });
            BeastmorphBeastformGreaterMutagenFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] {
                    BeastmorphBeastformMutagenPounce.ToReference<BlueprintUnitFactReference>(),
                    BeastmorphBeastformMutagenTrip.ToReference<BlueprintUnitFactReference>()
                };
            });
            BeastmorphBeastformGrandMutagenFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] {
                    BeastmorphBeastformMutagenBlindsense.ToReference<BlueprintUnitFactReference>(),
                    BeastmorphBeastformMutagenFerocity.ToReference<BlueprintUnitFactReference>(),
                    BeastmorphBeastformMutagenWeb.ToReference<BlueprintUnitFactReference>()
                };
            });
            #endregion
            #region Mutagen Conditionals
            var MutagenAbilities = new BlueprintAbility[] {
                Resources.GetBlueprint<BlueprintAbility>("7af42862f58edcc4cb825862ff2a0d10"), //Str
                Resources.GetBlueprint<BlueprintAbility>("b11d314d60f7a38498d1ed6933fe1638"), //Dex
                Resources.GetBlueprint<BlueprintAbility>("859629f49f06cd04492b1ba455a9b31b"), //Con
                Resources.GetBlueprint<BlueprintAbility>("ac1680d36a079a44bb58946b9d98f3fa"), //GreaterStrDex
                Resources.GetBlueprint<BlueprintAbility>("499ba008d5bde104ea9a1f039b3796b2"), //GreaterStrCon
                Resources.GetBlueprint<BlueprintAbility>("78d953b296fb2154aa2c85e6e724ce56"), //GreaterDexStr
                Resources.GetBlueprint<BlueprintAbility>("73b97114bf2f9914bba305fc3f032468"), //GreaterDexCon
                Resources.GetBlueprint<BlueprintAbility>("c942e12092a433440bbc73965b842c8a"), //GreaterConStr
                Resources.GetBlueprint<BlueprintAbility>("c1e46599fcade78418ef1ada71c1f487"), //GreaterConDex
                Resources.GetBlueprint<BlueprintAbility>("8444394f44f56b842bc2252782fde2e0"), //GrandStrDex
                Resources.GetBlueprint<BlueprintAbility>("3c2ef5a7ef926044aa168b494f7b341f"), //GrandStrCon
                Resources.GetBlueprint<BlueprintAbility>("dd2070ece4664eb42854a564ed4dddce"), //GrandDexStr
                Resources.GetBlueprint<BlueprintAbility>("b79cf60fbaa644042b98efed62fdd4f9"), //GrandDexCon
                Resources.GetBlueprint<BlueprintAbility>("3b69541e21f2d2c4fbf5956d0bb52768"), //GrandConStr
                Resources.GetBlueprint<BlueprintAbility>("49d44c166d9f1294d8dbd78ef93df865"), //GrandConDex
                Resources.GetBlueprint<BlueprintAbility>("14230cf35ac2b5b45a93b13cfe478585"), //Int
                Resources.GetBlueprint<BlueprintAbility>("84a9092b8430a1344a3c8b002cc68e7f"), //Wis
                Resources.GetBlueprint<BlueprintAbility>("d2f7656742d00b3438d1a6ebe41e135d"), //Cha
                Resources.GetBlueprint<BlueprintAbility>("1c46157abba66934aace22a9a909dd13"), //GreaterIntWis
                Resources.GetBlueprint<BlueprintAbility>("7fe18aed4a7a8f44289d1b262f432c16"), //GreaterIntCha
                Resources.GetBlueprint<BlueprintAbility>("088854aeb48cc7d43a6478eeed44a895"), //GreaterWisInt
                Resources.GetBlueprint<BlueprintAbility>("888fca5c7c218874c99bf78ed4b938e8"), //GreaterWisCha
                Resources.GetBlueprint<BlueprintAbility>("bd98572b88bdea94d9eb23ccf06ecfd8"), //GreaterChaInt
                Resources.GetBlueprint<BlueprintAbility>("0d51cf14921631c40b17b6e6a3b6b1ab"), //GreaterChaWis
                Resources.GetBlueprint<BlueprintAbility>("210218888fd34884583576d5035f46ea"), //GrandIntWis
                Resources.GetBlueprint<BlueprintAbility>("094c3a16cd6e1124ca873bb96fc336c5"), //GrandIntCha
                Resources.GetBlueprint<BlueprintAbility>("4c81642195d090448b92dc4673d7bce7"), //GrandWisInt
                Resources.GetBlueprint<BlueprintAbility>("8f899ac4f1a140347a9d8049c9049125"), //GrandWisCha
                Resources.GetBlueprint<BlueprintAbility>("f9f179c3015a6564faef97adf87c4662"), //GrandChaInt
                Resources.GetBlueprint<BlueprintAbility>("21bd5d475d1526f41bc1d269e8932612"), //GrandChaWis
                Resources.GetBlueprint<BlueprintAbility>("17903369fcc1aa241a87ec264597aed5") //TrueMutagen
            };
            foreach (var ability in MutagenAbilities) {
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenScentBuff, BeastmorphBeastformMutagenScentEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenScalesBuff, BeastmorphBeastformMutagenScalesEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenEyesBuff, BeastmorphBeastformMutagenEyesEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenSpeedBuff, BeastmorphBeastformMutagenSpeedEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenBuoyancyBuff, BeastmorphBeastformMutagenBuoyancyEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenPounceBuff, BeastmorphBeastformMutagenPounceEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenTripBuff, BeastmorphBeastformMutagenTripEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenBlindsenseBuff, BeastmorphBeastformMutagenBlindsenseEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenFerocityBuff, BeastmorphBeastformMutagenFerocityEffectBuff);
                LazyBeastformConditional(ability, BeastmorphBeastformMutagenWebBuff, BeastmorphBeastformMutagenWebEffectBuff);
            }
            #endregion

            BeastmorphArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, PoisonResistanceFeature),
                    Helpers.LevelEntry(5, PoisonResistanceFeature),
                    Helpers.LevelEntry(8, PoisonResistanceFeature),
                    Helpers.LevelEntry(10, PoisonImmunityFeature),
                    Helpers.LevelEntry(14, PersistentMutagenFeature)
            };
            BeastmorphArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(3, BeastmorphBeastformMutagenFeature),
                    Helpers.LevelEntry(6, BeastmorphBeastformImprovedMutagenFeature),
                    Helpers.LevelEntry(10, BeastmorphBeastformGreaterMutagenFeature),
                    Helpers.LevelEntry(14, BeastmorphBeastformGrandMutagenFeature)
            };
            AlchemistClass.Progression.UIGroups = AlchemistClass.Progression.UIGroups.AppendToArray(
                Helpers.CreateUIGroup(BeastmorphBeastformMutagenFeature, BeastmorphBeastformImprovedMutagenFeature, BeastmorphBeastformGreaterMutagenFeature, BeastmorphBeastformGrandMutagenFeature)
            );
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Beastmorph")) { return; }
            AlchemistClass.m_Archetypes = AlchemistClass.m_Archetypes.AppendToArray(BeastmorphArchetype.ToReference<BlueprintArchetypeReference>());
        }

        private static void LazyBeastformConditional(BlueprintAbility mutagenAbility, BlueprintBuff toggleBuff, BlueprintBuff effectBuff) {
            mutagenAbility.GetComponent<AbilityEffectRunAction>().TemporaryContext(c => {
                c.Actions.Actions = c.Actions.Actions.AppendToArray(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.Or,
                            Conditions = new Condition[] {
                                new ContextConditionHasBuff() {
                                    m_Buff = toggleBuff.ToReference<BlueprintBuffReference>(), Not = false
                                }
                            }                            
                        },
                        IfTrue = Helpers.CreateActionList(
                            new ContextActionApplyBuff() {
                                m_Buff = effectBuff.ToReference<BlueprintBuffReference>(),
                                DurationValue = new ContextDurationValue() {
                                    Rate = DurationRate.TenMinutes,
                                    DiceType = DiceType.Zero,
                                    DiceCountValue = new ContextValue() {
                                        ValueType = ContextValueType.Simple,
                                        Value = 0,
                                        ValueRank = AbilityRankType.Default,
                                        ValueShared = AbilitySharedValue.Damage,
                                        Property = UnitProperty.None
                                    },
                                    BonusValue = new ContextValue() {
                                        ValueType = ContextValueType.Rank,
                                        Value = 0,
                                        ValueRank = AbilityRankType.Default,
                                        ValueShared = AbilitySharedValue.Damage,
                                        Property = UnitProperty.None
                                    },
                                }
                            }
                            ),
                        IfFalse = Helpers.CreateActionList()
                    }
                    );
            });
                    
        }
    }
}
