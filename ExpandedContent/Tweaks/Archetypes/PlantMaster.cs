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
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.Enums;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UI.UnitSettings.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class PlantMaster {
        public static void AddPlantMaster() {

            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var HunterAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("446fe89490cab7d44957efebeb8cc2b1");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("65af7290b4efd5f418132141aaa36c1b");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");
            var AnimalCompanionSelectionHunter = Resources.GetBlueprint<BlueprintFeatureSelection>("715ac15eb8bd5e342bc8a0a3c9e3e38f");
            var HunterAnimalFocusFeature = Resources.GetBlueprint<BlueprintFeature>("443365823b7d6d14b8d12f4e7bce1077");
            var HunterOneWithTheWildFeature = Resources.GetBlueprint<BlueprintFeature>("c1e0f4ada7c673e4f8e5c57d1eea13d0");
            var HunterAnimalFocusResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("72c9cd6d5a1464447a882590715d2b23");
            var PlantShapeIIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeIII.jpg");

            var CompanionSaplingTreantFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionSaplingTreantFeature");
            var CompanionCrawlingMoundFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionCrawlingMoundFeature");
            

            var PlantMasterArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("PlantMasterArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"PlantMasterArchetype.Name", "Plant Master");
                bp.LocalizedDescription = Helpers.CreateString($"PlantMasterArchetype.Description", "Some hunters form a bond with plant life instead of an animal " +
                    "and take on those aspects instead. These hunters form potent bonds with plant creatures, and their leafy or fungal friends are more than capable " +
                    "of anything another hunter’s animal allies can accomplish.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"PlantMasterArchetype.Description", "Some hunters form a bond with plant life instead of an " +
                    "animal and take on those aspects instead. These hunters form potent bonds with plant creatures, and their leafy or fungal friends are more " +
                    "than capable of anything another hunter’s animal allies can accomplish.");                
            });

            var PlantMasterCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("PlantMasterCompanionSelection", bp => {
                bp.SetName("Plant Companion");
                bp.SetDescription("A plant master forms a mystic bond with a plant companion. This plant is a loyal companion that accompanies the palnt master on her adventures. " +
                    "Except for the companion being a creature of the plant type, drawn from the list of plant companions, this ability otherwise works like the standard " +
                    "hunter’s animal companion ability.");
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = HunterAnimalCompanionProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(CompanionSaplingTreantFeature, CompanionCrawlingMoundFeature);
            });





            var PlantFocusAssassinVineBuffEffect = Helpers.CreateBuff("PlantFocusAssassinVineBuffEffect", bp => {
                bp.SetName("Plant Focus - AssassinVine");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusAssassinVineEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });
            var PlantFocusBramblesBuffEffect = Helpers.CreateBuff("PlantFocusBramblesBuffEffect", bp => {
                bp.SetName("Plant Focus - Brambles");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusBramblesEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });
            var PlantFocusCreepingVineBuffEffect = Helpers.CreateBuff("PlantFocusCreepingVineBuffEffect", bp => {
                bp.SetName("Plant Focus - CreepingVine");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusCreepingVineEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });
            var PlantFocusGiantFlytrapBuffEffect = Helpers.CreateBuff("PlantFocusGiantFlytrapBuffEffect", bp => {
                bp.SetName("Plant Focus - GiantFlytrap");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusGiantFlytrapEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });
            var PlantFocusMushroomBuffEffect = Helpers.CreateBuff("PlantFocusMushroomBuffEffect", bp => {
                bp.SetName("Plant Focus - Mushroom");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusMushroomEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });
            var PlantFocusOakBuffEffect = Helpers.CreateBuff("PlantFocusOakBuffEffect", bp => {
                bp.SetName("Plant Focus - Oak");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusOakEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });
            var PlantFocusShriekerBuffEffect = Helpers.CreateBuff("PlantFocusShriekerBuffEffect", bp => {
                bp.SetName("Plant Focus - Shrieker");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusShriekerEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });
            var PlantFocusSporeBuffEffect = Helpers.CreateBuff("PlantFocusSporeBuffEffect", bp => {
                bp.SetName("Plant Focus - Spore");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PlantFocusSporeEffect.ToReference<BlueprintFeatureReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
            });

            var PlantFocusAssassinVineBuff = Helpers.CreateBuff("PlantFocusAssassinVineBuff", bp => {
                bp.SetName("Plant Focus - Assassin Vine");
                bp.SetDescription("The creature gains a +2 inherent bonus to combat maneuver checks to grapple. This bonus increases to +4 at 8th level and +6 at 15th level.");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusAssassinVineBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusAssassinVineEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )                            
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusAssassinVineEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var PlantFocusBramblesBuff = Helpers.CreateBuff("PlantFocusBramblesBuff", bp => {
                bp.SetName("Plant Focus - Brambles");
                bp.SetDescription("When the creature is hit by an unarmed strike or natural attack, the attacker takes 1 point of piercing damage. " +
                    "\nThis damage increases to 2 points at 8th level and 3 points at 15th level");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusBramblesBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusBramblesEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusBramblesEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var PlantFocusCreepingVineBuff = Helpers.CreateBuff("PlantFocusCreepingVineBuff", bp => {
                bp.SetName("Plant Focus - Creeping Vine");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Athletics}Athletics checks{/g}. " +
                    "This bonus increases to +6 at 8th level and +8 at 15th level.");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusCreepingVineBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusCreepingVineEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusCreepingVineEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var PlantFocusGiantFlytrapBuff = Helpers.CreateBuff("PlantFocusGiantFlytrapBuff", bp => {
                bp.SetName("Plant Focus - Giant Flytrap");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Stealth}Stealth checks{/g}. " +
                    "This bonus increases to +6 at 8th level and +8 at 15th level.");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusGiantFlytrapBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusGiantFlytrapEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusGiantFlytrapEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var PlantFocusMushroomBuff = Helpers.CreateBuff("PlantFocusMushroomBuff", bp => {
                bp.SetName("Plant Focus - Mushroom");
                bp.SetDescription("The creature gains a +4 inherent bonus on saves against poison. This bonus increases to +6 at 8th level and +8 at 15th level." +
                    "\nWhile plant companions are already immune to posion, this inherent bonus can be shared with the hunter.");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusMushroomBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusMushroomEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusMushroomEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var PlantFocusOakBuff = Helpers.CreateBuff("PlantFocusOakBuff", bp => {
                bp.SetName("Plant Focus - Oak");
                bp.SetDescription("The creature gains a +2 inherent bonus to {g|Encyclopedia:CMD}CMD{/g}. This bonus increases to +4 at 8th level and +6 at 15th level.");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusOakBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusOakEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusOakEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var PlantFocusShriekerBuff = Helpers.CreateBuff("PlantFocusShriekerBuff", bp => {
                bp.SetName("Plant Focus - Shrieker");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Perception}Perception checks{/g}, increasing to +6 at 8th level. " +
                    "At 15th level, the creature also gains blindsense with a range of 10 feet, making invisibility and {g|Encyclopedia:Concealment}concealment{/g} (even magical darkness) " +
                    "irrelevant to this creature.");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusShriekerBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusShriekerEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusShriekerEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var PlantFocusSporeBuff = Helpers.CreateBuff("PlantFocusSporeBuff", bp => {
                bp.SetName("Plant Focus - Spore");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Mobility}Mobility checks{/g}. " +
                    "This bonus increases to +6 at 8th level and +8 at 15th level.");
                bp.m_Icon = ??;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsPetDead() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = PlantFocusSporeBuffEffect.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            AsChild = true,
                                            SameDuration = true
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionRepeatedActions() {
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionsOnPet() {
                                            AllPets = false,
                                            PetType = PetType.AnimalCompanion,
                                            Actions = Helpers.CreateActionList(
                                                new ContextActionAddFeature() {
                                                    m_PermanentFeature = PlantFocusSporeEffect.ToReference<BlueprintFeatureReference>(),
                                                    m_SetRankFrom = null
                                                }
                                                )
                                        }
                                        ),
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        }
                                    }
                                }
                                )
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRepeatedActions() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionsOnPet() {
                                    AllPets = false,
                                    PetType = PetType.AnimalCompanion,
                                    Actions = Helpers.CreateActionList(
                                        new RemoveFact() {
                                            Unit = new ContextTargetUnit(),
                                            m_Fact = PlantFocusSporeEffect.ToReference<BlueprintUnitFactReference>()
                                        }
                                        )
                                }
                                ),
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 3
                            }
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        HunterClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 7, ProgressionValue = 1 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 20, ProgressionValue = 3 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });

            var PlantFocusAssassinVine = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusAssassinVine", bp => {
                bp.SetName("Plant Focus - Assassin Vine");
                bp.SetDescription("The creature gains a +2 inherent bonus to combat maneuver checks to grapple. This bonus increases to +4 at 8th level and +6 at 15th level.");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusAssassinVineBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantFocusBrambles = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusBrambles", bp => {
                bp.SetName("Plant Focus - Brambles");
                bp.SetDescription("When the creature is hit by an unarmed strike or natural attack, the attacker takes 1 point of piercing damage. " +
                    "\nThis damage increases to 2 points at 8th level and 3 points at 15th level");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusBramblesBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantFocusCreepingVine = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusCreepingVine", bp => {
                bp.SetName("Plant Focus - Creeping Vine");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Athletics}Athletics checks{/g}. " +
                    "This bonus increases to +6 at 8th level and +8 at 15th level.");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusCreepingVineBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantFocusGiantFlytrap = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusGiantFlytrap", bp => {
                bp.SetName("Plant Focus - Giant Flytrap");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Stealth}Stealth checks{/g}. " +
                    "This bonus increases to +6 at 8th level and +8 at 15th level.");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusGiantFlytrapBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantFocusMushroom = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusMushroom", bp => {
                bp.SetName("Plant Focus - Mushroom");
                bp.SetDescription("The creature gains a +4 inherent bonus on saves against poison. This bonus increases to +6 at 8th level and +8 at 15th level." +
                    "\nWhile plant companions are already immune to posion, this inherent bonus can be shared with the hunter.");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusMushroomBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantFocusOak = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusOak", bp => {
                bp.SetName("Plant Focus - Oak");
                bp.SetDescription("The creature gains a +2 inherent bonus to {g|Encyclopedia:CMD}CMD{/g}. This bonus increases to +4 at 8th level and +6 at 15th level.");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusOakBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantFocusShrieker = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusShrieker", bp => {
                bp.SetName("Plant Focus - Shrieker");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Perception}Perception checks{/g}, increasing to +6 at 8th level. " +
                    "At 15th level, the creature also gains blindsense with a range of 10 feet, making invisibility and {g|Encyclopedia:Concealment}concealment{/g} (even magical darkness) " +
                    "irrelevant to this creature.");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusShriekerBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantFocusSpore = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusSpore", bp => {
                bp.SetName("Plant Focus - Spore");
                bp.SetDescription("The creature gains a +4 competence {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Mobility}Mobility checks{/g}. " +
                    "This bonus increases to +6 at 8th level and +8 at 15th level.");
                bp.m_Icon = ??;
                bp.m_Buff = PlantFocusSporeBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
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
                bp.Group = ActivatableAbilityGroup.HunterAnimalFocus;
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                //bp.DoNotTurnOffOnRest = true; This is false on the animal focuses, need to test
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });

            var PlantFocusHunterActivatable = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantFocusHunterActivatable", bp => {
                bp.SetName("Plant Focus");
                bp.SetDescription("At 1st level, the hunter can grant an plant aspect to her plant companion. " +
                    "Unlike with the hunter herself, there is no duration on the plant aspect applied to her plant companion. " +
                    "An aspect applied in this way remains in effect until the hunter changes it. If the hunter's plant companion is dead, " +
                    "the hunter can apply her companion's plant focus to herself instead of her plant companion. Additionally, a hunter " +
                    "can take on the aspect of an plant as a {g|Encyclopedia:Swift_Action}swift action{/g}. She gets the same benefits " +
                    "as the current plant companion focus. The hunter can use this ability for a number of minutes per day equal to her " +
                    "level. This duration does not need to be consecutive, but must be spent in 1-minute increments. For the purposes " +
                    "of features and abilities that interact with animal focus, plant focuses are animal focuses.");
                bp.AddComponent<ActivatableAbilityVariants>(c => {
                    c.m_Variants = new BlueprintActivatableAbilityReference[] {
                        PlantFocusAssassinVine.ToReference<BlueprintActivatableAbilityReference>(),//Grapple
                        PlantFocusBrambles.ToReference<BlueprintActivatableAbilityReference>(),//Piercing
                        PlantFocusCreepingVine.ToReference<BlueprintActivatableAbilityReference>(),//Athletics
                        PlantFocusGiantFlytrap.ToReference<BlueprintActivatableAbilityReference>(),//Stealth
                        PlantFocusMushroom.ToReference<BlueprintActivatableAbilityReference>(),//PoisonSaves
                        PlantFocusOak.ToReference<BlueprintActivatableAbilityReference>(),//CMD
                        PlantFocusShrieker.ToReference<BlueprintActivatableAbilityReference>(),//Sight
                        PlantFocusSpore.ToReference<BlueprintActivatableAbilityReference>(),//Mobility
                        HunterPlantFocusForHimself.ToReference<BlueprintActivatableAbilityReference>()
                    };
                });
                bp.AddComponent<ActivationDisable>();
                bp.m_AllowNonContextActions = false;
                bp.m_Icon = PlantShapeIIIIcon;
                bp.m_Buff = new BlueprintBuffReference();
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var PlantMasterPlantFocusFeature = Helpers.CreateBlueprint<BlueprintFeature>("PlantMasterPlantFocusFeature", bp => {
                bp.SetName("Plant Focus");
                bp.SetDescription("At 1st level, the hunter can grant an plant aspect to her plant companion. " +
                    "Unlike with the hunter herself, there is no duration on the plant aspect applied to her plant companion. " +
                    "An aspect applied in this way remains in effect until the hunter changes it. If the hunter's plant companion is dead, " +
                    "the hunter can apply her companion's plant focus to herself instead of her plant companion. Additionally, a hunter " +
                    "can take on the aspect of an plant as a {g|Encyclopedia:Swift_Action}swift action{/g}. She gets the same benefits " +
                    "as the current plant companion focus. The hunter can use this ability for a number of minutes per day equal to her " +
                    "level. This duration does not need to be consecutive, but must be spent in 1-minute increments. For the purposes " +
                    "of features and abilities that interact with animal focus, plant focuses are animal focuses.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = HunterAnimalFocusResource;
                    c.RestoreAmount = true;
                    c.RestoreOnLevelUp = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        PlantFocusAssassinVine.ToReference<BlueprintUnitFactReference>(),//Grapple
                        PlantFocusBrambles.ToReference<BlueprintUnitFactReference>(),//Piercing
                        PlantFocusCreepingVine.ToReference<BlueprintUnitFactReference>(),//Athletics
                        PlantFocusGiantFlytrap.ToReference<BlueprintUnitFactReference>(),//Stealth
                        PlantFocusMushroom.ToReference<BlueprintUnitFactReference>(),//PoisonSaves
                        PlantFocusOak.ToReference<BlueprintUnitFactReference>(),//CMD
                        PlantFocusShrieker.ToReference<BlueprintUnitFactReference>(),//Sight
                        PlantFocusSpore.ToReference<BlueprintUnitFactReference>(),//Mobility
                        HunterPlantFocusForHimself.ToReference<BlueprintUnitFactReference>(),
                        PlantFocusHunterActivatable.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Icon = HunterAnimalFocusFeature.Icon;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
            });

            PlantMasterArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, AnimalCompanionSelectionHunter, HunterAnimalFocusFeature),
                    Helpers.LevelEntry(17, HunterOneWithTheWildFeature)
            };
            PlantMasterArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, PlantMasterCompanionSelection, PlantMasterPlantFocusFeature),
                    Helpers.LevelEntry(17, PlantMasterPlantShieldFeature)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Plant Master")) { return; }
            HunterClass.m_Archetypes = HunterClass.m_Archetypes.AppendToArray(PlantMasterArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
