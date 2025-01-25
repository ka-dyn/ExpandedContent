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

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Beastmorph {
        public static void AddBeastmorph() {

            var AlchemistClass = Resources.GetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var PoisonResistanceFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("c9022272c87bd66429176ce5c597989c");
            var PoisonImmunityFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("202af59b918143a4ab7c33d72c8eb6d5");
            var PersistentMutagenFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("75ba281feb2b96547a3bfb12ecaff052");
            var BeastShapeIIISpell = Resources.GetBlueprint<BlueprintAbility>("9b93040dad242eb43ac7de6bb6547030");



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
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenScalesEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenScalesEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Scaly Skin");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nGain a +2 natural armor bonus to AC.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenEyesEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenEyesEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Compound Eyes");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nGain a +4 bonus to {g|Encyclopedia:Perception}perception{/g}.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenSpeedEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenSpeedEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Speed");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYour movement speed is increased by 10 feet, increasing to 20 feet at 10th level, and 30 feet at 14th level.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenBuoyancyEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenBuoyancyEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Buoyancy");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou are immune to ground effects and being tripped.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenPounceEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenPounceEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Pounce");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou can perform a full attack at the end of your charge.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenTripEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenTripEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Trip");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nMake a free trip attempt on your first melee attack that hits each round.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenBlindsenseEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenBlindsenseEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Blindsense");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nDetect unseen foes within 30 feet, this range does not stack with the range from the scent mutagen.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenFerocityEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenFerocityEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Ferocity");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nWhen your hit point total is below 0 but you are not killed, you can fight on for 1 more {g|Encyclopedia:Combat_Round}round{/g}.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var BeastmorphBeastformMutagenWebEffectBuff = Helpers.CreateBuff("BeastmorphBeastformMutagenWebEffectBuff", bp => {
                bp.SetName("Beastform Mutagen - Web");
                bp.SetDescription("A beastmorph’s mutagen causes him to take on animalistic features. " +
                    "\nYou gain the ability to fire webs as if a spider, you must wait one minute between uses and the DC of the web is 10 + " +
                    "half your beastmorph level constitution modifier.");
                bp.m_Icon = BeastShapeIIISpell.Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
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
                    "half your beastmorph level constitution modifier.");
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

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Beastmorph")) { return; }
            AlchemistClass.m_Archetypes = AlchemistClass.m_Archetypes.AppendToArray(BeastmorphArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
