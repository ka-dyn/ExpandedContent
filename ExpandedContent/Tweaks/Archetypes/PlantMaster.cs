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
                        PlantFocusGiantFlytrap.ToReference<BlueprintUnitFactReference>(),//Stealth
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
